using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class FlowReporting
    {
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;


        public FlowReporting(IPersistenceFacade f, GeoLocationFacade g)
        {
            _persistence = f;
            _geo = g;
        }

        public IList<WorkerFlow> LoadFlows(int regionId, int provinceId, int year, int subsequentYears)
        {
            if (regionId == -1)
                return new List<WorkerFlow>();
            //devo eseguire una query di qwuesto tipo
            //select * from Subscriptions where id_lavoratore in 
                        //(select distinct id_lavoratore from subscriptions where regId = 4 and provId = 6 and anno = 2015)
            //and anno >= 2015


            Query q = _persistence.CreateNewQuery("MapperSubscription");

            //definisco la clausola wherre della subquery: la select fistinct ecc...
             AbstractBoolCriteria subQueryWhereClause = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            subQueryWhereClause.AddCriteria(Criteria.Equal("Anno", year.ToString()));
            
            if (provinceId != -1)
                subQueryWhereClause.AddCriteria(Criteria.Equal("Id_Provincia", provinceId.ToString()));
            else
                subQueryWhereClause.AddCriteria(Criteria.Equal("Id_Regione", regionId.ToString()));
            
            //definisco la query interna aggiungendogli la wheeree clause
            SubQueryCriteria subQuery = new SubQueryCriteria("Iscrizioni", "Id_Lavoratore",true);
            subQuery.AddCriteria(subQueryWhereClause);



            //definisco la where clause della query principale nella quale includo anche la in clause
            AbstractBoolCriteria mainWhereClause = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
             //definisco la clausola in
           
            mainWhereClause.AddCriteria(Criteria.INClause("Id_Lavoratore",subQuery));
            if (subsequentYears == -1)
                mainWhereClause.AddCriteria(Criteria.GreaterEqualThan("Anno", year.ToString()));
            else if (subsequentYears == 0)
                mainWhereClause.AddCriteria(Criteria.Equal("Anno", year.ToString()));
            else
            {
                mainWhereClause.AddCriteria(Criteria.GreaterEqualThan("Anno", year.ToString()));
                mainWhereClause.AddCriteria(Criteria.LessEqualThan("Anno", year.ToString()));
            }
            
                
            q.AddWhereClause(mainWhereClause);

             IList w = q.Execute(_persistence);

             if (w.Count == 0)
                 return new List<WorkerFlow>();

            
            WorkerFlowFactory fac = new WorkerFlowFactory();
            if (provinceId == -1)
                return fac.constructRegionalFlow(regionId, w);
            else
                return fac.constructProvincialFlow(regionId, provinceId, w);

         


           

           

        }


    }
}
