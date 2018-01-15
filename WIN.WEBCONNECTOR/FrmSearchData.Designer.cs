namespace WIN.WEBCONNECTOR
{
    partial class FrmSearchData
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
            this.cboComuni = new System.Windows.Forms.ComboBox();
            this.cboProvince = new System.Windows.Forms.ComboBox();
            this.cboNazioni = new System.Windows.Forms.ComboBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtCognome = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtAzienda = new System.Windows.Forms.TextBox();
            this.txtPiva = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grpaz = new System.Windows.Forms.GroupBox();
            this.grpAnag = new System.Windows.Forms.GroupBox();
            this.optAnag = new System.Windows.Forms.RadioButton();
            this.optAz = new System.Windows.Forms.RadioButton();
            this.grpaz.SuspendLayout();
            this.grpAnag.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboComuni
            // 
            this.cboComuni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComuni.FormattingEnabled = true;
            this.cboComuni.Location = new System.Drawing.Point(73, 158);
            this.cboComuni.Name = "cboComuni";
            this.cboComuni.Size = new System.Drawing.Size(177, 21);
            this.cboComuni.TabIndex = 12;
            // 
            // cboProvince
            // 
            this.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvince.FormattingEnabled = true;
            this.cboProvince.Location = new System.Drawing.Point(73, 130);
            this.cboProvince.Name = "cboProvince";
            this.cboProvince.Size = new System.Drawing.Size(177, 21);
            this.cboProvince.TabIndex = 10;
            this.cboProvince.SelectedIndexChanged += new System.EventHandler(this.cboProvince_SelectedIndexChanged);
            // 
            // cboNazioni
            // 
            this.cboNazioni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNazioni.FormattingEnabled = true;
            this.cboNazioni.Location = new System.Drawing.Point(73, 103);
            this.cboNazioni.Name = "cboNazioni";
            this.cboNazioni.Size = new System.Drawing.Size(177, 21);
            this.cboNazioni.TabIndex = 8;
            this.cboNazioni.SelectedIndexChanged += new System.EventHandler(this.cboNazioni_SelectedIndexChanged);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(38, 112);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(29, 13);
            this.Label11.TabIndex = 7;
            this.Label11.Text = "Naz.";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(13, 166);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(51, 13);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Com. res.";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(24, 137);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(40, 13);
            this.Label7.TabIndex = 9;
            this.Label7.Text = "Pr. res.";
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(73, 77);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(115, 20);
            this.dtpDate.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(25, 83);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "D. nas.";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(72, 45);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(156, 20);
            this.txtNome.TabIndex = 3;
            // 
            // txtCognome
            // 
            this.txtCognome.Location = new System.Drawing.Point(72, 18);
            this.txtCognome.Name = "txtCognome";
            this.txtCognome.Size = new System.Drawing.Size(156, 20);
            this.txtCognome.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(31, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Nome";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(31, 27);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Cogn.";
            // 
            // Button1
            // 
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button1.Location = new System.Drawing.Point(213, 350);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(60, 25);
            this.Button1.TabIndex = 14;
            this.Button1.Text = "Chiudi";
            this.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(140, 350);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(67, 25);
            this.cmdSearch.TabIndex = 13;
            this.cmdSearch.Text = "Ricerca";
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new System.Drawing.Point(201, 81);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(15, 14);
            this.chkDate.TabIndex = 6;
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(20, 333);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Visibilità nazionale";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtAzienda
            // 
            this.txtAzienda.Location = new System.Drawing.Point(73, 56);
            this.txtAzienda.Name = "txtAzienda";
            this.txtAzienda.Size = new System.Drawing.Size(156, 20);
            this.txtAzienda.TabIndex = 20;
            // 
            // txtPiva
            // 
            this.txtPiva.Location = new System.Drawing.Point(73, 29);
            this.txtPiva.Name = "txtPiva";
            this.txtPiva.Size = new System.Drawing.Size(156, 20);
            this.txtPiva.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Azienda";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "P. IVA";
            // 
            // grpaz
            // 
            this.grpaz.Controls.Add(this.txtAzienda);
            this.grpaz.Controls.Add(this.label8);
            this.grpaz.Controls.Add(this.txtPiva);
            this.grpaz.Controls.Add(this.label6);
            this.grpaz.Enabled = false;
            this.grpaz.Location = new System.Drawing.Point(12, 230);
            this.grpaz.Name = "grpaz";
            this.grpaz.Size = new System.Drawing.Size(269, 97);
            this.grpaz.TabIndex = 21;
            this.grpaz.TabStop = false;
            this.grpaz.Text = "Ricerca per azienda";
            // 
            // grpAnag
            // 
            this.grpAnag.Controls.Add(this.txtCognome);
            this.grpAnag.Controls.Add(this.Label7);
            this.grpAnag.Controls.Add(this.Label4);
            this.grpAnag.Controls.Add(this.chkDate);
            this.grpAnag.Controls.Add(this.Label11);
            this.grpAnag.Controls.Add(this.cboNazioni);
            this.grpAnag.Controls.Add(this.cboProvince);
            this.grpAnag.Controls.Add(this.txtNome);
            this.grpAnag.Controls.Add(this.cboComuni);
            this.grpAnag.Controls.Add(this.Label1);
            this.grpAnag.Controls.Add(this.Label2);
            this.grpAnag.Controls.Add(this.dtpDate);
            this.grpAnag.Controls.Add(this.Label3);
            this.grpAnag.Location = new System.Drawing.Point(12, 34);
            this.grpAnag.Name = "grpAnag";
            this.grpAnag.Size = new System.Drawing.Size(269, 190);
            this.grpAnag.TabIndex = 22;
            this.grpAnag.TabStop = false;
            this.grpAnag.Text = "Ricerca per dati anagrafici";
            // 
            // optAnag
            // 
            this.optAnag.AutoSize = true;
            this.optAnag.Checked = true;
            this.optAnag.Location = new System.Drawing.Point(20, 8);
            this.optAnag.Name = "optAnag";
            this.optAnag.Size = new System.Drawing.Size(124, 17);
            this.optAnag.TabIndex = 23;
            this.optAnag.TabStop = true;
            this.optAnag.Text = "Cerca per anagrafica";
            this.optAnag.UseVisualStyleBackColor = true;
            this.optAnag.CheckedChanged += new System.EventHandler(this.optAnag_CheckedChanged);
            // 
            // optAz
            // 
            this.optAz.AutoSize = true;
            this.optAz.Location = new System.Drawing.Point(151, 8);
            this.optAz.Name = "optAz";
            this.optAz.Size = new System.Drawing.Size(111, 17);
            this.optAz.TabIndex = 24;
            this.optAz.TabStop = true;
            this.optAz.Text = "Cerca per azienda";
            this.optAz.UseVisualStyleBackColor = true;
            // 
            // FrmSearchData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(300, 398);
            this.Controls.Add(this.optAz);
            this.Controls.Add(this.optAnag);
            this.Controls.Add(this.grpAnag);
            this.Controls.Add(this.grpaz);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.cmdSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSearchData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ricerca lavoratori";
            this.grpaz.ResumeLayout(false);
            this.grpaz.PerformLayout();
            this.grpAnag.ResumeLayout(false);
            this.grpAnag.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboComuni;
        private System.Windows.Forms.ComboBox cboProvince;
        private System.Windows.Forms.ComboBox cboNazioni;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label7;
        private System.Windows.Forms.DateTimePicker dtpDate;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtCognome;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtAzienda;
        private System.Windows.Forms.TextBox txtPiva;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpaz;
        private System.Windows.Forms.GroupBox grpAnag;
        private System.Windows.Forms.RadioButton optAnag;
        private System.Windows.Forms.RadioButton optAz;
    }
}