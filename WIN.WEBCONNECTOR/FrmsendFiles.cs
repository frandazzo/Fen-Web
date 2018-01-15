using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using WIN.TECHNICAL.MIDDLEWARE.Listeners;
using System.IO;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.WEBCONNECTOR.FileReaders;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using WIN.WEBCONNECTOR.Credential;
using WIN.TECHNICAL.MIDDLEWARE.Files;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmsendFiles : Form
    {
        bool readPath = false;
        bool excelFileRead  = false;
        IQATrace t;
        LiberiTrace t1;
        InpsTrace t2;
        bool reading;

        public FrmsendFiles()
        {
            InitializeComponent();
            cboEnte.SelectedIndex = 0;
            cboSettore.SelectedIndex = 0;
            cboSem.SelectedIndex = 0;
            numericUpDown1.Value = DateTime.Now.Year;

            Trace.Listeners.Add(new TextBoxTraceListener(txtTask));

        }

        private void optIQA_CheckedChanged(object sender, EventArgs e)
        {
            if (optIQA.Checked)
            {
                groupBox2.Enabled = true;
               
            }
            else
                groupBox2.Enabled = false;

            if (optIQA.Checked || optLib.Checked)
                cmdPath.Visible = false;
            else
                cmdPath.Visible = true;
        }

        private void optLib_CheckedChanged(object sender, EventArgs e)
        {
            if (optIQA.Checked)
            {
                groupBox2.Enabled = true;
               
            }
            else
                groupBox2.Enabled = false;


            if (optIQA.Checked || optLib.Checked)
                cmdPath.Visible = false;
            else
                cmdPath.Visible = true;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            //INVIO IL FILE

            try
            {
                if (optIQA.Checked)
                {
                    //invio in file iqa
                    if (t == null)
                        return;

                    //a questo punto posso inviare il file previa validazione
                    t.Validate();

                    if (t.ValidationErrors.Count > 0)
                    {
                        MessageBox.Show("Si sono verificati errori di validazione che andranno corretti!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowErrors(t.ValidationErrors);
                        return;
                    }

                    //ora posso inviare il file

                    //devo creare il file
                    string temp = Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml";
                    //salvo  e invio
                    ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.IQATrace>.Save(t, temp);

                    string error = SendFile(temp, true);

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("File non inviato: " + error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("File inviato con successo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    t = null;

                }
                else if (optLib.Checked)
                {
                    //invio un file liberi
                    if (t1 == null)
                        return;

                    //a questo punto posso inviare il file previa validazione
                    t1.Validate();

                    if (t1.ValidationErrors.Count > 0)
                    {
                        MessageBox.Show("Si sono verificati errori di validazione che andranno corretti!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowErrors(t1.ValidationErrors);
                        return;
                    }

                    //ora posso inviare il file
                    //devo creare il file
                    string temp = Path.GetTempPath() + "qqq.xml";
                    //salvo  e invio
                    ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTrace>.Save(t1, temp);

                    string error = SendFile(temp, false);

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("File non inviato: " + error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("File inviato con successo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    t1 = null;
                }
                else
                {
                    //invio un file quote inps
                    if (t2 == null)
                        return;

                    //a questo punto posso inviare il file previa validazione
                    t2.Validate();

                    if (t2.ValidationErrors.Count > 0)
                    {
                        MessageBox.Show("Si sono verificati errori di validazione che andranno corretti!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowErrors(t2.ValidationErrors);
                        return;
                    }

                    //ora posso inviare il file
                    //devo creare il file
                    string temp = Path.GetTempPath() + "qqq.xml";
                    //salvo  e invio
                    ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace>.Save(t2, temp);

                    string error = SendFileInps(temp);

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("File non inviato: " + error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("File inviato con successo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    t2 = null;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string SendFile(string sFile, bool isIQA)
        {
            FileStream objfilestream = new FileStream(sFile, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            Byte[] mybytearray = new Byte[len];
            objfilestream.Read(mybytearray, 0, len);

            FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
            FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
            w.Timeout = 1000000;
            c.UserName = CredentialDB.Instance.UserName;
            c.Password = CredentialDB.Instance.Password;
            c.Province = CredentialDB.Instance.Province;

            //gli imposto l'intestazione
            w.CredenzialiValue = c;
            //cerco il file o nella cartella dell'assembly corrente o nella cartella noesis dei programmi
            bool localFile = ClassPathFileFinder.ExistFileInExecutingAssemblyPathOrInAncestorFolder("OnlyLocal.txt", "NOESIS");
            //se lo trovo allora si tratta di una installazione serverside
            string error  = "";
            if (localFile)
                error = w.SaveImportExportFileFromServerSideAnalysis(mybytearray,isIQA);
            else
                error = w.SaveImportExportFile(mybytearray, isIQA);
            objfilestream.Close();


            return error;
        }
        //https://www.fenealgest.it/servizi/WebServices/FenealgestwebServices.asmx

        private string SendFileInps(string sFile)
        {
            FileStream objfilestream = new FileStream(sFile, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            Byte[] mybytearray = new Byte[len];
            objfilestream.Read(mybytearray, 0, len);

            FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
            FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
            c.UserName = CredentialDB.Instance.UserName;
            c.Password = CredentialDB.Instance.Password;
            c.Province = CredentialDB.Instance.Province;

            //gli imposto l'intestazione
            w.CredenzialiValue = c;



            //cerco il file o nella cartella dell'assembly corrente o nella cartella noesis dei programmi
            bool localFile = ClassPathFileFinder.ExistFileInExecutingAssemblyPathOrInAncestorFolder("OnlyLocal.txt", "NOESIS");
            //se lo trovo allora si tratta di una installazione serverside
            string error = "";
            if (localFile)
                error = w.SaveImportExportFileInpsForServerSideAnalysis(mybytearray);
            else
                error = w.SaveImportExportFileInps(mybytearray);

          
            objfilestream.Close();


            return error;
        }

        private void ShowErrors(IList<string> iList)
        {
            string tempPath = Path.GetTempPath() + "errori.txt";

            if (File.Exists(tempPath))
                File.Delete(tempPath);

            foreach (string item in iList)
            {
                File.AppendAllText(tempPath, item + Environment.NewLine);
            }

            Process.Start(tempPath);

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {

            string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            if (string.IsNullOrEmpty(txtsub.Text))
            {
                MessageBox.Show("Inserire unoggetto valido");
                return;
            }

            if (string.IsNullOrEmpty(txtMail.Text))
            {
                MessageBox.Show("Inserire una mail corretta");
                return;
            }

            //valido la mail
            if (!Regex.IsMatch(txtMail.Text, MatchEmailPattern))
            {
                MessageBox.Show("Inserire una mail corretta");
                return;
            }






            excelFileRead = true;
            openFileDialog1.Filter = "file excel 2000-2003 (*.xls)|*.xls|file excel 2007 (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lnkFile.Text = openFileDialog1.FileName;
                Import();
            }
        }

        private void Import()
        {
            cmdImport.Enabled = false;
            cmdImportXml.Enabled = false;
            cmdPath .Enabled = false;
            try
            {

                if (readPath)
                {
                    if (!Directory.Exists(lnkFile.Text))
                    {
                        MessageBox.Show("Il percorso selezionato non esiste", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    //per prima cosa verifico gli input
                    if (!File.Exists(lnkFile.Text))
                    {
                        MessageBox.Show("Il file selezionato non esiste", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                cmdClose.Enabled = false;
                cmdOk.Enabled = false;
                cmdView.Enabled = false;

                reading = true;
                if (optIQA.Checked)
                {
                    //importo le quote 
                    ImportIQA();
                }
                else if (optLib.Checked)
                {
                    //importo i liberi
                    ImportLiberi();
                }
                else
                {
                    ImportInps();
                }
                reading = false;

                //se l'oggetto è nullo mando un avviso
                //e non faccio nulla
                if (t == null && t1 == null && t2 == null)
                {
                    MessageBox.Show("Nessun oggetto trovato o caricato", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    cmdClose.Enabled = true;
                    cmdOk.Enabled = true;
                    cmdView.Enabled = true;
                    cmdPath.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                reading = false;
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                cmdImport.Enabled = true;
                cmdImportXml.Enabled = true;
                cmdPath.Enabled = true;

                //ripristino il readpath
                readPath = false;
            }
        }

        private void ImportInps()
        {
            t2 = null;
            t1 = null;
            t = null;
            //recupero gli input
            InputHeader h = new InputHeader();
            h.Mail = txtMail.Text;
            h.Struttura = txtsub.Text;
            //recupero il lettore
            IInpsReader reader = SendFileImportReaderFactory.GetInpsReader(lnkFile.Text, readPath);

            //ottengo l'oggetto
            t2 = reader.ReadTrace(lnkFile.Text, h);
        }

        private void ImportLiberi()
        {
            t2 = null;
            t1 = null;
            t = null;
            //recupero gli input
            InputHeader h = new InputHeader();
            h.Mail = txtMail.Text;
            h.Struttura = txtsub.Text;
            //recupero il lettore
            ILiberiReader reader = SendFileImportReaderFactory.GetLiberiReader(lnkFile.Text);

            //ottengo l'oggetto
            t1 = reader.ReadTrace(lnkFile.Text, h);

           
        }

        private void ImportIQA()
        {
            t2 = null;
            t1 = null;
            t = null;
            //recupero gli input
            InputHeader h = new InputHeader();
            h.Struttura = txtsub.Text;
            h.Entity = cboEnte.Text;
            h.Mail = txtMail.Text;
            h.Period = Convert.ToInt32(cboSem.Text);
            h.Year = (int)numericUpDown1.Value;


          

            //recupero il lettore
            IIQAReader reader = SendFileImportReaderFactory.GetIQAReader(lnkFile.Text);

            //ottengo l'oggetto
            t = reader.ReadTrace(lnkFile.Text, h);



           
        }

        private void cmdImportXml_Click(object sender, EventArgs e)
        {


            excelFileRead = false;
            openFileDialog1.Filter = "file xml (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lnkFile.Text = openFileDialog1.FileName;
                Import();
            }
        }

        private void lnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(lnkFile.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK,  MessageBoxIcon.Error);
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            if (reading)
                return;

            if (t != null)
                ViewIQA();
            else if (t1 != null)
                ViewLiberi();
            else if (t2 != null)
                ViewInps();

        }

        private void ViewInps()
        {

            FrmViewInps frm = new FrmViewInps(t2);

            frm.ShowDialog();

            frm.Dispose();
        }

        private void ViewIQA()
        {
            FrmViewIQA frm = new FrmViewIQA(t);

            frm.ShowDialog();

            frm.Dispose();
        }

        private void ViewLiberi()
        {
            FrmViewLiberi frm = new FrmViewLiberi(t1);

            frm.ShowDialog();

            frm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //creo un file temporaneo dove visualizzare i dati
                string path = Path.GetTempPath() + "pp.txt";

                if (File.Exists(path))
                    File.Delete(path);

                //devo scrivere su un file tutti i file presenti e le loro dimensioni


                //instanzio i servizi
                FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province;
                //gli imposto l'intestazione
                w.CredenzialiValue = c;


                //richiedo la lista dei file presenti
                string[] iqas = w.GetListOfFileToDownload("IQA");
                string[] lib = w.GetListOfFileToDownload("Liberi");
            
                 string[] inps = w.GetListOfFileToDownload("Inps");


                //verifico se c'è un errore
                string iqaError = CheckError(iqas);
                string libError = CheckError(lib);
                string inpsError = CheckError(inps);

                if (!string.IsNullOrEmpty(iqaError) || !string.IsNullOrEmpty(libError) || !string.IsNullOrEmpty(inpsError))
                {
                    MessageBox.Show(iqaError + Environment.NewLine + libError, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


               
                //se non ci sono errori proseguo nella visualizzazione
                var z = new string[iqas.Length + lib.Length + inps.Length];
                iqas.CopyTo(z, 0);
                lib.CopyTo(z, iqas.Length);
                inps.CopyTo(z, iqas.Length + lib.Length);

                if (z.Length == 0)
                {
                    MessageBox.Show("Nessun file trovato", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //adesso cerco la lunghezza di ogni elemento
                int index = 0;
                foreach (string item in z)
                {
                    int len = w.GetDocumentLen(item);
                    z[index] = z[index] + " - " + len.ToString();
                    index++;
                }


                //scrivo sul file e visulizzo il file
                foreach (string item in z)
                {
                    File.AppendAllText(path, item + Environment.NewLine);
                }

                Process.Start(path);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CheckError(string[] s)
        {
            if (s.Length == 1)
                if (s[0].StartsWith("ERRORE"))
                    return s[0];

            return "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //instanzio i servizi
                FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province;
                //gli imposto l'intestazione
                w.CredenzialiValue = c;


                //richiedo la lista dei file presenti
                string[] iqas = w.GetListOfFileToDownload("IQA");
                string[] lib = w.GetListOfFileToDownload("Liberi");
                string[] inps = w.GetListOfFileToDownload("Inps");


                //verifico se c'è un errore
                string iqaError = CheckError(iqas);
                string libError = CheckError(lib);
                string inpsError = CheckError(inps);

                if (!string.IsNullOrEmpty(iqaError) || !string.IsNullOrEmpty(libError) || !string.IsNullOrEmpty(inpsError))
                {
                    MessageBox.Show(iqaError + Environment.NewLine + libError, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                //se non ci sono errori proseguo nella visualizzazione
                var z = new string[iqas.Length + lib.Length + inps.Length];
                iqas.CopyTo(z, 0);
                lib.CopyTo(z, iqas.Length);
                inps.CopyTo(z, iqas.Length + lib.Length);

                if (z.Length == 0)
                {
                    MessageBox.Show("Nessun file trovato", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //ecco il primo file da rinominare
                string first = z[0];

                //lo visualizzo con una message box
                MessageBox.Show(first);

                // a questo punto procedo con la rinomina

                string error = w.RenameImportExportFile(first);

                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("File rinominato correttamente", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //instanzio i servizi
                FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province;
                //gli imposto l'intestazione
                w.CredenzialiValue = c;


                //richiedo la lista dei file presenti
                string[] iqas = w.GetListOfFileToDownload("IQA");
                string[] lib = w.GetListOfFileToDownload("Liberi");
                string[] inps = w.GetListOfFileToDownload("Inps");


                //verifico se c'è un errore
                string iqaError = CheckError(iqas);
                string libError = CheckError(lib);
                string inpsError = CheckError(inps);

                if (!string.IsNullOrEmpty(iqaError) || !string.IsNullOrEmpty(libError) || !string.IsNullOrEmpty(inpsError))
                {
                    MessageBox.Show(iqaError + Environment.NewLine + libError, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                //se non ci sono errori proseguo nella visualizzazione
                var z = new string[iqas.Length + lib.Length + inps.Length ];
                iqas.CopyTo(z, 0);
                lib.CopyTo(z, iqas.Length);
                inps.CopyTo(z, iqas.Length + lib.Length);
                if (z.Length == 0)
                {
                    MessageBox.Show("Nessun file trovato", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //ecco il primo file da rinominare
                string first = z[0];

                //lo visualizzo con una message box
                MessageBox.Show(first);

                // a questo punto procedo con il download del file

                //ma prima recupero il nome del file
                string sFile = "C:\\ImportExport\\" + first;


                MemoryStream objstreaminput = new MemoryStream();
                FileStream objfilestream = new FileStream(sFile, FileMode.Create, FileAccess.ReadWrite);


                int len = w.GetDocumentLen(first);
                Byte[] mybytearray = new Byte[len];
                mybytearray = w.GetDocument(first);
                objfilestream.Write(mybytearray, 0, len);
                objfilestream.Close();

                //una volta ottenuto il file lo rinomino sul server cosi non comparirà piu'

                string error = w.RenameImportExportFile(first);

                if (!string.IsNullOrEmpty(error))
                    MessageBox.Show(error, "Errore nel rinominare il file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("File scaricato e rinominato correttamente", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            JoinAllServices.IQATrace trace = new JoinAllServices.IQATrace();

            trace.Anno = 2019;
            trace.Entity = "CASSA EDILE";
            trace.Mailto = "sissio80@hotmail.com";
            trace.Period = 1;
            trace.Provincia = "MILANO";
            trace.Subject = "prova";

            JoinAllServices.IqaTraceDetail[] dets = new JoinAllServices.IqaTraceDetail[] { };


            JoinAllServices.IqaTraceDetail det1 = new JoinAllServices.IqaTraceDetail();
            det1.COGNOME_UTENTE = "randazzo";
            det1.NOME_UTENTE = "francesco";
           
            det1.DATA_NASCITA_UTENTE = "14/07/1977";
            det1.FISCALE = "rndfnc77l14f052f";
            det1.SETTORE = "EDILE";
            det1.ENTE = "CASSA EDILE";
            det1.AZIENDA_IMPIEGO = "Costruzioni srl";
            det1.DATA_INIZIO = "01/04/2013";
            det1.DATA_FINE = "30/09/2013";
            det1.QUOTA = "0,01";


            JoinAllServices.IqaTraceDetail det2 = new JoinAllServices.IqaTraceDetail();
            det2.COGNOME_UTENTE = "colomba   ";
            det2.NOME_UTENTE = "silvana";
            det2.DATA_NASCITA_UTENTE = "03/11/1974";
            det2.FISCALE = "clmsvn77l14f052f";
            det2.SETTORE = "EDILE";
            det2.ENTE = "CASSA EDILE";
            det2.AZIENDA_IMPIEGO = "Edil lop";
            det2.DATA_INIZIO = "01/04/2013";
            det2.DATA_FINE = "30/09/2013";
            det2.QUOTA = "0,01";





            Array.Resize<JoinAllServices.IqaTraceDetail>(ref dets, dets.Length + 1);
            dets[dets.Length - 1] = det1;

            Array.Resize<JoinAllServices.IqaTraceDetail>(ref dets, dets.Length + 1);
            dets[dets.Length - 1] = det2;

            trace.IqaTraceDetails = dets;

            // a questo punto posso inviare
            JoinAllServices.JoinAllWebService svc = new JoinAllServices.JoinAllWebService();
            string result = svc.ImportIQA("fenealmilano", "p_milano", trace);

            MessageBox.Show(result);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            JoinAllServices.LiberiTrace trace = new JoinAllServices.LiberiTrace();


            trace.Mailto = "fg.randazzo@hotmail.it";
            trace.Subject = "prova";
            trace.Provincia = "MILANO";

            JoinAllServices.LiberiTraceDetail[] dets = new JoinAllServices.LiberiTraceDetail[] { };


            JoinAllServices.LiberiTraceDetail det1 = new JoinAllServices.LiberiTraceDetail();
            det1.COGNOME_UTENTE = "randazzo";
            det1.NOME_UTENTE = "francesco";
            det1.DATA_NASCITA_UTENTE = "14/07/1977";
            det1.FISCALE = "rndfnc77l14f052f";

            det1.ENTE = "CASSA EDILE";
            det1.AZIENDA_IMPIEGO = "Costruzioni srl";
            det1.DATA = "01/04/2013";
        


            JoinAllServices.LiberiTraceDetail det2 = new JoinAllServices.LiberiTraceDetail();
            det2.COGNOME_UTENTE = "colomba   ";
            det2.NOME_UTENTE = "silvana";
            det2.DATA_NASCITA_UTENTE = "03/11/1974";
            det2.FISCALE = "clmsvn77l14f052f";

            det2.ENTE = "CASSA EDILE";
            det2.AZIENDA_IMPIEGO = "Edil lop";
            det2.DATA = "01/04/2013";






            Array.Resize<JoinAllServices.LiberiTraceDetail>(ref dets, dets.Length + 1);
            dets[dets.Length - 1] = det1;

            Array.Resize<JoinAllServices.LiberiTraceDetail>(ref dets, dets.Length + 1);
            dets[dets.Length - 1] = det2;

            trace.LiberiTraceDetails = dets;

            // a questo punto posso inviare
            JoinAllServices.JoinAllWebService svc = new JoinAllServices.JoinAllWebService();
            string result = svc.ImportLiberi("fenealmilano", "p_milano", trace);

            MessageBox.Show("--" + result + "--");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FenealgestServices.Credenziali c = new FenealgestServices.Credenziali();
            c.UserName = "fenealpiacenza";
            c.Password = "piacenzafeneal";
            c.Province = "PIACENZA";

            FenealgestServices.FenealgestwebServices svc = new FenealgestServices.FenealgestwebServices();
            svc.CredenzialiValue = c;


            svc.SendMailForLiberiSearch(new string[]{"rndfnc77l14fo52f"}, "sissio80@hotmail.com", "prova");

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (optIQA.Checked)
            {
                groupBox2.Enabled = true;
               
            }
            else
                groupBox2.Enabled = false;

            if (optIQA.Checked || optLib.Checked)
                cmdPath.Visible = false;
            else
                cmdPath.Visible = true;

        }

        private void cmdPath_Click(object sender, EventArgs e)
        {
            string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            if (string.IsNullOrEmpty(txtsub.Text))
            {
                MessageBox.Show("Inserire unoggetto valido");
                return;
            }

            if (string.IsNullOrEmpty(txtMail.Text))
            {
                MessageBox.Show("Inserire una mail corretta");
                return;
            }

            //valido la mail
            if (!Regex.IsMatch(txtMail.Text, MatchEmailPattern))
            {
                MessageBox.Show("Inserire una mail corretta");
                return;
            }







            readPath = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lnkFile.Text = folderBrowserDialog1.SelectedPath ;
                Import();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
           

            FenealgestServices.FenealgestwebServices svc = new FenealgestServices.FenealgestwebServices();

            FenealgestServices.MailData m = new FenealgestServices.MailData();
            m.body = "sei scarso a biliardino";
            m.tos = new string[] { "faliero@ideama.it" };
            m.sender = "randy";
            m.subject = "prova mail";
            //, "prova mail", , "randy" 

           svc.SendMailFromFenealgest(m);
        }
    }
}
