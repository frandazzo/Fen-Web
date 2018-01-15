using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN
{
    public class Appartenenza
    {

        private StrutturaApparteneza _struttura = StrutturaApparteneza.Feneal_Nazionale;
        private Regione _regione;
        private Provincia _provincia;


        public Regione Regione
        {
            get { return _regione; }
            set { _regione = value; }
        }


        public Provincia Provicnia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        public StrutturaApparteneza Struttura
        {
            get { return _struttura; }
            set { _struttura = value; }
        }


        public override string ToString()
        {
            if (_struttura == StrutturaApparteneza.Feneal_Nazionale)
                return "Nazionale";
            else if (_struttura == StrutturaApparteneza.Feneal_Provinciale)
                return _provincia.Descrizione;
            else
                return _regione.Descrizione;
        }

    }
}
