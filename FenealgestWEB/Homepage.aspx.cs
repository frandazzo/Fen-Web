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
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;

namespace FenealgestWEB
{
    public partial class WebForm1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        protected override  void LoadMenu()
        {
            Menu m = ((Fenealgestweb)this.Master).MainMenu;
            //MenuItem i = new MenuItem();
            //i.Text = "Area riservata";
            //i.Value = "ViewSecurePage";
            //i.NavigateUrl = LinkHandler.LinkLoginPage;
            //m.Items.Add(i);
            //i = new MenuItem();
            //i.Text = "Sito Feneal Nazionale";
            //i.Value = "LinkFenealSite";
            //i.NavigateUrl = LinkHandler.LinkFenealNazionale;
            //m.Items.Add(i);

            SessionServiceManager.MenuCustomizerController.CreateAndConstructSubMenuRepresentation(new MenuConstructor(m));


        }

        //private void SetHeaderLabelValue()
        //{
        //    Label l = ((Fenealgestweb)this.Master).HeaderLabel;
        //    l.Text = "";
        //}
    }
}
