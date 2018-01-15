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
using WIN.TECHNICAL.PERSISTENCE;
using FenealgestWEB.Utility;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.SECURITY_NEW.Login;


namespace FenealgestWEB
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //throw new Exception("Errore di prova");
            if (SessionServiceManager.SecurityService.IsUserLogged())
                Response.Redirect(LinkHandler.LinkReservedAreaHomepage);
        }

        private void EseguiAcesso()
        {
            SecurityController c = SessionServiceManager.SecurityService;
            LoginResult result = c.Login(txtUsername1.Text, txtPassword1.Text);

            if (result.CanAccess)
                HttpContext.Current.Response.Redirect(LinkHandler.LinkReservedAreaHomepage);
            else
            {
                string loginMessage = result.LoginMessage;
                string tryNumber = result.TryNumberMessage();
                errorMessage.Text = loginMessage + "<br />" + tryNumber;
            }
        }
  

        protected void GetNews()
        {

            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Query q = p.CreateNewQuery("MapperNews");

            IList news = q.Execute(p);



            //adesso costruisco la lista (ul) delle news da renderizzare
            string render = CreateRenderedNews(news);
            Response.Write(render);
        }

        private string CreateRenderedNews(IList l)
        {
            if (l.Count == 0)
                return "<ul id=\"news_1\"><li>" + "Nessuna news disponibile</li></ul>";

            StringBuilder b = new StringBuilder();
            //apro la lista
            b.Append("<ul id=\"news_1\">");

            foreach (News item in l)
            {
                
                b.Append("<li>");
               

                //inserisco l'header del titolo della news
                b.Append("<h4>");

                //inserisco il titolo
                string tit = item.Titolo;
                b.Append(HttpUtility.HtmlEncode(tit));

                //termino l'header
                b.Append("</h4>");

                //inserisco la data
                b.Append("<font class=\"data\">" + HttpUtility.HtmlEncode(item.DataCreazione.ToLongDateString()) + "</font>");



                //inserisco il corpo della news
                b.Append(HttpUtility.HtmlEncode(item.Testo));

               
                //chiudo l'elemento della lista
                b.Append("</li>");
            }

            //chiudo la lista
            b.Append("</ul>");

            return b.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            EseguiAcesso();
        }



    }
}
