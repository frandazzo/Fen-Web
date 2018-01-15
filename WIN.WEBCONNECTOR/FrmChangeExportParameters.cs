using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.WEBCONNECTOR.FileReaders;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmChangeExportParameters : Form
    {
        public InputHeader InputHeader;
       

        public FrmChangeExportParameters(string resp, string mail)
        {
            InitializeComponent();

            cboSettore.SelectedIndex = 0;
            cboSem.SelectedIndex = 0;
            cboEnte.SelectedIndex = 0;
            txtMail.Text = mail;
            txtResp.Text = resp;
        }



        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdChange_Click(object sender, EventArgs e)
        {

            InputHeader = new InputHeader();
            if (cboEnte.Enabled)
                InputHeader.Entity = cboEnte.Text;
            else
                InputHeader.Entity = "";
            InputHeader.Mail = txtMail.Text;
            InputHeader.Period = Convert.ToInt32(cboSem.Text);
            InputHeader.Year = (int)numericUpDown1.Value;
            InputHeader.Sector = cboSettore.Text;
            InputHeader.Responsible = txtResp.Text;


            try
            {
                //verifico l'input
                //se qualcosa non va viene lanciata una eccezione
                InputHeader.CheckValidity();

                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }

        private void cboSettore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSettore.Text == "EDILE")
            {
                cboEnte.Enabled = true;
                cboSem.Enabled = true;
            }
            else
            {
                cboEnte.Enabled = false;
                cboSem.Enabled = false;
            }
        }
    }
}
