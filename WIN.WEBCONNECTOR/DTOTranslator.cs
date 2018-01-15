using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.GeoElements;

namespace WIN.WEBCONNECTOR
{
    public class DTOTranslator
    {
        /// <summary>
        /// Funziona che serve a trasformare l'oggetto 
        /// ExportTrace in un dto per essere inviato su un web service
        /// </summary>
        /// <param name="trace"></param>
        /// <returns></returns>
        public static FenealgestServices.ExportTrace ToExportTraceDTO(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace trace)
        {
            WIN.WEBCONNECTOR.FenealgestServices.ExportTrace  etrace =  new WIN.WEBCONNECTOR.FenealgestServices.ExportTrace();

            etrace.EndDate = trace.EndDate;
            etrace.ExportDate = trace.ExportDate ;
            etrace.ExporterMail  = trace.ExporterMail;
            etrace.ExporterName = trace.ExporterName ;
            etrace.ExportNumber  = trace.ExportNumber ;
            etrace.ExportType = (WIN.WEBCONNECTOR.FenealgestServices.ExprtType)Enum.Parse(typeof(WIN.WEBCONNECTOR.FenealgestServices.ExprtType), trace.ExportType.ToString());
            etrace.InitialDate = trace.InitialDate ;
            etrace.Password = trace.Password;
            etrace.Period = trace.Period;
            etrace.PeriodType = (WIN.WEBCONNECTOR.FenealgestServices.PeriodType)Enum.Parse(typeof(WIN.WEBCONNECTOR.FenealgestServices.PeriodType), trace.PeriodType.ToString());
            etrace.Province = trace.Province;
            etrace.Sector = trace.Sector;
            etrace.Transacted = trace.Transacted;
            etrace.UserName = trace.UserName;
            etrace.Year = trace.Year;
            etrace.Struttura = trace.Struttura;
            if (trace.FenealwebData != null){
                etrace.FenealwebData = new FenealgestServices.FenealwebData();
                etrace.FenealwebData.AssociateDelega = trace.FenealwebData.AssociateDelega;
                etrace.FenealwebData.AuthomaticIntegrationFilename = trace.FenealwebData.AuthomaticIntegrationFilename;
                etrace.FenealwebData.CreateDelegaIfNotExist = trace.FenealwebData.CreateDelegaIfNotExist;
                etrace.FenealwebData.CreateListaLavoro = trace.FenealwebData.CreateListaLavoro;
                etrace.FenealwebData.DocumentType = trace.FenealwebData.DocumentType;
                etrace.FenealwebData.Guid = trace.FenealwebData.Guid;
                etrace.FenealwebData.NormalizedXmlFile = trace.FenealwebData.NormalizedXmlFile;

                etrace.FenealwebData.Notes1 = trace.FenealwebData.Notes1;
                etrace.FenealwebData.Notes2 = trace.FenealwebData.Notes2;
                etrace.FenealwebData.UpdateWorkers = trace.FenealwebData.UpdateWorkers;
            }
            
            TransformUsers(trace, etrace);



            return etrace;
        }

