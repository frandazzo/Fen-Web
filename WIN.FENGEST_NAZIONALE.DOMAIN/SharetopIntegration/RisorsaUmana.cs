using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class RisorsaUmana : SharetopIntegragrationSuperClass
    {
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string TipoRapporto { get; set; }
        public string Ruolo { get; set; }
        public string Indirizzo { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Note { get; set; }

    }
}
