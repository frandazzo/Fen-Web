using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmAnnoIscrizione : Form
    {
        public String AnnoIscrizione;

        public FrmAnnoIscrizione()
        {
            InitializeComponent();
        }




        private void button2_Click(object sender, EventArgs e)
        {
            AnnoIscrizione = textBox1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
