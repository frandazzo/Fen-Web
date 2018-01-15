using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class CongressPresence 
    {

        public string Regione { get; set; }
        public string Territorio { get; set; }
        public int Anno { get; set; }
        public string Tipo { get; set; } 
        public bool SegretarioGenerale { get; set; }
        public bool SegretarioOrganizzativo { get; set; }
        public bool MembriSegretaria { get; set; }
        public bool Tesoriere { get; set; }
        public bool ConsiglioTerritoriale { get; set; }
        public bool ConsiglioTerritorialeSupplente { get; set; }
        public bool AssembleaTerritoriale { get; set; }
        public bool AssembleaTerritorialeSupplente { get; set; }
        public bool RevisoriDeiConti { get; set; }
        public bool Probiviri { get; set; }

   


        public bool isPresent()
        {
            if (SegretarioGenerale)
                return true;

            if (SegretarioOrganizzativo)
                return true;
            if (MembriSegretaria)
                return true;
            if (Tesoriere)
                return true;
            if (ConsiglioTerritoriale)
                return true;
            if (ConsiglioTerritorialeSupplente)
                return true;
            if (AssembleaTerritoriale)
                return true;
            if (AssembleaTerritorialeSupplente)
                return true;
            if (RevisoriDeiConti)
                return true;
            if (Probiviri)
                return true;
           

            return false;
        }
    }
}
