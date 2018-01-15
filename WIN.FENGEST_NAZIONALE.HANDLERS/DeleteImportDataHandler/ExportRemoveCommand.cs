
using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.DeleteImportDataHandler
{
    public class ExportRemoveCommand : ICommand
    {
        private IPersistenceFacade _persistence;
        private int _exportId;

        public ExportRemoveCommand(IPersistenceFacade f, int exportId)
        {
            _persistence = f;
            _exportId = exportId;

        }

        public void Execute()
        {
            _persistence.ExecuteNonQuery("Delete from importazioni where id = " + _exportId.ToString());
        }

    }
}
