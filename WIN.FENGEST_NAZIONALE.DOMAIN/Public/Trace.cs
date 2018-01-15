using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Public
{
    public class Trace : AbstractPersistenceObject
    {

        public int Counter { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string App { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }


        protected override void DoValidation()
        {
            if (string.IsNullOrEmpty(App))
                ValidationErrors.Add("Applicazione non specificata");

            if (string.IsNullOrEmpty(Province))
                ValidationErrors.Add("Provincia non specificata");


            if (string.IsNullOrEmpty(Region))
                ValidationErrors.Add("Regione non specificata");


        }

    }
}
