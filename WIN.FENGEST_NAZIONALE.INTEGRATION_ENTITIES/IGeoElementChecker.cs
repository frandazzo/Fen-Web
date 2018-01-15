using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
   public  interface IGeoElementChecker
    {
       bool ExistRegion(string regionName);
       bool ExistProvince(string provinceName);
       bool ExistComune(string nomeComune);
       string GetComuneByFiscalCode(string fiscalCode);
       string CheckFiscalCode(string fiscalCode);
    }
}
