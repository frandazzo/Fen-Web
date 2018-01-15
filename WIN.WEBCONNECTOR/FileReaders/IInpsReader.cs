using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public interface IInpsReader
    {
        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace ReadTrace(string fileName, InputHeader header);
    }
}
