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
using FenealgestWEB.Utility;

namespace FenealgestWEB
{
    public partial class ReservedArea : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public DevExpress.Web.ASPxMenu MainMenu
        {
            get { return this.mainMenu; }
        }

        public DevExpress.Web.ASPxTreeView DevMainTree
        {
            get { return this.mainTree; }
        }


        public Label UtenteLabelPro
        {
            get { return this.UtenteLabel; }
        }

        public Label StrutturaLabelPro
        {
            get { return this.StrutturaLabel; }
        }

        public Label HomeLabelPro
        {
            get { return this.HomeLabel; }
        }

        public Label DataLabelPro
        {
            get { return this.DataLabel; }
        }

        public Label OraLabelPro
        {
            get { return this.OraLabel; }
        }

        public Label ScadenzaLabelPro
        {
            get { return this.ScadenzaLabel; }
        }

        public Label RegioneLabelPro
        {
            get { return this.RegioneLabel; }
        }

        public Label ProvinciaLabelPro
        {
            get { return this.ProvinciaLabel; }
        }

        public Image HeaderImage
        {
            get { return this.ImgHeaderReserved; }
            //get { return this.ImgHeaderReserved; }
        }

       

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SessionServiceManager.SecurityService.ResetLogin();
            Response.Redirect(LinkHandler.LinkHomepage);
        }
    }
}
