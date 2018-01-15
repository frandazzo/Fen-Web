using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.FenealgestServices;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;

namespace WIN.WEBCONNECTOR.Commands
{
    public class ExportDataCommand : ICommand
    {
        #region ICommand Membri di

        public void Execute()
        {
            WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace t = ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace>.Load(ProgramParameters.Instance.FileToExport);
            FrmViewData frm = new FrmViewData(t);
            frm.ShowDialog();
        }

        #endregion
    }
}
