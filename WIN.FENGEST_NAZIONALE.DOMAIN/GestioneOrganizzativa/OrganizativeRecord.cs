using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.GestioneOrganizzativa
{
    public class OrganizativeRecord : AbstractPersistenceObject
    {

        private IGeoElementChecker _checker;
        public OrganizativeRecord(IGeoElementChecker checker)
        {
            _checker = checker;
        }
        public OrganizativeRecord()
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
        public int TotalWorkers { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalSindacalizedWorkers { get; set; }

        public int FilcaWorkers { get; set; }
        public int FilleaWorkers { get; set; }
        public int FenealWorkers { get; set; }

        private double _sindacalizationPercentage;
        private double _filleaPercentage;
        private double _filcaPercentage;
        private double _fenealPercentage;

        

        public double SindacalizationPercentage
        {
            get 
            {
                if (TotalWorkers > 0 && TotalSindacalizedWorkers > 0)
                {
                    double d = ((double)TotalSindacalizedWorkers  / TotalWorkers)* 100;
                    return Math.Round((Double)d, 2);
                }

                return _sindacalizationPercentage;
            }
            set { _sindacalizationPercentage = value; }
        }


        public double FilleaPercentage
        {
            get 
            {
                if (FilleaWorkers > 0 && TotalSindacalizedWorkers > 0)
                {
                    double d = ((double)FilleaWorkers / TotalSindacalizedWorkers) * 100;
                    return Math.Round((Double)d, 2);
                }
                return _filleaPercentage; 
            }
            set { _filleaPercentage = value; }
        }


        public double FilcaPercentage
        {
            get 
            {
                if (FilcaWorkers > 0 && TotalSindacalizedWorkers > 0)
                {
                    double d = ((double)FilcaWorkers / TotalSindacalizedWorkers) * 100;
                    return Math.Round((Double)d, 2);
                }
                return _filcaPercentage; 
            }
            set { _filcaPercentage = value; }
        }



        public double FenealPercentage
        {
            get 
            {
                if (FenealWorkers > 0 && TotalSindacalizedWorkers > 0)
                {
                    double d = ((double)FenealWorkers / TotalSindacalizedWorkers) * 100;
                    return Math.Round((Double)d, 2);
                }
                return _fenealPercentage; 
            }
            set { _fenealPercentage = value; }
        }

        protected override void DoValidation()
        {
            if (!EntityValidator.ExistEntity(Bilateral))
                ValidationErrors.Add("Il campo ENTE deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
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
                ValidationErrors.Add("La regione " + Region + " non esiste.");


            if (!string.IsNullOrEmpty(Province))
            {
                if (!_checker.ExistProvince(Province))
                    ValidationErrors.Add("La provincia " + Province + " non esiste.");
            }


        }

        


        

    }
}
