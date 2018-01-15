using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelLiberiReader : ILiberiReader
    {
            ExcelLiberiRecordReader r;
            ArrayList _data = new ArrayList();

            public ExcelLiberiReader(string fileName)
        {
            r = new ExcelLiberiRecordReader(fileName);
            r.BeginParse += new WIN.BASEREUSE.BaseExcelReader.BeginParseEventHandler(r_BeginParse);
            r.BeginRetrieving += new WIN.BASEREUSE.BaseExcelReader.BeginRetrievingEventHandler(r_BeginRetrieving);
            r.EndParse += new WIN.BASEREUSE.BaseExcelReader.EndParseEventHandler(r_EndParse);
            r.EndRetrieving += new WIN.BASEREUSE.BaseExcelReader.EndRetrievingEventHandler(r_EndRetrieving);
            r.RetrievingRecord += new WIN.BASEREUSE.BaseExcelReader.RetrievingRecordEventHandler(r_RetrievingRecord);
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
            Trace.WriteLine("Termine analisi formato file. Trovati " + NumberOfRecords.ToString() + " records");
        }

        void r_BeginRetrieving()
        {
            Trace.WriteLine("Iniziato recupero dati dal file");
        }

        void r_BeginParse()
        {
            Trace.WriteLine("Iniziata analisi formato file");
        }


        public FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace ReadTrace(string fileName, InputHeader header)
        {
            //Apro excel
            r.OpenExcel();
            //eseguo un parsing dei dati
            r.ParseData();
            //li recupero
            _data = r.RetrieveData();



            LiberiTrace t = new LiberiTrace();

            Trace.WriteLine("Inizio attività di creazione oggetto");

            t.Mailto = header.Mail;
            t.Subject = header.Struttura;
           
            //carico i dettagli
            t.FromDetailsHashList(_data);


            Trace.WriteLine("Termine attivita di ricostruzione");


            return t;
        }
    }
}
