using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsCompliantDetailsCheckSysstem
{
    public abstract class AbstractChain
    {
        protected AbstractChain _chain = null;

        public AbstractChain(AbstractChain chain)
        {
            _chain = chain;
        }

        public abstract bool AreDatailsCompliant(InpsTraceDetails first, InpsTraceDetails second);
        
        
    }
}
