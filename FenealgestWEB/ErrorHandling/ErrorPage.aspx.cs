using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Diagnostics;
using System.Text;

namespace FenealgestWEB.ErrorHandling
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        private EventLog eventLog1;
        private const string unknownError = "Errore sconosciuto";

        protected void Page_Load(object sender, EventArgs e)
        {

            //Inizializzo il registratore degli eventi di sistema
            InitializeLog();
            //inizializzo la stringa per il log
            StringBuilder b = new StringBuilder();

            //prendo l'ultimo errore ricevuto sul server
            Exception ex = Server.GetLastError();
            Exception inner;


            string err = "";
            string errType = "";
            string errStack = "";
            string errInternal = "";
            string errInternalType = "";

            if (ex != null)
            {
                //imposto lìeccezione interna
                inner = ex.InnerException;

                if (inner == null)
                {
                    err = ex.Message;
                    errStack = ex.StackTrace;
                    errType = ex.GetType().ToString();
                    b.AppendLine("Eccezione: " + err);
                    b.AppendLine("Tipo eccezione: " + errType);
                    b.AppendLine("");
                    b.AppendLine("Stack eccezione: " + errStack);
                }
                else
                {
                    //definisco le variabile d'errore
                    err = inner.Message;

                    errType = inner.GetType().ToString();

                    errStack = inner.StackTrace;

                    if (inner.InnerException != null)
                    {
                        errInternal = inner.InnerException.Message;
                        errInternalType = inner.InnerException.GetType().ToString();
                    }

                    //costriusco la variabile errore da riporre nel log

                    b.AppendLine("Eccezione: " + err);
                    b.AppendLine("Tipo eccezione: " + errType);
                    b.AppendLine("");
                    b.AppendLine("");
                    b.AppendLine("Eccezione interna: " + errInternal);
                    b.AppendLine("Tipo eccezione interna: " + errInternalType);
                    b.AppendLine("");
                    b.AppendLine("");
                    b.AppendLine("Stack eccezione: " + errStack);
                }
              


            }
            else
            {
                err = unknownError;
            }





            //verifico la configurazione di sistema per mostrare un errore
            //più leggibile se sono in un ambiente di sviluppo
            string conf = WebConfigurationManager.AppSettings["ProductionEnvironment"];




            lblErrorMessage.Text = "Eccezione: " + err;
            lblErrorType.Text = "Tipo eccezione: " + errType;

            lblInternal.Text = "Eccezione interna: " + errInternal;
            lblInternalType.Text = "Tipo eccezione interna: " + errInternalType;


            //se sono in ambiente di produzione mostro una pagina semplice di errore come 
            //impostato nella sezione custom errors del web config
            //altrimenti mostro un errore completo anche dello stack
            if (conf == "false")
            {
                lblStack.Text = "Stack completo eccezione: " + errStack;
            }

            LogError(b.ToString());


            //fermo la catena delle eccezioni
            Server.ClearError();
            //}
            //else
            //{
            //    //non fa nulla e la catena di eccezioni viene intercettata dalle impostazioni
            //    //custom errors nel web.config
            //    if (err != unknownError)
            //        LogError(b.ToString());

            //}

            //LoadMenu();

        }

        private void LogError(string err)
        {
            try
            {
                eventLog1.WriteEntry(err, EventLogEntryType.Error);
            }
            catch (Exception)
            {
                //possono esserci problemi di sicurezza
            }
        }

        //private void LoadMenu()
        //{

        //    //MenuItem i = new MenuItem();
        //    //if (SessionServiceManager.SecurityService.IsUserLogged())
        //    //{
        //    //    i.Text = "HOME PAGE";
        //    //    i.Value = "ViewSHomepage";
        //    //    i.NavigateUrl = LinkHandler.LinkReservedAreaHomepage;
        //    //}
        //    //else
        //    //{
        //    //    i.Text = "HOME PAGE";
        //    //    i.Value = "ViewSHomepage";
        //    //    i.NavigateUrl = LinkHandler.LinkHomepage;
        //    //}
        //    //this.mainMenu.Items.Add(i);
        //}

        private void InitializeLog()
        {

            try
            {
                //if (!System.Diagnostics.EventLog.SourceExists(WebConfigurationManager.AppSettings["LogSource"]))
                //{
                //    System.Diagnostics.EventLog.CreateEventSource(
                //        WebConfigurationManager.AppSettings["LogSource"], WebConfigurationManager.AppSettings["LogName"]);
                //}
                eventLog1 = new EventLog();

                eventLog1.Source = WebConfigurationManager.AppSettings["LogSource"];
                eventLog1.Log = WebConfigurationManager.AppSettings["LogName"];
                eventLog1.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 1);
            }
            catch (Exception)
            {

                //
            }

        }
    }
}
