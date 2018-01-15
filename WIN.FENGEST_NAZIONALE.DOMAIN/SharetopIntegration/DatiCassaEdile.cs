using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class DatiCassaEdile: SharetopIntegragrationSuperClass
    {

        public int Addetti { get; set; }
        public int Imprese { get; set; }
        public double MSDenunciato { get; set; }
        public double MSVersato { get; set; }
        public int IscrittiOOSS { get; set; }
        public double PercSindacalizzati { get; set; }
        public int DelegheFeneal { get; set; }
        public double PercFeneal { get; set; }
        public int DelegheFilca { get; set; }
        public double PercFilca { get; set; }
        public int DelegheFillea { get; set; }
        public double PercFillea { get; set; }
        public double ImportoQACP { get; set; }
        public double ImportoQACR { get; set; }
        public double ImportoQACN { get; set; }
        public double ImportoDelegheFeneal { get; set; }

    }
}
