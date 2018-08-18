using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.TECHNICAL.PERSISTENCE;
using FenealgestWEB.Utility;
using WIN.FENGEST_NAZIONALE.HANDLERS.DeleteImportDataHandler;
using System.Text;

namespace FenealgestWEB.Reserved.AreaRegionale
{
    public partial class DettaglioTracciaImportazioni : BaseReservedArea
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //esegue la verifica che un utetne è loggato;
            //inizializza le info base della pagina insieme con 
            //l'inizializzaizone del menu per l'utente
            InitializeBasePage();

            ////se è una pagina che richiede una autorizzazione
            ////posso decommentare il codice che segue
            if (!IsPostBack)
                CheckUserIsAuthorized();

            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }

        protected string IntestazioneImportazione { get; set; }
        protected string DatiImportazione { get; set; }


        protected override string AuthorizedFunction
        {
            get { return "Visualizza lista importazioni"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Area regionale -> Dettaglio importazioni"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}

        protected override DevExpress.Web.ASPxTreeView DevPageTree
        {
            get { return null; }
        }

        protected override DevExpress.Web.ASPxMenu DevPageMenu
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
            get { return "Dettaglio importazioni"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;

                LoadImportazione(Request.QueryString["Id"]);
            }
        }

        private void LoadImportazione(string id)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Export ex = p.GetObject("Export", System.Convert.ToInt32(id)) as Export;

            //imposto l'interfaccia utente
            if (ex != null)
            {
                LoadObjectProperties(ex);
                return;
            }
            throw new Exception("Id dell'importazione non valido!");
        }

        private void LoadObjectProperties(Export ex)
        {
            //Valorizzo i campi del form legato all'importazione
            IntestazioneImportazione = string.Format("Importazione del <strong>{0}</strong> per la provincia di <strong>{1}</strong> (<strong>{2}</strong>)", ex.ExportDate.ToLongDateString(), ex.Province.ToString(), ex.Regione.ToString());

            DatiImportazione = CreateHtmlList(ex);

           


        }

        private string CreateHtmlList(Export ex)
        {

            string period;

            if (ex.Settore.Equals("EDILE"))
            {
                period = "(" + ex.Period.InitialDate.ToShortDateString() + "-" + ex.Period.EndDate.ToShortDateString() + ") Sem " + ex.Period.PeriodNumber + " " + ex.Period.Year.ToString();
            }
            else
            {
                period = "(" + ex.Period.InitialDate.ToShortDateString() + "-" + ex.Period.EndDate.ToShortDateString() + ")" + ex.Period.Year.ToString();
            }


            StringBuilder b = new StringBuilder();

            b.Append("<ul>");
                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Provincia: <font>" + ex.Province.ToString() + "</font>");
                b.Append("</li>");

                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Settore: <font>" + ex.Settore + "</font>");
                b.Append("</li>");

                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Periodo: <font>" + period + "</font>");
                b.Append("</li>");

                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Data esportazione: <font>" + ex.ExportDate.ToString() + "</font>");
                b.Append("</li>");

                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Responsabile: <font>" + ex.ExporterName + "</font>");
                b.Append("</li>");

                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Tipo esportazione: <font>" + ex.ExportType.ToString() + "</font>");
                b.Append("</li>");


                b.Append("<li>");
                b.Append("<img src=\"../../images/info.png\" />Ultima modifica: <font>" + ex.DataModifica.ToString() + "</font>");
                b.Append("</li>");

            b.Append("</ul>");

            return b.ToString();

            //lblDataEsportazione.Text = ex.ExportDate.ToShortDateString();
            //lblPeriodo.Text = ex.Period.ToString();
            //lblProvincia.Text = ex.Regione.ToString();
            //lblRegione.Text = ex.Province.ToString();
            //lblResponsabile.Text = ex.ExporterName;

            //lblTipoEsportazione.Text = ex.ExportType.ToString();
            //lblSettore.Text = ex.Settore;

           

            //lblUltimaModifica.Text = ex.DataModifica.ToShortDateString();

        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdAnnulla_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkHandler.LinkVisualizzaTracciaImportazioni);
        }

        protected void cmdDeleteImportazione_Click(object sender, EventArgs e)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Export ex = p.GetObject("Export", System.Convert.ToInt32(Request.QueryString["Id"])) as Export;

            if (ex != null)
            {
                ImportRemover.RemoveImportData(p, ex);
                Response.Redirect(LinkHandler.LinkVisualizzaTracciaImportazioni);
            }
        }

    }
}
