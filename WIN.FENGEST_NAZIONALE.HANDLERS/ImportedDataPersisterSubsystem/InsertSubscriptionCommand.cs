using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.Exceptions;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem
{
    internal class InsertSubscriptionCommand:ICommand
    {
            private IPersistenceFacade _persistence;
            private Subscription _new;

            public InsertSubscriptionCommand(IPersistenceFacade f, Subscription newsub)
            {
                _persistence = f;
                _new = newsub;
            }


        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                _persistence.InsertObject("Subscription", _new);
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateSubscriptionException(_new, ExceptionType.InsertElement  , ex);
            }
        }

        #endregion
    }
}
