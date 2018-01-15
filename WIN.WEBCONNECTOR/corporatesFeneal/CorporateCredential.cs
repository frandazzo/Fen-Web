using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIN.WEBCONNECTOR.corporatesFeneal
{
    public class CorporateCredential
    {
        private List<Credential> _credentials = new List<Credential>();
        public List<Credential> Credentials
        {
            get { return _credentials; }
            set { _credentials = value; }
        }

        public string Username { get; set; }
        public string Password { get; set; }
 
    }
}
