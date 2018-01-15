using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelInpsRecordReader : WIN.BASEREUSE.BaseExcelReader
    {

        public ExcelInpsRecordReader(string fileName)
        {
            base.m_Filename = fileName;
        }

        private string COGNOME_UTENTE  = "COGNOME_UTENTE";
        private string NOME_UTENTE  = "NOME_UTENTE";
        private string FISCALE  = "FISCALE";
        private string DATA_NASCITA_UTENTE = "DATA_NASCITA_UTENTE";

        private string PROVINCIA = "PROVINCIA";
        private string COMUNE  = "COMUNE";
        private string INDIRIZZO  = "INDIRIZZO";
        private string CAP = "CAP";


        private string SETTORE = "SETTORE";



        private string DATA_INIZIO  = "DATA_INIZIO";

        private string QUOTA  = "QUOTA";
        private string TIPO_PRESTAZIONE = "TIPO_PRESTAZIONE";
      
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

            if (!FindField(PROVINCIA)) 
                s.AppendLine("Formato file non valido: " + PROVINCIA);
            if (!FindField(COMUNE))
                s.AppendLine("Formato file non valido: " + COMUNE);

      
            if (!FindField(INDIRIZZO))
                s.AppendLine("Formato file non valido: " + INDIRIZZO);
            if (!FindField(CAP))
                s.AppendLine("Formato file non valido: " + CAP);
            



           
            if (!FindField(SETTORE))
                s.AppendLine("Formato file non valido: " + SETTORE);



            if (!FindField(DATA_INIZIO))
                s.AppendLine("Formato file non valido: " + DATA_INIZIO);

            if (!FindField(TIPO_PRESTAZIONE))
                s.AppendLine("Formato file non valido: " + TIPO_PRESTAZIONE);
            if (!FindField(QUOTA))
                s.AppendLine("Formato file non valido: " + QUOTA);
             if (!FindField(NOME_REFERENTE)) 
                s.AppendLine("Formato file non valido: " + NOME_REFERENTE);
            if (!FindField(COGNOME_REFERENTE))
                s.AppendLine("Formato file non valido: " + COGNOME_REFERENTE);


            

            if (!string.IsNullOrEmpty(s.ToString()))
                throw new Exception(s.ToString());

        }
    }
}