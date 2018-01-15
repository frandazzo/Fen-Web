using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using WIN.WEBCONNECTOR.Credential;
using WIN.WEBCONNECTOR.GeoElements;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmCalcolaCodiceFisclae : Form
    {
        public string CodiceCalcolato = "";

        public FrmCalcolaCodiceFisclae()
        {
            InitializeComponent();

            LoadNazioni();
            LoadProvince();
            LoadComuni(CredentialDB.Instance.Province);
            comboBox1.SelectedIndex = 0;

            cboNazioni.SelectedIndexChanged += new EventHandler(cboNazioni_SelectedIndexChanged);
           

        }

     

        void cboNazioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNazioni.Text == "ITALIA")
            {
                cboProvince.Enabled = true;
                cboComuni.Enabled = true;
            }
            else
            {
                cboProvince.Enabled = false;
                cboComuni.Enabled = false;
            }
        }

        private void LoadNazioni()
        {
            IList nazioni = GeoElements.GeoHandlerProvider.Instance.Geo.GetListaNazioni();

            cboNazioni.Items.Clear();


            foreach (string item in nazioni)
            {
                cboNazioni.Items.Add(item);
            }

            cboNazioni.Sorted = true;

            cboNazioni.Text = "ITALIA";
        }

        private void LoadProvince()
        {
            IList provionce = GeoElements.GeoHandlerProvider.Instance.Geo.GetListaProvincie();

            cboProvince.Items.Clear();


            foreach (string item in provionce)
            {
                cboProvince.Items.Add(item);
            }

            cboProvince.Sorted = true;

            cboProvince.Text = CredentialDB.Instance.Province.ToUpper ();
        }

        private void LoadComuni(string provincia)
        {
            IList comuni = GeoElements.GeoHandlerProvider.Instance.Geo.GetListaComuniPerProvincia(provincia);

            cboComuni.Items.Clear();


            foreach (string item in comuni)
            {
                cboComuni.Items.Add(item);
            }

            cboComuni.Sorted = true;

            cboComuni.Text = provincia;
        }

        private void SpCf_Click(object sender, EventArgs e)
        {
            try
            {
              
               txtCode.Text = GeoHandlerProvider.Instance.Geo.CalcolaCodiceFiscale(txtNome.Text, txtCognome.Text, comboBox1.Text, dtpDate.Value.Date, cboComuni.Text, cboNazioni.Text);
               CodiceCalcolato = txtCode.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                 //Verifico la correttezza del codice fiscale
                //se il codice non è corretto viene lanciata na eccezione
                WIN.BASEREUSE.DatiFiscali f = GeoHandlerProvider.Instance.Geo.CalcolaDatiFiscali(txtCode.Text);

                FrmDatiFisc fisc = new FrmDatiFisc(f);

                fisc.ShowDialog();


            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cboProvince_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadComuni(cboProvince.Text);
        }




    }
}
