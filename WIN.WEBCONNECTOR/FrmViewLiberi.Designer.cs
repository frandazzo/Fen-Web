namespace WIN.WEBCONNECTOR
{
    partial class FrmViewLiberi
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
            this.lblMail = new DevExpress.XtraEditors.LabelControl();
            this.cmdxml = new DevExpress.XtraEditors.SimpleButton();
            this.cmdExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.liberiTraceDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCOGNOME_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOME_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATA_NASCITA_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFISCALE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPROVINCIA_NASCITA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMUNE_NASCITA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPROVINCIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMUNE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINDIRIZZO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTELEFONO1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTELEFONO2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODICE_CE_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODICE_EC_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOTE_UTENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colENTE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAZIENDA_IMPIEGO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPARTITA_IVA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPROVINCIA_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMUNE_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINDIRIZZO_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCAP_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTELEFONO_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOTE_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODICE_CE_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCODICE_EC_AZIENDA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCONTRATTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLIVELLO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISCRITTO_A = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblsub = new DevExpress.XtraEditors.LabelControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liberiTraceDetailBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMail
            // 
            this.lblMail.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMail.Location = new System.Drawing.Point(29, 12);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(26, 13);
            this.lblMail.TabIndex = 11;
            this.lblMail.Text = "Mail:";
            this.lblMail.Click += new System.EventHandler(this.lblMail_Click);
            // 
            // cmdxml
            // 
            this.cmdxml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdxml.Location = new System.Drawing.Point(146, 389);
            this.cmdxml.Name = "cmdxml";
            this.cmdxml.Size = new System.Drawing.Size(115, 23);
            this.cmdxml.TabIndex = 10;
            this.cmdxml.Text = "Esporta in xml";
            this.cmdxml.Click += new System.EventHandler(this.cmdxml_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExcel.Location = new System.Drawing.Point(11, 390);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(115, 23);
            this.cmdExcel.TabIndex = 9;
            this.cmdExcel.Text = "Esporta su Excel";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.liberiTraceDetailBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 71);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1152, 312);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // liberiTraceDetailBindingSource
            // 
            this.liberiTraceDetailBindingSource.DataSource = typeof(WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport.LiberiTraceDetail);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCOGNOME_UTENTE,
            this.colNOME_UTENTE,
            this.colDATA_NASCITA_UTENTE,
            this.colFISCALE,
            this.colPROVINCIA_NASCITA,
            this.colCOMUNE_NASCITA,
            this.colPROVINCIA,
            this.colCOMUNE,
            this.colINDIRIZZO,
            this.colCAP,
            this.colTELEFONO1,
            this.colTELEFONO2,
            this.colCODICE_CE_UTENTE,
            this.colCODICE_EC_UTENTE,
            this.colNOTE_UTENTE,
            this.colENTE,
            this.colAZIENDA_IMPIEGO,
            this.colPARTITA_IVA,
            this.colPROVINCIA_AZIENDA,
            this.colCOMUNE_AZIENDA,
            this.colINDIRIZZO_AZIENDA,
            this.colCAP_AZIENDA,
            this.colTELEFONO_AZIENDA,
            this.colNOTE_AZIENDA,
            this.colCODICE_CE_AZIENDA,
            this.colCODICE_EC_AZIENDA,
            this.colCONTRATTO,
            this.colDATA,
            this.colLIVELLO,
            this.colISCRITTO_A});
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
            // colPROVINCIA_NASCITA
            // 
            this.colPROVINCIA_NASCITA.FieldName = "PROVINCIA_NASCITA";
            this.colPROVINCIA_NASCITA.Name = "colPROVINCIA_NASCITA";
            this.colPROVINCIA_NASCITA.Visible = true;
            this.colPROVINCIA_NASCITA.VisibleIndex = 4;
            // 
            // colCOMUNE_NASCITA
            // 
            this.colCOMUNE_NASCITA.FieldName = "COMUNE_NASCITA";
            this.colCOMUNE_NASCITA.Name = "colCOMUNE_NASCITA";
            this.colCOMUNE_NASCITA.Visible = true;
            this.colCOMUNE_NASCITA.VisibleIndex = 5;
            // 
            // colPROVINCIA
            // 
            this.colPROVINCIA.FieldName = "PROVINCIA";
            this.colPROVINCIA.Name = "colPROVINCIA";
            this.colPROVINCIA.Visible = true;
            this.colPROVINCIA.VisibleIndex = 6;
            // 
            // colCOMUNE
            // 
            this.colCOMUNE.FieldName = "COMUNE";
            this.colCOMUNE.Name = "colCOMUNE";
            this.colCOMUNE.Visible = true;
            this.colCOMUNE.VisibleIndex = 7;
            // 
            // colINDIRIZZO
            // 
            this.colINDIRIZZO.FieldName = "INDIRIZZO";
            this.colINDIRIZZO.Name = "colINDIRIZZO";
            this.colINDIRIZZO.Visible = true;
            this.colINDIRIZZO.VisibleIndex = 8;
            // 
            // colCAP
            // 
            this.colCAP.FieldName = "CAP";
            this.colCAP.Name = "colCAP";
            this.colCAP.Visible = true;
            this.colCAP.VisibleIndex = 9;
            // 
            // colTELEFONO1
            // 
            this.colTELEFONO1.FieldName = "TELEFONO1";
            this.colTELEFONO1.Name = "colTELEFONO1";
            this.colTELEFONO1.Visible = true;
            this.colTELEFONO1.VisibleIndex = 10;
            // 
            // colTELEFONO2
            // 
            this.colTELEFONO2.FieldName = "TELEFONO2";
            this.colTELEFONO2.Name = "colTELEFONO2";
            this.colTELEFONO2.Visible = true;
            this.colTELEFONO2.VisibleIndex = 11;
            // 
            // colCODICE_CE_UTENTE
            // 
            this.colCODICE_CE_UTENTE.FieldName = "CODICE_CE_UTENTE";
            this.colCODICE_CE_UTENTE.Name = "colCODICE_CE_UTENTE";
            this.colCODICE_CE_UTENTE.Visible = true;
            this.colCODICE_CE_UTENTE.VisibleIndex = 12;
            // 
            // colCODICE_EC_UTENTE
            // 
            this.colCODICE_EC_UTENTE.FieldName = "CODICE_EC_UTENTE";
            this.colCODICE_EC_UTENTE.Name = "colCODICE_EC_UTENTE";
            this.colCODICE_EC_UTENTE.Visible = true;
            this.colCODICE_EC_UTENTE.VisibleIndex = 13;
            // 
            // colNOTE_UTENTE
            // 
            this.colNOTE_UTENTE.FieldName = "NOTE_UTENTE";
            this.colNOTE_UTENTE.Name = "colNOTE_UTENTE";
            this.colNOTE_UTENTE.Visible = true;
            this.colNOTE_UTENTE.VisibleIndex = 14;
            // 
            // colENTE
            // 
            this.colENTE.FieldName = "ENTE";
            this.colENTE.Name = "colENTE";
            this.colENTE.Visible = true;
            this.colENTE.VisibleIndex = 15;
            // 
            // colAZIENDA_IMPIEGO
            // 
            this.colAZIENDA_IMPIEGO.FieldName = "AZIENDA_IMPIEGO";
            this.colAZIENDA_IMPIEGO.Name = "colAZIENDA_IMPIEGO";
            this.colAZIENDA_IMPIEGO.Visible = true;
            this.colAZIENDA_IMPIEGO.VisibleIndex = 16;
            // 
            // colPARTITA_IVA
            // 
            this.colPARTITA_IVA.FieldName = "PARTITA_IVA";
            this.colPARTITA_IVA.Name = "colPARTITA_IVA";
            this.colPARTITA_IVA.Visible = true;
            this.colPARTITA_IVA.VisibleIndex = 17;
            // 
            // colPROVINCIA_AZIENDA
            // 
            this.colPROVINCIA_AZIENDA.FieldName = "PROVINCIA_AZIENDA";
            this.colPROVINCIA_AZIENDA.Name = "colPROVINCIA_AZIENDA";
            this.colPROVINCIA_AZIENDA.Visible = true;
            this.colPROVINCIA_AZIENDA.VisibleIndex = 18;
            // 
            // colCOMUNE_AZIENDA
            // 
            this.colCOMUNE_AZIENDA.FieldName = "COMUNE_AZIENDA";
            this.colCOMUNE_AZIENDA.Name = "colCOMUNE_AZIENDA";
            this.colCOMUNE_AZIENDA.Visible = true;
            this.colCOMUNE_AZIENDA.VisibleIndex = 19;
            // 
            // colINDIRIZZO_AZIENDA
            // 
            this.colINDIRIZZO_AZIENDA.FieldName = "INDIRIZZO_AZIENDA";
            this.colINDIRIZZO_AZIENDA.Name = "colINDIRIZZO_AZIENDA";
            this.colINDIRIZZO_AZIENDA.Visible = true;
            this.colINDIRIZZO_AZIENDA.VisibleIndex = 20;
            // 
            // colCAP_AZIENDA
            // 
            this.colCAP_AZIENDA.FieldName = "CAP_AZIENDA";
            this.colCAP_AZIENDA.Name = "colCAP_AZIENDA";
            this.colCAP_AZIENDA.Visible = true;
            this.colCAP_AZIENDA.VisibleIndex = 21;
            // 
            // colTELEFONO_AZIENDA
            // 
            this.colTELEFONO_AZIENDA.FieldName = "TELEFONO_AZIENDA";
            this.colTELEFONO_AZIENDA.Name = "colTELEFONO_AZIENDA";
            this.colTELEFONO_AZIENDA.Visible = true;
            this.colTELEFONO_AZIENDA.VisibleIndex = 22;
            // 
            // colNOTE_AZIENDA
            // 
            this.colNOTE_AZIENDA.FieldName = "NOTE_AZIENDA";
            this.colNOTE_AZIENDA.Name = "colNOTE_AZIENDA";
            this.colNOTE_AZIENDA.Visible = true;
            this.colNOTE_AZIENDA.VisibleIndex = 23;
            // 
            // colCODICE_CE_AZIENDA
            // 
            this.colCODICE_CE_AZIENDA.FieldName = "CODICE_CE_AZIENDA";
            this.colCODICE_CE_AZIENDA.Name = "colCODICE_CE_AZIENDA";
            this.colCODICE_CE_AZIENDA.Visible = true;
            this.colCODICE_CE_AZIENDA.VisibleIndex = 24;
            // 
            // colCODICE_EC_AZIENDA
            // 
            this.colCODICE_EC_AZIENDA.FieldName = "CODICE_EC_AZIENDA";
            this.colCODICE_EC_AZIENDA.Name = "colCODICE_EC_AZIENDA";
            this.colCODICE_EC_AZIENDA.Visible = true;
            this.colCODICE_EC_AZIENDA.VisibleIndex = 25;
            // 
            // colCONTRATTO
            // 
            this.colCONTRATTO.FieldName = "CONTRATTO";
            this.colCONTRATTO.Name = "colCONTRATTO";
            this.colCONTRATTO.Visible = true;
            this.colCONTRATTO.VisibleIndex = 26;
            // 
            // colDATA
            // 
            this.colDATA.FieldName = "DATA";
            this.colDATA.Name = "colDATA";
            this.colDATA.Visible = true;
            this.colDATA.VisibleIndex = 27;
            // 
            // colLIVELLO
            // 
            this.colLIVELLO.FieldName = "LIVELLO";
            this.colLIVELLO.Name = "colLIVELLO";
            this.colLIVELLO.Visible = true;
            this.colLIVELLO.VisibleIndex = 28;
            // 
            // colISCRITTO_A
            // 
            this.colISCRITTO_A.FieldName = "ISCRITTO_A";
            this.colISCRITTO_A.Name = "colISCRITTO_A";
            this.colISCRITTO_A.Visible = true;
            this.colISCRITTO_A.VisibleIndex = 29;
            // 
            // lblsub
            // 
            this.lblsub.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsub.Location = new System.Drawing.Point(29, 40);
            this.lblsub.Name = "lblsub";
            this.lblsub.Size = new System.Drawing.Size(45, 13);
            this.lblsub.TabIndex = 12;
            this.lblsub.Text = "Oggetto:";
            this.lblsub.Click += new System.EventHandler(this.lblsub_Click);
            // 
            // FrmViewLiberi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 421);
            this.Controls.Add(this.lblsub);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.cmdxml);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.gridControl1);
            this.Name = "FrmViewLiberi";
            this.Text = "FrmViewLiberi";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liberiTraceDetailBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblMail;
        private DevExpress.XtraEditors.SimpleButton cmdxml;
        private DevExpress.XtraEditors.SimpleButton cmdExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource liberiTraceDetailBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCOGNOME_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOME_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colDATA_NASCITA_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colFISCALE;
        private DevExpress.XtraGrid.Columns.GridColumn colPROVINCIA_NASCITA;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMUNE_NASCITA;
        private DevExpress.XtraGrid.Columns.GridColumn colPROVINCIA;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMUNE;
        private DevExpress.XtraGrid.Columns.GridColumn colINDIRIZZO;
        private DevExpress.XtraGrid.Columns.GridColumn colCAP;
        private DevExpress.XtraGrid.Columns.GridColumn colTELEFONO1;
        private DevExpress.XtraGrid.Columns.GridColumn colTELEFONO2;
        private DevExpress.XtraGrid.Columns.GridColumn colCODICE_CE_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colCODICE_EC_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colNOTE_UTENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colENTE;
        private DevExpress.XtraGrid.Columns.GridColumn colAZIENDA_IMPIEGO;
        private DevExpress.XtraGrid.Columns.GridColumn colPARTITA_IVA;
        private DevExpress.XtraGrid.Columns.GridColumn colPROVINCIA_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMUNE_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colINDIRIZZO_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colCAP_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colTELEFONO_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colNOTE_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colCODICE_CE_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colCODICE_EC_AZIENDA;
        private DevExpress.XtraGrid.Columns.GridColumn colCONTRATTO;
        private DevExpress.XtraGrid.Columns.GridColumn colDATA;
        private DevExpress.XtraGrid.Columns.GridColumn colLIVELLO;
        private DevExpress.XtraGrid.Columns.GridColumn colISCRITTO_A;
        private DevExpress.XtraEditors.LabelControl lblsub;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}