using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.WEBCONNECTOR.Credential;
using WIN.WEBCONNECTOR.FenealgestServices;
using WIN.WEBCONNECTOR.ExcelExport;
using System.IO;
using System.Diagnostics;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.GUI.UTILITY;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using DevExpress.XtraPrinting;

namespace WIN.WEBCONNECTOR
{

    public enum StatusMode
    {
        View,
        Send
    }

    public partial class FrmViewData : Form
    {
        StatusMode _mode = StatusMode.View;
    //    Exporter exporter;
        ValidatorHandler v;
        FrmElaborazioneInCorso frmElab;
        TraceExporter _exporter;
        int _currentIndex = 0;
        int _currentIndex1 = 0;

        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace _trace;
        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO _worker;
        IBindingList _workers = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();
       // FrmCredential frmCredential;

        public FrmViewData(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace trace)
        {
            InitializeComponent();

            _exporter = new TraceExporter(trace, null);
            _exporter.BeginExport += new EventHandler(_exporter_BeginExport);
            _exporter.EndExport += new EventHandler(_exporter_EndExport);
            _exporter.ExportingRow += new TraceExporter.TraceExportEventHandler(_exporter_ExportingRow);
           
            _trace = trace;
            _mode = StatusMode.Send;
            cmdSend.Visible  = true;
            cmdExportExcel.Visible = false;
            cmdChange.Visible = true;
            cmdFilter.Visible = false;
            cmdRemoveFilter.Visible = false;

            CreateValidator();
            ValidateTrace();

            LoadWorkers(new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>( new List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(_trace.Workers)));

        }

        void v_ExportingRow(object sender, RowValidatedEventArgs fe)
        {
            frmElab.SetPercentageValue(fe.RowPercentage);
            frmElab.SetActivity("Validazione in corso. Percentuale elementi validati: " + fe.RowPercentage.ToString());
            Application.DoEvents();
        }

       

        private void ValidateTrace()
        {
            //eseguo la validazione del trace creato
            try
            {
                //Helper.ShowWaitBox("Attendere. Validazione dati in corso...", Properties.Resources.Waiting3);
                frmElab = new FrmElaborazioneInCorso();
                frmElab.EnableCancel(false);
                frmElab.TopLevel = true;

                //visualizzo il form di stato esecuzione
                frmElab.Show();


               // CreateValidator();

                v.Validate();


                frmElab.Dispose();

                lblErrNumber.Text = v.IncorrectWorkers.Length.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Helper.HideWaitBox();
            }
        }

        private void CreateValidator()
        {
            v = new ValidatorHandler("Feneal", _trace, new GeoElementChecker());
            v.ExportingRow += new ValidatorHandler.RowValidatedEventHandler(v_ExportingRow);

        }


        public FrmViewData(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO worker)
        {
            InitializeComponent();

            _worker = worker;

            _workers = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();
            _workers.Add(_worker);

            _mode = StatusMode.View ;
            

            cmdSend.Visible = false;
            cmdExportExcel.Visible = true;
            cmdChange.Visible = false;

            IBindingList list = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();
            list.Add(worker);


           // List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> l = (List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>)list;

           


            LoadWorkers(list);
        }

        private void ValidateWorkers(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO[] workerDTO)
        {
            ValidatorHandler val = new ValidatorHandler("Feneal", new GeoElementChecker());

           lblErrNumber.Text =  val.ValidateWorkerArray(workerDTO).ToString();
        }


        public FrmViewData(IBindingList workers)
        {
            InitializeComponent();
            _workers = workers;
            _mode = StatusMode.View;
            cmdSend.Visible = false;
            cmdExportExcel.Visible = true;
            cmdChange.Visible = false;


            //List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> l = (List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>)workers;

            //ValidateWorkers(l.ToArray());

            IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> l = new List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();

            //tolgo gli elementi non trovati
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in workers)
            {
                if (!string.IsNullOrEmpty(item.Surname))
                    l.Add(item);
            }

            _workers = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(l);
            LoadWorkers(_workers);

        }

