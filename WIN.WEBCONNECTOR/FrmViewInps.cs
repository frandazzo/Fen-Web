using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using System.Diagnostics;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmViewInps : DevExpress.XtraEditors.XtraForm
    {

         InpsTrace _trace;
         public FrmViewInps(InpsTrace trace)
        {
            InitializeComponent();
            _trace = trace;
            gridControl1.DataSource = _trace.InpsTraceDetails;

            lblMail.Text = "Mail da inviare a:" + _trace.Mailto;
            lblsub.Text = "Oggetto: " + _trace.Subject;
        }

      

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //""
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string savePath = folderBrowserDialog1.SelectedPath + "\\Inps.xlsx";
                    gridControl1.ExportToXlsx(savePath);

                    Process.Start(savePath);
                }


            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdxml_Click(object sender, EventArgs e)
        {
            try
            {
                //""
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string savePath = folderBrowserDialog1.SelectedPath + "\\Inps.xml";
                    ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTrace>.Save(_trace, savePath);

                    MessageBox.Show("Esportazione terminata", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}