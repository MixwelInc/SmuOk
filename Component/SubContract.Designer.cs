namespace SmuOk.Component
{
    partial class SubContract
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBudg = new System.Windows.Forms.DataGridView();
            this.dgv_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.btnExport = new System.Windows.Forms.Button();
            this.BudgId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBudg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBudg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBudg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_btn_folder,
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
            this.dgv_BComm});
            this.dgvBudg.Location = new System.Drawing.Point(3, 30);
            this.dgvBudg.Name = "dgvBudg";
            this.dgvBudg.ReadOnly = true;
            this.dgvBudg.RowHeadersVisible = false;
            this.dgvBudg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBudg.Size = new System.Drawing.Size(1622, 499);
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
            // dgv_id_SId
            // 
            this.dgv_id_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_id_SId.DataPropertyName = "SId";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgv_id_SId.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_id_SId.HeaderText = "id";
            this.dgv_id_SId.Name = "dgv_id_SId";
            this.dgv_id_SId.ReadOnly = true;
            this.dgv_id_SId.Width = 40;
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
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.dgv_BMIPRegNum.DefaultCellStyle = dataGridViewCellStyle6;
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
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(163, 4);
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
            this.lstSpecDone.Location = new System.Drawing.Point(446, 4);
            this.lstSpecDone.Name = "lstSpecDone";
            this.lstSpecDone.Size = new System.Drawing.Size(101, 21);
            this.lstSpecDone.TabIndex = 70;
            this.lstSpecDone.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(319, 4);
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
            this.lstSpecManagerAO.Location = new System.Drawing.Point(553, 4);
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
            this.btnImport.Location = new System.Drawing.Point(1448, 531);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(83, 23);
            this.btnImport.TabIndex = 32;
            this.btnImport.Text = "Обновить";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
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
            this.label1.Location = new System.Drawing.Point(858, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Удаление договора";
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
            // SubContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvBudg);
            this.Name = "SubContract";
            this.Size = new System.Drawing.Size(1628, 557);
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
        private System.Windows.Forms.Label lblPb;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.ComboBox lstSpecDone;
        private System.Windows.Forms.ComboBox lstSpecHasFillingFilter;
        private System.Windows.Forms.TextBox txtSpecNameFilter;
        private System.Windows.Forms.ComboBox lstSpecManagerAO;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dgv_btn_folder;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BComm;
        private System.Windows.Forms.TextBox BudgId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}
