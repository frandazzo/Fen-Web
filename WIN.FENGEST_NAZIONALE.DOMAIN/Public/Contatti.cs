using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using System.Text.RegularExpressions;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Public
{
    public class Contatti : AbstractPersistenceObject
    {
        private string _mail = "";

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

        protected override void DoValidation()
        {
            //Mail regular exp pattern:\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
            string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            if (string.IsNullOrEmpty(_mail))
                ValidationErrors.Add("Mail non inserita");

            if (!string.IsNullOrEmpty(_mail))
                if (!Regex.IsMatch(_mail, MatchEmailPattern))
                    ValidationErrors.Add("Mail non valida");
        }

    }
}
