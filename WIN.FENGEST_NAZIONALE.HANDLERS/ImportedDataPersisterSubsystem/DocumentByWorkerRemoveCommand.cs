using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.Exceptions;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem
{
    internal class DocumentByWorkerRemoveCommand: ICommand
    {
         private IPersistenceFacade _persistence;
         private Worker _worker;
         private Export _export;

        public DocumentByWorkerRemoveCommand(IPersistenceFacade f, Worker worker, Export export)
        {
            _persistence = f;
            _worker = worker;
            _export = export;

        }
 

        public void Execute()
        {
            try
            {
                _persistence.ExecuteNonQuery(string.Format("Delete from documenti where Id_Lavoratore = {0} and Id_Provincia = {1}", _worker.Id.ToString(), _export.Province.Id.ToString()));
            }
            catch (Exception ex)
            {
                throw new UpdateDocumentException(_worker, ExceptionType.DeleteElement, ex);
               
            }
            
        }

    }
}
