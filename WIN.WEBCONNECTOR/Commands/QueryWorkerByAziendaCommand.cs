using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.Commands
{
    public class QueryWorkerByAziendaCommand : ICommand
    {
        #region ICommand Membri di

        public void Execute()
        {
            FrmNomeAzienda frm = new FrmNomeAzienda(ProgramParameters.Instance.Azienda, true);
            frm.ShowDialog();
        }

        #endregion
    }
}
