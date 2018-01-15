using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem
{
    public class LiberoFactory
    {
        private static DatiFiscali _fiscali;
        private static Libero w;

       

        public static Libero CreateLibero(LiberoDTO workerPrototype, GeoLocationFacade geoFacade, Export parent)
        {
            if (workerPrototype == null)
                throw new ArgumentNullException("Impossibile creare un worker da un liberoDTO nullo!");



            //Questo controllo è ridondante
            try
            {
                //se il codice fiscale non è corretto non incomincio neanche
                _fiscali = geoFacade.CalcolaDatiFiscali(workerPrototype.Fiscalcode.ToUpper());
            }
            catch (InvalidFiscalCodeException ex)
            {
                throw new Exception("Codice fiscale non corretto! Impossibile processare i dati del lavoratore!", ex);
            }

            w = new Libero();

            //Imposto le proprità per la persona
            if (string.IsNullOrEmpty(workerPrototype.Name))
                workerPrototype.Name = "";

            w.Nome = workerPrototype.Name;
            w.Cognome = workerPrototype.Surname;
            w.CodiceFiscale = workerPrototype.Fiscalcode;

            if (workerPrototype.BirthDate.Equals(new DateTime(1900, 1, 1)))
                w.DataNascita = GetFromFiscalCode();
            else
                w.DataNascita = workerPrototype.BirthDate;

            w.Sesso = (WIN.BASEREUSE.AbstractPersona.Sex)Enum.Parse(typeof(WIN.BASEREUSE.AbstractPersona.Sex), _fiscali.SessoPersona);

            SetBirthPlaceData(workerPrototype, geoFacade);

            SetLivingPlaceData(workerPrototype, geoFacade);

            SetComunicationData(workerPrototype);

            w.Ente = workerPrototype.Ente;
            w.CurrentAzienda = workerPrototype.CurrentAzienda;
            w.LiberoAl = workerPrototype.LiberoAl;
            w.IscrittoA = workerPrototype.IscrittoA;
            w.Provincia = parent.Province;


            return w;
        }

        private static void SetComunicationData(LiberoDTO workerPrototype)
        {
            if (workerPrototype.Phone == null)
                workerPrototype.Phone = "";

            w.Comunicazione.Cellulare1 = workerPrototype.Phone;
        }

        private static void SetLivingPlaceData(LiberoDTO workerPrototype, GeoLocationFacade geoFacade)
        {
            //evito a priori qualunque problema di riferimento nullo anche se non dovrebbe mai accadere
            if (workerPrototype.LivingPlace == null)
                workerPrototype.LivingPlace = "";
            //calcolo il comune di residenza
            Comune c = geoFacade.GetGeoHandler().GetComuneByName(workerPrototype.LivingPlace);

            //se il comune non è nullo o di tipo nullo allora imposto il comune di residenza dell'utente
            if (c != null)
                if (c.Id != -1)
                {
                    w.Residenza.Comune = c;
                }
            //se il comune non è stato specificato allora rimarrà un comune nullo
            //**********************************************


            //prendo a questo punto la provincia legata al comune trovato
            //se il comune è nullo il geohandler restituirà una provincia nulla
            w.Residenza.Provincia = geoFacade.GetGeoHandler().GetProvinciaById(w.Residenza.Comune.IdProvincia.ToString());


            //se il comune è nullo verranno restituiti cap come stringhe vuote
            if (workerPrototype.Cap == null)
                workerPrototype.Cap = "";

            if (string.IsNullOrEmpty(workerPrototype.Cap))
                w.Residenza.Cap = geoFacade.GetCapForComune(workerPrototype.LivingPlace).ToString();
            else
                w.Residenza.Cap = workerPrototype.Cap;
            //imposto la via

            if (workerPrototype.Address == null)
                workerPrototype.Address = "";

            w.Residenza.Via = workerPrototype.Address;


        }

        private static void SetBirthPlaceData(LiberoDTO workerPrototype, GeoLocationFacade geoFacade)
        {
            //evito a priori qualunque problema di riferimento nullo anche se non dovrebbe mai accadere
            if (workerPrototype.BirthPlace == null)
                workerPrototype.BirthPlace = "";
            //la prima cosa da verificare e se nel campo birthplace cè la nazione
            //o il comune di nascita 
            Nazione n = geoFacade.GetGeoHandler().GetNazioneByName(workerPrototype.BirthPlace);

            //Se la nazione non è nulla o di tipo nullo allora imposto la nazionalita dell'utente
            //che sicuramente sarà straniero. Percio anche il comune risulterà nullo;
            if (n != null)
                if (n.Id != -1)
                {
                    w.Nazionalita = n;
                }

            //se la nazionalità non è stata specificata ne imposto la nazionalità
            //presa dal codice fiscale. 
            if (w.Nazionalita.Id == -1)
                w.Nazionalita = _fiscali.Nazione;

            //In ogni caso la nazionalita non è stata trovata l'oggetto nazionalità 
            //sarà sempre di tipo nazione nulla
            //**********************************************




            //calcolo adesso il comune di nascita
            Comune c = geoFacade.GetGeoHandler().GetComuneByName(workerPrototype.BirthPlace);

            //se il comune non è nullo o di tipo nullo allora imposto il comune dell'utente
            if (c != null)
                if (c.Id != -1)
                {
                    w.ComuneNascita = c;
                }


            //se il comune non è stato specificato allora lo prendo dal codice fiscale
            if (c.Id == -1)
                w.ComuneNascita = _fiscali.Comune;


            //In ogni caso il comune non è stato trovato l'oggetto comune 
            //sarà sempre di tipo comune nullo
            //**********************************************


            //prendo a questo punto la provincia legata al comune trovato
            w.ProvinciaNascita = geoFacade.GetGeoHandler().GetProvinciaById(w.ComuneNascita.IdProvincia.ToString());

        }

        private static DateTime GetFromFiscalCode()
        {
            return _fiscali.DataNascita;
        }

       
    }
}
