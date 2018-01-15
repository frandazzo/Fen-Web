namespace WIN.WEBCONNECTOR
{
    partial class FrmChangeExportParameters
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
            this.txtResp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSem = new System.Windows.Forms.ComboBox();
            this.lblSemestre = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEnte = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSettore = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdChange = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(90, 43);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(171, 20);
            this.txtMail.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(54, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Mail";
            // 
            // txtResp
            // 
            this.txtResp.Location = new System.Drawing.Point(90, 17);
            this.txtResp.Name = "txtResp";
            this.txtResp.Size = new System.Drawing.Size(171, 20);
            this.txtResp.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Resp. export";
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
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 88);
            this.groupBox2.TabIndex = 38;
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
            // cmdChange
            // 
            this.cmdChange.Location = new System.Drawing.Point(203, 172);
            this.cmdChange.Name = "cmdChange";
            this.cmdChange.Size = new System.Drawing.Size(96, 27);
            this.cmdChange.TabIndex = 43;
            this.cmdChange.Text = "Cambia";
            this.cmdChange.UseVisualStyleBackColor = true;
            this.cmdChange.Click += new System.EventHandler(this.cmdChange_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(304, 172);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 27);
            this.cmdClose.TabIndex = 44;
            this.cmdClose.Text = "Chiudi";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // FrmChangeExportParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 208);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdChange);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmChangeExportParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambia parametri esportazione";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResp;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboSem;
        private System.Windows.Forms.Label lblSemestre;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboEnte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSettore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdChange;
        private System.Windows.Forms.Button cmdClose;
    }
}