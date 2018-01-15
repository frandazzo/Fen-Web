using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.Threading;
using WIN.WEBCONNECTOR.SharetopServices;
using WIN.WEBCONNECTOR.Reports;
using DevExpress.XtraReports.UI;

namespace WIN.WEBCONNECTOR.controls
{
    public partial class DatiStrutturaControl : DevExpress.XtraEditors.XtraUserControl, IPrintableAndSearchable
    {



        public DatiStrutturaControl(TestSharetoIntegration mainForm)
        {
            InitializeComponent();


            mainForm.lblFunction.Text = "Dati Struttura";
            
        }



        //private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.Column.Name == "colProvincia")
        //    {
        //        AbstractMovimentoContabile app = gridView1.GetRow(e.RowHandle) as AbstractMovimentoContabile;
        //        if (app != null)
        //        {
        //            if (app.Provincia == null)
        //                e.DisplayText = "";
        //            else
        //                e.DisplayText = app.Provincia.Descrizione;
        //        }
        //    }
        //    else if (e.Column.Name == "colRegione")
        //    {
        //        AbstractMovimentoContabile app = gridView1.GetRow(e.RowHandle) as AbstractMovimentoContabile;
        //        if (app != null)
        //        {
        //            if (app.Regione == null)
        //                e.DisplayText = "";
        //            else
        //                e.DisplayText = app.Regione.Descrizione;
        //        }
        //    }
        //    else if (e.Column.Name == "colCausale")
        //    {
        //        AbstractMovimentoContabile app = gridView1.GetRow(e.RowHandle) as AbstractMovimentoContabile;
        //        if (app != null)
        //        {
        //            if (app.Causale == null)
        //                e.DisplayText = "";
        //            else
        //                e.DisplayText = app.Causale.Descrizione;
        //        }
        //    }
        //    else if (e.Column.Name == "colImporto")
        //    {
        //        AbstractMovimentoContabile app = gridView1.GetRow(e.RowHandle) as AbstractMovimentoContabile;
        //        if (app != null)
        //        {

        //            e.DisplayText = app.Importo.ToString("n");
        //        }
        //    }
        //}

       
        //private void gridView1_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        Point pt = view.GridControl.PointToClient(Control.MousePosition);
        //        DoRowDoubleClick(view, pt);
        //    }
        //    catch (AccessDeniedException)
        //    {
        //        XtraMessageBox.Show("Impossibile accedere alla funzionalità richiesta. Accesso negato", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex);
        //    }
        //}

        //private void DoRowDoubleClick(GridView view, Point pt)
        //{
        //    GridHitInfo info = view.CalcHitInfo(pt);
        //    if (info.InRowCell)
        //    {
        //        AbstractMovimentoContabile label = view.GetRow(view.FocusedRowHandle) as AbstractMovimentoContabile;
        //        if (label != null)
        //            ShowDialogForm(label);
        //    }
        //}

       
      
       
       
        //private void cmdPrintList_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //gridControl1.ShowPrintPreview();
        //        if (gridView1.SortInfo.Count > 0)
        //        {
        //            GridColumnSortInfo i = gridView1.SortInfo[0];
        //            string sortType = "crescente";
        //            if (i.SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
        //                sortType = "decrescente";
        //            orderBy = string.Format("ordinato per {0} {1}", i.Column.FieldName.ToLower(), sortType);
        //        }
        //        PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
        //        link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
        //        link.CreateMarginalFooterArea += new CreateAreaEventHandler(link_CreateMarginalFooterArea);
        //        link.Component = gridControl1;
        //        link.PaperKind = System.Drawing.Printing.PaperKind.A4;
        //        link.ShowPreview();


        //    }
        //    catch (Exception ex)
        //    {
        //         XtraMessageBox.Show(ex.Message , "Errore",  MessageBoxButtons.OK ,  MessageBoxIcon.Error );
        //    }
        //}

        //void link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        //{

        //    PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "", Color.DarkBlue,

        //       new RectangleF(0, 0, 100, 20), BorderSide.None);

        //    brick.LineAlignment = BrickAlignment.Far;

        //    brick.Alignment = BrickAlignment.Far;

        //    brick.AutoWidth = true;
        //}

        //private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        //{

        //    //    PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.DateTime, "", Color.DarkBlue,

        //    //       new RectangleF(0, 0, 100, 20), BorderSide.None);

        //    //    brick.LineAlignment = BrickAlignment.Center;

        //    //    brick.Alignment = BrickAlignment.Center;

        //    //    brick.AutoWidth = true;
        //    DevExpress.XtraPrinting.TextBrick brick1;

