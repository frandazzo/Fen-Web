using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FenealgestWEB.WebServices.DTOWrappers.Utils
{

    public class FiscalData
    {
        public string Provincia { get; set; }
        public string Comune { get; set; }
        public string Nazione { get; set; }
        public DateTime DataNascita { get; set; }
        public string Sesso { get; set; }

    }
}