using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WIN.WEBCONNECTOR.ServiceAgents;
using WIN.WEBCONNECTOR.SharetopServices;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmCongressPresence : XtraForm
    {
        public FrmCongressPresence()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                WIN.GUI.UTILITY.Helper.ShowWaitBox("Richiesta dati in corso...", Properties.Resources.Waiting3);

                CongressPresence[] presences = new SharetopIntegrationServiceAgent().FindCongressPresence(txtnome.Text, txtcognome.Text);

                if (presences.Length == 0)
                {
                    gridControl1.DataSource = null;
                    WIN.GUI.UTILITY.Helper.HideWaitBox();
                    XtraMessageBox.Show("Nessun elemento trovato", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                gridControl1.DataSource = presences;


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                WIN.GUI.UTILITY.Helper.HideWaitBox();
            }
        }
    }
}
