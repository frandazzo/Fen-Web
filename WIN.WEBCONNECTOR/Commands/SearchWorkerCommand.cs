using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.Commands
{
    public class SearchWorkerCommand : ICommand
    {

        #region ICommand Membri di

        public void Execute()
        {
            FrmSearchData frm = new FrmSearchData();
            frm.ShowDialog();
        }

        #endregion
    }
}
