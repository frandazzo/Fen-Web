using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class CongressoTerritorialeDataHandler
    {
         protected IPersistenceFacade _ps;
         protected CongressoTerritorialeDataRetriever _ret;

        public CongressoTerritorialeDataHandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new CongressoTerritorialeDataRetriever(_ps);
        }


        public void Save(CongressoTerritoriale c)
        {
            CongressoTerritoriale r = _ret.FindCongressoTerritorialeBySurveyId(c.BaseData.SurveyId);
            if (r != null)
                _ps.DeleteObject(c.GetType ().Name , r);


            _ps.InsertObject(c.GetType().Name, c);

        }
    }
}
