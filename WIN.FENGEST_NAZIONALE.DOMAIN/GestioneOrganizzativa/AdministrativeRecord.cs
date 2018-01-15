using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.GestioneOrganizzativa
{
    public class AdministrativeRecord : AbstractPersistenceObject
    {
        private IGeoElementChecker _checker;
        public AdministrativeRecord(IGeoElementChecker checker)
        {
            _checker = checker;
        }
        public AdministrativeRecord()
        {
           
        }

        public void SetGeoChecker(IGeoElementChecker checker)
        {
            _checker = checker;
        }


        public int Year { get; set; }
        public string Bilateral { get; set; }
        public string SpecificBilateral { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }

        public int Workers { get; set; }
        public int Companies { get; set; }

        public double DeclaredSalary { get; set; }
        public double GivenSalary { get; set; }
        public double QACP { get; set; }
        public double QACN { get; set; }
        public double QACR { get; set; }
        public double DelegheAmount { get; set; }
        public double Pending { get; set; }



        protected override void DoValidation()
        {
            if (!EntityValidator.ExistEntity(Bilateral))
                ValidationErrors.Add("Il campo ENTE  deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
                                    " CALEC, CEA, CEAV, CEC, CEDA, CEDAF, CEDAM, CELCOF, CEMA, CERT, CEVA, CEDAIIER, FALEA, COOP");

            if (!EntityValidator.ExistEntity(SpecificBilateral))
                ValidationErrors.Add("Il campo ENTE Bilaterale deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
                                    " CALEC, CEA, CEAV, CEC, CEDA, CEDAF, CEDAM, CELCOF, CEMA, CERT, CEVA, CEDAIIER, FALEA");

            if ((Year < 1980) || (Year > 2040))
                ValidationErrors.Add("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


            if (String.IsNullOrEmpty(Bilateral) || String.IsNullOrEmpty(SpecificBilateral))
                ValidationErrors.Add("I campi Ente e ente secondario devono essere entrambi valorizzati");





            if (string.IsNullOrEmpty(Region))
                ValidationErrors.Add("La regione non è stata impostata");
            if (!_checker.ExistRegion(Region))
                ValidationErrors.Add("La regione " + Region  + "  non esiste.");


            if (!string.IsNullOrEmpty(Province))
            {
                if (!_checker.ExistProvince(Province))
                    ValidationErrors.Add("La provincia " + Province + " non esiste.");
            }


        }
    }
}
