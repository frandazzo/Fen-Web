using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class XmlInpsReader : IInpsReader 
    {
        public FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace ReadTrace(string fileName, InputHeader header)
        {
            Trace.WriteLine("Iniziato recupero dati dal file xml");
            FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace i = ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace>.Load(fileName);
            Trace.WriteLine("Termine recupero dati dal file xml");
            return i;
        }
    }
}
