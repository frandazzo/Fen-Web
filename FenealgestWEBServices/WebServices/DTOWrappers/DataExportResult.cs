using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    public class DataExportResult
    {
        public int NumFeneal { get; set; }
        public int NumFilca { get; set; }
        public int NumFillea { get; set; }
        public int NumLiberi { get; set; }
        public int TotLavoratori { get; set; }
        public int TotSindacalizzati { get; set; }
        public decimal RappresentativitaFeneal { get; set; }
        public decimal RappresentativitaFilca { get; set; }
        public decimal RappresentativitaFillea { get; set; }
        public decimal Sindacalizzazione { get; set; }
    }
}