using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class DatiCassaEdileDataRetriever
    {
        private TECHNICAL.PERSISTENCE.IPersistenceFacade _ps;

        public DatiCassaEdileDataRetriever(TECHNICAL.PERSISTENCE.IPersistenceFacade _ps)
        {
            // TODO: Complete member initialization
            this._ps = _ps;
        }
        

        public IList FindDatiCassaEdileByAnno(int anno)
        {
            Query q = _ps.CreateNewQuery("MapperDatiCassaEdile");

            if (anno != -1)
            {
                Criteria c = Criteria.Equal("Anno", anno.ToString());

                q.AddWhereClause(c);
            }
            

            IList l = q.Execute(_ps);
            return l;
        }
    }
}
