using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExceInpsPathReaderFactory : WIN.BASEREUSE.IBaseExcelReaderFactory 
    {
        public BASEREUSE.BaseExcelReader GetReader(string readerName, string filename)
        {
            return new ExcelInpsTemplateFileReader(filename);
        }
    }
}
