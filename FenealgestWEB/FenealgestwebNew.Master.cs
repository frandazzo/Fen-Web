using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace FenealgestWEB
{
    public partial class FenealgestwebNew : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DevExpress.Web.ASPxMenu MainMenu
        {
            get { return this.homemenu1.FindControl("mainMenu") as DevExpress.Web.ASPxMenu; }
        }


        //public Menu MainMenu
        //{
        //    get { return this.mainMenu; }
        //}


        //public DevExpress.Web.ASPxMenu DevMainMenu
        //{
        //    get { return this.mainMenu; }
        //}
        /*
        public Label HeaderLabel
        {
            get { return this.headerLabel; }
        }*/
    }
}
