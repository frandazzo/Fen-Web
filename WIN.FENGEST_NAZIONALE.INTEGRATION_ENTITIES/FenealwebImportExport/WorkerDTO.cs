using System;
using System.Collections.Generic;

using System.Text;


namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport
{
    public class WorkerDTO
    {
        private string _name = "";
        private string _surname = "";
        private string _fiscalcode = "";
        private DateTime _birthDate = new DateTime(1900, 1, 1);
        private string _birthPlace = "";
        private string _livingPlace = "";
        private string _address = "";
        private string _phone = "";
        private string _phone2 = "";
        private string _cap = "";
        private string _nationality = "";




        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Fiscalcode
        {
            get { return _fiscalcode; }
            set { _fiscalcode = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public string BirthPlace
        {
            get { return _birthPlace; }
            set { _birthPlace = value; }
        }

        public string LivingPlace
        {
            get { return _livingPlace; }
            set { _livingPlace = value; }
        }


        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Cap
        {
            get { return _cap; }
            set { _cap = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string Phone2
        {
            get { return _phone2; }
            set { _phone2 = value; }
        }

        public string Nationality
        {
            get
            {
                return _nationality;
            }

            set
            {
                _nationality = value;
            }
        }

        public string CE { get; set; }

        public string EC { get; set; }
    }
}
