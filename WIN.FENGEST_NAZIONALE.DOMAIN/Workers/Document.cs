using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class Document : AbstractPersistenceObject
    {
        private DateTime _documentDate = new DateTime(1900, 1, 1);
        private string _documentType = "";
        private string _state = "";
        private string _notes = "";
        private Provincia _province = new ProvinciaNulla();
        private Comune _comune = new ComuneNullo();
        private Worker _worker;
        private Export _export;
        private Regione _regione = new RegioneNulla();
        private string _struttura = "";
        private string _area = "";

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }
        public string Struttura
        {
            get { return _struttura; }
            set { _struttura = value; }
        }

        public Regione Regione
        {
            get { return _regione; }
            set { _regione = value; }
        }


        public Comune Comune
        {
            get { return _comune; }
            set { _comune = value; }
        }

        protected override void DoValidation()
        {

            if (string.IsNullOrEmpty(_struttura))
                ValidationErrors.Add("Struttura nulla per il documento");

            if (string.IsNullOrEmpty(_area))
                ValidationErrors.Add("Area nulla per il documento");


            if (_province == null)
                ValidationErrors.Add("Provincia nulla per il documento");


            if (_province != null)
                if (_province.Id == -1)
                    ValidationErrors.Add("Provincia nulla per il documento");

            if (_export == null)
                ValidationErrors.Add("Importazione di appartenenza nulla per il documento");


            if (_worker == null)
                ValidationErrors.Add("Lavoratore di appartenenza nulla per il documento");


            if ((_documentDate  ==  DateTime .MaxValue) || (_documentDate  ==  DateTime .MinValue))
                ValidationErrors.Add("Data documento non definita");

            if (string.IsNullOrEmpty (_documentType))
                ValidationErrors.Add("Tipo documento non specificato");



        }


        public Provincia Province
        {
            get { return _province; }
            set { _province = value; }
        }

        public Export ParentExport
        {
            get { return _export; }
            set { _export = value; }
        }



        public Worker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

 
   
        public DateTime DocumentDate
        {
            get { return _documentDate; }
            set { _documentDate = value; }
        }


        
  
        public string DocumentType
        {
            get { return _documentType; }
            set { _documentType = value; }
        }


       


    
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }



    
      
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }


        public WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO ToDocumentDTO()
        {
            DocumentDTO dto = new DocumentDTO();

            dto.DocumentDate = this.DocumentDate;
            dto.DocumentType = this.DocumentType;
            dto.Notes = this.Notes;
            dto.State = this.State;
            dto.Region = this.Regione.Descrizione;
            dto.Province = this.Province.Descrizione;
            dto.City = this.Comune.Descrizione ;
            dto.Struttura = this.Struttura;
            dto.Area = this.Area;


            return dto;
        }
    }
}
