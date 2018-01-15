using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using WIN.WEBCONNECTOR.SharetopServices;
using System.Collections.Generic;

namespace WIN.WEBCONNECTOR.Reports
{
    public partial class OrganizzativeDataStrutturaReport : DevExpress.XtraReports.UI.XtraReport
    {
        public OrganizzativeDataStrutturaReport()
        {
            InitializeComponent();
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            List<OrganizzativeData> d = this.DataSource as List<OrganizzativeData>;


            if (!String.IsNullOrEmpty(d[0].Struttura.BaseData.OperationResult.Error))
            {
                lblTipoStruttura.Text = "(Nessun dato trovato!)";
                return;
            }

            if (d[0].Struttura.BaseData.Provincia == null)
                lblTipoStruttura.Text = String.Format("regionale -- {0}", d[0].Struttura.BaseData.Regione.Descrizione);
            else
                lblTipoStruttura.Text = String.Format("territoriale -- {0}", d[0].Struttura.BaseData.Provincia.Descrizione);

        }

        //private void OrganizzativeDataStrutturaReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    OrganizzativeData d = this.DataSource as OrganizzativeData;


        //    if (!String.IsNullOrEmpty(d.Struttura.BaseData.OperationResult.Error))
        //    {
        //        lblTipoStruttura.Text = "(Nessun dato trovato!)";
        //        return;
        //    }

        //    if (d.Struttura.BaseData.Provincia == null)
        //        lblTipoStruttura.Text = String.Format("regionale -- {0}", d.Struttura.BaseData.Regione.Descrizione);
        //    else
        //        lblTipoStruttura.Text = String.Format("territoriale -- {0}", d.Struttura.BaseData.Provincia.Descrizione);
        //}

    }
}
