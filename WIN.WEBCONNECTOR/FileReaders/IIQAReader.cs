using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public interface IIQAReader
    {
        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.IQATrace ReadTrace(string fileName, InputHeader header);
    }
}
