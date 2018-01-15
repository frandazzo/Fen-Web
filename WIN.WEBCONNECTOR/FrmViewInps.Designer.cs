namespace WIN.WEBCONNECTOR
{
    partial class FrmViewInps
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
            this.components = new System.ComponentModel.Container();
            this.lblsub = new DevExpress.XtraEditors.LabelControl();
            this.lblMail = new DevExpress.XtraEditors.LabelControl();
            this.cmdxml = new DevExpress.XtraEditors.SimpleButton();
            this.cmdExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.inpsTraceDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCOGNOME_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOME_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATA_NASCITA_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFISCALE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPROVINCIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMUNE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINDIRIZZO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSETTORE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATA_INIZIO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQUOTA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIPO_PRESTAZIONE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOME_REFERENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOGNOME_REFERENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFILE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inpsTraceDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblsub
            // 
            this.lblsub.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsub.Location = new System.Drawing.Point(19, 36);
            this.lblsub.Name = "lblsub";
            this.lblsub.Size = new System.Drawing.Size(45, 13);
            this.lblsub.TabIndex = 17;
            this.lblsub.Text = "Oggetto:";
            // 
            // lblMail
            // 
            this.lblMail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMail.Location = new System.Drawing.Point(19, 8);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(26, 13);
            this.lblMail.TabIndex = 16;
            this.lblMail.Text = "Mail:";
            // 
            // cmdxml
            // 
            this.cmdxml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdxml.Location = new System.Drawing.Point(137, 457);
            this.cmdxml.Name = "cmdxml";
            this.cmdxml.Size = new System.Drawing.Size(115, 23);
            this.cmdxml.TabIndex = 15;
            this.cmdxml.Text = "Esporta in xml";
            this.cmdxml.Click += new System.EventHandler(this.cmdxml_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExcel.Location = new System.Drawing.Point(2, 458);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(115, 23);
            this.cmdExcel.TabIndex = 14;
            this.cmdExcel.Text = "Esporta su Excel";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.inpsTraceDetailsBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 67);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(906, 385);
            this.gridControl1.TabIndex = 13;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // inpsTraceDetailsBindingSource
            // 
            this.inpsTraceDetailsBindingSource.DataSource = typeof(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.InpsTraceDetails);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCOGNOME_UTENTE,
            this.colNOME_UTENTE,
            this.colDATA_NASCITA_UTENTE,
            this.colFISCALE,
            this.colPROVINCIA,
            this.colCOMUNE,
            this.colINDIRIZZO,
            this.colCAP,
            this.colSETTORE,
            this.colDATA_INIZIO,
            this.colQUOTA,
            this.colTIPO_PRESTAZIONE,
            this.colNOME_REFERENTE,
            this.colCOGNOME_REFERENTE,
            this.colFILE_NAME});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.AllowZoomDetail = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsDetail.SmartDetailExpand = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // colCOGNOME_UTENTE
            // 
            this.colCOGNOME_UTENTE.FieldName = "COGNOME_UTENTE";
            this.colCOGNOME_UTENTE.Name = "colCOGNOME_UTENTE";
            this.colCOGNOME_UTENTE.Visible = true;
            this.colCOGNOME_UTENTE.VisibleIndex = 0;
            // 
            // colNOME_UTENTE
            // 
            this.colNOME_UTENTE.FieldName = "NOME_UTENTE";
            this.colNOME_UTENTE.Name = "colNOME_UTENTE";
            this.colNOME_UTENTE.Visible = true;
            this.colNOME_UTENTE.VisibleIndex = 1;
            // 
            // colDATA_NASCITA_UTENTE
            // 
            this.colDATA_NASCITA_UTENTE.FieldName = "DATA_NASCITA_UTENTE";
            this.colDATA_NASCITA_UTENTE.Name = "colDATA_NASCITA_UTENTE";
            this.colDATA_NASCITA_UTENTE.Visible = true;
            this.colDATA_NASCITA_UTENTE.VisibleIndex = 2;
            // 
            // colFISCALE
            // 
            this.colFISCALE.FieldName = "FISCALE";
            this.colFISCALE.Name = "colFISCALE";
            this.colFISCALE.Visible = true;
            this.colFISCALE.VisibleIndex = 3;
            // 
            // colPROVINCIA
            // 
            this.colPROVINCIA.FieldName = "PROVINCIA";
            this.colPROVINCIA.Name = "colPROVINCIA";
            this.colPROVINCIA.Visible = true;
            this.colPROVINCIA.VisibleIndex = 4;
            // 
            // colCOMUNE
            // 
            this.colCOMUNE.FieldName = "COMUNE";
            this.colCOMUNE.Name = "colCOMUNE";
            this.colCOMUNE.Visible = true;
            this.colCOMUNE.VisibleIndex = 5;
            // 
            // colINDIRIZZO
            // 
            this.colINDIRIZZO.FieldName = "INDIRIZZO";
            this.colINDIRIZZO.Name = "colINDIRIZZO";
            this.colINDIRIZZO.Visible = true;
            this.colINDIRIZZO.VisibleIndex = 6;
            // 
            // colCAP
            // 
            this.colCAP.FieldName = "CAP";
            this.colCAP.Name = "colCAP";
            this.colCAP.Visible = true;
            this.colCAP.VisibleIndex = 7;
            // 
            // colSETTORE
            // 
            this.colSETTORE.FieldName = "SETTORE";
            this.colSETTORE.Name = "colSETTORE";
            this.colSETTORE.Visible = true;
            this.colSETTORE.VisibleIndex = 8;
            // 
            // colDATA_INIZIO
            // 
            this.colDATA_INIZIO.FieldName = "DATA_INIZIO";
            this.colDATA_INIZIO.Name = "colDATA_INIZIO";
            this.colDATA_INIZIO.Visible = true;
            this.colDATA_INIZIO.VisibleIndex = 10;
            // 
            // colQUOTA
            // 
            this.colQUOTA.FieldName = "QUOTA";
            this.colQUOTA.Name = "colQUOTA";
            this.colQUOTA.Visible = true;
            this.colQUOTA.VisibleIndex = 11;
            // 
            // colTIPO_PRESTAZIONE
            // 
            this.colTIPO_PRESTAZIONE.FieldName = "TIPO_PRESTAZIONE";
            this.colTIPO_PRESTAZIONE.Name = "colTIPO_PRESTAZIONE";
            this.colTIPO_PRESTAZIONE.Visible = true;
            this.colTIPO_PRESTAZIONE.VisibleIndex = 12;
            // 
            // colNOME_REFERENTE
            // 
            this.colNOME_REFERENTE.FieldName = "NOME_REFERENTE";
            this.colNOME_REFERENTE.Name = "colNOME_REFERENTE";
            this.colNOME_REFERENTE.Visible = true;
            this.colNOME_REFERENTE.VisibleIndex = 13;
            // 
            // colCOGNOME_REFERENTE
            // 
            this.colCOGNOME_REFERENTE.FieldName = "COGNOME_REFERENTE";
            this.colCOGNOME_REFERENTE.Name = "colCOGNOME_REFERENTE";
            this.colCOGNOME_REFERENTE.Visible = true;
            this.colCOGNOME_REFERENTE.VisibleIndex = 14;
            // 
            // colFILE_NAME
            // 
            this.colFILE_NAME.FieldName = "FILE_NAME";
            this.colFILE_NAME.Name = "colFILE_NAME";
            this.colFILE_NAME.Visible = true;
            this.colFILE_NAME.VisibleIndex = 9;
            // 
            // FrmViewInps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 490);
            this.Controls.Add(this.lblsub);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.cmdxml);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmViewInps";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizza dati Inps";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inpsTraceDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblsub;
        private DevExpress.XtraEditors.LabelControl lblMail;
        private DevExpress.XtraEditors.SimpleButton cmdxml;
        private DevExpress.XtraEditors.SimpleButton cmdExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource inpsTraceDetailsBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevExpress.XtraGrid.Columns.GridColumn colCOGNOME_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOME_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colDATA_NASCITA_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colFISCALE;
        private DevExpress.XtraGrid.Columns.GridColumn colPROVINCIA;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMUNE;
        private DevExpress.XtraGrid.Columns.GridColumn colINDIRIZZO;
        private DevExpress.XtraGrid.Columns.GridColumn colCAP;
        private DevExpress.XtraGrid.Columns.GridColumn colSETTORE;
        private DevExpress.XtraGrid.Columns.GridColumn colDATA_INIZIO;
        private DevExpress.XtraGrid.Columns.GridColumn colQUOTA;
        private DevExpress.XtraGrid.Columns.GridColumn colTIPO_PRESTAZIONE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOME_REFERENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colCOGNOME_REFERENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colFILE_NAME;
    }
}