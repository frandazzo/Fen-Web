using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsCompliantDetailsCheckSysstem
{
    public class FiscalCodeCheckerChain : AbstractChain 
    {
        public FiscalCodeCheckerChain(AbstractChain chain)
            : base(chain)
        { }

        public override bool AreDatailsCompliant(InpsTraceDetails first, InpsTraceDetails second)
        {
            if (first == null || second == null)
                return false;

            //se uno dei due è nullo ritorno che non sono uguali e passo alla prossima
            if (string.IsNullOrEmpty(first.FISCALE) || string.IsNullOrEmpty(second.FISCALE))
            {
                if (_chain != null)
                    return _chain.AreDatailsCompliant(first, second);
                else
                    return false;
            }

            if (first.FISCALE.ToUpper().Trim().Equals(second.FISCALE.ToUpper().Trim()))
                return true;
            else
                return false;


           // return _chain.AreDatailsCompliant(first, second);
        }
    }
}
