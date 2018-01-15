namespace WIN.WEBCONNECTOR
{
    partial class FrmsendFiles
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
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSem = new System.Windows.Forms.ComboBox();
            this.lblSemestre = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEnte = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSettore = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.optIQI = new System.Windows.Forms.RadioButton();
            this.optIQA = new System.Windows.Forms.RadioButton();
            this.optLib = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdPath = new System.Windows.Forms.Button();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.cmdImportXml = new System.Windows.Forms.Button();
            this.cmdImport = new System.Windows.Forms.Button();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cmdView = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtsub = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(184, 38);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(276, 20);
            this.txtMail.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(148, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Mail";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(66, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 22);
            this.label7.TabIndex = 11;
            this.label7.Text = "Selezione dati da esportare";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdClose.Enabled = false;
            this.cmdClose.Location = new System.Drawing.Point(452, 467);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(64, 29);
            this.cmdClose.TabIndex = 10;
            this.cmdClose.Text = "Chiudi";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdOk.Enabled = false;
            this.cmdOk.Location = new System.Drawing.Point(375, 467);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(71, 30);
            this.cmdOk.TabIndex = 19;
            this.cmdOk.Text = "Invia file";
            this.cmdOk.UseVisualStyleBackColor = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
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
            this.groupBox2.Location = new System.Drawing.Point(21, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 88);
            this.groupBox2.TabIndex = 17;
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
            this.cboSem.Location = new System.Drawing.Point(367, 51);
            this.cboSem.Name = "cboSem";
            this.cboSem.Size = new System.Drawing.Size(81, 21);
            this.cboSem.TabIndex = 7;
            // 
            // lblSemestre
            // 
            this.lblSemestre.AutoSize = true;
            this.lblSemestre.Location = new System.Drawing.Point(329, 57);
            this.lblSemestre.Name = "lblSemestre";
            this.lblSemestre.Size = new System.Drawing.Size(31, 13);
            this.lblSemestre.TabIndex = 6;
            this.lblSemestre.Text = "Sem.";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(367, 18);
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
            this.label6.Location = new System.Drawing.Point(329, 23);
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
            "EDILE"});
            this.cboSettore.Location = new System.Drawing.Point(50, 17);
            this.cboSettore.Name = "cboSettore";
            this.cboSettore.Size = new System.Drawing.Size(164, 21);
            this.cboSettore.TabIndex = 1;
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
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.optIQI);
            this.groupBox5.Controls.Add(this.optIQA);
            this.groupBox5.Controls.Add(this.optLib);
            this.groupBox5.Location = new System.Drawing.Point(184, 90);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(319, 82);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Selezionetipo file da inviare";
            // 
            // optIQI
            // 
            this.optIQI.AutoSize = true;
            this.optIQI.Location = new System.Drawing.Point(6, 61);
            this.optIQI.Name = "optIQI";
            this.optIQI.Size = new System.Drawing.Size(174, 17);
            this.optIQI.TabIndex = 2;
            this.optIQI.Text = "File per importazione quote Inps";
            this.optIQI.UseVisualStyleBackColor = true;
            this.optIQI.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // optIQA
            // 
            this.optIQA.AutoSize = true;
            this.optIQA.Checked = true;
            this.optIQA.Location = new System.Drawing.Point(6, 19);
            this.optIQA.Name = "optIQA";
            this.optIQA.Size = new System.Drawing.Size(142, 17);
            this.optIQA.TabIndex = 0;
            this.optIQA.TabStop = true;
            this.optIQA.Text = "File per importazione IQA";
            this.optIQA.UseVisualStyleBackColor = true;
            this.optIQA.CheckedChanged += new System.EventHandler(this.optIQA_CheckedChanged);
            // 
            // optLib
            // 
            this.optLib.AutoSize = true;
            this.optLib.Location = new System.Drawing.Point(6, 39);
            this.optLib.Name = "optLib";
            this.optLib.Size = new System.Drawing.Size(217, 17);
            this.optLib.TabIndex = 1;
            this.optLib.Text = "File per importazione lavoratori non iscritti";
            this.optLib.UseVisualStyleBackColor = true;
            this.optLib.CheckedChanged += new System.EventHandler(this.optLib_CheckedChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.cmdPath);
            this.GroupBox1.Controls.Add(this.lnkFile);
            this.GroupBox1.Controls.Add(this.cmdImportXml);
            this.GroupBox1.Controls.Add(this.cmdImport);
            this.GroupBox1.Controls.Add(this.txtTask);
            this.GroupBox1.Location = new System.Drawing.Point(12, 272);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(500, 182);
            this.GroupBox1.TabIndex = 18;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Attività";
            // 
            // cmdPath
            // 
            this.cmdPath.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdPath.Location = new System.Drawing.Point(392, 120);
            this.cmdPath.Name = "cmdPath";
            this.cmdPath.Size = new System.Drawing.Size(93, 36);
            this.cmdPath.TabIndex = 4;
            this.cmdPath.Text = "Importa da direcory file inps";
            this.cmdPath.UseVisualStyleBackColor = false;
            this.cmdPath.Visible = false;
            this.cmdPath.Click += new System.EventHandler(this.cmdPath_Click);
            // 
            // lnkFile
            // 
            this.lnkFile.AutoSize = true;
            this.lnkFile.ForeColor = System.Drawing.Color.Black;
            this.lnkFile.Location = new System.Drawing.Point(15, 16);
            this.lnkFile.MaximumSize = new System.Drawing.Size(220, 0);
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size(115, 13);
            this.lnkFile.TabIndex = 3;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "Nessun file selezionato";
            this.lnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFile_LinkClicked);
            // 
            // cmdImportXml
            // 
            this.cmdImportXml.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdImportXml.Location = new System.Drawing.Point(392, 69);
            this.cmdImportXml.Name = "cmdImportXml";
            this.cmdImportXml.Size = new System.Drawing.Size(93, 36);
            this.cmdImportXml.TabIndex = 2;
            this.cmdImportXml.Text = "Importa da file xml";
            this.cmdImportXml.UseVisualStyleBackColor = false;
            this.cmdImportXml.Click += new System.EventHandler(this.cmdImportXml_Click);
            // 
            // cmdImport
            // 
            this.cmdImport.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdImport.Location = new System.Drawing.Point(392, 19);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(93, 34);
            this.cmdImport.TabIndex = 1;
            this.cmdImport.Text = "Importa da file Excel";
            this.cmdImport.UseVisualStyleBackColor = false;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // txtTask
            // 
            this.txtTask.Location = new System.Drawing.Point(18, 51);
            this.txtTask.Multiline = true;
            this.txtTask.Name = "txtTask";
            this.txtTask.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTask.Size = new System.Drawing.Size(368, 124);
            this.txtTask.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(9, 74);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "IQA";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cmdView
            // 
            this.cmdView.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdView.Enabled = false;
            this.cmdView.Location = new System.Drawing.Point(1, 469);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(83, 30);
            this.cmdView.TabIndex = 20;
            this.cmdView.Text = "Visualizza dati";
            this.cmdView.UseVisualStyleBackColor = false;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtsub
            // 
            this.txtsub.Location = new System.Drawing.Point(184, 64);
            this.txtsub.Name = "txtsub";
            this.txtsub.Size = new System.Drawing.Size(276, 20);
            this.txtsub.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(129, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Oggetto";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(90, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 39);
            this.button1.TabIndex = 23;
            this.button1.Text = "Lista file";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(169, 470);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 39);
            this.button3.TabIndex = 25;
            this.button3.Text = "Rinomina il primo";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Gainsboro;
            this.button4.Location = new System.Drawing.Point(276, 470);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 39);
            this.button4.TabIndex = 26;
            this.button4.Text = "Scarica il primo";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(12, 45);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Liberi";
            this.simpleButton2.Visible = false;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(9, 103);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 27;
            this.simpleButton3.Text = "send mail";
            this.simpleButton3.Visible = false;
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(12, 145);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(75, 23);
            this.simpleButton4.TabIndex = 28;
            this.simpleButton4.Text = "send free mail";
            this.simpleButton4.Visible = false;
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // FrmsendFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WIN.WEBCONNECTOR.Properties.Resources.sfondo_area_riservata_italia;
            this.ClientSize = new System.Drawing.Size(528, 511);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtsub);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdView);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmsendFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invio file";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboSem;
        private System.Windows.Forms.Label lblSemestre;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboEnte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSettore;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.RadioButton optIQA;
        internal System.Windows.Forms.RadioButton optLib;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button cmdImportXml;
        internal System.Windows.Forms.Button cmdImport;
        internal System.Windows.Forms.TextBox txtTask;
        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.LinkLabel lnkFile;
        private System.Windows.Forms.TextBox txtsub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        internal System.Windows.Forms.RadioButton optIQI;
        internal System.Windows.Forms.Button cmdPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
    }
}