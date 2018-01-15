using System;
using System.Collections.Generic;
using System.Web;

namespace FenealgestWEB.WebServices
{
    public class Credenziali : System.Web.Services.Protocols.SoapHeader
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Province { get; set; }
    }
}
