using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class RappresentanteDataHandler
    {
         protected IPersistenceFacade _ps;
          protected RappresentanteDataRetriever _ret;

          public RappresentanteDataHandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new RappresentanteDataRetriever(_ps);
        }


         public void Save(Rappresentante[] c)
        {
            if (c.Length == 0)
                return;
            IList r = _ret.FindRappresentanteBySurveyId(c[0].BaseData.SurveyId);
            foreach (Rappresentante item in r)
            {
                _ps.DeleteObject(item.GetType().Name, item);
            }


            foreach (Rappresentante item in c)
            {
                _ps.InsertObject(item.GetType().Name, item);
            }


           

        }
    }
}
