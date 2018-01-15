using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class LiberoHandler
    {
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;

       

        public LiberoHandler(IPersistenceFacade f, GeoLocationFacade g)
        {
            _persistence = f;
            _geo = g;
        }

   

        private IList<Libero> SearchLiberi(string azienda, int provinceId)
        {
            //devo ricercare tra tutti i liberi di un territorio quelli che stanno con una azienda

            IList<Libero> l = new List<Libero>();
            WorkerQueryResult result = new WorkerQueryResult();

            Query q = _persistence.CreateNewQuery("MapperLibero");
     
            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            c.AddCriteria(Criteria.MatchesEqual("CurrentAzienda", azienda, _persistence.DBType));
            c.AddCriteria(Criteria.Equal("Id_ProvinciaFeneal", provinceId.ToString()));
            
            q.AddWhereClause(c);
           
            //esecuzione query
            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                foreach (Libero item in w)
                {
                    l.Add(item);
                }
            }

            return l;     
        }


        public IList<Libero> FindLiberiData(string azienda, int provinceId)
        {
            //adesso recupero la lista dei codici fiscali per cui recuperare le iscrizioni
            SubscriptionHandler h = new SubscriptionHandler(_persistence, _geo);

            IList<Libero> result = this.SearchLiberi(azienda, provinceId);


            foreach (Libero item in result)
            {
                item.Subscriptions = h.LoadSubscriptionsByWorker(item.CodiceFiscale);
            }

            return result;
        }

        


    }
}
