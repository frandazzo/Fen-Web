using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    [Serializable]
    public class WorkerDTO
    {
        public WorkerDTO() { }

        [XmlAttribute("RowNumber")]
        public int RowNumber { get; set; }


        [XmlAttribute("IsValid")]
        public bool  IsValid { get; set; }


        [XmlAttribute("ExistBirthPlace")]
        public bool ExistBirthPlace { get; set; }

        [XmlAttribute("ExistLivingPlace")]
        public bool ExistLivingPlace { get; set; }


        private string _nationality = "";

        [XmlAttribute("Nationality")]
        public string Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }


        private string _lastModifier = "";

        [XmlAttribute("LastModifer")]
        public string LastModifier
        {
            get { return _lastModifier; }
            set { _lastModifier = value; }
        }

        [XmlIgnore]
        public int Id { get; set; }
        
        private string _errors = "";

        [XmlAttribute("Errors")]
        public string Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }


        private SubscriptionDTO _subscription;

        [XmlElement("Subscription")]
        public SubscriptionDTO Subscription
        {
            get { return _subscription; }
            set { _subscription = value; }
        }

        private SubscriptionDTO[] _subscriptions;
        [XmlArray("Subscriptions"), XmlArrayItem("Subscription", typeof(SubscriptionDTO))]
        public SubscriptionDTO[] Subscriptions
        {
            get { return _subscriptions; }
            set { _subscriptions = value; }
        }


        private DocumentDTO[] _documents;
        [XmlArray("Documents"), XmlArrayItem("Document", typeof(DocumentDTO))] 
        public DocumentDTO[] Documents
        {
            get { return _documents; }
            set { _documents = value; }
        }



        private string _name = "";

        [XmlAttribute("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _surname = "";
        [XmlAttribute("Surname")]
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }


        private string _fiscalcode = "";
        [XmlAttribute("Fiscalcode")]
        public string Fiscalcode
        {
            get { return _fiscalcode; }
            set { _fiscalcode = value; }
        }


        private DateTime _birthDate = new DateTime(1900, 1, 1);

        [XmlAttribute("BirthDate")]
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }



        private DateTime _lastUpdate =DateTime.Now;

        [XmlAttribute("LastUpdate")]
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set { _lastUpdate = value; }
        }



        private string _birthPlace = "";


        [XmlAttribute("BirthPlace")]
        public string BirthPlace
        {
            get { return _birthPlace; }
            set { _birthPlace = value; }
        }


        private string _birthProvince = "";


        [XmlIgnore]
        public string BirthProvince
        {
            get { return _birthProvince; }
            set { _birthProvince = value; }
        }

        private string _livingProvince = "";


        [XmlIgnore]
        public string LivingProvince
        {
            get { return _livingProvince; }
            set { _livingProvince = value; }
        }


        private string _currentAzienda = "";
        [XmlAttribute("CurrentAzienda")]
        public string CurrentAzienda
        {
            get { return _currentAzienda; }
            set { _currentAzienda = value; }
        }

        private string _iscrittoA = "";
       [XmlAttribute("IscrittoA")]
        public string IscrittoA
        {
            get { return _iscrittoA; }
            set { _iscrittoA = value; }
        }

        private DateTime _liberoAl;
       [XmlAttribute("LiberoAl")]
        public DateTime LiberoAl
        {
            get { return _liberoAl; }
            set { _liberoAl = value; }
        }
        


        private string _livingPlace = "";
        [XmlAttribute("LivingPlace")]
        public string LivingPlace
        {
            get { return _livingPlace; }
            set { _livingPlace = value; }
        }

        private string _address = "";
        [XmlAttribute("Address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        private string _cap = "";

        [XmlAttribute("Cap")]
        public string Cap
        {
            get { return _cap; }
            set { _cap = value; }
        }



        private string _phone = "";
        [XmlAttribute("Phone")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }




        public void AllineaListe()
        {
            //allineo le iscrizioni
            _arraylist = new ArrayList();
            if (_subscriptions != null)
            {
                foreach (SubscriptionDTO item in _subscriptions)
                {
                    _arraylist.Add(item);
                }
                if (_arraylist.Count == 0)
                    if (_subscription != null)
                        _arraylist.Add(_subscription);
            }
            else
            {
                if (_subscription != null)
                    _arraylist.Add(_subscription);
            }



            // allineo i documenti
            _arraylist1 = new ArrayList();
            if (_documents != null)
            {
                foreach (DocumentDTO item in _documents)
                {
                    _arraylist1.Add(item);
                }
            }

        }

        private ArrayList _arraylist = new ArrayList();
        [XmlIgnore]
        public ArrayList SubscriptionsForMasterDetail
        {
            get { return _arraylist; }
            set { _arraylist = value; }
        }

        private ArrayList _arraylist1 = new ArrayList();
        [XmlIgnore]
        public ArrayList DocumentsForMasterDetail
        {
            get { return _arraylist1; }
            set { _arraylist1 = value; }
        }



        public bool WasInProvince(string province)
        {
            if (_subscriptions == null)
                return false;

            foreach (SubscriptionDTO item in _subscriptions)
            {
                if (item.Province.ToUpper().Equals(province.ToUpper()))
                    return true;
            }

            return false;
        }
        
    }
}
