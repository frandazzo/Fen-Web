using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class DatiCassaEdileDataHandler
    {
          protected IPersistenceFacade _ps;
          protected DatiCassaEdileDataRetriever _ret;

         public DatiCassaEdileDataHandler(IPersistenceFacade ps)
        {
            _ps = ps;
            _ret = new DatiCassaEdileDataRetriever(_ps);
        }


         public void Save(DatiCassaEdile[] c)
        {
            if (c.Length > 0)
            {
                _ps.ExecuteNonQuery(string.Format("delete from sharetop_daticasseedili where Id_Survey = {0}", c[0].BaseData.SurveyId));

                foreach (DatiCassaEdile item in c)
                {
                    _ps.InsertObject(item.GetType().Name, item);
                }

                
            }
            

        }
    }
}
