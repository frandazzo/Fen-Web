using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FenealgestWEB.assets.control
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string GetVisitorNumber()
        {
            try
            {
                int l = Convert.ToInt32(HttpContext.Current.Application.Get("SessionCount"));
                return l.ToString();
            }
            catch (Exception)
            {
                return "0";
            }

        }
    }
}