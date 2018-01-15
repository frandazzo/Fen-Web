using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using System.Diagnostics;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class XmlLiberiReader : ILiberiReader
    {
        public FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace ReadTrace(string fileName, InputHeader header)
        {
            Trace.WriteLine("Iniziato recupero dati dal file xml");
            FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace i =  ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace>.Load(fileName);
            Trace.WriteLine("Termine recupero dati dal file xml");
            return i;
        }
    }
}