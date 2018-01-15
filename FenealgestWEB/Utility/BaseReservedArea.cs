using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.MENU_CUSTOMIZER;
using WIN.TECHNICAL.SECURITY_NEW.Login;
using System.Web.UI;
using System.Text;

namespace FenealgestWEB.Utility
{
    public abstract class BaseReservedArea : Page
    {

        protected abstract string AuthorizedFunction { get; }
        protected abstract string SitePagePath { get; }
        protected abstract string FunctionHeaderTitle { get; }

        //protected abstract Menu PageMenu { get; }
        protected abstract DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu { get; }
        protected abstract DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree { get; }


        protected abstract Image ImageHeader { get; }
        //protected abstract Image BkgrTable { get; }

        protected virtual void LoadPageInfo()
        {

        }
        protected virtual void InitializePage()
        {

        }

        protected string HtmlLoginDataList(string imagePath)
        {
            StringBuilder b = new StringBuilder();

            b.Append("<ul style=\"list-style:none;\">");

            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Utente: </strong> {1}</li>", imagePath, CurrentUser));
            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Struttura: </strong> {1}</li>", imagePath, UserStructure));
            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Regione: </strong> {1}</li>", imagePath, UserRegion));
            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Provincia: </strong> {1}</li>", imagePath, UserProvice));
            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Scadenza password: </strong> {1}</li>", imagePath, UserDecayDataPassword.ToShortDateString()));
            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Ora accesso: </strong> {1}</li>", imagePath, DateTime.Now.ToString()));
            b.Append(string.Format("<li><img src=\"{0}\" /> <strong>Mail: </strong> {1}</li>", imagePath, UserMail));


            b.Append("</ul>");

            return b.ToString();

        }


        protected virtual void LoadCustomizedMenu()
        {

            SecurityController c = SessionServiceManager.SecurityService;

            Utente u = c.CurrentUser as Utente;


            CustomizerController m = SessionServiceManager.MenuCustomizerController;



            IMenuWidgetConstructor w = new DevMenuConstructor(DevPageMenu);

            m.CreateAndConstructSubMenuRepresentation(u.EnabledFunctionNames(), w);

            //IMenuWidgetConstructor w1 = new DevTreeConstructor(DevPageTree);

            //m.CreateAndConstructSubMenuRepresentation(u.EnabledFunctionNames(), w1);

            //SessionServiceManager.MenuCustomizerController.CreateAndConstructSubMenuRepresentation(SessionServiceManager.SecurityService.CurrentUser.EnabledFunctionNames (),new MenuConstructor(new Menu()));

        }



        protected virtual void InitializeBasePage()
        {
            CheckUserIsLogged();

            //Page.Title = FunctionHeaderTitle;
            if (!IsPostBack)
            {
                //LoadPageInfo();
                LoadCustomizedMenu();
            }
        }


