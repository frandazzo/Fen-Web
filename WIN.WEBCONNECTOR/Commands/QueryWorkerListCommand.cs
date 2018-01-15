using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.Commands
{
    public class QueryWorkerListCommand : ICommand
    {
        #region ICommand Membri di

        public void Execute()
        {
            FrmQueryMultipleWorkers frm = new FrmQueryMultipleWorkers(ProgramParameters.Instance.FiscalCodeList);
            frm.ShowDialog();
        }

        #endregion
    }
}
