using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class CongressoRegionale : SharetopIntegragrationSuperClass
    {
        public string SegretarioGenerale { get; set; }
        public string SegretarioOrganizzativo { get; set; }
        public string MembriSegretaria { get; set; }
        public string Tesoriere { get; set; }
        public string ConsiglioTerritoriale { get; set; }
        public string ConsiglioTerritorialeSupplente { get; set; }
        public string AssembleaTerritoriale { get; set; }
        public string AssembleaTerritorialeSupplente { get; set; }
        public string RevisoriDeiConti { get; set; }
        public string Probiviri { get; set; }
        public CongressPresence BuildCongressPresence(string name, string surname)
        {
            string completeName = string.Format("{0} {1}", name, surname);
            string inverseName = string.Format("{0} {1}", surname, name);

            CongressPresence presence = new CongressPresence();

            presence.Anno = BaseData.Anno;
            presence.Regione = BaseData.Regione.Descrizione;
            presence.Territorio = (BaseData.Provincia != null) ? BaseData.Provincia.Descrizione : "";
            presence.Tipo = "Regionale";


            if (Probiviri.ToLower().Contains(completeName.ToLower()) || Probiviri.ToLower().Contains(inverseName.ToLower()))
            {
                presence.Probiviri = true;
            }
            if (RevisoriDeiConti.ToLower().Contains(completeName.ToLower()) || RevisoriDeiConti.ToLower().Contains(inverseName.ToLower()))
            {
                presence.RevisoriDeiConti = true;
            }

            if (AssembleaTerritorialeSupplente.ToLower().Contains(completeName.ToLower()) || AssembleaTerritorialeSupplente.ToLower().Contains(inverseName.ToLower()))
            {
                presence.AssembleaTerritorialeSupplente = true;
            }

            if (AssembleaTerritoriale.ToLower().Contains(completeName.ToLower()) || AssembleaTerritoriale.ToLower().Contains(inverseName.ToLower()))
            {
                presence.AssembleaTerritoriale = true;
            }


            if (Tesoriere.ToLower().Contains(completeName.ToLower()) || Tesoriere.ToLower().Contains(inverseName.ToLower()))
            {
                presence.Tesoriere = true;
            }

            if (ConsiglioTerritoriale.ToLower().Contains(completeName.ToLower()) || ConsiglioTerritoriale.ToLower().Contains(inverseName.ToLower()))
            {
                presence.ConsiglioTerritoriale = true;
            }

            if (ConsiglioTerritorialeSupplente.ToLower().Contains(completeName.ToLower()) || ConsiglioTerritorialeSupplente.ToLower().Contains(inverseName.ToLower()))
            {
                presence.ConsiglioTerritorialeSupplente = true;
            }





            if (SegretarioGenerale.ToLower().Contains(completeName.ToLower()) || SegretarioGenerale.ToLower().Contains(inverseName.ToLower()))
            {
                presence.SegretarioGenerale = true;
            }

            if (SegretarioOrganizzativo.ToLower().Contains(completeName.ToLower()) || SegretarioOrganizzativo.ToLower().Contains(inverseName.ToLower()))
            {
                presence.SegretarioOrganizzativo = true;
            }

            if (MembriSegretaria.ToLower().Contains(completeName.ToLower()) || MembriSegretaria.ToLower().Contains(inverseName.ToLower()))
            {
                presence.MembriSegretaria = true;
            }

            return presence;
        }
    }
}
