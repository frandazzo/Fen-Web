using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    public class DataExport
    {
        public string CompleteFileName { get; set; }
        public string FileName { get; set; }
        public bool Managed { get; set; }
        public bool IsIQA { get; set; }
        public DateTime Date { get; set; }


    }
}