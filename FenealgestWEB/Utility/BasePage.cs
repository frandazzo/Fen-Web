using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace FenealgestWEB.Utility
{
    public abstract class BasePage : Page
    {

        protected abstract string FunctionHeaderTitle { get; }

        /*
        protected virtual void SetHeaderLabelValue()
        {
            HeaderLabel.Text = "";
        }
        */
        protected virtual void LoadMenu()
        {
            AddHomeLink();
            //i = new MenuItem();
            //i.Text = "Rinnova Password";
            //i.Value = "LinkRinnovaPassword";
            //i.NavigateUrl = LinkHandler.LinkRinnovaPassword;
            //m.Items.Add(i);
            //i = new MenuItem();
            //i.Text = "Password dimenticata";
            //i.Value = "LinkPasswordDimenticata";
            //i.NavigateUrl = LinkHandler.LinkPasswordDimenticata;
            //m.Items.Add(i);
        }


        protected virtual void Initialize()
        {
            Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                LoadMenu();
                //la label è stata eliminata
                //SetHeaderLabelValue();
            }
        }


        protected void AddHomeLink()
        {
            //MenuItem i = new MenuItem();
            //i.Text = "HOME PAGE";
            //i.Value = "ViewSHomepage";
            //i.NavigateUrl = LinkHandler.LinkHomepage;
            //MainMenu.Items.Add(

            DevExpress.Web.ASPxMenu.MenuItem  i = new DevExpress.Web.ASPxMenu.MenuItem();
            i.Text = "HOME PAGE";
            i.DataItem = "ViewSHomepage";
            i.NavigateUrl = LinkHandler.LinkHomepage;
            MainMenu.Items.Add (i);
        }

        public virtual DevExpress.Web.ASPxMenu.ASPxMenu MainMenu
        {
            get
            {
                return ((Fenealgestweb)this.Master).DevMainMenu;
            }
        }


        public virtual DevExpress.Web.ASPxTreeView.ASPxTreeView   MainTree
        {
            get
            {
                return ((Fenealgestweb)this.Master).DevMainTree;
            }
        }


        /*
        public virtual Label HeaderLabel
        {
            get
            {
                return ((Fenealgestweb)this.Master).HeaderLabel ;
            }
        }*/
    }
}
