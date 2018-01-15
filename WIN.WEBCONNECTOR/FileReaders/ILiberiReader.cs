using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public interface ILiberiReader
    {
        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace ReadTrace(string fileName, InputHeader header);
    }
}
