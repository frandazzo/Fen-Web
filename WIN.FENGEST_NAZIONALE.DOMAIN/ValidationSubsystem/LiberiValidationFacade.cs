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
    public class LiberiValidationFacade
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

        public LiberiValidationFacade(GeoLocationFacade geoFacade, IGeoElementChecker geoChecker)
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


        public void Transform(LiberiTrace trace, string validatorName, string errorDirPath)
        {

           
            _exportError = null;
            _isResultValid = false;


             containsworkerError = false;
             containsTraceError = false;


            // mi proteggo da errori inattesi
            //anche se non dovrebbe mai accadere poichè tutte le eccezioni sono gestite
            try
            {
                //la validazion eper i liberi non deve essere una validazione o tutto o niente per gli errori riferiti
                //ai workers
                //ma deve essere obbligatoria per errori nella trace
                LiberiValidationHandler validator = new LiberiValidationHandler(validatorName, trace, _geoChecker);

                IList<ValidationResult> errors = validator.Validate();

                containsworkerError = validator.ContainsWorkerErrors;
                containsTraceError = validator.ContainsTraceErrors;

                if (validator.IsValidationOK)
                {

                    //Trasformo i risultati ottenuti
                    _result = CreateExport(trace);
                    _result.NotSubscribers = CreateWorkerList(validator.CorrectTrace);


                }
                else
                {
                    //gestisco l'errore se cè un errore nella traccia
                    if (containsTraceError)
                    {
                        ManageError(trace, errorDirPath, string.Format("{0} -- {1}", validator.TraceErrors, ConstructErrorString(errors)), ErrorType.LiberiDTOError);
                        _transformationExecuted = false;
                        return;
                    }
                    //Security ci sono errori nei lavoratori allora creo lo stesso la traccia e la inserisco ma 
                    //gestisco anche l'errore
                    _result = CreateExport(trace);
                    _result.NotSubscribers = CreateWorkerList(validator.CorrectTrace);
                    ManageError(trace, errorDirPath, ConstructErrorString(errors), ErrorType.PartialLiberiImportError);
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

                    ManageError(trace, errorDirPath, b.ToString (), ErrorType.LiberiDomainError );
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

                ManageError(trace, errorDirPath, message,   ErrorType.LiberiUnknown );
            }

            

        }

        private string ConstructErrorString(IList<ValidationResult> errors)
        {
            string result = "";

            foreach (ValidationResult item in errors)
            {
                result = result + " -- " + item.Errors + Environment.NewLine;
            }


            return result;

        }

        private Export CreateExport(LiberiTrace trace)
        {
            return ExportFactory.CreateExportTrace(trace, _geoFacade);
        }



        private IList<Libero> CreateWorkerList(LiberiTrace trace)
        {
            IList<Libero> list = new List<Libero>();

            foreach (LiberoDTO item in trace.NotSubscribers)
            {
                Libero w = LiberoFactory.CreateLibero(item, _geoFacade, _result);
                list.Add(w);
            }

            return list;
        }





        public bool IsResultsValid
        {
            get { return _isResultValid; }
        }



        private void ManageError(LiberiTrace trace, string errorDirPath, string message, ErrorType type)
        {
            trace.Errore = message + "_exportNumber:" + trace.ExportNumber;
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, type, message, containsworkerError, containsTraceError);
        }



        public ExportError ExportError
        {
            get { return _exportError; }
        }

    }
}
