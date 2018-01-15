using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.GestioneOrganizzativa;
using WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem;
using WIN.TECHNICAL.PERSISTENCE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.GestioneOrganizzativa
{
    public class OrganizzativeDataHandler
    {
         private IPersistenceFacade _persistence;
       
        private bool _found;
        private GeoLocationFacade g;
        

        public OrganizzativeDataHandler(IPersistenceFacade f)
        {
            _persistence = f;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(f));
            g = GeoLocationFacade.Instance();
            
        }

        public void InsertData(int year, List<OrganizativeRecord> data)
        {

            string validationError = ValidateData(data);
            if (!string.IsNullOrEmpty(validationError))
                throw new Exception(validationError);

            try
            {

                _persistence.BeginTransaction();

                _persistence.ExecuteScalar("delete from organizzativedata where anno = " + year.ToString());

                foreach (var item in data)
                {
                    _persistence.MarkNew(item);
                }

                _persistence.CommitTransaction();
            }
            catch (Exception ex)
            {
                _persistence.RollBackTRansaction();
                throw ex;
            }
            
        }

        private string ValidateData(List<OrganizativeRecord> data)
        {
            string validationError = "";
            int i = 1;
            GeoElementChecker c = new GeoElementChecker(g);
            foreach (OrganizativeRecord item in data)
            {
                i++;
                item.SetGeoChecker(c);
                if (!item.IsValid())
                {
                    validationError = validationError + "Errore alla riga " + i.ToString() + ":  " + ExtractErrorString(item) + Environment.NewLine;
                }
            }
            return validationError;
        }

        private string ExtractErrorString(OrganizativeRecord elem)
        {
            string err = "";
            foreach (string item in elem.ValidationErrors)
            {

                err = err + item + "; ";
            }
            return err;
        }


        public List<OrganizativeRecord> GetDataByYear(int year)
        {
            AbstractBoolCriteria c = Criteria.Equal("Anno", year.ToString());
            Query q = _persistence.CreateNewQuery("MapperOrganizativeRecord");
            q.AddWhereClause(c);
            string d = q.CreateQuery(_persistence);
            IList res = q.Execute(_persistence);

            return new List<OrganizativeRecord>(((ArrayList)res).Cast<OrganizativeRecord>().ToList());

        }



    }
}
