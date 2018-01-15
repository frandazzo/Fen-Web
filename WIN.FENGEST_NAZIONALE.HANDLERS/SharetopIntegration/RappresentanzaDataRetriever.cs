using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class RappresentanzaDataRetriever
    {

          protected IPersistenceFacade _ps;


        public RappresentanzaDataRetriever(IPersistenceFacade ps)
        {
            _ps = ps;
        }



        public IList FindRappresentanzaBySurveyId(int surveyId)
        {
            Query q = _ps.CreateNewQuery("MapperRappresentanza");

            Criteria c = Criteria.Equal("Id_Survey", surveyId.ToString());

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            return l;
        }

        public List<Rappresentanza> FindRappresentanza(int regionId, int provinceId, int year)
        {
            Query q = _ps.CreateNewQuery("MapperRappresentanza");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            if (regionId > 0)
                c.AddCriteria (Criteria.Equal ("Id_Regione", regionId.ToString ()));
            else
                c.AddCriteria(Criteria.IsNull("Id_Regione"));

            if(provinceId > 0)
                c.AddCriteria(Criteria.Equal("Id_provincia", provinceId.ToString()));
            else
                c.AddCriteria(Criteria.IsNull("Id_Provincia"));
            //c.AddCriteria(Criteria.Equal("Anno", year.ToString()));

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            return FilterList(l, year);
        }


        private List<Rappresentanza> FilterList(IList l, int year)
        {
            List<Rappresentanza> result = new List<Rappresentanza>();
            //per prima cosa verifico se cè l'elemento per l'anno selezioanto
            foreach (Rappresentanza item in l)
            {
                if (item.BaseData.Anno == year)
                    result.Add(item);
            }

            if (result.Count > 0)
                return result;

            //se non cè creo una lista con tutti gli elementi che hanno l'anno < dello year
            List<Rappresentanza> d = new List<Rappresentanza>();
            foreach (Rappresentanza item in l)
            {
                if (item.BaseData.Anno < year)
                    d.Add(item);
            }

            //adesso che ho la lista devo prendere tutti gli elementi con l'anno maggiore
            if (d.Count == 0)
                return result;


            int maxYear = (d[0] as Rappresentanza).BaseData.Anno;

            foreach (Rappresentanza item in d)
            {
                if (maxYear < item.BaseData.Anno)
                {
                    maxYear = item.BaseData.Anno;

                }

            }

            //non mi rimane che tirare fuori tutti gli elementi per l'anno considerato

            foreach (Rappresentanza item in d)
            {
                if (maxYear == item.BaseData.Anno)
                    result.Add(item);
            }

            return result;
        }


    }
}
