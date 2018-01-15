using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.Exceptions;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem
{
    internal class UpdateSubscriptionCommand:ICommand
    {
            private IPersistenceFacade _persistence;
            private Subscription _new;
            private Subscription _old;

            public UpdateSubscriptionCommand(IPersistenceFacade f, Subscription newsub, Subscription oldsub)
            {
                _persistence = f;
                _old = oldsub;
                _new = newsub;
            }


        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                _old.EmployCompany = _new.EmployCompany;
                _old.Entity  = _new.Entity;
                _old.Quota = _new.Quota ;
                _old.Level  = _new.Level ;

                _persistence.UpdateObject("Subscription", _old);
                _new.Key = _old.Key;
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateSubscriptionException(_old, ExceptionType.UpdateElement   , ex);
            }
           
        }

        #endregion

    }
}