using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem;
using WIN.FENGEST_NAZIONALE.DOMAIN.Serializzation;
using System.Reflection;
using System.IO;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport;
using System.Diagnostics;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler
{
    public class ImportHandler
    {
        private ExportTrace _trace;
        private string _errorLogDir = "";
        private string _validator = "";
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;
        private ImportOptions _op;

        private Export currentExport;
        public ImportHandler(ExportTrace trace, EventLog _log)
        {
            try
            {
                _log.WriteEntry("Entro nel costruttore di ImportHandler: Sto per instanziare il persistenceFacade", EventLogEntryType.Warning);
                //instanzio il servizio di persistenza e quello per la gestione geografica
                _persistence = DataAccessServices.Instance().PersistenceFacade;
                _log.WriteEntry("Sto per instanziare il geoFacade", EventLogEntryType.Warning);

                GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(_persistence));
                _geo = WIN.BASEREUSE.GeoLocationFacade.Instance();


                _log.WriteEntry("Sto per recuperare il file delle opzioni", EventLogEntryType.Warning);

                //Recupero il file delle opzioni di importazione
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
                FileInfo f = new FileInfo(path);
                path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");

                //recupero le opzioni
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
            catch (Exception ex)
            {
                StringBuilder b = new StringBuilder();
                b.AppendLine(ex.Message);
                b.AppendLine(ex.StackTrace);
                if (ex.InnerException != null)
                    b.AppendLine(ex.InnerException.Message);
                _log.WriteEntry(b.ToString(), EventLogEntryType.Error);

                throw ex;
            }
           

        }


        public ImportHandler(ExportTrace trace)
        {
          
            //instanzio il servizio di persistenza e quello per la gestione geografica
            _persistence = DataAccessServices.Instance().PersistenceFacade;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(_persistence ));
            _geo = WIN.BASEREUSE.GeoLocationFacade.Instance();

            //Recupero il file delle opzioni di importazione
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            FileInfo f = new FileInfo(path);
            path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");

            //recupero le opzioni
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
            //prima di importare i dati nel database nazione devo 
            //assicurami che non si tratti di quote previsionali
           
            bool previzionali = false;
            if (_trace.FenealwebData != null)
                if (_trace.FenealwebData.DocumentType.Equals("IQP"))
                {
                    previzionali = true;
                    log.WriteEntry("Si tratta di quote previsionali. non effettuerò import DB naz.", EventLogEntryType.Information);
                }
                   


            //importo i dati nel database nazionale
            bool correctOutcome = false;
            bool fenealwebCorrectOutcome = false;

            //se non si tratta di previzionale
            if (!previzionali){
                log.WriteEntry("Avvio import DB naz.", EventLogEntryType.Information);
                 correctOutcome = ImporDBNazionale(log);

                if (!correctOutcome)
                {
                    log.WriteEntry("Import DB naz. terminato con errori. Invio la mail di notifica ed esco", EventLogEntryType.Information);
                    //notifico con una mail di errore
                    string message = "Errori nel processo di importazione nel database nazionale. Non si proseguirà ad una eventuale importaizone Fenealweb";
                    SendMail(message);
                    return;
                }
            }else{
                //altrimenti indico come ok l'esportazione anche se no è stata fatta
                //cio' mi permette di continuare...
                correctOutcome = true;
                
            }

           

            //posso avviare l'integrazione con fenealweb
            if (correctOutcome)
            {
                //recupero adesso la tracca di esportazione
                log.WriteEntry("Avvio import Fenealweb", EventLogEntryType.Information);
                fenealwebCorrectOutcome = ImportFenealweb(log);

                if (!fenealwebCorrectOutcome)
                {
                    log.WriteEntry("Import Fenealweb terminato con errori. Invio la mail di notifica ed esco", EventLogEntryType.Information);
                    string message = "Errori nel processo di importazione in FenealWeb";
                    SendMail(message);
                    return;
                }

            }

            log.WriteEntry("Termine importazione", EventLogEntryType.Information);

            string message1 = "Importazione terminata con successo";
            SendMail(message1);
            return;

        }
        private void SendMail(string message)
        {
            try
            {
                

                String documentType = "IQA";
                String ente = "";
                if (_trace.FenealwebData != null){
                    documentType = _trace.FenealwebData.DocumentType;
                    ente = _trace.FenealwebData.Entity;
                }
                   



                MailProvider m = new MailProvider(_op.SmtpServer, _op.SmtpAccount, _op.SmtpPassword, true, _op.SmtpMailFrom);
                string subject = String.Format("Importazione {0} num: {1}; prov.: {2}; settore: {3} ({4}); periodo: da {5} a {6}" , 
                    documentType, 
                    currentExport.ExportNumber, 
                    currentExport.Province.Descrizione, 
                    currentExport.Settore,
                    ente,
                    currentExport.Period.InitialDate.ToShortDateString(),
                    currentExport.Period.EndDate.ToShortDateString());





                if (!string.IsNullOrEmpty(currentExport.ExporterMail))
                    m.SendMail(currentExport.ExporterMail,subject , message);

                m.SendMail(_op.MailAdministrator, subject, message);


            }
            catch (Exception ex)
            {
                //non fa nulla
            }
        }

        private bool ImportFenealweb(EventLog log)
        {
            if (currentExport != null)
            {
                //posso adesso avviare l'integrazione com Fenealweb
                FenealwebImportHandler h1 = new FenealwebImportHandler(_trace);
                return h1.Import(currentExport.Id, log);

            }
            else if (_trace.FenealwebData.DocumentType.Equals("IQP"))
            {
                FenealwebImportHandler h1 = new FenealwebImportHandler(_trace);
                return h1.Import(-1, log);
            }
           
            log.WriteEntry("Nessuna traccia da importare", EventLogEntryType.Information);
            return true;
        }

        private bool ImporDBNazionale(EventLog log)
        {
            //per prima cosa eseguo la trasformazione della traccia arrivata

            ValidationFacade transformer = new ValidationFacade(_geo, new GeoElementChecker(_geo));

            transformer.Transform(_trace, _validator, _errorLogDir);
            log.WriteEntry("Eseguo la trasformazione della traccia", EventLogEntryType.Information);

            //se la trasformazione non è andata a buon fine regitro l'errore
            if (!transformer.IsResultsValid)
            {
                log.WriteEntry("Ci sono degli errori nella trasformazione della traccia", EventLogEntryType.Error);
                ExportError err = transformer.ExportError;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione finisce
                
                return false;
            }

            //se arrivo a questo punto la trasformazione è andata a buon fine
            //posso recuperare l'oggetto da inserire nel dominio
            log.WriteEntry("Trasformazione della traccia: OK", EventLogEntryType.Information);
            currentExport = transformer.TransformedResult;


            //instanzio il servizio per la persistenza dei dati di importazione
            log.WriteEntry("Avvio il PersisterFacade", EventLogEntryType.Information);
            PersisterFacade persister = new PersisterFacade(_persistence, _geo, _errorLogDir);
            persister.ImportData(currentExport, _op);

            //a questo punto verifico eventuali errori e li registro
            if (persister.HasErrors)
            {
                log.WriteEntry("Si sono verificati errori nel servizio di persistenza dati", EventLogEntryType.Error);
                ExportError err = persister.Error;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione termina
                return false;
            }
            log.WriteEntry("Persistenza dati: OK", EventLogEntryType.Information);


            //se si tratta di una esportazione con numero 0 allora applico le notifiche a fenealweb
            //**********************************************************
            //**********************************************************
            //**********************************************************

            if (_trace.ExportNumber == 0 
                
                && _trace.Sector.Equals("EDILE"))
            {

                //notifico le importaizoni al db nazionale solamente se si tratta di quote iqa con settore edile
                log.WriteEntry("Invio notifiche Fenealweb", EventLogEntryType.Information);
                HttpRequestManager man = new HttpRequestManager();
                try
                {


                    if (_trace.FenealwebData != null)
                    {
                        if (_trace.FenealwebData.DocumentType.Equals("IQA"))
                        {
                            man.NotiFyFenealWebOfNewExport(String.Format("{0}{1}", _op.NotifyFenealwebUrl, currentExport.Id));
                        }
                    }
                    else
                    {
                        //se non ho dati notifico comunque
                        man.NotiFyFenealWebOfNewExport(String.Format("{0}{1}", _op.NotifyFenealwebUrl, currentExport.Id));
                    }
                        






                   
                }
                catch (Exception ex)
                {
                    log.WriteEntry("Errore durante l'invio delle notifiche a Fenealweb:" + ex.Message, EventLogEntryType.Error);
                    //questo è un errore non bloccante
                }
                
                log.WriteEntry("Invio notifiche Fenealweb: OK", EventLogEntryType.Information);
            }



            return true;
        }

        public Export CurrentExport
        {
            get
            {
                return currentExport;
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
                b.AppendLine("Notifica automaticamente generata per un errore nella importazione di dati al database nazionale");
                b.AppendLine("");
                b.AppendLine("Struttura: " + _trace.Struttura);
                b.AppendLine("Provincia: " + _trace.Province );
                b.AppendLine("Settore: " + _trace.Sector);
                b.AppendLine("Data importazione: " + _trace.ExportDate.ToShortDateString () );
                b.AppendLine("Numero importazione: " + _trace.ExportNumber);
                b.AppendLine("Periodo da: " + _trace.InitialDate.ToShortDateString());
                b.AppendLine("Periodo a: " + _trace.InitialDate.ToShortDateString());
                b.AppendLine("");
                b.AppendLine ("Tipo errore: " + err.ErrorType.ToString());
                b.AppendLine("Errore: " + err.ApplicationErrorMessage);
          

                string message = b.ToString ();

                

                m.SendMail(_op.MailAdministrator, "Errore nel processo di importazione", message);

                if (!string.IsNullOrEmpty (_trace.ExporterMail ))
                    m.SendMail(_trace.ExporterMail , "Errore nel processo di importazione", message);

            }
            catch (Exception)
            {
                //se l'errore arriva qui ci sono problemi molto seri!!!!
                //non faccio nulla;
            }
            
        }



    }
}
