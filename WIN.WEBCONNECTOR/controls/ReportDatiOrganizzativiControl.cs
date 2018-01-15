using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using WIN.WEBCONNECTOR.SharetopServices;
using WIN.WEBCONNECTOR.ServiceAgents;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.Data;

namespace WIN.WEBCONNECTOR.controls
{
    public partial class ReportDatiOrganizzativiControl : DevExpress.XtraEditors.XtraUserControl, IPrintableAndSearchable
    {

       
        int totalFilcaSindacalizedWorkers;
        int totalFilcaWorkers;


        int totalFilleaSindacalizedWorkers;
        int totalFilleaWorkers;

        int totalSindacalizedWorkers;
        int totalWorkers;

        int totalGroupFenealSindacalizedWorkers;
        int totalFenealdWorkers;

        int totalGroupFilcaSindacalizedWorkers;
        int totalFilcadWorkers;

        int totalGroupFilleaSindacalizedWorkers;
        int totalFilleadWorkers;



        public ReportDatiOrganizzativiControl(TestSharetoIntegration mainForm)
        {
            InitializeComponent();


            mainForm.lblFunction.Text = "Report dati organizzativi";
            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            mainForm.lblFilter.Text = "";
        }

        public void PrintReport(OrganizzativeData data)
        {
            if (nodatagroup.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                return;
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(link_CreateMarginalFooterArea);
            //link.CreateReportHeaderArea += new CreateAreaEventHandler(link_CreateReportHeaderArea);
            link.Component = gridControl1;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

        private void link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "", Color.DarkBlue,

               new RectangleF(0, 0, 100, 20), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Far;

            brick.Alignment = BrickAlignment.Far;

            brick.AutoWidth = true;
        }

        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            DevExpress.XtraPrinting.TextBrick brick1;

            brick1 = e.Graph.DrawString("               Report organizzativo", Color.Navy, new RectangleF(0, 0, 500, 40), DevExpress.XtraPrinting.BorderSide.None);

            brick1.Font = new Font("Tahoma", 16);

            brick1.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);

            brick1.VertAlignment = DevExpress.Utils.VertAlignment.Top;



            //DevExpress.XtraPrinting.TextBrick brick2;

            //brick2 = e.Graph.DrawString("Elenco rimesse delle strutture periferiche Feneal UIL \nal: " + AbstractMovimentoContabile.GetLastMovivimentoDate(gridControl1.DataSource as IList).ToShortDateString(), Color.Navy, new RectangleF(0, 25, 600, 31), DevExpress.XtraPrinting.BorderSide.None);

            //// brick2.Font = new Font("Tahoma", 10, FontStyle.Bold);

            //brick2.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);

            //brick2.VertAlignment = DevExpress.Utils.VertAlignment.Center;




            //DevExpress.XtraPrinting.TextBrick brick4;

            //brick4 = e.Graph.DrawString(orderBy, Color.Navy, new RectangleF(0, 50, 600, 25), DevExpress.XtraPrinting.BorderSide.None);

            //brick4.Font = new Font("Tahoma", 10, FontStyle.Bold);

            //brick4.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);

            //brick4.VertAlignment = DevExpress.Utils.VertAlignment.Bottom;




            DevExpress.XtraPrinting.TextBrick brick3;

            brick3 = e.Graph.DrawString("FENEAL U.I.L. \nSEGRETERIA NAZIONALE \nServizio Organizzazione", Color.Red, new RectangleF(0, 0, 150, 75), DevExpress.XtraPrinting.BorderSide.None);

            brick3.Font = new Font("Tahoma", 7, FontStyle.Bold);

            brick3.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Near);

