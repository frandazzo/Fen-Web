using System;
using System.Collections.Generic;

using System.Text;


namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport
{
    public class RiepilogoQuotaDTO
    {

        private List<DettaglioQuotaDTO> dettagli;

        private DateTime dataDocumento;
        private DateTime dataRegistrazione;
        private String tipoDocumento;
        private String settore;
        private String ente;
        private String compentenza;

        private String provincia;
        private long companyId;

        private String guid;
        private String originalFilename;
        private String xmlFilename;
        private String logFilename;

        private String note1;
        private String note2;

        private bool updateFirmas;
        private bool updateWorkers;
        private bool creaDelegaIfNotExist;
        private bool creaListaLavoro;
        private bool associaDelega;

        private String exporterMail;
        private String exporterName;
        private int exportNumber;

       

        public DateTime DataDocumento
        {
            get
            {
                return dataDocumento;
            }

            set
            {
                dataDocumento = value;
            }
        }

        public DateTime DataRegistrazione
        {
            get
            {
                return dataRegistrazione;
            }

            set
            {
                dataRegistrazione = value;
            }
        }

        public string TipoDocumento
        {
            get
            {
                return tipoDocumento;
            }

            set
            {
                tipoDocumento = value;
            }
        }

        public string Settore
        {
            get
            {
                return settore;
            }

            set
            {
                settore = value;
            }
        }

        public string Ente
        {
            get
            {
                return ente;
            }

            set
            {
                ente = value;
            }
        }

        public string Compentenza
        {
            get
            {
                return compentenza;
            }

            set
            {
                compentenza = value;
            }
        }

        public string Provincia
        {
            get
            {
                return provincia;
            }

            set
            {
                provincia = value;
            }
        }

        public long CompanyId
        {
            get
            {
                return companyId;
            }

            set
            {
                companyId = value;
            }
        }

        public string Guid
        {
            get
            {
                return guid;
            }

            set
            {
                guid = value;
            }
        }

        public string OriginalFilename
        {
            get
            {
                return originalFilename;
            }

            set
            {
                originalFilename = value;
            }
        }

        public string XmlFilename
        {
            get
            {
                return xmlFilename;
            }

            set
            {
                xmlFilename = value;
            }
        }

        public string LogFilename
        {
            get
            {
                return logFilename;
            }

            set
            {
                logFilename = value;
            }
        }

        public string Note1
        {
            get
            {
                return note1;
            }

            set
            {
                note1 = value;
            }
        }

        public string Note2
        {
            get
            {
                return note2;
            }

            set
            {
                note2 = value;
            }
        }

        public bool UpdateFirmas
        {
            get
            {
                return updateFirmas;
            }

            set
            {
                updateFirmas = value;
            }
        }

        public bool UpdateWorkers
        {
            get
            {
                return updateWorkers;
            }

            set
            {
                updateWorkers = value;
            }
        }

        public bool CreaDelegaIfNotExist
        {
            get
            {
                return creaDelegaIfNotExist;
            }

            set
            {
                creaDelegaIfNotExist = value;
            }
        }

        public bool CreaListaLavoro
        {
            get
            {
                return creaListaLavoro;
            }

            set
            {
                creaListaLavoro = value;
            }
        }

        public bool AssociaDelega
        {
            get
            {
                return associaDelega;
            }

            set
            {
                associaDelega = value;
            }
        }

        public string ExporterMail
        {
            get
            {
                return exporterMail;
            }

            set
            {
                exporterMail = value;
            }
        }

        public string ExporterName
        {
            get
            {
                return exporterName;
            }

            set
            {
                exporterName = value;
            }
        }

        public int ExportNumber
        {
            get
            {
                return exportNumber;
            }

            set
            {
                exportNumber = value;
            }
        }

        public List<DettaglioQuotaDTO> Dettagli
        {
            get
            {
                return dettagli;
            }

            set
            {
                dettagli = value;
            }
        }
    }
}
