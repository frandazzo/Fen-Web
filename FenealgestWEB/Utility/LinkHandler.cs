using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace FenealgestWEB.Utility
{
    public class LinkHandler
    {


        public static string ConstructSimpleQueryString(string link, string name, string value)
        {
            string query = "?" + name + "=" + value;
            
            return link + query;
        }

        public static string LinkFenealNazionale
        {
            get { return "http://www.fenealuil.it"; }
        }

        public static string LinkRicercaUtenti
        {
            get { return "~/Reserved/AreaAmministrativa/RicercaUtenti.aspx"; }
        }
        public static string LinkReservedAreaHomepage
        {
            get { return "~/ReservedAreaHomePage.aspx"; }
        }
        public static string LinkLoginPage
        {
            get { return "~/Index.aspx"; }
        }
        public static string LinkHomepage
        {
            get { return "~/Index.aspx"; }
        }
        public static string LinkRinnovaPasswordNotLogged
        {
            get { return "~/Reserved/AreaPubblica/RinnovaPassNotLogged.aspx"; }
        }
        public static string LinkPasswordDimenticata
        {
            get { return "~/Reserved/AreaPubblica/PassDimenticata.aspx"; }
        }

         public static string LinkTemp
        {
            get { return "~/PaginaVisualizzaMasterTemp.aspx"; }
        }


         public static string ErrorPage
         {
             get { return "~/ErrorHandling/ErrorPage.aspx"; }
         }

         public static string GestioneUtenti
         {
             get { return "~/Reserved/AreaAmministrativa/GestioneUtenti.aspx"; }
         }
        
         public static string DettagioImportazioni
         {
             get { return "~/Reserved/AreaRegionale/DettaglioTracciaImportazioni.aspx"; }
         }

         public static string LinkVisualizzaTracciaImportazioni
         {
             get { return "~/Reserved/AreaRegionale/VisualizzaTracciaImportazione.aspx"; }
         }

         public static string LinkVisualizzaLavoratore
         {
             get { return "~/Reserved/AreaNazionale/VisualizzaLavoratore.aspx"; }
         }

         public static string LinkGestioneDownload
         {
             get { return "~/Reserved/AreaAmministrativa/GestioneDownload.aspx"; }
         }

         public static string LinkVisualizzaDownload
         {
             get { return "~/Reserved/AreaAmministrativa/VisualizzaDownload.aspx"; }
         }

         public static string LinkGestioneNews
         {
             get { return "~/Reserved/AreaAmministrativa/GestioneNews.aspx"; }
         }

         public static string LinkRicercaNews
         {
             get { return "~/Reserved/AreaAmministrativa/RicercaNews.aspx"; }
         }

         public static string LinkGestioneNewsletter
         {
             get { return "~/Reserved/AreaAmministrativa/GestioneNewsletter.aspx"; }
         }

         public static string LinkRicercaNewsletter
         {
             get { return "~/Reserved/AreaAmministrativa/RicercaNewsletter.aspx"; }
         }
        
    }
}
