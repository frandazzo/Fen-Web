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
using WIN.TECHNICAL.SECURITY_NEW.Login;

namespace FenealgestWEB
{
    public partial class Login : BasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
            //decommenta per testare la pagina non trovata
           // Response.Redirect("~/homes.aspx");

            //decommenta per testare l'errore generico
           // throw new Exception("Prova errore");
        }

        protected override string FunctionHeaderTitle
        {
            get { return "Area Riservata"; }
        }

        protected override void Initialize()
        {
            Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                lblTitolo.Text = FunctionHeaderTitle;
                LoadMenu();
            }
        }

        protected override void LoadMenu()
        {
            AddHomeLink();

            //MenuItem i = new MenuItem();
            //i.Text = "Rinnova Password";
            //i.Value = "LinkRinnovaPassword";
            //i.NavigateUrl = LinkHandler.LinkRinnovaPasswordNotLogged;
            //MainMenu.Items.Add(i);
            //i = new MenuItem();
            //i.Text = "Password dimenticata";
            //i.Value = "LinkPasswordDimenticata";
            //i.NavigateUrl = LinkHandler.LinkPasswordDimenticata;
            //MainMenu.Items.Add(i);



            DevExpress.Web.ASPxMenu.ASPxMenu m = ((Fenealgestweb)this.Master).DevMainMenu;
            DevExpress.Web.ASPxMenu.MenuItem i = new DevExpress.Web.ASPxMenu.MenuItem();
            DevExpress.Web.ASPxMenu.MenuItem q = new DevExpress.Web.ASPxMenu.MenuItem();


            i.Text = "Rinnova Password";
            i.DataItem = "LinkRinnovaPassword";
            i.NavigateUrl = LinkHandler.LinkRinnovaPasswordNotLogged;

            q.Text = "Password dimenticata";
            q.DataItem = "LinkPasswordDimenticata";
            q.NavigateUrl = LinkHandler.LinkPasswordDimenticata;




            MainMenu.Items.Add(i);
            MainMenu.Items.Add(q);

        }


        protected void ImageMap1_Click(object sender, ImageMapEventArgs e)
        {
            EseguiAcesso();
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            EseguiAcesso();
        }

        private void EseguiAcesso()
        {
            SecurityController c = SessionServiceManager.SecurityService;
            LoginResult result = c.Login(txtUserName.Text, txtPassword.Text);

            if (result.CanAccess)
                HttpContext.Current.Response.Redirect(LinkHandler.LinkReservedAreaHomepage);
            else
            {
                lblText.Text = result.LoginMessage;
                lblTryMessage.Text = result.TryNumberMessage();
            }
        }



    }
}
