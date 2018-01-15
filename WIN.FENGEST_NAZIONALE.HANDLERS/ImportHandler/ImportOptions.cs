using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler
{
    [XmlRootAttribute("ImportOptions", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class ImportOptions
    {
        [XmlAttribute("Validatore")]
        public string Validator { get; set; }

        [XmlAttribute("ErrorLogDir")]
        public string ErrorLogDir { get; set; }

        [XmlAttribute("NomeCoda")]
        public string QueueName { get; set; }

        [XmlAttribute("NomeCodaMorta")]
        public string DeadQueueName { get; set; }


        [XmlAttribute("NomeCodaRecupero")]
        public string RetryQueueName { get; set; }

        [XmlAttribute("MailServer")]
        public string SmtpServer { get; set; }

        [XmlAttribute("MailAccount")]
        public string SmtpAccount { get; set; }

        [XmlAttribute("MailPassword")]
        public string SmtpPassword { get; set; }

        [XmlAttribute("MailFrom")]
        public string SmtpMailFrom { get; set; }


        [XmlAttribute("MailAdministrator")]
        public string MailAdministrator { get; set; }

        [XmlAttribute("FenealwebImportQuoteUrl")]
        public string FenealwebImportQuoteUrl { get; set; }
        [XmlAttribute("NotifyFenealwebUrl")]
        public string NotifyFenealwebUrl { get; set; }
    }
}
