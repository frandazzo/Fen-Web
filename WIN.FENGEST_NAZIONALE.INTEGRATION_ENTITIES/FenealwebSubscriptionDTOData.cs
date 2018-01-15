using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    [Serializable]
    public class FenealwebSubscriptionDTOData
    {
        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }
        [XmlAttribute("EndDate")]
        public DateTime EndDate { get; set; }
        [XmlAttribute("TipoPrestazione")]
        public string TipoPrestazione { get; set; }
        [XmlAttribute("Note")]
        public string Note { get; set; }

        [XmlAttribute("FirmCity")]
        public string FirmCity { get; set; }
        [XmlAttribute("WorkerEC")]
        public string WorkerEC { get; set; }
        [XmlAttribute("WorkerCE")]
        public string WorkerCE { get; set; }
        [XmlAttribute("FirmEC")]
        public string FirmEC { get; set; }
        [XmlAttribute("FirmCE")]
        public string FirmCE { get; set; }
        [XmlAttribute("FirmNotes")]
        public string FirmNotes { get; set; }
        [XmlAttribute("FirmTel")]
        public string FirmTel { get; set; }
        [XmlAttribute("FirmCap")]
        public string FirmCap { get; set; }
        [XmlAttribute("FirmAddress")]
        public string FirmAddress { get; set; }
        [XmlAttribute("WorkerPhone2")]
        public string WorkerPhone2 { get; set; }
    }
}
