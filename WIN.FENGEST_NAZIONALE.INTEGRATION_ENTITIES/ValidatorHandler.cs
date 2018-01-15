using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{


    public class RowValidatedEventArgs : EventArgs
    {
        public RowValidatedEventArgs(int rowPercentage)
        {
           
            this.RowPercentage = rowPercentage;
        }
        public int RowPercentage;

    }



    public class ValidatorHandler
    {
        public event RowValidatedEventHandler ExportingRow;
        public delegate void RowValidatedEventHandler(object sender, RowValidatedEventArgs fe);




        private string _validatorName = "";
        private ExportTrace _trace;
        private IGeoElementChecker _checker;
        
        private WorkerDTO[] _correctDTOs = new WorkerDTO[]{};
        private WorkerDTO[] _incorrectDTOs = new WorkerDTO[] { };
        private bool _validationExecuted = false;
        private bool _containsWorkerErrors = false;
        private string _traceError = "";
        private Hashtable h;
        

        public  ValidatorHandler(string validatorName, ExportTrace trace, IGeoElementChecker checker)
        {
            _validatorName = validatorName;
            _trace = trace ;
            _checker = checker;
        }


        public ValidatorHandler(string validatorName, IGeoElementChecker checker)
        {
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




        public IList<ExportTrace> CreateSubExportTraceList(int workerNumber)
        {
            if (workerNumber  <= 0)
                throw new Exception("Numero di elementi per traccia non valido");


            h = new Hashtable();
            
            


            if (CorrectTrace.Workers.Length == 0)
                throw new Exception("Impossibile creare una lista di pacchetit di esportazione. La traccia non contiene nessun lavoratore");

            int traceNumber = 0;
            int j = 0;

            IList<ExportTrace> list = new List<ExportTrace>();
            //costruisco la tabella hash con i lavoratori
            foreach (WorkerDTO item in CorrectTrace.Workers)
            {
                

                if (j <= workerNumber -1)
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
                ExportTrace t = CorrectTrace.Clone();
                t.ExportNumber = i;
                t.Workers = (WorkerDTO[])h[i];
                list.Add(t);
            }

            return list;
        }

        private void AddToTrace( WorkerDTO item, int traceNumber)
        {
            if (h[traceNumber] == null)
                h[traceNumber] = new WorkerDTO[]{};

            WorkerDTO[] arr = (WorkerDTO[])h[traceNumber];

            Array.Resize<WorkerDTO>(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = item;

            h[traceNumber] = arr;

        }






        public IList<ValidationResult> Validate()
        {
            _validationExecuted = false;
            _traceError = "";
            _containsWorkerErrors = false;
            _correctDTOs = new WorkerDTO[]{};
            _incorrectDTOs = new WorkerDTO[] { };


            IList<ValidationResult> resultVal = new List<ValidationResult>();



            //valido la struttura di riferimento
            if (string.IsNullOrEmpty(_trace.Struttura))
                _traceError = "Struttura di riferimento mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.Struttura))
                if (_trace.Struttura.Length > 30)
                    _traceError += "La struttura di riferimento non può superare 30 caratteri" + Environment.NewLine;

            if (!string.IsNullOrEmpty (_trace.Struttura ))
                if (!UILDescriber.Instance.ExsistStruttura (_trace.Struttura ))
                    _traceError += "Struttura di riferimento specificata ma inesistente" + Environment.NewLine;


            //valido il trace di esportazione
            if (string.IsNullOrEmpty(_trace.ExporterName))
                _traceError += "Nome dell'utente che effettua l'esportazione mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.ExporterName))
                if (_trace.ExporterName.Length > 60)
                    _traceError += "Il nome dell'utente che effettua l'esportazione non può superare 60 caratteri" + Environment.NewLine;


            //valido username
            if (string.IsNullOrEmpty(_trace.UserName))
                _traceError += "Username mancante" + Environment.NewLine;

            //valido password
            if (string.IsNullOrEmpty(_trace.Password))
                _traceError += "Password mancante" + Environment.NewLine;

            //valido provincia
            if (string.IsNullOrEmpty(_trace.Province))
                _traceError += "Provincia mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.Province))
                if (_trace.Province.Length > 50)
                    _traceError += "La provincia non può superare i 50 caratteri" + Environment.NewLine;


            //valido settore
            if (string.IsNullOrEmpty(_trace.Sector))
                _traceError += "Settore mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.Sector))
                if (!UILDescriber.Instance.ExsistSettoreByStruttura(_trace.Struttura , _trace.Sector ))
                    _traceError += "Settore specificato ma inesistente" + Environment.NewLine;


            if (!_checker.ExistProvince (_trace.Province))
                _traceError += "Provincia inesistente" + Environment.NewLine;

            if (_trace.Workers.Length == 0)
                _traceError += "Nessun lavoratore aggiunto alla lista di importazione!" + Environment.NewLine;

            

            //valido il periodo della traccia

            CheckTracePeriod();





            //Valido i workers
            IWorkerValidator v = ValidatorFactory.GetValidator(_validatorName);

            //contatore righe validate
            int cont = 0;

            foreach (WorkerDTO  item in _trace.Workers)
            {
                
                ValidationResult valRes = v.Validate (item, _checker, _trace);
                if (valRes.IsValidated == false)
                {
                    //aggiungo alla lista degli elementi non corretti
                    Array.Resize(ref _incorrectDTOs, _incorrectDTOs.Length + 1);
                    _incorrectDTOs[_incorrectDTOs.Length - 1] = item;

                    resultVal.Add(valRes);
                }
                else
                {
                    //aggiungo alla lista degli elementi corretti
                    Array.Resize(ref _correctDTOs, _correctDTOs.Length + 1);
                    _correctDTOs[_correctDTOs.Length - 1] = item;
                }


                cont++;
                if (ExportingRow != null)
                {
                    if (_trace.Workers.Length > 0)
                    {
                        decimal t = (100  * cont) / _trace.Workers.Length;


                        RowValidatedEventArgs e = new RowValidatedEventArgs(Convert.ToInt32((t)));
                        ExportingRow(this, e);
                    }
                }
                
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

        private void CheckTracePeriod()
        {
            StringBuilder b = new StringBuilder();


            if (_trace.PeriodType == PeriodType.Semester)
            {

                //verifica anno
                if (_trace != null)
                    if ((_trace.Year < 1980) || (_trace.Year > 2040))
                        b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


                //verifica semestre
                if (_trace != null)
                    if ((_trace.Period != 1) && (_trace.Period != 2))
                        b.AppendLine("Semestre non corretto. Immettere un semestre valido");

            }
            else if (_trace.PeriodType == PeriodType.Interval)
            {
                //verifica date
                if (_trace != null)
                    if ((_trace.InitialDate == DateTime.MinValue) || (_trace.InitialDate == DateTime.MaxValue))
                        b.AppendLine("Data inizio non valida. ");

                //verifica data fine
                if (_trace != null)
                    if ((_trace.EndDate == DateTime.MinValue) || (_trace.EndDate == DateTime.MaxValue))
                        b.AppendLine("Data fine non valida. ");

                //verifica date
                if (_trace != null)
                    if ((_trace.EndDate < _trace.InitialDate))
                        b.AppendLine("Data fine precedente data inizio.");

            }
            else
            {
                //verifica anno
                if (_trace != null)
                    if ((_trace.Year < 1980) || (_trace.Year > 2040))
                        b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


                //verifica semestre
                if (_trace != null)
                    if ((_trace.Period < 1) || (_trace.Period > 12))
                        b.AppendLine("Mese non corretto. Immettere un semestre valido");
            }


            if (b.ToString() != "")
                _traceError += b.ToString() + Environment.NewLine;
        }


        public ExportTrace CorrectTrace
        {
            get
            {
                if (!_validationExecuted)
                    throw new Exception("Validazione non eseguita. Eseguire la validazione prima di richiedere la traccia per l'invio!");
                if (!string.IsNullOrEmpty(_traceError))
                    throw new Exception("Errori nella testata dell'importazione: " + _traceError);

                //costruisco la trace corretta
                ExportTrace t = _trace;

                t.Workers = _correctDTOs;

                return t;
            }
        }


        public WorkerDTO[] IncorrectWorkers
        {
            get
            {
                return _incorrectDTOs;
            }
        }




    }
}
