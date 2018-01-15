using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class Worker : AbstractPersona 
    {
        public IList<Document> _documents = new List<Document>();

        public IList<Subscription> _subscriptions = new List<Subscription>();

        private Subscription _subscription;

        public string Cellulare1
        {
            get { return Comunicazione.Cellulare1; }
        }

        public Comune ComuneResidenza
        {
            get { return Residenza.Comune; }
        }

        public IList<Document> Documents
        {
            get { return _documents; }
            set { _documents = value; }
        }
        public IList<Subscription> Subscriptions
        {
            get { return _subscriptions; }
            set { _subscriptions = value; }
        }


        public Subscription Subscription
        {
            get { return _subscription; }
            set { _subscription = value; }
        }

        protected override void DoValidation()
        {
            //valido l'iscrizione
            if (_subscription == null)
                ValidationErrors.Add("Iscrizione nulla per il lavoratore");

            if (_subscription != null)
            {
                if (!_subscription.IsValid())
                {
                    AddError(_subscription);
                }
            }
            
            //valido eventuali documenti
            foreach (Document item in _documents)
            {
                if (!item.IsValid())
                {
                    AddError(item);
                }
            }

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

        public WorkerDTO ToWorkerDTO()
        {
            WorkerDTO dto = new WorkerDTO();
            dto.Id = this.Id;
            dto.Name = this.Nome;
            dto.Surname = this.Cognome;
            dto.BirthDate  = this.DataNascita;
            dto.Fiscalcode = this.CodiceFiscale ;
            dto.Nationality = this.Nazionalita.Descrizione;
            dto.BirthPlace  = this.ComuneNascita.Descrizione;
            dto.LivingPlace = this.Residenza.Comune.Descrizione;
            dto.Address = this.Residenza.Via;
            dto.Cap = this.Residenza.Cap;
            dto.Phone = this.Comunicazione.Cellulare1;
            dto.LastModifier = this.ModificatoDa;
            dto.LastUpdate = this.DataModifica;

            DocumentDTO[] dtos = new DocumentDTO[] { };

            foreach (Document item in this.Documents)
            {
                Array.Resize<DocumentDTO>(ref dtos, dtos.Length + 1);
                dtos[dtos.Length - 1] = item.ToDocumentDTO();
            }

            dto.Documents = dtos;


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
