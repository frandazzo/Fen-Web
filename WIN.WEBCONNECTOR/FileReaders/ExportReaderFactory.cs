using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExportReaderFactory
    {
        public static IExportReader GetReader(string fileName)
        {
            FileInfo f = new FileInfo(fileName);


            string ext = f.Extension;


            if (ext.ToUpper() == ".XML")
                return new XmlReader();

            return new ExcelReader(fileName);

        }
    }
}
