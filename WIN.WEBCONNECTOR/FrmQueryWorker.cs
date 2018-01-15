using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.Credential;
using WIN.TECHNICAL.MIDDLEWARE.Internet;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmQueryWorker : Form
    {
        public FrmQueryWorker()
        {
            InitializeComponent();
        }
        public FrmQueryWorker(string fiscalCode)
        {
            InitializeComponent();
            textBox1.Text = fiscalCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("Nessun codice inserito", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


                Regione reg = new RegioneNulla();
                //prendo il nome della regione 
                if (!checkBox1.Checked)
                {
                    Provincia r = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaByName(CredentialDB.Instance.Province);
                    reg = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetRegioneById(r.IdRegione.ToString());
                }


                //Verifico la correttezza del codice fiscale
                //se il codice non è corretto viene lanciata na eccezione
                DatiFiscali f = GeoHandlerProvider.Instance.Geo.CalcolaDatiFiscali(textBox1.Text);

                //se arrivo qui vuol dire che il codice è scritto correttamente
                //creo l'intestazione del webService
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName ;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province ;


                //metto in attesaa
                 //metto in attesa
                WIN.GUI.UTILITY.Helper.ShowWaitBox ("Attendere richiesta codice fiscale...", Properties.Resources.Waiting3);

                //creo il servizio
                FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();


                //gli imposto l'intestazione
                service.CredenzialiValue = c;

                //richiedo il dato
                FenealgestServices.QueryResultDTO dto = service.ExportWorker(textBox1.Text.ToUpper(), reg.Descrizione);



                WIN.GUI.UTILITY.Helper.HideWaitBox();


                // a questo punto posso verificare la presenza di un errore di elaborazione
                if (!dto.IsResultValid)
                {
                    MessageBox.Show(dto.Error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //verifico la presenza di un messaggio per la notifica che nessun elemento 
                //è stato trovato
                if (!string.IsNullOrEmpty(dto.Message))
                {
                    MessageBox.Show(dto.Message, "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return;
                }



                // a questo punto posso inviare il dato in visualizzazione...

                //prima lo trasformo
                WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO worker = DTOTranslator.ToCustomWorkerDTO(dto.WorkerDTO);
                //poi lo invio al form per la visualizzazione

                FrmViewData frm = new FrmViewData(worker);
                frm.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                 //metto in attesa
                WIN.GUI.UTILITY.Helper.HideWaitBox ();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmCalcolaCodiceFisclae frm = new FrmCalcolaCodiceFisclae();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = frm.CodiceCalcolato;
            }
        }
    }
}
