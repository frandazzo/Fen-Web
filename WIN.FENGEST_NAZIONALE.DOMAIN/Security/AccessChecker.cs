using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.Login;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class AccessChecker : IAccessChecker
    {
        private IUserLocker _locker;

        public AccessChecker(IUserLocker locker)
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
            if ((action.TryNumber > 4) && (!CheckPassword(action.LoginPassword, action.User.Password)))
            {
                _locker.LockUser(action.User);
                result = new LoginResult(false, "Nessun tentativo disponibile. Utente bloccato. Contattare l'amministratore per lo sblocco dell'account", -1,  LoginActionResult.UserLocked );
                return result;
            }

          





            //Verifico la password
            //Se nn è uguale devo incrementare il numero di tentativi eseguiti
            if (!CheckPassword(action.LoginPassword,action.User.Password))
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
                        result = new LoginResult(true, "Benvenuto " + action.User.CompleteName + "!", -1,  LoginActionResult.AccessOk );
                        return result;
                }
            }
           
        }

        private bool CheckPassword(string password, string dbPassword)
        {
            if (String.IsNullOrEmpty (password))
                return false;
            if (String.IsNullOrEmpty(dbPassword))
                return false;

            if (dbPassword.Equals(password))
                return true;


            //per fare in modo che la password salvata sul db rimanga inalterata per i servizi web faro' in modo che le password 
            //abbiano i seguenti possibili suffissi o prefissi (o l'uno o l'altro)

            //possibili suffissi o prefissi
            //1_3_5
            //2_4_6
            //a_c_e
            //b_d_f
            string a = "1_3_5";
            string b = "2_4_6";
            string c = "a_c_e";
            string d = "b_d_f";


            if (password.StartsWith(c))
                if (password.Replace(c, "").Equals(dbPassword))
                    return true;
            if (password.EndsWith(c))
                if (password.Replace(c, "").Equals(dbPassword))
                    return true;

            if (password.StartsWith(d))
                if (password.Replace(d, "").Equals(dbPassword))
                    return true;
            if (password.EndsWith(d))
                if (password.Replace(d, "").Equals(dbPassword))
                    return true;


            if (password.StartsWith(a))
                if (password.Replace(a, "").Equals(dbPassword))
                    return true;
            if (password.EndsWith(a))
                if (password.Replace(a, "").Equals(dbPassword))
                    return true;

            if (password.StartsWith(b))
                if (password.Replace(b, "").Equals(dbPassword))
                    return true;
            if (password.EndsWith(b))
                if (password.Replace(b, "").Equals(dbPassword))
                    return true;


            return false;
        }

    }
}
