using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class SubscriptionHandler
    {
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;
        private Subscription _subscription;
        private bool _found;

        public SubscriptionHandler(IPersistenceFacade f, GeoLocationFacade g)
        {
            _persistence = f;
            _geo = g;
        }

        public Subscription Subscription
        {
            get { return _subscription; }
        }

        public bool Found
        {
            get { return _found; }
        }


        public void LoadUniqueSubscription(Subscription subscription,Worker worker)
        {
            Query q = _persistence.CreateNewQuery("MapperSubscription");

            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria(Criteria.Equal("Id_Provincia", subscription.ParentExport.Province.Id.ToString()));
            c.AddCriteria(Criteria.Equal("Id_Lavoratore", worker.Id.ToString()));
            c.AddCriteria(Criteria.MatchesEqual("Settore", subscription.Sector, _persistence.DBType));
            c.AddCriteria(Criteria.DateEqual("DataInizio", subscription.Period.InitialDate, _persistence.DBType));
            c.AddCriteria(Criteria.DateEqual("DataFine", subscription.Period.EndDate, _persistence.DBType));
            q.AddWhereClause(c);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                _found = true;
                _subscription = w[0] as Subscription;
            }
            else
            {
                _subscription = null;
                _found  = false;
            }

        }


        public IList<Subscription> LoadSubscriptionsByWorker(Worker worker)
        {
            Query q = _persistence.CreateNewQuery("MapperSubscription");

            q.AddWhereClause(Criteria.Equal("Id_Lavoratore", worker.Id.ToString()));

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("DataInizio", true));


            q.AddOrderByClause(orderclause);


            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList < Subscription > result =  new List<Subscription>();

                foreach (Subscription  item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Subscription>();
            }

        }


        public IList<Worker> FindWorkersByAzienda(string azienda, int provinceId)
        {
            IList<Worker> result = new List<Worker>();
            //cerco tutti i lavoratori che sono stati iscritti all'azienda almeno una volta
            //presso il territorio richiedente
            IList<Subscription> subs = LoadSubscriptionsByAzienda(azienda, provinceId);

            //adesso per le sottoscrizioni trovate estraggo gli id per trovare il worker
            IList<string> ids = Extractids(subs);


            //adesso per ogni id recupero il worker
            foreach (string item in ids)
            {
                Worker w = RetrieveWorkerByFiscalCode(item);
                //non più necessario
                //w.Subscriptions = RetrieveSubscriptions(subs, item);
                result.Add(w);
            }
            return result;

        }

        private Worker RetrieveWorkerByFiscalCode(string item)
        {
            WorkerHandler h = new WorkerHandler(_persistence, _geo);
            h.LoadByFiscalCode(item);


            if (h.Found)
            {
                h.LoadWorkerWithSusbscriptionsAndDocuments(null);
                return h.CurrentWorker;
            }
                

            return null;
        }

        private IList<DOMAIN.Workers.Subscription> RetrieveSubscriptions(IList<DOMAIN.Workers.Subscription> subs, string fiscalCode)
        {
            IList<DOMAIN.Workers.Subscription>  r = new List<DOMAIN.Workers.Subscription> ();
            foreach (Subscription item in subs)
            {
                if (item.Worker.CodiceFiscale.Equals(fiscalCode))
                    r.Add(item);
            }

                return r;
        }

        private IList<string> Extractids(IList<DOMAIN.Workers.Subscription> subs)
        {
            Hashtable h = new Hashtable();
            foreach (Subscription item in subs)
            {
                if (!h.ContainsKey(item.Worker.CodiceFiscale))
                    h.Add(item.Worker.CodiceFiscale, "");
            }

            IList<string> result = new List<string>();
            IDictionaryEnumerator enumw = h.GetEnumerator();
            while (enumw.MoveNext())
	        {
                string key = enumw.Key as string;
                if (key != null)
	                result.Add(key);
	        }

            return result;
        }

        
        private IList<Subscription> LoadSubscriptionsByAzienda(string azienda, int provinceId)
        {
            //voglio ricercare le subscription edili  per una data azienda in un dato territorio
            Query q = _persistence.CreateNewQuery("MapperSubscription");
            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            c.AddCriteria(Criteria.Equal("Id_Provincia", provinceId.ToString()));
            c.AddCriteria(Criteria.MatchesEqual("Azienda", azienda, _persistence.DBType));
            c.AddCriteria(Criteria.MatchesEqual("Settore", "EDILE", _persistence.DBType));
            q.AddWhereClause(c);

            string aa = q.CreateQuery(_persistence);
            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Subscription> result = new List<Subscription>();

                foreach (Subscription item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Subscription>();
            }
        }

        //questo metodo è utilizzato per calcolare le subscriptioin per i non iscritti dove non ho gli id 
        //del lavoratore proveniente dalla tabella dei lavoratori ma da quella dei liberi
        public IList<Subscription> LoadSubscriptionsByWorker(string fiscalCode)
        {

            //faccio una query sul codice fiscale nella tabella dei lavoratori
            Query q1 = _persistence.CreateNewQuery("MapperWorker");
            q1.AddWhereClause(Criteria.MatchesEqual("CodiceFiscale", fiscalCode, _persistence.DBType));

            IList l = q1.Execute(_persistence);
            if (l.Count != 1)
            {
                return new List<Subscription>();
            }

            Query q = _persistence.CreateNewQuery("MapperSubscription");


            //AbstractBoolCriteria c1 = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
          

            //SubQueryCriteria subqry = new SubQueryCriteria("Lavoratori", "Id", true);
            //subqry.AddCriteria(Criteria.MatchesEqual("CodiceFiscale", fiscalCode, _persistence.DBType));

            //c1.AddCriteria(Criteria.INClause("Id_Lavoratore", subqry));

            Worker ww = l[0] as Worker;
            q.AddWhereClause(Criteria.Equal("Id_Lavoratore", ww.Id.ToString()));



            string query = q.CreateQuery(_persistence);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Subscription> result = new List<Subscription>();

                foreach (Subscription item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Subscription>();
            }

        }



        public IList<Subscription> LoadSubscriptionsByWorker(int workerId)
        {
            Query q = _persistence.CreateNewQuery("MapperSubscription");

            q.AddWhereClause(Criteria.Equal("Id_Lavoratore", workerId.ToString()));

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("DataInizio", true));


            q.AddOrderByClause(orderclause);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Subscription> result = new List<Subscription>();

                foreach (Subscription item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Subscription>();
            }

        }




        public IList<Subscription> LoadSubscriptionsByWorker(int workerId, int regionId)
        {
            Query q = _persistence.CreateNewQuery("MapperSubscription");

            AbstractBoolCriteria AndOP = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            AndOP.AddCriteria(Criteria.Equal("Id_Lavoratore", workerId.ToString()));
            AndOP.AddCriteria(Criteria.Equal("Id_Regione", regionId.ToString()));


            q.AddWhereClause(AndOP);

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("DataInizio", true));


            q.AddOrderByClause(orderclause);


            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Subscription> result = new List<Subscription>();

                foreach (Subscription item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Subscription>();
            }

        }


        public IList<Worker> FindWorkersByAzienda(string azienda)
        {
            IList<Worker> result = new List<Worker>();
            //cerco tutti i lavoratori che sono stati iscritti all'azienda almeno una volta
            //presso il territorio richiedente
            IList<Subscription> subs = LoadSubscriptionsByAzienda(azienda);

            //adesso per le sottoscrizioni trovate estraggo gli id per trovare il worker
            IList<string> ids = Extractids(subs);


            //adesso per ogni id recupero il worker
            foreach (string item in ids)
            {
                Worker w = RetrieveWorkerByFiscalCode(item);
                //non più necessario
                //w.Subscriptions = RetrieveSubscriptions(subs, item);
                result.Add(w);
            }
            return result;
        }

        private IList<DOMAIN.Workers.Subscription> LoadSubscriptionsByAzienda(string azienda)
        {
            if (string.IsNullOrEmpty(azienda))
                return new List<Subscription>();

            if (azienda.Length < 5)
                return new List<Subscription>();

            Query q = _persistence.CreateNewQuery("MapperSubscription");
            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            c.AddCriteria(Criteria.Matches("Azienda", "%" + azienda, _persistence.DBType));
            c.AddCriteria(Criteria.MatchesEqual("Settore", "EDILE", _persistence.DBType));
            q.AddWhereClause(c);

            string aa = q.CreateQuery(_persistence);
            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Subscription> result = new List<Subscription>();

                foreach (Subscription item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Subscription>();
            }
        }
    }
}
