using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.Credential
{
    public class CredentialDB
    {
        private CredentialDB()
        {
        }

        private static CredentialDB _instance;

        public static CredentialDB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CredentialDB();
                return _instance;
            }
        }


        public string UserName { get; set; }
        public string Password { get; set; }
        public string Province { get; set; }
    }
}