        private static void TransformUsers(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace trace, WIN.WEBCONNECTOR.FenealgestServices.ExportTrace etrace)
        {
            //Adesso ciclo sui lavoratori
            WIN.WEBCONNECTOR.FenealgestServices.WorkerDTO[] woks = new WIN.WEBCONNECTOR.FenealgestServices.WorkerDTO[] { };

            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in trace.Workers)
            {
                //Creo il lavoratore
                FenealgestServices.WorkerDTO dto = new WIN.WEBCONNECTOR.FenealgestServices.WorkerDTO();

                //lo trasformo
                dto.Address = item.Address;
                dto.BirthDate = item.BirthDate;
                dto.BirthPlace = item.BirthPlace;
                dto.Cap = item.Cap;
                dto.Fiscalcode = item.Fiscalcode;
                dto.LivingPlace = item.LivingPlace;
                dto.Name  = item.Name;
                dto.Phone  = item.Phone;
                dto.RowNumber = item.RowNumber;
                dto.Surname = item.Surname;
                
                //creo anche l'iscrizione
                FenealgestServices.SubscriptionDTO subDto = new WIN.WEBCONNECTOR.FenealgestServices.SubscriptionDTO();

                subDto.Contract = item.Subscription.Contract;
                subDto.EmployCompany = item.Subscription.EmployCompany ;
                subDto.EndDate = item.Subscription.EndDate;
                subDto.FiscalCode = item.Subscription.FiscalCode;
                subDto.Entity = item.Subscription.Entity;
                subDto.InitialDate = item.Subscription.InitialDate;
                subDto.Level = item.Subscription.Level;
                subDto.PeriodType = (WIN.WEBCONNECTOR.FenealgestServices.PeriodType)Enum.Parse(typeof(WIN.WEBCONNECTOR.FenealgestServices.PeriodType), item.Subscription.PeriodType.ToString());
                subDto.Province = item.Subscription.Province;
                subDto.Quota = item.Subscription.Quota;
                subDto.Region = item.Subscription.Region;
                subDto.Sector = item.Subscription.Sector;
                subDto.Semester = item.Subscription.Semester;
                subDto.Year = item.Subscription.Year;
                if (item.Subscription.FenealwebSubscriptionDTOData != null)
                {
                    subDto.FenealwebSubscriptionDTOData = new FenealgestServices.FenealwebSubscriptionDTOData();
                    subDto.FenealwebSubscriptionDTOData.EndDate = item.Subscription.FenealwebSubscriptionDTOData.EndDate;
                    subDto.FenealwebSubscriptionDTOData.Note = item.Subscription.FenealwebSubscriptionDTOData.Note;
                    subDto.FenealwebSubscriptionDTOData.StartDate = item.Subscription.FenealwebSubscriptionDTOData.StartDate;
                    subDto.FenealwebSubscriptionDTOData.TipoPrestazione = item.Subscription.FenealwebSubscriptionDTOData.TipoPrestazione;
                }

                dto.Subscription = subDto;


                //creo adesso i documenti
                TransformDocuments(item, dto);


                //lo inserisco nell'array
                Array.Resize<FenealgestServices.WorkerDTO>(ref woks, woks.Length + 1);
                woks[woks.Length - 1] = dto;
            }


            etrace.Workers = woks;
        }

        private static void TransformDocuments(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item, WIN.WEBCONNECTOR.FenealgestServices.WorkerDTO worker)
        { 
            //Creo la lista documenti
            FenealgestServices.DocumentDTO[] docs = new WIN.WEBCONNECTOR.FenealgestServices.DocumentDTO[]{};


            if (item.Documents == null)
                return;

            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO elem in item.Documents )
            {

                //creo il documento
                FenealgestServices.DocumentDTO dto = new WIN.WEBCONNECTOR.FenealgestServices.DocumentDTO();

                dto.DocumentDate = elem.DocumentDate;
                dto.DocumentType = elem.DocumentType;
                dto.Notes = elem.Notes;
                dto.Province = elem.Province;
                dto.Region = elem.Region;
                dto.State = elem.State;
                dto.City = elem.City;

                //lo inserisco nell'array
                Array.Resize<FenealgestServices.DocumentDTO>(ref docs, docs.Length + 1);
                docs[docs.Length - 1] = dto;
            }

