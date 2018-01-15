﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelIQARecordReader : WIN.BASEREUSE.BaseExcelReader
    {

        public ExcelIQARecordReader(string fileName)
        {
            base.m_Filename = fileName;
        }

        private string COGNOME_UTENTE  = "COGNOME_UTENTE";
        private string NOME_UTENTE  = "NOME_UTENTE";
        private string FISCALE  = "FISCALE";
        private string DATA_NASCITA_UTENTE = "DATA_NASCITA_UTENTE";
        private string PROVINCIA_NASCITA = "PROVINCIA_NASCITA";
        private string COMUNE_NASCITA  = "COMUNE_NASCITA";
        private string PROVINCIA = "PROVINCIA";
        private string COMUNE  = "COMUNE";
        private string INDIRIZZO  = "INDIRIZZO";
        private string CAP = "CAP";
        private string TELEFONO1 = "TELEFONO1";
        private string TELEFONO2 = "TELEFONO2";
        private string CODICE_CE_UTENTE = "CODICE_CE_UTENTE";
        private string CODICE_EC_UTENTE = "CODICE_EC_UTENTE";
        private string NOTE_UTENTE = "NOTE_UTENTE";

        private string SETTORE = "SETTORE";
        private string ENTE  = "ENTE";

        private string AZIENDA_IMPIEGO = "AZIENDA_IMPIEGO";
        private string PARTITA_IVA = "PARTITA_IVA";
        private string PROVINCIA_AZIENDA = "PROVINCIA_AZIENDA";
        private string COMUNE_AZIENDA = "COMUNE_AZIENDA";
        private string INDIRIZZO_AZIENDA = "INDIRIZZO_AZIENDA";
        private string CAP_AZIENDA = "CAP_AZIENDA";
        private string TELEFONO_AZIENDA = "TELEFONO_AZIENDA";
        private string NOTE_AZIENDA = "NOTE_AZIENDA";
        private string CODICE_CE_AZIENDA = "CODICE_CE_AZIENDA";
        private string CODICE_EC_AZIENDA = "CODICE_EC_AZIENDA";
        private string CONTRATTO = "CONTRATTO";

        private string DATA_INIZIO  = "DATA_INIZIO";
        private string DATA_FINE  = "DATA_FINE";
        private string QUOTA  = "QUOTA";
        private string LIVELLO = "LIVELLO";
        private string NOTE = "NOTE";
        private string ORE_AVORATE = "ORE_LAVORATE";
        private string ORE_MALATTIA_INFORTUNIO = "ORE_MALATTIA_INFORTUNIO";
        private string NOME_REFERENTE = "NOME_REFERENTE";
        private string COGNOME_REFERENTE = "COGNOME_REFERENTE";





        public override void ParseImportFile()
        {
            
            StringBuilder s = new StringBuilder();

            if (!FindField(COGNOME_UTENTE))
                s.AppendLine("Formato file non valido: " + COGNOME_UTENTE);
            if (!FindField(NOME_UTENTE))
                s.AppendLine("Formato file non valido: " + NOME_UTENTE);
            if (!FindField(FISCALE)) 
                s.AppendLine("Formato file non valido: " + FISCALE);
            if (!FindField(DATA_NASCITA_UTENTE))
                s.AppendLine("Formato file non valido: " + DATA_NASCITA_UTENTE);

            if (!FindField(PROVINCIA_NASCITA))
                s.AppendLine("Formato file non valido: " + PROVINCIA_NASCITA);
            if (!FindField(COMUNE_NASCITA))
                s.AppendLine("Formato file non valido: " + COMUNE_NASCITA);
            if (!FindField(PROVINCIA)) 
                s.AppendLine("Formato file non valido: " + PROVINCIA);
            if (!FindField(COMUNE))
                s.AppendLine("Formato file non valido: " + COMUNE);

      
            if (!FindField(INDIRIZZO))
                s.AppendLine("Formato file non valido: " + INDIRIZZO);
            if (!FindField(CAP))
                s.AppendLine("Formato file non valido: " + CAP);
            if (!FindField(TELEFONO1)) 
                s.AppendLine("Formato file non valido: " + TELEFONO1);
            if (!FindField(TELEFONO2))
                s.AppendLine("Formato file non valido: " + TELEFONO2);


            if (!FindField(CODICE_CE_UTENTE))
                s.AppendLine("Formato file non valido: " + CODICE_CE_UTENTE);
            if (!FindField(CODICE_EC_UTENTE)) 
                s.AppendLine("Formato file non valido: " + CODICE_EC_UTENTE);
            if (!FindField(NOTE_UTENTE))
                s.AppendLine("Formato file non valido: " + NOTE_UTENTE);



            if (!FindField(ENTE)) 
                s.AppendLine("Formato file non valido: " + ENTE);
            if (!FindField(SETTORE))
                s.AppendLine("Formato file non valido: " + SETTORE);



            if (!FindField(NOTE))
                s.AppendLine("Formato file non valido: " + NOTE);
            if (!FindField(ORE_AVORATE))
                s.AppendLine("Formato file non valido: " + ORE_AVORATE);
            if (!FindField(ORE_MALATTIA_INFORTUNIO)) 
                s.AppendLine("Formato file non valido: " + ORE_MALATTIA_INFORTUNIO);
            if (!FindField(DATA_INIZIO))
                s.AppendLine("Formato file non valido: " + DATA_INIZIO);
            if (!FindField(DATA_FINE))
                s.AppendLine("Formato file non valido: " + DATA_FINE);
            if (!FindField(LIVELLO)) 
                s.AppendLine("Formato file non valido: " + LIVELLO);
            if (!FindField(QUOTA))
                s.AppendLine("Formato file non valido: " + QUOTA);
             if (!FindField(NOME_REFERENTE)) 
                s.AppendLine("Formato file non valido: " + NOME_REFERENTE);
            if (!FindField(COGNOME_REFERENTE))
                s.AppendLine("Formato file non valido: " + COGNOME_REFERENTE);


              if (!FindField(AZIENDA_IMPIEGO))
                s.AppendLine("Formato file non valido: " + AZIENDA_IMPIEGO);
            if (!FindField(PARTITA_IVA))
                s.AppendLine("Formato file non valido: " + PARTITA_IVA);
            if (!FindField(PROVINCIA_AZIENDA)) 
                s.AppendLine("Formato file non valido: " + PROVINCIA_AZIENDA);
            if (!FindField(INDIRIZZO_AZIENDA))
                s.AppendLine("Formato file non valido: " + INDIRIZZO_AZIENDA);
            if (!FindField(COMUNE_AZIENDA))
                s.AppendLine("Formato file non valido: " + COMUNE_AZIENDA);
            if (!FindField(CAP_AZIENDA)) 
                s.AppendLine("Formato file non valido: " + CAP_AZIENDA);
            if (!FindField(TELEFONO_AZIENDA))
                s.AppendLine("Formato file non valido: " + TELEFONO_AZIENDA);
             if (!FindField(NOTE_AZIENDA)) 
                s.AppendLine("Formato file non valido: " + NOTE_AZIENDA);
            if (!FindField(CODICE_CE_AZIENDA))
                s.AppendLine("Formato file non valido: " + CODICE_CE_AZIENDA);
             if (!FindField(CODICE_EC_AZIENDA)) 
                s.AppendLine("Formato file non valido: " + CODICE_EC_AZIENDA);
            if (!FindField(CONTRATTO))
                s.AppendLine("Formato file non valido: " + CONTRATTO);


            if (!string.IsNullOrEmpty(s.ToString()))
                throw new Exception(s.ToString());

        }
    


    }
}
