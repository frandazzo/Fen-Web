using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    public class SharetopIntegragrationSuperClass : AbstractPersistenceObject
    {
        //dati di base di ogni survey di sharetop
        public SharetopBaseData  BaseData { get; set; }



    }
}
