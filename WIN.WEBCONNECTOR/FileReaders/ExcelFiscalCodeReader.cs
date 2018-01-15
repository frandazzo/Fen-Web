using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace WIN.WEBCONNECTOR.FileReaders
{
   public  class ExcelFiscalCodeReader
    {

        ExcelFiscalCodeRecordReader  r;
        ArrayList _data = new ArrayList();

        public ExcelFiscalCodeReader(string fileName)
        {



            r = new ExcelFiscalCodeRecordReader(fileName);
            r.BeginParse += new WIN.BASEREUSE.BaseExcelReader.BeginParseEventHandler(r_BeginParse);
            r.BeginRetrieving += new WIN.BASEREUSE.BaseExcelReader.BeginRetrievingEventHandler(r_BeginRetrieving);
            r.EndParse += new WIN.BASEREUSE.BaseExcelReader.EndParseEventHandler(r_EndParse);
            r.EndRetrieving += new WIN.BASEREUSE.BaseExcelReader.EndRetrievingEventHandler(r_EndRetrieving);
            r.RetrievingRecord += new WIN.BASEREUSE.BaseExcelReader.RetrievingRecordEventHandler(r_RetrievingRecord);

        }


      

        public IList  ReadCodici()
        {
            try
            {
                IList l = new ArrayList();


                //Apro excel
                r.OpenExcel();
                //eseguo un parsing dei dati
                r.ParseData();
                //li recupero
                _data = r.RetrieveData();


          
                //Trace.WriteLine("Inizio attività recupero codici fiscali");
                int i = 0;

                foreach (Hashtable  item in _data )
                {
                    //if (i > Properties.Settings.Default.MaxFiscalCodeRequests)
                    //    break;
                    if (item["FISCALE"] == null)
                        break;
                    if (string.IsNullOrEmpty(item["FISCALE"].ToString()))
                        break;

                    l.Add(item["FISCALE"].ToString());
                    i++;
                }

              

               //Trace.WriteLine("Termine attivita recupero codici fiscali");


                return l;
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
            //Trace.WriteLine("Recupero del record " + IdRecord.ToString());
        }

        void r_EndRetrieving(int NumberOfRecords)
        {
            //Trace.WriteLine("Termine recupero dati file.");
        }

        void r_EndParse(int NumberOfRecords, int NumberOfFields)
        {
            //Trace.WriteLine("Termine analisi formato file. Trovati " + NumberOfRecords.ToString() + " records");
        }

        void r_BeginRetrieving()
        {
            //Trace.WriteLine("Iniziato recupero dati dal file");
        }

        void r_BeginParse()
        {
            //Trace.WriteLine("Iniziata analisi formato file");
        }


    }
}
