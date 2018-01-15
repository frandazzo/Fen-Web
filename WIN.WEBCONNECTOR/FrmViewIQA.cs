using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using System.Diagnostics;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmViewIQA : DevExpress.XtraEditors.XtraForm
    {

        IQATrace _trace;

        public FrmViewIQA(IQATrace trace)
        {
            InitializeComponent();
            _trace = trace;
            gridControl1.DataSource = _trace.IqaTraceDetails;
            lblProvincia.Text = "Provincia: " + _trace.Provincia;
            lblSemester.Text = string.Format("Semestre: {0}-{1}", _trace.Period, _trace.Anno);
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
                    string savePath = folderBrowserDialog1.SelectedPath + "\\IQA.xlsx";
                    gridControl1.ExportToXlsx(savePath );

                    Process.Start(savePath);
                }

               
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message,"Errore",   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdxml_Click(object sender, EventArgs e)
        {
            try
            {
                //""
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string savePath = folderBrowserDialog1.SelectedPath+ "\\IQA.xml";
                    ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.IQATrace>.Save(_trace, savePath);

                    MessageBox.Show("Esportazione terminata", "Info",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}