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
    public class AdministrativeDataHandler{
    private IPersistenceFacade _persistence;
       
        private bool _found;
        private GeoLocationFacade g;


        public AdministrativeDataHandler(IPersistenceFacade f)
        {
            _persistence = f;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(f));
            g = GeoLocationFacade.Instance();
            
        }

        public void InsertData(int year, List<AdministrativeRecord> data)
        {
            string validationError = ValidateData(data);
            if (!string.IsNullOrEmpty(validationError))
                throw new Exception(validationError);
            try
            {
               
                
                _persistence.BeginTransaction();
                _persistence.ExecuteScalar("delete from administrativedata where anno = " + year.ToString());

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
        private string ValidateData(List<AdministrativeRecord> data)
        {
            string validationError = "";
            int i = 1;
            GeoElementChecker c = new GeoElementChecker(g);
            foreach (AdministrativeRecord item in data)
            {
                i++;
                item.SetGeoChecker(c);
                if (!item.IsValid())
                {
                    validationError = validationError + "Errore alla riga "  + i.ToString() + ":  " +ExtractErrorString(item) + Environment.NewLine;
                }
            }
            return validationError;
        }

        private string ExtractErrorString(AdministrativeRecord elem)
        {
            string err = "";
            foreach (string item in elem.ValidationErrors)
            {

                err = err + item + "; ";
            }
            return err;
        }



        public List<AdministrativeRecord> GetDataByYear(int year)
        {
            AbstractBoolCriteria c = Criteria.Equal("Anno", year.ToString());
            Query q = _persistence.CreateNewQuery("MapperAdministrativeRecord");
            q.AddWhereClause(c);

            IList res = q.Execute(_persistence);

            return new List<AdministrativeRecord>(((ArrayList)res).Cast<AdministrativeRecord>().ToList());

        }



    }
}