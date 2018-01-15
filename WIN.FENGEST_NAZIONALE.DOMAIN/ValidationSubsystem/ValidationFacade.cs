using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ValidationSubsystem
{
    public class ValidationFacade
    {

        private GeoLocationFacade _geoFacade;
        private IGeoElementChecker _geoChecker;
        private Export _result;
        private RiepilogoQuotaDTO _fenealwebResult;
        //private string _validationError = "";
        private bool _transformationExecuted = false;
     
        private bool _isResultValid = false;
        private ExportError _exportError;

        bool containsworkerError = false;
        bool containsTraceError = false;

        //public string ValidationError
        //{
        //    get
        //    {
        //        return _validationError;
        //    }
        //}

        public ValidationFacade(GeoLocationFacade geoFacade, IGeoElementChecker geoChecker)
        {
           
            _geoFacade = geoFacade;
            _geoChecker = geoChecker;
            
        }

        public RiepilogoQuotaDTO FenealwebTransformedResult
        {
            get
            {
                if (_transformationExecuted == false)
                    throw new Exception("Trasformazione non ancora eseguita!");
                return _fenealwebResult;
            }
        }


        public Export TransformedResult
        {
            get 
            {
                if (_transformationExecuted == false)
                    throw new Exception("Trasformazione non ancora eseguita!");
                return _result; 
            }
        }

        public bool TransformationExecuted
        {
            get
            {
                return _transformationExecuted;
            }
        }

        public void FenealwebTransform(ExportTrace trace, string validatorName, string errorDirPath, int companyId)
        {
            _isResultValid = false;


            // mi proteggo da errori inattesi
            //anche se non dovrebbe mai accadere poichè tutte le eccezioni sono gestite
            try
            {
                ////rieffettuo in maniera ridondante la stessa validazione effettuata lato client
                ValidatorHandler validator = new ValidatorHandler(validatorName, trace, _geoChecker);

                validator.Validate();



                if (validator.IsValidationOK)
                {

                    //Trasformo i risultati ottenuti
                    _fenealwebResult = CreateFenealwebRiepilogoQuota(trace, companyId);

                }
                else
                {
                    //gestisco l'errore

                    ManageError(trace, errorDirPath, validator.TraceErrors, ErrorType.FenealwebValidationError);

                    _transformationExecuted = false;
                    return;
                }

                
                //se la validazione sull'oggetto di dominio è stata eseguita
                //allora posso procedere
                _isResultValid = true;
                _transformationExecuted = true;
            }
            catch (Exception ex)
            {
                _transformationExecuted = false;

                string message = ex.Message;
                if (ex.InnerException != null)
                    message += Environment.NewLine + ex.InnerException.Message;

                ManageError(trace, errorDirPath, message, ErrorType.Unknown);
            }



        }

        private RiepilogoQuotaDTO CreateFenealwebRiepilogoQuota(ExportTrace trace, int companyId)
        {
            RiepilogoQuotaDTO dto = new RiepilogoQuotaDTO();

            dto.CompanyId = companyId;
            dto.DataRegistrazione = trace.ExportDate.Date;
            //come data documento imposto il giorno dopo la fine della competenza del pagamento
            dto.DataDocumento = CalculateDataDocumento(trace);
            dto.Compentenza = CalculateCompentenza(trace);
            dto.ExporterMail = trace.ExporterMail;
            dto.ExporterName = trace.ExporterName;
            dto.ExportNumber = trace.ExportNumber;
            dto.Provincia = trace.Province;
            dto.Settore = trace.Sector;



            dto.Guid = trace.FenealwebData.Guid;
            dto.TipoDocumento = trace.FenealwebData.DocumentType;

            dto.Ente = trace.FenealwebData.Entity;

            dto.Note1 = trace.FenealwebData.Notes1;
            dto.Note2 = trace.FenealwebData.Notes2;
            dto.OriginalFilename = trace.FenealwebData.AuthomaticIntegrationFilename;
            dto.XmlFilename = trace.FenealwebData.NormalizedXmlFile;
           

            dto.AssociaDelega = trace.FenealwebData.AssociateDelega;
            dto.CreaDelegaIfNotExist = trace.FenealwebData.CreateDelegaIfNotExist;
            dto.CreaListaLavoro = trace.FenealwebData.CreateListaLavoro;
            dto.UpdateFirmas = trace.FenealwebData.UpdateFirmas;
            dto.UpdateWorkers = trace.FenealwebData.UpdateWorkers;

            dto.Dettagli = new List<DettaglioQuotaDTO>();

            foreach (var item in trace.Workers)
            {
                DettaglioQuotaDTO dett = CreateDettaglioQuota(trace, item, dto);
                dto.Dettagli.Add(dett);

            }



            return dto;
        }

        private DettaglioQuotaDTO CreateDettaglioQuota(ExportTrace trace, INTEGRATION_ENTITIES.WorkerDTO item, RiepilogoQuotaDTO dto)
        {
            DettaglioQuotaDTO dett = new DettaglioQuotaDTO();

            dett.DataDocumento = dto.DataDocumento.Date;
            dett.DataRegistrazione = dto.DataRegistrazione.Date;
            dett.Ente = dto.Ente;
            dett.Provincia = dto.Provincia;
            dett.Settore = dto.Settore;
            dett.TipoDocumento = dto.TipoDocumento;
            

            dett.Contratto = item.Subscription.Contract;
            dett.Livello = item.Subscription.Level;
            dett.Note = "";
            dett.Quota = item.Subscription.Quota;

            SubscriptionPeriod p = new SubscriptionPeriod(trace.Period, trace.Year, trace.PeriodType);

            if (item.Subscription.FenealwebSubscriptionDTOData == null)
            {
                dett.DataInizio = p.InitialDate.Date;
                dett.DataFine = p.EndDate.Date;
                dett.TipoPrestazione = "";
                dett.Note = "";
            }
            else
            {
                if ( item.Subscription.FenealwebSubscriptionDTOData.StartDate != null)
                    dett.DataInizio = item.Subscription.FenealwebSubscriptionDTOData.StartDate.Date;
                else
                    dett.DataInizio = p.InitialDate.Date;

                if (item.Subscription.FenealwebSubscriptionDTOData.EndDate != null)
                    dett.DataFine = item.Subscription.FenealwebSubscriptionDTOData.EndDate.Date;
                else
                    dett.DataFine = p.EndDate.Date;

                //eseguo una possibile conversione nel caso nel file ci siano le date invertite
                if (dett.DataInizio > dett.DataFine)
                {
                    DateTime s1 = dett.DataInizio;
                    dett.DataInizio = dett.DataFine;
                    dett.DataFine = s1;
                }


                dett.TipoPrestazione = item.Subscription.FenealwebSubscriptionDTOData.TipoPrestazione;
                dett.Note = item.Subscription.FenealwebSubscriptionDTOData.Note;
            }
            

            dett.Worker = CreateWorker(item, dto);




            if (!string.IsNullOrEmpty(item.Subscription.EmployCompany))
            {
                dett.Firm = CreateFirm(item, dto);
            }
            

            return dett;
        }

        private FirmDTO CreateFirm(INTEGRATION_ENTITIES.WorkerDTO item, RiepilogoQuotaDTO data)
        {
            FirmDTO dto = new FirmDTO();
            dto.Description = item.Subscription.EmployCompany;
            dto.Piva = item.Subscription.FiscalCode;
            if (item.Subscription.FenealwebSubscriptionDTOData != null)
            {
                dto.Phone = item.Subscription.FenealwebSubscriptionDTOData.FirmTel;
                dto.City = item.Subscription.FenealwebSubscriptionDTOData.FirmCity;
                dto.Cap = item.Subscription.FenealwebSubscriptionDTOData.FirmCap;
                dto.Address = item.Subscription.FenealwebSubscriptionDTOData.FirmAddress;
            }
           

            return dto;
        }

        private INTEGRATION_ENTITIES.FenealwebImportExport.WorkerDTO CreateWorker(INTEGRATION_ENTITIES.WorkerDTO item, RiepilogoQuotaDTO dto)
        {
            INTEGRATION_ENTITIES.FenealwebImportExport.WorkerDTO result = new INTEGRATION_ENTITIES.FenealwebImportExport.WorkerDTO();
            result.Address = item.Address;

            //se si tratta di quote inps probabilmente non ho i dati
            //anagrafici che prendo dal codice fiscale
            if (dto.TipoDocumento.Equals("IQI"))
            {
                DatiFiscali g = _geoFacade.CalcolaDatiFiscali(item.Fiscalcode);
                result.BirthDate = g.DataNascita.Date;
                result.BirthPlace = g.Comune == null ? "" : g.Comune.Descrizione;
                result.Nationality = g.Nazione == null ? "" : g.Nazione.Descrizione;
                
            }
            else
            {
                result.BirthDate = item.BirthDate.Date;
                result.BirthPlace = item.BirthPlace;
                result.Nationality = item.Nationality;
                if (!string.IsNullOrEmpty(item.BirthPlace))
                {
                    if (string.IsNullOrEmpty(item.Nationality))
                    {
                        //se cè un comune e la nazione nopn è vaslorizzata allora la calcolo
                        if (_geoChecker.ExistComune(item.BirthPlace))
                        {
                            result.Nationality = "Italia";
                        }
                        else
                        {
                            //altrimenti nel birthplace cè la nazione
                            result.Nationality = item.BirthPlace;
                            result.BirthPlace = "";
                        }
                    }
                }

            }

            result.Cap = item.Cap;
            result.Fiscalcode = item.Fiscalcode;
            result.LivingPlace = item.LivingPlace;
            result.Name = item.Name;
            
            result.Phone = item.Phone;
            result.Surname = item.Surname;
            if (item.Subscription.FenealwebSubscriptionDTOData != null)
            {
                result.CE = item.Subscription.FenealwebSubscriptionDTOData.WorkerCE;
                result.EC = item.Subscription.FenealwebSubscriptionDTOData.WorkerEC;
                result.Phone2 = item.Subscription.FenealwebSubscriptionDTOData.WorkerPhone2;

            }
            
            return result;
        }

        private string CalculateCompentenza(ExportTrace trace)
        {
            //il calcolo della competenza deve avvenire in base al tipo documento.
            //suppongo infatti che per qualunque documento la competenza degli items è sempre la stessa eccetto che per le quote
            //inps.

            //pertanto calcolo la competenza a partire dalla testata solamnete se si tratta di quote inps
            //altrimenti le recupero dalla prima subscriptiondto se presente

            if (trace.FenealwebData.DocumentType.Equals("IQI") || trace.Workers[0].Subscription.FenealwebSubscriptionDTOData == null)
            {
                SubscriptionPeriod p = new SubscriptionPeriod(trace.Period, trace.Year, trace.PeriodType);
                string start = p.InitialDate.ToShortDateString();
                string end = p.EndDate.ToShortDateString();
                return String.Format("{0} - {1}", start, end);
            }

            if (trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.StartDate == null ||
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.StartDate == DateTime.MinValue || 
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.StartDate == DateTime.MaxValue ||
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.EndDate == null ||
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.EndDate == DateTime.MinValue ||
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.EndDate == DateTime.MaxValue )
            {
                SubscriptionPeriod p = new SubscriptionPeriod(trace.Period, trace.Year, trace.PeriodType);
                string start = p.InitialDate.ToShortDateString();
                string end = p.EndDate.ToShortDateString();
                return String.Format("{0} - {1}", start, end);
            }

            if (trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.StartDate >
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.EndDate)
            {
                SubscriptionPeriod p = new SubscriptionPeriod(trace.Period, trace.Year, trace.PeriodType);
                string start = p.InitialDate.ToShortDateString();
                string end = p.EndDate.ToShortDateString();
                return String.Format("{0} - {1}", start, end);
            }


            SubscriptionPeriod p1 = new SubscriptionPeriod(trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.StartDate,
                trace.Workers[0].Subscription.FenealwebSubscriptionDTOData.EndDate);
            string start1 = p1.InitialDate.ToShortDateString();
            string end1 = p1.EndDate.ToShortDateString();
            return String.Format("{0} - {1}", start1, end1);


        }

        private DateTime CalculateDataDocumento(ExportTrace trace)
        {
            SubscriptionPeriod p = new SubscriptionPeriod(trace.Period, trace.Year, trace.PeriodType);
            return p.EndDate.AddDays(1);
        }


        public void Transform(ExportTrace trace, string validatorName, string errorDirPath)
        {

           
            _exportError = null;
            _isResultValid = false;


             containsworkerError = false;
             containsTraceError = false;


            // mi proteggo da errori inattesi
            //anche se non dovrebbe mai accadere poichè tutte le eccezioni sono gestite
            try
            {
                ////rieffettuo in maniera ridondante la stessa validazione effettuata lato client
                ValidatorHandler validator = new ValidatorHandler(validatorName, trace, _geoChecker);

                validator.Validate();

                containsworkerError = validator.ContainsWorkerErrors;
                containsTraceError = validator.ContainsTraceErrors;

                if (validator.IsValidationOK)
                {

                    //Trasformo i risultati ottenuti
                    _result = CreateExport(trace);
                    _result.Workers = CreateWorkerList(trace);


                }
                else
                {
                    //gestisco l'errore
                    ManageError(trace, errorDirPath, validator.TraceErrors ,  ErrorType.DTOError );
                    _transformationExecuted = false;
                    return;
                }

                //a questo punto effettuo la validazione degli oggetti di dominio
                //ottenuti


                //Valido l'oggetto di dominio costruito
                // se non è valido l'oggetto costruito gestisco l 'errore
                if (!_result.IsValid())
                {
                    StringBuilder b = new StringBuilder ();
                    foreach (string item in _result.ValidationErrors)
	                {
                	    b.AppendLine (item);	 
	                }

                    ManageError(trace, errorDirPath, b.ToString (), ErrorType.DomainError );
                    _transformationExecuted = false;
                    return;
                }
                //se la validazione sull'oggetto di dominio è stata eseguita
                //allora posso procedere
                _isResultValid = true;
                _transformationExecuted = true;
            }
            catch (Exception ex )
            {
                _transformationExecuted = false;

                string message = ex.Message;
                if (ex.InnerException != null)
                    message += Environment.NewLine + ex.InnerException.Message;

                ManageError(trace, errorDirPath, message,   ErrorType.Unknown );
            }

            

        }

        private Export CreateExport(ExportTrace trace)
        {
            return ExportFactory.CreateExportTrace(trace, _geoFacade);
        }



        private IList<Worker> CreateWorkerList(ExportTrace trace)
        {
            IList<Worker> list = new List<Worker>();

            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in trace.Workers)
            {
                Worker w = WorkerFactory.CrateWorker(item, _geoFacade, _result);
                list.Add(w);
            }

            return list;
        }





        public bool IsResultsValid
        {
            get { return _isResultValid; }
        }

        //private void ManageDomainObjectError(ExportTrace trace, string errorDirPath, string message)
        //{
        //    trace.Errore = message;
        //    ErrorManager m = new ErrorManager(errorDirPath);
        //    _exportError = m.CreateExportError(trace, ErrorType.DomainError,message, containsworkerError,containsTraceError);
        //}

        //private void ManageDTOError(ExportTrace trace, string errorDirPath, string message)
        //{
        //    trace.Errore = message;
        //    ErrorManager m = new ErrorManager(errorDirPath);
        //    _exportError = m.CreateExportError(trace, ErrorType.DTOError,message, containsworkerError, containsTraceError);
        //}

        //private void ManageUnknownError(ExportTrace trace, string errorDirPath, string message)
        //{
        //    ErrorManager m = new ErrorManager(errorDirPath);
        //    _exportError = m.CreateExportError(trace, ErrorType.Unknown, message, containsworkerError, containsTraceError);
        //}

        private void ManageError(ExportTrace trace, string errorDirPath, string message, ErrorType type)
        {
            trace.Errore = message;
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, type, message, containsworkerError, containsTraceError);
        }

        private void ManageError(LiberiTrace trace, string errorDirPath, string message, ErrorType type)
        {
            trace.Errore = message;
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, type, message, containsworkerError, containsTraceError);
        }


        public ExportError ExportError
        {
            get { return _exportError; }
        }


        public void FenealwebLiberiTransform(INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace trace, string validator, string errorLogDir, int fenealwebCompanyId)
        {
            _isResultValid = false;


            // mi proteggo da errori inattesi
            //anche se non dovrebbe mai accadere poichè tutte le eccezioni sono gestite
            try
            {
               

                //Trasformo i risultati ottenuti
                _fenealwebResult = CreateFenealwebRiepilogoQuotaForLiberi(trace, fenealwebCompanyId);

                _isResultValid = true;
                _transformationExecuted = true;
            }
            catch (Exception ex)
            {
                _transformationExecuted = false;

                string message = ex.Message;
                if (ex.InnerException != null)
                    message += Environment.NewLine + ex.InnerException.Message;

                ManageError(trace, errorLogDir, message, ErrorType.Unknown);
            }
        }

        private RiepilogoQuotaDTO CreateFenealwebRiepilogoQuotaForLiberi(LiberiTrace trace, int companyId)
        {
            RiepilogoQuotaDTO dto = new RiepilogoQuotaDTO();

            dto.CompanyId = companyId;
            dto.DataRegistrazione = DateTime.Now;
            //come data documento imposto il giorno dopo la fine della competenza del pagamento
            dto.DataDocumento = DateTime.Now;
            dto.Compentenza = "";
            dto.ExporterMail = trace.Mailto;
            dto.ExporterName = "";
            dto.ExportNumber = trace.ExportNumber;
            dto.Provincia = trace.Provincia;
            dto.Settore = "";



            dto.Guid = "";
            dto.TipoDocumento = "";

            dto.Ente = "";

            dto.Note1 = "";
            dto.Note2 = "";
            dto.OriginalFilename = "";
            dto.XmlFilename = "";


            dto.AssociaDelega = false;
            dto.CreaDelegaIfNotExist = false;
            dto.CreaListaLavoro = false;
            dto.UpdateFirmas = false;
            dto.UpdateWorkers = false;

            dto.Dettagli = new List<DettaglioQuotaDTO>();

            foreach (var item in trace.LiberiTraceDetails)
            {
                DettaglioQuotaDTO dett = CreateDettaglioQuotaForLiberi(trace, item, dto);
                dto.Dettagli.Add(dett);

            }



            return dto;
        }

        private DettaglioQuotaDTO CreateDettaglioQuotaForLiberi(LiberiTrace trace, LiberiTraceDetail item, RiepilogoQuotaDTO dto)
        {
            DettaglioQuotaDTO dett = new DettaglioQuotaDTO();

            dett.DataDocumento = DateTime.Now;
            dett.DataRegistrazione = DateTime.Now;
            dett.Ente = "";
            dett.Provincia = "";
            dett.Settore = "";
            dett.TipoDocumento = "";


            dett.Contratto = "";
            dett.Livello = "";
            dett.Note = "";
            dett.Quota = 0;

            
            dett.DataInizio = DateTime.Now;
            dett.DataFine = DateTime.Now;
            dett.TipoPrestazione = "";
            dett.Note = "";
            


            dett.TipoPrestazione = "";
            dett.Note = "";
            


            dett.Worker = CreateWorker(item, dto);

            dett.Firm = CreateFirm(item, dto);
            


            return dett;
        }

        private FirmDTO CreateFirm(LiberiTraceDetail item, RiepilogoQuotaDTO quota)
        {
            FirmDTO dto = new FirmDTO();
            dto.Description = item.AZIENDA_IMPIEGO;
            dto.Piva = item.PARTITA_IVA;
            dto.Phone = item.TELEFONO_AZIENDA;
            dto.City = item.COMUNE_AZIENDA;
            dto.Cap = item.CAP_AZIENDA;
            dto.Address = item.INDIRIZZO_AZIENDA;
           


            return dto;
        }

        private INTEGRATION_ENTITIES.FenealwebImportExport.WorkerDTO CreateWorker(LiberiTraceDetail item, RiepilogoQuotaDTO dto)
        {
            INTEGRATION_ENTITIES.FenealwebImportExport.WorkerDTO result = new INTEGRATION_ENTITIES.FenealwebImportExport.WorkerDTO();

            result.Name = item.NOME_UTENTE;
            result.Surname = item.COGNOME_UTENTE;
            result.Fiscalcode = item.FISCALE;

            if (!string.IsNullOrEmpty(item.FISCALE))
            {
                DatiFiscali g = _geoFacade.CalcolaDatiFiscali(item.FISCALE);
                result.BirthDate = g.DataNascita.Date;
                result.BirthPlace = g.Comune == null ? "" : g.Comune.Descrizione;
                result.Nationality = g.Nazione == null ? "" : g.Nazione.Descrizione;
            }
            

            result.LivingPlace = item.COMUNE;
            result.Address = item.INDIRIZZO;
            result.Cap = item.CAP;

            result.Phone = item.TELEFONO1;
            result.CE = item.CODICE_CE_UTENTE;
            result.EC = item.CODICE_EC_UTENTE;
            result.Phone2 = item.TELEFONO2;


            return result;
        }
    }
}
