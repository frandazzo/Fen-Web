using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    [Serializable]
    public class SubscriptionDTO
    {

        public SubscriptionDTO() { }

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

        private string _sector = "";

        [XmlAttribute("Sector")]
        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        
        }

        private string _contract = "";

        [XmlAttribute("Contract")]
        public string Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }
  

        private string _region = "";
        [XmlAttribute("Region")]
        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }


        private string _entity = "";
        [XmlAttribute("Entity")]
        public string Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        private string _employCompany = "";
        [XmlAttribute("EmployCompany")]
        public string EmployCompany
        {
            get { return _employCompany; }
            set { _employCompany = value; }
        }


        private string _fiscalCode = "";
        [XmlAttribute("FiscalCode")]
        public string FiscalCode
        {
            get { return _fiscalCode; }
            set { _fiscalCode = value; }
        }


        private double _quota = 0d;
        [XmlAttribute("Quota")]
        public double Quota
        {
            get { return _quota; }
            set { _quota = value; }
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


        private int _year = 2010;
        [XmlAttribute("Year")]
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }


        private int _semester = 1;
        [XmlAttribute("Semester")]
        public int Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }


        private PeriodType _periodType = PeriodType.Semester;

        [XmlAttribute("PeriodType")]
        public PeriodType PeriodType
        {
            get { return _periodType; }
            set { _periodType = value; }
        }

        private FenealwebSubscriptionDTOData _fenealwebSubscriptionDTOData;
        [XmlElement("FenealwebSubscriptionDTOData")]
        public FenealwebSubscriptionDTOData FenealwebSubscriptionDTOData
        {
            get { return _fenealwebSubscriptionDTOData; }
            set { _fenealwebSubscriptionDTOData = value; }
        }

        private string _level = "";
        [XmlAttribute("Level")]
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }


        private string _province = "";
        [XmlAttribute("Province")]
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }



    }
}
