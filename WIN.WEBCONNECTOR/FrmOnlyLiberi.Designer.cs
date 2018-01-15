namespace WIN.WEBCONNECTOR
{
    partial class FrmOnlyLiberi
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
            this.txtsub = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.cmdImportXml = new System.Windows.Forms.Button();
            this.cmdImport = new System.Windows.Forms.Button();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdView = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtsub
            // 
            this.txtsub.Location = new System.Drawing.Point(175, 60);
            this.txtsub.Name = "txtsub";
            this.txtsub.Size = new System.Drawing.Size(276, 20);
            this.txtsub.TabIndex = 26;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(175, 34);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(276, 20);
            this.txtMail.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(57, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 22);
            this.label7.TabIndex = 23;
            this.label7.Text = "Selezione dati da esportare";
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.lnkFile);
            this.GroupBox1.Controls.Add(this.cmdImportXml);
            this.GroupBox1.Controls.Add(this.cmdImport);
            this.GroupBox1.Controls.Add(this.txtTask);
            this.GroupBox1.Location = new System.Drawing.Point(12, 86);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(500, 182);
            this.GroupBox1.TabIndex = 25;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Attività";
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
            // 
            // cmdImportXml
            // 
            this.cmdImportXml.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdImportXml.Location = new System.Drawing.Point(392, 101);
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
            this.cmdImport.Location = new System.Drawing.Point(392, 51);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(123, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Oggetto";
            // 
            // cmdView
            // 
            this.cmdView.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdView.Enabled = false;
            this.cmdView.Location = new System.Drawing.Point(30, 293);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(83, 30);
            this.cmdView.TabIndex = 30;
            this.cmdView.Text = "Visualizza dati";
            this.cmdView.UseVisualStyleBackColor = false;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(142, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Mail";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdClose.Enabled = false;
            this.cmdClose.Location = new System.Drawing.Point(433, 294);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(64, 29);
            this.cmdClose.TabIndex = 27;
            this.cmdClose.Text = "Chiudi";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdOk.Enabled = false;
            this.cmdOk.Location = new System.Drawing.Point(356, 294);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(71, 30);
            this.cmdOk.TabIndex = 29;
            this.cmdOk.Text = "Invia file";
            this.cmdOk.UseVisualStyleBackColor = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmOnlyLiberi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 335);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtsub);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmOnlyLiberi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Esportazione liberi Fenealweb";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtsub;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.LinkLabel lnkFile;
        internal System.Windows.Forms.Button cmdImportXml;
        internal System.Windows.Forms.Button cmdImport;
        internal System.Windows.Forms.TextBox txtTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}