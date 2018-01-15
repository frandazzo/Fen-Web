using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FenealgestWEB.Utility;
using WIN.BASEREUSE;
using System.Collections;
using DevExpress.Utils;
using FenealgestWEB.FenealgestUtilsService;
using System.Linq;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace FenealgestWEB.Reserved.AreaNazionale
{
    public partial class ReportIscritti : BaseReservedArea
    {
        public bool _redundance = false;


        public string DataSourceForPivot
        {
            get
            {
                if (_redundance == true)
                    return "SqlDataSource2";
                return "SqlDataSource1";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //esegue la verifica che un utetne è loggato;
            //inizializza le info base della pagina insieme con 
            //l'inizializzaizone del menu per l'utente
            InitializeBasePage();

            ////se è una pagina che richiede una autorizzazione
            ////posso decommentare il codice che segue
            if (!IsPostBack)
            {
                CheckUserIsAuthorized();

            }

            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();


           
        }


        protected override string AuthorizedFunction
        {
            get
            {
                return "Report iscritti";
            }
        }

        protected override string SitePagePath
        {
            get
            {
                return "Home -> Area riservata -> Nazionale -> Report iscritti";
            }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}


        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
        {
            get { return null; }
        }
        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }
        protected override void LoadPageInfo()
        {
            //string tempSubstring;
            ////imposto tutte le info necessarie alla creazione di tutte le informazioni della pagina
            //Label lbl = ((ReservedArea)this.Master).UtenteLabelPro;

            //tempSubstring = base.CurrentUser;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 20)
            //        tempSubstring = tempSubstring.Substring(0, 19) + "...";
            //lbl.Text = tempSubstring;


            //Label lbl1 = ((ReservedArea)this.Master).StrutturaLabelPro;
            //lbl1.Text = base.UserStructure;

            //Label lbl2 = ((ReservedArea)this.Master).HomeLabelPro;

            //tempSubstring = SitePagePath;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 20)
            //        tempSubstring = tempSubstring.Substring(0, 19) + "...";
            //lbl2.Text = tempSubstring;

            //Label lbl3 = ((ReservedArea)this.Master).DataLabelPro;
            //lbl3.Text = DateTime.Now.ToShortDateString();

            //Label lbl4 = ((ReservedArea)this.Master).OraLabelPro;
            //lbl4.Text = DateTime.Now.ToShortTimeString();

            //Label lbl5 = ((ReservedArea)this.Master).ScadenzaLabelPro;
            //lbl5.Text = base.UserDecayDataPassword.ToShortDateString();

            //Label lbl6 = ((ReservedArea)this.Master).RegioneLabelPro;

            //tempSubstring = base.UserRegion;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 15)
            //        tempSubstring = tempSubstring.Substring(0, 14) + "...";
            //lbl6.Text = tempSubstring;

            //Label lbl7 = ((ReservedArea)this.Master).ProvinciaLabelPro;

            //tempSubstring = base.UserProvice;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 15)
            //        tempSubstring = tempSubstring.Substring(0, 14) + "...";
            //lbl7.Text = tempSubstring;


            //LoadContextImage();
        }



        protected override string FunctionHeaderTitle
        {
            get { return "Report iscritti"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                loadCombos();
            }
        }



        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        private void loadCombos()
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            if (base.IsNationalUser)//utente nazionale
            {
                IList regioni = f.GetListaRegioni();

                cboRegioni.Items.Clear();
                cboRegioni.Items.Add("#");
                foreach (string item in regioni)
                {
                    cboRegioni.Items.Add(item);
                }
                //si mette sul primo elemento
                cboRegioni.SelectedIndex = 0;

                cboRegioni.Enabled = true;
                cboProvince.Enabled = true;

            }
            else if (string.IsNullOrEmpty(base.UserProvice)) // utente regionale
            {
                cboRegioni.Items.Clear();
                cboRegioni.Items.Add(base.UserRegion);

                IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
                cboProvince.Items.Clear();
                cboProvince.Items.Add("#");
                foreach (string item in province)
                {
                    cboProvince.Items.Add(item);
                }
                //si mette sul primo elemento
                cboProvince.SelectedValue = base.UserRegion;
                
                cboRegioni.Enabled = false;
                cboProvince.Enabled = true;

            }
            else //utente provinciale
            {
                cboRegioni.Items.Clear();
                cboRegioni.Items.Add(base.UserRegion);

                IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
                cboProvince.Items.Clear();
                cboProvince.Items.Add("#");
                foreach (string item in province)
                {
                    cboProvince.Items.Add(item);
                }
                //si mette sul primo elemento
                cboProvince.SelectedValue = base.UserProvice;

                cboRegioni.Enabled = false;
                cboProvince.Enabled = true;
            }

            int anno;
            for (int i = 0; i <= 50; i++)
            {
                anno = i + 2000;
                cboAnno.Items.Add(new ListItem(anno.ToString(), anno.ToString()));
            }
            cboAnno.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void cboRegioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            if (base.IsNationalUser)
            {
                IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
                cboProvince.Items.Clear();
                cboProvince.Items.Add("#");
                foreach (string item in province)
                {
                    cboProvince.Items.Add(item);
                }
                //si mette sul primo elemento
                cboProvince.SelectedIndex = 0;
            }

        }
        protected void buttonSaveAs_Click(object sender, EventArgs e)
        {
            Export(true);
        }

        private void Export(bool saveAs)
        {
            ASPxPivotGridExporter1.OptionsPrint.PrintHeadersOnEveryPage = true;
            ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False;
            ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True;
            ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = DefaultBoolean.True;
            ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = DefaultBoolean.False;

            string fileName = "PivotGrid";
            switch (DropDownList4.SelectedIndex)
            {
                case 0:
                    ASPxPivotGridExporter1.ExportPdfToResponse(fileName, saveAs);
                    break;
                case 1:
                    ASPxPivotGridExporter1.ExportXlsToResponse(fileName, saveAs);
                    break;
                case 2:
                    ASPxPivotGridExporter1.ExportMhtToResponse(fileName, "utf-8", "ASPxPivotGrid Printing Sample", true, saveAs);
                    break;
                case 3:
                    ASPxPivotGridExporter1.ExportRtfToResponse(fileName, saveAs);
                    break;
                case 4:
                    ASPxPivotGridExporter1.ExportTextToResponse(fileName, saveAs);
                    break;
                case 5:	// TODO
                    ASPxPivotGridExporter1.ExportHtmlToResponse(fileName, "utf-8", "ASPxPivotGrid Printing Sample", true, saveAs);
                    break;
            }
        }

        protected void chkRedundance_CheckedChanged(object sender, EventArgs e)
        {
            _redundance = chkRedundance.Checked;

            if (_redundance)
                SqlDataSource1.SelectCommand = "SELECT Id_Lavoratore, NomeProvinciaNascita, NomeProvincia, Settore, NomeRegione, NomeNazioneNascita, Ente, funDescEta(AnnoNascita, ?) AS Fascia_età FROM iscrizioni WHERE (Anno = ?) AND (NomeRegione = ? OR ? = '#' ) AND (NomeProvincia = ? OR ? = '#' ) GROUP BY Id_Lavoratore";
            else
                SqlDataSource1.SelectCommand = "SELECT Id_Lavoratore, NomeProvinciaNascita, NomeProvincia, Settore, NomeRegione, NomeNazioneNascita, Ente, funDescEta(AnnoNascita, ?) AS Fascia_età FROM iscrizioni WHERE (Anno = ?) AND (NomeRegione = ? OR ? = '#' ) AND (NomeProvincia = ? OR ? = '#' ) GROUP BY Id_Lavoratore, Settore, NomeProvincia";

            ASPxPivotGrid1.DataBind();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //ottengo la lista di tutte le province
            IList province = SessionServiceManager.GeoHandlerService.GetListaProvincie();
            //ottengo la lista di tutte le importaizoni


            List<Export> tacceAnnoCorrente = retrieveTracceAnnoCorrente();

            //tacceAnnoCorrente.Sort(
            //            delegate(ExportTrace p1, ExportTrace p2)
            //            {
            //                return p1.Province.CompareTo(p2.Province);
            //            }
            //    );
            tacceAnnoCorrente = tacceAnnoCorrente.OrderBy(o => o.Province).ToList();

            FenealgestUtilsService.ExcelDocument doc = new FenealgestUtilsService.ExcelDocument();
            doc.Rows = new ExcelRow[] { };
            doc.Rows = CreateRows(province, tacceAnnoCorrente);

            FenealgestUtils svc = new FenealgestUtils();
            byte[] result = svc.ExportDocumentToExcel(doc);

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=ReportInviiDbNazionale.xlsx");
            Response.OutputStream.Write(result, 0, result.Length);
            Response.Flush();

        }

        private ExcelRow[] CreateRows(IList province, List<Export> tacceAnnoCorrente)
        {

            ExcelRow[] r1 = new ExcelRow[]{};

            foreach (String item in province)
            {
                List<Export> tracceProvincia = tacceAnnoCorrente.FindAll(o => o.Province.Descrizione.Equals(item));

                ExcelRow r = new ExcelRow();
                r.Properties = new ExcelProperty[4];

                ExcelProperty p0 = new ExcelProperty();
                p0.Name = "Descrizione";
                p0.Value = item;
                p0.Priority = 1;
                r.Properties[0] = p0;

                ExcelProperty p1 = new ExcelProperty();
                p1.Name = "Settore Edile";
                p1.Value = tracceProvincia.Find(o => o.Settore.Equals("EDILE")) != null ? "OK" : "";
                p1.Priority = 2;
                r.Properties[1] = p1;

                ExcelProperty p2 = new ExcelProperty();
                p2.Name = "Settore Impianti fissi";
                p2.Value = tracceProvincia.Find(o => o.Settore.Equals("IMPIANTI FISSI")) != null ? "OK" : "";
                p2.Priority = 3;
                r.Properties[2] = p2;

                ExcelProperty p3 = new ExcelProperty();
                p3.Name = "Settore Inps";
                p3.Value = tracceProvincia.Find(o => o.Settore.Equals("INPS")) != null ? "OK" : "";
                p3.Priority = 4;
                r.Properties[3] = p3;

                Array.Resize<ExcelRow>(ref r1, r1.Length + 1);
                r1[r1.Length - 1] = r;
            }
            return r1;
        }

        private List<Export> retrieveTracceAnnoCorrente()
        {
            IList traces = SessionServiceManager.PersistentService.GetAllObjects("Export");
            List<Export> result = new List<Export>();
            //ottengo l'anno corrente
            int anno = retrieveAnnoCorrente();

            foreach (Export item in traces)
            {
                if (item.Period.Year == anno)
                    result.Add(item);
            }

            return result;
        }

        private int retrieveAnnoCorrente()
        {
            String anno = cboAnno.Text;
            int annoCorrente = DateTime.Now.Year;
            try
            {
                int.TryParse(anno, out annoCorrente);
            }
            catch (Exception)
            {
            }
            return annoCorrente;
        }
    }
}
