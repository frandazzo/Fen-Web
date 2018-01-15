using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using WIN.TECHNICAL.PERSISTENCE;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.MENU_CUSTOMIZER;
using WIN.FENGEST_NAZIONALE.HANDLERS.GeoHandler;
using WIN.BASEREUSE;

namespace FenealgestWEB.Utility
{
    public class SessionServiceManager
    {
        public static void InitializePersistenceService()
        {
            IPersistenceFacade f = DataAccessServices.SimplePersistenceFacadeInstance();

            HttpContext.Current.Session["PersistentServiceFacade"] = f;

            
        }

        public static void InitializeMenuCustomizerService()
        {
            CustomizerController c = CustomizerController.NewInstance;

            c.Initialize(new MenuProvider());


            HttpContext.Current.Session["MenuCustomizer"] = c;


        }

        public static void InitializeGeoHandlerService()
        {
            WIN.BASEREUSE.GeoLocationFacade f;

      
      
            GeoLocationFacade.InitializeInstance(new GeoHandler(PersistentService));
            f = GeoLocationFacade.Instance();
       


            HttpContext.Current.Session["GeoHandler"] = f;

        }

        public static WIN.BASEREUSE.GeoLocationFacade GeoHandlerService
        {
            get
            {
                if (HttpContext.Current.Session["GeoHandler"] == null)
                    InitializeGeoHandlerService();
                return (GeoLocationFacade)HttpContext.Current.Session["GeoHandler"];
            }
        }

        public static void InitializeSecurityService()
        {
            if (HttpContext.Current.Session["PersistentServiceFacade"] == null)
                InitializePersistenceService();

            IPersistenceFacade f = (IPersistenceFacade)HttpContext.Current.Session["PersistentServiceFacade"];

            SecurityController c = SecurityController.NewInstance;
            c.InializeSecurityController(new UserProvider(f), new WIN.FENGEST_NAZIONALE.DOMAIN.Security.RoleProvider(), new AccessChecker(new UserLocker(f)));


            HttpContext.Current.Session["SecurityService"] = c;
        }


        public static IPersistenceFacade PersistentService
        {
            get 
            {
                if (HttpContext.Current.Session["PersistentServiceFacade"] == null)
                    InitializePersistenceService();
                return (IPersistenceFacade)HttpContext.Current.Session["PersistentServiceFacade"];
            }
        }


        public static SecurityController  SecurityService
        {
            get
            {
                if (HttpContext.Current.Session["SecurityService"] == null)
                    InitializeSecurityService();
                return (SecurityController)HttpContext.Current.Session["SecurityService"];
            }
        }


        public static CustomizerController MenuCustomizerController
        {
            get
            {
                if (HttpContext.Current.Session["MenuCustomizer"] == null)
                    InitializeMenuCustomizerService();
                return (CustomizerController)HttpContext.Current.Session["MenuCustomizer"];
            }
        }

    }
}
