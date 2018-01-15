using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class CoordinataBancaria : AbstractPersistenceObject
    {
        public string Iban { get; set; }
        public string Banca { get; set; }
        public string Indirizzo { get; set; }
        public string NumAgenzia { get; set; }
        public int Id_Struttura { get; set; }
    }
}