        public FrmViewData(IBindingList workers, string message)
        {
            InitializeComponent();

            txtErrors.Text = message;

            _workers = workers;
            _mode = StatusMode.View;
            cmdSend.Visible = false;
            cmdExportExcel.Visible = true;
            cmdChange.Visible = false;


            //List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> l = (List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>)workers;

            //ValidateWorkers(l.ToArray());



            LoadWorkers(workers);

        }


        private void LoadWorkers(IBindingList dtos)
        {
            InitializeGrids();

            //serve per la gestione del master detail della griglia devexpress
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in dtos)
            {
                item.AllineaListe();
            }

            gridLavoratori.DataSource = dtos;
            gridControlLavoratori1.DataSource = dtos;
            //gridView1.BestFitColumns();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            cmdSend.Enabled = false;
            try
            {
                if (_trace == null)
                    return;
                if (_trace != null)
                    if (_trace.Workers == null)
                        return;

                if (_trace != null)
                    if (_trace.Workers != null)
                        if (_trace.Workers.Length == 0)
                        {
                            MessageBox.Show("Nulla da esportare", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }







                //verifico la connessiona a internet
                //verifico per prima cosa la connessione a internet
                if (Properties.Settings.Default.CheckInternetConnection)
                {
                    if (!InternetConnectionChecker.IsConnectedToInternet())
                    {
                        throw new Exception("Connessione ad internet non disponibile!");
                    }
                }


                //rivalido la traccia;
                CreateValidator();

                frmElab = new FrmElaborazioneInCorso();
                frmElab.EnableCancel(false);
                frmElab.TopLevel = true;
                frmElab.Show();
                v.Validate();

                frmElab.Dispose();

                //a questo punto conosco il numero di elementi che verranno esportati ed eventuali errori

                bool continueExport = CommunicateResultToUser(v);

                //posso procedere
                if (continueExport)
                {

                    frmElab = new FrmElaborazioneInCorso();
                    frmElab.EnableCancel(true);
                    frmElab.TopLevel = true;

                    //visualizzo il form di stato esecuzione
                    frmElab.Show();

                    _exporter.SetValidator(v); ;

                    _exporter.Export(Convert.ToInt32(Properties.Settings.Default.PacketSize));

                    frmElab.Dispose();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmdSend.Enabled = true;
            }
            
        }

        void _exporter_ExportingRow(object sender, TraceExportedEventArgs fe)
        {
            if (frmElab.Annulla)
            {
                if (MessageBox.Show("Sicuro di voler annullare l'esportazione dei dati?", "Messaggio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Operazione annullata dall'utente", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fe.Cancel = true;
                }
                else
                {
                    frmElab.Annulla = false;
                }
            }
            



            frmElab.SetPercentageValue(fe.RowPercentage);
            frmElab.SetActivity("Percentuale di esportazione: " + fe.RowPercentage.ToString());
            Application.DoEvents();
        }

        void _exporter_EndExport(object sender, EventArgs e)
        {
            frmElab.SetPercentageValue(100);
            frmElab.SetActivity("Esportazione terminata");
            MessageBox.Show("Esportazione terminata", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Application.DoEvents();
        }

        void _exporter_BeginExport(object sender, EventArgs e)
        {
            frmElab.SetPercentageValue(0);
            frmElab.SetActivity("Inizio esportazione lavoratori al database regionale");
            Application.DoEvents();
        }

      

        

        private bool CommunicateResultToUser(ValidatorHandler v)
        {
            bool result;
            string message = "";
            int errNumber = v.IncorrectWorkers .Length;
            int correctNumber = v.CorrectTrace.Workers.Length;

            if (errNumber > 0)
                message = "Sono presenti num. " + errNumber.ToString() + " errori. " + Environment .NewLine;

            if (correctNumber > 0)
            {
                message += "L'esportazione dei dati può continuare: saranno esportati num. " + correctNumber.ToString() + " elementi!";
                if (MessageBox.Show(message,"Domanda", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes)
                    result = true;
                else
                    result = false;
            }
            else
            {
                message += "L'esportazione non può continuare poichè non ci sono dati corretti!";
                MessageBox.Show(message, "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                result = false;
            }

            return result;
        }

       

        private void StartSendDataProcess()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (_mode != StatusMode.View)
                    return;


                PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                link.Component = gridControlLavoratori1;
                link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                link.ShowPreview();

                ////instanzio l'exporter e gli associo gli eventi
                //exporter = new Exporter();
                //exporter.BeginExportDocumentList += new EventHandler(exporter_BeginExportDocumentList);
                //exporter.BeginExportSubscriptionList += new EventHandler(exporter_BeginExportSubscriptionList);
                //exporter.BeginExportWorkerList += new EventHandler(exporter_BeginExportWorkerList);
                //exporter.EndExportdocumentList += new EventHandler(exporter_EndExportdocumentList);
                //exporter.EndExportSubscriptionList += new EventHandler(exporter_EndExportSubscriptionList);
                //exporter.EndExportWorkerList += new EventHandler(exporter_EndExportWorkerList);
                //exporter.ExportingRow += new Exporter.RowExportEventHandler(exporter_ExportingRow);



                //frmElab = new FrmElaborazioneInCorso();

                //frmElab.EnableCancel(true);
                //frmElab.TopLevel = true;

                ////visualizzo il form di stato esecuzione
                //frmElab.Show();

                ////avvio l'esportazione



                //exporter.Export(_workers ); ;

                ////chiudo il form al termine dell'elaborazione
                //frmElab.Dispose();


                ////salvo il file ottenuot


                //string savePath = "";
                //saveFileDialog1.Title = "Inserire il percorso per salvare il file";


                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    savePath = saveFileDialog1.FileName;
                //    if (savePath.EndsWith("\\"))
                //        savePath = savePath + "Export_Lavoratori";
                    
                //    savePath = savePath + ".xls";
                //    exporter.SaveAs(savePath);
                //}
               

                ////'chiudo i processi excel
                //exporter.CloseExporter();


                ////'se ho salvato il file lo apro
                //if (File.Exists(savePath))
                //    Process.Start(savePath);
                




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void exporter_ExportingRow(object sender, RowExportedEventArgs fe)
        {
             if (frmElab.Annulla)
             {
                    if ( MessageBox.Show("Sicuro di voler annullare l'esportazione dei dati?", "Messaggio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
                    {   MessageBox.Show("Operazione annullata dall'utente", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fe.Cancel = true;
                    }
                    else
                    {
                        frmElab.Annulla = false;
                    }
             }
            frmElab.SetPercentageValue(fe.RowPercentage);
            frmElab.SetActivity("Percentuale di esportazione: " + fe.RowPercentage.ToString());
            Application.DoEvents();
        }

        void exporter_EndExportWorkerList(object sender, EventArgs e)
        {
            
            //
        }

        void exporter_EndExportSubscriptionList(object sender, EventArgs e)
        {
            //
        }

        void exporter_EndExportdocumentList(object sender, EventArgs e)
        {
            MessageBox.Show("Esportazione terminata", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void exporter_BeginExportWorkerList(object sender, EventArgs e)
        {
            frmElab.SetPercentageValue(0);
            frmElab.SetActivity("Inizio esportazione lavoratori su excel");
            Application.DoEvents();
        }

        void exporter_BeginExportSubscriptionList(object sender, EventArgs e)
        {
            frmElab.SetPercentageValue(0);
            frmElab.SetActivity("Inizio esportazione iscrizioni lavoratori su excel");
            Application.DoEvents();
        }

        void exporter_BeginExportDocumentList(object sender, EventArgs e)
        {
            frmElab.SetPercentageValue(0);
            frmElab.SetActivity("Inizio esportazione documenti lavoratori su excel");
            Application.DoEvents();
        }

        private void FrmViewData_Load(object sender, EventArgs e)
        {

           
        }

        private void InitializeGrids()
        {
            gridLavoratori.AutoGenerateColumns = false;
            GrigDocumenti.AutoGenerateColumns = false;
            gridIscrizioni.AutoGenerateColumns = false;

            if (_mode == StatusMode.View)
            {
                button1.Visible = false;
                button2.Visible = false;

                gridLavoratori.Columns[0].Visible = false;
                gridLavoratori.Columns["Nazionalita"].Visible = true;
                gridLavoratori.Columns["LastModifier"].Visible = true;
                gridLavoratori.Columns["LastUpdate"].Visible = true;

                //num riga
                gridView1.Columns[0].Visible = false;
                //nazionalità
                gridView1.Columns[5].Visible = true;
                //ultimo a modificare
                gridView1.Columns[11].Visible = true;
                //ultima modifica
                gridView1.Columns[12].Visible = true;


                gridView2.ViewCaption = "Iscrizioni";
                gridView3.ViewCaption = "Documenti";


                //imposto le regole di visibilità
                gridControlLavoratori1.Visible = true;
                gridControlLavoratori1.Dock = DockStyle.Fill;
                splitContainer1.Panel2Collapsed = true;

                gridLavoratori.Visible = false;
                gridLavoratori.Dock = DockStyle.None;


            }
            else
            {
                button1.Visible = true;
                button2.Visible = true;


                gridLavoratori.Columns["Nazionalita"].Visible = false;
                gridLavoratori.Columns["LastModifier"].Visible = false;
                gridLavoratori.Columns["LastUpdate"].Visible = false;


                //imposto le regole di visibilità
                gridControlLavoratori1.Visible = false;
                gridControlLavoratori1.Dock = DockStyle.None;
                splitContainer1.Panel2Collapsed = false;

                gridLavoratori.Visible = true;
                gridLavoratori.Dock = DockStyle.Fill;


                ////nazionalità
                //gridView1.Columns[5].Visible = false;
                ////ultimo a modificare
                //gridView1.Columns[11].Visible = false;
                ////ultima modifica
                //gridView1.Columns[12].Visible = false;



                //gridView1.Columns["colNationality"].Visible = false;
                //gridView1.Columns["colLastModifier"].Visible = false;
                //gridView1.Columns["colLastUpdate"].Visible = false;
            }

            
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            if (_mode != StatusMode.Send)
                return;

            if (_trace == null)
                return;

            FrmChangeExportParameters frm = new FrmChangeExportParameters(_trace.ExporterName, _trace.ExporterMail);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Helper.ShowWaitBox("Attendere cambio parametri di input...", Properties.Resources.Waiting3);
                    ReCreateTrace(frm.InputHeader);
                    LoadWorkers(new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(new List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(_trace.Workers)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Helper.HideWaitBox();
                }
                
            }
            frm.Dispose();
        }

        private void ReCreateTrace(WIN.WEBCONNECTOR.FileReaders.InputHeader header)
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
                

            CorrectWorkersArray(header);
        }

        private void CorrectWorkersArray(WIN.WEBCONNECTOR.FileReaders.InputHeader header)
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

        private void gridLavoratori_RowEnter(object sender, DataGridViewCellEventArgs e)
        {


            


            WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO current = (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO)gridLavoratori.Rows[e.RowIndex].DataBoundItem;

            if (current != null)
            {

                //visualizzo eventuali errori
                if (_mode == StatusMode.Send)
                    txtErrors.Text = current.Errors;

                //visualizzo le sottoscrizioni
                if (current.Subscriptions != null)
                    gridIscrizioni.DataSource = current.Subscriptions;
                else
                {
                    if (current.Subscription != null)
                    {
                        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO[] l = new WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO[] { current.Subscription };

                        gridIscrizioni.DataSource = l;
                    }
                    else
                    {
                        gridIscrizioni.DataSource = null;
                    }
                }

                //visualizzo i docuemnti
                if (current.Documents != null)
                    GrigDocumenti.DataSource = current.Documents;
                else
                {
                    GrigDocumenti.DataSource = null; 
                }


            }
        }

        private void gridLavoratori_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_mode == StatusMode.Send)
            {
                WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO w = gridLavoratori.Rows[e.RowIndex].DataBoundItem as WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO;
                if (w != null)
                {
                    if (!w.IsValid)
                        gridLavoratori.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;

                    if (gridLavoratori.Columns[e.ColumnIndex].Name.Equals("ComuneResidenza"))
                    {
                        if (!w.ExistLivingPlace)
                        {
                            gridLavoratori.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                            e.CellStyle.BackColor = Color.OliveDrab;
                        }
                    }


                }
            }
                        
        }

        private void gridLavoratori_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void gridLavoratori_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex != -1) && (e.RowIndex != -1))
            {

                if (gridLavoratori.Columns[e.ColumnIndex].Name != "ComuneResidenza")
                    return;

                FrmSelezionaComune frm = new FrmSelezionaComune();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    WIN.FENGEST_NAZIONALE .INTEGRATION_ENTITIES .WorkerDTO w = gridLavoratori.Rows[e.RowIndex].DataBoundItem as WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO;
                    if (w != null)
                    {
                        w.LivingPlace = frm.Comune;
                        w.ExistLivingPlace = true;
                       // gridLavoratori.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = SystemColors.AppWorkspace;
                    }
                }
                frm.Dispose();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (_currentIndex >= _trace.Workers.Length - 1)
                {
                    _currentIndex = -1;
                }

                while (_currentIndex + 1 <= _trace.Workers.Length -1)
                {
                    _currentIndex++;
                    WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO s = gridLavoratori.Rows[_currentIndex].DataBoundItem as WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO;
                    if (!s.IsValid)
                    {
                        gridLavoratori.FirstDisplayedScrollingRowIndex = _currentIndex;
                       // gridLavoratori.Rows[_currentIndex].Selected = true;

                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Errore",  MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }

                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentIndex1 >= _trace.Workers.Length - 1)
                {
                    _currentIndex1 = -1;
                }

                while (_currentIndex1 + 1 <= _trace.Workers.Length - 1)
                {
                    _currentIndex1++;
                    WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO s = gridLavoratori.Rows[_currentIndex1].DataBoundItem as WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO;
                    if (!s.ExistLivingPlace)
                    {
                        gridLavoratori.FirstDisplayedScrollingRowIndex = _currentIndex1;
                        

                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            FrmAnnoIscrizione frmAnno = new FrmAnnoIscrizione();
            if (frmAnno.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string anni = frmAnno.AnnoIscrizione;

                if (string.IsNullOrEmpty(anni))
                    return;

                string[] anniSplitted = anni.Split(';');

                IBindingList resultToShow = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();

                foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers)
                {
                    if (HasSubscriptionsInYear(item, anniSplitted))
                    {
                        resultToShow.Add(item);
                    }
                }

                //visualizzo tutto in griglia
                gridLavoratori.DataSource = null;
               // gridLavoratori.DataSource = resultToShow;

                gridLavoratori.DataSource = resultToShow;
                gridControlLavoratori1.DataSource = resultToShow;
            }
            frmAnno.Dispose();



        }

        private bool HasSubscriptionsInYear(FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO worker, string[] anniSplitted)
        {

            bool result = false;

            if (worker.Subscriptions == null)
                return result;

            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO  item in worker.Subscriptions )
            {
                foreach (string filter in anniSplitted )
	            {
                    if (item.Year.ToString().Equals(filter))
                        return true;
	            }
                

            }

            return result;
        }

        private void cmdRemoveFilter_Click(object sender, EventArgs e)
        {
            gridLavoratori.DataSource = null;
          
            gridControlLavoratori1.DataSource = null;

            gridLavoratori.DataSource = _workers;
            gridControlLavoratori1.DataSource = _workers;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FrmCrossLiberiDataWithLiberiFile frm = new FrmCrossLiberiDataWithLiberiFile(_workers);

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gridLavoratori.DataSource = null;

                gridControlLavoratori1.DataSource = null;

                gridLavoratori.DataSource = _workers;
                gridControlLavoratori1.DataSource = _workers;
            }

            frm.Dispose();
        }


    }
}
