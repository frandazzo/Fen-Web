using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using System.ComponentModel;

namespace WIN.WEBCONNECTOR.ExcelExport
{
    public class Exporter
    {


        public delegate void RowExportEventHandler(object sender, RowExportedEventArgs fe);


        public event RowExportEventHandler ExportingRow;
        public event EventHandler BeginExportWorkerList;
        public event EventHandler EndExportWorkerList;

        public event EventHandler BeginExportSubscriptionList;
        public event EventHandler EndExportSubscriptionList;

        public event EventHandler BeginExportDocumentList;
        public event EventHandler EndExportdocumentList;


        private OfficeExcelHandler _handler;
        private bool cancel = false;

        public Exporter()
        {
            _handler = new OfficeExcelHandler();
        }

    
        

        private string[] CreateWorkerHeaderArray() 
        {
            string[] arr = {"COGNOME_UTENTE", "NOME_UTENTE", "DATA_NASCITA_UTENTE", "FISCALE", 
                                  "NAZIONALITA","COMUNE_NASCITA","COMUNE", "INDIRIZZO", 
                                  "CAP", "TELEFONO1", "MODIFICATO DA", "DATA ULTIMA MODIFICA"};


            return arr;
        }


        private string[] CreateSubscriptionHeaderArray()
        {
            string[] arr = {"FISCALE", "REGIONE","PROVINCIA", "SETTORE", 
                                  "ENTE", "AZIENDA","CONTRATTO", "LIVELLO",
                                  "PERIODO", "ANNO", "QUOTA"};


            return arr;
        }


        private string[] CreateDocumentHeaderArray()
        {
            string[] arr = {"FISCALE", "REGIONE","PROVINCIA", "DATA_DOCUMENTO", 
                                  "TIPO_DOCUMENTO", "STATO_DOCUMENTO","NOTE"};


            return arr;
        }


        public void CloseExporter()
        {
            if (_handler != null)
                _handler.KillAllExcelProcesses();
        }
    

         public void SaveAs(string filename)
         {
             if (_handler != null)
                 if (_handler.CurrentWorkbook != null)
                    _handler.SaveAs(filename);
         }
        

         public void Export(IBindingList list)
         {

        
             //lancio l'evento dfi inizio esportazione
            BeginExportWorkerList(this,new EventArgs ());

            if (cancel)
                return;

            WriteWorkersWorkbook(1, "Lavoratori", list);

             //lancio l'evento di fine export lavoratori
            EndExportWorkerList(this, new EventArgs());


            //lancio l'evento dfi inizio esportazione
            BeginExportSubscriptionList (this, new EventArgs());

            if (cancel)
                return;

            WriteWorkersWorkbook(2, "Iscrizioni", list);

            //lancio l'evento di fine export lavoratori
            EndExportSubscriptionList(this, new EventArgs());




            //lancio l'evento dfi inizio esportazione
            BeginExportDocumentList(this, new EventArgs());

            if (cancel)
                return;

            WriteWorkersWorkbook(3, "Documenti", list);

            //lancio l'evento di fine export lavoratori
            EndExportdocumentList(this, new EventArgs());

         }


         private void WriteWorkersWorkbook(int workBookindex, string workBookName, IBindingList dtos)
        {

            if (dtos.Count == 0 )
                return;

            if (string.IsNullOrEmpty(workBookName))
                workBookName = "Export " + workBookindex.ToString ();

            if (workBookName.Length >20)
                workBookName = workBookName.Substring(0,19);


            try
            {
                if (_handler.ExcelInstance == null)
                //'creo una nuova istanza di excel
                    _handler.OpenExcelApplication(true);
            

                if (_handler.CurrentWorkbook == null)
                //'Aggiungo un nuovo documento ad excel 
                    _handler.AddDocumentToExcel("");
            

                //'lo attivo
                _handler.ActivateWorkBook();




                //'seleziono lo sheet di interesse
                _handler.SelectSheetByIndex(workBookindex);
                _handler.SetCurrentSheetName ( workBookName);


                //'Creo le intestazioni
                string[] intestazioneArr;
                if (workBookindex == 1)
                    intestazioneArr = CreateWorkerHeaderArray();
                else if (workBookindex == 2)
                    intestazioneArr = CreateSubscriptionHeaderArray();
                else
                    intestazioneArr = CreateDocumentHeaderArray();
                    //'Inserisco l'intestazione
                _handler.FillExcelRow(_handler.CurrentSheet, 1, intestazioneArr, 0);



                //adesso ciclo su tutte le righe
                int row = 2;
                int rowSub = 2;
                int rowDoc = 2;
            
                foreach (WorkerDTO item in dtos)
	            {
            		 //bool continua = true;

                    //(('riempio la riga con i dati del lavoratore
                    if (workBookindex == 1)
                        FillWorkerRow(item, row);
                    //(('riempio la riga con i dati delle iscrizioni del lavoratore
                    if (workBookindex == 2)
                        FillSubscriptionRow(item,ref rowSub);
                    //(('riempio la riga con i dati dei documenti del lavoratore
                    if (workBookindex == 3)
                        FillDocumentRow(item, ref rowDoc);

                    //incremento la rownumber
                    row ++;

                    //inizializzo l'eventarg
                    RowExportedEventArgs a = new RowExportedEventArgs(Convert.ToInt32((100 * (row-2) / dtos.Count)),  cancel);
                    //lancio l'evento
                    ExportingRow(this,a);

                    cancel = a.Cancel;
                    
                    //se cancello esco
                    if (cancel)
                    {
                        _handler.Dispose();
                        return;
                    }
	            }

            }
            catch (Exception)
            {
                _handler.Dispose();
                throw;
            }



        

        }

