namespace WIN.WEBCONNECTOR.controls
{
    partial class ReportDatiEconomiciControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.administrativeRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRegion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProvince = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBilateral = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpecificBilateral = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanies = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeclaredSalary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGivenSalary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQACP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQACR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQACN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelegheAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPending = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.datagroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.nodatagroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.administrativeRecordBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodatagroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.comboBoxEdit1);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(907, 551);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(455, 14);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(440, 22);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "Ricerca";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_2);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(106, 14);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026"});
            this.comboBoxEdit1.Size = new System.Drawing.Size(345, 20);
            this.comboBoxEdit1.StyleController = this.layoutControl1;
            this.comboBoxEdit1.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(24, 71);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(335, 39);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Nessun dato trovato!";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.administrativeRecordBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(24, 157);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemImageComboBox2,
            this.repositoryItemImageComboBox3});
            this.gridControl1.Size = new System.Drawing.Size(859, 370);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // administrativeRecordBindingSource
            // 
            this.administrativeRecordBindingSource.DataSource = typeof(WIN.WEBCONNECTOR.SharetopServices.AdministrativeRecord);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRegion,
            this.colProvince,
            this.colBilateral,
            this.colSpecificBilateral,
            this.colYear,
            this.colWorkers,
            this.colCompanies,
            this.colDeclaredSalary,
            this.colGivenSalary,
            this.colQACP,
            this.colQACR,
            this.colQACN,
            this.colDelegheAmount,
            this.colPending});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Workers", this.colWorkers, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Companies", this.colCompanies, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DeclaredSalary", this.colDeclaredSalary, "{0:c2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GivenSalary", this.colGivenSalary, "{0:c2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QACN", this.colQACN, "{0:c2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QACP", this.colQACP, "{0:c2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QACR", this.colQACR, "{0:c2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DelegheAmount", this.colDelegheAmount, "{0:c2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Pending", this.colPending, "{0:c2}")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.AllowZoomDetail = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsDetail.SmartDetailExpand = false;
            this.gridView1.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            this.gridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.gridView1.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // colRegion
            // 
            this.colRegion.Caption = "Regione";
            this.colRegion.FieldName = "Region";
            this.colRegion.Name = "colRegion";
            this.colRegion.Visible = true;
            this.colRegion.VisibleIndex = 0;
            // 
            // colProvince
            // 
            this.colProvince.Caption = "Provincia";
            this.colProvince.FieldName = "Province";
            this.colProvince.Name = "colProvince";
            this.colProvince.Visible = true;
            this.colProvince.VisibleIndex = 1;
            // 
            // colBilateral
            // 
            this.colBilateral.Caption = "Ente bilaterale";
            this.colBilateral.FieldName = "Bilateral";
            this.colBilateral.Name = "colBilateral";
            this.colBilateral.Visible = true;
            this.colBilateral.VisibleIndex = 2;
            // 
            // colSpecificBilateral
            // 
            this.colSpecificBilateral.Caption = "Sotto ente bilaterale";
            this.colSpecificBilateral.FieldName = "SpecificBilateral";
            this.colSpecificBilateral.Name = "colSpecificBilateral";
            this.colSpecificBilateral.Visible = true;
            this.colSpecificBilateral.VisibleIndex = 3;
            // 
            // colYear
            // 
            this.colYear.Caption = "Anno";
            this.colYear.FieldName = "Year";
            this.colYear.Name = "colYear";
            this.colYear.Visible = true;
            this.colYear.VisibleIndex = 4;
            // 
            // colWorkers
            // 
            this.colWorkers.Caption = "Addetti";
            this.colWorkers.DisplayFormat.FormatString = "n0";
            this.colWorkers.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWorkers.FieldName = "Workers";
            this.colWorkers.Name = "colWorkers";
            this.colWorkers.Visible = true;
            this.colWorkers.VisibleIndex = 5;
            // 
            // colCompanies
            // 
            this.colCompanies.Caption = "Aziende";
            this.colCompanies.CustomizationCaption = "n0";
            this.colCompanies.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCompanies.FieldName = "Companies";
            this.colCompanies.Name = "colCompanies";
            this.colCompanies.Visible = true;
            this.colCompanies.VisibleIndex = 6;
            // 
            // colDeclaredSalary
            // 
            this.colDeclaredSalary.Caption = "Monte salari denunciato";
            this.colDeclaredSalary.DisplayFormat.FormatString = "c2";
            this.colDeclaredSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDeclaredSalary.FieldName = "DeclaredSalary";
            this.colDeclaredSalary.Name = "colDeclaredSalary";
            this.colDeclaredSalary.Visible = true;
            this.colDeclaredSalary.VisibleIndex = 7;
            // 
            // colGivenSalary
            // 
            this.colGivenSalary.Caption = "Monte salari versato";
            this.colGivenSalary.DisplayFormat.FormatString = "c2";
            this.colGivenSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colGivenSalary.FieldName = "GivenSalary";
            this.colGivenSalary.Name = "colGivenSalary";
            this.colGivenSalary.Visible = true;
            this.colGivenSalary.VisibleIndex = 8;
            // 
            // colQACP
            // 
            this.colQACP.Caption = "Importo QACP Feneal";
            this.colQACP.DisplayFormat.FormatString = "c2";
            this.colQACP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colQACP.FieldName = "QACP";
            this.colQACP.Name = "colQACP";
            this.colQACP.Visible = true;
            this.colQACP.VisibleIndex = 9;
            // 
            // colQACR
            // 
            this.colQACR.Caption = "Importo QACR Feneal";
            this.colQACR.DisplayFormat.FormatString = "c2";
            this.colQACR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colQACR.FieldName = "QACR";
            this.colQACR.Name = "colQACR";
            this.colQACR.Visible = true;
            this.colQACR.VisibleIndex = 10;
            // 
            // colQACN
            // 
            this.colQACN.Caption = "Importo QACR Feneal";
            this.colQACN.DisplayFormat.FormatString = "c2";
            this.colQACN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colQACN.FieldName = "QACN";
            this.colQACN.Name = "colQACN";
            this.colQACN.Visible = true;
            this.colQACN.VisibleIndex = 11;
            // 
            // colDelegheAmount
            // 
            this.colDelegheAmount.Caption = "Importo Deleghe Feneal";
            this.colDelegheAmount.DisplayFormat.FormatString = "c2";
            this.colDelegheAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDelegheAmount.FieldName = "DelegheAmount";
            this.colDelegheAmount.Name = "colDelegheAmount";
            this.colDelegheAmount.Visible = true;
            this.colDelegheAmount.VisibleIndex = 12;
            // 
            // colPending
            // 
            this.colPending.Caption = "Saldi e/o arretrati vs Feneal Naz";
            this.colPending.DisplayFormat.FormatString = "c2";
            this.colPending.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPending.FieldName = "Pending";
            this.colPending.Name = "colPending";
            this.colPending.Visible = true;
            this.colPending.VisibleIndex = 13;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pianificato", "Pianificato", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In_Scadenza", "In_Scadenza", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Scade_Oggi", "Scade_Oggi", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Scaduto", "Scaduto", 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Eseguito", "Eseguito", 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Concluso", "Concluso", 5)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Completata", "CompleteFlag", 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Urgente", "SuperRedFlag", 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Scade domani", "RedFlag", 8),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Scade prossimamente", "MinorRedFlag", 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Non urgente", "EmptyFlag", 14)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            // 
            // repositoryItemImageComboBox3
            // 
            this.repositoryItemImageComboBox3.AutoHeight = false;
            this.repositoryItemImageComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox3.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Priorità alta", "Alta", 12),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Priorità bassa", "Bassa", 13),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Priorità normale", "Normale", 15)});
            this.repositoryItemImageComboBox3.Name = "repositoryItemImageComboBox3";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleSeparator1,
            this.datagroup,
            this.nodatagroup,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(907, 551);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.CustomizationFormText = "simpleSeparator1";
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 0);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(887, 2);
            this.simpleSeparator1.Text = "simpleSeparator1";
            // 
            // datagroup
            // 
            this.datagroup.CustomizationFormText = "Risultati";
            this.datagroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.datagroup.Location = new System.Drawing.Point(0, 114);
            this.datagroup.Name = "datagroup";
            this.datagroup.Size = new System.Drawing.Size(887, 417);
            this.datagroup.Text = "Report dati casse edili";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(863, 374);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // nodatagroup
            // 
            this.nodatagroup.CustomizationFormText = "Nessun risultato trovato";
            this.nodatagroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.nodatagroup.Location = new System.Drawing.Point(0, 28);
            this.nodatagroup.Name = "nodatagroup";
            this.nodatagroup.Size = new System.Drawing.Size(887, 86);
            this.nodatagroup.Text = "Nessun risultato trovato";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(863, 43);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEdit1;
            this.layoutControlItem2.CustomizationFormText = "Anno di riferimento";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 2);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(443, 26);
            this.layoutControlItem2.Text = "Anno di riferimento";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(91, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(443, 2);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(444, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // ReportDatiEconomiciControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ReportDatiEconomiciControl";
            this.Size = new System.Drawing.Size(907, 551);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.administrativeRecordBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodatagroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraGrid.GridControl gridControl1;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        protected DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        protected DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        protected DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox3;
        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        protected DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        protected DevExpress.XtraLayout.LayoutControlGroup datagroup;
        protected DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup nodatagroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource administrativeRecordBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colRegion;
        private DevExpress.XtraGrid.Columns.GridColumn colProvince;
        private DevExpress.XtraGrid.Columns.GridColumn colBilateral;
        private DevExpress.XtraGrid.Columns.GridColumn colSpecificBilateral;
        private DevExpress.XtraGrid.Columns.GridColumn colYear;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkers;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanies;
        private DevExpress.XtraGrid.Columns.GridColumn colDeclaredSalary;
        private DevExpress.XtraGrid.Columns.GridColumn colGivenSalary;
        private DevExpress.XtraGrid.Columns.GridColumn colQACP;
        private DevExpress.XtraGrid.Columns.GridColumn colQACR;
        private DevExpress.XtraGrid.Columns.GridColumn colQACN;
        private DevExpress.XtraGrid.Columns.GridColumn colDelegheAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPending;
    }
}
