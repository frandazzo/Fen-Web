using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class ValidatorFactory
    {
        public static IWorkerValidator GetValidator(string validatorName)
        {
            if (validatorName == "Feneal")
                return new FenealValidator();

            throw new Exception("Nome validatore sconosciuto");
        }
    }
}
