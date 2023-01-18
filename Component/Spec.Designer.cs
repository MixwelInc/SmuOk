namespace SmuOk.Component
{
  partial class Spec
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSpec = new System.Windows.Forms.DataGridView();
            this.lstSpecTypeFilter = new System.Windows.Forms.ComboBox();
            this.lblPb = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.lstSpecDone = new System.Windows.Forms.ComboBox();
            this.lstSpecHasFillingFilter = new System.Windows.Forms.ComboBox();
            this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
            this.lstSpecManagerAO = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnReportF7 = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExportCurator = new System.Windows.Forms.Button();
            this.btnExportManager = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.filter1 = new System.Windows.Forms.ComboBox();
            this.filter2 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.dgv_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_id_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_has_filling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__Curator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__SContractNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SExecutor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVProjectSignDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVProjectBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_NewestFillingCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SDog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SBudget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SBudgetTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_btn_folder,
            this.dgv_id_SId,
            this.dgv_has_filling,
            this.dgv_SSystem,
            this.dgv_SStation,
            this.dgv__Curator,
            this.dgv__SContractNum,
            this.dgv_SVName,
            this.dgv_STName,
            this.dgv_SExecutor,
            this.dgv_SArea,
            this.dgv_SNo,
            this.dgv_SVNo,
            this.dgv_SVStage,
            this.dgv_SVProjectSignDate,
            this.dgv_SVProjectBy,
            this.dgv_SVDate,
            this.dgv_SComment,
            this.dgv_SObject,
            this.dgv_NewestFillingCount,
            this.dgv_SDog,
            this.dgv_SBudget,
            this.dgv_SBudgetTotal,
            this.dgv_SState});
            this.dgvSpec.Location = new System.Drawing.Point(3, 85);
            this.dgvSpec.Name = "dgvSpec";
            this.dgvSpec.ReadOnly = true;
            this.dgvSpec.RowHeadersVisible = false;
            this.dgvSpec.Size = new System.Drawing.Size(1622, 444);
            this.dgvSpec.TabIndex = 1;
            this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            this.dgvSpec.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellLeave);
            this.dgvSpec.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellMouseEnter);
            this.dgvSpec.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvSpec_RowPrePaint);
            // 
            // lstSpecTypeFilter
            // 
            this.lstSpecTypeFilter.FormattingEnabled = true;
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(359, 3);
            this.lstSpecTypeFilter.Name = "lstSpecTypeFilter";
            this.lstSpecTypeFilter.Size = new System.Drawing.Size(150, 21);
            this.lstSpecTypeFilter.TabIndex = 34;
            this.lstSpecTypeFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lblPb
            // 
            this.lblPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPb.AutoSize = true;
            this.lblPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPb.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPb.Location = new System.Drawing.Point(1314, 4);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 69;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(1317, 20);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(307, 5);
            this.pb.TabIndex = 68;
            this.pb.Tag = "lblPb";
            this.pb.Visible = false;
            // 
            // lstSpecDone
            // 
            this.lstSpecDone.FormattingEnabled = true;
            this.lstSpecDone.Location = new System.Drawing.Point(642, 3);
            this.lstSpecDone.Name = "lstSpecDone";
            this.lstSpecDone.Size = new System.Drawing.Size(101, 21);
            this.lstSpecDone.TabIndex = 70;
            this.lstSpecDone.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(515, 3);
            this.lstSpecHasFillingFilter.Name = "lstSpecHasFillingFilter";
            this.lstSpecHasFillingFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecHasFillingFilter.TabIndex = 71;
            this.lstSpecHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // txtSpecNameFilter
            // 
            this.txtSpecNameFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtSpecNameFilter.Location = new System.Drawing.Point(3, 4);
            this.txtSpecNameFilter.Name = "txtSpecNameFilter";
            this.txtSpecNameFilter.Size = new System.Drawing.Size(151, 20);
            this.txtSpecNameFilter.TabIndex = 73;
            this.txtSpecNameFilter.Tag = "Шифр...";
            this.txtSpecNameFilter.Enter += new System.EventHandler(this.txtSpecNameFilter_Enter);
            this.txtSpecNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSpecNameFilter_KeyUp);
            this.txtSpecNameFilter.Leave += new System.EventHandler(this.txtSpecNameFilter_Leave);
            // 
            // lstSpecManagerAO
            // 
            this.lstSpecManagerAO.FormattingEnabled = true;
            this.lstSpecManagerAO.Location = new System.Drawing.Point(749, 3);
            this.lstSpecManagerAO.Name = "lstSpecManagerAO";
            this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
            this.lstSpecManagerAO.TabIndex = 70;
            this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "0";
            this.dataGridViewImageColumn1.Image = global::SmuOk.Properties.Resources.shared;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 28;
            // 
            // btnReportF7
            // 
            this.btnReportF7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReportF7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportF7.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnReportF7.FlatAppearance.BorderSize = 0;
            this.btnReportF7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportF7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnReportF7.Image = global::SmuOk.Properties.Resources.report_excel;
            this.btnReportF7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportF7.Location = new System.Drawing.Point(230, 531);
            this.btnReportF7.Name = "btnReportF7";
            this.btnReportF7.Size = new System.Drawing.Size(50, 23);
            this.btnReportF7.TabIndex = 32;
            this.btnReportF7.Text = "Ф7";
            this.btnReportF7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportF7.UseVisualStyleBackColor = true;
            this.btnReportF7.Click += new System.EventHandler(this.btnReportF7_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnImport.Image = global::SmuOk.Properties.Resources.open;
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(1448, 531);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(83, 23);
            this.btnImport.TabIndex = 32;
            this.btnImport.Text = "Обновить";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExportCurator
            // 
            this.btnExportCurator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportCurator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCurator.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExportCurator.FlatAppearance.BorderSize = 0;
            this.btnExportCurator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCurator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnExportCurator.Image = global::SmuOk.Properties.Resources.user;
            this.btnExportCurator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportCurator.Location = new System.Drawing.Point(147, 531);
            this.btnExportCurator.Name = "btnExportCurator";
            this.btnExportCurator.Size = new System.Drawing.Size(84, 23);
            this.btnExportCurator.TabIndex = 33;
            this.btnExportCurator.Text = "Кураторы";
            this.btnExportCurator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportCurator.UseVisualStyleBackColor = true;
            this.btnExportCurator.Click += new System.EventHandler(this.btnExportCurator_Click);
            // 
            // btnExportManager
            // 
            this.btnExportManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportManager.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExportManager.FlatAppearance.BorderSize = 0;
            this.btnExportManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportManager.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnExportManager.Image = global::SmuOk.Properties.Resources.user;
            this.btnExportManager.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportManager.Location = new System.Drawing.Point(3, 531);
            this.btnExportManager.Name = "btnExportManager";
            this.btnExportManager.Size = new System.Drawing.Size(138, 23);
            this.btnExportManager.TabIndex = 33;
            this.btnExportManager.Text = "Ответственные АО";
            this.btnExportManager.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportManager.UseVisualStyleBackColor = true;
            this.btnExportManager.Click += new System.EventHandler(this.btnExportManager_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.Green;
            this.btnExport.Image = global::SmuOk.Properties.Resources.report_excel;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(1537, 531);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 23);
            this.btnExport.TabIndex = 33;
            this.btnExport.Text = "Выгрузить";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtFilter1
            // 
            this.txtFilter1.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter1.Location = new System.Drawing.Point(3, 30);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(151, 20);
            this.txtFilter1.TabIndex = 74;
            this.txtFilter1.Tag = "Фильтр 1...";
            this.txtFilter1.Enter += new System.EventHandler(this.txtFilter1_Enter);
            this.txtFilter1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter1_KeyUp);
            this.txtFilter1.Leave += new System.EventHandler(this.txtFilter1_Leave);
            // 
            // txtFilter2
            // 
            this.txtFilter2.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter2.Location = new System.Drawing.Point(3, 56);
            this.txtFilter2.Name = "txtFilter2";
            this.txtFilter2.Size = new System.Drawing.Size(151, 20);
            this.txtFilter2.TabIndex = 75;
            this.txtFilter2.Tag = "Фильтр 2...";
            this.txtFilter2.Enter += new System.EventHandler(this.txtFilter2_Enter);
            this.txtFilter2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter2_KeyUp);
            this.txtFilter2.Leave += new System.EventHandler(this.txtFilter2_Leave);
            // 
            // filter1
            // 
            this.filter1.FormattingEnabled = true;
            this.filter1.Items.AddRange(new object[] {
            "(фильтр 1)",
            "Объект",
            "Исполнитель"});
            this.filter1.Location = new System.Drawing.Point(163, 29);
            this.filter1.Name = "filter1";
            this.filter1.Size = new System.Drawing.Size(150, 21);
            this.filter1.TabIndex = 76;
            // 
            // filter2
            // 
            this.filter2.FormattingEnabled = true;
            this.filter2.Items.AddRange(new object[] {
            "(фильтр 2)",
            "Объект",
            "Исполнитель"});
            this.filter2.Location = new System.Drawing.Point(163, 56);
            this.filter2.Name = "filter2";
            this.filter2.Size = new System.Drawing.Size(150, 21);
            this.filter2.TabIndex = 77;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(163, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 23);
            this.button4.TabIndex = 78;
            this.button4.Text = "Поиск";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dgv_btn_folder
            // 
            this.dgv_btn_folder.HeaderText = "0";
            this.dgv_btn_folder.Image = global::SmuOk.Properties.Resources.shared;
            this.dgv_btn_folder.Name = "dgv_btn_folder";
            this.dgv_btn_folder.ReadOnly = true;
            this.dgv_btn_folder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_btn_folder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgv_btn_folder.Width = 28;
            // 
            // dgv_id_SId
            // 
            this.dgv_id_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_id_SId.DataPropertyName = "SId";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgv_id_SId.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_id_SId.HeaderText = "id";
            this.dgv_id_SId.Name = "dgv_id_SId";
            this.dgv_id_SId.ReadOnly = true;
            this.dgv_id_SId.Width = 40;
            // 
            // dgv_has_filling
            // 
            this.dgv_has_filling.DataPropertyName = "has_filling";
            this.dgv_has_filling.HeaderText = "Есть наполнение";
            this.dgv_has_filling.Name = "dgv_has_filling";
            this.dgv_has_filling.ReadOnly = true;
            // 
            // dgv_SSystem
            // 
            this.dgv_SSystem.DataPropertyName = "SSystem";
            this.dgv_SSystem.HeaderText = "Наименовние работ";
            this.dgv_SSystem.Name = "dgv_SSystem";
            this.dgv_SSystem.ReadOnly = true;
            // 
            // dgv_SStation
            // 
            this.dgv_SStation.DataPropertyName = "SStation";
            this.dgv_SStation.HeaderText = "Станция";
            this.dgv_SStation.Name = "dgv_SStation";
            this.dgv_SStation.ReadOnly = true;
            // 
            // dgv__Curator
            // 
            this.dgv__Curator.DataPropertyName = "curator";
            this.dgv__Curator.HeaderText = "Куратор";
            this.dgv__Curator.Name = "dgv__Curator";
            this.dgv__Curator.ReadOnly = true;
            // 
            // dgv__SContractNum
            // 
            this.dgv__SContractNum.DataPropertyName = "SContractNum";
            this.dgv__SContractNum.HeaderText = "№ договора";
            this.dgv__SContractNum.Name = "dgv__SContractNum";
            this.dgv__SContractNum.ReadOnly = true;
            // 
            // dgv_SVName
            // 
            this.dgv_SVName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SVName.DataPropertyName = "SVName";
            this.dgv_SVName.HeaderText = "Шифр проекта";
            this.dgv_SVName.Name = "dgv_SVName";
            this.dgv_SVName.ReadOnly = true;
            this.dgv_SVName.Width = 96;
            // 
            // dgv_STName
            // 
            this.dgv_STName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_STName.DataPropertyName = "STName";
            this.dgv_STName.HeaderText = "Тип шифра";
            this.dgv_STName.Name = "dgv_STName";
            this.dgv_STName.ReadOnly = true;
            this.dgv_STName.Width = 81;
            // 
            // dgv_SExecutor
            // 
            this.dgv_SExecutor.DataPropertyName = "SExecutor";
            this.dgv_SExecutor.HeaderText = "Исполнитель";
            this.dgv_SExecutor.Name = "dgv_SExecutor";
            this.dgv_SExecutor.ReadOnly = true;
            // 
            // dgv_SArea
            // 
            this.dgv_SArea.DataPropertyName = "SArea";
            this.dgv_SArea.HeaderText = "Участок строительства";
            this.dgv_SArea.Name = "dgv_SArea";
            this.dgv_SArea.ReadOnly = true;
            // 
            // dgv_SNo
            // 
            this.dgv_SNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SNo.DataPropertyName = "SNo";
            this.dgv_SNo.HeaderText = "Этап строительства";
            this.dgv_SNo.Name = "dgv_SNo";
            this.dgv_SNo.ReadOnly = true;
            this.dgv_SNo.Width = 123;
            // 
            // dgv_SVNo
            // 
            this.dgv_SVNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SVNo.DataPropertyName = "SVNo";
            this.dgv_SVNo.HeaderText = "Версия";
            this.dgv_SVNo.Name = "dgv_SVNo";
            this.dgv_SVNo.ReadOnly = true;
            this.dgv_SVNo.Width = 69;
            // 
            // dgv_SVStage
            // 
            this.dgv_SVStage.DataPropertyName = "SVStage";
            this.dgv_SVStage.HeaderText = "Стадия проектной документации";
            this.dgv_SVStage.Name = "dgv_SVStage";
            this.dgv_SVStage.ReadOnly = true;
            // 
            // dgv_SVProjectSignDate
            // 
            this.dgv_SVProjectSignDate.DataPropertyName = "SVProjectSignDate";
            this.dgv_SVProjectSignDate.HeaderText = "Дата подписания версии проектантом";
            this.dgv_SVProjectSignDate.Name = "dgv_SVProjectSignDate";
            this.dgv_SVProjectSignDate.ReadOnly = true;
            // 
            // dgv_SVProjectBy
            // 
            this.dgv_SVProjectBy.DataPropertyName = "SVProjectBy";
            this.dgv_SVProjectBy.HeaderText = "Проектный институт";
            this.dgv_SVProjectBy.Name = "dgv_SVProjectBy";
            this.dgv_SVProjectBy.ReadOnly = true;
            // 
            // dgv_SVDate
            // 
            this.dgv_SVDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SVDate.DataPropertyName = "SVDate";
            this.dgv_SVDate.HeaderText = "Дата поступления версии";
            this.dgv_SVDate.Name = "dgv_SVDate";
            this.dgv_SVDate.ReadOnly = true;
            this.dgv_SVDate.Width = 117;
            // 
            // dgv_SComment
            // 
            this.dgv_SComment.DataPropertyName = "SComment";
            this.dgv_SComment.HeaderText = "Комментарий";
            this.dgv_SComment.Name = "dgv_SComment";
            this.dgv_SComment.ReadOnly = true;
            // 
            // dgv_SObject
            // 
            this.dgv_SObject.DataPropertyName = "SObject";
            this.dgv_SObject.HeaderText = "Объект";
            this.dgv_SObject.Name = "dgv_SObject";
            this.dgv_SObject.ReadOnly = true;
            this.dgv_SObject.Visible = false;
            // 
            // dgv_NewestFillingCount
            // 
            this.dgv_NewestFillingCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_NewestFillingCount.DataPropertyName = "NewestFillingCount";
            this.dgv_NewestFillingCount.FillWeight = 50F;
            this.dgv_NewestFillingCount.HeaderText = "(строк)";
            this.dgv_NewestFillingCount.Name = "dgv_NewestFillingCount";
            this.dgv_NewestFillingCount.ReadOnly = true;
            this.dgv_NewestFillingCount.Visible = false;
            this.dgv_NewestFillingCount.Width = 67;
            // 
            // dgv_SDog
            // 
            this.dgv_SDog.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SDog.DataPropertyName = "SDog";
            this.dgv_SDog.FillWeight = 50F;
            this.dgv_SDog.HeaderText = "Договор";
            this.dgv_SDog.Name = "dgv_SDog";
            this.dgv_SDog.ReadOnly = true;
            this.dgv_SDog.Width = 76;
            // 
            // dgv_SBudget
            // 
            this.dgv_SBudget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SBudget.DataPropertyName = "SBudget";
            this.dgv_SBudget.FillWeight = 50F;
            this.dgv_SBudget.HeaderText = "Смета";
            this.dgv_SBudget.Name = "dgv_SBudget";
            this.dgv_SBudget.ReadOnly = true;
            this.dgv_SBudget.Width = 64;
            // 
            // dgv_SBudgetTotal
            // 
            this.dgv_SBudgetTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_SBudgetTotal.DataPropertyName = "SBudgetTotal";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.dgv_SBudgetTotal.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_SBudgetTotal.FillWeight = 50F;
            this.dgv_SBudgetTotal.HeaderText = "Сумма по смете";
            this.dgv_SBudgetTotal.Name = "dgv_SBudgetTotal";
            this.dgv_SBudgetTotal.ReadOnly = true;
            this.dgv_SBudgetTotal.Width = 72;
            // 
            // dgv_SState
            // 
            this.dgv_SState.DataPropertyName = "SState";
            this.dgv_SState.HeaderText = "Статус";
            this.dgv_SState.Name = "dgv_SState";
            this.dgv_SState.ReadOnly = true;
            this.dgv_SState.Visible = false;
            // 
            // Spec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.filter2);
            this.Controls.Add(this.filter1);
            this.Controls.Add(this.txtFilter2);
            this.Controls.Add(this.txtFilter1);
            this.Controls.Add(this.txtSpecNameFilter);
            this.Controls.Add(this.lstSpecManagerAO);
            this.Controls.Add(this.lstSpecDone);
            this.Controls.Add(this.lstSpecHasFillingFilter);
            this.Controls.Add(this.lblPb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.lstSpecTypeFilter);
            this.Controls.Add(this.btnReportF7);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExportCurator);
            this.Controls.Add(this.btnExportManager);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvSpec);
            this.Name = "Spec";
            this.Size = new System.Drawing.Size(1628, 557);
            this.Load += new System.EventHandler(this.Spec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgvSpec;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.ComboBox lstSpecTypeFilter;
    private System.Windows.Forms.Button btnExportManager;
    private System.Windows.Forms.Label lblPb;
    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.Button btnExportCurator;
    private System.Windows.Forms.ComboBox lstSpecDone;
    private System.Windows.Forms.ComboBox lstSpecHasFillingFilter;
    private System.Windows.Forms.TextBox txtSpecNameFilter;
    private System.Windows.Forms.ComboBox lstSpecManagerAO;
    private System.Windows.Forms.Button btnReportF7;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.ComboBox filter1;
        private System.Windows.Forms.ComboBox filter2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewImageColumn dgv_btn_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_has_filling;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv__Curator;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv__SContractNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SExecutor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVStage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVProjectSignDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVProjectBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_NewestFillingCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SDog;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SBudget;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SBudgetTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SState;
    }
}
