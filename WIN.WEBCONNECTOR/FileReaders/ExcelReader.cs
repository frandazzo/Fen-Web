using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.WEBCONNECTOR.Credential;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelReader : IExportReader 
    {
       
        ExcelRecordReader r;
        ArrayList _data = new ArrayList ();

        public ExcelReader( string fileName)
        {

           

            r = new ExcelRecordReader(fileName);
            r.BeginParse += new WIN.BASEREUSE.BaseExcelReader.BeginParseEventHandler(r_BeginParse);
            r.BeginRetrieving += new WIN.BASEREUSE.BaseExcelReader.BeginRetrievingEventHandler(r_BeginRetrieving);
            r.EndParse += new WIN.BASEREUSE.BaseExcelReader.EndParseEventHandler(r_EndParse);
            r.EndRetrieving += new WIN.BASEREUSE.BaseExcelReader.EndRetrievingEventHandler(r_EndRetrieving);
            r.RetrievingRecord += new WIN.BASEREUSE.BaseExcelReader.RetrievingRecordEventHandler(r_RetrievingRecord);

        }


        #region IExportReader Membri di

        public WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace ReadExport(string fileName, InputHeader header)
        {
            try
            {
                //Apro excel
                r.OpenExcel();
                //eseguo un parsing dei dati
                r.ParseData();
                //li recupero
                _data = r.RetrieveData();


          
                Trace.WriteLine("Inizio attività di creazione oggetto");

                ExportTrace t = new ExportTrace();

                //dati esportazione
                t.ExportDate = DateTime.Now;
                t.ExportType = ExprtType.SimpleExport;
                //dati responsabile
                t.ExporterMail = header.Mail;
                t.ExporterName = header.Responsible;
                //dati di settore
                t.Sector = header.Sector;
                //dati periodo
                SubscriptionPeriod p = header.CalculatePeriod();
                t.InitialDate = p.InitialDate;
                t.EndDate = p.EndDate;
                t.Period = p.PeriodNumber;
                t.Year = p.Year;
                t.PeriodType = p.PeriodType;
                t.Struttura = header.Struttura;

                //credenziali
                t.UserName = CredentialDB.Instance.UserName;
                t.Province = CredentialDB.Instance.Province;
                t.Password = CredentialDB.Instance.Password ;

                //a questo punto posso aggiungere i workers

                t.Workers = CreateWorkersArray(header);

               Trace.WriteLine("Termine attivita di ricostruzione");


                return t;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DisposeExcelApp();
            }

           
          




        }

        private WorkerDTO[] CreateWorkersArray(InputHeader header)
        {


            //definisco un indice per il listener
            int i = 0;

            WorkerDTO[] workers = new WorkerDTO[] { };


            foreach (Hashtable item in _data)
            {
                //creo il worker
                WorkerDTO dto = CreateWorker(item, i+2, header);



                //lo aggiungo all'array
                Array.Resize<WorkerDTO>(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = dto;

               Trace.WriteLine("Costruito dato lavoratore num. " + i.ToString());

                i++;
            }

            return workers;
        }

        private WorkerDTO CreateWorker(Hashtable item, int rownumber, InputHeader header)
        {
            WorkerDTO dto = new WorkerDTO();
            SubscriptionDTO sub = new SubscriptionDTO();


            //verifico i dati utente provenienti da excel
            if ( item["FISCALE"]  == null) item["FISCALE"] = "";
            if (item["COGNOME_UTENTE"] == null) item["COGNOME_UTENTE"] = "";
            if ( item["NOME_UTENTE"]  == null) item["NOME_UTENTE"] = "";
            if (item["DATA_NASCITA_UTENTE"] == null) item["DATA_NASCITA_UTENTE"] = "";
            if (item["COMUNE_NASCITA"] == null) item["COMUNE_NASCITA"] = "";
            if (item["COMUNE"] == null) item["COMUNE"] = "";
            if (item["INDIRIZZO"] == null) item["INDIRIZZO"] = "";
            if (item["CAP"] == null) item["CAP"] = "";
            if ( item["TELEFONO1"]  == null) item["TELEFONO1"] = "";
            
            //verifico i dati iscrizione provenienti da excel
            if (item["AZIENDA_IMPIEGO"] == null) item["AZIENDA_IMPIEGO"] = "";
            if (item["P_IVA"] == null) item["P_IVA"] = "";
            if (item["CONTRATTO"] == null) item["CONTRATTO"] = "";
            if (item["LIVELLO"] == null) item["LIVELLO"] = "";
            if (item["QUOTA"] == null) item["QUOTA"] = "";
            




            dto.BirthDate = TryDateCast(item["DATA_NASCITA_UTENTE"]);
            dto.Fiscalcode = item["FISCALE"].ToString();
            dto.Surname = item["COGNOME_UTENTE"].ToString().Trim();
            dto.Name = item["NOME_UTENTE"].ToString().Trim();
            dto.BirthPlace = item["COMUNE_NASCITA"].ToString().Trim();
            dto.LivingPlace = item["COMUNE"].ToString().Trim();
            dto.Address = item["INDIRIZZO"].ToString().Trim();
            dto.Cap = item["CAP"].ToString().Trim();
            dto.Phone = item["TELEFONO1"].ToString().Trim();
            dto.RowNumber = rownumber;

            //dati di iscrizione
            sub.Sector = header.Sector;
            sub.Entity = header.Entity;
            sub.EmployCompany = item["AZIENDA_IMPIEGO"].ToString().Trim();
            sub.FiscalCode = item["P_IVA"].ToString();
            sub.Contract = item["CONTRATTO"].ToString().Trim();
            sub.Level = item["LIVELLO"].ToString().Trim();
            sub.Quota = TryDecimalCast(item["QUOTA"]);

            
            SubscriptionPeriod p = header.CalculatePeriod();
            sub.InitialDate = p.InitialDate;
            sub.EndDate = p.EndDate;
            sub.Semester = p.PeriodNumber;
            sub.Year = p.Year;
            sub.PeriodType = p.PeriodType;

            sub.Province = CredentialDB.Instance.Province;


            Provincia pr = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaByName(CredentialDB.Instance.Province);
            Regione reg = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetRegioneById(pr.IdRegione.ToString());

            sub.Region = reg.ToString();


            dto.Subscription = sub;
            return dto;
        }


        private DateTime TryDateCast(object o)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                return new DateTime(1900, 1, 1);
            }
        }



        private double  TryDecimalCast(object o)
        {
            try
            {
                return Convert.ToDouble(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void DisposeExcelApp()
        {
            try
            {
                r.Dispose();
            }
            catch (Exception)
            {
                //non fa nulla
            }
        }

        void r_RetrievingRecord(int IdRecord)
        {
           Trace.WriteLine("Recupero del record " + IdRecord.ToString());
        }

        void r_EndRetrieving(int NumberOfRecords)
        {
           Trace.WriteLine("Termine recupero dati file.");
        }

        void r_EndParse(int NumberOfRecords, int NumberOfFields)
        {
           Trace.WriteLine("Termine analisi formato file. Trovati " + NumberOfRecords.ToString () + " records");
        }

        void r_BeginRetrieving()
        {
           Trace.WriteLine("Iniziato recupero dati dal file");
        }

        void r_BeginParse()
        {
           Trace.WriteLine("Iniziata analisi formato file");
        }

        #endregion
    }
}
