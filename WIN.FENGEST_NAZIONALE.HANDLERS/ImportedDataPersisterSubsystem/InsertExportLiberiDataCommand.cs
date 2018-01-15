using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.Exceptions;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem
{
    public class InsertExportLiberiDataCommand: ICommand
    {
         private IPersistenceFacade _persistence;
         private Export _export;

         public InsertExportLiberiDataCommand(IPersistenceFacade f, Export export)
        {
            _persistence = f;
            _export= export;

        }

    
        #region ICommand Membri di

        public void  Execute()
        {
            try
            {
                _persistence.InsertObject("ExportLiberi", _export);
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateExportException(_export, ExceptionType.InsertElement , ex);
            }
        }

#endregion
}
}
