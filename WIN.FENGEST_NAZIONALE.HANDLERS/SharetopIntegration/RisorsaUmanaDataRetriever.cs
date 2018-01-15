using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class RisorsaUmanaDataRetriever
    {
        private TECHNICAL.PERSISTENCE.IPersistenceFacade _ps;

        public RisorsaUmanaDataRetriever(TECHNICAL.PERSISTENCE.IPersistenceFacade _ps)
        {
            // TODO: Complete member initialization
            this._ps = _ps;
        }
        

        public IList FindRisorseUmaneByAnno(int anno)
        {
            Query q = _ps.CreateNewQuery("MapperRisorsaUmana");

            if (anno != -1)
            {
                Criteria c = Criteria.Equal("Anno", anno.ToString());

                q.AddWhereClause(c);
            }


            IList l = q.Execute(_ps);
            return l;
        }

        public List<RisorsaUmana> FindRisorseUmane(int regionId, int provinceId, int year)
        {
            Query q = _ps.CreateNewQuery("MapperRisorsaUmana");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            if (regionId > 0)
                c.AddCriteria(Criteria.Equal("Id_Regione", regionId.ToString()));
            else
                c.AddCriteria(Criteria.IsNull("Id_Regione"));


            if (provinceId > 0)
                c.AddCriteria(Criteria.Equal("Id_provincia", provinceId.ToString()));
            else
                c.AddCriteria(Criteria.IsNull("Id_Provincia"));
            c.AddCriteria(Criteria.Equal("Anno", year.ToString()));

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            List<RisorsaUmana> rrr = new List<RisorsaUmana>();
            foreach (RisorsaUmana item in l)
            {
                rrr.Add(item);
            }
            return rrr;
        }

        
    }
}
