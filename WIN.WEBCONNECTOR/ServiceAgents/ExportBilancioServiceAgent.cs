using System;
using System.Collections.Generic;
using System.Text;
using BilancioFenealgest.DomainLayer;
using WIN.WEBCONNECTOR.Credential;
using WIN.BASEREUSE;
using System.Collections;

namespace WIN.WEBCONNECTOR.ServiceAgents
{
    public class ExportBilancioServiceAgent
    {

        private DTORendiconto _rendiconto;

        public ExportBilancioServiceAgent(DTORendiconto rendiconto)
        {
            _rendiconto = rendiconto;
            
        }


        public string SendBilancio()
        {
            //effettuo la prima validazione
            if (_rendiconto.IsRegionale)
            {
                CheckRegione(_rendiconto);
            }
            else
            {
                CheckRegioneProvincia(_rendiconto);
            }
            //effettuo la trasformazione con i proxy
            FenealgestServices.DTORendiconto r =    TransformToProxyCompatibleData();


            FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
            c.UserName = CredentialDB.Instance.UserName;
            c.Password = CredentialDB.Instance.Password;
            c.Province = CredentialDB.Instance.Province;

            FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();

            service.CredenzialiValue = c;

            string result = service.ImportRendiconto(r);

            return result;
        }

        private void CheckRegione(DTORendiconto r)
        {
            try
            {
                Regione reg = GeoElements.GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetRegioneByName (CredentialDB.Instance.Province);


                string regioneName = reg.Descrizione;
               

                //li confornto con i dati del rendiconto
                bool validated = true;

              


                if (!r.Regione.ToUpper().Equals(regioneName.ToUpper()))
                    validated = false;

                if (!validated)
                    throw new Exception("Regione  non compatibile con l'utente loggato");

            }
            catch (Exception ex)
            {
                throw new Exception("Validazione compatibilità regione provincia non riuscita!" + Environment.NewLine + ex.Message);
            }
        }

        private WIN.WEBCONNECTOR.FenealgestServices.DTORendiconto TransformToProxyCompatibleData()
        {
            FenealgestServices.DTORendiconto r = new WIN.WEBCONNECTOR.FenealgestServices.DTORendiconto();

            r.Anno = _rendiconto.Anno;
            r.IsBilancioQuadrato = _rendiconto.IsBilancioQuadrato;
            r.IsPreventivoQuadrato = _rendiconto.IsPreventivoQuadratoQuadrato;
            r.IsRegionale = _rendiconto.IsRegionale;
            r.Proprietario = _rendiconto.Proprietario;
            r.Provincia = _rendiconto.Provincia;
            r.Regione = _rendiconto.Regione;

            FenealgestServices.DTORendicontoItem[] items = new FenealgestServices.DTORendicontoItem[] { };
            foreach (DTORendicontoItem  item in _rendiconto.Items )
            {
                FenealgestServices.DTORendicontoItem i = new WIN.WEBCONNECTOR.FenealgestServices.DTORendicontoItem();
                i.DescrizioneNodo = item.DescrizioneNodo;
                i.IdNodo = item.IdNodo;
                i.IdNodoPadre = item.IdNodoPadre;
                i.ImportoNodoBilancio = item.ImportoBilancio ;
                i.ImportoNodoPreventivo = item.ImportoPreventivo ;
                i.IsLeaf = item.IsLeaf;
                i.Livello = item.Livello;
                
                Array.Resize<FenealgestServices.DTORendicontoItem>(ref items, items.Length + 1);
                items[items.Length - 1] = i;
            }

            r.Items = items;



            return r;
        }

     

        private void CheckRegioneProvincia(DTORendiconto r)
        {
            try
            {
                Provincia provincia = GeoElements.GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaByName(CredentialDB.Instance.Province);


                string regioneName = GeoElements.GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetRegioneById(provincia.IdRegione.ToString()).Descrizione;
                string ProvinciaName = provincia.Descrizione;


                //li confornto con i dati del rendiconto
                bool validated = true;

                if (!r.Provincia.ToUpper().Equals(ProvinciaName.ToUpper()))
                    validated = false;


                if (!r.Regione.ToUpper().Equals(regioneName.ToUpper()))
                    validated = false;

                if (!validated)
                    throw new Exception("Regione o provincia non compatibili con l'utente loggato");

            }
            catch (Exception ex)
            {
                throw new Exception("Validazione compatibilità regione provincia non riuscita!" + Environment.NewLine + ex.Message);
            }


        }
    }
}
