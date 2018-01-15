using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class ExportHandler
    {
        private IPersistenceFacade _persistence;
       
        private bool _found;
        private Export _export;

        public ExportHandler(IPersistenceFacade f)
        {
            _persistence = f;
            
        }
          

    

        public Export Export
        {
            get { return _export; }
        }

        public bool Found
        {
            get { return _found ; }
        }


        public void LoadUniqueExport(int idProvince, string sector, DateTime initialDate, DateTime endDate)
        {
            Query q = _persistence.CreateNewQuery("MapperExport");

            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            c.AddCriteria(Criteria.Equal("Id_Provincia", idProvince.ToString()));
            c.AddCriteria(Criteria.MatchesEqual("Settore", sector,_persistence.DBType));
            c.AddCriteria(Criteria.DateEqual("DataInizio", initialDate , _persistence.DBType));
            c.AddCriteria(Criteria.DateEqual("DataFine", endDate, _persistence.DBType));

            q.AddWhereClause(c);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                _found = true;
                _export = w[0] as Export;
            }
            else
            {
                _export  = null;
                _found = false;
            }

        }



    }
}
