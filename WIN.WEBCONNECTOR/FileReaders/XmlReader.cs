using System;
using System.Collections.Generic;
using System.Text;
using WIN.WEBCONNECTOR.FenealgestServices;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
namespace WIN.WEBCONNECTOR.FileReaders
{
    public class XmlReader : IExportReader
    {
        #region IExportReader Membri di

        public WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace ReadExport(string fileName, InputHeader header)
        {
            return ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace>.Load(fileName);
        }

        #endregion
    }
}