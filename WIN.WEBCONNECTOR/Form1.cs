using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.WEBCONNECTOR.Credential;
using System.Reflection;
using System.IO;
using WIN.TECHNICAL.MIDDLEWARE.Internet;

namespace WIN.WEBCONNECTOR
{
    public partial class Form1 : Form
    {
      // private FrmCredential frmCredential;


        public Form1()
        {
            InitializeComponent();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //creo il form
            FrmexportData frm = new FrmexportData();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                FileInfo f = new FileInfo(path);

                path = f.DirectoryName + "\\Template\\WebConnectorTemplate.xlt";

                //path = Path.Combine(path, "\\Template\\WebConnectorTemplate.xlt");

                SimpleFileSystemManager.ViewFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            StartQueryWorkerProcess();

        }

        private void StartQueryWorkerProcess()
        {
            FrmQueryWorker frm = new FrmQueryWorker();
            frm.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            StartQueryForMultipleWorkersProcess();
          
        }

        private void StartQueryForMultipleWorkersProcess()
        {
            FrmQueryMultipleWorkers frm = new FrmQueryMultipleWorkers();
            frm.ShowDialog();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSearchData frm = new FrmSearchData();
            frm.ShowDialog();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmExportBilancio frm = new FrmExportBilancio();
            frm.ShowDialog();
        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmsendFiles frm = new FrmsendFiles();

            frm.ShowDialog();

            frm.Dispose();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmNomeAzienda frm = new FrmNomeAzienda();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {


                    if (frm.Azienda == "")
                    {
                        MessageBox.Show("Selezionare il nome di una azienda!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


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
                    FenealgestServices.QueryResultDTO dto = service.SearchWorkersByAzienda(frm.Azienda );

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
                    frm1.ShowDialog();



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
            frm.Dispose();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                FileInfo f = new FileInfo(path);

                path = f.DirectoryName + "\\Template\\TemplateImportLiberi.xlt";

                //path = Path.Combine(path, "\\Template\\WebConnectorTemplate.xlt");

                SimpleFileSystemManager.ViewFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmOnlyLiberi frm = new FrmOnlyLiberi();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
