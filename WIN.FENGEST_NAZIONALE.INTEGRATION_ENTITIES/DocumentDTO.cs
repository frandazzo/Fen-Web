using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    [Serializable]
    public class DocumentDTO
    {

        public DocumentDTO() { }

        private DateTime _documentDate = new DateTime(1900, 1, 1);

        [XmlAttribute("DocumentDate")]
        public DateTime DocumentDate
        {
            get { return _documentDate; }
            set { _documentDate = value; }
        }

       


        private string _documentType = "";
        [XmlAttribute("DocumentType")]
        public string DocumentType
        {
            get { return _documentType; }
            set { _documentType = value; }
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

        private string _province = "";
        [XmlAttribute("Province")]
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }


        private string _city = "";
        [XmlAttribute("City")]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _region = "";
        [XmlAttribute("Region")]
        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }



        private string _state = "";


        [XmlAttribute("State")]
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }



        private string _notes = "";
        [XmlAttribute("Notes")]
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }


    }
}
