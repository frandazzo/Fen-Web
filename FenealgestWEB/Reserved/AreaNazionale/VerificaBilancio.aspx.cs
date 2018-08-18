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
using WIN.FENGEST_NAZIONALE.HANDLERS.Financial;
using System.Collections.Generic;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

namespace FenealgestWEB.Reserved.AreaNazionale
{
    public partial class VerificaBilancio : BaseReservedArea
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //esegue la verifica che un utetne è loggato;
            //inizializza le info base della pagina insieme con 
            //l'inizializzaizone del menu per l'utente
            InitializeBasePage();

            ////se è una pagina che richiede una autorizzazione
            ////posso decommentare il codice che segue
            if (!IsPostBack)
            {
                CheckUserIsAuthorized();
                
            }

            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }


        protected override string AuthorizedFunction
        {
            get
            {
                return "Verifica bilancio";
            }
        }

        protected override string SitePagePath
        {
            get
            {
                return "Home -> Area riservata -> Nazionale -> Verifica invio bilanci";
            }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}


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

            //tempSubstring = base.CurrentUser;
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
            //lbl5.Text = base.UserDecayDataPassword.ToShortDateString();

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
            get { return "Verifica invio bilanci"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                
                loadCombos();
                //lblTitolo.Text = FunctionHeaderTitle;
                InitializeGridView();
            }
        }

        private void InitializeGridView()
        {
            GridView1.Columns.Clear();

            BoundField Label = new BoundField();
            Label.DataField = ("Description");
            Label.HeaderText = "Descrizione";

            BoundField Data = new BoundField();
            Data.DataField = ("Sended");
            Data.HeaderText = "Invio";

            GridView1.Columns.Add(Label);
            GridView1.Columns.Add(Data);

            GridView1.AutoGenerateColumns = false;
        }



        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}


        private void loadCombos()
        {
            int anno;
            for (int i = 0; i <= 50; i++)
            {
                anno = i + 2000;
                cboAnno.Items.Add(new ListItem(anno.ToString(), anno.ToString()));
            }
            cboAnno.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void cmdRicerca_Click(object sender, EventArgs e)
        {
            CaricaLista();
            GridView1.Visible = true;
        }

        private void CaricaLista()
        {

            bool b, c;
            if (RadioButtonList1.SelectedValue.Equals("Regionale")) 
                	b = true;
            else
                    b = false;

            if (cboTipo.SelectedValue == "FENEAL")
                c = false;
            else
                c = true;;


            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            BilancioHandler bh = new BilancioHandler(p,f);

            IList<SendCheck> listFil = bh.GetSendedRendiconto(int.Parse (cboAnno.SelectedValue), b, c);
            GridView1.DataSource = listFil;
            GridView1.DataBind();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList l = sender as RadioButtonList;


            if (l.Items[0].Selected)
                cboTipo.Enabled = true;
            else
            {
                cboTipo.SelectedIndex = 0;
                cboTipo.Enabled = false;
            }


        }

        //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        //{

        //}

     

    }
}
