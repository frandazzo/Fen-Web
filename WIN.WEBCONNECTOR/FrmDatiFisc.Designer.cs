namespace WIN.WEBCONNECTOR
{
    partial class FrmDatiFisc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatiFisc));
            this.cmdOK = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtNazione = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtComune = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtProvincia = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtSesso = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(229, 192);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(72, 27);
            this.cmdOK.TabIndex = 24;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.ForeColor = System.Drawing.Color.DarkGray;
            this.Label6.Location = new System.Drawing.Point(14, 160);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(295, 13);
            this.Label6.TabIndex = 23;
            this.Label6.Text = "________________________________________________";
            // 
            // txtNazione
            // 
            this.txtNazione.Location = new System.Drawing.Point(89, 69);
            this.txtNazione.Name = "txtNazione";
            this.txtNazione.Size = new System.Drawing.Size(212, 20);
            this.txtNazione.TabIndex = 18;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(14, 76);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 13);
            this.Label5.TabIndex = 17;
            this.Label5.Text = "Nazionalità";
            // 
            // txtComune
            // 
            this.txtComune.Location = new System.Drawing.Point(89, 126);
            this.txtComune.Name = "txtComune";
            this.txtComune.Size = new System.Drawing.Size(212, 20);
            this.txtComune.TabIndex = 22;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(27, 133);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(46, 13);
            this.Label3.TabIndex = 21;
            this.Label3.Text = "Comune";
            // 
            // txtProvincia
            // 
            this.txtProvincia.Location = new System.Drawing.Point(89, 99);
            this.txtProvincia.Name = "txtProvincia";
            this.txtProvincia.Size = new System.Drawing.Size(212, 20);
            this.txtProvincia.TabIndex = 20;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(22, 106);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(51, 13);
            this.Label2.TabIndex = 19;
            this.Label2.Text = "Provincia";
            // 
            // txtSesso
            // 
            this.txtSesso.Location = new System.Drawing.Point(89, 12);
            this.txtSesso.Name = "txtSesso";
            this.txtSesso.Size = new System.Drawing.Size(117, 20);
            this.txtSesso.TabIndex = 14;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(37, 19);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(36, 13);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Sesso";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(89, 38);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(117, 20);
            this.txtData.TabIndex = 16;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 45);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(61, 13);
            this.Label1.TabIndex = 15;
            this.Label1.Text = "Data Nasc.";
            // 
            // FrmDatiFisc
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(320, 231);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtNazione);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtComune);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtProvincia);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtSesso);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDatiFisc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dati fiscali";
            this.Load += new System.EventHandler(this.FrmDatiFisc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtNazione;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtComune;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtProvincia;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtSesso;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtData;
        internal System.Windows.Forms.Label Label1;
    }
}