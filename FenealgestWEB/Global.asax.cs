using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.SessionState;


using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.SECURITY_NEW;
using FenealgestWEB.Utility;

namespace FenealgestWEB
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Application.Set("SessionCount", 0);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Application.Lock();

            int count = Convert.ToInt32(HttpContext.Current.Application.Get("SessionCount"));
            HttpContext.Current.Application.Set("SessionCount", count + 1);

            HttpContext.Current.Application.UnLock();


           SessionServiceManager.InitializeSecurityService();
           SessionServiceManager.InitializeMenuCustomizerService();
           SessionServiceManager.InitializeGeoHandlerService();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Server.Transfer (LinkHandler.ErrorPage);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                Application.Lock();

                int count = Convert.ToInt32(Application.Get("SessionCount"));
                Application.Set("SessionCount", count - 1);

                Application.UnLock();
            }
            catch (Exception ex)
            {

                string l = ex.Message;
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}