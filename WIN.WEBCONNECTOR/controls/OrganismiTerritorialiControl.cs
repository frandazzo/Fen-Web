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
    public partial class OrganismiTerritorialiControl : DevExpress.XtraEditors.XtraUserControl, IPrintableAndSearchable
    {
        public OrganismiTerritorialiControl(TestSharetoIntegration mainForm)
        {
            InitializeComponent();


            mainForm.lblFunction.Text = "Organismi territoriali";
            
        }



     

        

        public void PrintReport(OrganizzativeData data)
        {
            if (nodatagroup.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                return;
            OrganizzativeDataOrganismiTerritorialiReport rep1 = new OrganizzativeDataOrganismiTerritorialiReport();
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
            if (data.CongressoTerritoriale == null)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;

            }
            if (!string.IsNullOrEmpty(data.CongressoTerritoriale.BaseData.OperationResult.Error))
            {
                MessageBox.Show(String.Format("Errore nel caricamento dei dati: {0}", data.CongressoTerritoriale.BaseData.OperationResult.Error));
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }

            //qui scrivo il codice per fare il binding con i controllini

           // Regione regione = data.CongressoRegionale.BaseData.Regione;
            Provincia territorio = data.CongressoTerritoriale.BaseData.Provincia;
            int anno = data.CongressoTerritoriale.BaseData.Anno;


            //if (territorio == null)
            //txtDescrizione.Text = String.Format("Organismi regione {0} anno {1}", regione.Descrizione, anno);
            //else
            txtDescrizione.Text = String.Format("Organismi territorio {0} anno {1}", territorio.Descrizione, anno);

                //txtDescrizione.Text = String.Format("Dati struttura territoriale {0} ", territorio.Descrizione);
                txtseggen.Text = data.CongressoTerritoriale.SegretarioGenerale;
                txtsegorg.Text = data.CongressoTerritoriale.SegretarioOrganizzativo;
                txttes.Text = data.CongressoTerritoriale.Tesoriere;
                memseg.Text = data.CongressoTerritoriale.MembriSegretaria;
                memcons.Text = data.CongressoTerritoriale.ConsiglioTerritoriale;
                memconssup.Text = data.CongressoTerritoriale.ConsiglioTerritorialeSupplente;
                memass.Text = data.CongressoTerritoriale.AssembleaTerritoriale;
                memasssupp.Text = data.CongressoTerritoriale.AssembleaTerritorialeSupplente;
                memrev.Text = data.CongressoTerritoriale.RevisoriDeiConti;
            



            nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
    }
}
