namespace WIN.WEBCONNECTOR
{
    partial class FrmCalcolaCodiceFisclae
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCalcolaCodiceFisclae));
            this.cmdOK = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.SpCf = new System.Windows.Forms.Button();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtCognome = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cboNazioni = new System.Windows.Forms.ComboBox();
            this.cboProvince = new System.Windows.Forms.ComboBox();
            this.cboComuni = new System.Windows.Forms.ComboBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(317, 192);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(107, 28);
            this.cmdOK.TabIndex = 45;
            this.cmdOK.Text = "&Ok";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.ForeColor = System.Drawing.Color.DarkGray;
            this.Label5.Location = new System.Drawing.Point(69, 134);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(361, 13);
            this.Label5.TabIndex = 44;
            this.Label5.Text = "___________________________________________________________";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(211, 21);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(29, 13);
            this.Label11.TabIndex = 38;
            this.Label11.Text = "Naz.";
            // 
            // Button1
            // 
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.Location = new System.Drawing.Point(316, 156);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(114, 25);
            this.Button1.TabIndex = 35;
            this.Button1.Text = "Calc. Dati F&isc.";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(25, 103);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 13);
            this.Label1.TabIndex = 40;
            this.Label1.Text = "D. nas.";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(38, 75);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(28, 13);
            this.Label6.TabIndex = 41;
            this.Label6.Text = "Ses.";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(31, 46);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 42;
            this.Label2.Text = "Nome";
            // 
            // SpCf
            // 
            this.SpCf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SpCf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SpCf.Location = new System.Drawing.Point(205, 156);
            this.SpCf.Name = "SpCf";
            this.SpCf.Size = new System.Drawing.Size(110, 25);
            this.SpCf.TabIndex = 34;
            this.SpCf.Text = "Calc. Cod. &Fisc.";
            this.SpCf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SpCf.UseVisualStyleBackColor = true;
            this.SpCf.Click += new System.EventHandler(this.SpCf_Click);
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(19, 165);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(51, 13);
            this.Label17.TabIndex = 39;
            this.Label17.Text = "Cod. fisc.";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(31, 21);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 13);
            this.Label3.TabIndex = 36;
            this.Label3.Text = "Cogn.";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(186, 75);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(54, 13);
            this.Label4.TabIndex = 37;
            this.Label4.Text = "Com. nas.";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(197, 46);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(43, 13);
            this.Label7.TabIndex = 43;
            this.Label7.Text = "Pr. nas.";
            // 
            // txtCognome
            // 
            this.txtCognome.Location = new System.Drawing.Point(72, 12);
            this.txtCognome.Name = "txtCognome";
            this.txtCognome.Size = new System.Drawing.Size(116, 20);
            this.txtCognome.TabIndex = 46;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(72, 39);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(116, 20);
            this.txtNome.TabIndex = 47;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "MASCHIO",
            "FEMMINA"});
            this.comboBox1.Location = new System.Drawing.Point(72, 66);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(108, 21);
            this.comboBox1.TabIndex = 48;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(73, 97);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(115, 20);
            this.dtpDate.TabIndex = 49;
            // 
            // cboNazioni
            // 
            this.cboNazioni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNazioni.FormattingEnabled = true;
            this.cboNazioni.Location = new System.Drawing.Point(246, 12);
            this.cboNazioni.Name = "cboNazioni";
            this.cboNazioni.Size = new System.Drawing.Size(177, 21);
            this.cboNazioni.TabIndex = 50;
            // 
            // cboProvince
            // 
            this.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvince.FormattingEnabled = true;
            this.cboProvince.Location = new System.Drawing.Point(246, 39);
            this.cboProvince.Name = "cboProvince";
            this.cboProvince.Size = new System.Drawing.Size(177, 21);
            this.cboProvince.TabIndex = 51;
            this.cboProvince.SelectedIndexChanged += new System.EventHandler(this.cboProvince_SelectedIndexChanged_1);
            // 
            // cboComuni
            // 
            this.cboComuni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComuni.FormattingEnabled = true;
            this.cboComuni.Location = new System.Drawing.Point(246, 67);
            this.cboComuni.Name = "cboComuni";
            this.cboComuni.Size = new System.Drawing.Size(177, 21);
            this.cboComuni.TabIndex = 52;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(74, 158);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(125, 20);
            this.txtCode.TabIndex = 53;
            // 
            // FrmCalcolaCodiceFisclae
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(449, 231);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.cboComuni);
            this.Controls.Add(this.cboProvince);
            this.Controls.Add(this.cboNazioni);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.txtCognome);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.SpCf);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCalcolaCodiceFisclae";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calcola codice fiscale";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button SpCf;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label7;
        private System.Windows.Forms.TextBox txtCognome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboNazioni;
        private System.Windows.Forms.ComboBox cboProvince;
        private System.Windows.Forms.ComboBox cboComuni;
        private System.Windows.Forms.TextBox txtCode;
    }
}