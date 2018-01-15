using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class WorkerHandler
    {

        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;

        private bool _found;
        private Worker _worker;

        public WorkerHandler(IPersistenceFacade f, GeoLocationFacade g)
        {
            _persistence = f;
            _geo = g;
        }

        public Worker CurrentWorker
        {
            get { return _worker; }
        }

        public bool Found
        {
            get { return _found ; }
        }

        public void LoadByFiscalCode(string fiscalCode)
        {
            //per prima cosa verifica la correttezza del codice fiscale

            DatiFiscali d;

            try
            {
                //se il codice fiscale non è corretto non  procedo alla ricerca
                d = _geo.CalcolaDatiFiscali(fiscalCode);
            }
            catch (Exception)
            {
                _found = false;
                _worker = null;
                return;
            }


            Query q = _persistence.CreateNewQuery("MapperWorker");
            AbstractBoolCriteria c = Criteria.MatchesEqual("CodiceFiscale", fiscalCode, _persistence.DBType);


            q.AddWhereClause(c);

            IList w = q.Execute(_persistence) ;

            if (w.Count > 0)
            {
                _found = true;
                _worker = w[0] as Worker ;
            }
            else
            {
                _worker = null;
                _found = false;
            }

        }






        public WorkerQueryResult SearchWorkers(WorkerQueryParameters parameters, bool completeMaterializzation)
        {
            //per prima cosa verifica la correttezza del codice fiscale
            IList<Worker> list = new List<Worker>();
            WorkerQueryResult result = new WorkerQueryResult();

            PaginationQueryHandler q = _persistence.CreateNewPaginationQuery("MapperWorker", parameters.MaxResult, parameters.MaxResult);

            q.SetTable("lavoratori");

            
            //clausola where
            AbstractBoolCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);


            if (!string.IsNullOrEmpty(parameters.Name))
                c.AddCriteria(Criteria.Matches("Nome",parameters.Name , _persistence.DBType));

            if (!string.IsNullOrEmpty(parameters.Surname))
                c.AddCriteria(Criteria.Matches("Cognome", parameters.Surname , _persistence.DBType));

            if (parameters.CheckDate)
                c.AddCriteria(Criteria.DateEqual("DataNascita", parameters.BirthDate, _persistence.DBType));

            if (parameters.Province != null)
                if (parameters.Province.Id != -1)
                    c.AddCriteria(Criteria.Equal("Id_Provincia_Residenza", parameters.Province.Id.ToString()));

            if (parameters.NationalityObject.Id != -1)
                c.AddCriteria(Criteria.Equal("Id_Nazione", parameters.NationalityObject.Id.ToString()));

            if (parameters.LivingPlaceObject.Id != -1)
                c.AddCriteria(Criteria.Equal("Id_Comune_Residenza", parameters.LivingPlaceObject.Id.ToString()));


            if ((!string.IsNullOrEmpty (parameters.CompanyFiscalCode )) || (!string.IsNullOrEmpty(parameters.CompanyDescription )))
            {
                AbstractBoolCriteria c1 = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

                if (!string.IsNullOrEmpty (parameters.CompanyFiscalCode ))
                {
                    c1.AddCriteria (Criteria.Matches("Piva",parameters.CompanyFiscalCode, _persistence.DBType));
                }

                if (!string.IsNullOrEmpty (parameters.CompanyDescription ))
                {
                    c1.AddCriteria (Criteria.Matches ("Azienda",parameters.CompanyDescription, _persistence.DBType));
                }

                SubQueryCriteria subqry = new SubQueryCriteria("Iscrizioni", "Id_Lavoratore", true);
                subqry.AddCriteria(c1);


                 c.AddCriteria(Criteria.INClause("ID", subqry));


            }


            
            q.AddWhereClause(c);

            //clausola ordnimaneto
            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("Cognome", true));
            q.AddOrderByClause(orderclause);


            //esecuzione query
            IList w = q.ExecuteFirstQuery(_persistence);


            //trasferisco i dati dall'arraylist alla lista in modo brutale
            //calcolandone eventuali dati di iscrizioni e documenti
            if (w.Count > 0)
            {
                foreach (Worker item in w)
                {
                    if (completeMaterializzation)
                        LoadWorkerWithSusbscriptionsAndDocuments(parameters.RegionObject, item);
                    list.Add(item);
                }


                result.Workers = list;
                result.Message = q.FoundElementMessage;
            }
            else
            {
                result.Message = "Nessun elemento trovato!";
            }
            return result;

            
        }






















        public void LoadById(int id)
        {
            _worker = _persistence.GetObject("Worker", id) as Worker;


            if (_worker != null)
            {
                _found = true;
                return;
            }
            _found = false;
        }


        public void LoadWorkerWithSusbscriptionsAndDocuments(Regione regione )
        {
            if (_worker == null)
                return;
            IList<Subscription> subs = GetSubscriptions( regione, _worker .Id );
            IList<Document> docs = GetDocuments( regione,_worker .Id );
            _worker.Documents = docs;
            _worker.Subscriptions = subs;

        }

        public void LoadWorkerWithSusbscriptionsAndDocuments(Regione regione, Worker worker)
        {
            if (worker == null)
                return;
            IList<Subscription> subs = GetSubscriptions(regione, worker.Id);
            IList<Document> docs = GetDocuments(regione, worker.Id);
            worker.Documents = docs;
            worker.Subscriptions = subs;

        }


        private IList<Document> GetDocuments(Regione regione, int workerId)
        {
            bool findByRegion = true;

            if (regione == null)
                findByRegion = false;
            else
            {
                if (regione.Id == -1)
                    findByRegion = false;
            }

            IList<Document> subs;
            //carico le subscription
            DocumentHandler h = new DocumentHandler(_persistence );

            if (findByRegion)
                subs = h.LoadDocumentsByWorker(workerId, regione.Id);
            else
                subs = h.LoadDocumentsByWorker(workerId);

           

            return subs;

        }

        private IList<Subscription> GetSubscriptions(Regione regione, int workerId)
        {
            bool findByRegion = true;

            if (regione == null)
                findByRegion = false;
            else
            {
                if (regione.Id == -1)
                    findByRegion = false;
            }

            IList<Subscription> subs;
            //carico le subscription
            SubscriptionHandler h = new SubscriptionHandler(_persistence , _geo);

            if (findByRegion)
                subs = h.LoadSubscriptionsByWorker( workerId, regione.Id);
            else
                subs = h.LoadSubscriptionsByWorker(workerId);

           

            return subs;

        }

    }
}
