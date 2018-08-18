using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FenealgestWEB.Utility;
using WIN.FENGEST_NAZIONALE.DOMAIN;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using System.Web.Configuration;

namespace FenealgestWEB.Reserved.AreaAmministrativa
{
    public partial class RicercaUtenti : BaseReservedArea
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
            get { return "Ricerca utenti"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Amministrazione -> Ricerca utenti"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}


        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }
        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
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
            get { return "Ricerca utenti"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                LoadCombo();
                InitializeGrid();
                SetResultVisibility(false, "");
            }
        }

        private void InitializeGrid()
        {
                GridView1.Columns.Clear();
 
            //Aggiungiamo le colonne che ci interessano
                BoundField Cognome = new BoundField();
                Cognome.DataField = ("Username");
                Cognome.HeaderText = "Username";
                

                BoundField Nome = new BoundField();
                Nome.DataField = ("Appartenenza");
                Nome.HeaderText = "Appartenenza";

                BoundField Mail = new BoundField();
                Mail.DataField = ("Cognome");
                Mail.HeaderText = "Cognome";

                BoundField Struttura = new BoundField();
                Struttura.DataField = ("TipoStruttura");
                Struttura.HeaderText = "Struttura";

                //BoundField Id = new BoundField();
                //Id.DataField = ("Id");
                //Id.HeaderText = "Id";
                //Id.Visible = false;

                //ButtonField bfcol = new ButtonField();
                //bfcol.HeaderText = "Aggiorna/Modifica";
                //bfcol.ButtonType = ButtonType.Link;
                //bfcol.CommandName = "select";
                //bfcol.Text = "Aggiorna";

                HyperLinkField hlfield = new HyperLinkField();
                hlfield.DataNavigateUrlFields = new string[] { "Id" };
                hlfield.DataNavigateUrlFormatString = LinkHandler.GestioneUtenti + "?ID={0}";
                hlfield.HeaderText = "<img src='../../immagini/icon/edit32.png' alt='MOD' border='0' />";
                hlfield.Text = "<img src='../../immagini/icon/edit16.png' alt='mod'border='0' />";
                hlfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                hlfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                GridView1.Columns.Add(Cognome);
                GridView1.Columns.Add(Nome);
                GridView1.Columns.Add(Mail);
                GridView1.Columns.Add(Struttura);
                //GridView1.Columns.Add(bfcol);
                GridView1.Columns.Add(hlfield);
                GridView1.DataKeyNames = new string[] { "Id" };
           

                GridView1.AutoGenerateColumns = false;
        }

        private void LoadCombo()
        {
            string[] arr = Enum.GetNames (typeof(StrutturaApparteneza));

            cboTipo.Items.Clear();
            cboTipo.Items.Add("");
            foreach (string item in arr)
	        {
        	    cboTipo.Items.Add (item);	 
	        }
            //si mette sul primo elemento
            cboTipo.SelectedIndex = 0;
        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            int maxQueryResult = Convert.ToInt32 (WebConfigurationManager.AppSettings["MaxQueryResult"]);
            int resMax = TryCastToInt();

            //seguo la query
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            PaginationQueryHandler q = p.CreateNewPaginationQuery("MapperUtente", maxQueryResult, resMax);
           


            q.SetTable("Utenti");


            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            if (!string.IsNullOrEmpty(txtCognome.Text))
                c.AddCriteria(Criteria.Matches("Username","%" + txtCognome.Text,p.DBType));

            if (!string.IsNullOrEmpty(cboTipo.Text))
                c.AddCriteria(Criteria.MatchesEqual("Structure",cboTipo.Text,p.DBType));

            q.AddWhereClause(c);

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("Cognome", true));
            q.AddOrderByClause(orderclause);

            IList utenti = q.ExecuteFirstQuery(p);


            //imposto l'interfaccia utente
            if (utenti.Count > 0)
            {
                SetInterfaceAfterQuery(q, utenti);
                //memorizzo la query nella session
                Session["PaginationQuery"] = q;
                return;
            }

            SetResultVisibility(false, "Nessun risultato trovato!");
            //memorizzo la query nella session
            Session["PaginationQuery"] = q;
            

        }

        private void SetInterfaceAfterQuery(PaginationQueryHandler q, IList utenti)
        {
            GridView1.DataSource = utenti;
            GridView1.DataBind();
            SetResultVisibility(true, "");

            lblVis.Text = q.ViewedRecordMessage;
            lblTotalResults.Text = q.FoundElementMessage;
            lnkPrec.Enabled = q.IsPreviousQueryEnabled;
            lnkSucc.Enabled = q.IsNextQueryEnabled;
        }

        private int TryCastToInt()
        {
            string resperPage = txtResultPerPage.Text;

            try
            {
                int res = Convert.ToInt32(resperPage);

                if (res > 50)
                    return 50;
                if (res < 10)
                    return 10;
                return res;
            }
            catch (Exception)
            {
                return 10;
            }
            
        }

        private void SetResultVisibility(bool visible, string message)
        {
            if (visible)
            {
                GridView1.Visible = true;
                lblVis.Visible = true;
                lnkPrec.Visible = true;
                lnkSucc.Visible = true;
               // pnlGrid.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
                lblVis.Visible = false;
                lnkPrec.Visible = false;
                lnkSucc.Visible = false;
                lblTotalResults.Text = message;
                //pnlGrid.Visible = false;
            }

        }

        protected void lnkPrec_Click(object sender, EventArgs e)
        {
            PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
            if (q == null)
                throw new Exception("Query di paginazione non memorizzata nella session");

            IList utenti = q.PreviousPageQuery(SessionServiceManager.PersistentService);

            SetInterfaceAfterQuery(q, utenti);
        }

        protected void lnkSucc_Click(object sender, EventArgs e)
        {
            PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
            if (q == null)
                throw new Exception("Query di paginazione non memorizzata nella session");

            IList utenti = q.NextPageQuery(SessionServiceManager.PersistentService);

            SetInterfaceAfterQuery(q, utenti);
        }

        protected void cmdCreaUtente_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect(LinkHandler.GestioneUtenti);
        }
    }
}
