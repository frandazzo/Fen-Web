using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.TECHNICAL.PERSISTENCE;

using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.ImportDataErrorHandling;
using WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.HANDLERS.DeleteImportDataHandler;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem
{
    public class PersisterFacade
    {

        
        private IPersistenceFacade _persistence;
        private string _errorLogPath = "";
        private GeoLocationFacade _geo;
        private LogDescriptor _log;
        private Export _exportData;


        public bool HasErrors
        {
           get
           {
               if (_log == null)
                   return false;
               return _log.HasErrors;
            }
        }


        public PersisterFacade(IPersistenceFacade f, GeoLocationFacade geo, string errorLogPath)
        {
            _persistence = f;
            _geo = geo;
            _errorLogPath = errorLogPath;
            
        }

        //public LogDescriptor Log
        //{
        //    get { return _log; }
        //}


        public ExportError Error
        {
            get
            {
                if (_log == null)
                    return null;

                if (!HasErrors)
                    return null;

                //Creo l'exportError
                int num = 0;
                if (_log.Errors != null)
                    num = _log.Errors.Length;

                ExportError err = new ExportError(ErrorType.PersistenceError, DateTime.Now, _log.CreateErrorLog(), "Errore nel sottosistema di persistenza dell'importazione! Numero di errori: " + num.ToString(), false, false,_exportData.ExportNumber );

                return err;
            }
        }

        public ExportError LiberiError
        {
            get
            {
                if (_log == null)
                    return null;

                if (!HasErrors)
                    return null;

                //Creo l'exportError
                int num = 0;
                if (_log.Errors != null)
                    num = _log.Errors.Length;

                ExportError err = new ExportError(ErrorType.LiberiPersistenceError, DateTime.Now, _log.CreateErrorLog(), "Errore nel sottosistema di persistenza dell'importazione! Numero di errori: " + num.ToString() + ".  -- " + _log.ConstructError() , false, false, _exportData.ExportNumber);

                return err;
            }
        }



        public void ImportData(Export exportData,ImportOptions options)
        {
            _exportData = exportData;
            //creo il log degli errori
            _log = new LogDescriptor(_errorLogPath, exportData);


            //inserisco o aggiorno la traccia
            InsertOrUpdateExport(exportData);

            //se c'è un errore a questo livello l'importazione termina
            if (_log.HasErrors)
                return;

            //Ciclo su tutti gli utenti e ne inserisco i dati
            foreach (Worker item in exportData.Workers)
            {
                InsertOrUpdateWorker(item);
                InsertOrUpdateSubscription(item.Subscription, item);
                UpdateWorkerDocuments(item, exportData );
            }

        }

        

        private void SendMailForLiberi(Export exportData, ImportOptions options)
        {
            try
            {
                string message = "";
                string err = "";
                if (_log != null)
                {
                    if (_log.HasErrors)
                    {
                        err = " con errori!";
                        message = "Importazione terminata " + err;
                    }
                    else
                    {
                        err = " senza errori!";
                        message = "Importazione terminata " + err;
                    }
                }
                else
                {
                    message = "Importazione nulla!";
                }

                MailProvider m = new MailProvider(options.SmtpServer, options.SmtpAccount, options.SmtpPassword, true, options.SmtpMailFrom);

                //if (!string.IsNullOrEmpty(exportData.ExporterMail))
                //    m.SendMail(exportData.ExporterMail, "Importazione Liberi " + exportData.Province.Descrizione + ";",  message);

                m.SendMail(options.MailAdministrator, String.Format("Importazione Liberi {0}",exportData.Province.Descrizione), message);


            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void UpdateWorkerDocuments(Worker worker, Export exportData)
        {
            InsertDocumentsCommand c = new InsertDocumentsCommand(_persistence, worker, exportData);

            try
            {
                c.Execute();
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
            
        }

        private void InsertOrUpdateSubscription(Subscription subscription, Worker worker)
        {
            try
            {
                //instanzio il componente per la verifica della iscrizione del lavoratore
                SubscriptionHandler h = new SubscriptionHandler(_persistence, _geo);
                //carico l'iscrizione se esiste
                h.LoadUniqueSubscription(subscription, worker);

                if (h.Found)
                {
                    //Se la trovo la aggiorno (ne ente, azienda, livello, quota )
                    UpdateSubscriptionCommand c = new UpdateSubscriptionCommand(_persistence , subscription,h.Subscription  );
                    c.Execute();
                }
                else
                {
                    //Se non la trovo la inserisco
                    InsertSubscriptionCommand c = new InsertSubscriptionCommand(_persistence, subscription);
                    c.Execute();
                }
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void InsertOrUpdateWorker(Worker worker)
        {
            try
            {
                //instanzio il componente per la verifica della presenza del lavoratore
                WorkerHandler h = new WorkerHandler(_persistence, _geo);
                //carico il lvavoratore se esiste
                h.LoadByFiscalCode(worker.CodiceFiscale);

                if (h.Found)
                {
                    //Se lo trovo lo aggiorno (ne aggiorno la provincia che modifica,
                    //telefono, indirizzo ,cap , comune se assenti)
                    UpdateWorkerCommand c = new UpdateWorkerCommand(_persistence, worker, h.CurrentWorker);
                    c.Execute();
                    
                }
                else
                {
                    //Se non lo trovo lo inserisco
                    InsertWorkerCommand c = new InsertWorkerCommand(_persistence, worker);
                    c.Execute();
                }
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void InsertOrUpdateExport(Export exportData)
        {
            try
            {
                //instanzio il componente per la verifica della presenza della traccia
                ExportHandler h = new ExportHandler(_persistence);
                //carico la traccia se esiste
                h.LoadUniqueExport(exportData.Province.Id, exportData.Settore, exportData.Period.InitialDate, exportData.Period.EndDate);

                if (h.Found)
                {
                    //Se la trovo la aggiorno (ne aggiorno la data di modifica)
                    UpdateExportCommand c = new UpdateExportCommand(_persistence, h.Export, exportData);
                    c.Execute();
                }
                else
                {
                    //Se non la trovo la inserisco
                    InsertExportCommand c = new InsertExportCommand(_persistence, exportData);
                    c.Execute();
                }
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        internal void ImportLiberiData(Export exportData, ImportOptions options)
        {
            _exportData = exportData;
            //creo il log degli errori
            _log = new LogDescriptor(_errorLogPath, exportData);


            //Non inserisco la traccia nel db
            InsertOrUpdateLiberiExport(exportData);

            if (_log.HasErrors)
                return;
           

            //Ciclo su tutti gli utenti e ne inserisco i dati 
            //rimuovendo prima tutti i libeir del territorio per quellente
            //ovviamnete rinuovo solamente se l'export number è 0 cioè ho iniziato una nuova esportazione
            if (exportData.ExportNumber == 0)
                RemoveLiberiFor(exportData.Province, exportData.NotSubscribers[0].Ente);
            foreach (Libero item in exportData.NotSubscribers)
            {
                InsertOrUpdateLibero(item);      
            }

            //se c'è un errore a questo livello l'importazione termina
            if (_log.HasErrors)
                return;
            //invio una mail al responsabile
            SendMailForLiberi(exportData, options);
        }

        private void RemoveLiberiFor(Provincia provincia, string ente)
        {
            try
            {

                //Se non la trovo la inserisco
                LiberiRemoveCommand c = new LiberiRemoveCommand(_persistence, provincia.Id, ente);
                c.Execute();

            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void InsertOrUpdateLibero(Libero item)
        {
            try
            {

                //Se non la trovo la inserisco
                InsertLiberoCommand c = new InsertLiberoCommand(_persistence, item);
                c.Execute();

            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void InsertOrUpdateLiberiExport(Export exportData)
        {
            try
            {
               
                    //Se non la trovo la inserisco
                    InsertExportLiberiDataCommand c = new InsertExportLiberiDataCommand(_persistence, exportData);
                    c.Execute();
               
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }
    }
}
