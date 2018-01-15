using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class SendFileImportReaderFactory
    {

        public static IIQAReader GetIQAReader(string fileName)
        {
            FileInfo f = new FileInfo(fileName);


            string ext = f.Extension;


            if (ext.ToUpper() == ".XML")
                return new XmlIQAReader();

            return new ExcelIQAReader(fileName);

        }

        public static ILiberiReader GetLiberiReader(string fileName)
        {
            FileInfo f = new FileInfo(fileName);


            string ext = f.Extension;


            if (ext.ToUpper() == ".XML")
                return new XmlLiberiReader();

            return new ExcelLiberiReader(fileName);

        }

        public static IInpsReader GetInpsReader(string fileName,bool readPath)
        {

            //se  il readPath è true  sto analizzando un percorso per i file inps

            if (!readPath)
            {
                FileInfo f = new FileInfo(fileName);


                string ext = f.Extension;


                if (ext.ToUpper() == ".XML")
                    return new XmlInpsReader();

                return new ExcelInpsReader(fileName);
            }
            

            //qui devo creare il lettore di più file inps
            return new ExcelInpsPathReader (fileName );
        }

    }
}
