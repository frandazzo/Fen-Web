using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class WorkerFlow
    {
        public string Region { get; set; }
        public string Province { get; set; }
        
        public int MigratedWorkers { get; set; }
        public string MigratedWorkersPercentage { get; set; }
       

    }
}
