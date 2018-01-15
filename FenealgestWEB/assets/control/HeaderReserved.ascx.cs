using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FenealgestWEB.Utility;

namespace FenealgestWEB.assets.control
{
    public partial class HeaderReserved : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SessionServiceManager.SecurityService.ResetLogin();
            Response.Redirect(LinkHandler.LinkHomepage);
        }
    }
}