using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class RappresentanteDataRetriever
    {

         protected IPersistenceFacade _ps;


         public RappresentanteDataRetriever(IPersistenceFacade ps)
        {
            _ps = ps;
        }



         public IList FindRappresentanteBySurveyId(int surveyId)
        {
            Query q = _ps.CreateNewQuery("MapperRappresentante");

            Criteria c = Criteria.Equal("Id_Survey", surveyId.ToString());

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

           

            return l;
        }

         public List<Rappresentante> FindRappresentante(int regionId, int provinceId, int year)
        {
            Query q = _ps.CreateNewQuery("MapperRappresentante");

            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            if (regionId > 0)
                c.AddCriteria (Criteria.Equal ("Id_Regione", regionId.ToString ()));
               else
                c.AddCriteria(Criteria.IsNull("Id_Regione"));


            if(provinceId > 0)
                c.AddCriteria(Criteria.Equal("Id_provincia", provinceId.ToString()));
            else
                c.AddCriteria (Criteria.IsNull("Id_Provincia"));
            //c.AddCriteria(Criteria.Equal("Anno", year.ToString()));

            q.AddWhereClause(c);

            IList l = q.Execute(_ps);

            return FilterList(l, year);
        }

         private List<Rappresentante> FilterList(IList l, int year)
         {
             List<Rappresentante> result = new List<Rappresentante>();
             //per prima cosa verifico se cè l'elemento per l'anno selezioanto
             foreach (Rappresentante item in l)
             {
                 if (item.BaseData.Anno == year)
                     result.Add( item);
             }

             if (result.Count > 0)
                 return result;

             //se non cè creo una lista con tutti gli elementi che hanno l'anno < dello year
             List<Rappresentante> d = new List<Rappresentante>();
             foreach (Rappresentante item in l)
             {
                 if (item.BaseData.Anno < year)
                     d.Add(item);
             }

             //adesso che ho la lista devo prendere tutti gli elementi con l'anno maggiore
             if (d.Count == 0)
                 return result;


             int maxYear = (d[0] as Rappresentante).BaseData.Anno;

             foreach (Rappresentante item in d)
             {
                 if (maxYear < item.BaseData.Anno)
                 {
                     maxYear = item.BaseData.Anno;
                     
                 }

             }

             //non mi rimane che tirare fuori tutti gli elementi per l'anno considerato

             foreach (Rappresentante item in d)
             {
                 if (maxYear == item.BaseData.Anno)
                     result.Add(item);
             }

             return result;
         }

    }
}
