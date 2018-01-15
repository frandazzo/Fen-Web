using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using System.Diagnostics;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class XmlIQAReader : IIQAReader
    {
        public FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.IQATrace ReadTrace(string fileName, InputHeader header)
        {
            Trace.WriteLine("Iniziato recupero dati dal file xml");
            FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.IQATrace i =  ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.IQATrace>.Load(fileName);
            Trace.WriteLine("Termine recupero dati dal file xml");
            return i;
        }
    }
}
