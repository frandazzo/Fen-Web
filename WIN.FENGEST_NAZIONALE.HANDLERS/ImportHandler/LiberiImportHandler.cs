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
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler
{
    public class LiberiImportHandler
    {
        private LiberiTrace _trace;
        private string _errorLogDir = "";
        private string _validator = "";
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;
        private ImportOptions _op;



        public LiberiImportHandler(LiberiTrace trace)
        {

            //instanzio il servizio di persistenza e quello per la gestione geografica
            _persistence = DataAccessServices.Instance().PersistenceFacade;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(_persistence ));
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


        public void Import(EventLog log)
        {
            //per prima cosa eseguo la trasformazione della traccia arrivata
            LiberiValidationFacade transformer = new LiberiValidationFacade(_geo, new GeoElementChecker(_geo));

            transformer.Transform(_trace, _validator , _errorLogDir );

            //se la trasformazione non è andata a buon fine regitro l'errore
            if (!transformer.IsResultsValid)
            {
                ExportError err = transformer.ExportError;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione finisce
                return;
            }

            //puo' succedere che ci siano delle imperfezioni nei dati dei soli lavoratori
            //l'importante e' che non ci siano errori a livello di traccia
            if (!string.IsNullOrEmpty(_trace.Errore))
            {
                ExportError err = transformer.ExportError;
                
                //registro l'errore e continuo
                PersisteAndNotifyError(err);
            }

            //se arrivo a questo punto la trasformazione è andata a buon fine
            //posso recuperare l'oggetto da inserire nel dominio
            Export exp = transformer.TransformedResult;

            //instanzio il servizio per la persistenza dei dati di importazione
            PersisterFacade persister = new PersisterFacade(_persistence, _geo, _errorLogDir);
            persister.ImportLiberiData(exp, _op );

            //a questo punto verifico eventuali errori e li registro
            if (persister.HasErrors)
            {
                ExportError err = persister.LiberiError;
                 //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione termina
                return;
            }

            //invio tutto a fenealweb
            FenealwebLiberiImportHandler h = new FenealwebLiberiImportHandler(_trace);
            h.Import(log);


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
                b.AppendLine("Notifica automaticamente generata per un errore nella importazione liberi  al database nazionale");
                b.AppendLine("");

                b.AppendLine("Provincia: " + _trace.Provincia);
            
                b.AppendLine("Data importazione: " + DateTime.Now.ToShortDateString () );
                b.AppendLine("Numero importazione: " + _trace.ExportNumber);
                //b.AppendLine("Periodo da: " + _trace.InitialDate.ToShortDateString());
                //b.AppendLine("Periodo a: " + _trace.InitialDate.ToShortDateString());
                b.AppendLine("");
                b.AppendLine ("Tipo errore: " + err.ErrorType.ToString());
                b.AppendLine("Errore: " + err.ApplicationErrorMessage);
          

                string message = b.ToString ();

                

                m.SendMail(_op.MailAdministrator, "Errore nel processo di importazione dei liberi", message);

                if (!string.IsNullOrEmpty(_trace.Mailto))
                    m.SendMail(_trace.Mailto, "Errore nel processo di importazione dei liberi", message);

            }
            catch (Exception)
            {
                //se l'errore arriva qui ci sono problemi molto seri!!!!
                //non faccio nulla;
            }
            
        }



    }
}
