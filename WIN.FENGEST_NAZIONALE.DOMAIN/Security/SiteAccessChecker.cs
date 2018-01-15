using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.Login;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class SiteAccessChecker: IAccessChecker
    {
        private IUserLocker _locker;

        public SiteAccessChecker(IUserLocker locker)
        {
            if (locker == null)
                throw new ArgumentException ("Locker utente mancante!");
            _locker = locker ;
        }




        public LoginResult CheckAccess(LoginAction action)
        {
            LoginResult result = null;
            //Verifico se l'utente è bloccato
            if (action.User.Locked)
            {
                result = new LoginResult(false, "Utente bloccato. Contattare l'amministratore per lo sblocco dell'account", -1, LoginActionResult.UserLocked );
                return result;
            }

            //Verifico se il numero di tentativi else terminato
            if ((action.TryNumber > 4) && (!action.User.Password.Equals(action.LoginPassword)))
            {
                _locker.LockUser(action.User);
                result = new LoginResult(false, "Nessun tentativo disponibile. Utente bloccato. Contattare l'amministratore per lo sblocco dell'account", -1,  LoginActionResult.UserLocked );
                return result;
            }

          

            //Verifico la password
            //Se nn è uguale devo incrementare il numero di tentativi eseguiti
            if (!action.User.Password.Equals(action.LoginPassword))
            {
                action.IncrementTryNumber();
                string message = "Password non corretta! Reinserisci la password.";
                int remainingtry = 5 - action.TryNumber; 
                if (remainingtry == 0)
                    message = "Nessun tentativo disponibile. Un nuovo errore bloccherà l'utente";
                result = new LoginResult(false, message, (5 - action.TryNumber),  LoginActionResult.WrongUserOrPassword );
                return result;
            }
            //Verifico la password
            //Se è uguale devo verificare la scadenza della password
            else
            {
                //se la password è scaduta: data odierna minore di datas scadenzza pwd
                if (DateTime.Now.Date.CompareTo(action.User.PasswordDecay) > -1)
                {
                    result = new LoginResult(false, "Password scaduta. Rinnova la password!", -1,  LoginActionResult.DecayedPassword );
                    return result;
                } 
                else
                {
                        result = new LoginResult(true, "Benvenuto " + action.User .CompleteName + "!", -1,  LoginActionResult.AccessOk );
                        return result;
                }
            }
           
        }

    }
}
