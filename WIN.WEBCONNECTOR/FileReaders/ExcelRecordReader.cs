using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelRecordReader : WIN.BASEREUSE.BaseExcelReader
    {

        private const string COGNOME_UTENTE  = "COGNOME_UTENTE";

        private const string  NOME_UTENTE  = "NOME_UTENTE";
        private const string  FISCALE  = "FISCALE";
        private const string  COMUNE_NASCITA = "COMUNE_NASCITA";
        private const string  DATA_NASCITA_UTENTE  = "DATA_NASCITA_UTENTE";
        private const string  COMUNE = "COMUNE";
        private const string  INDIRIZZO  = "INDIRIZZO";
        private const string  CAP  = "CAP";
        private const string  TELEFONO1  = "TELEFONO1";
        
         //private const string  ENTE  = "ENTE";
         //private const string  SETTORE  = "SETTORE";
       
        private const string   AZIENDA_IMPIEGO  = "AZIENDA_IMPIEGO";
        private const string   P_IVA = "P_IVA";
        private const string  CONTRATTO  = "CONTRATTO";
        private const string   LIVELLO  = "LIVELLO";
        private const string   QUOTA  = "QUOTA";


   




        public override void ParseImportFile()
        {

            StringBuilder s = new StringBuilder();

                //Campi Utente
            if (!FindField(COGNOME_UTENTE))
                s.AppendLine("Formato file non valido: " + COGNOME_UTENTE);
            if (!FindField(NOME_UTENTE))
                s.AppendLine("Formato file non valido: " + NOME_UTENTE);
            if (!FindField(FISCALE)) 
                s.AppendLine("Formato file non valido: " + FISCALE);
            if (!FindField(DATA_NASCITA_UTENTE))
                s.AppendLine("Formato file non valido: " + DATA_NASCITA_UTENTE);

            if (!FindField(COMUNE_NASCITA))
                s.AppendLine("Formato file non valido: " + COMUNE_NASCITA);

            if (!FindField(COMUNE))
                s.AppendLine("Formato file non valido: " + COMUNE);
            if (!FindField(INDIRIZZO))
                s.AppendLine("Formato file non valido: " + INDIRIZZO);
            if (!FindField(CAP))
                s.AppendLine("Formato file non valido: " + CAP);
            if (!FindField(TELEFONO1))
                s.AppendLine("Formato file non valido: " + TELEFONO1);
           


            ////'*************************+
            ////'Campi Ente Settore
            //if (!FindField(ENTE))
            //    s.AppendLine("Formato file non valido: " + ENTE);
            //if (!FindField(SETTORE))
            //    s.AppendLine("Formato file non valido: " + SETTORE);




            //'*********************************
            //'Campi Quota
        
            if (!FindField(LIVELLO))
                s.AppendLine("Formato file non valido: " + LIVELLO);
            if (!FindField(QUOTA))
                s.AppendLine("Formato file non valido: " + QUOTA);
          

            //'**********************
            //Campi Azienda
            if (!FindField(AZIENDA_IMPIEGO))
                s.AppendLine("Formato file non valido: " + AZIENDA_IMPIEGO);
            if (!FindField(P_IVA))
                s.AppendLine("Formato file non valido: " + P_IVA );
            if (!FindField(CONTRATTO))
                s.AppendLine("Formato file non valido: " + CONTRATTO);


            if (!string.IsNullOrEmpty (s.ToString ()))
                throw new Exception(s.ToString());
    
        }
        
        public ExcelRecordReader(string fileName)
        {
            base.m_Filename = fileName;
        }




    }
}
