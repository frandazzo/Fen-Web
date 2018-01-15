using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class Rappresentanza : SharetopIntegragrationSuperClass
    {

        public string Azienda { get; set; }
        public string CodInpsAzienda { get; set; }
        public string ProvinciaAzienda { get; set; }
        public string ComuneAzienda { get; set; }
        public string Contratto { get; set; }

        public int NumDipendenti { get; set; }
        public int NumIscrittiFeneal { get; set; }
        public int VotiListaFeneal { get; set; }

        public string TipoNomina { get; set; }
        public DateTime  DataElezione { get; set; }
        public int NumRappresentantiFeneal { get; set; }
        public string  UrlVerbale { get; set; }
    }
}
