using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.BASEREUSE;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmDatiFisc : Form
    {
        public FrmDatiFisc(DatiFiscali f)
        {
            InitializeComponent();
            txtComune.Text = f.Comune.Descrizione;
            txtNazione.Text = f.Nazione.Descrizione;
            txtProvincia.Text = f.Provincia.Descrizione;
            if ( f.SessoPersona.ToString() == "0")
                txtSesso.Text =  "MASCHIO";
            else
                txtSesso.Text = "FEMMINA";
            txtData.Text = f.DataNascita.ToShortDateString();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDatiFisc_Load(object sender, EventArgs e)
        {

        }
    }
}
