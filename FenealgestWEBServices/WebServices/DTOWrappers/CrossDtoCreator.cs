using System;
using System.Collections.Generic;
using System.Web;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    public class CrossDtoCreator
    {


        IPersistenceFacade f = DataAccessServices.SimplePersistenceFacadeInstance();
        private string _province;

        public CrossDtoCreator(string province)
        {
            // TODO: Complete member initialization
            this._province = province;
        }

        



        public IList<CrossDto> CreateDtos(QueryResultDTO result)
        {
            IList<CrossDto> data = new List<CrossDto>();

            //ottengo tutti gli utenti delle feneal provinciali presenti nel db
            //per ognuno di essi
            IList<Utente> utentiProvinciali = RetrieveUtentiProvinciali();

            //per ogni utente recupero tutti i lavoratori che sono stati nella provincia
            foreach (Utente item in utentiProvinciali)
            {
                //non invio la mail alla stessa provincia
                if (!item.Appartenenza.Provicnia.Descrizione.Equals(_province))
                {
                    WorkerDTO[] workers = GetWorkersWithSubscriptionInProvince(result, item.Appartenenza.Provicnia.Descrizione);

                    if (workers.Length > 0)
                    {
                        //aggiungo un crossdto alla lista
                        QueryResultDTO dto = new QueryResultDTO();
                        dto.Workers = workers;
                        dto.IsResultValid = true;


                        CrossDto d = new CrossDto();
                        d.Dto = dto;
                        d.FromProvince = item.Appartenenza.Provicnia.Descrizione;
                        d.Province = _province;
                        d.MailTo = item.Mail;

                        data.Add(d);
                    }

                }

            }


            return data;
        }

        private WorkerDTO[] GetWorkersWithSubscriptionInProvince(QueryResultDTO listOfWorkers, string provinceId)
        {

            WorkerDTO[] result = new WorkerDTO[] { };

            foreach (WorkerDTO item in listOfWorkers.Workers)
            {
                if (item.WasInProvince(provinceId))
                {
                    Array.Resize<WorkerDTO>(ref result, result.Length + 1);
                    result[result.Length - 1] = item;
                }
            }

            return result;
        }

        private IList<Utente> RetrieveUtentiProvinciali()
        {
            UserProvider p = new UserProvider(f);


            IList<Utente> result = new List<Utente>();


            foreach (Utente item in p.GetUtenti())
            {
                if (item.Appartenenza.Struttura == WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Provinciale)
                    result.Add(item);
            }


            return result;

        }
    }
}