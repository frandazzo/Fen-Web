using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class LiberoDTO
    {
        public LiberoDTO() { }

     
        private string _name = "";
        private string _surname = "";
        private string _fiscalcode = "";
        private DateTime _birthDate = new DateTime(1900, 1, 1);
        private string _nationality = "";
        private string _birthProvince = "";
        private string _birthPlace = "";
        private string _livingProvince = "";
        private string _livingPlace = "";
        private string _address = "";
        private string _cap = "";
        private string _phone = "";


        private DateTime _liberoAl = DateTime.Now;
        private string _ente = "";
        private string _currentAzienda = "";
        private string _iscrittoA = "";

        private string _provinceName = "";
        public int Id { get; set; }
        public bool IsValid { get; set; }
        private string _errors = "";

  
        public string Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }


        public DateTime LiberoAl
        {
            get { return _liberoAl; }
            set { _liberoAl = value; }
        }

        public string ProvinceName
        {
            get { return _provinceName; }
            set { _provinceName = value; }
        }
       
        public string Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }

        public string Ente
        {
            get { return _ente; }
            set { _ente = value; }
        }


       
    
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


        public string BirthProvince
        {
            get { return _birthProvince; }
            set { _birthProvince = value; }
        }


        public string LivingProvince
        {
            get { return _livingProvince; }
            set { _livingProvince = value; }
        }


        public string CurrentAzienda
        {
            get { return _currentAzienda; }
            set { _currentAzienda = value; }
        }

        public string IscrittoA
        {
            get { return _iscrittoA; }
            set { _iscrittoA = value; }
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




   

        
    }
}
