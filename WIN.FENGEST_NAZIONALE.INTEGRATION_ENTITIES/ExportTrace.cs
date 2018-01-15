using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    [XmlRootAttribute("ExportTrace", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class ExportTrace
    {


        public ExportTrace() { }


        private DateTime _exportDate = DateTime.Now;


        [XmlAttribute("ExportDate")]
        public DateTime ExportDate
        {
            get { return _exportDate; }
            set { _exportDate = value; }
        }


        private string _province = "";

        [XmlAttribute("Province")]
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }


       


        private string _exporterMail = "";

        [XmlAttribute("ExporterMail")]
        public string ExporterMail
        {
            get { return _exporterMail; }
            set { _exporterMail = value; }
        }

        private int _exportNumber = 0;

        [XmlAttribute("ExportNumber")]
        public int ExportNumber
        {
            get { return _exportNumber; }
            set { _exportNumber = value; }
        }

        private string _userName = "";

        [XmlAttribute("UserName")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _sector = "";

        [XmlAttribute("Sector")]
        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }


        private string _exporterName = "";

        [XmlAttribute("ExporterName")]
        public string ExporterName
        {
            get { return _exporterName; }
            set { _exporterName = value; }
        }


        private string _password = "";
        [XmlAttribute("Password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }



        private ExprtType _exportType = ExprtType.SimpleExport;

        [XmlAttribute("ExportType")]
        public ExprtType ExportType
        {
            get { return _exportType; }
            set { _exportType = value; }
        }




        private WorkerDTO[] _workers;



        [XmlArray("Workers"), XmlArrayItem("Worker", typeof(WorkerDTO))] 
        public WorkerDTO[] Workers
        {
            get { return _workers; }
            set { _workers = value; }
        }


        private PeriodType _periodType = PeriodType.Semester;

        [XmlAttribute("PeriodType")]
        public PeriodType PeriodType
        {
            get { return _periodType; }
            set { _periodType = value; }
        }


        private DateTime _initialDate = new DateTime(1900, 1, 1);
        [XmlAttribute("InitialDate")]
        public DateTime InitialDate
        {
            get { return _initialDate; }
            set { _initialDate = value; }
        }


        private DateTime _endDate = new DateTime(1900, 1, 1);
        [XmlAttribute("EndDate")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }


        private int _year = -1;
        [XmlAttribute("Year")]
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }


        private int _period = -1;
        [XmlAttribute("Period")]
        public int Period
        {
            get { return _period; }
            set { _period = value; }
        }

        private bool _transacted = false;
        [XmlAttribute("Transacted")]
        public bool Transacted
        {
            get { return _transacted; }
            set { _transacted = value; }
        }


        private string _struttura = "";

        [XmlAttribute("Struttura")]
        public string Struttura
        {
            get { return _struttura; }
            set { _struttura = value; }
        }

        private string _area = "";

        [XmlAttribute("Area")]
        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }

        private string _error = "";

        [XmlElement ("Errore")]
        public string Errore
        {
            get { return _error; }
            set { _error = value; }
        }


        [XmlElement("FenealwebData")]
        public FenealwebData FenealwebData { get; set; }


        public ExportTrace Clone()
        {
            ExportTrace etrace = new ExportTrace();


            etrace.EndDate = this.EndDate;
            etrace.ExportDate = this.ExportDate;
            etrace.ExporterMail = this.ExporterMail;
            etrace.ExporterName = this.ExporterName;
            etrace.ExportNumber = this.ExportNumber;
            etrace.ExportType = this.ExportType;
            etrace.InitialDate = this.InitialDate;
            etrace.Password = this.Password;
            etrace.Period = this.Period;
            etrace.PeriodType = this.PeriodType;
            etrace.Province = this.Province;
            etrace.Sector = this.Sector;
            etrace.Transacted = this.Transacted;
            etrace.UserName = this.UserName;
            etrace.Year = this.Year;
            etrace.Struttura = this.Struttura;
            etrace.FenealwebData = this.FenealwebData;
            return etrace;
        }


    }


    public enum ExprtType
    {
        SimpleExport,
        ProgramExport,
        LiberiExport,
        ServerSideExport
    }


    

}
