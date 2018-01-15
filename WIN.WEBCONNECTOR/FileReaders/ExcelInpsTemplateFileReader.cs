using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelInpsTemplateFileReader: WIN.BASEREUSE.BaseExcelReader
    {


    //    Friend Const CognomeNome As String = "Cognome/Nome"
    //Friend Const Fiscale As String = "Codice Fiscale"
    //Friend Const DataNascita As String = "Data di Nascita"
    //Friend Const Importo As String = "Importo Pagamento"
    //Friend Const DataValuta As String = "Data Valuta"
    //Friend Const Domanda As String = "N. Domanda"
    //Friend Const Comune As String = "Comune Res."
    //Friend Const Patronato As String = "Patronato"


        public ExcelInpsTemplateFileReader(string fileName)
        {
            base.m_Filename = fileName;
            base.m_startColumn = 2;
            base.m_startRow = 11;
        }

        private string CognomeNome = "Cognome/Nome";
        private string Fiscale = "Codice Fiscale";
        private string DataNascita = "Data di Nascita";
        private string Importo = "Importo Pagamento";

        private string DataValuta = "Data Valuta";
        private string Domanda = "N. Domanda";
        private string Comune = "Comune Res.";
        private string Patronato = "Patronato";




        public override void ParseImportFile()
        {
            
            StringBuilder s = new StringBuilder();

            if (!FindField(CognomeNome))
                s.AppendLine("Formato file non valido: " + CognomeNome);
            if (!FindField(Fiscale))
                s.AppendLine("Formato file non valido: " + Fiscale);
            if (!FindField(DataNascita)) 
                s.AppendLine("Formato file non valido: " + DataNascita);
            if (!FindField(Importo))
                s.AppendLine("Formato file non valido: " + Importo);

            if (!FindField(DataValuta))
                s.AppendLine("Formato file non valido: " + DataValuta);
            if (!FindField(Domanda))
                s.AppendLine("Formato file non valido: " + Domanda);


            if (!FindField(Comune))
                s.AppendLine("Formato file non valido: " + Comune);
            if (!FindField(Patronato))
                s.AppendLine("Formato file non valido: " + Patronato);
            




            

            if (!string.IsNullOrEmpty(s.ToString()))
                throw new Exception(s.ToString());

        }
    }
}