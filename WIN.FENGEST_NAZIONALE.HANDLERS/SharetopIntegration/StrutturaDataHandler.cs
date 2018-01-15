using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class StrutturaDataHandler
    {
         protected IPersistenceFacade _ps;
          protected StrutturaDataRetriever _ret;

          public StrutturaDataHandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new StrutturaDataRetriever(_ps);
        }


         public void Save(Struttura c)
        {
            Struttura r = _ret.FindStrutturaBySurveyId(c.BaseData.SurveyId);
            if (r != null)
                _ps.DeleteObject(c.GetType ().Name , r);


            _ps.InsertObject(c.GetType().Name, c);

        }
    }
}