            if (docs.Length > 0)
                worker.Documents = docs;
            
        }
        


        /// <summary>
        /// Funzione per la lettura da un web service di un lavoratore
        /// </summary>
        /// <param name="workerDTO"></param>
        /// <returns></returns>
        public static WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO ToCustomWorkerDTO(FenealgestServices.WorkerDTO1 dto)
        {
            WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO worker =  new WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO();


            //lo trasformo
            worker.Address = dto.Address;
            worker.BirthDate = dto.BirthDate;
            worker.BirthPlace = dto.BirthPlace;
            worker.Cap = dto.Cap;
            worker.Fiscalcode = dto.Fiscalcode;
            worker.LivingPlace = dto.LivingPlace;
            worker.Name = dto.Name;
            worker.Phone = dto.Phone;
            worker.RowNumber = dto.RowNumber;
            worker.Surname = dto.Surname;
            worker.Nationality = dto.Nationality;
            worker.LastUpdate = dto.LastUpdate;
            worker.LastModifier = dto.LastModifer;
            worker.CurrentAzienda = dto.CurrentAzienda;
            worker.IscrittoA = dto.IscrittoA;
            worker.LiberoAl = dto.LiberoAl;

            if (!string.IsNullOrEmpty (dto.BirthPlace))
            {
                Comune cNas = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetComuneByName(dto.BirthPlace );
                if (cNas != null)
                    if (cNas.Id > 0)
                    {
                        Provincia ppNas = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaById(cNas.IdProvincia.ToString ());
                        if (ppNas != null)
                            worker.BirthProvince = ppNas.Descrizione;
                    }
            }
            
            if (!string.IsNullOrEmpty (dto.LivingPlace))
            {
                Comune cRes = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetComuneByName(dto.LivingPlace);
                if (cRes != null)
                    if (cRes.Id > 0)
                    {
                        Provincia ppRes = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaById(cRes.IdProvincia.ToString());
                        if (ppRes != null)
                            worker.LivingProvince = ppRes.Descrizione;
                    }

            }
            

            TransformSubscriptions(worker, dto);
            TransformDocuments1(worker, dto);

            return worker;
        }



        private static void TransformDocuments1(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item, WIN.WEBCONNECTOR.FenealgestServices.WorkerDTO1 worker)
        {
            WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO[] docs = new WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO[] { };

            if (worker.Documents == null)
                return;

            foreach (FenealgestServices.DocumentDTO1 elem in worker.Documents)
            {

                //creo il documento
                WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO dto = new WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO();



                dto.DocumentDate = elem.DocumentDate;
                dto.DocumentType = elem.DocumentType;
                dto.Notes = elem.Notes;
                dto.Province = elem.Province;
                dto.Region = elem.Region;
                dto.State = elem.State;
                dto.City = elem.City;


               

                //lo inserisco nell'array
                Array.Resize<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.DocumentDTO>(ref docs, docs.Length + 1);
                docs[docs.Length - 1] = dto;
            }

            if (docs.Length > 0)
                item.Documents = docs;

        }

        private static void TransformSubscriptions(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO worker, WIN.WEBCONNECTOR.FenealgestServices.WorkerDTO1 workerDto)
        {
            //Creo la lista documenti
            WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO[] docs = new WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO[] { };

            if (workerDto.Subscriptions == null)
                return;

            foreach (FenealgestServices.SubscriptionDTO1 elem in workerDto.Subscriptions)
            {

                //creo il documento
                WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO dto = new WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO();

                
                dto.Contract = elem.Contract;
                dto.EmployCompany = elem.EmployCompany;
                dto.EndDate = elem.EndDate;
                dto.Entity = elem.Entity;
                dto.InitialDate = elem.InitialDate;
                dto.Level = elem.Level;
                dto.PeriodType = (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.PeriodType)Enum.Parse(typeof(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.PeriodType), elem.PeriodType.ToString());
                dto.Province = elem.Province;
                dto.Quota = elem.Quota;
                dto.Region = elem.Region;
                dto.Sector = elem.Sector;
                dto.Semester = elem.Semester;
                dto.Year = elem.Year;
                dto.FiscalCode = elem.FiscalCode;

                //lo inserisco nell'array
                Array.Resize<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO>(ref docs, docs.Length + 1);
                docs[docs.Length - 1] = dto;
            }

            if (docs.Length > 0)
                worker.Subscriptions = docs;

        }

        /// <summary>
        /// Funzione per la lettura da un web service di uno o più  lavoratori
        /// </summary>
        /// <param name="workerDTO"></param>
        /// <returns></returns>
        public static IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> ToCustomWorkerDTOList(IList<FenealgestServices.WorkerDTO1> workerDTOs)
        {
            IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> list = new List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();

            foreach (FenealgestServices.WorkerDTO1 item in workerDTOs)
            {
                list.Add(ToCustomWorkerDTO(item));
            }

            return list;
        }

    }

}
