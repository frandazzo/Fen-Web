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
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Net;
using System.IO;

namespace WIN.WEBCONNECTOR.controls
{
    public partial class RapprersentanzeControl : DevExpress.XtraEditors.XtraUserControl, IPrintableAndSearchable
    {
        public RapprersentanzeControl(TestSharetoIntegration mainForm)
        {
            InitializeComponent();


            mainForm.lblFunction.Text = "Rappresentanza";
            
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


        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colDataElezione")
            {
                Rappresentanza app = gridView1.GetRow(e.RowHandle) as Rappresentanza;
                if (app != null)
                {
                    if (app.DataElezione == DateTime.MinValue )
                        e.DisplayText = "";
                    else
                        e.DisplayText = app.DataElezione.ToShortDateString() ;
                }
            }
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);
                DoRowDoubleClick(view, pt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRowCell)
            {
                Rappresentanza label = view.GetRow(view.FocusedRowHandle) as Rappresentanza;
                if (label != null)
                {
                    if (!String.IsNullOrEmpty(label.UrlVerbale))
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            

                            //recupero l'estensione del file come ultima stringa prima del punto nell'url
                            string[] d = label.UrlVerbale.Split(new Char[] { '.' });
                            if (d.Length > 0)
                            {
                                string path = folderBrowserDialog1.SelectedPath;
                                string extension = d[d.Length - 1];
                                string filename = label.BaseData.Provincia.Descrizione + "." + extension;
                                path = Path.Combine(path, filename);
                                //WebClient webClient = new WebClient();
                                //webClient.DownloadFile(label.UrlVerbale, path);

                                WebClient webClient = new WebClient();
                                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                                webClient.DownloadFileAsync(new Uri(label.UrlVerbale), path);

                            }
                            else
                            {
                                MessageBox.Show("Nessun verbale scaricato perchè non è stato possibile trovare l'estensione del file nell'url!!");
                            }
                        }
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
          //progressBar.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
          MessageBox.Show("Download completato!");
        }

        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            DevExpress.XtraPrinting.TextBrick brick1;

            brick1 = e.Graph.DrawString("               Rappresentanze per azienda", Color.Navy, new RectangleF(0, 0, 500, 40), DevExpress.XtraPrinting.BorderSide.None);

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
            if (data == null)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            if (data.Rappresentanze == null)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;

            }
            if (data.Rappresentanze.Length == 0)
            {
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;

            }
            if (!string.IsNullOrEmpty(data.Rappresentanze[0].BaseData.OperationResult.Error))
            {
                MessageBox.Show(String.Format("Errore nel caricamento dei dati: {0}", data.Rappresentanze[0].BaseData.OperationResult.Error));
                nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }

            gridControl1.DataSource = data.Rappresentanze;
            nodatagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            datagroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
    }
}
