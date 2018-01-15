using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class CongressoRegionaleDataRetriever
    {
         protected IPersistenceFacade _ps;
         

        public CongressoRegionaleDataRetriever(IPersistenceFacade ps)
        {
            _ps = ps;
        }


        public List<CongressoRegionale> FindCongressiRegionali()
        {
            Query q = _ps.CreateNewQuery("MapperCongressoRegionale");
            IList l = q.Execute(_ps);
            List<CongressoRegionale> result = new List<CongressoRegionale>();

            foreach (CongressoRegionale item in l)
            {
                result.Add(item);
            }

            return result;

        }
        public CongressoRegionale FindCongressoRegionaleBySurveyId(int surveyId)
        {
            Query q = _ps.CreateNewQuery("MapperCongressoRegionale");

            Criteria c = Criteria.Equal("Id_Survey", surveyId.ToString());

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            if (l.Count == 1)
                return l[0] as CongressoRegionale;

            return null;
        }

        public CongressoRegionale FindCongressoRegionale(int regionId, int year)
        {
            Query q = _ps.CreateNewQuery("MapperCongressoRegionale");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria (Criteria.Equal ("Id_Regione", regionId.ToString ()));
            
            c.AddCriteria(Criteria .IsNull ("Id_Provincia"));
            //c.AddCriteria(Criteria.Equal("Anno", year.ToString()));

            q.AddWhereClause(c);

            //devo cercare tra tutti i congressi della regione quello per cui la differenza tra l'anno di
            //compilaizone e l'anno indicato sia minima e positiva



            IList l = q.Execute(_ps);

            return FilterList(l, year);

           
        }

        private CongressoRegionale FilterList(IList l, int year)
        {
            
            //per prima cosa verifico se cè l'elemento per l'anno selezioanto
            foreach (CongressoRegionale item in l)
            {
                if (item.BaseData.Anno == year)
                    return item;
            }

            //se non cè creo una lista con tutti gli elementi che hanno l'anno < dello year
            ArrayList d = new ArrayList();
            foreach (CongressoRegionale item in l)
            {
                if (item.BaseData.Anno < year)
                    d.Add( item);
            }

            //adesso che ho la lista devo prendere quello con l'anno maggiore
            if (d.Count == 0)
                return null;


            int maxYear = (d[0] as CongressoRegionale).BaseData.Anno;
            CongressoRegionale result = d[0] as CongressoRegionale;
            foreach (CongressoRegionale item in d)
            {
                if (maxYear < item.BaseData.Anno){
                    maxYear = item.BaseData.Anno;
                    result = item;
                }
                    
            }


            return result;

        }


    }
}
