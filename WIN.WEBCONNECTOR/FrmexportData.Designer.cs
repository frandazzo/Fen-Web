namespace WIN.WEBCONNECTOR
{
    partial class FrmexportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmexportData));
            this.optXML = new System.Windows.Forms.RadioButton();
            this.optXLS = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdImport = new System.Windows.Forms.Button();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSem = new System.Windows.Forms.ComboBox();
            this.lblSemestre = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEnte = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSettore = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResp = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // optXML
            // 
            this.optXML.AutoSize = true;
            this.optXML.Location = new System.Drawing.Point(104, 18);
            this.optXML.Name = "optXML";
            this.optXML.Size = new System.Drawing.Size(73, 17);
            this.optXML.TabIndex = 1;
            this.optXML.Text = "Da file xml";
            this.optXML.UseVisualStyleBackColor = true;
            // 
            // optXLS
            // 
            this.optXLS.AutoSize = true;
            this.optXLS.Checked = true;
            this.optXLS.Location = new System.Drawing.Point(6, 19);
            this.optXLS.Name = "optXLS";
            this.optXLS.Size = new System.Drawing.Size(84, 17);
            this.optXLS.TabIndex = 0;
            this.optXLS.TabStop = true;
            this.optXLS.Text = "Da file Excel";
            this.optXLS.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.cmdImport);
            this.GroupBox1.Controls.Add(this.txtTask);
            this.GroupBox1.Location = new System.Drawing.Point(10, 282);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(390, 153);
            this.GroupBox1.TabIndex = 8;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Attività";
            // 
            // cmdImport
            // 
            this.cmdImport.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdImport.Location = new System.Drawing.Point(292, 19);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(85, 26);
            this.cmdImport.TabIndex = 1;
            this.cmdImport.Text = "Leggi file";
            this.cmdImport.UseVisualStyleBackColor = false;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // txtTask
            // 
            this.txtTask.Location = new System.Drawing.Point(18, 23);
            this.txtTask.Multiline = true;
            this.txtTask.Name = "txtTask";
            this.txtTask.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTask.Size = new System.Drawing.Size(258, 124);
            this.txtTask.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.optXLS);
            this.groupBox5.Controls.Add(this.optXML);
            this.groupBox5.Controls.Add(this.lnkFile);
            this.groupBox5.Location = new System.Drawing.Point(163, 92);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(236, 90);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Selezione file";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // lnkFile
            // 
            this.lnkFile.AutoSize = true;
            this.lnkFile.ForeColor = System.Drawing.Color.Black;
            this.lnkFile.Location = new System.Drawing.Point(6, 41);
            this.lnkFile.MaximumSize = new System.Drawing.Size(220, 0);
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size(115, 13);
            this.lnkFile.TabIndex = 2;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "Nessun file selezionato";
            this.lnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cboSem);
            this.groupBox2.Controls.Add(this.lblSemestre);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboEnte);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboSettore);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 88);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parametri di esportazione";
            // 
            // cboSem
            // 
            this.cboSem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSem.FormattingEnabled = true;
            this.cboSem.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cboSem.Location = new System.Drawing.Point(294, 54);
            this.cboSem.Name = "cboSem";
            this.cboSem.Size = new System.Drawing.Size(81, 21);
            this.cboSem.TabIndex = 7;
            // 
            // lblSemestre
            // 
            this.lblSemestre.AutoSize = true;
            this.lblSemestre.Location = new System.Drawing.Point(256, 60);
            this.lblSemestre.Name = "lblSemestre";
            this.lblSemestre.Size = new System.Drawing.Size(31, 13);
            this.lblSemestre.TabIndex = 6;
            this.lblSemestre.Text = "Sem.";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(294, 21);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1970,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(81, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Anno";
            // 
            // cboEnte
            // 
            this.cboEnte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEnte.FormattingEnabled = true;
            this.cboEnte.Items.AddRange(new object[] {
            "CASSA EDILE",
            "EDILCASSA",
            "CALEC",
            "CEA",
            "CEAV",
            "CEC",
            "CEDA",
            "CEDAF",
            "CEDAM",
            "CELCOF",
            "CEMA",
            "CERT",
            "CEVA",
            "CEDAIIER",
            "FALEA"});
            this.cboEnte.Location = new System.Drawing.Point(50, 51);
            this.cboEnte.Name = "cboEnte";
            this.cboEnte.Size = new System.Drawing.Size(164, 21);
            this.cboEnte.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ente";
            // 
            // cboSettore
            // 
            this.cboSettore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSettore.FormattingEnabled = true;
            this.cboSettore.Items.AddRange(new object[] {
            "EDILE",
            "IMPIANTI FISSI",
            "INPS"});
            this.cboSettore.Location = new System.Drawing.Point(50, 17);
            this.cboSettore.Name = "cboSettore";
            this.cboSettore.Size = new System.Drawing.Size(164, 21);
            this.cboSettore.TabIndex = 1;
            this.cboSettore.SelectedIndexChanged += new System.EventHandler(this.cboSettore_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Settore";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cmdOk
            // 
            this.cmdOk.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdOk.Enabled = false;
            this.cmdOk.Location = new System.Drawing.Point(242, 441);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(83, 30);
            this.cmdOk.TabIndex = 9;
            this.cmdOk.Text = "Visualizza dati";
            this.cmdOk.UseVisualStyleBackColor = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdClose.Enabled = false;
            this.cmdClose.Location = new System.Drawing.Point(336, 442);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(64, 29);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "Chiudi";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Selezione dati da esportare";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(92, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Resp. export";
            // 
            // txtResp
            // 
            this.txtResp.Location = new System.Drawing.Point(169, 39);
            this.txtResp.Name = "txtResp";
            this.txtResp.Size = new System.Drawing.Size(171, 20);
            this.txtResp.TabIndex = 2;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(169, 65);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(171, 20);
            this.txtMail.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(133, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mail";
            // 
            // FrmexportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WIN.WEBCONNECTOR.Properties.Resources.sfondo_area_riservata_italia;
            this.ClientSize = new System.Drawing.Size(411, 478);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmexportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selezione dati da esportare";
            this.Load += new System.EventHandler(this.FrmexportData_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmexportData_FormClosing);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton optXML;
        internal System.Windows.Forms.RadioButton optXLS;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button cmdImport;
        internal System.Windows.Forms.TextBox txtTask;
        internal System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.LinkLabel lnkFile;
        internal System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboEnte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSettore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboSem;
        private System.Windows.Forms.Label lblSemestre;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResp;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label2;
    }
}