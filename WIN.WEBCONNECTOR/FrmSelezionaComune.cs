using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.WEBCONNECTOR.Credential;
using System.Collections;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmSelezionaComune : Form
    {


        public string Comune = "";


        public FrmSelezionaComune()
        {
            InitializeComponent();
            LoadProvince();
            LoadComuni(CredentialDB.Instance.Province);

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

            cboProvince.Text = CredentialDB.Instance.Province.ToUpper();
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

       

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Comune = cboComuni.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComuni(cboProvince.Text);
        }

        private void FrmSelezionaComune_Load(object sender, EventArgs e)
        {

        }
    }
}
