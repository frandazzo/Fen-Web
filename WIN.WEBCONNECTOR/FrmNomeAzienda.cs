using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmNomeAzienda : Form
    {
        public string Azienda;
        private bool  _background;

        public FrmNomeAzienda()
        {
            InitializeComponent();
        }
        public FrmNomeAzienda(string nomeAzienda)
        {
            InitializeComponent();
            textBox1.Text = nomeAzienda;
        }

        public FrmNomeAzienda(string nomeAzienda, bool background)
        {
            InitializeComponent();
            textBox1.Text = nomeAzienda;
            _background = background;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Azienda = textBox1.Text;
            if (!_background)
            {
                Azienda = textBox1.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                try
                {


                    if (Azienda == "")
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
           




        }
    }
}
