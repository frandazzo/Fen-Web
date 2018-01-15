using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class RappresentanzaDataHandler
    {

          protected IPersistenceFacade _ps;
          protected RappresentanzaDataRetriever _ret;

         public RappresentanzaDataHandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new RappresentanzaDataRetriever(_ps);
        }


         public void Save(Rappresentanza[] c)
        {
            if (c.Length == 0)
                return;


            IList r = _ret.FindRappresentanzaBySurveyId(c[0].BaseData.SurveyId);
            foreach (Rappresentanza item in r)
            {
                _ps.DeleteObject(item.GetType().Name, item);
            }


            foreach (Rappresentanza item in c)
            {
                _ps.InsertObject(item.GetType().Name, item);
            }
           

        }


    }
}
