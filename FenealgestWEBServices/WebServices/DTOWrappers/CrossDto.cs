using System;
using System.Collections.Generic;
using System.Web;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    public class CrossDto
    {
	public CrossDto(){IsValid  = true;}

        public QueryResultDTO Dto { get; set; }
        public string Province { get; set; }
        public string FromProvince { get; set; }
        public string MailTo  { get; set; }
        public bool IsValid { get; set; }
    }
}