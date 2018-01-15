using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ExcelExport
{
     [DataContract]
    public class ExcelRow
    {
          [DataMember]
        public List<ExcelProperty> Properties { get; set; }
          [DataMember]
        public ExcelDocument Document { get; set; }

    }
}