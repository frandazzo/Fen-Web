using System;
using System.Collections.Generic;

using System.Text;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;
using System.Text.RegularExpressions;
using WIN.TECHNICAL.SECURITY_NEW.PasswordManagement;


namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class Utente : WIN.BASEREUSE.AbstractPersona, IUserNew
    {

        private string _mail = "";
        private string _userName = "";
        private string _password = "";
        private bool _loked;
        private DateTime _passwordData = DateTime.Now.Date.AddMonths(-3);
        private DateTime _passwordDecay = DateTime.Now.Date;
        private IList<Role> _roles = new List<Role>();
        private Appartenenza _appartenenza = new Appartenenza();



        public Appartenenza Appartenenza
        {
            get { return _appartenenza; }
            set { _appartenenza = value; }
        }


        public StrutturaApparteneza TipoStruttura
        {
            get { return _appartenenza.Struttura; }
        }



        //private bool _nationalUser = true;

        //public bool NationalUser
        //{
        //    get
        //    {
        //        return _nationalUser;
        //    }
        //    set
        //    {
        //        _nationalUser = value;
        //    }
        //}



        public bool IsNationalUser
        {
            get
            {
                if (_appartenenza.Struttura == StrutturaApparteneza.Feneal_Nazionale)
                    return true;
                return false;
            }
        }


        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        //public new string CompleteName
        //{
        //    get { return  base.CompleteName; }
        //}

        public bool Locked
        {
            get
            {
                return _loked;
            }
            set
            {
                _loked = value;
            }
        }

        public DateTime PasswordData
        {
            get
            {
                return _passwordData;
            }
            set
            {
                _passwordData = value;
                DecayDataCalculator c = new DecayDataCalculator();
                _passwordDecay = c.CalculateDecayDate(value);
            }
        }

        public DateTime PasswordDecay
        {
            get
            {
                return _passwordDecay;
            }
        }



        

        public IList<Role> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }

        

        public bool IsEnabled(string profileName)
        {
            if (_roles == null)
                return false;

            foreach (Role item in _roles)
            {
                if (item.IsEnabled(profileName))
                    return true;
            }
            return false;
        }


        protected override void DoValidation()
        {

            //Mail regular exp pattern:\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
            string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


            if (string.IsNullOrEmpty(m_Cognome))
                ValidationErrors.Add("Cognome non valido");


            if (string.IsNullOrEmpty(m_Nome))
                ValidationErrors.Add("Nome non valido");


            if (string.IsNullOrEmpty(_userName ))
                ValidationErrors.Add("UserName non valido");

            if (!string.IsNullOrEmpty(_userName))
                if (_userName.Contains(" "))
                    ValidationErrors.Add("Il nome utente non può contenere spazi vuoti");

            if (!string.IsNullOrEmpty(_userName))
                if (_userName.Length > 30 || _userName.Length<6)
                    ValidationErrors.Add("UserName non può eccedere 30 caratteri e non può essere inferiore a 6 caratteri");

            if (!string.IsNullOrEmpty(_password ))
                if (_password.Length > 30 || _password .Length < 6)
                    ValidationErrors.Add("La password non può eccedere 30 caratteri e non può essere inferiore a 6 caratteri");


            if (!string.IsNullOrEmpty(_password))
                if(_password.Contains(" "))
                    ValidationErrors.Add("La password non può contenere spazi vuoti");


            if (string.IsNullOrEmpty(_password))
                ValidationErrors.Add("Password non inserita");

            if (string.IsNullOrEmpty(_mail))
                ValidationErrors.Add("Mail non inserita");

            if (!string.IsNullOrEmpty(_mail))
                if(!Regex.IsMatch (_mail ,MatchEmailPattern))
                    ValidationErrors.Add("Mail non valida");


            if (_passwordDecay == DateTime.MaxValue ||_passwordDecay == DateTime.MinValue)
                ValidationErrors.Add("Scadenza password non definita");


            if (_passwordData == DateTime.MaxValue || _passwordData == DateTime.MinValue)
                ValidationErrors.Add("Data inserimento password non definita");


            if (_passwordDecay.CompareTo (_passwordData ) < 1)
                ValidationErrors.Add("Data di scadenza password non valida");


            //if (_nationalUser == true && _appartenenza.Struttura != StrutturaApparteneza.Feneal_Nazionale)
            //    ValidationErrors.Add("Impossibile impostare una struttura regionale o provinciale per un utente nazionale");

            if (_appartenenza.Struttura == StrutturaApparteneza.Feneal_Regionale && _appartenenza.Regione == null)
                ValidationErrors.Add("Regione mancante per un utente di una struttura regionale");

            if (_appartenenza.Struttura == StrutturaApparteneza.Feneal_Provinciale)
                if (_appartenenza.Regione == null || _appartenenza.Provicnia == null)
                    ValidationErrors.Add("Regione o provincia mancante per un utente di una struttura provinciale");


        }




        public string ToRoleDescriptor()
        {
            string result = "";


            foreach (Role item in _roles)
            {
                result += item.Name + ";";
            }

            return result;
        }


        public void AddRole(Role role)
        {
            foreach (Role item in _roles )
	        {
                if (item.Name.ToLower().Equals(role.Name.ToLower()))
                    return;
	        }
            _roles .Add(role);
        }



        #region IUserNew Membri di


        public void RemoveRole(Role role)
        {
            Role temp = null;
            foreach (Role item in _roles)
            {
                if (item.Name.ToLower().Equals(role.Name.ToLower()))
                {
                    temp = item;
                    break;
                }
            }
            if (temp != null)
                _roles.Remove(temp);
        }

        #endregion

        #region IUserNew Membri di


        public IList<string> EnabledFunctionNames()
        {
            RoleDescriptor d = new RoleDescriptor(new List<Role>(_roles).ToArray ());
            return d.EnabledFunctionNames();
        }

        #endregion
    }
}
