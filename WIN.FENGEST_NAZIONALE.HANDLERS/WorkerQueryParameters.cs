using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.HANDLERS
{
    public class WorkerQueryParameters
    {

        public string Name { get ; private set; }
        public string Surname { get; private set; }
        public string LivingPlace { get; private set; }
        public string Nationality { get; private set; }
        public DateTime  BirthDate { get; private set; }
        public bool CheckDate { get; private set; }
        public string Region { get; private set; }
        public int MaxResult { get; private set; }

        public string CompanyFiscalCode { get; private set; }
        public string CompanyDescription { get; private set; }

        public Provincia  Province { get; set; }
        public WorkerQueryParameters(string name, string surname, string livingPlace, string nationality, DateTime birthDate, bool checkDate, string region, int maxResult, string piva, string companyDescription)
        {
            CompanyDescription = companyDescription;
            CompanyFiscalCode  = piva;
            Name = name;
            Surname = surname;
            LivingPlace = livingPlace;
            Nationality = nationality;
            BirthDate = birthDate;
            CheckDate = checkDate;
            Region = region;
            MaxResult = maxResult;

            _regione = GeoLocationFacade.Instance().GetGeoHandler().GetRegioneByName(Region);
            _comune = GeoLocationFacade.Instance().GetGeoHandler().GetComuneByName(LivingPlace );
            _nazione = GeoLocationFacade.Instance().GetGeoHandler().GetNazioneByName(Nationality );


        }


        private Comune _comune = new ComuneNullo();
        private Regione _regione = new RegioneNulla();
        private Nazione _nazione = new NazioneNulla();


        public Comune LivingPlaceObject
        {
            get
            {
                return _comune;
            }
        }


        public Nazione NationalityObject
        {
            get
            {
                return _nazione ;
            }
        }

        public Regione RegionObject
        {
            get
            {
                return _regione;
            }
        }

    }
}
