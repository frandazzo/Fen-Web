using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class StrutturaDataRetriever
    {
         protected IPersistenceFacade _ps;


         public StrutturaDataRetriever(IPersistenceFacade ps)
        {
            _ps = ps;
        }



        public Struttura FindStrutturaBySurveyId(int surveyId)
        {
            Query q = _ps.CreateNewQuery("MapperStruttura");

            Criteria c = Criteria.Equal("Id_Survey", surveyId.ToString());

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            if (l.Count == 1)
                return l[0] as Struttura;

            return null;
        }

        public Struttura FindStruttura(int regionId, int provinceId, int year)
        {
            Query q = _ps.CreateNewQuery("MapperStruttura");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria (Criteria.Equal ("Id_Regione", regionId.ToString ()));
            if (provinceId > 0)
                c.AddCriteria(Criteria.Equal("Id_provincia", provinceId.ToString()));
            else
                c.AddCriteria(Criteria.IsNull("Id_Provincia"));
            //if (year > 0)
            //    c.AddCriteria(Criteria.Equal("Anno", year.ToString()));

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            if (l.Count == 1)
                return l[0] as Struttura;

            return null;
        }
    }
}
