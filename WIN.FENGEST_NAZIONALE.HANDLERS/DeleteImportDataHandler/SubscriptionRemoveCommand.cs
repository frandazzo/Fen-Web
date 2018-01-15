using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.DeleteImportDataHandler
{
   public  class SubscriptionRemoveCommand : ICommand
    {
        private IPersistenceFacade _persistence;

        private int _exportId;

        public SubscriptionRemoveCommand(IPersistenceFacade f, int exportId)
        {
            _persistence = f;
            _exportId = exportId;

        }

        #region ICommand Membri di

        public void Execute()
        {
            _persistence.ExecuteNonQuery("Delete from iscrizioni where Id_Importazione = " + _exportId.ToString());
        }

        #endregion
    }
}
