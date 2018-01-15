using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration
{
    [Serializable ]
    public class SharetopBaseData
    {
        public SharetopBaseData()
        {
            OperationResult = new OperationResult();
        }

        public int SurveyId { get; set; }
        public Regione Regione { get; set; }
        public Provincia Provincia { get; set; }
        public int Anno { get; set; }


        public OperationResult  OperationResult { get; set; }
    }
}
