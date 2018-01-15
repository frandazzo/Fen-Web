using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public interface IWorkerValidator
    {
        ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker, ExportTrace trace);
        ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker);
        ValidationResult Validate(LiberoDTO worker, IGeoElementChecker checker, int rowNumber);
        
    }
}
