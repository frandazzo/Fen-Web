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
using WIN.TECHNICAL.SECURITY_NEW.Login;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.MENU_CUSTOMIZER;

namespace FenealgestWEB
{
    public partial class ReservedAreaHomePage : BaseReservedArea 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //esegue la verifica che un utetne è loggato;
            //inizializza il menu per l'utente
            InitializeBasePage();

            ////se è una pagina che richiede una autorizzazione
            ////posso decommentare il codice che segue
            //if (!IsPostBack)
            //    CheckUserIsAuthorized();


            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }

     

        protected override string AuthorizedFunction
        {
            get { return ""; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata"; }
        }

        protected override DevExpress.Web.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }
        protected override DevExpress.Web.ASPxTreeView DevPageTree
        {
            get { return null; }
        }

        protected override void LoadPageInfo()
        {
            //string tempSubstring;
            ////imposto tutte le info necessarie alla creazione di tutte le informazioni della pagina
            //Label lbl = ((ReservedArea)this.Master).UtenteLabelPro;
            
            //tempSubstring =base.CurrentUser;
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
            //lbl5.Text =  base.UserDecayDataPassword.ToShortDateString ();

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
            get { return ""; }
        }



        protected override Image ImageHeader
        {
            get { return null;  }
        }


    }
}
