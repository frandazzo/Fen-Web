using System;
using System.Collections.Generic;
using System.Web;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    public class MailData
    {
        public string[] tos { get; set; }
        public string body { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }

    }
}