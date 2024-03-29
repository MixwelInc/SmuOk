﻿namespace SmuOk.Component
{
    partial class Budg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBudg = new System.Windows.Forms.DataGridView();
            this.dgv_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_BId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_id_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__Curator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SExecutor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BSMRorPNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_NewestFillingCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BMIPRegNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BCostWOVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_rowsFinished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BComm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstSpecTypeFilter = new System.Windows.Forms.ComboBox();
            this.lblPb = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.lstSpecDone = new System.Windows.Forms.ComboBox();
            this.lstSpecHasFillingFilter = new System.Windows.Forms.ComboBox();
            this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
            this.lstSpecManagerAO = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExportCurator = new System.Windows.Forms.Button();
            this.btnExportManager = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.BudgId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.filter2 = new System.Windows.Forms.ComboBox();
            this.filter1 = new System.Windows.Forms.ComboBox();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudg)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBudg
            // 
            this.dgvBudg.AllowUserToAddRows = false;
            this.dgvBudg.AllowUserToDeleteRows = false;
            this.dgvBudg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBudg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgvBudg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBudg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_btn_folder,
            this.dgv_BId,
            this.dgv_id_SId,
            this.dgv_SSystem,
            this.dgv_SStation,
            this.dgv__Curator,
            this.dgv_SVName,
            this.dgv_STName,
            this.dgv_SExecutor,
            this.dgv_SVNo,
            this.dgv_SVStage,
            this.dgv_SComment,
            this.dgv_BSMRorPNR,
            this.dgv_NewestFillingCount,
            this.dgv_BNumber,
            this.dgv_BVer,
            this.dgv_BMIPRegNum,
            this.dgv_BStage,
            this.dgv_BCostWOVAT,
            this.dgv_rowsFinished,
            this.dgv_BComm});
            this.dgvBudg.Location = new System.Drawing.Point(3, 83);
            this.dgvBudg.Name = "dgvBudg";
            this.dgvBudg.ReadOnly = true;
            this.dgvBudg.RowHeadersVisible = false;
            this.dgvBudg.Size = new System.Drawing.Size(1607, 446);
            this.dgvBudg.TabIndex = 1;
            this.dgvBudg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            this.dgvBudg.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellLeave);
            // 
            // dgv_btn_folder
            // 
            this.dgv_btn_folder.HeaderText = "0";
            this.dgv_btn_folder.Image = global::SmuOk.Properties.Resources.shared;
            this.dgv_btn_folder.Name = "dgv_btn_folder";
            this.dgv_btn_folder.ReadOnly = true;
            this.dgv_btn_folder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_btn_folder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgv_btn_folder.Visible = false;
            this.dgv_btn_folder.Width = 28;
            // 
            // dgv_BId
            // 
            this.dgv_BId.DataPropertyName = "BId";
            this.dgv_BId.HeaderText = "id сметы";
            this.dgv_BId.Name = "dgv_BId";
            this.dgv_BId.ReadOnly = true;
            // 
            // dgv_id_SId
            // 
            this.dgv_id_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_id_SId.DataPropertyName = "SId";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgv_id_SId.DefaultCellStyle = dataGridViewCellStyle29;
            this.dgv_id_SId.HeaderText = "id Шифра";
            this.dgv_id_SId.Name = "dgv_id_SId";
            this.dgv_id_SId.ReadOnly = true;
            this.dgv_id_SId.Width = 72;
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
            // dgv_SComment
            // 
            this.dgv_SComment.DataPropertyName = "SComment";
            this.dgv_SComment.HeaderText = "Комментарий";
            this.dgv_SComment.Name = "dgv_SComment";
            this.dgv_SComment.ReadOnly = true;
            // 
            // dgv_BSMRorPNR
            // 
            this.dgv_BSMRorPNR.DataPropertyName = "BSMRorPNR";
            this.dgv_BSMRorPNR.HeaderText = "СМР/ПНР";
            this.dgv_BSMRorPNR.Name = "dgv_BSMRorPNR";
            this.dgv_BSMRorPNR.ReadOnly = true;
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
            // 
            // dgv_BNumber
            // 
            this.dgv_BNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_BNumber.DataPropertyName = "BNumber";
            this.dgv_BNumber.FillWeight = 50F;
            this.dgv_BNumber.HeaderText = "Номер сметы";
            this.dgv_BNumber.Name = "dgv_BNumber";
            this.dgv_BNumber.ReadOnly = true;
            this.dgv_BNumber.Width = 94;
            // 
            // dgv_BVer
            // 
            this.dgv_BVer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_BVer.DataPropertyName = "BVer";
            this.dgv_BVer.FillWeight = 50F;
            this.dgv_BVer.HeaderText = "Изм. по смете";
            this.dgv_BVer.Name = "dgv_BVer";
            this.dgv_BVer.ReadOnly = true;
            this.dgv_BVer.Width = 97;
            // 
            // dgv_BMIPRegNum
            // 
            this.dgv_BMIPRegNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_BMIPRegNum.DataPropertyName = "BMIPRegNum";
            dataGridViewCellStyle30.Format = "N2";
            dataGridViewCellStyle30.NullValue = null;
            this.dgv_BMIPRegNum.DefaultCellStyle = dataGridViewCellStyle30;
            this.dgv_BMIPRegNum.FillWeight = 50F;
            this.dgv_BMIPRegNum.HeaderText = "Рег. Номер сметы от МИП";
            this.dgv_BMIPRegNum.Name = "dgv_BMIPRegNum";
            this.dgv_BMIPRegNum.ReadOnly = true;
            this.dgv_BMIPRegNum.Width = 118;
            // 
            // dgv_BStage
            // 
            this.dgv_BStage.DataPropertyName = "BStage";
            this.dgv_BStage.HeaderText = "Стадия сметы";
            this.dgv_BStage.Name = "dgv_BStage";
            this.dgv_BStage.ReadOnly = true;
            // 
            // dgv_BCostWOVAT
            // 
            this.dgv_BCostWOVAT.DataPropertyName = "BCostWOVAT";
            this.dgv_BCostWOVAT.HeaderText = "Сметная стоимость без НДС";
            this.dgv_BCostWOVAT.Name = "dgv_BCostWOVAT";
            this.dgv_BCostWOVAT.ReadOnly = true;
            // 
            // dgv_rowsFinished
            // 
            this.dgv_rowsFinished.DataPropertyName = "rowsFinished";
            this.dgv_rowsFinished.HeaderText = "Сумма разнесена";
            this.dgv_rowsFinished.Name = "dgv_rowsFinished";
            this.dgv_rowsFinished.ReadOnly = true;
            // 
            // dgv_BComm
            // 
            this.dgv_BComm.DataPropertyName = "BComm";
            this.dgv_BComm.HeaderText = "Комментарий по смете";
            this.dgv_BComm.Name = "dgv_BComm";
            this.dgv_BComm.ReadOnly = true;
            // 
            // lstSpecTypeFilter
            // 
            this.lstSpecTypeFilter.FormattingEnabled = true;
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(322, 3);
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
            this.lblPb.Location = new System.Drawing.Point(1299, 4);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 69;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(1302, 20);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(307, 5);
            this.pb.TabIndex = 68;
            this.pb.Tag = "lblPb";
            this.pb.Visible = false;
            // 
            // lstSpecDone
            // 
            this.lstSpecDone.FormattingEnabled = true;
            this.lstSpecDone.Location = new System.Drawing.Point(605, 3);
            this.lstSpecDone.Name = "lstSpecDone";
            this.lstSpecDone.Size = new System.Drawing.Size(101, 21);
            this.lstSpecDone.TabIndex = 70;
            this.lstSpecDone.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(478, 3);
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
            this.lstSpecManagerAO.Location = new System.Drawing.Point(712, 3);
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
            this.btnImport.Location = new System.Drawing.Point(1433, 531);
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
            this.btnExport.Location = new System.Drawing.Point(1522, 531);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 23);
            this.btnExport.TabIndex = 33;
            this.btnExport.Text = "Выгрузить";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // BudgId
            // 
            this.BudgId.Location = new System.Drawing.Point(971, 4);
            this.BudgId.Name = "BudgId";
            this.BudgId.Size = new System.Drawing.Size(100, 20);
            this.BudgId.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(872, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Удаление сметы";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1077, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 76;
            this.button1.Text = "Удалить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // filter2
            // 
            this.filter2.FormattingEnabled = true;
            this.filter2.Items.AddRange(new object[] {
            "(фильтр 2)",
            "Объект",
            "Исполнитель",
            "Наименование работ"});
            this.filter2.Location = new System.Drawing.Point(163, 56);
            this.filter2.Name = "filter2";
            this.filter2.Size = new System.Drawing.Size(150, 21);
            this.filter2.TabIndex = 81;
            // 
            // filter1
            // 
            this.filter1.FormattingEnabled = true;
            this.filter1.Items.AddRange(new object[] {
            "(фильтр 1)",
            "Объект",
            "Исполнитель",
            "Наименование работ"});
            this.filter1.Location = new System.Drawing.Point(163, 29);
            this.filter1.Name = "filter1";
            this.filter1.Size = new System.Drawing.Size(150, 21);
            this.filter1.TabIndex = 80;
            // 
            // txtFilter2
            // 
            this.txtFilter2.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter2.Location = new System.Drawing.Point(3, 56);
            this.txtFilter2.Name = "txtFilter2";
            this.txtFilter2.Size = new System.Drawing.Size(151, 20);
            this.txtFilter2.TabIndex = 79;
            this.txtFilter2.Tag = "Фильтр 2...";
            this.txtFilter2.Enter += new System.EventHandler(this.txtFilter2_Enter);
            this.txtFilter2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter2_KeyUp);
            this.txtFilter2.Leave += new System.EventHandler(this.txtFilter2_Leave);
            // 
            // txtFilter1
            // 
            this.txtFilter1.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter1.Location = new System.Drawing.Point(3, 30);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(151, 20);
            this.txtFilter1.TabIndex = 78;
            this.txtFilter1.Tag = "Фильтр 1...";
            this.txtFilter1.Enter += new System.EventHandler(this.txtFilter1_Enter);
            this.txtFilter1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter1_KeyUp);
            this.txtFilter1.Leave += new System.EventHandler(this.txtFilter1_Leave);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(163, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 23);
            this.button4.TabIndex = 82;
            this.button4.Text = "Поиск";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Budg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.filter2);
            this.Controls.Add(this.filter1);
            this.Controls.Add(this.txtFilter2);
            this.Controls.Add(this.txtFilter1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BudgId);
            this.Controls.Add(this.txtSpecNameFilter);
            this.Controls.Add(this.lstSpecManagerAO);
            this.Controls.Add(this.lstSpecDone);
            this.Controls.Add(this.lstSpecHasFillingFilter);
            this.Controls.Add(this.lblPb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.lstSpecTypeFilter);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExportCurator);
            this.Controls.Add(this.btnExportManager);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvBudg);
            this.Name = "Budg";
            this.Size = new System.Drawing.Size(1613, 557);
            this.Load += new System.EventHandler(this.Budg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBudg;
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
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.TextBox BudgId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewImageColumn dgv_btn_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv__Curator;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SExecutor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVStage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BSMRorPNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_NewestFillingCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BMIPRegNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BStage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BCostWOVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_rowsFinished;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BComm;
        private System.Windows.Forms.ComboBox filter2;
        private System.Windows.Forms.ComboBox filter1;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.Button button4;
    }
}
