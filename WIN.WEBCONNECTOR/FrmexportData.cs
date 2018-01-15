using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.BASEREUSE;
using System.Collections;
using System.Diagnostics;
using System.IO;
using WIN.WEBCONNECTOR.FileReaders;
using WIN.WEBCONNECTOR.FenealgestServices;
using WIN.TECHNICAL.MIDDLEWARE.Listeners;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmexportData : Form
    {
        WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace t;
        bool reading = false;

        public FrmexportData(  )
        {
            InitializeComponent();
          
        }

        private void FrmexportData_Load(object sender, EventArgs e)
        {
            //LoadComboProvince();
            cboSettore.SelectedIndex = 0;
            cboSem.SelectedIndex = 0;
            cboEnte.SelectedIndex = 0;


            Trace.Listeners.Add(new TextBoxTraceListener(txtTask));
        }

        //private void LoadComboProvince()
        //{
        //    IList province = _geo.GetListaProvincie();
        //    foreach (string item in province)
        //    {
        //        cboProv.Items.Add(item);
        //    }

        //    cboProv.Sorted = true;


        //    cboProv.SelectedIndex = 0;
        //}

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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //try
            //{
            //    SimpleFileSystemManager.ViewFile(lnkFile.Text);
            //}
            //catch (Exception)
            //{
               
            //}

            if (optXLS.Checked)
            {
                openFileDialog1.Filter = "file excel 2000-2003 (*.xls)|*.xls|file excel 2007 (*.xlsx)|*.xlsx";
            }
            else
            {
                openFileDialog1.Filter = "file xml (*.xml)|*.xml";
            }


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lnkFile.Text = openFileDialog1.FileName;
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            cmdImport.Enabled = false;
            try
            {

                //per prima cosa verifico gli input
                if (!File.Exists(lnkFile.Text))
                {
                    MessageBox.Show("Il file selezionato non esiste", "Messaggio",   MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return;
                }



                cmdClose.Enabled = false;
                cmdOk.Enabled = false;
                t = null;


                //recupero gli input
                InputHeader h = new InputHeader();
                //recupero l'input solo per il file excel
                //altrimeti il file xml è già completamente
                //valorizzato
                h.Struttura = "FENEAL";
                if (optXLS.Checked)
                {
                    h.Sector = cboSettore.Text;
                    if(cboSettore.Text != "EDILE")
                        h.Entity = "";
                    else
                        h.Entity = cboEnte.Text;
                    h.Mail = txtMail.Text;
                    h.Period = Convert.ToInt32(cboSem.Text);
                    h.Year = (int)numericUpDown1.Value;
                    
                    h.Responsible = txtResp.Text;

                    //verifico l'input
                    //se qualcosa non va viene lanciata una eccezione
                    h.CheckValidity();
                }

                reading = true;

                //recupero il lettore
                IExportReader reader = ExportReaderFactory.GetReader(lnkFile.Text);

                //ottengo l'oggetto
                t = reader.ReadExport(lnkFile.Text, h);

                reading = false;
                //se l'oggetto è nullo mando un avviso
                //e non faccio nulla

                if (t == null)
                {
                    MessageBox.Show("Nessun oggetto trovato o caricato", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    cmdClose.Enabled = true;
                    cmdOk.Enabled = true;
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
            }

        }

        private void cmdOk_Click(object sender, EventArgs e)
        {



            if (t != null)
            {
                cmdOk.Enabled = false;
                cmdClose.Enabled = false;
                cmdImport.Enabled = false;
                reading = true;
                FrmViewData frm = new FrmViewData(t);
                frm.Show();
                cmdOk.Enabled = true ;
                cmdClose.Enabled = true;
                cmdImport.Enabled = true;
                reading = false;
            }
            else
            {
                MessageBox.Show("Nulla da visualizzare", "Messaggio", MessageBoxButtons.OK);
            }



        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void FrmexportData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (reading)
                if (e.CloseReason == CloseReason.UserClosing)
                    e.Cancel = true;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
