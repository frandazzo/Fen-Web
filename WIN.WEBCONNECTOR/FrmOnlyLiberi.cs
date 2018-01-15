using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.TECHNICAL.MIDDLEWARE.Files;
using WIN.TECHNICAL.MIDDLEWARE.Listeners;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using WIN.WEBCONNECTOR.Credential;
using WIN.WEBCONNECTOR.FileReaders;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmOnlyLiberi : Form
    {
        bool readPath = false;
        bool excelFileRead = false;
     
        LiberiTrace t1;
        

        bool reading;

        public FrmOnlyLiberi()
        {
            InitializeComponent();
            
            Trace.Listeners.Add(new TextBoxTraceListener(txtTask));
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
           
            try
            {

                //per prima cosa verifico gli input
                if (!File.Exists(lnkFile.Text))
                {
                    MessageBox.Show("Il file selezionato non esiste", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            


                cmdClose.Enabled = false;
                cmdOk.Enabled = false;
                cmdView.Enabled = false;

                reading = true;
            
                ImportLiberi();
                
                reading = false;

                //se l'oggetto è nullo mando un avviso
                //e non faccio nulla
                if (t1 == null )
                {
                    MessageBox.Show("Nessun oggetto trovato o caricato", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    cmdClose.Enabled = true;
                    cmdOk.Enabled = true;
                    cmdView.Enabled = true;
                   
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
               

                //ripristino il readpath
                readPath = false;
            }
        }

        private void ImportLiberi()
        {
           
            t1 = null;
          
            //recupero gli input
            InputHeader h = new InputHeader();
            h.Mail = txtMail.Text;
            h.Struttura = txtsub.Text;
            //recupero il lettore
            ILiberiReader reader = SendFileImportReaderFactory.GetLiberiReader(lnkFile.Text);

            //ottengo l'oggetto
            t1 = reader.ReadTrace(lnkFile.Text, h);

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

        private void cmdView_Click(object sender, EventArgs e)
        {
            if (reading)
                return;

      
            if (t1 != null)
                ViewLiberi();
           
        }

        private void ViewLiberi()
        {
            FrmViewLiberi frm = new FrmViewLiberi(t1);

            frm.ShowDialog();

            frm.Dispose();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cmdOk_Click(object sender, EventArgs e)
        {
            //INVIO IL FILE

            try
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string error = "";
            if (localFile)
                error = w.SaveImportExportFileFromServerSideAnalysis(mybytearray, isIQA);
            else
                error = w.SaveImportExportFile(mybytearray, isIQA);
            objfilestream.Close();


            return error;
        }
    }
}
