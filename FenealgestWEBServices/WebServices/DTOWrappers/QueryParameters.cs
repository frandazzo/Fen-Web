using System;
using System.Collections.Generic;
using System.Web;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    [Serializable]
    public class QueryParameters
    {
        public string Name { get ;  set; }
        public string Surname { get;  set; }
        public string LivingPlace { get;  set; }
        public string Nationality { get;  set; }
        public DateTime  BirthDate { get;  set; }
        public bool CheckDate { get;  set; }
        public string Region { get;  set; }
        public int MaxResult { get;  set; }
        public string CompanyFiscalCode { get; set; }
        public string CompanyDescription { get; set; }

        public QueryParameters()
        {
            
        }
    }
}
