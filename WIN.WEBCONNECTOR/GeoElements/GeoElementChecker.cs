using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.GeoElements
{
    public class GeoElementChecker : IGeoElementChecker

    {
        private GeoLocationFacade _geo;


        public GeoElementChecker()
        {
            _geo = GeoHandlerProvider.Instance.Geo;
        }

        #region IGeoElementChecker Membri di

        public bool ExistProvince(string provinceName)
        {
            Provincia p = _geo.GetGeoHandler().GetProvinciaByName(provinceName);
            if (p.Id != -1)
                return true;
            return false;
        }

        public bool ExistComune(string nomeComune)
        {
            return _geo.ExistComune(nomeComune);
        }

        public string GetComuneByFiscalCode(string fiscalCode)
        {
            try
            {
                DatiFiscali f = _geo.CalcolaDatiFiscali(fiscalCode);
                return f.Comune.Descrizione;
            }
            catch (InvalidFiscalCodeException)
            {
                return "";
            }
        }

        public string CheckFiscalCode(string fiscalCode)
        {

            try
            {
                string result = "";
                //eseguo il calcolo. se non ho errori restituisco una stringa nulla
                DatiFiscali f = _geo.CalcolaDatiFiscali(fiscalCode);
                return result;
            }
            catch (InvalidFiscalCodeException ex)
            {
                //altrimenti il messaggio dell'errore.
                return ex.Message;
            }
        }

        #endregion

        public bool ExistRegion(string regionName)
        {
            Regione p = _geo.GetGeoHandler().GetRegioneByName(regionName);
            if (p.Id != -1)
                return true;
            return false;
        }
    }
}
