using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration
{
    public class CongressDataRetriever
    {

        protected IPersistenceFacade _ps;


        public CongressDataRetriever(IPersistenceFacade ps)
        {
            _ps = ps;
        }
        private CongressoRegionaleDataRetriever cregRet;
        private CongressoTerritorialeDataRetriever cterrRet;

        public IList<CongressPresence> FindCongressData(string name, string surname)
        {
            cregRet = new CongressoRegionaleDataRetriever(_ps);
            cterrRet = new CongressoTerritorialeDataRetriever(_ps);


            IList<CongressPresence> result = new List<CongressPresence>();

            IList<CongressoTerritoriale> ters = cterrRet.FindCongressiTerritoriali();
            IList<CongressoRegionale> regs = cregRet.FindCongressiRegionali();


            foreach (CongressoTerritoriale item in ters)
            {
                CongressPresence presence = item.BuildCongressPresence(name, surname);
                if (presence.isPresent())
                {
                    result.Add(presence);
                }
                    

            }

            foreach (CongressoRegionale item in regs)
            {
                CongressPresence presence = item.BuildCongressPresence(name, surname);
                if (presence.isPresent())
                {
                    result.Add(presence);
                }


            }


            return result;


        }
    }
}
