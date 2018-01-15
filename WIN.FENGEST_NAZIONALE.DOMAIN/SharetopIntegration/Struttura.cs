using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class Struttura : SharetopIntegragrationSuperClass
    {

        public string Denominazione { get; set; }
        public string Responsabile { get; set; }
        public string Fiscale { get; set; }
        public string Altri { get; set; }
        
        public string Indirizzo { get; set; }
        public string RecapitiTelefonici { get; set; }
        public string Mail { get; set; }
        public string SitoInternet { get; set; }

        public CoordinataBancaria[] CoordinateBancarie { get; set; }


    }
}
