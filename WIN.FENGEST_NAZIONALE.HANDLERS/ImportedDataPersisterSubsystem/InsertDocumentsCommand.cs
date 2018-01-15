using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.Exceptions;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem
{
    internal class InsertDocumentsCommand : ICommand
    {
         private IPersistenceFacade _persistence;
         private Worker _worker;
         private Export  _export;

         public InsertDocumentsCommand(IPersistenceFacade f, Worker worker, Export exportData)
        {
            _persistence = f;
            _worker= worker;
            _export = exportData;

        }
 

        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                //Cancello tutti i documenti dell'utente per lla provicnia selezionata
                DocumentByWorkerRemoveCommand c = new DocumentByWorkerRemoveCommand(_persistence, _worker, _export);
                c.Execute();


                // li inserisco tutti in transazione
                _persistence.BeginTransaction();

                foreach (Document  item in _worker .Documents )
	            {
                    _persistence .MarkNew (item);
            		 
	            }
                _persistence.CommitTransaction();

            }
            catch (Exception ex)
            {
                throw new UpdateDocumentException(_worker, ExceptionType.InsertElement , ex);
            }
        }

        #endregion
    }
}
