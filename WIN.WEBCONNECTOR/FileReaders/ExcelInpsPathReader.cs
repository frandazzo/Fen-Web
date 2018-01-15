using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using System.Collections;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class ExcelInpsPathReader : IInpsReader 
    {

        WIN.BASEREUSE.MultipleFileDataRetriever r;
        int _filesFound;
        IList _data;


        public ExcelInpsPathReader(string path)
        {
            r = new BASEREUSE.MultipleFileDataRetriever(path, new ExceInpsPathReaderFactory(), "");

            r.BeginParse +=new BASEREUSE.MultipleFileDataRetriever.BeginParseEventHandler(r_BeginParse); //new WIN.BASEREUSE.BaseExcelReader.BeginParseEventHandler(r_BeginParse);
            r.BeginRetrieving += new WIN.BASEREUSE.MultipleFileDataRetriever.BeginRetrievingEventHandler(r_BeginRetrieving);
            r.EndParse += new WIN.BASEREUSE.MultipleFileDataRetriever.EndParseEventHandler(r_EndParse);
            r.EndRetrieving += new WIN.BASEREUSE.MultipleFileDataRetriever.EndRetrievingEventHandler(r_EndRetrieving);
            r.RetrievingRecord += new WIN.BASEREUSE.MultipleFileDataRetriever.RetrievingRecordEventHandler(r_RetrievingRecord);

            r.BeginParsePath += new BASEREUSE.MultipleFileDataRetriever.BeginParsePathEventHandler(r_BeginParsePath);
            r.EndParsePath += new BASEREUSE.MultipleFileDataRetriever.EndParsePathEventHandler(r_EndParsePath);

            r.BeginParseFile += new BASEREUSE.MultipleFileDataRetriever.BeginParseFileEventHandler(r_BeginParseFile);
            r.EndParseFile += new BASEREUSE.MultipleFileDataRetriever.EndParseFileEventHandler(r_EndParseFile);
            r.EndCreatingRecord += new BASEREUSE.MultipleFileDataRetriever.EndCreatingRecordEventHandler(r_EndCreatingRecord);
        }

        void r_EndCreatingRecord(object sheet, Hashtable hash)
        {
            dynamic dyn = sheet;
            hash.Add("TIPO_PRESTAZIONE", dyn.Cells(6, 7).Value);
        }

        void r_EndParseFile(string filename, Exception exception)
        {
            FileInfo filei = new FileInfo(filename);

            if (exception == null)
                Trace.WriteLine("Importazione terminata con successo del file " + filei.Name);
            else
            {
                Trace.WriteLine("Errore nell'importazione del file " + filei.Name);
                Trace.WriteLine("Errore: " + exception.Message);
            }
       
        }

        void r_BeginParseFile(string filename, int number)
        {
            FileInfo filei = new FileInfo(filename);


            Trace.WriteLine("Importazione file " + number + " di " + _filesFound  + "; file: " + filei.Name);
        
        }

        void r_EndParsePath(int NumberOfFilesFound)
        {
            _filesFound = NumberOfFilesFound;
            Trace.WriteLine("Sono stati trovati " + NumberOfFilesFound + " file da importare");
       
        }

        void r_BeginParsePath()
        {
            Trace.WriteLine("Iniziata analisi del percorso indicato");
        
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





        public FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace ReadTrace(string fileName, InputHeader header)
        {
            ////Apro excel
            //r.OpenExcel();
            ////eseguo un parsing dei dati
            //r.ParseData();
            ////li recupero
            //_data = r.RetrieveData();


             r.ValidateAndParsePath();
             r.StartImport();
             _data = HashTableFromInpsTemplateConverter.ConvertToIQITemplateHashList(r.Importresult);




            InpsTrace t = new InpsTrace();

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
