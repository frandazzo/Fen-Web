using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Serializzation;
using WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport
{
    public class FenealwebLiberiImportHandler
    {
         private LiberiTrace _trace;
        private string _errorLogDir = "";
        private string _validator = "";
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;
        private ImportOptions _op;

        private ExportError _error;

        public ExportError FenealwebError
        {
            get
            {
                return _error;
            }
        }

        public FenealwebLiberiImportHandler(LiberiTrace trace)
        {
            //instanzio il servizio di persistenza e quello per la gestione geografica
            _persistence = DataAccessServices.Instance().PersistenceFacade;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(_persistence));
            _geo = WIN.BASEREUSE.GeoLocationFacade.Instance();

            //Recupero il file delle opzioni di importazione
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

            FileInfo f = new FileInfo(path);

            path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");


            _op = ObjectXMLSerializer<ImportOptions>.Load(path);

            if (_op != null)
            {
                _errorLogDir = _op.ErrorLogDir;
                _validator = _op.Validator;
            }
            else
            {
                throw new Exception("File opzioni non trovato!");
            }

            _trace = trace;
        }


        public bool Import( EventLog log)
        {
            log.WriteEntry("****************************************************************", EventLogEntryType.Information);
             
            log.WriteEntry("***************************************INVIO DATI NON ISCRITTI FENEALWEB *************************", EventLogEntryType.Information);
                
            //prima di partire devo verificare se esiste una company in fenealweb per l provincia
            //che sta esportando
            int fenealwebCompanyId = CheckIfFenealwebCompanyExist(_trace.Provincia);
            if (fenealwebCompanyId <= 0)
            {
                log.WriteEntry("Nessun dato non iscritti da importare: Territorio = " + _trace.Provincia + " non anagrafato in Fenealweb", EventLogEntryType.Warning);
                log.WriteEntry("****************************************************************", EventLogEntryType.Information);
                return true;
            }
                

            //per prima cosa eseguo la trasformazione della traccia arrivata
            log.WriteEntry("Avvio la trasformazione della traccia liberi Fenealweb", EventLogEntryType.Information);
            ValidationFacade transformer = new ValidationFacade(_geo, new GeoElementChecker(_geo));

            transformer.FenealwebLiberiTransform(_trace, _validator, _errorLogDir, fenealwebCompanyId);

            //se la trasformazione non è andata a buon fine regitro l'errore
            if (!transformer.IsResultsValid)
            {
                log.WriteEntry("Errori nella trasformazione traccia Fenealweb per i liberi. La procedura termina", EventLogEntryType.Error);
                log.WriteEntry("****************************************************************", EventLogEntryType.Information);
                ExportError err = transformer.ExportError;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione finisce
                _error = err;
                return false;
            }
            log.WriteEntry("Trasformazione traccia liberi  Fenealweb OK", EventLogEntryType.Information);
            
            //se arrivo a questo punto la trasformazione è andata a buon fine
            //posso recuperare l'oggetto da inserire nel dominio
            RiepilogoQuotaDTO exp = transformer.FenealwebTransformedResult;

          
            try
            {

                log.WriteEntry("Invio dati liberi Fenealweb", EventLogEntryType.Information);
                //adesso posso inviare i dati a fenealweb
                HttpRequestManager man = new HttpRequestManager();
                man.SendQuotetofenealweb(exp, String.Format("{0}{1}", _op.FenealwebImportQuoteUrl.Replace("importquote", "importquoteliberi"), fenealwebCompanyId));
               
                log.WriteEntry("Invio dati liberi Fenealweb: OK", EventLogEntryType.Information);


                

                
                return true;

            }
            catch (Exception ex)
            {
                string errorType = "Errore nell'invio dei liberi al Fenealweb";
                log.WriteEntry("****************************************************************", EventLogEntryType.Information);

                log.WriteEntry(errorType + ": " + ex.Message, EventLogEntryType.Error);
                ErrorManager man = new ErrorManager(_errorLogDir);
                _error = man.CreateExportError(_trace, ErrorType.Unknown, errorType + "; " +ex.Message,false, false);
                PersisteAndNotifyError(_error);
                 return false;
            }
        }

        public int CheckIfFenealwebCompanyExist(string province)
        {
            string quey = String.Format(@"select c.id from fenealweb_company c 
                            inner join 
                            fenealweb_companies_provinces cp 
                            on c.id = cp.companyId

                            inner join tb_provincie p 
                            on  cp.provinceId = p.id

                            where p.descrizione = '{0}'", province.Replace("'", "''"));
            Object o = _persistence.ExecuteScalar(quey);

            if (o == null)
                return -1;

            try
            {
                return Convert.ToInt32(o);
            }
            catch (Exception)
            {

                return -1;
            }



        }

        private void PersisteAndNotifyError(ExportError err)
        {
            try
            {
                //persisto l'errore
                _persistence.InsertObject("ExportError", err);

                //invio una mail
                MailProvider m = new MailProvider(_op.SmtpServer, _op.SmtpAccount, _op.SmtpPassword, true, _op.SmtpMailFrom);

                StringBuilder b = new StringBuilder();
                b.AppendLine("Notifica automaticamente generata. Errore nella importazione liberi  Fenealweb");
                b.AppendLine("");
                b.AppendLine("Provincia: " + _trace.Provincia);
                b.AppendLine("Data importazione: " + DateTime.Now.ToLongDateString());
                b.AppendLine("Numero importazione: " + _trace.ExportNumber);
                b.AppendLine("");
                b.AppendLine("Tipo errore: " + err.ErrorType.ToString());
                b.AppendLine("Errore: " + err.ApplicationErrorMessage);


                string message = b.ToString();



                m.SendMail(_op.MailAdministrator, "Errore nel processo di importazione liberi Fenealweb", message);

                if (!string.IsNullOrEmpty(_trace.Mailto))
                    m.SendMail(_trace.Mailto, "Errore nel processo di importazione dati liberi Fenealweb", message);

            }
            catch (Exception)
            {
                //se l'errore arriva qui ci sono problemi molto seri!!!!
                //non faccio nulla;
            }

        }


    }
    
}
