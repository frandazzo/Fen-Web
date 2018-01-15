using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class DocumentHandler
    {

        private IPersistenceFacade _persistence;
       // private GeoLocationFacade _geo;

        public DocumentHandler(IPersistenceFacade f)
        {
            _persistence = f;
           // _geo = g;
        }


        public IList<Document> LoadDocumentsByWorker(int workerId, int regionId)
        {
            Query q = _persistence.CreateNewQuery("MapperDocument");

            //q.AddWhereClause(Criteria.Equal("Id_Lavoratore", workerId.ToString()));
            //q.AddWhereClause(Criteria.Equal("Id_Regione", regionId.ToString()));


            AbstractBoolCriteria AndOP = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            AndOP.AddCriteria(Criteria.Equal("Id_Lavoratore", workerId.ToString()));
            AndOP.AddCriteria(Criteria.Equal("Id_Regione", regionId.ToString()));


            q.AddWhereClause(AndOP);



            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("DataDocumento", true));


            q.AddOrderByClause(orderclause);



            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Document> result = new List<Document>();

                foreach (Document item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Document>();
            }

        }


        public IList<Document> LoadDocumentsByWorker(int workerId)
        {
            Query q = _persistence.CreateNewQuery("MapperDocument");

            q.AddWhereClause(Criteria.Equal("Id_Lavoratore", workerId.ToString()));


            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("DataDocumento", true));


            q.AddOrderByClause(orderclause);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                IList<Document> result = new List<Document>();

                foreach (Document item in w)
                {
                    result.Add(item);
                }
                return result;
            }
            else
            {
                return new List<Document>();
            }

        }
    }
}
