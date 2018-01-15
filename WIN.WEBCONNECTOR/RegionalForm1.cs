using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.WEBCONNECTOR.Credential;
using System.Reflection;
using System.IO;

namespace WIN.WEBCONNECTOR
{
    public partial class RegionalForm1 : Form
    {
      // private FrmCredential frmCredential;


        public RegionalForm1()
        {
            InitializeComponent();
            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmExportBilancio frm = new FrmExportBilancio();
            frm.ShowDialog();
        }
    }
}
