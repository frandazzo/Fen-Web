using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ExcelExport
{
    [DataContract]
    public class ExcelProperty
    {
        public ExcelProperty()
        {
           
        }
        public ExcelProperty(String name, String value, int priority)
        {
            this.Priority = priority;
            this.Name = name;
            this.Value = value;
        }
         [DataMember]
        public int Priority { get; set; }
         [DataMember]
        public string Name { get; set; }
         [DataMember]
        public string Value { get; set; }
    }
}