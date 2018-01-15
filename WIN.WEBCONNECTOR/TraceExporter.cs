using System;
using System.Collections.Generic;
using System.Text;
using WIN.WEBCONNECTOR.FenealgestServices;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using System.IO;

namespace WIN.WEBCONNECTOR
{
    public class TraceExportedEventArgs : EventArgs
    {
        public TraceExportedEventArgs(int rowPercentage,  bool cancel)
        {
            this.Cancel = cancel;
            this.RowPercentage = rowPercentage;
        }


        public bool Cancel;
        public int RowPercentage;

    }


    public class TraceExporter
    {


        public delegate void TraceExportEventHandler(object sender, TraceExportedEventArgs fe);


        public event TraceExportEventHandler ExportingRow;
        public event EventHandler BeginExport;
        public event EventHandler EndExport;


        private WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace _trace;
        private WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ValidatorHandler _validator;
        private string _logFile = "log.txt";
        private string _errorDir = "";
        bool cancel = false;


        public TraceExporter(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace trace, ValidatorHandler validator)
        {
            _trace = trace;
            _validator = validator;
        }

        public void SetValidator(ValidatorHandler validator)
        {
            _validator = validator;
        }

        public  void Export(int packetSize)
        {
            //differentemente dalla esportazione in background qui l'utente vuole espressamente, o
            //direttamwentenda connector o da fenealgest aggiornare il db nazionale
            //in questo caso posso mettere i dati Fenealweb Data nella traccia in modo da aggiornare anche fenealweb




            if (BeginExport != null) 
                BeginExport(this, new EventArgs());


            CreateLogDir();

            //creo il servizio per il web service
            FenealgestServices.FenealgestwebServices service = new FenealgestwebServices();

            File.AppendAllText(_logFile, "Inizio suddivisione traccia di esportazione in pacchetti" + Environment.NewLine );
            //adesso può partire la esportazione vera e propria
            //Suddivido la traccia in pacchetti inferiori a 4MB
            IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace> list = _validator.CreateSubExportTraceList(packetSize );

            File.AppendAllText(_logFile, "Termine suddivisione pacchetti: saranno inviati num. " + list.Count.ToString() + " pacchetti" + Environment.NewLine);
            


            //ciclo sulla lista e spedisco ogni pacchetto dopo ovviamente averlo trasformato
            //tengo traccia del ciclo con un contatore che 
            //puo servirmi per la gestione dell'errore
            string guid = Guid.NewGuid().ToString();
            string tipoDocumento = "IQA";
            if (_trace.Sector.Equals("INPS"))
                tipoDocumento = "IQI";



            int i = 0;
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace item in list)
            {
               
                //eseguo la rasformazione
                File.AppendAllText(_logFile, "  Inizio trasformazione pacchetto num. " + (i + 1).ToString() + Environment.NewLine);
                FenealgestServices.ExportTrace t = DTOTranslator.ToExportTraceDTO(item);

                t.FenealwebData = new FenealgestServices.FenealwebData();
                t.FenealwebData.Guid = guid;
                t.FenealwebData.DocumentType = tipoDocumento;
                t.FenealwebData.UpdateWorkers = true;
                t.FenealwebData.CreateDelegaIfNotExist = true;
                t.FenealwebData.AssociateDelega = true;
                t.FenealwebData.UpdateFirmas = false;
                //poiche il web connetcto può non inviate l'ente nella testata
                //per ragioni storiche dovute alla confformazione del servizio sul db nazione
                //se non cè un ente lo prendo dalla prima subscription disponibile del primo lavoratore
                //p.s.   avendo passato la validazione so che ci sono lavoratori e iscrizioni...
                t.FenealwebData.Entity = item.Workers[0].Subscription.Entity;
               

                File.AppendAllText(_logFile, "  Termine trasformazione pacchetto num. " + (i + 1).ToString() + Environment.NewLine);




                if (ExportingRow != null)
                {
                    TraceExportedEventArgs arg = new TraceExportedEventArgs(Convert.ToInt32((100 / list.Count) * i), cancel);
                    ExportingRow(this, arg);
                    cancel = arg.Cancel;
                    if (cancel)
                        return;
                }


               

                
                //esporto il dato
                File.AppendAllText(_logFile, "    Inizio invio pacchetto num. " + (i + 1).ToString() + Environment.NewLine);
                string error = service.ImportData(t);


                //gestisco eventuali errori
                if (!string.IsNullOrEmpty(error))
                {
                    //Gestisco l'errore
                    ManageError(list[i], i);
                }
                else
                {
                    File.AppendAllText(_logFile, "    Pacchetto num. " + (i + 1).ToString() + " correttamente inviato" + Environment.NewLine);
                }

                i++; ;
            }

            if (EndExport != null)
                    EndExport(this, new EventArgs());
        }



