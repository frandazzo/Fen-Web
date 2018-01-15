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
using FenealgestWEB.Utility;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using WIN.TECHNICAL.PERSISTENCE;

namespace FenealgestWEB.Reserved.AreaAmministrativa
{
    public partial class GestioneNews : BaseReservedArea
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



        protected override string AuthorizedFunction
        {
            get { return "Gestione news"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Amministrazione -> Gestione news"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}

        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }
        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
        {
            get { return null; }
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
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                { return "Aggiorna news"; }
                else
                { return "Crea news"; }
            }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                //Verifico se sono in modalità di update
                //e nel caso carico i dati del download da modificare
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    LoadNews(Request.QueryString["Id"]);
                    cmdSave1.Text = "Aggiorna news";
                }
                //altrimenti inizializzo i valori
                else
                {
                    cmdSave1.Text = "Inserisci news";
                }
            }
        }

        private void LoadNews(string id)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            News n = p.GetObject("News", System.Convert.ToInt32(id)) as News;

            //imposto l'interfaccia utente
            if (n != null)
            {
                LoadObjectProperties(n);
                return;
            }
            throw new Exception("Id della news non valido!");


        }

        private void LoadObjectProperties(News n)
        {
            //Valorizzo i campi del form legato al download
            txtCreatoDa.Text = n.CreatoDa;
            txtTitolo.Text = n.Titolo;
            txtTesto.Text = n.Testo;
        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdCreaNews_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkHandler.LinkGestioneNews);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;
            if (cmdSave1.Text == "Aggiorna news")
            {
                News n = p.GetObject("News", System.Convert.ToInt32(Request.QueryString["Id"])) as News;

                if (n != null)
                {
                    SetBaseData(n);
                    p.UpdateObject("News", n);
                }
            }
            else
            {

                News n = new News();
                SetBaseData(n);
                p.InsertObject("News", n);
                Response.Redirect(LinkHandler.ConstructSimpleQueryString(LinkHandler.LinkGestioneNews, "ID", n.Id.ToString()));
            }
        }

        private void SetBaseData(News n)
        {
            n.DataCreazione = DateTime.Now;
            n.CreatoDa = txtCreatoDa.Text;
            n.Titolo = txtTitolo.Text;
            n.Testo = txtTesto.Text;
        }

        protected void cmdAnnulla_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkHandler.LinkRicercaNews);
        }

        protected void cmdDeleteNews_Click(object sender, EventArgs e)
        {

            //codice per cancellare un download
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            News n = p.GetObject("News", System.Convert.ToInt32(Request.QueryString["Id"])) as News;

            if (n != null)
            {
                p.DeleteObject("News", n);
                Response.Redirect(LinkHandler.LinkRicercaNews);
            }
        }
    }
}
