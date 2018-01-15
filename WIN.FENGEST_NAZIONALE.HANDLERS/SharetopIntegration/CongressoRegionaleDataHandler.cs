using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class CongressoRegionaleDataHandler
    {

         protected IPersistenceFacade _ps;
         protected CongressoRegionaleDataRetriever _ret;

        public CongressoRegionaleDataHandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new CongressoRegionaleDataRetriever(_ps);
        }


        public void Save(CongressoRegionale c)
        {
            CongressoRegionale r = _ret.FindCongressoRegionaleBySurveyId(c.BaseData.SurveyId);
            if (r != null)
                _ps.DeleteObject(c.GetType ().Name , r);


            _ps.InsertObject(c.GetType().Name, c);

        }

    }
}
