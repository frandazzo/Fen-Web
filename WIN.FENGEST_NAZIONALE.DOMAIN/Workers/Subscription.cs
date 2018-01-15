using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class Subscription : AbstractPersistenceObject
    {
        private string _sector = "";
        private string _fiscalCode = "";
        private string _entity = "";
        private string _employCompany = "";
        private double _quota = 0;
        private string _level = "";
        private Provincia _province = new ProvinciaNulla();
        private SubscriptionPeriod _period;
        private Worker _worker;
        private Export _export;
        private Regione _regione = new RegioneNulla();
        private string _contract = "";
        private string _struttura = "";
        private string _area = "";

        public string Area
        {
            get { return _area ; }
            set { _area  = value; }
        }
        public string Struttura
        {
            get { return _struttura; }
            set { _struttura = value; }
        }

        private DenormalizedData _denormalizedData = new DenormalizedData();

        public DenormalizedData DenormalizedData
        {
            get { return _denormalizedData; }
            set { _denormalizedData = value; }
        }

        public string Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }

        public string FiscalCode
        {
            get { return _fiscalCode; }
            set { _fiscalCode = value; }
        }

        public DateTime InitialDate
        {
            get
            {
                if (_period != null)
                    return _period.InitialDate;
                return new DateTime(1900, 1, 1);
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (_period != null)
                    return _period.EndDate;
                return new DateTime(1900, 1, 1);
            }
        }

        public int PeriodNumber
        {
            get 
            {
                if (_period == null)
                    return 0;
                return _period.PeriodNumber; 
            }
        }

        public int PeriodYear
        {
            get 
            {
                if (_period == null)
                    return 1900;
                return _period.Year; 
            }
        }

        public PeriodType PeriodType
        {
            get 
            {
                if (_period == null)
                    return PeriodType.Semester;
                return _period.PeriodType; 
            }

        }


        public Regione Regione
        {
            get { return _regione; }
            set { _regione = value; }
        }


        public Export ParentExport
        {
            get { return _export; }
            set { _export = value; }
        }



        public Worker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }


        public SubscriptionPeriod Period
        {
            get { return _period; }
            set { _period = value; }
        }
   
        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }


        
      
        public string Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

      
        public string EmployCompany
        {
            get { return _employCompany; }
            set { _employCompany = value; }
        }




        public double Quota
        {
            get { return _quota; }
            set { _quota = value; }
        }

        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }


        
 
        public Provincia Province
        {
            get { return _province; }
            set { _province = value; }
        }



        protected override void DoValidation()
        {
            //DataRange r = new DataRange (_semeter.InitialDate ,_semeter .EndDate );
            //if (r.IsEmpty())
            //    ValidationErrors.Add("Periodo iscrizione non specificato.");

            if (string.IsNullOrEmpty(_struttura))
                ValidationErrors.Add("Struttura nulla per l'iscrizione");

            if (string.IsNullOrEmpty(_area))
                ValidationErrors.Add("Area nulla per l'iscrizione");

            if (_province == null)
                ValidationErrors.Add("Provincia di iscrizione non specificata.");

            if (_province != null)
                if (_province.Id == -1)
                    ValidationErrors.Add("Provincia di iscrizione non specificata.");

            if (_period  == null)
                ValidationErrors.Add("Periodo di iscrizione non specificato.");

            if (_period != null)
            {
                string error = _period.Validate();
                if (!string.IsNullOrEmpty(error))
                    ValidationErrors.Add(error);
            }

            if (_worker == null)
                ValidationErrors.Add("Utente non specificato per l'iscrizione.");


            if (string.IsNullOrEmpty(_sector))
                ValidationErrors.Add("Settore non specificato per l'iscrizione.");

        }

        public SubscriptionDTO ToSubscriptionDTO()
        {
            SubscriptionDTO dto = new SubscriptionDTO();

            dto.EmployCompany = this.EmployCompany;
            dto.Region = this.Regione.Descrizione;
            dto.FiscalCode = this.FiscalCode;
            dto.Province = this.Province.Descrizione;
            dto.EndDate  = this.Period.EndDate;
            dto.Entity  = this.Entity;
            dto.InitialDate  = this.Period.InitialDate;
            dto.Level  = this.Level;
            dto.PeriodType  = this.PeriodType;
            dto.Quota  = this.Quota;
            dto.Sector = this.Sector;
            dto.Semester  = this.Period.PeriodNumber;
            dto.Year = this.Period.Year;
            dto.Struttura = this.Struttura;
            dto.Area = this.Area;

            return dto;
        }
    }
}
