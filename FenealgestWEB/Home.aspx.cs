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
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using WIN.TECHNICAL.PERSISTENCE;
using System.Web.Configuration;

namespace FenealgestWEB
{
    public partial class Home : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (SessionServiceManager.SecurityService.IsUserLogged())
                Response.Redirect(LinkHandler.LinkReservedAreaHomepage);

            if (!IsPostBack)
                LoadNews();

            Initialize();

        }

        private void LoadNews()
        {
            int contNews;
            if (string.IsNullOrEmpty(Request.QueryString["Cont"]))
                contNews = 0;
            else
                contNews = TryCastToInt();


            int maxQueryResult = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxQueryResult"]);
            int resMax = 4;
            
            //seguo la query
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            PaginationQueryHandler q = p.CreateNewPaginationQuery("MapperNews", maxQueryResult, resMax);

            q.SetTable("News");


            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            q.AddWhereClause(c);

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("Data", false));
            q.AddOrderByClause(orderclause);

            IList news = q.ExecuteFirstQuery(p);


            //imposto l'interfaccia utente
            if (news.Count > 0)
            {
                SetInterfaceAfterQuery(q, news);
                //    //memorizzo la query nella session
                Session["PaginationQuery"] = q;
                return;
            }
            else
            {
                lnkPrec.Enabled = false;
                lnkSucc.Enabled = false;
                lblTitolo1.Text = "Nessuna news caricata";
            }
        }

        private int TryCastToInt()
        {
            try
            {
                return Convert.ToInt32(Request.QueryString["Cont"]);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void SetInterfaceAfterQuery(PaginationQueryHandler q, IList n)
        {
            News nw;

            if (n.Count > 0)
            {
                nw = (News)n[0] as News;
                lblTitolo1.Text = nw.Titolo;
                lblData1.Text = nw.DataCreazione.ToShortDateString();
                lblTesto1.Text = nw.Testo;
            }
            else
            {
                lblTitolo1.Text = "";
                lblData1.Text = "";
                lblTesto1.Text = "";
            }
            if (n.Count > 1)
            {
                nw = (News)n[1] as News;
                lblTitolo2.Text = nw.Titolo;
                lblData2.Text = nw.DataCreazione.ToShortDateString();
                lblTesto2.Text = nw.Testo;
            }
            else
            {
                lblTitolo2.Text = "";
                lblData2.Text = "";
                lblTesto2.Text = "";
            }
            if (n.Count > 2)
            {
                nw = (News)n[2] as News;
                lblTitolo3.Text = nw.Titolo;
                lblData3.Text = nw.DataCreazione.ToShortDateString();
                lblTesto3.Text = nw.Testo;
            }
            else
            {
                lblTitolo3.Text = "";
                lblData3.Text = "";
                lblTesto3.Text = "";
            }
            if (n.Count > 3)
            {
                nw = (News)n[3] as News;
                lblTitolo4.Text = nw.Titolo;
                lblData4.Text = nw.DataCreazione.ToShortDateString();
                lblTesto4.Text = nw.Testo;
            }
            else
            {
                lblTitolo4.Text = "";
                lblData4.Text = "";
                lblTesto4.Text = "";
            }
            lnkPrec.Enabled = q.IsPreviousQueryEnabled;
            lnkSucc.Enabled = q.IsNextQueryEnabled;
        }

        protected void lnkPrec_Click(object sender, EventArgs e)
        {
            PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
            if (q == null)
                throw new Exception("Query di paginazione non memorizzata nella session");

            IList news = q.PreviousPageQuery(SessionServiceManager.PersistentService);

            SetInterfaceAfterQuery(q, news);
        }

        protected void lnkSucc_Click(object sender, EventArgs e)
        {
            PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
            if (q == null)
                throw new Exception("Query di paginazione non memorizzata nella session");

            IList news = q.NextPageQuery(SessionServiceManager.PersistentService);

            SetInterfaceAfterQuery(q, news);
        }

        protected override string FunctionHeaderTitle
        {
            get { return "Home page"; }
        }

        protected override  void LoadMenu()
        {
            //Menu m = ((Fenealgestweb)this.Master).MainMenu;
            //MenuItem i = new MenuItem();
            //i.Text = "Area riservata";
            //i.Value = "ViewSecurePage";
            //i.NavigateUrl = LinkHandler.LinkLoginPage;
            //m.Items.Add(i);
            //i = new MenuItem();
            //i.Text = "Sito Feneal Nazionale";
            //i.Value = "LinkFenealSite";
            //i.NavigateUrl = LinkHandler.LinkFenealNazionale;
            //m.Items.Add(i);


           // SessionServiceManager.MenuCustomizerController.CreateAndConstructSubMenuRepresentation(new MenuConstructor(m));
            DevExpress.Web.ASPxMenu m = ((Fenealgestweb)this.Master).DevMainMenu;
            DevExpress.Web.MenuItem i = new DevExpress.Web.MenuItem();
            DevExpress.Web.MenuItem q = new DevExpress.Web.MenuItem();


            i.Text = "Area riservata";
            i.DataItem = "ViewSecurePage";
            i.NavigateUrl = LinkHandler.LinkLoginPage; ;

            q.Text = "Sito Feneal Nazionale";
            q.DataItem = "LinkFenealSite";
            q.NavigateUrl = LinkHandler.LinkFenealNazionale;
            

          

            MainMenu.Items.Add(i);
            MainMenu.Items.Add(q);


        }

        protected void lkbIscriviti_Click(object sender, EventArgs e)
        {

            //IPersistenceFacade p = SessionServiceManager.PersistentService;
           
            //Query q = p.CreateNewQuery("MapperContatti");

            //q.SetTable("Contatti");

            //CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            //if (!string.IsNullOrEmpty(txtMail.Text))
            //    c.AddCriteria(Criteria.Matches("Mail", txtMail.Text, p.DBType));

            //q.AddWhereClause(c);

            
            //IList cont = q.Execute(p);

            ////imposto l'interfaccia utente
            //if (cont.Count > 0)
            //{
            //    lblRis.Text = "Contatto già presente";
            //}
            //else
            //{
            //    Contatti cn = new Contatti();
            //    cn.Mail = txtMail.Text;
            //    p.InsertObject("Contatti", cn);
            //    lblRis.Text = "Registrazione ok";
            //}
        }

        protected void lkbRimuovi_Click(object sender, EventArgs e)
        {
            //IPersistenceFacade p = SessionServiceManager.PersistentService;

            //Query q = p.CreateNewQuery("MapperContatti");

            //q.SetTable("Contatti");

            //CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            //if (!string.IsNullOrEmpty(txtMail.Text))
            //    c.AddCriteria(Criteria.Matches("Mail", txtMail.Text, p.DBType));

            //q.AddWhereClause(c);


            //IList listCont = q.Execute(p);

            ////imposto l'interfaccia utente
            //if (listCont.Count > 0)
            //{
            //    foreach (Contatti cn in listCont)
            //    {
            //        p.DeleteObject("Contatti", cn);
            //    }
            //    lblRis.Text = "Rimozione effettuata";
            //}
            //else
            //{
            //    lblRis.Text = "Contatto non presente";
            //}
        }

    }
}
