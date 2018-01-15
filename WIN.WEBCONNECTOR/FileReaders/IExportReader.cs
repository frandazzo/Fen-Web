using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public interface IExportReader
    {
        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace ReadExport(string fileName, InputHeader header);
    }
}
