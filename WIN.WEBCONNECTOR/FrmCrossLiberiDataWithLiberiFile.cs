using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.TECHNICAL.MIDDLEWARE.Listeners;
using WIN.WEBCONNECTOR.FileReaders;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmCrossLiberiDataWithLiberiFile : Form
    {
        IBindingList _workers;
        LiberiTrace t1;


        public FrmCrossLiberiDataWithLiberiFile(IBindingList workers)
        {
            InitializeComponent();
            _workers = workers;
            Trace.Listeners.Add(new TextBoxTraceListener(txtTask));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //qui leggo il file

            openFileDialog1.Filter = "file excel 2000-2003 (*.xls)|*.xls|file excel 2007 (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                cmdImport.Enabled = false;
              
                try
                {

                 
                        //per prima cosa verifico gli input
                    if (!File.Exists(openFileDialog1.FileName))
                    {
                        MessageBox.Show("Il file selezionato non esiste", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                   


                    
                        //importo i liberi
                    ILiberiReader reader = SendFileImportReaderFactory.GetLiberiReader(openFileDialog1.FileName);

                    InputHeader h = new InputHeader();
                    //ottengo l'oggetto
                    t1 = reader.ReadTrace(openFileDialog1.FileName, h);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    cmdImport.Enabled = true;
                   
                }
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //qui posso incrociare i dati....
            foreach ( WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers)
            {
                //ne prendo il codice fiscale per recuperarlo nella lista della traccia dei liberi
                string fiscale = item.Fiscalcode;

                LiberiTraceDetail detail = GetDetailByFiscalCode(fiscale);

                if (detail != null)
                {
                    item.CurrentAzienda = detail.AZIENDA_IMPIEGO;
                    item.IscrittoA = detail.ISCRITTO_A;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private LiberiTraceDetail GetDetailByFiscalCode(string fiscale)
        {
            if (string.IsNullOrEmpty(fiscale) )
                return null;

            if (t1 == null)
                return null;

            if (t1.LiberiTraceDetails == null)
                return null;

            foreach (LiberiTraceDetail item in t1.LiberiTraceDetails )
            {
                if (fiscale.Equals(item.FISCALE))
                    return item;
            }

            return null;
        }
    }
}
