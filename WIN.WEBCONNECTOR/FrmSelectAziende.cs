using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmSelectAziende : Form
    {
         string Azienda;
        public FrmSelectAziende()
        {
            InitializeComponent();
        }

        private void cmdImportList_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > Properties.Settings.Default.MaxFiscalCodeRequests)
            {
                MessageBox.Show("Limite di " + Properties.Settings.Default.MaxFiscalCodeRequests.ToString() + " codici fiscali raggiunto", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string file = "";
            openFileDialog1.Filter = "file excel 2000-2003 (*.xls)|*.xls|file excel 2007 (*.xlsx)|*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }


            if (File.Exists(file))
            {

                try
                {
                    ReadAndFillList(file);
                    label5.Text = "Codici da ricercare: " + listBox1.Items.Count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void ReadAndFillList(string file)
        {
            FileReaders.ExcelFiscalCodeReader r = new WIN.WEBCONNECTOR.FileReaders.ExcelFiscalCodeReader(file);

            IList l = r.ReadCodici();

            foreach (string item in l)
            {
                //if (listBox1.Items.Count > Properties.Settings.Default.MaxFiscalCodeRequests)
                //    break;

                if (!ListConainsCode(item.ToUpper()))
                    listBox1.Items.Add(item.ToUpper());
            }
        }

        private bool ListConainsCode(string code)
        {
            bool found = false;
            foreach (string item in listBox1.Items)
            {


                if (item.Equals(code))
                    found = true;

            }

            return found;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Azienda = listBox1.SelectedItem as string;
           
                try
                {


                    if (string.IsNullOrEmpty(Azienda))
                    {
                        MessageBox.Show("Selezionare il nome di una azienda!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                Azienda = Azienda.Trim();

                    //verifico per prima cosa la connessione a internet
                    if (Properties.Settings.Default.CheckInternetConnection)
                    {
                        if (!InternetConnectionChecker.IsConnectedToInternet())
                        {
                            throw new Exception("Connessione ad internet non disponibile!");
                        }
                    }


                    FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                    c.UserName = CredentialDB.Instance.UserName;
                    c.Password = CredentialDB.Instance.Password;
                    c.Province = CredentialDB.Instance.Province;

                    //creo il servizio
                    FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();


                    //gli imposto l'intestazione
                    service.CredenzialiValue = c;


                    //metto in attesa
                    WIN.GUI.UTILITY.Helper.ShowWaitBox("Attendere richiesta codici fiscali...", Properties.Resources.Waiting3);


                    //richiedo il dato
                    FenealgestServices.QueryResultDTO dto = service.SearchWorkersByAzienda(Azienda);

                    WIN.GUI.UTILITY.Helper.HideWaitBox();

                    // a questo punto posso verificare la presenza di un errore di elaborazione
                    if (!dto.IsResultValid)
                    {
                        MessageBox.Show(dto.Error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    // a questo punto posso inviare il dato in visualizzazione...

                    //prima lo trasformo
                    IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> worker = DTOTranslator.ToCustomWorkerDTOList(dto.Workers);
                    //poi lo invio al form per la visualizzazione

                    FrmViewDataForAzienda frm1 = new FrmViewDataForAzienda(new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(worker));
                    frm1.Show();



                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    WIN.GUI.UTILITY.Helper.HideWaitBox();
                }
            
        }
    }
}