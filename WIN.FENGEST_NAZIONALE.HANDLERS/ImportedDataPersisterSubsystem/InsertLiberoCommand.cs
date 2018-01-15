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
    public class InsertLiberoCommand:ICommand
    {
            private IPersistenceFacade _persistence;
            private Libero _new;

            public InsertLiberoCommand(IPersistenceFacade f, Libero newWorker)
            {
                _persistence = f;
                _new = newWorker;
            }


        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                _persistence.InsertObject("Libero", _new);
            }
            catch (Exception ex)
            {
                throw new InsertLiberoException(_new, ExceptionType.InsertElement, ex);
            }
        }

        #endregion
    }
}
