using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BilancioFenealgest.DomainLayer;
using BilancioFenealgest.DataAccess;
using WIN.WEBCONNECTOR.Credential;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.ServiceAgents;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmExportBilancio : Form
    {
        public FrmExportBilancio()
        {
            InitializeComponent();
        }

        private void lnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "file xml (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lnkFile.Text = openFileDialog1.FileName;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            try
            {
                DTORendicontoMappaer m = new DTORendicontoMappaer();

                DTORendiconto r = m.LoadDTORendicontoByPath(lnkFile.Text);

                if (r == null)
                    throw new Exception("File non valido!");



                //metto in attesa
                WIN.GUI.UTILITY.Helper.ShowWaitBox("Attendere invio bilancio...", Properties.Resources.Waiting3);


                ExportBilancioServiceAgent a = new ExportBilancioServiceAgent(r);



                string result = a.SendBilancio();

                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Esportazione terminata con successo!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Esportazione non effettuata: " + Environment.NewLine + result, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //metto in attesa
                WIN.GUI.UTILITY.Helper.HideWaitBox();

            }
        }

       
    }
}
