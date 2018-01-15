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
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport
{
    public class FenealwebImportHandler
    {
        private ExportTrace _trace;
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

        public FenealwebImportHandler(ExportTrace trace)
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


        public bool Import(int exportId, EventLog log)
        {
            //verifico che ci siano i fenealweb data 
            if (_trace.FenealwebData == null)
            {
                log.WriteEntry("Nessun dato da importare: FenealwebData = Null", EventLogEntryType.Information);
                return true;
            }
                
            //prima di partire devo verificare se esiste una company in fenealweb per l provincia
            //che sta esportando
            int fenealwebCompanyId = CheckIfFenealwebCompanyExist(_trace.Province);
            if (fenealwebCompanyId <= 0)
            {
                log.WriteEntry("Nessun dato da importare: Territorio = " + _trace.Province + " non anagrafato in Fenealweb", EventLogEntryType.Information);
                return true;
            }
                

            //per prima cosa eseguo la trasformazione della traccia arrivata
            log.WriteEntry("Avvio la trasformazione della traccia Fenealweb", EventLogEntryType.Information);
            ValidationFacade transformer = new ValidationFacade(_geo, new GeoElementChecker(_geo));

            transformer.FenealwebTransform(_trace, _validator, _errorLogDir, fenealwebCompanyId);

            //se la trasformazione non è andata a buon fine regitro l'errore
            if (!transformer.IsResultsValid)
            {
                log.WriteEntry("Errori nella trasformazione traccia Fenealweb. La procedura termina", EventLogEntryType.Error);
                ExportError err = transformer.ExportError;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione finisce
                _error = err;
                return false;
            }
            log.WriteEntry("Trasformazione traccia Fenealweb OK", EventLogEntryType.Information);
            //se arrivo a questo punto la trasformazione è andata a buon fine
            //posso recuperare l'oggetto da inserire nel dominio
            RiepilogoQuotaDTO exp = transformer.FenealwebTransformedResult;

          
            try
            {

                log.WriteEntry("Invio dati Fenealweb", EventLogEntryType.Information);
                //adesso posso inviare i dati a fenealweb
                HttpRequestManager man = new HttpRequestManager();
                man.SendQuotetofenealweb(exp, String.Format("{0}{1}", _op.FenealwebImportQuoteUrl, fenealwebCompanyId));
               
                log.WriteEntry("Invio dati Fenealweb: OK", EventLogEntryType.Information);


                if (_trace.ExportNumber == 0)
                {
                    if (exportId > 0)
                    {
                        //imposto la traccia corrente a synched
                        _persistence.ExecuteNonQuery("update importazioni set synched = b'1' where ID = " + exportId.ToString());
                        log.WriteEntry("Ho impostato la traccia " + exportId.ToString() + " come sincronizzata", EventLogEntryType.Information);
                    }
                    
                }

                
                return true;

            }
            catch (Exception ex)
            {
                string errorType = "Errore nell'invio della quota al Fenealweb";  

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
                b.AppendLine("Notifica automaticamente generata. Errore nella importazione Fenealweb");
                b.AppendLine("");
                b.AppendLine("Struttura: " + _trace.Struttura);
                b.AppendLine("Provincia: " + _trace.Province);
                b.AppendLine("Settore: " + _trace.Sector);
                b.AppendLine("Data importazione: " + _trace.ExportDate.ToShortDateString());
                b.AppendLine("Numero importazione: " + _trace.ExportNumber);
                b.AppendLine("Periodo da: " + _trace.InitialDate.ToShortDateString());
                b.AppendLine("Periodo a: " + _trace.InitialDate.ToShortDateString());
                b.AppendLine("");
                b.AppendLine("Tipo errore: " + err.ErrorType.ToString());
                b.AppendLine("Errore: " + err.ApplicationErrorMessage);


                string message = b.ToString();



                m.SendMail(_op.MailAdministrator, "Errore nel processo di importazione Fenealweb", message);

                if (!string.IsNullOrEmpty(_trace.ExporterMail))
                    m.SendMail(_trace.ExporterMail, "Errore nel processo di importazione dati Fenealweb", message);

            }
            catch (Exception)
            {
                //se l'errore arriva qui ci sono problemi molto seri!!!!
                //non faccio nulla;
            }

        }


    }
}
