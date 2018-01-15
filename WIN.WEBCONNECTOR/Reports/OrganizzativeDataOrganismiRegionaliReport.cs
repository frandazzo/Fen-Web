using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using WIN.WEBCONNECTOR.SharetopServices;
using System.Collections.Generic;

namespace WIN.WEBCONNECTOR.Reports
{
    public partial class OrganizzativeDataOrganismiRegionaliReport : DevExpress.XtraReports.UI.XtraReport
    {
        public OrganizzativeDataOrganismiRegionaliReport()
        {
            InitializeComponent();
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           List< OrganizzativeData> d = this.DataSource as List<OrganizzativeData>;


            if (!String.IsNullOrEmpty(d[0].CongressoRegionale.BaseData.OperationResult.Error))
            {
                lblTipoStruttura.Text = "(Nessun dato trovato!)";
                return;
            }

            //if (d.Struttura.BaseData.Provincia == null)
            lblTipoStruttura.Text = String.Format("{0}, anno {1}", d[0].CongressoRegionale.BaseData.Regione.Descrizione, d[0].CongressoRegionale.BaseData.Anno);
            //else
            //    lblTipoStruttura.Text = String.Format("territoriale -- {0}", d.Struttura.BaseData.Provincia.Descrizione);

        }

    }
}
