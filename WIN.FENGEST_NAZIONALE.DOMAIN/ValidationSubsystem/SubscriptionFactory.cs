using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem
{
    public class SubscriptionFactory
    {
        public static Subscription CreateSubscription(SubscriptionDTO subscriptionPrototype, GeoLocationFacade geoFacade, Export parentExport, Worker worker)
        {
            //non deve mai accadere
            if (subscriptionPrototype == null)
                throw new ArgumentException("Iscrizione nulla");

            Subscription s = new Subscription();
            s.Province = geoFacade.GetGeoHandler().GetProvinciaByName(subscriptionPrototype.Province);

            //anche questi controlli sono ridondanti
            if (s.Province == null)
                throw new Exception("Iscrizione senza una provincia specificata");

            if (s.Province == null)
                if (s.Province.Id  == -1)
                    throw new Exception("Iscrizione senza una provincia specificata");


            s.Regione = geoFacade.GetGeoHandler().GetRegioneById(s.Province.IdRegione.ToString());

            s.Struttura = parentExport.Structure.ToUpper ();
            s.Area = UILDescriber.Instance.AreaByStruttura(parentExport.Structure );
            s.Sector = subscriptionPrototype.Sector;
            s.Contract = subscriptionPrototype.Contract;

            if (subscriptionPrototype.Entity == null)
                subscriptionPrototype.Entity = "";


            s.Entity = subscriptionPrototype.Entity;

            if (subscriptionPrototype.FiscalCode == null)
                subscriptionPrototype.FiscalCode = "";

            s.FiscalCode = subscriptionPrototype.FiscalCode;

            if (subscriptionPrototype.EmployCompany == null)
                subscriptionPrototype.EmployCompany = "";

            s.EmployCompany = subscriptionPrototype.EmployCompany;


            if (subscriptionPrototype.Level == null)
                subscriptionPrototype.Level = "";

            s.Level = subscriptionPrototype.Level;

            s.Quota = subscriptionPrototype.Quota;

            
            s.Period = CreateSubscriptionPeriod(subscriptionPrototype);


            s.DenormalizedData = CreateDenormalizedData(worker);



            s.Worker = worker;
            s.ParentExport = parentExport;


           



            return s;
        }

        private static DenormalizedData CreateDenormalizedData(Worker worker)
        {
            DenormalizedData d = new DenormalizedData();

            d.annoNascita = worker.DataNascita.Year;
            d.NomeCompleto = worker.CompleteName.Trim();
            d.NomeNazioneNascita = worker.Nazionalita.Descrizione;
            d.NomeComuneNascita = worker.ComuneNascita.Descrizione;
            d.NomeProvicnciaNascita = worker.ProvinciaNascita.Descrizione;
            d.Sesso = worker.Sesso.ToString();

            return d;
        }

        private static SubscriptionPeriod CreateSubscriptionPeriod(SubscriptionDTO subscriptionPrototype)
        {
            SubscriptionPeriod sem;
            if (subscriptionPrototype.PeriodType == PeriodType.Interval)
                sem = new SubscriptionPeriod(subscriptionPrototype.InitialDate, subscriptionPrototype.EndDate);
            else
                sem = new SubscriptionPeriod(subscriptionPrototype.Semester, subscriptionPrototype.Year, PeriodType.Semester);
            return sem;
        }
    }
}
