using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SERVICE_PROCESSOR;
using WIN.FENGEST_NAZIONALE.DOMAIN.Serializzation;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.TECHNICAL.MIDDLEWARE.QueueHandlers;
using System.Reflection;
using System.IO;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport;
using System.Diagnostics;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler
{
    public class QueueRetriever : IServiceProcessor
    {
        private EventLog _log;
        private ImportOptions _op;
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;

        public QueueRetriever()
        { 
            //recupero il file delle opzioni
            string EVENT_LOG_SOURCE = "FenealgestImportExportDataService2";
            string EVENT_LOG = "QueueRetrieverFenealgest";

            if (!System.Diagnostics.EventLog.SourceExists(EVENT_LOG_SOURCE))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    EVENT_LOG_SOURCE, EVENT_LOG);
            }



            _log = new EventLog();
            _log.Source = EVENT_LOG_SOURCE;
            _log.Log = EVENT_LOG;
            _log.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 5);
        
        }


        #region IServiceProcessor Membri di

        public void Process()
        {

            try
            {
                _log.WriteEntry("Avvio processo. Recupero il file delle opzioni", EventLogEntryType.Warning);
                //_op = ObjectXMLSerializer<ImportOptions>.Load(@"OpzioniImport.xml");

                //Recupero il file delle opzioni di importazione
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                FileInfo f = new FileInfo(path);

                path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");


                _op = ObjectXMLSerializer<ImportOptions>.Load(path);


                //in questo contesto prelevo l'oggetto
                //dalla coda e lo faccio processare;
                _log.WriteEntry("Avvio processo di recupero dalla coda", EventLogEntryType.Information);
                Object traceData = RetrieveFromQueue();

                ExportTrace trace = traceData as ExportTrace;

                if (trace != null)
                {
                    _log.WriteEntry("Ho ottenuto una traccia di importazione quote", EventLogEntryType.Information);
                    //instanzio il servizio
                    ImportHandler h = new ImportHandler(trace, _log);

                    //avvio l'importazione dei dati
                    _log.WriteEntry("Ne avvio l'import!", EventLogEntryType.Information);
                    h.Import(_log);

                    return;
                }

                LiberiTrace libTrace = traceData as LiberiTrace;

                //se non recupero nulla ritorno
                if (libTrace == null)
                    return;

                LiberiImportHandler ll = new LiberiImportHandler(libTrace);
                ll.Import(_log);


            }
            catch (Exception ex )
            {
                _log.WriteEntry("Errore irreversibile!" + ex.Message, EventLogEntryType.Error);
                NotifyError(ex.Message + Environment .NewLine + "Servizio non stoppato!");
            }
            
        }

        private Object RetrieveFromQueue()
        {
            QueueHandler h = new QueueHandler();

            TraceFromQueueRetriever q = new TraceFromQueueRetriever(h.CreateAndGetQueue (_op.QueueName ));

            q.receiveMessage (FailureHandlerFactory.CreateFailureHandler (FailureHandlerType.RetrySendToDead, _op.QueueName , _op.DeadQueueName ,_op.RetryQueueName ));


            


            ExportTrace t = q.Trace;
            if (t != null)
                return t;

            LiberiTrace t1 = q.LiberiTrace;
            if (t1 != null)
                return t1;

            return null;

        }

        public void NotifyError(string message)
        {
            try
            {
                MailProvider m = new MailProvider(_op.SmtpServer, _op.SmtpAccount, _op.SmtpPassword, true, _op.SmtpMailFrom);

                m.SendMail(_op.MailAdministrator, "Errore nel processo di elaborazione importazioni FenealgetWeb ", message);

            }
            catch (Exception)
            {
                
                //non fa nulla                
            }
            
        }

        #endregion

        public void Process1()
        {
            //instanzio il servizio di persistenza e quello per la gestione geografica
            _persistence = DataAccessServices.Instance().PersistenceFacade;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(_persistence));
            _geo = WIN.BASEREUSE.GeoLocationFacade.Instance();
        }
    }
}
