using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR.Commands
{
    class BackgroundSearchWorkersCommand : ICommand
    {
        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                Log("Inizio esecuzione comando di ricerca codici fiscali su WEB_CONNECTOR*******************");

                string[] fiscalCodes = ((List<string>)ProgramParameters.Instance.FiscalCodeList).ToArray();
                string mailto = ProgramParameters.Instance.Mailto;
                string subject = ProgramParameters.Instance.MailSubject;

                Log(string.Format("Sono stati trovati {0} codici fiscali", fiscalCodes.Length));

                //a questo punto posso richiamare il servizio per la ricerca dei dati 


                //instanzio i servizi
                FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province;
                //gli imposto l'intestazione
                w.CredenzialiValue = c;

                //invio dati al server
                Log("Invio dati al server per richiesta info lavoratori ed invio mail");
                string error = w.SendMailForLiberiSearch(fiscalCodes,mailto,subject);

                if (!string.IsNullOrEmpty(error))
                {
                    Log("Si è verificato un errore durante l'ivio dei dati al server: " + error);
                    Log("Termine comando di ricerca codici fiscali con errori");
                    return;
                }


                Log("Termine comando di ricerca codici fiscali");

            }
            catch (Exception ex)
            {
                Log("Si è verificato un errore nella esecuzione del comando: " + ex.Message);

                if (ex.InnerException != null)
                    Log("InnerEx: " + ex.InnerException.Message);
            }
        }

        private void Log(string message)
        {
            if (File.Exists(ProgramParameters.Instance.LogFile))
            {
                File.AppendAllText(ProgramParameters.Instance.LogFile, message + Environment.NewLine);
            }
        }
    }

}
        #endregion