using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using System.Text;
using System.Web.Configuration;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.SECURITY_NEW.Login;
using System.IO;
using WIN.FENGEST_NAZIONALE.DOMAIN.Serializzation;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler;
using System.Reflection;

namespace FenealgestWEB.WebServices
{
    /// <summary>
    /// Descrizione di riepilogo per JoinAllWebService
    /// </summary>
    [WebService(Namespace = "http://www.fenealgestweb.it/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class JoinAllWebService : System.Web.Services.WebService
    {

        IPersistenceFacade f;
        SecurityController c;
      
        private static ImportOptions GetOptionsFile()
        {
            //Recupero il file delle opzioni di importazione
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

            FileInfo f = new FileInfo(path);

            path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");


            return ObjectXMLSerializer<ImportOptions>.Load(path);
        }

        private void SendMail(string searchSubject, string body, string mailsTo)
        {
            ImportOptions options = GetOptionsFile();
            MailProvider m = new MailProvider(options.SmtpServer, options.SmtpAccount, options.SmtpPassword, true, options.SmtpMailFrom);


            m.SendMail(mailsTo, searchSubject, body);
        }

        private void CheckProvincia(string prov)
        {
            //verifico la provincia
            Utente u = (Utente)c.CurrentUser;
            if (u.Appartenenza.Provicnia != null)
            {
                if (!u.Appartenenza.Provicnia.Descrizione.Equals(prov))
                    throw new Exception("Accesso negato");
            }
            else
            {
                throw new Exception("Accesso negato");
            }
        }


        private string CheckIdentificationAndAuthorizzation(LoginResult r, string profileName)
        {
            //verifico l'accesso
            if (!r.CanAccess)
                return r.LoginMessage;



            //verifico l'autorizzazione
            if (!c.IsUserauthorized(profileName))
                return "Utente non autorizzato";

            return "";
        }

        private void Initialize()
        {
            //inizializzo i servizi per la persistenza
            f = DataAccessServices.SimplePersistenceFacadeInstance();


            //inizializzo i servizi per la sicurezza
            c = SecurityController.NewInstance;
            c.InializeSecurityController(new UserProvider(f), new WIN.FENGEST_NAZIONALE.DOMAIN.Security.RoleProvider(), new AccessChecker(new UserLocker(f)));
            c.ResetLogin();
        }

        [WebMethod]
        public string ImportIQA(string username, string password, IQATrace trace)
        {
            Initialize();
            string baseSavePath = "";
            if (WebConfigurationManager.AppSettings["testJoinAll"] == "true")
            {
                baseSavePath = WebConfigurationManager.AppSettings["JoinAllImportExportDir"];

            }
            else
            {
                baseSavePath = WebConfigurationManager.AppSettings["ImportExportDir"];
            }

            
            return ImportIQA(username, password, trace, baseSavePath);
        }



        [WebMethod]
        public string ImportLiberi(string username, string password, LiberiTrace trace)
        {
            
             Initialize();
            string baseSavePath = "";
            if (WebConfigurationManager.AppSettings["testJoinAll"] == "true")
            {
                baseSavePath = WebConfigurationManager.AppSettings["JoinAllImportExportDir"];

            }
            else
            {
                baseSavePath = WebConfigurationManager.AppSettings["ImportExportDir"];
            }
            return ImportLiberi(username, password, trace, baseSavePath);
        }



        private string ImportIQA(string username, string password, IQATrace trace, string baseSavePath)
        {

            if (trace == null)
                return "Traccia nulla. Nessuna elaborazione eseguita!";

            string prov = trace.Provincia;


            try
            {

             
                //verifico il'autanticazione
                //************************************
                //************************************
                CheckIdentificationData(username, password, prov);



                //valido la traccia
                //************************************
                //************************************
                string error = ValidateIQATrace(trace);

                if (!string.IsNullOrEmpty(error))
                {
                    SendMailForJoinAllIntegrationError(prov, error, "IQA");
                    return error;
                }
                   



                //adesso posso salvare il file
                //verifico il percorso , genero il nome del file e salvo
                //************************************
                //************************************
                string strdocPath = baseSavePath;

                //verifico se esiste il path
                if (!Directory.Exists(strdocPath))
                {
                    string errm =  "Cartella di base per il salvataggio dei documenti di integrazione joinall non esistente. Contattare l'amministratore";
                    SendMailForJoinAllIntegrationError(prov, errm, "IQA");
                    return errm;

                }
                    

                //se la cartella esiste verifico se esiste la cartella della provincia
                strdocPath = strdocPath + "//" + prov;
                //verifico se esiste il path se non esiste la creo
                if (!Directory.Exists(strdocPath))
                    Directory.CreateDirectory(strdocPath);

                //ora posso inserire il file
                //tutti i file avranno il seguente formato tipo_yyyy_MM_dd_hh_mm.xml
                //il campo tipo può essere "iqa" oppure "liberi"

                DateTime t = DateTime.Now;
                string type = "IQA" ;
                strdocPath += string.Format("//{0}_{1}_{2}_{3}_{4}_{5}.xml", type, t.Year, t.Month, t.Day, t.Hour, t.Minute);

                //salvo il file
                ObjectXMLSerializer<IQATrace>.Save(trace, strdocPath);
            

                // a questo punto posso inviare la mail di conferma avvenuta integrazione
                //************************************
                //************************************
                //.....
                string subject = string.Format( "(Servizio JoinAll) Invio IQA  da {0} ", prov);
                string body = "E' stato appena iviato un file IQA dal servizio JoinAll";

                try
                {
                    SendMail(subject, body, WebConfigurationManager.AppSettings["adminMail"]);
                }
                catch (Exception)
                {
                    
                    //non fa nulla
                }

                

                return "";

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string type = "IQA";
                try
                {
                    SendMailForJoinAllIntegrationError(prov, message, type);
                }
                catch (Exception)
                {

                    return ex.Message;
                }

                return ex.Message;
            }

        }

        private void SendMailForJoinAllIntegrationError(string prov, string message, string type)
        {
            string subject = string.Format("(Servizio JoinAll) Errore nell'invio di un documento {0}  da {1} ",type, prov);
            string body = "E' stato appena iviato un file "  + type + " dal servizio JoinAll ed è stato prodotto il seguente errore: " + message;

            SendMail(subject, body, WebConfigurationManager.AppSettings["adminMail"]);
        }

        private  string ValidateIQATrace(IQATrace trace)
        {
            //a questo punto posso inviare il file previa validazione
            trace.Validate();

            if (trace.ValidationErrors.Count > 0)
            {
                StringBuilder error = new StringBuilder();
                error.AppendLine("Si sono verificati errori di validazione nell'oggetto inviato!" + Environment.NewLine + Environment.NewLine);
                foreach (string item in trace.ValidationErrors)
                {
                    error.Append(item + Environment.NewLine);
                }
                return error.ToString(); ;
            }

            return "";
        }

        private void CheckIdentificationData(string username, string password, string prov)
        {
           

            //eseguo il login
            LoginResult r = c.Login(username, password);

            //verifico identificazione ed autorizzazione
            string error = CheckIdentificationAndAuthorizzation(r, "SaveImportExportFile");


            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);


            //se arrivo qui l'utente è loggato
            CheckProvincia(prov);
        }




        private string ImportLiberi(string username, string password, LiberiTrace trace, string baseSavePath)
        {
            if (trace == null)
                return "Traccia nulla. Nessuna elaborazione eseguita!";



            string prov = trace.Provincia;

            try
            {

          


                //verifico il'autanticazione
                //***********************************
                //************************************
                CheckIdentificationData(username, password, prov);



                //valido la traccia
                //************************************
                //************************************
                string error = ValidateLiberiTrace(trace);

                if (!string.IsNullOrEmpty(error))
                {
                    SendMailForJoinAllIntegrationError(prov, error, "Liberi");
                    return error;
                }



                //adesso posso salvare il file
                //verifico il percorso , genero il nome del file e salvo
                //************************************
                //************************************
                string strdocPath = baseSavePath;

                //verifico se esiste il path
                if (!Directory.Exists(strdocPath))
                {
                    string errm = "Cartella di base per il salvataggio dei documenti di integrazione joinall non esistente. Contattare l'amministratore";
                    SendMailForJoinAllIntegrationError(prov, errm, "Liberi");
                    return errm;
                }
                   

                //se la cartella esiste verifico se esiste la cartella della provincia
                strdocPath = strdocPath + "//" + prov;
                //verifico se esiste il path se non esiste la creo
                if (!Directory.Exists(strdocPath))
                    Directory.CreateDirectory(strdocPath);

                //ora posso inserire il file
                //tutti i file avranno il seguente formato tipo_yyyy_MM_dd_hh_mm.xml
                //il campo tipo può essere "iqa" oppure "liberi"

                DateTime t = DateTime.Now;
                string type = "Liberi" ;
                strdocPath += string.Format("//{0}_{1}_{2}_{3}_{4}_{5}.xml", type, t.Year, t.Month, t.Day, t.Hour, t.Minute);

                //salvo il file
                ObjectXMLSerializer<LiberiTrace>.Save(trace, strdocPath);
            

                // a questo punto posso inviare la mail di conferma avvenuta integrazione
                //************************************
                //************************************
                string subject = string.Format("(Servizio JoinAll) Invio Liberi  da {0} ", prov);
                string body = "E' stato appena iviato un file Liberi dal servizio JoinAll";

                try
                {
                    SendMail(subject, body, WebConfigurationManager.AppSettings["adminMail"]);
                }
                catch (Exception)
                {
                    
                    //nonn fa nulla
                }
               
                //.....


                return "";

            }
            catch (Exception ex)
            {

                string message = ex.Message;
                string type = "Liberi";
                try
                {
                    SendMailForJoinAllIntegrationError(prov, message, type);
                }
                catch (Exception)
                {

                    return ex.Message;
                }

                return ex.Message;
            }

        }

        private string ValidateLiberiTrace(LiberiTrace trace)
        {
            //a questo punto posso inviare il file previa validazione
            trace.Validate();

            if (trace.ValidationErrors.Count > 0)
            {
                StringBuilder error = new StringBuilder();
                error.AppendLine("Si sono verificati errori di validazione nell'oggetto inviato!" + Environment.NewLine + Environment.NewLine);
                foreach (string item in trace.ValidationErrors)
                {
                    error.Append(item + Environment.NewLine);
                }
                return error.ToString(); ;
            }

            return "";
        }


    }
}
