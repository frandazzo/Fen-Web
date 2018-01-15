using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace WIN.FENGEST_NAZIONALE.DOMAIN
{
    public class WorkerFlowFactory
    {

        public IList<WorkerFlow> constructRegionalFlow(int regionId, IList subscriptions)
        {
            IList < WorkerFlow > result = new List<WorkerFlow>();

            //devo raggruppare tutte le iscrizioni per regione
            //creo una hashtable che contiene in ogni elemento la chiave della regione una lista di iscrizioni per regione 

            Hashtable h = new Hashtable();
            ConstructRegionalHash(subscriptions, h);

            //adesso che ho ottenuto la lista dei dati regionali posso trasformarli in una lista di worker flows
            result = creatFlows(h, regionId);

            return result;
        }

        private IList<WorkerFlow> creatFlows(Hashtable h, int regionId)
        {
            IList<WorkerFlow> flows = new List<WorkerFlow>();
            //qui devo creare unal ista di flussi ad iniziare dal flusso della regione di partenza
            //recupero la lista delle iscrizioni della regione di partenza

            //queste ci sono sicuramente
            IList<Subscription> currentRegionSubscriptions = h[regionId] as IList<Subscription>;
            // da queste recupero il numero di lavoratori trovati
            int workers = calculateWorkers(currentRegionSubscriptions);
            // adesso devo creare un flusso per ogni provincia ma in cima a tutto il il flusso per la regione
            WorkerFlow mainFlow = new WorkerFlow();
            mainFlow.Region = currentRegionSubscriptions[0].Regione.Descrizione;
            mainFlow.MigratedWorkers = workers;
            mainFlow.MigratedWorkersPercentage = "100%";
            flows.Add(mainFlow);

            // adesso devo aggiungere sotto il flusso della regione corrente i flussi delle provicne relative a quella regione
            appendProvincialFlowsToList(flows, currentRegionSubscriptions, mainFlow.MigratedWorkers, true);


            //adesso facvcio la stessa cosa per tutte le regioni diverse dalla regione principale
            IDictionaryEnumerator enumerator = h.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if ((int)enumerator.Key != regionId)
                {

                    int regId = (int)enumerator.Key;

                    IList<Subscription> currentSubscriptions = h[regId] as IList<Subscription>;
                    // da queste recupero il numero di lavoratori trovati
                    int calculatedWorkers = calculateWorkers(currentSubscriptions);
                    // adesso devo creare un flusso per ogni provincia ma in cima a tutto il il flusso per la regione
                    WorkerFlow mainFlowForRegion = new WorkerFlow();
                    mainFlowForRegion.Region = currentSubscriptions[0].Regione.Descrizione;
                    mainFlowForRegion.MigratedWorkers = calculatedWorkers;
                    mainFlowForRegion.MigratedWorkersPercentage = Math.Round(((double)calculatedWorkers / (double)mainFlow.MigratedWorkers) * 100, 2).ToString() + "%";
                    flows.Add(mainFlowForRegion);

                    // adesso devo aggiungere sotto il flusso della regione corrente i flussi delle provicne relative a quella regione
                    appendProvincialFlowsToList(flows, currentSubscriptions, mainFlowForRegion.MigratedWorkers, false);

                }
            }
            return flows;
        }

        private void appendProvincialFlowsToList(IList<WorkerFlow> flows, IList<Subscription> currentRegionSubscriptions, int regionalWorkers, bool mainFlow)
        {
            //recupero tutte le province della regione 
            Hashtable h = new Hashtable();
            ConstructProvincialHash(currentRegionSubscriptions, h);

            //adesso uso un enumeratore per enumerare tutte le procvinvie
            //e crearne i relativi flussi
            IDictionaryEnumerator enumerator = h.GetEnumerator();
            while (enumerator.MoveNext())
            {
                //costruisco il flusso  ++++++++++++++++
                IList<Subscription> provincialSubscriptions = enumerator.Value as List<Subscription>;

                
		            WorkerFlow provFlow = new WorkerFlow();
                    provFlow.Province = provincialSubscriptions[0].Province.Descrizione;
                    provFlow.MigratedWorkers = calculateWorkers(provincialSubscriptions);
                //se sono nel main flow ha sewnso calcolare le ercentuali per provincia
                if (mainFlow)
                    provFlow.MigratedWorkersPercentage = Math.Round(((double)provFlow.MigratedWorkers / (double)regionalWorkers) * 100, 2).ToString() + "%";
                    flows.Add(provFlow);
	            
            }



        }

        private int calculateWorkers(IList<Subscription> currentRegionSubscriptions)
        {
            Hashtable h = new Hashtable();

            foreach (Subscription item in currentRegionSubscriptions)
            {
                if (!h.ContainsKey(item.Worker.Id))
                {
                    h.Add(item.Worker.Id, 1);
                }
            }

            return h.Keys.Count;
        }


        private int calculateWorkers(IList<Subscription> currentSubscriptions, int provinceId)
        {
            Hashtable h = new Hashtable();

            foreach (Subscription item in currentSubscriptions)
            {
                if (provinceId == item.Province.Id)
                {
                    if (!h.ContainsKey(item.Worker.Id))
                    {
                        h.Add(item.Worker.Id, 1);
                    }
                }
            }

            return h.Keys.Count;
        }


        private static void ConstructProvincialHash(IList<Subscription> subscriptions, Hashtable h)
        {
            foreach (Subscription item in subscriptions)
            {
                if (h.ContainsKey(item.Province.Id))
                {
                    //recupero la lista di iscrizioni
                    IList<Subscription> ss = h[item.Province.Id] as IList<Subscription>;
                    ss.Add(item);
                }
                else
                {
                    IList<Subscription> ss = new List<Subscription>();
                    ss.Add(item);
                    h.Add(item.Province.Id, ss);
                }
            }
        }

        private static void ConstructRegionalHash(IList subscriptions, Hashtable h)
        {
            foreach (Subscription item in subscriptions)
            {
                if (h.ContainsKey(item.Regione.Id))
                {
                    //recupero la lista di iscrizioni
                    IList<Subscription> ss = h[item.Regione.Id] as IList<Subscription>;
                    ss.Add(item);
                }
                else
                {
                    IList<Subscription> ss = new List<Subscription>();
                    ss.Add(item);
                    h.Add(item.Regione.Id, ss);
                }
            }
        }

        public IList<WorkerFlow> constructProvincialFlow(int regionId,int provinceId, IList subscriptions)
        {
            IList<WorkerFlow> result = new List<WorkerFlow>();


            Hashtable h = new Hashtable();
            ConstructRegionalHash(subscriptions, h);

            //adesso che ho ottenuto la lista dei dati regionali posso trasformarli in una lista di worker flows
            result = createProvincialFlows(h, regionId, provinceId);


          

            return result;
        }

        private IList<WorkerFlow> createProvincialFlows(Hashtable h, int regionId, int provinceId)
        {
            IList<WorkerFlow> flows = new List<WorkerFlow>();
            //qui devo creare unal ista di flussi ad iniziare dal flusso della provincia di partenza
            //recupero la lista delle iscrizioni della regione di partenza

            //queste ci sono sicuramente
            IList<Subscription> currentRegionSubscriptions = h[regionId] as IList<Subscription>;
            // da queste recupero il numero di lavoratori trovati
            int workers = calculateWorkers(currentRegionSubscriptions, provinceId);
            string provinceName = getSubscriptionProvinceName(currentRegionSubscriptions, provinceId);
            // adesso devo creare un flusso per ogni provincia ma in cima a tutto il il flusso per la regione
            WorkerFlow mainFlow = new WorkerFlow();
            mainFlow.Region = currentRegionSubscriptions[0].Regione.Descrizione;
            mainFlow.Province = provinceName;
            mainFlow.MigratedWorkers = workers;
            mainFlow.MigratedWorkersPercentage = "100%";
            flows.Add(mainFlow);

            // adesso devo aggiungere sotto il flusso della regione corrente i flussi delle provicne relative a quella regione
            appendProvincialFlowsToListForProvincialReport(flows, currentRegionSubscriptions, mainFlow.MigratedWorkers, provinceId);


            //adesso facvcio la stessa cosa per tutte le regioni diverse dalla regione principale
            IDictionaryEnumerator enumerator = h.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if ((int)enumerator.Key != regionId)
                {

                    IList<Subscription> currentSubscriptions = h[(int)enumerator.Key] as IList<Subscription>;
                    // adesso devo aggiungere sotto il flusso della regione corrente i flussi delle provicne relative a quella regione
                    appendProvincialFlowsToListForProvincialReport(flows, currentSubscriptions, mainFlow.MigratedWorkers, provinceId);

                }
            }
            return flows;
        }


        private void appendProvincialFlowsToListForProvincialReport(IList<WorkerFlow> flows, IList<Subscription> currentRegionSubscriptions, int provincialWorkers, int provinceId)
        {
            //recupero tutte le province della regione 
            Hashtable h = new Hashtable();
            ConstructProvincialHash(currentRegionSubscriptions, h);

            //adesso uso un enumeratore per enumerare tutte le procvinvie
            //e crearne i relativi flussi
            IDictionaryEnumerator enumerator = h.GetEnumerator();
            while (enumerator.MoveNext())
            {
                //costruisco il flusso  ++++++++++++++++
                IList<Subscription> provincialSubscriptions = enumerator.Value as List<Subscription>;

                if (provinceId != provincialSubscriptions[0].Province.Id)
                {
                    WorkerFlow provFlow = new WorkerFlow();
                    provFlow.Region = provincialSubscriptions[0].Regione.Descrizione;
                    provFlow.Province = provincialSubscriptions[0].Province.Descrizione;
                    provFlow.MigratedWorkers = calculateWorkers(provincialSubscriptions);
                    
                    //se sono nel main flow ha sewnso calcolare le ercentuali per provincia
                    provFlow.MigratedWorkersPercentage = Math.Round(((double)provFlow.MigratedWorkers / (double)provincialWorkers) * 100, 2).ToString() + "%";
                    flows.Add(provFlow);
                }

                

            }



        }



        private string getSubscriptionProvinceName(IList<Subscription> currentRegionSubscriptions, int provinceId)
        {
            foreach (Subscription item in currentRegionSubscriptions)
            {
                if (item.Province.Id == provinceId)
                    return item.Province.Descrizione;
            }

            return "";
        }
    }
}
