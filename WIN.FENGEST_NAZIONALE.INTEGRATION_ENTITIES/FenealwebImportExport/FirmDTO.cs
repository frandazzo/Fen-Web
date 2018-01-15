using System;
using System.Collections.Generic;

using System.Text;


namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport
{
    public class FirmDTO
    {
        private String description;
        private String piva;
        private String city;
        private String address;
        private String cap;
        private String phone;

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Piva
        {
            get
            {
                return piva;
            }

            set
            {
                piva = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string Cap
        {
            get
            {
                return cap;
            }

            set
            {
                cap = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }
    }
}
