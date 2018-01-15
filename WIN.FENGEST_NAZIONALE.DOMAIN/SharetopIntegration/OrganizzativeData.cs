using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class OrganizzativeData
    {
        public Struttura Struttura { get; set; }
        public CongressoRegionale CongressoRegionale { get; set; }
        public CongressoTerritoriale CongressoTerritoriale { get; set; }
        public List<Rappresentante> Rappresentanti { get; set; }
        public List<Rappresentanza > Rappresentanze { get; set; }
        public List<RisorsaUmana> RisorseUmane { get; set; }
        public string FirstLevelError { get; set; }

    }
}