            brick3.VertAlignment = DevExpress.Utils.VertAlignment.Top;
        }
     


        public void LoadData(SharetopServices.OrganizzativeData data)
        {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //qui aggiungo i dati per testare il servizio
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
          
        }

        private void simpleButton1_Click_2(object sender, EventArgs e)
        {
            SharetopIntegrationServiceAgent agent = new SharetopIntegrationServiceAgent();

            OrganizativeRecord[] list = null;

            try
            {
                list = agent.FindOrganizzativeData(Convert.ToInt32(comboBoxEdit1.Text));


                if (list == null)
                {
                    nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    return;

                }

                if (list.Length == 0)
                {
                    nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    return;

                }


                gridControl1.DataSource = list;
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            catch (Exception ex)
            {

                MessageBox.Show(String.Format("Errore nel caricamento dei dati: {0}", ex.Message));
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

        }

        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            //double partialRappresentativita;
            //double totalRappresentativita;
            //int totalSindacalizedWorkers;
            //int totalWorkers;

            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // Initialization 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {

                totalFilcaSindacalizedWorkers = 0;
                totalFilcaWorkers = 0;


                totalFilleaSindacalizedWorkers = 0;
                totalFilleaWorkers = 0;


                totalSindacalizedWorkers = 0;
                totalWorkers = 0;

                totalGroupFenealSindacalizedWorkers = 0;
                totalFenealdWorkers = 0;

                totalGroupFilcaSindacalizedWorkers = 0;
                totalFilcadWorkers = 0;

                totalGroupFilleaSindacalizedWorkers = 0;
                totalFilleadWorkers = 0;
            }
            // Calculation 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                int TotalRowSindacalizedWorkers = (int)View.GetRowCellValue(e.RowHandle, "TotalSindacalizedWorkers");
                int TotalRowFenealSindacalizedWorkers = (int)View.GetRowCellValue(e.RowHandle, "TotalSindacalizedWorkers");
                int TotalRowFilcaSindacalizedWorkers = (int)View.GetRowCellValue(e.RowHandle, "TotalSindacalizedWorkers");
                int TotalRowFilleaSindacalizedWorkers = (int)View.GetRowCellValue(e.RowHandle, "TotalSindacalizedWorkers");
                int TotalRowFenealdWorkers = (int)View.GetRowCellValue(e.RowHandle, "FenealWorkers");
                int TotalRowFilcadWorkers = (int)View.GetRowCellValue(e.RowHandle, "FilcaWorkers");
                int TotalRowFilleadWorkers = (int)View.GetRowCellValue(e.RowHandle, "FilleaWorkers");


                //int totalGroupFilcaSindacalizedWorkers;
                //int totalFilcadWorkers;

                //int totalGroupFilleaSindacalizedWorkers;
                //int totalFilleadWorkers;

                switch (summaryID)
                {
                    case 1: //the total feneal summary
                        totalSindacalizedWorkers = totalSindacalizedWorkers + TotalRowSindacalizedWorkers;
                        totalWorkers = totalWorkers + TotalRowFenealdWorkers;
                        break;
                    case 2: // The group summary. Feneal
                        totalGroupFenealSindacalizedWorkers = totalGroupFenealSindacalizedWorkers + TotalRowFenealSindacalizedWorkers;
                        totalFenealdWorkers = totalFenealdWorkers + TotalRowFenealdWorkers;
                        break;
                    case 3: // The group summary. Filca
                        totalGroupFilcaSindacalizedWorkers = totalGroupFilcaSindacalizedWorkers + TotalRowFilcaSindacalizedWorkers;
                        totalFilcadWorkers = totalFilcadWorkers + TotalRowFilcadWorkers;
                        break;
                    case 4: // The group summary. Fillea
                        totalGroupFilleaSindacalizedWorkers = totalGroupFilleaSindacalizedWorkers + TotalRowFilleaSindacalizedWorkers;
                        totalFilleadWorkers = totalFilleadWorkers + TotalRowFilleadWorkers;
                        break;
                    case 5: //the total filca summary
                        totalFilcaSindacalizedWorkers = totalFilcaSindacalizedWorkers + TotalRowSindacalizedWorkers;
                        totalFilcaWorkers = totalFilcaWorkers + TotalRowFilcadWorkers;
                        break;
                    case 6: //the total fillea summary
                        totalFilleaSindacalizedWorkers = totalFilleaSindacalizedWorkers + TotalRowSindacalizedWorkers;
                        totalFilleaWorkers = totalFilleaWorkers + TotalRowFilleadWorkers;
                        break;
                }
            }
            // Finalization 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = ((double)totalWorkers / totalSindacalizedWorkers) * 100;
                        break;
                    case 2:
                        e.TotalValue = ((double)totalFenealdWorkers / totalGroupFenealSindacalizedWorkers) * 100;
                        break;
                    case 3:
                        e.TotalValue = ((double)totalFilcadWorkers / totalGroupFilcaSindacalizedWorkers) * 100;
                        break;
                    case 4:
                        e.TotalValue = ((double)totalFilleadWorkers / totalGroupFilleaSindacalizedWorkers) * 100;
                        break;

                    case 5:
                        e.TotalValue = ((double)totalFilcaWorkers / totalFilcaSindacalizedWorkers) * 100;
                        break;

                    case 6:
                        e.TotalValue = ((double)totalFilleaWorkers / totalFilleaSindacalizedWorkers) * 100;
                        break;
                }
            }
        }
    }
}