        protected virtual string CurrentUser
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                    return u.CompleteName;
                return "";
            }
        }

        protected virtual string UserMail
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                    return u.Mail;
                return "";
            }
        }

        protected virtual string UserStructure
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                    return u.Appartenenza.Struttura.ToString ();
                return "";
            }
        }

        protected virtual bool  IsNationalUser
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                {
                    return u.IsNationalUser;
                }
                return false;
            }
        }

        protected virtual string UserRegion
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                    if (u.Appartenenza.Regione != null)
                        return u.Appartenenza.Regione.ToString();
                return "";
            }
        }


        protected virtual DateTime UserDecayDataPassword
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                    return u.PasswordDecay.Date;
                return DateTime.Now.Date;
            }
        }


        protected virtual string UserProvice
        {
            get
            {
                SecurityController c = SessionServiceManager.SecurityService;
                Utente u = c.CurrentUser as Utente;
                if (u != null)
                    if (u.Appartenenza.Provicnia != null)
                        return u.Appartenenza.Provicnia.ToString();
                return "";
            }
        }




        protected virtual void CheckUserIsLogged()
        {
            try
            {
                SessionServiceManager.SecurityService.CheckUserLogged();
            }
            catch (UserNotLoggedException)
            {
                HttpContext.Current.Response.Redirect(LinkHandler.LinkLoginPage);
            }
        }



        protected virtual void CheckUserIsAuthorized()
        {

            SessionServiceManager.SecurityService.CheckAuthorizzation(AuthorizedFunction);
            
           
        }



        protected virtual void LoadContextImage()
        {
           /* Image img = ImageHeader;//
           // Image tbl = BkgrTable;// 

            if (IsNationalUser )
            {
                img.ImageUrl = "~/immagini/header/header_nazionale.png";
               // tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_italia.png";

            }
            else
            {
                switch (UserRegion)
                {
                    case "ABRUZZO":
                        img.ImageUrl = "~/immagini/header/header_abruzzo.png";
                       // tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_abruzzo.png";
                        break;
                    case "BASILICATA":
                        img.ImageUrl = "~/immagini/header/header_basilicata.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_basilicata.png";
                        break;
                    case "CALABRIA":
                        img.ImageUrl = "~/immagini/header/header_calabria.png";
                       // tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_calabria.png";
                        break;
                    case "CAMPANIA":
                        img.ImageUrl = "~/immagini/header/header_campania.png";
                       // tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_campania.png";
                        break;
                    case "EMILIA ROMAGNA":
                        img.ImageUrl = "~/immagini/header/header_emilia.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_emilia.png";
                        break;
                    case "FRIULI VENEZIA GIULIA":
                        img.ImageUrl = "~/immagini/header/header_friuli.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_friuli.png";
                        break;
                    case "LAZIO":
                        img.ImageUrl = "~/immagini/header/header_lazio.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_lazio.png";
                        break;
                    case "LIGURIA":
                        img.ImageUrl = "~/immagini/header/header_liguria.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_liguria.png";
                        break;
                    case "LOMBARDIA":
                        img.ImageUrl = "~/immagini/header/header_lombardia.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_lombardia.png";
                        break;
                    case "MARCHE":
                        img.ImageUrl = "~/immagini/header/header_marche.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_marche.png";
                        break;
                    case "MOLISE":
                        img.ImageUrl = "~/immagini/header/header_molise.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_molise.png";
                        break;
                    case "PIEMONTE":
                        img.ImageUrl = "~/immagini/header/header_piemonte.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_piemonte.png";
                        break;
                    case "PROV. AUT. BOLZANO":
                        img.ImageUrl = "~/immagini/header/header_bolzano.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_trentino.png";
                        break;
                    case "PROV. AUT. TRENTO":
                        img.ImageUrl = "~/immagini/header/header_trento.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_trentino.png";
                        break;
                    case "PUGLIA":
                        img.ImageUrl = "~/immagini/header/header_puglia.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_puglia.png";
                        break;
                    case "SARDEGNA":
                        img.ImageUrl = "~/immagini/header/header_sardegna.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_sardegna.png";
                        break;
                    case "SICILIA":
                        img.ImageUrl = "~/immagini/header/header_sicilia.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_sicilia.png";
                        break;
                    case "TOSCANA":
                        img.ImageUrl = "~/immagini/header/header_toscana.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_toscana.png";
                        break;
                    case "UMBRIA":
                        img.ImageUrl = "~/immagini/header/header_umbria.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_umbria.png";
                        break;
                    case "VAL D'AOSTA":
                        img.ImageUrl = "~/immagini/header/header_valle.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_valle.png";
                        break;
                    case "VENETO":
                        img.ImageUrl = "~/immagini/header/header_veneto.png";
                        //tbl.ImageUrl = "~/immagini/sfondi_area_riservata/sfondo_area_riservata_veneto.png";
                        break;
                    default:
                        break; 
                }
            }*/
        }

    }
}
