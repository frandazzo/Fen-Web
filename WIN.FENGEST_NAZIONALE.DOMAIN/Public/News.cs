using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Public
{
    public class News : AbstractPersistenceObject
    {
        private string _titolo = "";
        private string _testo = "";

        public string Titolo
        {
            get { return _titolo; }

            set
            {
                _titolo = value;
            }
        }

        public string Testo
        {
            get { return _testo; }

            set
            {
                _testo = value;
            }
        }

        protected override void DoValidation()
        {

            if (string.IsNullOrEmpty(_titolo))
                ValidationErrors.Add("Titolo non valido");

            if (!string.IsNullOrEmpty(_titolo))
                if (_titolo.Length > 255)
                    ValidationErrors.Add("Titolo non può eccedere 255 caratteri");

            if (string.IsNullOrEmpty(_testo))
                ValidationErrors.Add("Testo non valido");

            if (!string.IsNullOrEmpty(_testo))
                if (_testo.Length > 10000)
                    ValidationErrors.Add("Testo non può eccedere 10000 caratteri");

            if (string.IsNullOrEmpty(CreatoDa))
                ValidationErrors.Add("Creato da non valido");

            if (!string.IsNullOrEmpty(CreatoDa))
                if (CreatoDa.Length > 60)
                    ValidationErrors.Add("Creato da non può eccedere 60 caratteri");

            if (DataCreazione == DateTime.MaxValue || DataCreazione == DateTime.MinValue)
                ValidationErrors.Add("Data non definita");
        }

    }
}
