using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class RisorsaUmanaDatahandler
    {
          protected IPersistenceFacade _ps;
         protected RisorsaUmanaDataRetriever _ret;

         public RisorsaUmanaDatahandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new RisorsaUmanaDataRetriever(_ps);
        }


        public void Save(RisorsaUmana[] c)
        {
            if (c.Length > 0)
            {
                _ps.ExecuteNonQuery(string.Format("delete from sharetop_risorseumane where Id_Survey = {0}", c[0].BaseData.SurveyId));

                foreach (RisorsaUmana item in c)
                {
                    _ps.InsertObject(item.GetType().Name, item);
                }


            }

        }
    }
}
