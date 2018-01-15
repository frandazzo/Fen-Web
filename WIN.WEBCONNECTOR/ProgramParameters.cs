using System;
using System.Collections.Generic;
using System.Text;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR
{
    internal class ProgramParameters
    {

        public IList<string> FiscalCodeList { get; set; }

        public string Responsable { get; set; }
        public string UserName
        {
            get {return CredentialDB.Instance.UserName ;}
            set { CredentialDB.Instance.UserName = value; } 
        }
        public string Password 
        {
            get { return CredentialDB.Instance.Password; }
            set { CredentialDB.Instance.Password = value; } 
        }
        public string Province 
        {
            get { return CredentialDB.Instance.Province; }
            set { CredentialDB.Instance.Province = value; } 
          
        }

        //Possibili valori "1", "2", "3", "4", "5" (Export, Import Lav, Import Lavs, Search lavs, ExportBackground)
        public string Command { get; set; }

        //Possibili valori "Program"
        public string ExportType { get; set; }

        //posibili valori "False"
        public string CheckCredential { get; set; }


        //posibili valori sono i percorsi di un file xml da esportare
        public string FileToExport { get; set; }

        //Codice fiscale da esportare
        public string FiscalCode { get; set; }

        //Ente per il settore edile
        public string Entity { get; set; }


        //Anno di riferimento per l'iscrizione
        public string Year { get; set; }

        //Periodo di riferimento per l'iscrizione
        public string Period { get; set; }

        //percorso per un eventuale file di log
        public string LogFile { get; set; }


        private ProgramParameters() 
        {
            MailSubject = "";
            Mailto = "";
            LogFile = "";
            Year = DateTime.Now.Year.ToString();
            Period = "1";
            Entity = "";
            UserName = "";
            Password = "";
            Province = "";
            Command = "";
            ExportType = "";
            CheckCredential = "";
            FileToExport = "";
            FiscalCode = "";
            Responsable = "";
            FiscalCodeList = new List<string>();
            Azienda = "";
            OpenedByHumanActor = false;
        }

        private static ProgramParameters _instance;

        public static ProgramParameters Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProgramParameters();
                return _instance;
            }
        }

        //indica che è un uomo ad aprire il programma e non un altro programma
        public bool OpenedByHumanActor { get; set; }


        //mai l a cui inviare il resoconto dell'operazione
        public string Mailto { get; set; }

        public string MailSubject { get; set; }

        //azienda per richiedere gli iscritti e i non iscritti per l'azienda
        public string Azienda { get; set; }
    }
}
