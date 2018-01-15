using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FenealgestWEB.Utility;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;

namespace FenealgestWEB.Reserved.AreaPubblica
{
    public partial class RinnovaPassNotLogged : BasePage
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
            get { return "Rinnova password"; }
        }

        //protected override void Initialize()
        //{
        //    Page.Title = FunctionHeaderTitle;
        //    if (!this.IsPostBack)
        //    { 
        //        lblTitolo.Text = FunctionHeaderTitle;
        //        LoadMenu();
        //    }
        //}

        //protected override void LoadMenu()
        //{
        //    AddHomeLink();

        //    //MenuItem i = new MenuItem();
        //    //i.Text = "Password dimenticata";
        //    //i.Value = "LinkPasswordDimenticata";
        //    //i.NavigateUrl = LinkHandler.LinkPasswordDimenticata;
        //    //MainMenu.Items.Add(i);
        //    //i = new MenuItem();
        //    //i.Text = "Area riservata";
        //    //i.Value = "ViewSecurePage";
        //    //i.NavigateUrl = LinkHandler.LinkLoginPage;
        //    //MainMenu.Items.Add(i);

        //    DevExpress.Web.ASPxMenu.ASPxMenu m = ((Fenealgestweb)this.Master).DevMainMenu;
        //    DevExpress.Web.ASPxMenu.MenuItem i = new DevExpress.Web.ASPxMenu.MenuItem();
        //    DevExpress.Web.ASPxMenu.MenuItem q = new DevExpress.Web.ASPxMenu.MenuItem();


        //    i.Text = "Password dimenticata";
        //    i.DataItem = "LinkPasswordDimenticata";
        //    i.NavigateUrl = LinkHandler.LinkRinnovaPasswordNotLogged;


        //    q.Text = "Area riservata";
        //    q.DataItem = "ViewSecurePage";
        //    q.NavigateUrl = LinkHandler.LinkLoginPage;



        //    MainMenu.Items.Add(i);
        //    MainMenu.Items.Add(q);
        //}

        protected void btnRinnova_Click(object sender, EventArgs e)
        {
            SecurityController c = SessionServiceManager.SecurityService;
           
            try
            {
                if (!c.RenewPassword(txtUser.Text, txtPass.Text, txtNewPass.Text))
                    lblPassErrata1.Text = "Impossibile rinnovare la password. Contattare l'amministratore!";
                else
                    lblPassErrata1.Text = "Password modificata con successo";

            }
           
            catch (Exception)
            {
                lblPassErrata1.Text = "Impossibile rinnovare la password. Contattare l'amministratore!"; 
            }
            
               
        }

    }
}
