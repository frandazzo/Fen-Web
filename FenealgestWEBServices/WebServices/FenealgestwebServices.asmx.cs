using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.SECURITY_NEW.Login;
using WIN.TECHNICAL.MIDDLEWARE.QueueHandlers;
using System.Messaging;
using System.Web.Configuration;
//using FenealgestWEB.Utility;
using FenealgestWEB.WebServices.DTOWrappers;
using System.Web.Services.Protocols;
using WIN.FENGEST_NAZIONALE.HANDLERS;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.HANDLERS.GeoHandler;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using BilancioFenealgest.DomainLayer;
using WIN.FENGEST_NAZIONALE.HANDLERS.Financial;
using System.IO;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using System.Reflection;
using System.Web.Handlers;
using System.Threading;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace FenealgestWEB.WebServices
{
    /// <summary>
    /// Descrizione di riepilogo per FenealgestwebServices
    /// </summary>
    [WebService(Namespace = "http://www.fenealgestweb.it/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class FenealgestwebServices : System.Web.Services.WebService
    {
        IPersistenceFacade f;
        SecurityController c;
        WIN.BASEREUSE.GeoLocationFacade g;

        public Credenziali credential;

        [WebMethod]
        public string ImportData(ExportTrace trace)
        {
            string result = "";

            try
            {
                string userName = trace.UserName;
                string password = trace.Password;
                string prov = trace.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);


                string error = CheckIdentificationAndAuthorizzation(r, "ImportazioneWebService");

                if (!string.IsNullOrEmpty(error))
                    return error;

                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);

                //verifico che la provincia dell'utente sia 
                //la stessa della provincia della traccia
                if (trace.Province == null)
                    return "Provincia di importazioe errata";

                Utente u = c.CurrentUser as Utente;

                //situazione impossibile ma per sicurezza!!!!
                if (u == null)
                    return "Utente non trovato";

                //verifico la presenza di una provincia per l'utente
                if (u.Appartenenza.Provicnia == null)
                    return "Utente con dati incompleti";

                //situazione impossibile sul primo if ma possibile sul secondo
                if (!string.IsNullOrEmpty(trace.Province))
                    if (!trace.Province.ToUpper().Equals(u.Appartenenza.Provicnia.Descrizione))
                        return "Provincia di importazioe errata";


                //a questo punto punto posso procedere all'importazione
                QueueHandler h = new QueueHandler();

                string queue = WebConfigurationManager.AppSettings["QueueName"];

                string lab = trace.Province + "_" + trace.Sector + "_";

                string period = "";
                if (trace.PeriodType == PeriodType.Semester)
                    period = "(sem " + trace.Period.ToString() + " " + trace.Year.ToString() + ")";
                else
                    period = "(" + trace.Year.ToString() + ")";

                string errorLogDir = WebConfigurationManager.AppSettings["ErrorLogDir"];

                h.SendMessage(lab + period + "_" + trace.ExportNumber, trace, queue, errorLogDir);


                return "";
            }
            catch (Exception ex)
            {
                return result = "Errore irreversibile: " + Environment.NewLine + ex.Message;
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
        [SoapHeader("credential")]
        public QueryResultDTO ExportWorker(string fiscalCode, string regionName)
        {
            QueryResultDTO result = new QueryResultDTO();

            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;



                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "EsportazioneLavoratoreWebService");

                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);
                    
                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);



                //creo il servizio geografico
                
                GeoLocationFacade.InitializeInstance(new GeoHandler(f));
                g = GeoLocationFacade.Instance();

                Regione regione;
                if (string.IsNullOrEmpty(regionName))
                    regione = new RegioneNulla();
                else
                    regione = g.GetGeoHandler().GetRegioneByName(regionName);

                


                //a questo punto posso eseguire il metodo
                WorkerHandler h = new WorkerHandler(f, g);

                h.LoadByFiscalCode(fiscalCode);

                if (h.Found)
                {
                    h.LoadWorkerWithSusbscriptionsAndDocuments(regione);
                    WorkerDTO w = h.CurrentWorker.ToWorkerDTO();
                    result.IsResultValid = true;
                    result.WorkerDTO = w;
                }
                else
                {
                    result.IsResultValid = true;
                    result.WorkerDTO = new WorkerDTO();
                    result.WorkerDTO.Fiscalcode = fiscalCode;
                    result.Message = "Nessun risultato trovato";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.IsResultValid = false;
                result.Error = ex.Message;
                return result;
            }
        
        
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

        private void CheckRegione(string regione)
        {
            //verifico la provincia
            Utente u = (Utente)c.CurrentUser;
            if (u.Appartenenza.Regione != null)
            {
                if (!u.Appartenenza.Regione.Descrizione.Equals(regione))
                    throw new Exception("Accesso negato");
            }
            else
            {
                throw new Exception("Accesso negato");
            }
        }



        [WebMethod]
        [SoapHeader("credential")]
        public string ImportRendicontoRlst(DTORendiconto rendiconto)
        {

            return "Scaricare una versione più recente del rendiconto per effettuare l'invio dei dati al database nazionale. Contattare l'amministratore del sistema";//ExecuteImportRendiconto(rendiconto, true);

        }


        [WebMethod]
        [SoapHeader("credential")]
        public string ImportRendiconto(DTORendiconto rendiconto)
        {

            return "Scaricare una versione più recente del rendiconto per effettuare l'invio dei dati al database nazionale. Contattare l'amministratore del sistema";//return ExecuteImportRendiconto(rendiconto, false);

        }


        [WebMethod]
        
        public string TraceUsage(string province, string app, string region)
        {
            try
            {

                TraceProvider t = new TraceProvider(DataAccessServices.SimplePersistenceFacadeInstance());
                t.InsertOrUpdateTrace(app, province, region);


                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " +  ex.Message;
            }
            
        }


        [WebMethod]
        [SoapHeader("credential")]
        public string ImportRendicontoNew(DTORendiconto rendiconto)
        {
         
            if (rendiconto.Version >= 500)
                return ExecuteImportRendiconto(rendiconto, false);


            return "La versine del rendiconto è obsoleta. Contattare l'amministratore per l'invio dei dati al db nazionale";
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string ImportRendicontoRlstNew(DTORendiconto rendiconto)
        {
            if (rendiconto.Version >= 500)
                return ExecuteImportRendiconto(rendiconto, true);
            return "La versine del rendiconto è obsoleta. Contattare l'amministratore per l'invio dei dati al db nazionale";
        }





        private string ExecuteImportRendiconto(DTORendiconto rendiconto, bool forRlst)
        {
            if (credential == null)
                throw new Exception("Accesso negato");

            string userName = credential.UserName;
            string password = credential.Password;
            string prov = credential.Province;



            Initialize();



            //eseguo il login
            LoginResult r = c.Login(userName, password);


            string error = CheckIdentificationAndAuthorizzation(r, "ImportazioneBilancioWebService");

            if (!string.IsNullOrEmpty(error))
                return error;





            string result = "";

            try
            {


                //verifico i dati della regione e della provincia
                //la validazione sui corretti nomi della provincia
                //e della regione viene eseguita con il confronto
                //con i nomi della provincia e della regione  dell'utente loggato.
                //poichè l'utente è creato correttamente dal portale non dovrebbero crearsi problemi
                ValidateRendiconto(rendiconto, prov , forRlst);




                BilancioHandler h = new BilancioHandler(f, g);

                h.InsertOrUpdateRendiconto(rendiconto, forRlst);

                result = "";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }




            return result;
        }

        private void ValidateRendiconto(DTORendiconto rendiconto, string provincia_o_regione, bool forRlst)
        {
            if (!forRlst)
            {
                if (rendiconto.IsRegionale)
                {
                    //verifico i dati dell'utente loggato con le credenziali immesse nell'header soap
                    //(creadenziali di accesso al web service)
                    CheckUtenteIsRegionale(provincia_o_regione);
                    //verifico che la tipologia di rendiconto (regionale o provinciale) 
                    //corrisponda ai dati di provincia e/o regione dell'utente loggato
                    CheckRegione(rendiconto.Regione);
                    ChechProvinciaNulla(rendiconto);
                }
                else
                {
                    //verifico i dati dell'utente loggato con le credenziali immesse nell'header soap
                    //(creadenziali di accesso al web service)
                    CheckUtenteIsProvinciale(provincia_o_regione);
                    //verifico che la tipologia di rendiconto (regionale o provinciale) 
                    //corrisponda ai dati di provincia e/o regione dell'utente loggato
                    CheckProvincia(rendiconto.Provincia);
                    CheckRegione(rendiconto.Regione);
                }
            }
            else
            {
                //verifico che il bilancio sia provinciale
                if (rendiconto.IsRegionale)
                    throw new Exception("Il rendiconto Rlst può essere solamente provinciale");


                //eseguo tutti gli altri check
                CheckUtenteIsProvinciale(provincia_o_regione);
                CheckProvincia(rendiconto.Provincia);
                CheckRegione(rendiconto.Regione);

            }

               

        }

        private void ChechProvinciaNulla(DTORendiconto rendiconto)
        {
            if (!string.IsNullOrEmpty(rendiconto.Provincia))
                rendiconto.Provincia  = "";
        }

        private void CheckUtenteIsProvinciale(string credenzialeProvincia)
        {
            Utente u = (Utente)c.CurrentUser;

            if (u.TipoStruttura != WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Provinciale )
                throw new Exception("Accesso negato. Utente non provinciale");

            if (!u.Appartenenza.Provicnia.Descrizione.Equals(credenzialeProvincia))
                throw new Exception("Accesso negato");

        }

        private void CheckUtenteIsRegionale(string credenzialeRegione)
        {
            Utente u = (Utente)c.CurrentUser;

            if (u.TipoStruttura != WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Regionale)
                throw new Exception("Accesso negato. Utente non regionale o provinciale");

            if (!u.Appartenenza.Regione.Descrizione .Equals(credenzialeRegione ))
                throw new Exception("Accesso negato");
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SendMailForLiberiSearch(string[] fiscalCodes, string mailto, string searchSubject)
        {
            try
            {
                string SendCrossMail = WebConfigurationManager.AppSettings["sendCrossMail"];


                QueryResultDTO result = ExportWorkers(fiscalCodes, "");

                string[] mailsTo = mailto.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                //aggiungo la mail dell'amministratore
                Array.Resize<string>(ref mailsTo, mailsTo.Length + 1);
                //aggiungo alle mail la mail dell'amministratore del sistema
                mailsTo[mailsTo.Length - 1] = WebConfigurationManager.AppSettings["adminMail"];



                //creo l'oggetto della mail principale
                string mailBody = CreateHtmlTabularResults(result);






                if (SendCrossMail != "true")
                {
                    //adesso posso inviare la mail alla peersona interessata
                    SendMainMail(searchSubject, mailBody, mailsTo);
                   
                }
                else
                {

                    ////creo una lista di di cross dto 
                    ////ogni cross dto è un oggetto che che ha al suo interno unaproprietà
                    ////provincia e una proprietà queryresultdto
                    ////grazie a questa proprietà riesco ad inviare una mail alla provincia con il relativo dto
                    CrossDtoCreator creator = new CrossDtoCreator(credential.Province);
                    IList<CrossDto> dtos = creator.CreateDtos(result);

                    //se non ci sono errori e il result è valido allora posso inviare le mail incrociate a
                    //ttutte le province
                    if (result.IsResultValid)
                        if (result.Workers.Length > 0)
                        {
                            foreach (CrossDto item in dtos)
                            {
                                Thread t = new Thread(new ParameterizedThreadStart(FenealgestwebServices.SendCrossDataMail));

                                t.Start(item);
                            }
                        }

                    //invio infine la mail principale 
                    //avendo cura di ritoccare il corpo della mail con l'intestazione indicante che una ulteriore mail 
                    //è stata inviate alle altre province interessate

                    IList<string> provinces = ExtractProvinceFromCrossDTO(dtos);

                    if (provinces.Count > 0)
                    {
                        StringBuilder mailHeader = new StringBuilder();

                        mailHeader.Append("<p>Di seguito sono elencati i lavoratori che sono stati iscritti alla Feneal in altri territori.</p>");
                        mailHeader.Append("<br/>");
                        mailHeader.Append("<p>Una mail di notifica di tale report è stata inviata alle seguenti province:</p>");
                        mailHeader.Append("<br/>");
                        mailHeader.Append("<br/>");
                        foreach (string item in provinces)
                        {
                            mailHeader.Append(string.Format("<strong>{0}</strong><br/>", item));
                        }
                        mailHeader.Append("<br/>");
                        mailHeader.Append("<br/>");

                        mailBody = mailHeader.ToString() + mailBody;
                        SendMainMail(searchSubject, mailBody, mailsTo);
                    }
                    else
                    {
                        SendMainMail(searchSubject, mailBody, mailsTo);
                    }
                }


                    


               


                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
          
        }

        private IList<string> ExtractProvinceFromCrossDTO(IList<CrossDto> dtos)
        {
            throw new NotImplementedException();
        }

        //private IList<CrossDto> CreateCrossDtos(QueryResultDTO result)
        //{
        //    throw new NotImplementedException();
        //}



        [WebMethod]
        public void SendMailFromFenealgest(MailData maildata)
        {
            if (maildata.tos == null)
                return;

            if (maildata.tos.Length == 0)
                return;

            if (string.IsNullOrEmpty(maildata.sender))
                return;


            if (string.IsNullOrEmpty(maildata.subject))
                maildata.subject = "Messaggio senza oggetto";

            if (string.IsNullOrEmpty(maildata.body))
                maildata.body = "Messaggio vuoto";

            string[] tosEmails = maildata.tos;

            Array.Resize<string>(ref tosEmails, tosEmails.Length + 1);
            tosEmails[tosEmails.Length -1] = WebConfigurationManager.AppSettings["adminMail"];

            ImportOptions options = GetOptionsFile();
            MailProvider m = new MailProvider(options.SmtpServer, options.SmtpAccount, options.SmtpPassword, true, options.SmtpMailFrom);
            m.SendMail(tosEmails, maildata.subject + " (Inviato da " + maildata.sender + ")", maildata.body);

        }


        private void SendMainMail(string searchSubject, string body, string[] mailsTo)
        {
            ImportOptions options = GetOptionsFile();
            MailProvider m = new MailProvider(options.SmtpServer, options.SmtpAccount, options.SmtpPassword, true, options.SmtpMailFrom);


            m.SendMail(mailsTo, searchSubject, body);
        }

        public static void SendCrossDataMail(object item)
        {
            CrossDto dto = item as CrossDto;

            if (dto == null)
                return;

           

            if (dto.IsValid)
            {
                ImportOptions options = GetOptionsFile();
                MailProvider m = new MailProvider(options.SmtpServer, options.SmtpAccount, options.SmtpPassword, true, options.SmtpMailFrom);



                //invio la mail
                string subiect = "Richiesta informazioni lavoratori non iscritti per la provincia di " + dto.Province;

                string[] tos = new string[] { dto.MailTo ,WebConfigurationManager.AppSettings["adminMail"]};

                //creo la tabella dei lavoratori da inviare

                string messaggioMail = string.Format("<p><b>Alla Feneal di {0}</b></p><p>La provincia di {1} richiede informazioni circa i seguenti lavoratori precedentemente iscritti presso di Voi e che attualmente operano nel suo territorio di competenza</p><p>E' gradito un cortese riscontro. </p><br/>", dto.FromProvince,dto.Province);

                string mailBody = messaggioMail + CreateHtmlTabularResults(dto.Dto);

                m.SendMail(tos, subiect, mailBody);
            }
        }




        private static string CreateHtmlTabularResults(QueryResultDTO result)
        {
            HtmlTransformer t = new HtmlTransformer();
            return t.TransformToHtml(result);
        }

        private static ImportOptions GetOptionsFile()
        {
            //Recupero il file delle opzioni di importazione
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

            FileInfo f = new FileInfo(path);

            path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");


            return ObjectXMLSerializer<ImportOptions>.Load(path);
        }


        [WebMethod]
        [SoapHeader("credential")]
        public QueryResultDTO ExportWorkers(string[] fiscalCodes, string regionName)
        {
            QueryResultDTO result = new QueryResultDTO();

            if (fiscalCodes == null)
            {
                result.IsResultValid = false;
                return result;
            }

            if (fiscalCodes != null)
                if (fiscalCodes.Length == 0)
                {
                    result.IsResultValid = false;
                    return result;
                }



            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "EsportazioneLavoratoriWebService");


                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //creo il servizio geografico

                GeoLocationFacade.InitializeInstance(new GeoHandler(f));
                g = GeoLocationFacade.Instance();

                Regione regione;
                if (string.IsNullOrEmpty(regionName))
                    regione = new RegioneNulla();
                else
                    regione = g.GetGeoHandler().GetRegioneByName(regionName);



                //instanzio l'array dei risultati
                WorkerDTO[] wks = new WorkerDTO[] { };

                //a questo punto posso eseguire il metodo
                WorkerHandler h = new WorkerHandler(f, g);
                //int i = 0;
                //ciclo sui codici fiscali
                foreach (string fiscalcode in fiscalCodes)
                {
                    //if (i > 500)
                    //    break;

                    h.LoadByFiscalCode(fiscalcode);
                    WorkerDTO w;
                    if (h.Found)
                    {
                        h.LoadWorkerWithSusbscriptionsAndDocuments(regione);
                        w = h.CurrentWorker.ToWorkerDTO(); //   CreateWorker(regione, h.CurrentWorker);
                    }
                    else
                    {
                        w = new WorkerDTO();
                        w.Fiscalcode = fiscalcode;
                    }
                    Array.Resize<WorkerDTO>(ref wks, wks.Length + 1);
                    wks[wks.Length - 1] = w;

                }

                result.IsResultValid = true;
                result.Workers = wks;

                return result;
            }
            catch (Exception ex)
            {
                result.IsResultValid = false;
                result.Error = ex.Message;
                return result;
            }


        }





        [WebMethod]
        [SoapHeader("credential")]
        public bool UserIsValid()
        {


            try
            {
                if (credential == null)
                    return false;

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;



                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico l'accesso
                if (!r.CanAccess)
                    return false;


                //se arrivo qui l'utente è loggato
                Utente u = c.CurrentUser as Utente;

                //non dovrebbe mai accadere
                if (u == null)
                    return false;


                if (u.Appartenenza.Struttura == WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Provinciale)
                {

                    CheckProvincia(prov);



                    //verifico adesso le autorizzazioni
                    bool exportData;
                    bool searchWorkerList;
                    bool queryWorker;
                    bool queryWorkerList;
                    bool importBilancio;

                    exportData = c.IsUserauthorized("ImportazioneWebService");

                    queryWorker = c.IsUserauthorized("EsportazioneLavoratoreWebService");

                    queryWorkerList = c.IsUserauthorized("EsportazioneLavoratoriWebService");

                    searchWorkerList = c.IsUserauthorized("RicercaLavoratoriWebService");

                    importBilancio = c.IsUserauthorized("ImportazioneBilancioWebService");

                    if (exportData && queryWorker && queryWorkerList && searchWorkerList)
                        return true;

                    return false;

                }
                else if (u.Appartenenza.Struttura == WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Regionale)
                {
                    CheckRegione(prov);
                    if (c.IsUserauthorized("ImportazioneBilancioWebService"))
                        return true;
                    return false;
                }
                else
                {
                    return false;
                }

               

            }
            catch (Exception)
            {
                return false;
            }

        }

        [WebMethod]
        [SoapHeader("credential")]
        public QueryResultDTO SearchWorkersByAzienda(string azienda)
        {
            QueryResultDTO result = new QueryResultDTO();

            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = "";// CheckIdentificationAndAuthorizzation(r, "RicercaLavoratoriWebService");


                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //creo il servizio geografico

                GeoLocationFacade.InitializeInstance(new GeoHandler(f));
                g = GeoLocationFacade.Instance();




                //instanzio l'array dei risultati
                WorkerDTO[] wks = new WorkerDTO[] { };

               
                //passo il nome della provincia
                Provincia p = GeoLocationFacade.Instance().GetGeoHandler().GetProvinciaByName(prov);

                //recupero tutti coloro che in italia hanno avuto a che fare con l'azienda
                //o con aziende omonime
                SubscriptionHandler h = new SubscriptionHandler(f, g);
                IList<Worker> workers = h.FindWorkersByAzienda(azienda);

                //ciclo sui codici fiscali
                foreach (Worker item in workers)
                {
                    WorkerDTO w;
                    w = item.ToWorkerDTO();
                    //imposto inoltre il campo iscritto_a a feneal in modo da identificare lato client gli iscritti alla feneal "Feneal"
                    w.IscrittoA = "GENERALE";

                    Array.Resize<WorkerDTO>(ref wks, wks.Length + 1);
                    wks[wks.Length - 1] = w;

                }





                //recupero gli iscritti dell'azienda
              
                //IList<Worker> workers1 = h.FindWorkersByAzienda(azienda, p.Id);

                ////ciclo sui codici fiscali
                //foreach (Worker item in workers1)
                //{
                //    WorkerDTO w;
                //    w = item.ToWorkerDTO();
                //    //imposto inoltre il campo iscritto_a a feneal in modo da identificare lato client gli iscritti alla feneal "Feneal"
                //    w.IscrittoA = "FENEAL";
                    
                //    Array.Resize<WorkerDTO>(ref wks, wks.Length + 1);
                //    wks[wks.Length - 1] = w;

                //}

                //recupero i liberi dell'azienda
                //LiberoHandler h1 = new LiberoHandler(f, g);
                //IList<Libero> libeir = h1.FindLiberiData(azienda, p.Id);
                //foreach (Libero item in libeir)
                //{
                //    WorkerDTO w;
                //    w = item.ToWorkerDTO();
                   

                //    Array.Resize<WorkerDTO>(ref wks, wks.Length + 1);
                //    wks[wks.Length - 1] = w;

                //}



                result.IsResultValid = true;
                result.Workers = wks;
                result.Message = "";

                return result;
            }
            catch (Exception ex)
            {
                result.IsResultValid = false;
                result.Error = ex.Message;
                return result;
            }


        }

        [WebMethod]
        [SoapHeader("credential")]
        public QueryResultDTO SearchWorkers(QueryParameters parameters)
        {
            QueryResultDTO result = new QueryResultDTO();

            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "RicercaLavoratoriWebService");


                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //creo il servizio geografico

                GeoLocationFacade.InitializeInstance(new GeoHandler(f));
                g = GeoLocationFacade.Instance();

               


                //instanzio l'array dei risultati
                WorkerDTO[] wks = new WorkerDTO[] { };

                //a questo punto posso eseguire la query
                WorkerHandler h = new WorkerHandler(f, g);
               //definisco i parametri
                WorkerQueryParameters param = new WorkerQueryParameters(parameters.Name, parameters.Surname, parameters.LivingPlace, parameters.Nationality, parameters.BirthDate, parameters.CheckDate, parameters.Region,200,parameters.CompanyFiscalCode ,parameters.CompanyDescription );

                //passo il nome della provincia
                param.Province = GeoLocationFacade.Instance().GetGeoHandler().GetProvinciaByName(prov);


                WorkerQueryResult res = h.SearchWorkers(param, true);


                //ciclo sui codici fiscali
                foreach (Worker item in res.Workers)
                {
                    WorkerDTO w;
                    w = item.ToWorkerDTO(); 
                   
                    Array.Resize<WorkerDTO>(ref wks, wks.Length + 1);
                    wks[wks.Length - 1] = w;

                }

                result.IsResultValid = true;
                result.Workers = wks;
                result.Message = res.Message;

                return result;
            }
            catch (Exception ex)
            {
                result.IsResultValid = false;
                result.Error = ex.Message;
                return result;
            }


        }


        [WebMethod]
        [SoapHeader("credential")]
        public int GetDocumentLen(string fileName)
        {
            return DoGetDocumentLength(fileName, false);
            
        }

        [WebMethod]
        [SoapHeader("credential")]
        public int GetDocumentLenFromServerSideAnalysis(string fileName)
        {
            return DoGetDocumentLength(fileName, true);

        }


        private int DoGetDocumentLength(string fileName, bool serverSide)
        {
            try
            {

                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "GetDocumentLen");


                if (!string.IsNullOrEmpty(error))
                    return 0;


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //adesso posso salvare il file
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];


                string file = "";
                if (serverSide)
                    file = String.Format("{0}//{1}//{2}//{3}", strdocPath, prov, "ServerSideAnalysisData", fileName);
                else 
                    file = String.Format("{0}//{1}//{2}", strdocPath, prov, fileName);

                //verifico se esiste il path se non esiste  ritorno
                if (!File.Exists(file))
                    return 0;

                FileStream objfilestream = new FileStream(file, FileMode.Open, FileAccess.Read);
                int len = (int)objfilestream.Length;
                objfilestream.Close();

                return len;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        [WebMethod]
        [SoapHeader("credential")]
        public Byte[] GetDocument(string fileName)
        {

            return DoGetDocument(fileName, false);

        }

        [WebMethod]
        [SoapHeader("credential")]
        public Byte[] GetDocumentFromServerSideAnalysis(string fileName)
        {

            return DoGetDocument(fileName, true);

        }

        private byte[] DoGetDocument(string fileName, bool serverSide)
        {
            try
            {

                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "GetDocument");


                if (!string.IsNullOrEmpty(error))
                    return new Byte[] { };


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //adesso posso salvare il file
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];


                string file = "";
                if (serverSide)
                    file = String.Format("{0}//{1}//{2}//{3}", strdocPath, prov, "ServerSideAnalysisData",fileName);
                else
                    file =String.Format("{0}//{1}//{2}", strdocPath, prov, fileName);

                //verifico se esiste il path se non esiste  ritorno
                if (!File.Exists(file))
                    return new Byte[] { };



                FileStream objfilestream = new FileStream(file, FileMode.Open, FileAccess.Read);
                int len = (int)objfilestream.Length;
                Byte[] documentcontents = new Byte[len];
                objfilestream.Read(documentcontents, 0, len);
                objfilestream.Close();

                return documentcontents;


            }
            catch (Exception)
            {
                return new Byte[] { };
            }
        }



        [WebMethod]
        [SoapHeader("credential")]
        public string[] GetListOfFileToDownload(string type)
        {

            return DoGetListOfFileToDownload(type, false);

        }

        [WebMethod]
        [SoapHeader("credential")]
        public string[] GetListOfFileToDownloadFromServerSideAnalysis(string type)
        {

            return DoGetListOfFileToDownload(type, true);

        }

        private string[] DoGetListOfFileToDownload(string type, bool serverSide)
        {
            try
            {



                string[] result = new string[] { };

                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "GetListOfFileToDownload");


                if (!string.IsNullOrEmpty(error))
                    return new string[] { "ERRORE:" + error };


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //adesso posso salvare il file
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];



               
                if (serverSide)
                    strdocPath = String.Format("{0}//{1}//{2}//", strdocPath, prov, "ServerSideAnalysisData");
                else
                    strdocPath = String.Format("{0}//{1}//", strdocPath, prov);

                //verifico se esiste il path se non esiste  ritorno
                if (!Directory.Exists(strdocPath))
                    return result;



                //cerco adesso tuttti i file che iniziano con IQA o con liberi
                string[] filePaths;

                if (type == "IQA")
                    filePaths = Directory.GetFiles(strdocPath, "IQA_*");
                else if (type == "Liberi")
                    filePaths = Directory.GetFiles(strdocPath, "Liberi_*");
                else if (type == "Inps")
                    filePaths = Directory.GetFiles(strdocPath, "Inps_*");
                else
                    filePaths = Directory.GetFiles(strdocPath, "IQA_*");



                //poichè ho ottenuto la lista di tutti i nomi comppleti aggiungo all'array result
                //lista di tutti if nomi dei file 

                foreach (string item in filePaths)
                {
                    Array.Resize<string>(ref result, result.Length + 1);
                    result[result.Length - 1] = Path.GetFileName(item);

                }



                return result;


            }
            catch (Exception ex)
            {
                return new string[] { "ERRORE:" + ex.Message };
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string RenameImportExportFile(string fileName)
        {
            return DoRenameImportExportFiles(fileName, false);

        }

        [WebMethod]
        [SoapHeader("credential")]
        public string RenameImportExportFileFomServerSideAnalysis(string fileName)
        {
            return DoRenameImportExportFiles(fileName, true);

        }

        private string DoRenameImportExportFiles(string fileName, bool serverSide)
        {
            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "RenameImportExportFile");


                if (!string.IsNullOrEmpty(error))
                    return error;


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);


                //adesso posso salvare il file
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];


                string file = "";
                if (serverSide)
                    file = String.Format("{0}//{1}//{2}//{3}", strdocPath, prov, "ServerSideAnalysisData", fileName);
                else
                    file = String.Format("{0}//{1}//{2}", strdocPath, prov, fileName);

                //verifico se esiste il path se non esiste  ritorno
                if (!File.Exists(file))
                    return "File non trovato";


                //posso adesso rinominare il file 

                File.Move(file, String.Format("{0}//{1}//Managed_{2}", strdocPath, prov, fileName));


                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        [SoapHeader("credential")]
        public string SaveImportExportFile(Byte[] docbinaryarray, bool isIqa)
        {
            return DoSaveImportExportFile(docbinaryarray, isIqa, false);


        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveImportExportFileFromServerSideAnalysis(Byte[] docbinaryarray, bool isIqa)
        {
            return DoSaveImportExportFile(docbinaryarray, isIqa, true);


        }


        private string DoSaveImportExportFile(Byte[] docbinaryarray, bool isIqa, bool serverSide)
        {
            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "SaveImportExportFile");


                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);



                //adesso posso salvare il file
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];

                //verifico se esiste il path
                if (!Directory.Exists(strdocPath))
                    return "Cartella di base per il salvataggio dei documenti non esistente. Contattare l'amministratore";

                //se la cartella esiste verifico se esiste la cartella della provincia
                strdocPath = strdocPath + "//" + prov;
                //verifico se esiste il path se non esiste la creo
                if (!Directory.Exists(strdocPath))
                    Directory.CreateDirectory(strdocPath);


                if (serverSide)
                {
                    strdocPath = strdocPath + "//ServerSideAnalysisData";
                    if (!Directory.Exists(strdocPath))
                        Directory.CreateDirectory(strdocPath);
                }



                //ora posso inserire il file
                //tutti i file avranno il seguente formato tipo_yyyy_MM_dd_hh_mm.xml
                //il campo tipo può essere "iqa" oppure "liberi"

                DateTime t = DateTime.Now;
                string type = (isIqa) ? "IQA" : "Liberi";

                strdocPath += string.Format("//{0}_{1}_{2}_{3}_{4}_{5}_{6}.xml", type, t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);

                FileStream objfilestream = new FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite);
                objfilestream.Write(docbinaryarray, 0, docbinaryarray.Length);
                objfilestream.Close();

                if (!isIqa)
                {
                    LiberiData d = new LiberiData();
                    d.Filename = strdocPath;
                    d.ProvinceName = prov;
                    Thread thr = new Thread(new ParameterizedThreadStart(ImportLiberi));
                    thr.Start(d);
                }



                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        private void ImportLiberi(object data)
        {
            LiberiData d = (LiberiData)data;
            string file = d.Filename;

            LiberiTrace trace = ObjectXMLSerializer<LiberiTrace>.Load(file);

            LiberiValidationHandler vv = new LiberiValidationHandler("Feneal", null);
            IList<LiberiTrace> ll = vv.CreateSubExportTraceList(500, trace);


            foreach (LiberiTrace item in ll)
            {
                if (item != null)
                {
                    item.Provincia = d.ProvinceName;

                    //metto tutto in coda
                    //a questo punto punto posso procedere all'importazione
                    QueueHandler h = new QueueHandler();

                    string queue = WebConfigurationManager.AppSettings["QueueName"];

                    string lab = item.Provincia + "__IMPORTLIBERI__";

                    string period = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

                    string errorLogDir = WebConfigurationManager.AppSettings["ErrorLogDir"];

                    h.SendMessage(lab + period + "__" + item.ExportNumber, item, queue, errorLogDir );


                    //LiberiImportHandler h = new LiberiImportHandler(tr);
                    //h.Import();
                }
            }

        }



        [WebMethod]
        [SoapHeader("credential")]
        public string SaveImportExportFileInps(Byte[] docbinaryarray)
        {
            return DoSaveImportExportFileInps(docbinaryarray, false);


        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveImportExportFileInpsForServerSideAnalysis(Byte[] docbinaryarray)
        {
            return DoSaveImportExportFileInps(docbinaryarray, true);


        }

        private string DoSaveImportExportFileInps(Byte[] docbinaryarray, bool serverSide)
        {
            try
            {
                if (credential == null)
                    throw new Exception("Accesso negato");

                string userName = credential.UserName;
                string password = credential.Password;
                string prov = credential.Province;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                //verifico identificazione ed autorizzazione
                string error = CheckIdentificationAndAuthorizzation(r, "SaveImportExportFile");


                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);


                //se arrivo qui l'utente è loggato
                CheckProvincia(prov);



                //adesso posso salvare il file
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];

                //verifico se esiste il path
                if (!Directory.Exists(strdocPath))
                    return "Cartella di base per il salvataggio dei documenti non esistente. Contattare l'amministratore";

                //se la cartella esiste verifico se esiste la cartella della provincia
                strdocPath = strdocPath + "//" + prov;
                //verifico se esiste il path se non esiste la creo
                if (!Directory.Exists(strdocPath))
                    Directory.CreateDirectory(strdocPath);




                if (serverSide)
                {
                    strdocPath = strdocPath + "//ServerSideAnalysisData";
                    if (!Directory.Exists(strdocPath))
                        Directory.CreateDirectory(strdocPath);
                }

                //ora posso inserire il file
                //tutti i file avranno il seguente formato tipo_yyyy_MM_dd_hh_mm.xml
                //il campo tipo può essere "iqa" oppure "liberi"

                DateTime t = DateTime.Now;
                string type = "Inps";

                strdocPath += string.Format("//{0}_{1}_{2}_{3}_{4}_{5}.xml", type, t.Year, t.Month, t.Day, t.Hour, t.Minute);

                FileStream objfilestream = new FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite);
                objfilestream.Write(docbinaryarray, 0, docbinaryarray.Length);
                objfilestream.Close();




                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }



    }
}
