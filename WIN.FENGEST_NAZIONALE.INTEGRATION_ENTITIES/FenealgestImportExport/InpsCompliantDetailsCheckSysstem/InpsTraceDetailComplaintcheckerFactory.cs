using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsCompliantDetailsCheckSysstem
{
    public class InpsTraceDetailComplaintcheckerFactory
    {
        public static AbstractChain CreateChain()
        {
            return new FiscalCodeCheckerChain(new NameSurnameDateOfBirthCheckerChain(new NameSurnameCheckerChain()));
        }
    }
}
