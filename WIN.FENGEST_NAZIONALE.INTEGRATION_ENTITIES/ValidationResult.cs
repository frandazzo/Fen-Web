using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class ValidationResult
    {

        public bool IsValidated { get; set; }

        private string _errors = "";

        public string Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

    }
}
