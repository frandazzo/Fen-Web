using System;
using System.Collections.Generic;

using System.Text;
using WIN.TECHNICAL.SERVICE_PROCESSOR;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using WIN.TECHNICAL.MIDDLEWARE.Files;

namespace WIN.FENGEST_NAZIONALE.IMPORT_EXPORT
{
    public class ImportExportService : IServiceProcessor
    {
        private bool _partial = false;
        private static void InitializeX509CertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
        }


        private static bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
         
            return true;
        }

        public ImportExportService()
        {
            InitializeX509CertificateValidation();
        }

        public void Process()
        {

            EventLog log = new EventLog();
            log.Source = "FenealgestImportExportDataService";
            log.Log = "ImportExportServiceLogFenealgest";
            log.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 5);

                //***************************************************************
                log.WriteEntry("Inizializzazione servizio web");
          
                FenealgestServices.FenealgestwebServices svc = new FenealgestServices.FenealgestwebServices();

                log.WriteEntry("Inizializzazione servizio web eseguita");
                //***************************************************************




                //******************************************************************
                log.WriteEntry("Verifica se si tratta di Sincronizzazione parziale di Fenealgest");
                
                _partial = ClassPathFileFinder.ExistFileInExecutingAssemblyPathOrInAncestorFolder("OnlyLocal.txt", "NOESIS");
                if (_partial)
                {
                    log.WriteEntry("Esiste il file OnlyLocal.txt. Pertanto la sincronizzazione sarà parziale");
                }
                else
                {
                    log.WriteEntry("Non esiste il file OnlyLocal.txt. Pertanto la sincronizzazione sarà completa");
                }


                //gli imposto l'intestazione

                log.WriteEntry("Inizio recupero credenziali");
                svc.CredenzialiValue = CreateCredentials();


                if ((svc.CredenzialiValue == null))
                {
                    log.WriteEntry("Credenziali nulle. Il processo viene chiuso");
                    return;
                }

                log.WriteEntry(string.Format("Inizializzazione servizio web eseguita con le credenziali: username: {0}; provincia:{1}", svc.CredenzialiValue.UserName, svc.CredenzialiValue.Province));

                //richiedo la lista dei file presenti
                //***************************************************************
                //***************************************************************

                log.WriteEntry("Inizio recupero lista documenti IQA");


                string[] iqas = null;
                try
                {
                    if (_partial)
                        iqas = svc.GetListOfFileToDownloadFromServerSideAnalysis("IQA");
                    else
                        iqas = svc.GetListOfFileToDownload("IQA");
                }
                catch (Exception ex)
                {
                    log.WriteEntry(string.Format("Errore nella chiamata al servizio ricerca dei documenti IQA: {0}; il servizio chiude!", ex.Message));
                    return;
                    
                }
                

                log.WriteEntry(string.Format("Trovati {0} documenti IQA", iqas.Length ));
                //***************************************************************
                //***************************************************************
                log.WriteEntry("Inizio recupero lista documenti Liberi");

                string[] lib = null;
                try
                {
                    if (_partial)
                        lib = svc.GetListOfFileToDownloadFromServerSideAnalysis("Liberi");
                    else
                        lib = svc.GetListOfFileToDownload("Liberi");
                   
                }
                catch (Exception ex)
                {
                    log.WriteEntry(string.Format("Errore nella chiamata al servizio ricerca dei documenti Liberi: {0}; il servizio chiude!", ex.Message));
                    return;

                }

                log.WriteEntry(string.Format("Trovati {0} documenti Liberi", lib.Length));
                //***************************************************************
                //***************************************************************
                log.WriteEntry("Inizio recupero lista documenti Inps");

                string[] inps = null;
                try
                {
                    if (_partial)
                        inps = svc.GetListOfFileToDownloadFromServerSideAnalysis("Inps");
                    else
                        inps = svc.GetListOfFileToDownload("Inps");
                    
                }
                catch (Exception ex)
                {
                    log.WriteEntry(string.Format("Errore nella chiamata al servizio ricerca dei documenti inps: {0}; il servizio chiude!", ex.Message));
                    return;

                }

                log.WriteEntry(string.Format("Trovati {0} documenti inps", inps.Length));
                //***************************************************************
                //***************************************************************

                string type = "iqa";
                string filename = "";

                if (iqas.Length > 0)
                {
                    //scarico il primo elemento e avvio il processo

                    //verifico prima se ci sia n errore
                    if (iqas[0].StartsWith("ERRORE"))
                    {
                        log.WriteEntry(string.Format("Errore interno al servizio di ricerca dei documenti IQA: {0}", iqas[0]));
                        return;
                    }


                    log.WriteEntry("Inizio recupero del primo documento IQA: " + iqas[0]);
                    //a questo punto posso recuperare il documento
                    //ma prima recupero il nome del file
                    string fileToImport = DownloadFile(svc, iqas[0], log);

                    if (string.IsNullOrEmpty(fileToImport))
                    {
                        log.WriteEntry("File da importare nullo. Il precesso di chiude!");
                        return;
                    }

                    log.WriteEntry(string.Format("Download documento IQA completato: {0}.", fileToImport));


                    filename = iqas[0];

                    // non mi rimane che chiamare l'importer

                    log.WriteEntry("Preparazione all'avvio dell'importer");
                    CallImporter(fileToImport, type, log);

                    return;
                }

                log.WriteEntry("Nessun documento IQA trovato!");

                //***************************************************************
                //***************************************************************


                if (lib.Length > 0) //altrimenti testo se ci sono file di dilberi
                {
                    //verifico prima se ci sia n errore
                    if (lib[0].StartsWith("ERRORE"))
                    {
                        log.WriteEntry(string.Format("Errore interno al servizio di ricerca dei documenti Liberi: {0}", lib[0]));
                        return;
                    }


                    log.WriteEntry("Inizio recupero del primo documento Liberi: " + lib[0]);
                    //a questo punto posso recuperare il documento
                    //ma prima recupero il nome del file
                    string fileToImport = DownloadFile(svc, lib[0], log);

                    if (string.IsNullOrEmpty(fileToImport))
                    {
                        log.WriteEntry("File da importare nullo. Il precesso di chiude!");
                        return;
                    }



                    log.WriteEntry(string.Format("Download documento Liberi completato: {0}.", fileToImport));

                    type = "liberi";
                    filename = lib[0];

                    log.WriteEntry("Preparazione all'avvio dell'importer");
                    CallImporter(fileToImport, type, log);

                    return;
                }

                log.WriteEntry("Nessun documento Liberi trovato!");

            //***************************************************************
            //***************************************************************
                if (inps.Length > 0) //altrimenti testo se ci sono file di inps
                {
                    //verifico prima se ci sia n errore
                    if (inps[0].StartsWith("ERRORE"))
                    {
                        log.WriteEntry(string.Format("Errore interno al servizio di ricerca dei documenti Inps: {0}", inps[0]));
                        return;
                    }


                    log.WriteEntry("Inizio recupero del primo documento inps: " + inps[0]);
                    //a questo punto posso recuperare il documento
                    //ma prima recupero il nome del file
                    string fileToImport = DownloadFile(svc, inps[0], log);

                    if (string.IsNullOrEmpty(fileToImport))
                    {
                        log.WriteEntry("File da importare nullo. Il precesso di chiude!");
                        return;
                    }



                    log.WriteEntry(string.Format("Download documento inps completato: {0}.", fileToImport));

                    type = "inps";
                    filename = inps[0];

                    log.WriteEntry("Preparazione all'avvio dell'importer");
                    CallImporter(fileToImport, type, log);

                    return;
                }

                log.WriteEntry("Nessun documento Liberi trovato!");

            //NotifyError("service is on");
        }

        private void CallImporter(string filename, string type, EventLog log)
        {
           
            string parameters = ConstructParameters(filename, type);
            log.WriteEntry("Parametri recuperati: " + parameters);

            string importer = GetImporterexePath();
            log.WriteEntry("Percorso importer recuperato: " + importer);
            if(File.Exists(importer))
                log.WriteEntry("Percorso importer esistente");
            else
                log.WriteEntry("Percorso importer non  esistente");



            ProcessStartInfo p = new ProcessStartInfo();
            p.Arguments = parameters;
            p.FileName = importer;

            log.WriteEntry("Avvio importer!");

            try
            {
                System.Diagnostics.Process.Start(p); 
            }
            catch (Exception ex)
            {
                log.WriteEntry("Errore nell'avvio dell'importer: " + ex.Message );
               
            }
           
        }

        private string GetImporterexePath()
        {
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

            //ottengo la cartella in cui sto eseguendo il servizio
            path = Path.GetDirectoryName(path);

            //recupero il file in cui sono scritte le credenziali del servizio
            return path + "\\Importer\\WIN.FENGEST.IMPORT.GUI.exe";


            
        }


        private string ConstructParameters(string filename, string type)
        {

            return string.Format("x x {0} {1}", type, "\"" +  filename + "\""  );
        }

        private string DownloadFile(FenealgestServices.FenealgestwebServices svc, string fileName, EventLog log)
        {
            try
            {
                string sFile = GetPathForFileToDownload(fileName, log);


                MemoryStream objstreaminput = new MemoryStream();
                FileStream objfilestream = new FileStream(sFile, FileMode.Create, FileAccess.ReadWrite);
                int length = 0;

                if (_partial)
                 length = svc.GetDocumentLenFromServerSideAnalysis(fileName);
                else
                    length = svc.GetDocumentLen(fileName);


                Byte[] mybytearray = new Byte[length];

                if (_partial)
                    mybytearray = svc.GetDocumentFromServerSideAnalysis(fileName);
                else
                    mybytearray = svc.GetDocument(fileName);



                objfilestream.Write(mybytearray, 0, length);
                objfilestream.Close();

                //una volta ottenuto il file lo rinomino sul server cosi non comparirà piu'

                string error = "";
                if (_partial)
                    svc.RenameImportExportFileFomServerSideAnalysis(fileName);
                else
                    svc.RenameImportExportFile(fileName);


                return sFile;
            }
            catch (Exception ex)
            {

                log.WriteEntry("Errore durante il download del documento: " + ex.Message );
                return "";
            }
            
        }

        private static string GetPathForFileToDownload(string fileName, EventLog log)
        {

            try
            {

                //per prima cosa recupero il percorso del file che sto eseguendo per recuperare il percorso della selffolder
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                //ottengo la cartella in cui sto eseguendo il servizio
                path = Path.GetDirectoryName(path);


                //verifico se esiste la directory downloads
                if (Directory.Exists(path + "\\downloads"))
                    log.WriteEntry("Directory downloads esistente");
                else
                {
                    log.WriteEntry("Directory downloads non esistente");
                    log.WriteEntry("Tentativo di creazione cartella di download");
                    //tento di crearla
                    try
                    {
                        Directory.CreateDirectory(path + "\\downloads");
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry("Errore nella creazione cartella di download: " + ex.Message);
                        throw;
                    }
                   

                    log.WriteEntry("Creazione cartella di download riuscita");
                }

                //tento di inserire un file di prova per verificare se l'inserimento va a buon fine
                log.WriteEntry("Tentativo di scrivere un file di test");
                string testFile = path + "\\downloads\\p.txt";
                try
                {
                    File.AppendAllText(testFile, "prova");
                    File.Delete(testFile);
                }
                catch (Exception ex)
                {
                    log.WriteEntry("Errore nella creazione del file di test: " + ex.Message);
                    throw;
                }
               
                log.WriteEntry("Tentativo di scrivere un file di test riuscito");


                return path + "\\downloads\\" + fileName ;

            }
            catch (Exception ex)
            {
                log.WriteEntry("Si è verificato un errore nel tentativo di gestire i file scaricati nella selffolder: " + ex.Message, EventLogEntryType.Warning );
                return Path.Combine(Path.GetTempPath(), fileName);
            }


            
        }

        public void NotifyError(string message)
        {
          
                string filename = "SeriviceProcessor.txt";

                filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "//" + filename;

                File.AppendAllText(filename, message + Environment.NewLine);
   
        }

        private FenealgestServices.Credenziali CreateCredentials()
        {
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///","");

            //ottengo la cartella in cui sto eseguendo il servizio
            path = Path.GetDirectoryName(path);

            //recupero il file in cui sono scritte le credenziali del servizio
            string credentialFile = Path.Combine(path, "credentials.txt");

            if (!File.Exists(credentialFile))
                return null;


            //a questo punto recupero le credenziali
            string[] credString = File.ReadAllText(credentialFile,Encoding.Default ).Split(new string[]{"#"},   StringSplitOptions.RemoveEmptyEntries);

            if (credString.Length == 3)
            {
                FenealgestServices.Credenziali  cred = new FenealgestServices.Credenziali();
                cred.UserName = credString[0];
                cred.Password = credString[1];
                cred.Province  = credString[2];
                return cred;
            }
            else
                return null;





        }


        
    }
}
