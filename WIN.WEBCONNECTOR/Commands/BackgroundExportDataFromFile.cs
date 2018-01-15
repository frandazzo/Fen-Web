using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.WEBCONNECTOR.GeoElements;
using System.Windows.Forms;
using WIN.WEBCONNECTOR.FileReaders;
using System.IO;

namespace WIN.WEBCONNECTOR.Commands
{
    /// <summary>
    /// Questa esportazione serve solo il settore edile
    /// I parametri di input per questo comando sono il percorso del file da esportare, 
    /// l'ente, l'anno e il periodo di riferimento
    /// </summary>
    public class BackgroundExportDataFromFile : ICommand
    {
        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                Log("Inizio esecuzione comando di esportazione su WEB_CONNECTOR*******************");

                //ottengo la exportTrace da esportare
                WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace t = ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace>.Load(ProgramParameters.Instance.FileToExport);

                Log("Deserializzazione traccia eseguita");


                //se non riesco a materializzare la traccia esco
                if (t == null)
                {
                    Log("Traccia deserializzata nulla. L'applicazione verrà chiusa");

                    return;
                }

                Log("Traccia correttamente deserializzata. Inizio validazione");

                //ne eseguo la validazione
                ValidatorHandler v = new ValidatorHandler("Feneal", t, new GeoElementChecker());
                v.Validate();


                Log("Validazione eseguita");


                //verifico il numero di elementi da inviarese è maggiore di zero invio
                if (t.Workers.Length == 0)
                {
                    Log("Dalla validazione nessun elemento risulta inviabile al db nazionale. L'applicazione verrà chiusa");
                    return;
                }

                Log(string.Format("Verranno inviati al db nazionale {0} elementi", t.Workers.Length.ToString ()));
        
                //prima di passare alla esportazione 
                //ricreo la traccia tenendo conto dei parametri che arrivano in input
                InputHeader header = CreateHeaderFromParams(t);
                ReCreateTrace(t, header);

                Log("Avvenuta ridefinizione degli input header della traccia");

                Log("Avvio esportazione");
                //eseguo l'esportazione
                TraceExporter _exporter = new TraceExporter(t, v);
                _exporter.ExportInBacground(500, ProgramParameters.Instance.LogFile);

                Log("Termine esportazione");

            }
            catch (Exception ex)
            {
                Log("Si è verificato un errore nella esecuzione del comando: " + ex.Message);

                if (ex.InnerException != null)
                    Log("InnerEx: " + ex.InnerException.Message);
            }
        }

        private void Log(string message)
        {
            if (File.Exists(ProgramParameters.Instance.LogFile))
            {
                File.AppendAllText(ProgramParameters.Instance.LogFile, message);
            }
        }

        private InputHeader CreateHeaderFromParams(ExportTrace t)
        {
            InputHeader InputHeader = new InputHeader();
            //all'ente sostituisco l'underscore che è servito per passare il parametro "cassa edile" da riga di comando
            InputHeader.Entity = ProgramParameters.Instance.Entity.Replace("_", " ");
            InputHeader.Mail = t.ExporterMail;
            InputHeader.Period = t.Period;
            InputHeader.Year = t.Year;
            InputHeader.Sector = "EDILE";
            InputHeader.Responsible = t.ExporterName;

            return InputHeader;
        }


        private void ReCreateTrace(ExportTrace _trace, WIN.WEBCONNECTOR.FileReaders.InputHeader header)
        {

            //dati responsabile
            _trace.ExporterMail = header.Mail;
            _trace.ExporterName = header.Responsible;
            //dati di settore
            _trace.Sector = header.Sector;
            //dati periodo
            SubscriptionPeriod p = header.CalculatePeriod();
            _trace.InitialDate = p.InitialDate;
            _trace.EndDate = p.EndDate;
            _trace.Period = p.PeriodNumber;
            _trace.Year = p.Year;
            _trace.PeriodType = p.PeriodType;


            CorrectWorkersArray(_trace, header);
        }


        private void CorrectWorkersArray(ExportTrace _trace,  WIN.WEBCONNECTOR.FileReaders.InputHeader header)
        {
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _trace.Workers)
            {
                if (item.Subscription != null)
                {
                    item.Subscription.Sector = header.Sector;
                    item.Subscription.Entity = header.Entity;
                    SubscriptionPeriod p = header.CalculatePeriod();
                    item.Subscription.InitialDate = p.InitialDate;
                    item.Subscription.EndDate = p.EndDate;
                    item.Subscription.Semester = p.PeriodNumber;
                    item.Subscription.Year = p.Year;
                    item.Subscription.PeriodType = p.PeriodType;
                }
            }
        }
    }
}
#endregion