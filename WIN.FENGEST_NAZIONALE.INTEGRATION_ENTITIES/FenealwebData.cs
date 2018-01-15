using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
      [Serializable]
    public class FenealwebData
    {
        //riferimento al file natico inviato alle normalizzazioni automatiche
            [XmlAttribute("AuthomaticIntegrationFilename")]
            public string AuthomaticIntegrationFilename { get; set; }
          [XmlAttribute("NormalizedXmlFile")]
            public string NormalizedXmlFile { get; set; }
        //tipo documento per riconoscere se si tratta di quote inps, previsionali o edili
            [XmlAttribute("DocumentType")]
            public string DocumentType { get; set; }
        //guid per identificare univocamente un incasso quote in fenealweb
            [XmlAttribute("Guid")]
            public string Guid { get; set; }

           [XmlAttribute("Entity")]
            public string Entity { get; set; }
            [XmlAttribute("UpdateFirmas")]
            public bool UpdateFirmas { get; set; }
           [XmlAttribute("UpdateWorkers")]
            public bool UpdateWorkers { get; set; }
           [XmlAttribute("CreateDelegaIfNotExist")]
            public bool CreateDelegaIfNotExist { get; set; }
           [XmlAttribute("CreateListaLavoro")]
            public bool CreateListaLavoro { get; set; }
           [XmlAttribute("AssociateDelega")]
            public bool AssociateDelega { get; set; }
           [XmlAttribute("Notes1")]
            public string Notes1 { get; set; }
           [XmlAttribute("Notes2")]
            public string Notes2 { get; set; }

    }
}
