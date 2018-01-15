using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.BASEREUSE;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Workers
{
    public class ExportTransformer
    {

        private GeoLocationFacade _geoFacade;
        private IGeoElementChecker _geoChecker;
        private Export _result;
        
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

        public ExportTransformer(GeoLocationFacade geoFacade, IGeoElementChecker geoChecker)
        {
           
            _geoFacade = geoFacade;
            _geoChecker = geoChecker;
            
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
                //rieffettuo in maniera ridondante la stessa validazione effettuata lato client
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
                    ManageDTOError(trace, errorDirPath);
                    _transformationExecuted = false;
                    return;
                }

                //a questo punto effettuo la validazione degli oggetti di dominio
                //ottenuti


                //Valido l'oggetto di dominio costruito
                // se non è valido l'oggetto costruito gestisco l 'errore
                if (!_result.IsValid())
                {
                    ManageDomainObjectError(trace, errorDirPath);
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
                ManageUnknownError(trace, errorDirPath, ex.Message);
            }

            

        }

        private Export CreateExport(ExportTrace trace)
        {
            return ExportFactory.CreateExportTrace(trace, _geoFacade);
        }



        private IList<Worker> CreateWorkerList(ExportTrace trace)
        {
            IList<Worker> list = new List<Worker>();

            foreach (WorkerDTO item in trace.Workers)
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

        private void ManageDomainObjectError(ExportTrace trace, string errorDirPath)
        {
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, ErrorType.DomainError, containsworkerError,containsTraceError);
        }

        private void ManageDTOError(ExportTrace trace, string errorDirPath)
        {
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, ErrorType.DTOError, containsworkerError, containsTraceError);
        }

        private void ManageUnknownError(ExportTrace trace, string errorDirPath, string message)
        {
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, ErrorType.Unknown, message, containsworkerError, containsTraceError);
        }




        public ExportError ExportError
        {
            get { return _exportError; }
        }

    }
}
