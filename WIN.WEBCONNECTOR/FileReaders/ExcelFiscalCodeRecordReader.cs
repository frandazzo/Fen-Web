using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelFiscalCodeRecordReader : WIN.BASEREUSE.BaseExcelReader
    {
        private const string FISCALE = "FISCALE";


           public override void ParseImportFile()
        {

            StringBuilder s = new StringBuilder();

              
           
            if (!FindField(FISCALE)) 
                s.AppendLine("Formato file non valido: manca l'intestazione " + FISCALE);
            

            if (!string.IsNullOrEmpty (s.ToString ()))
                throw new Exception(s.ToString());
    
        }

           public ExcelFiscalCodeRecordReader(string fileName)
        {
            base.m_Filename = fileName;
        }
    }
}
