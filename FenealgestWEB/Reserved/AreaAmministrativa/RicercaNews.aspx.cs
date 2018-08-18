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
using WIN.TECHNICAL.PERSISTENCE;
using System.Web.Configuration;

namespace FenealgestWEB.Reserved.AreaAmministrativa
{
    public partial class RicercaNews : BaseReservedArea
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
            get { return "Gestione news"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Amministrazione -> Ricerca news"; }
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
            get { return "Ricerca news"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                InitializeGrid();
                SetResultVisibility(false, "");
            }
        }

        private void InitializeGrid()
        {
            GridView1.Columns.Clear();

            //Aggiungiamo le colonne che ci interessano
            BoundField Data = new BoundField();
            Data.DataField = ("DataCreazione");
            Data.DataFormatString = "{0:d}";
            Data.HeaderText = "Data";

            BoundField Titolo = new BoundField();
            Titolo.DataField = ("Titolo");
            Titolo.HeaderText = "Titolo";

            BoundField InseritoDa = new BoundField();
            InseritoDa.DataField = ("CreatoDa");
            InseritoDa.HeaderText = "Creato da";

            HyperLinkField hlfield = new HyperLinkField();
            hlfield.DataNavigateUrlFields = new string[] { "Id" };
            hlfield.DataNavigateUrlFormatString = LinkHandler.LinkGestioneNews + "?ID={0}";
            hlfield.HeaderText = "<img src='../../immagini/icon/edit32.png' alt='MOD' border='0' />";
            hlfield.Text = "<img src='../../immagini/icon/edit16.png' alt='mod'border='0' />";
            hlfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            hlfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            GridView1.Columns.Add(Data);
            GridView1.Columns.Add(Titolo);
            GridView1.Columns.Add(InseritoDa);
            //GridView1.Columns.Add(bfcol);
            GridView1.Columns.Add(hlfield);
            GridView1.DataKeyNames = new string[] { "Id" };


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

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            //int maxQueryResult = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxQueryResult"]);
            //int resMax = TryCastToInt();

            //seguo la query
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Query q = p.CreateNewQuery("MapperNews");

            //q.SetTable("News");


            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            if (!string.IsNullOrEmpty(txtTitolo.Text))
            {
                c.AddCriteria(Criteria.Matches("Titolo", txtTitolo.Text, p.DBType));
                q.AddWhereClause(c);
            }

            //if ((chbDate.Checked) && (bdpDataStart.IsDate) && (bdpDataEnd.IsDate))
            //    if ((bdpDataStart.SelectedDate != DateTime.MinValue) && (bdpDataEnd.SelectedDate != DateTime.MaxValue))
            //        c.AddCriteria(Criteria.DateContained("Data", bdpDataStart.SelectedDate, bdpDataEnd.SelectedDate, p.DBType));
           
            

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("Data", true));
            q.AddOrderByClause(orderclause);

            IList news = q.Execute(p);

            SetInterfaceAfterQuery(news);
            //imposto l'interfaccia utente
            //if (news.Count > 0)
            //{
            //    SetInterfaceAfterQuery(q, news);
            //    //memorizzo la query nella session
            //    //Session["PaginationQuery"] = q;
            //    return;
            //}

            //SetResultVisibility(false, "Nessun risultato trovato!");
            ////memorizzo la query nella session
            //Session["PaginationQuery"] = q;


        }

        //private void SetInterfaceAfterQuery(PaginationQueryHandler q, IList news)
        //{
        //    GridView1.DataSource = news;
        //    GridView1.DataBind();
        //    SetResultVisibility(true, "");

        //    lblVis.Text = q.ViewedRecordMessage;
        //    lblTotalResults.Text = q.FoundElementMessage;
        //    lnkPrec.Enabled = q.IsPreviousQueryEnabled;
        //    lnkSucc.Enabled = q.IsNextQueryEnabled;
        //}


        private void SetInterfaceAfterQuery(IList news)
        {
            string message = "";
            if (news.Count > 0)
                message = string.Format("Sono state trovate {0} news!", news.Count.ToString());

            GridView1.DataSource = news;
            GridView1.DataBind();

            bool viewGrid = false;
            if (news.Count > 0)
                viewGrid = true;

            SetResultVisibility(viewGrid , message);

            //lblVis.Text = q.ViewedRecordMessage;
            //lblTotalResults.Text = q.FoundElementMessage;
            //lnkPrec.Enabled = q.IsPreviousQueryEnabled;
            //lnkSucc.Enabled = q.IsNextQueryEnabled;
        }

        //private int TryCastToInt()
        //{
        //    string resperPage = txtResultPerPage.Text;

        //    try
        //    {
        //        int res = Convert.ToInt32(resperPage);

        //        if (res > 50)
        //            return 50;
        //        if (res < 10)
        //            return 10;
        //        return res;
        //    }
        //    catch (Exception)
        //    {
        //        return 10;
        //    }

        //}

        private void SetResultVisibility(bool visible, string message)
        {
                GridView1.Visible = visible;
                lblTotalResults1.Text = message;
        }

        //private void SetResultVisibility(bool visible, string message)
        //{
        //    if (visible)
        //    {
        //        GridView1.Visible = true;
        //        lblVis.Visible = true;
        //        lnkPrec.Visible = true;
        //        lnkSucc.Visible = true;
        //        pnlGrid.Visible = true;
        //    }
        //    else
        //    {
        //        GridView1.Visible = false;
        //        lblVis.Visible = false;
        //        lnkPrec.Visible = false;
        //        lnkSucc.Visible = false;
        //        lblTotalResults.Text = message;
        //        pnlGrid.Visible = false;
        //    }

        //}

        //protected void lnkPrec_Click(object sender, EventArgs e)
        //{
        //    PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
        //    if (q == null)
        //        throw new Exception("Query di paginazione non memorizzata nella session");

        //    IList news = q.PreviousPageQuery(SessionServiceManager.PersistentService);

        //    SetInterfaceAfterQuery(q, news);
        //}

        //protected void lnkSucc_Click(object sender, EventArgs e)
        //{
        //    PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
        //    if (q == null)
        //        throw new Exception("Query di paginazione non memorizzata nella session");

        //    IList news = q.NextPageQuery(SessionServiceManager.PersistentService);

        //    SetInterfaceAfterQuery(q, news);
        //}

        protected void cmdCreaNews_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect(LinkHandler.LinkGestioneNews);
        }

        //protected void chbDate_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chbDate.Checked)
        //    {
        //        bdpDataStart.SelectedValue = DateTime.Now.Date;
        //        bdpDataEnd.SelectedValue = DateTime.Now.Date;
        //        bdpDataStart.Enabled = true;
        //        bdpDataEnd.Enabled = true;
        //    }
        //    else
        //    {
        //        bdpDataStart.SelectedValue = "";
        //        bdpDataEnd.SelectedValue = "";
        //        bdpDataStart.Enabled = false;
        //        bdpDataEnd.Enabled = false;
        //    }
        //}
    }
}
