using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class CongressoTerritorialeDataRetriever
    {


          protected IPersistenceFacade _ps;


        public CongressoTerritorialeDataRetriever(IPersistenceFacade ps)
        {
            _ps = ps;
        }

        public List<CongressoTerritoriale> FindCongressiTerritoriali()
        {
            Query q = _ps.CreateNewQuery("MapperCongressoTerritoriale");
            IList l = q.Execute(_ps);
            List<CongressoTerritoriale> result = new List<CongressoTerritoriale>();

            foreach (CongressoTerritoriale item in l)
            {
                result.Add(item);
            }

            return result;

        }

        public CongressoTerritoriale FindCongressoTerritorialeBySurveyId(int surveyId)
        {
            Query q = _ps.CreateNewQuery("MapperCongressoTerritoriale");

            Criteria c = Criteria.Equal("Id_Survey", surveyId.ToString());

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            if (l.Count == 1)
                return l[0] as CongressoTerritoriale;

            return null;
        }

        public CongressoTerritoriale FindCongressoTerritoriale(int regionId, int provinceId, int year)
        {
            Query q = _ps.CreateNewQuery("MapperCongressoTerritoriale");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria (Criteria.Equal ("Id_Regione", regionId.ToString ()));
            c.AddCriteria(Criteria.Equal("Id_provincia", provinceId.ToString()));
            //c.AddCriteria(Criteria.Equal("Anno", year.ToString()));

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            return FilterList(l, year);
        }
        private CongressoTerritoriale FilterList(IList l, int year)
        {

            //per prima cosa verifico se cè l'elemento per l'anno selezioanto
            foreach (CongressoTerritoriale item in l)
            {
                if (item.BaseData.Anno == year)
                    return item;
            }

            //se non cè creo una lista con tutti gli elementi che hanno l'anno < dello year
            ArrayList d = new ArrayList();
            foreach (CongressoTerritoriale item in l)
            {
                if (item.BaseData.Anno < year)
                    d.Add(item);
            }

            //adesso che ho la lista devo prendere quello con l'anno maggiore
            if (d.Count == 0)
                return null;


            int maxYear = (d[0] as CongressoTerritoriale).BaseData.Anno;
            CongressoTerritoriale result = d[0] as CongressoTerritoriale;
            foreach (CongressoTerritoriale item in d)
            {
                if (maxYear < item.BaseData.Anno)
                {
                    maxYear = item.BaseData.Anno;
                    result = item;
                }

            }


            return result;

        }

    }
}
