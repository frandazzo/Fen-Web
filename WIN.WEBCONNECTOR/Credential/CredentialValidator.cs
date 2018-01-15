using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR
{
    public class CredentialValidator
    {
        public static bool AreCredentialValid(string userName, string password, string province)
         {


             //verifico per prima cosa la connessione a internet
             if (Properties.Settings.Default.CheckInternetConnection)
             {
                 if (!InternetConnectionChecker.IsConnectedToInternet())
                 {
                     throw new Exception("Connessione ad internet non disponibile!");
                 }
             }
             //qui devo verificare le credenziali.
             //se non vengono verificate la maschera non può chiudersi con un ok;


             //creo l'intestazione del webService
             FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
             c.UserName = userName ;
             c.Password = password;
             c.Province = province;

             //creo il servizio
             FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();


             //gli imposto l'intestazione
             service.CredenzialiValue = c;


             //lo richiamo per verificare l'identità dell'utente
             //try
             //{
                 if (service.UserIsValid())
                 {
                     //se l'identità verificata la memorizzo
                     CredentialDB.Instance.UserName = userName;
                     CredentialDB.Instance.Password = password;
                     CredentialDB.Instance.Province = province;
                     
                     return true;
                     //this.DialogResult = DialogResult.OK;
                     //this.Close();
                 }
                 else
                 {
                     return false;
                     //this.DialogResult = DialogResult.Cancel;
                 }
             //}
             //catch (Exception ex)
             //{
             //    //this.DialogResult = DialogResult.Cancel;
             //    //MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
             //    //return;
             //    throw;
             //}
         }

        public static bool AreRegionalCredentialValid(string userName, string password, string region)
        {


            //verifico per prima cosa la connessione a internet
            if (Properties.Settings.Default.CheckInternetConnection)
            {
                if (!InternetConnectionChecker.IsConnectedToInternet())
                {
                    throw new Exception("Connessione ad internet non disponibile!");
                }
            }
            //qui devo verificare le credenziali.
            //se non vengono verificate la maschera non può chiudersi con un ok;


            //creo l'intestazione del webService
            FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
            c.UserName = userName;
            c.Password = password;
            c.Province = region;

            //creo il servizio
            FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();


            //gli imposto l'intestazione
            service.CredenzialiValue = c;


            //lo richiamo per verificare l'identità dell'utente
            //try
            //{
            if (service.UserIsValid())
            {
                //se l'identità verificata la memorizzo
                CredentialDB.Instance.UserName = userName;
                CredentialDB.Instance.Password = password;
                CredentialDB.Instance.Province = region;

                return true;
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            else
            {
                return false;
                //this.DialogResult = DialogResult.Cancel;
            }
            //}
            //catch (Exception ex)
            //{
            //    //this.DialogResult = DialogResult.Cancel;
            //    //MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //return;
            //    throw;
            //}
        }
    }
}
