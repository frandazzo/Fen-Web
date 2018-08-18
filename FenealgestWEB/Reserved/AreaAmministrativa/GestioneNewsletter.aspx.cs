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
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.HANDLERS;
using System.Web.Configuration;
using WIN.TECHNICAL.PERSISTENCE;

namespace FenealgestWEB.Reserved.AreaAmministrativa
{
    public partial class GestioneNewsletter : BaseReservedArea
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
            get { return "Gestione newsletter"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Amministrazione -> Invia newsletter"; }
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
            get{ return "Invia newsletter"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            //if (!this.IsPostBack)
            //{
            //    lblTitolo.Text = FunctionHeaderTitle;
            //}
        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtOggetto.Text.Trim()))
                {
                    lblMessaggio1.Text = "Inserire un oggetto valido per la mail!";
                    return;
                }
                if (string.IsNullOrEmpty(txtTesto1.Html.Trim()))
                {
                    lblMessaggio1.Text = "Inserire un corpo valido per la mail!";
                    return;
                }

                string smtp = WebConfigurationManager.AppSettings["smtpServer"];
                string smtpAccount = WebConfigurationManager.AppSettings["smtpAccount"];
                string smtpPassword = WebConfigurationManager.AppSettings["smtpPassword"];
                string from = WebConfigurationManager.AppSettings["from"];

                IPersistenceFacade p = SessionServiceManager.PersistentService;

                Newsletter n = new Newsletter(new MailProvider(smtp, smtpAccount, smtpPassword, true, from), new ContactProvider(p));

                n.Send(txtOggetto.Text, txtTesto1.Html);
                lblMessaggio1.Text = "Newsletter inviata";

            }
            catch (Exception ex)
            {
                lblMessaggio1.Text = "ATTENZIONE! Si è verificato un errore nell'invio della mail <br /> <br />" + ex.Message;
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtOggetto.Text))
                {
                    lblMessaggio1.Text = "Inserire un oggetto valido per la mail!";
                    return;
                }
                if (string.IsNullOrEmpty(txtTesto1.Html))
                {
                    lblMessaggio1.Text = "Inserire un corpo valido per la mail!";
                    return;
                }



                string smtp = WebConfigurationManager.AppSettings["smtpServer"];
                string smtpAccount = WebConfigurationManager.AppSettings["smtpAccount"];
                string smtpPassword = WebConfigurationManager.AppSettings["smtpPassword"];
                string from = WebConfigurationManager.AppSettings["from"];

                IPersistenceFacade p = SessionServiceManager.PersistentService;

                Newsletter n = new Newsletter(new MailProvider(smtp, smtpAccount, smtpPassword, true, from), SessionServiceManager.SecurityService.CurrentUser.Mail);

                n.Send(txtOggetto.Text, txtTesto1.Html);
                lblMessaggio1.Text = "Newsletter inviata presso " + SessionServiceManager.SecurityService.CurrentUser.Mail;
            }
            catch (Exception ex)
            {
                lblMessaggio1.Text = "ATTENZIONE! Si è verificato un errore nell'invio della mail <br /> <br />" + ex.Message;
            }
            
        }
    }
}
