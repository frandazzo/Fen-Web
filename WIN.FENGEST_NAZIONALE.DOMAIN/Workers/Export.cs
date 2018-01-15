using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class Export : AbstractPersistenceObject
    {

        private DateTime _exportDate = DateTime.Now;

        private Provincia _province = new ProvinciaNulla();
        private Regione _regione = new RegioneNulla();
        private string _exporterName = "";
        private string _structure = "";
        private string _area = "";
        //private string _exporterMail = "";
        private ExprtType _exportType = ExprtType.SimpleExport;
        private IList<Worker> _workers = new List<Worker>();
        private IList<Libero> _notSubscibers = new List<Libero>();
        //private PeriodType _periodType = PeriodType.Semester;
        private SubscriptionPeriod _period;
        private bool _transacted = false;

        private int _exportNumber = 0;


        public int ExportNumber
        {
            get { return _exportNumber; }
            set { _exportNumber = value; }
        }


        public bool Transacted
        {
            get { return _transacted; }
            set { _transacted = value; }
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

        private string _mail = "";
        private string _sector = "EDILE";

        public string Settore
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string Structure
        {
            get { return _structure; }
            set { _structure = value; }
        }

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
  

        public string ExporterMail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        protected override void DoValidation()
        {
            if (_province == null)
                ValidationErrors.Add("Provincia nulla per la traccia di importazione");

            if (string.IsNullOrEmpty (_structure ))
                ValidationErrors.Add("Struttura nulla per la traccia di importazione");

            if (string.IsNullOrEmpty(_area))
                ValidationErrors.Add("Area nulla per la traccia di importazione");

            if (string.IsNullOrEmpty (_sector ))
                ValidationErrors.Add("SettoreNullo");

            if (_province != null)
                if (_province.Id == -1)
                    ValidationErrors.Add("Provincia nulla per la traccia di importazione");

            if ((_exportDate== DateTime.MaxValue) || (_exportDate == DateTime.MinValue))
                ValidationErrors.Add("Data documento non definita");

            if (string.IsNullOrEmpty(_exporterName))
                ValidationErrors.Add("Responsabile dell'esportazione non specificato");

            if (_period == null)
                ValidationErrors.Add("Periodo di esportazione non specificato.");

            if (_period != null)
            {
                string error = _period.Validate();
                if (!string.IsNullOrEmpty(error))
                    ValidationErrors.Add(error);
            }



            if (_workers != null)
            {
                foreach (Worker  item in _workers )
                {
                    if (!item.IsValid())
                        AddError(item);
                }
            }

            if (_notSubscibers != null)
            {
                foreach (Libero item in _notSubscibers)
                {
                    if (!item.IsValid())
                        AddError(item);
                }
            }
            

        }

        private void AddError(AbstractPersistenceObject item)
        {
            string errors = "";
            foreach (string err in item.ValidationErrors)
            {
                errors += err + Environment.NewLine;
            }
            ValidationErrors.Add(errors);
        }

        public Export(){}


        public SubscriptionPeriod Period
        {
            get { return _period; }
            set { _period = value; }
        }


        public DateTime ExportDate
        {
            get { return _exportDate; }
            set { _exportDate = value; }
        }




        public Provincia Province
        {
            get { return _province; }
            set { _province = value; }
        }



        public Regione Regione
        {
            get { return _regione; }
            set { _regione = value; }
        }

  


        public string ExporterName
        {
            get { return _exporterName; }
            set { _exporterName = value; }
        }



        public ExprtType ExportType
        {
            get { return _exportType; }
            set { _exportType = value; }
        }



        public IList<Libero> NotSubscribers
        {
            get { return _notSubscibers; }
            set { _notSubscibers = value; }
        }

   
      
        public IList<Worker> Workers
        {
            get { return _workers; }
            set { _workers = value; }
        }




        public PeriodType PeriodType
        {
            get { return _period.PeriodType; }

        }


   

      

     
       

    }

}

