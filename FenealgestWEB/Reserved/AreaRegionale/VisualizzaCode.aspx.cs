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
using WIN.TECHNICAL.MIDDLEWARE.QueueHandlers;
using System.Messaging;
using System.Web.Configuration;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.BASEREUSE;
using System.Collections.Generic;

namespace FenealgestWEB.Reserved.AreaRegionale
{
    public partial class VisualizzaCode : BaseReservedArea
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
                CheckUserIsAuthorized();
                
            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }



        protected override string AuthorizedFunction
        {
            get { return "Visualizza code in attesa"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Area regionale -> Visualizza code in attesa"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}

        protected override DevExpress.Web.ASPxTreeView DevPageTree
        {
            get { return null; }
        }

        protected override DevExpress.Web.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
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
            get { return "Visualizza code in attesa"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;

                LoadCombo();

                InitializeGridView();

                Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;

                //se l'utente è nazionale setto la list box con tutte le regioni e visualizzo il pulsante ricerca
                //altrimenti setto la listbox con la sua regione e restituisco la lista senza visualizzare il pulsante ricerca
                
                if (u.IsNationalUser)
                {
                    cboRegione.Enabled = true;
                    cmdRicerca1.Visible = true;
                    GridView1.Visible = false;
                    cmdAggiorna1.Visible = false;
                }
                else
                {
                    cboRegione.SelectedValue = u.Appartenenza.Regione.Descrizione;
                    cboRegione.Enabled = false;
                    cmdRicerca1.Visible = false;
                    CaricaElementiInCoda(u.Appartenenza.Regione.Descrizione);
                    GridView1.Visible = true;
                    cmdAggiorna1.Visible = true;
                    
                }
                
            }
        }

        private void LoadCombo()
        {

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            //pulisco e aggingo tutte le voci compresa una prima voce vuota
            cboRegione.Items.Clear();
            cboRegione.Items.Add("");
            foreach (string item in f.GetListaRegioni())
            {
                cboRegione.Items.Add(item);
            }
            //si mette sul primo elemento
            cboRegione.SelectedIndex = 0;
        }

        private void CaricaElementiInCoda(string regione)
        {

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            IList province = f.GetGeoHandler().GetNomiProviciePerRegione(regione);

            QueueHandler q = new QueueHandler();
            
            MessageQueue c = q.CreateAndGetQueue(WebConfigurationManager.AppSettings["QueueName"].ToString());
            c.MessageReadPropertyFilter.SetAll();
            Message[] list = c.GetAllMessages();

            IList<Message> listFil = new List<Message>();

            if(regione.Equals(""))
                GridView1.DataSource = list;
            else
            {
                foreach (Message m in list)
                {
                    string[] strSplit = m.Label.Split('_');
                    foreach (string prov in province)
                        if (strSplit[0].Equals(prov))
                        {
                            listFil.Add(m);
                        }
 
                }
                GridView1.DataSource = listFil;
            }

            GridView1.DataBind();
        }

        private void InitializeGridView()
        {
            GridView1.Columns.Clear();

            BoundField Label = new BoundField();
            Label.DataField = ("Label");
            Label.HeaderText = "Etichetta";

            BoundField Data = new BoundField();
            Data.DataField = ("ArrivedTime");
            Data.HeaderText = "Data di arrivo";

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

        protected void cmdRicerca_Click(object sender, EventArgs e)
        {
            CaricaElementiInCoda(cboRegione.Text);
            GridView1.Visible = true;
            cmdAggiorna1.Visible = true;

        }

        protected void cmdAggiorna_Click(object sender, EventArgs e)
        {
             Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;
             //se l'utente è nazionale setto la list box con tutte le regioni e visualizzo il pulsante ricerca
             //altrimenti setto la listbox con la sua regione e restituisco la lista senza visualizzare il pulsante ricerca

             if (u.IsNationalUser)
                 CaricaElementiInCoda(cboRegione.Text);
             else
                 CaricaElementiInCoda(u.Appartenenza.Regione.Descrizione);
        }
    }
}
