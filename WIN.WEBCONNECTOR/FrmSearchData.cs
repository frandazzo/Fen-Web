using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.WEBCONNECTOR.Credential;
using System.Collections;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.GeoElements;
using System.Xml;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmSearchData : Form
    {
        public FrmSearchData()
        {
            InitializeComponent();

            LoadNazioni();
            LoadProvince();
            LoadComuni(CredentialDB.Instance.Province);
           

            cboNazioni.SelectedIndexChanged += new EventHandler(cboNazioni_SelectedIndexChanged);
        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
                dtpDate.Enabled = true;
            else
                dtpDate.Enabled = false;
        }


        void cboNazioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboNazioni.Text == "ITALIA")
            //{
            //    cboProvince.Enabled = true;
            //    cboComuni.Enabled = true;
            //}
            //else
            //{
            //    cboProvince.Enabled = false;
            //    cboComuni.Enabled = false;
            //}
        }

        private void LoadNazioni()
        {
            IList nazioni = GeoElements.GeoHandlerProvider.Instance.Geo.GetListaNazioni();

            cboNazioni.Items.Clear();
            cboNazioni.Items.Add("");

            foreach (string item in nazioni)
            {
                cboNazioni.Items.Add(item);
            }

            cboNazioni.Sorted = true;

            cboNazioni.Text = "ITALIA";
        }

        private void LoadProvince()
        {
            //IList provionce = GeoElements.GeoHandlerProvider.Instance.Geo.GetListaProvincie();

            //cboProvince.Items.Clear();
            ////cboProvince.Items.Add("");

            //foreach (string item in provionce)
            //{
            //    cboProvince.Items.Add(item);
            //}

            //cboProvince.Sorted = true;

            cboProvince.Items.Add(CredentialDB.Instance.Province.ToUpper());
            cboProvince.SelectedIndex = 0;
        }

        private void LoadComuni(string provincia)
        {
            IList comuni = GeoElements.GeoHandlerProvider.Instance.Geo.GetListaComuniPerProvincia(provincia);

            cboComuni.Items.Clear();

            cboComuni.Items.Add("");
            foreach (string item in comuni)
            {
                cboComuni.Items.Add(item);
            }

            cboComuni.Sorted = true;

            cboComuni.Text = provincia;
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComuni(cboProvince.Text);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {

                //if (string.IsNullOrEmpty(txtCognome.Text) && string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(cboNazioni.Text) && string.IsNullOrEmpty(cboComuni.Text) && chkDate.Checked == false)
                //{
                //    MessageBox.Show("Inserire almeno un parametro di ricerca", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

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



               

                //se arrivo qui vuol dire che il codice è scritto correttamente
                //creo l'intestazione del webService
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province;

                //creo il servizio
                FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();


                //gli imposto l'intestazione
                service.CredenzialiValue = c;


                //imposto i parametri della ricerca
                FenealgestServices.QueryParameters param = new WIN.WEBCONNECTOR.FenealgestServices.QueryParameters();

                param.MaxResult = Properties.Settings.Default.MaxQueryResult;
                param.Region = reg.Descrizione;

                if (chkDate.Checked)
                {
                    param.CheckDate = true;
                    param.BirthDate = dtpDate.Value.Date;
                }
                else
                {
                    param.CheckDate = false;
                    param.BirthDate = dtpDate.Value.Date;
                }

                param.Name = txtNome.Text;
                param.Surname = txtCognome.Text;
                param.Nationality = cboNazioni.Text;
                param.CompanyFiscalCode = txtPiva.Text;
                param.CompanyDescription = txtAzienda.Text;

                //if (cboComuni.Enabled)
                    param.LivingPlace = cboComuni.Text;
                //else
                //    param.LivingPlace = "";


                
                
                


                //metto in attesa
                WIN.GUI.UTILITY.Helper.ShowWaitBox("Attendere ricerca lavoratori...", Properties.Resources.Waiting3);





                //richiedo il dato
                FenealgestServices.QueryResultDTO dto = service.SearchWorkers(param);

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



                IBindingList l = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(worker);


                FrmViewData frm = new FrmViewData(l, dto.Message );
                frm.Show();

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

        private void optAnag_CheckedChanged(object sender, EventArgs e)
        {
            if (optAnag.Checked)
            {
                grpAnag.Enabled = true;
                grpaz.Enabled = false;

                ReloadComuni();
                txtAzienda.Text = "";
                txtPiva.Text = "";
            }
            else
            {
                grpAnag.Enabled = false;
                grpaz.Enabled = true;

                txtCognome.Text = "";
                txtNome.Text = "";
                chkDate.Checked = false;
                cboNazioni.Text = "";
                cboProvince.Items.Clear();
                cboComuni.Items.Clear();
            }
        }

        private void ReloadComuni()
        {
            cboProvince.Items.Add(CredentialDB.Instance.Province.ToUpper());
            cboProvince.SelectedIndex = 0;
            LoadComuni(cboProvince.Text);
        }

     


    }
}
