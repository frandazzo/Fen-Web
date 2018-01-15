using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class Rappresentante : SharetopIntegragrationSuperClass
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string CodiceFiscale { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }


        public string Azienda { get; set; }
        public string CodInpsAzienda { get; set; }
        public string ProvinciaAzienda { get; set; }
        public string ComuneAzienda { get; set; }
        public string Contratto { get; set; }
        public string Tipo { get; set; }
    }
}
