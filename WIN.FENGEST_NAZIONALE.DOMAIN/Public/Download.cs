using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using System.IO;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Public
{
    public class Download : AbstractPersistenceObject
    {
        //private DateTime _date = DateTime.Now;
        //private string _descrizione = "";
        private string _percorso = "";
        private TipoDownload _tipo = null;
        private Regione _regione = new RegioneNulla();

        

        public string DescrizioneRegione
        {
            get { return _regione.Descrizione; }
        }

        public string DescrizioneTipo
        {
            get { return _tipo.Descrizione; }
        }
       
        public string NomeFile
        {
            get 
            {
                if (string.IsNullOrEmpty(_percorso))
                    return "";
                if (!System.IO.File.Exists(_percorso))
                    return "";
                return Path.GetFileName(_percorso); 
            }
        }

        public TipoDownload Tipo
        {
            get { return _tipo; }

            set { _tipo = value; }
        }

        public Regione Regione
        {
            get { return _regione; }

            set { _regione = value; }
        }

        public double DimensioneFileInKB
        {
            get 
            {
                if (string.IsNullOrEmpty(_percorso))
                    return 0;
                if (!System.IO.File.Exists(_percorso))
                    return 0;
                FileInfo i = new FileInfo (_percorso );

                return ((double)i.Length / 1024);
            }
        }

        public string DimensioneToString()
        {
            //return "(" + DimensioneFileInKB.ToString() + " Kb)";
            return "(" + String.Format("{0:0.##}", DimensioneFileInKB) + " Kb)";
        }


        public string Percorso
        {
            get { return _percorso; }

            set { _percorso = value; }
        }

        //public string InseritoDa
        //{
        //    get { return _inseritoDa; }
        //}

        //public Download(DateTime date, string descrizione, string percorso, string inseritoDa)
        //{
        //    DataCreazione = date;
        //    Descrizione = descrizione;
        //    _percorso = percorso;
        //    CreatoDa = inseritoDa;
        //}

        protected override void DoValidation()
        {

            if (string.IsNullOrEmpty(Descrizione))
                ValidationErrors.Add("Descrizione non valida");

            if (!string.IsNullOrEmpty(Descrizione))
                if (Descrizione.Length > 255)
                    ValidationErrors.Add("Descrizione non può eccedere 255 caratteri");

            if (string.IsNullOrEmpty(_percorso))
                ValidationErrors.Add("Percorso non valido");

            if (!string.IsNullOrEmpty(_percorso))
                if (_percorso.Length > 4000)
                    ValidationErrors.Add("Percorso non può eccedere 4000 caratteri");
            
            if (!System.IO.File.Exists(_percorso))
                ValidationErrors.Add("File non esistente");

            if (string.IsNullOrEmpty(CreatoDa))
                ValidationErrors.Add("Creato da non valido");

            if (!string.IsNullOrEmpty(CreatoDa))
                if (CreatoDa.Length > 60)
                    ValidationErrors.Add("Creato da non può eccedere 60 caratteri");

            if (DataCreazione == DateTime.MaxValue || DataCreazione == DateTime.MinValue)
                ValidationErrors.Add("Data non definita");

            if (_tipo == null)
                ValidationErrors.Add("Tipo download non specificato");

        }

    }
}
