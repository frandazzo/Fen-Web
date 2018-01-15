using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class LiberiTraceSerializationHandler
    {
        public static void Serialize(LiberiTrace trace, string path)
        {
            Serializzation.Serializer<LiberiTrace>.Save(trace, path);
        }


        public static LiberiTrace Deserialize(string path)
        {
            return Serializzation.Serializer<LiberiTrace>.Load(path);
        }
    }
}
