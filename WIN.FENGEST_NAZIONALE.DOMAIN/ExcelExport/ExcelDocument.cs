using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ExcelExport
{
    [DataContract]
    public class ExcelDocument
    {
         [DataMember]
        public List<ExcelRow> Rows { get; set; }

    }
}