         private void FillDocumentRow(WorkerDTO item, ref int row)
        {
             int i = 0;
            if (item.Documents != null)
            {
                foreach (DocumentDTO elem in item.Documents)
                {
                    _handler.FillExcelRow(_handler.CurrentSheet, row, CreateDocumentArrayData(elem, item, i), 0);    
                    i++;
                    row++;
                }

                
            }
        }

        private object[] CreateDocumentArrayData(DocumentDTO elem, WorkerDTO item, int i)
        {
            Object[] values = new Object[7];

            string[] arr = {"FISCALE", "REGIONE","PROVINCIA", "DATA_DOCUMENTO", 
                                  "TIPO_DOCUMENTO", "STATO_DOCUMENTO","NOTE"};



            if (i == 0)
                values[0] = item.Fiscalcode;
            else
                values[0] = "";
            values[1] = elem.Region;
            values[2] = elem.Province;
            values[3] = elem.DocumentDate;
            values[4] = elem.DocumentType;
            values[5] = elem.State;
            values[6] = elem.Notes;
           

            return values;
        }

       
     

        private void FillSubscriptionRow(WorkerDTO item, ref int  row)
        {
            int i = 0;
            if (item.Subscriptions != null)
            {
                foreach (SubscriptionDTO elem in item.Subscriptions)
                {
                    _handler.FillExcelRow(_handler.CurrentSheet, row, CreateSubscriptionArrayData(elem, item, i), 0);    
                    i++;
                    row++;
                }

                
            }
        }

        private object[] CreateSubscriptionArrayData(SubscriptionDTO item, WorkerDTO worker, int setCode)
        {
            Object[] values = new Object[11];

            if (setCode == 0)
                values[0] = worker.Fiscalcode;
            else
                values[0] = "";
            values[1] = item.Region;
            values[2] = item.Province;
            values[3] = item.Sector;
            values[4] = item.Entity;
            values[5] = item.EmployCompany;
            values[6] = item.Contract;
            values[7] = item.Level;
            if (item.Semester == -1)
                values[8] = "";
            else
                values[8] = item.Semester;
            values[9] = item.Year;
            values[10] = item.Quota;
            

            return values;
        }

        private void FillWorkerRow(WorkerDTO item, int row)
        {
             Object[] values;
        
            values = CreateWorkerArrayData(item);

            _handler.FillExcelRow(_handler.CurrentSheet, row, values, 0);
        }

        private object[] CreateWorkerArrayData(WorkerDTO item)
        {
            Object[] values = new Object[12];

            values[0] = item.Surname;
            values[1] = item.Name;
            values[2] = item.BirthDate.ToShortDateString();
            values[3] = item.Fiscalcode ;
            values[4] = item.Nationality;
            values[5] = item.BirthPlace;
            values[6] = item.LivingPlace;
            values[7] = item.Address;
            values[8] = item.Cap ;
            values[9] = item.Phone;
            values[10] = item.LastModifier ;
            values[11] = item.LastUpdate.ToShortDateString();

            return values;
        }
     
    }



    public class RowExportedEventArgs : EventArgs
    {
        public RowExportedEventArgs(int rowPercentage,  bool cancel)
        {
            this.Cancel = cancel ;
            this.RowPercentage = rowPercentage ;
        }

       
        public bool Cancel;
        public int RowPercentage;

    }
}
