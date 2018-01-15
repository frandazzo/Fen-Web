using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using WIN.WEBCONNECTOR.SharetopServices;
using WIN.WEBCONNECTOR.Reports;
using DevExpress.XtraReports.UI;

namespace WIN.WEBCONNECTOR.controls
{
    public partial class OrganismiRegionaliControl : DevExpress.XtraEditors.XtraUserControl, IPrintableAndSearchable
    {
        public OrganismiRegionaliControl(TestSharetoIntegration mainForm)
        {
            InitializeComponent();


            mainForm.lblFunction.Text = "Organismi regionali";
            
        }



     


        public void PrintReport(OrganizzativeData data)
        {
            if (nodatagroup.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                return;
            OrganizzativeDataOrganismiRegionaliReport rep1 = new OrganizzativeDataOrganismiRegionaliReport();
            List<OrganizzativeData> s = new List<OrganizzativeData>();
            s.Add(data);
            rep1.DataSource = s;




            ReportPrintTool printTool = new ReportPrintTool(rep1);
            printTool.Report.CreateDocument(false);
            //printTool.PreviewForm.Load += new EventHandler(PreviewForm_Load);
            printTool.ShowPreviewDialog();
        }

   


        public void LoadData(SharetopServices.OrganizzativeData data)
        {
            if (data == null)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (data.CongressoRegionale == null)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;

            }
            if (!string.IsNullOrEmpty(data.CongressoRegionale.BaseData.OperationResult.Error))
            {
                MessageBox.Show(String.Format("Errore nel caricamento dei dati: {0}", data.CongressoRegionale.BaseData.OperationResult.Error));
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }

            //qui scrivo il codice per fare il binding con i controllini

            Regione regione = data.CongressoRegionale.BaseData.Regione;
            //Provincia territorio = data.CongressoRegionale.BaseData.Provincia;
            int anno = data.CongressoRegionale.BaseData.Anno;


            //if (territorio == null)
            txtDescrizione.Text = String.Format("Organismi regione {0} anno {1}", regione.Descrizione, anno);
            //else
            //    txtDescrizione.Text = String.Format("Dati struttura territoriale {0} ", territorio.Descrizione);
            txtseggen.Text = data.CongressoRegionale.SegretarioGenerale;
            txtsegorg.Text = data.CongressoRegionale.SegretarioOrganizzativo;
            txttes.Text = data.CongressoRegionale.Tesoriere;
            memseg.Text = data.CongressoRegionale.MembriSegretaria;
            memcons.Text = data.CongressoRegionale.ConsiglioTerritoriale;
            memconssupp.Text = data.CongressoRegionale.ConsiglioTerritorialeSupplente;
            memass.Text = data.CongressoRegionale.AssembleaTerritoriale;
            memasssupp.Text = data.CongressoRegionale.AssembleaTerritorialeSupplente;
            memrev.Text = data.CongressoRegionale.RevisoriDeiConti;
            mempro.Text = data.CongressoRegionale.Probiviri;
            


            nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
    }
}
