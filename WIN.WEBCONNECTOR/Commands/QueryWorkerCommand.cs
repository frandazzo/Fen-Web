using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.Commands
{
   public  class QueryWorkerCommand : ICommand
    {
        #region ICommand Membri di

        public void Execute()
        {
            FrmQueryWorker frm = new FrmQueryWorker(ProgramParameters.Instance.FiscalCode);
            frm.ShowDialog();
        }

        #endregion
    }
}
