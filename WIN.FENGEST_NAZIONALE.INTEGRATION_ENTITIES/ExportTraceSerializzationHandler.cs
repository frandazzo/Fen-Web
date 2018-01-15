using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class ExportTraceSerializzationHandler
    {
        public static void Serialize(ExportTrace trace, string path)
        {
            Serializzation.Serializer<ExportTrace>.Save(trace, path);
        }


        public static ExportTrace Deserialize(string path)
        {
            return Serializzation.Serializer<ExportTrace>.Load(path);
        }

    }
}
