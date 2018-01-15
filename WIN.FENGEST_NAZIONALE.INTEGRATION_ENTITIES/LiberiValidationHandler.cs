using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class LiberiValidationHandler
    {

        private string _validatorName = "Feneal";
        private LiberiTrace _trace;
        private IGeoElementChecker _checker;
        
        private LiberoDTO[] _correctDTOs = new LiberoDTO[]{};
        private LiberoDTO[] _incorrectDTOs = new LiberoDTO[] { };
        private bool _validationExecuted = false;
        private bool _containsWorkerErrors = false;
        private string _traceError = "";
        private Hashtable h;
        

        public  LiberiValidationHandler(string validatorName, LiberiTrace trace, IGeoElementChecker checker)
        {
            if (!string.IsNullOrEmpty(validatorName))
                _validatorName = validatorName;
            _trace = trace ;
            _checker = checker;
        }


        public LiberiValidationHandler(string validatorName, IGeoElementChecker checker)
        {
            if (!string.IsNullOrEmpty(validatorName))
                _validatorName = validatorName;
            _checker = checker;
        }


        public bool IsValidationOK
        {
            get
            {
                if (!_validationExecuted)
                    return false;

                if (string.IsNullOrEmpty(_traceError) && _containsWorkerErrors == false)
                    return true;

                return false;
            }
        }


        public bool ContainsWorkerErrors
        {
            get
            {
                return _containsWorkerErrors;
            }
        }


        public string TraceErrors
        {
            get
            {
                return _traceError;
            }
        }

        public bool ContainsTraceErrors
        {
            get
            {
                return (!string.IsNullOrEmpty(_traceError));
            }
        }

        public IList<ValidationResult> Validate()
        {
            _validationExecuted = false;
            _traceError = "";
            _containsWorkerErrors = false;
            _correctDTOs = new LiberoDTO[]{};
            _incorrectDTOs = new LiberoDTO[] { };


            IList<ValidationResult> resultVal = new List<ValidationResult>();



            //valido provincia
            if (string.IsNullOrEmpty(_trace.Provincia))
                _traceError += "Provincia mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.Provincia))
                if (_trace.Provincia.Length > 50)
                    _traceError += "La provincia non può superare i 50 caratteri" + Environment.NewLine;




            if (!_checker.ExistProvince(_trace.Provincia))
                _traceError += "Provincia inesistente" + Environment.NewLine;

            if (_trace.LiberiTraceDetails.Length == 0)
                _traceError += "Nessun lavoratore aggiunto alla lista di importazione!" + Environment.NewLine;

            

          
            //Valido i workers
            IWorkerValidator v = ValidatorFactory.GetValidator(_validatorName);

            //contatore righe validate
            int cont = 0;
            int rowNumber = 1;
            foreach (LiberiTraceDetail  item in _trace.LiberiTraceDetails)
            {
                LiberoDTO elem = item.ToLiberoDTO();
                ValidationResult valRes = v.Validate (elem, _checker,rowNumber);
                if (valRes.IsValidated == false)
                {
                    //aggiungo alla lista degli elementi non corretti
                    Array.Resize(ref _incorrectDTOs, _incorrectDTOs.Length + 1);
                    _incorrectDTOs[_incorrectDTOs.Length - 1] = elem;

                    resultVal.Add(valRes);
                }
                else
                {
                    //aggiungo alla lista degli elementi corretti
                    Array.Resize(ref _correctDTOs, _correctDTOs.Length + 1);
                    _correctDTOs[_correctDTOs.Length - 1] = elem;
                }


                cont++;
               rowNumber++;
            }

            //testo che per lo meno ci sia un lavoratore impostato correttamente
            if (_correctDTOs.Length == 0)
                _traceError += "Nessun lavoratore è stato aggiunto dopo la validazione alla lista di importazione corretta!" + Environment .NewLine;



            _validationExecuted = true;

            if (resultVal.Count > 0)
                _containsWorkerErrors = true;

            return resultVal;
        }



        public int ValidateWorkerArray( WorkerDTO[] dtos)
        {
            int Errors = 0;
            //Valido i workers
            IWorkerValidator v = ValidatorFactory.GetValidator(_validatorName);

            foreach (WorkerDTO item in dtos)
            {
                ValidationResult r = v.Validate(item, _checker);
                if (!r.IsValidated)
                    Errors++;
            }
            return Errors;
        }

       

        public LiberiTrace CorrectTrace
        {
            get
            {
                if (!_validationExecuted)
                    throw new Exception("Validazione non eseguita. Eseguire la validazione prima di richiedere la traccia per l'invio!");
                if (!string.IsNullOrEmpty(_traceError))
                    throw new Exception("Errori nella testata dell'importazione: " + _traceError);

                //costruisco la trace corretta
                LiberiTrace t = _trace;

                t.NotSubscribers = _correctDTOs;

                return t;
            }
        }


        public LiberoDTO[] IncorrectWorkers
        {
            get
            {
                return _incorrectDTOs;
            }
        }


        public IList<LiberiTrace> CreateSubExportTraceList(int workerNumber, LiberiTrace mainTrace)
        {
            if (workerNumber <= 0)
                throw new Exception("Numero di elementi per traccia non valido");


            h = new Hashtable();



            int traceNumber = 0;
            int j = 0;

            IList<LiberiTrace> list = new List<LiberiTrace>();
            //costruisco la tabella hash con i lavoratori
            foreach (LiberiTraceDetail item in mainTrace.LiberiTraceDetails)
            {


                if (j <= workerNumber - 1)
                {
                    AddToTrace(item, traceNumber);
                    j++;
                }
                else
                {
                    traceNumber++;
                    j = 0;
                    AddToTrace(item, traceNumber);
                    j++;
                }
            }

            //adesso creo tanti cloni con i lavoratori presenti nella hash

            for (int i = 0; i <= traceNumber; i++)
            {
                LiberiTrace t = mainTrace.Clone();
                t.ExportNumber = i;
                t.LiberiTraceDetails = (LiberiTraceDetail[])h[i];
                list.Add(t);
            }

            return list;
        }

        private void AddToTrace(LiberiTraceDetail item, int traceNumber)
        {
            if (h[traceNumber] == null)
                h[traceNumber] = new LiberiTraceDetail[] { };

            LiberiTraceDetail[] arr = (LiberiTraceDetail[])h[traceNumber];

            Array.Resize<LiberiTraceDetail>(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = item;

            h[traceNumber] = arr;

        }

    }
}

