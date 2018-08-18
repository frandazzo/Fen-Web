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
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using System.Web.Configuration;

namespace FenealgestWEB.Reserved.AreaPubblica
{
    public partial class PassDimenticata : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize();


            //decommenta per testare la pagina non trovata
            // Response.Redirect("~/homes.aspx");

            //decommenta per testare l'errore generico
            // throw new Exception("Prova errore");
        }

        protected override string FunctionHeaderTitle
        {
            get { return "Password dimenticata"; }
        }

        //protected override void Initialize()
        //{
        //    //Page.Title = FunctionHeaderTitle;
        //    //if (!this.IsPostBack)
        //    //{
        //    //    lblTitolo.Text = FunctionHeaderTitle;
        //    //    LoadMenu();
        //    //}

        //    //non fa nulla
        //}

        //protected override void LoadMenu()
        //{
        //    AddHomeLink();


        //    DevExpress.Web.ASPxMenu.ASPxMenu m = ((Fenealgestweb)this.Master).DevMainMenu;
        //    DevExpress.Web.ASPxMenu.MenuItem i = new DevExpress.Web.ASPxMenu.MenuItem();
        //    DevExpress.Web.ASPxMenu.MenuItem q = new DevExpress.Web.ASPxMenu.MenuItem();


        //    i.Text = "Rinnova Password";
        //    i.DataItem = "LinkRinnovaPassword";
        //    i.NavigateUrl = LinkHandler.LinkRinnovaPasswordNotLogged;


        //    q.Text = "Area riservata";
        //    q.DataItem = "ViewSecurePage";
        //    q.NavigateUrl = LinkHandler.LinkLoginPage;



        //    MainMenu.Items.Add(i);
        //    MainMenu.Items.Add(q);



        //}

        protected void btnInvio_Click(object sender, EventArgs e)
        {
            SecurityController c = SessionServiceManager.SecurityService;

            string smtp = WebConfigurationManager.AppSettings["smtpServer"];
            string smtpAccount = WebConfigurationManager.AppSettings["smtpAccount"];
            string smtpPassword = WebConfigurationManager.AppSettings["smtpPassword"];
            string from = WebConfigurationManager.AppSettings["from"];
            MailProvider mail = new MailProvider(smtp, smtpAccount, smtpPassword, true, from);
            
            c.SendNewPasswordbyEmail(txtUser.Text, mail);

            lblMessaggio1.Text = "Password inviata all'indirizzo dell'utente";
        }

    }
}
