using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class Libero : AbstractPersona
    {
        private DateTime _liberoAl = DateTime.Now;
        private string _ente = "";
        private string _currentAzienda = "";
        private string _iscrittoA = "";
        private Provincia _provincia;


        private IList<Subscription> _subscriptions;
        public IList<Subscription> Subscriptions
        {
            get { return _subscriptions; }
            set { _subscriptions = value; }
        }

        public DateTime LiberoAl
        {
            get { return _liberoAl; }
            set { _liberoAl = value; }
        }

        public string IscrittoA
        {
            get { return _iscrittoA; }
            set { _iscrittoA = value; }
        }


        public Provincia Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        public string CurrentAzienda
        {
            get { return _currentAzienda; }
            set { _currentAzienda = value; }
        }

        public string Ente
        {
            get { return _ente; }
            set { _ente = value; }
        }



        public string Cellulare1
        {
            get { return Comunicazione.Cellulare1; }
        }

        public Comune ComuneResidenza
        {
            get { return Residenza.Comune; }
        }

        private void AddError(AbstractPersistenceObject item)
        {
            string errors = "";
            foreach (string err in item.ValidationErrors)
            {
                errors += err + Environment.NewLine;
            }
            ValidationErrors.Add(errors);
        }

        public LiberoDTO ToLiberoDTO()
        {
            LiberoDTO dto = new LiberoDTO();
            dto.Id = this.Id;
            dto.Name = this.Nome;
            dto.Surname = this.Cognome;
            dto.BirthDate = this.DataNascita;
            dto.Fiscalcode = this.CodiceFiscale;
            dto.Nationality = this.Nazionalita.Descrizione;
            dto.BirthPlace = this.ComuneNascita.Descrizione;
            dto.BirthProvince = this.ProvinciaNascita.Sigla;
            dto.LivingPlace = this.Residenza.Comune.Descrizione;
            dto.LivingProvince = this.Residenza.Provincia.Sigla;
            dto.Address = this.Residenza.Via;
            dto.Cap = this.Residenza.Cap;
            dto.Phone = this.Comunicazione.Cellulare1;

            dto.LiberoAl = this.LiberoAl;
            dto.Ente = this.Ente;
            dto.CurrentAzienda = this.CurrentAzienda;
            dto.IscrittoA = this.IscrittoA;
            dto.ProvinceName = this.Provincia.Descrizione;

             
           
            return dto;
        }

        public WorkerDTO ToWorkerDTO()
        {
            WorkerDTO dto = new WorkerDTO();
            dto.Id = this.Id;
            dto.Name = this.Nome;
            dto.Surname = this.Cognome;
            dto.BirthDate = this.DataNascita;
            dto.Fiscalcode = this.CodiceFiscale;
            dto.Nationality = this.Nazionalita.Descrizione;
            dto.BirthPlace = this.ComuneNascita.Descrizione;
            dto.LivingPlace = this.Residenza.Comune.Descrizione;
            dto.Address = this.Residenza.Via;
            dto.Cap = this.Residenza.Cap;
            dto.Phone = this.Comunicazione.Cellulare1;
            dto.LastModifier = this.ModificatoDa;
            dto.LastUpdate = this.DataModifica;

            dto.CurrentAzienda = this.CurrentAzienda;
            dto.LiberoAl = this.LiberoAl;
            dto.IscrittoA = this.IscrittoA;


            SubscriptionDTO[] subs = new SubscriptionDTO[] { };

            foreach (Subscription item in this.Subscriptions)
            {
                Array.Resize<SubscriptionDTO>(ref subs, subs.Length + 1);
                subs[subs.Length - 1] = item.ToSubscriptionDTO();
            }

            dto.Subscriptions = subs;



            return dto;
        }
    }
}