        //    brick1 = e.Graph.DrawString("Quote di adesione contrattuale " + cboAnno.Text, Color.Navy, new RectangleF(0, 0, 620, 20), DevExpress.XtraPrinting.BorderSide.None);

        //    brick1.Font = new Font("Tahoma", 14);

        //    brick1.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);

        //    brick1.VertAlignment = DevExpress.Utils.VertAlignment.Top;



        //    DevExpress.XtraPrinting.TextBrick brick2;


        //    ColumnFilterInfo f = gridView1.Columns["Causale"].FilterInfo;
        //  //  MessageBox.Show(f.FilterString);

        //    string ind = "Industria";
        //    string art = "Artigianato";
        //    string coo = "Cooperazione";
        //    string reg = "Regionali";

        //    string label = "Industria - Artigianato - Cooperative - Regionali";

        //    if (!string.IsNullOrEmpty(f.FilterString))
        //    {
        //        if (f.FilterString.Contains(ind.ToUpper()))
        //            label = ind;
        //        else if (f.FilterString.Contains(art.ToUpper()))
        //            label = art;
        //        else if (f.FilterString.Contains(coo.ToUpper()))
        //            label = coo;
        //        else
        //            label = reg;
        //    }

        //    IList data = GetVisibileData();


        //    brick2 = e.Graph.DrawString("Riepilogo generale da \n" + label + " \n alla data: " + AbstractMovimentoContabile.GetLastMovivimentoDate(GetVisibileData()).ToShortDateString() + " " + orderBy, Color.Navy, new RectangleF(0, 20, 650, 50), DevExpress.XtraPrinting.BorderSide.None);

        //    brick2.Font = new Font("Tahoma", 10, FontStyle.Bold);

        //    brick2.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);

        //    brick2.VertAlignment = DevExpress.Utils.VertAlignment.Bottom;






        //    DevExpress.XtraPrinting.TextBrick brick3;

        //    brick3 = e.Graph.DrawString("FENEAL U.I.L. \nSEGRETERIA NAZIONALE \nServizio Amministrazione \nServizio Organizzazione", Color.Red, new RectangleF(0, 0, 150, 50), DevExpress.XtraPrinting.BorderSide.None);

        //    brick3.Font = new Font("Tahoma", 7, FontStyle.Bold);

        //    brick3.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Near);

        //    brick3.VertAlignment = DevExpress.Utils.VertAlignment.Bottom;


        //}

        //private IList GetVisibileData()
        //{
        //    IList l = new ArrayList();

        //    for (int i = 0; i < gridView1 .RowCount; i++)
        //    {
        //        int handle = gridView1.GetVisibleRowHandle(i);
        //        if (!gridView1.IsGroupRow(handle))
        //        {
        //            Quac c = gridView1.GetRow(handle) as Quac;
        //            if (c != null)
        //                l.Add(c);
        //        }
        //    }
          

        //    return l;
        //}







        public void PrintReport(OrganizzativeData data)
        {
            if (nodatagroup.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                return;
            OrganizzativeDataStrutturaReport rep1 = new OrganizzativeDataStrutturaReport();
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
                tabbedControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (data.Struttura == null)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabbedControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;

            }
            if (!string.IsNullOrEmpty(data.Struttura.BaseData.OperationResult.Error))
            {
                MessageBox.Show(String.Format("Errore nel caricamento dei dati: {0}",data.Struttura.BaseData.OperationResult.Error));
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabbedControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            
            //qui scrivo il codice per fare il binding con i controllini
            
            Regione regione = data.Struttura.BaseData.Regione;
            Provincia territorio = data.Struttura.BaseData.Provincia ;

            if (territorio == null)
                txtDescrizione.Text = String.Format("Dati struttura regionale {0} ", regione.Descrizione);
            else
                txtDescrizione.Text = String.Format("Dati struttura territoriale {0} ", territorio.Descrizione );


            txtRegione.Text = regione.Descrizione;
            if (territorio != null)
                txtProvincia.Text = territorio.Descrizione;
            txtIndirizzo.Text = data.Struttura.Indirizzo;
            txtMail.Text = data.Struttura.Mail;
            txtSitoInternet.Text = data.Struttura.SitoInternet;
            txtTel.Text = data.Struttura.RecapitiTelefonici;

            txtden.Text = data.Struttura.Denominazione;
            txtresp.Text = data.Struttura.Responsabile;
            txtFisc.Text = data.Struttura.Fiscale;
            data.Struttura.Altri =  data.Struttura.Altri.Replace("#", Environment.NewLine );
            txtalt.Text = data.Struttura.Altri;


            gridControl1.DataSource = data.Struttura.CoordinateBancarie;


            nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            tabbedControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
    }
}
