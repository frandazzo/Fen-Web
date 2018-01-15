using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class WorkerQueryResult
    {
        public string Message = "";
        public IList<Worker> Workers = new List<Worker>();
    }
}
