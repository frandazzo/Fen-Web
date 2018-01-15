using System;
using System.Collections.Generic;

using System.Text;


namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport
{
    public class DettaglioQuotaDTO
    {
        private FirmDTO firm;
        private WorkerDTO worker;
       

        private String provincia;
        private DateTime dataRegistrazione;
        private DateTime dataDocumento;
        private String tipoDocumento;
        private String settore;
        private String ente;
        private DateTime dataInizio;
        private DateTime dataFine;
        private double quota;
        private String livello;
        private String contratto;
        private String tipoPrestazione;
        private String note;

        public FirmDTO Firm
        {
            get
            {
                return firm;
            }

            set
            {
                firm = value;
            }
        }

        public WorkerDTO Worker
        {
            get
            {
                return worker;
            }

            set
            {
                worker = value;
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

        public DateTime DataInizio
        {
            get
            {
                return dataInizio;
            }

            set
            {
                dataInizio = value;
            }
        }

        public DateTime DataFine
        {
            get
            {
                return dataFine;
            }

            set
            {
                dataFine = value;
            }
        }

        public double Quota
        {
            get
            {
                return quota;
            }

            set
            {
                quota = value;
            }
        }

        public string Livello
        {
            get
            {
                return livello;
            }

            set
            {
                livello = value;
            }
        }

        public string Contratto
        {
            get
            {
                return contratto;
            }

            set
            {
                contratto = value;
            }
        }

        public string TipoPrestazione
        {
            get
            {
                return tipoPrestazione;
            }

            set
            {
                tipoPrestazione = value;
            }
        }

        public string Note
        {
            get
            {
                return note;
            }

            set
            {
                note = value;
            }
        }

       
    }
}