        public void ExportInBacground(int packetSize, string logFile)
        {
            //se proprio deve avvenire un export in background sarà solo per aggiornare il dbnazionale e non fenealweb
            //infatti per non andare in conflitto con eventuali importaszioni autonmatiche non
            //metto nella traccia alcun FenealwebData.

          
            if (BeginExport != null)
                BeginExport(this, new EventArgs());


            //creo il servizio per il web service
            FenealgestServices.FenealgestwebServices service = new FenealgestwebServices();
            if (File.Exists(logFile))
                File.AppendAllText(logFile, "Inizio suddivisione traccia di esportazione in pacchetti" + Environment.NewLine);
            //adesso può partire la esportazione vera e propria
            //Suddivido la traccia in pacchetti inferiori a 4MB
            IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace> list = _validator.CreateSubExportTraceList(packetSize);
            if (File.Exists(logFile))
                File.AppendAllText(logFile, "Termine suddivisione pacchetti: saranno inviati num. " + list.Count.ToString() + " pacchetti" + Environment.NewLine);



            //ciclo sulla lista e spedisco ogni pacchetto dopo ovviamente averlo trasformato
            //tengo traccia del ciclo con un contatore che 
            //puo servirmi per la gestione dell'errore

            int i = 0;
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace item in list)
            {

                //eseguo la rasformazione
                if (File.Exists(logFile))
                    File.AppendAllText(logFile, "  Inizio trasformazione pacchetto num. " + (i + 1).ToString() + Environment.NewLine);
                FenealgestServices.ExportTrace t = DTOTranslator.ToExportTraceDTO(item);
                if (File.Exists(logFile))
                    File.AppendAllText(logFile, "  Termine trasformazione pacchetto num. " + (i + 1).ToString() + Environment.NewLine);

              
                //esporto il dato
                if (File.Exists(logFile))
                    File.AppendAllText(logFile, "    Inizio invio pacchetto num. " + (i + 1).ToString() + Environment.NewLine);
                string error = service.ImportData(t);


                //gestisco eventuali errori
                if (!string.IsNullOrEmpty(error))
                {
                    if (File.Exists(logFile))
                        File.AppendAllText(logFile, String.Format("Errore nell'invio del pacchetto num. {0}:  {1}{2}", i + 1,error, Environment.NewLine));
                }
                else
                {
                    if (File.Exists(logFile))
                        File.AppendAllText(logFile, "    Pacchetto num. " + (i + 1).ToString() + " correttamente inviato" + Environment.NewLine);
                }

                i++; ;
            }

        }






        private void CreateLogDir()
        {



            string dirname = @"LogExport_" + DateTime.Now.Year.ToString() + "_" +
                                 DateTime.Now.Month.ToString() + "_" +
                                 DateTime.Now.Day.ToString() + "_" +
                                 DateTime.Now.Hour.ToString () + "_" +
                                 DateTime.Now.Minute.ToString() + "_" +
                                 DateTime.Now.Second.ToString();

            _errorDir  = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dirname); 

            if (!Directory.Exists(_errorDir))
                Directory.CreateDirectory (_errorDir);



            _logFile = Path.Combine(_errorDir, _logFile);
            
        }

        private void ManageError(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace exportTrace, int index)
        {
            //verifico se esiste il file completo altrimenti lo insersco
            InsertCompleteTraceFile(index);

            //creo il file del pacchetto andato in errore
            CreatePacketFile(exportTrace, index);

        }



        private void CreatePacketFile(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace exportTrace, int index)
        {
            string ind = (index + 1).ToString();
            string compleFile = Path.Combine(_errorDir, "Packet_" + ind + ".xml");

            if (!File.Exists(compleFile))
            {
                //tento di inserire il file
                try
                {
                    WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation.ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace>.Save(exportTrace, compleFile);
                    File.AppendAllText(_logFile, "        ERRORE: Creazione file xml per la traccia di esportazione (pacchetto num. " + ind + "): file creato  " + compleFile.ToUpper() + Environment.NewLine);
                }
                catch (Exception)
                {
                    //Se c'e'' un errore a questo livello non posso fare altro che lofggare
                    File.AppendAllText(_logFile, "        ERRORE (pacchetto " + ind + "): Errore nella creazione del file xml per la traccia di esportazione!" + Environment.NewLine);
                }


            }
        }

        private void InsertCompleteTraceFile(int index)
        {

            string compleFile = Path.Combine(_errorDir, "Complete.xml");

            if (!File.Exists(compleFile))
            {
                //tento di inserire il file
                try
                {
                    WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation.ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace>.Save(_trace, compleFile); 
                }
                catch (Exception)
                {
                    //Se c'e'' un errore a questo livello non posso fare altro che lofggare
                    File.AppendAllText(_logFile, "        ERRORE (pacchetto " + (index + 1).ToString() + "): Errore nella creazione del file xml per la traccia completa di esportazione!" + Environment.NewLine);
                }


            }
        }
    }
}
