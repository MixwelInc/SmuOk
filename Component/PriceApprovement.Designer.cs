namespace SmuOk.Component
{
    partial class PriceApprovement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
            this.lstSpecManagerAO = new System.Windows.Forms.ComboBox();
            this.lstSpecUserFilter = new System.Windows.Forms.ComboBox();
            this.lstSpecHasFillingFilter = new System.Windows.Forms.ComboBox();
            this.lstSpecTypeFilter = new System.Windows.Forms.ComboBox();
            this.SpecList_ShowFolder = new System.Windows.Forms.CheckBox();
            this.SpecList_ShowManagerAO = new System.Windows.Forms.CheckBox();
            this.SpecList_ShowType = new System.Windows.Forms.CheckBox();
            this.SpecList_ShowID = new System.Windows.Forms.CheckBox();
            this.dgvSpec = new System.Windows.Forms.DataGridView();
            this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SManagerAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_S_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_SState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSpecFill = new System.Windows.Forms.DataGridView();
            this.dgv_SFNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BFCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_NameFromSmth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__ProjQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BFKoeff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_BFPrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ICK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PrcFromSmth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_MinCostReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkDoneType = new System.Windows.Forms.CheckBox();
            this.chkDoneSubcode = new System.Windows.Forms.CheckBox();
            this.SpecInfo = new System.Windows.Forms.TextBox();
            this.chkDoneMultiline = new System.Windows.Forms.CheckBox();
            this.lblPb = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SFBId = new System.Windows.Forms.TextBox();
            this.ExportExcluded = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecFill)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSpecNameFilter
            // 
            this.txtSpecNameFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtSpecNameFilter.Location = new System.Drawing.Point(3, 3);
            this.txtSpecNameFilter.Name = "txtSpecNameFilter";
            this.txtSpecNameFilter.Size = new System.Drawing.Size(151, 20);
            this.txtSpecNameFilter.TabIndex = 34;
            this.txtSpecNameFilter.Tag = "Шифр...";
            this.txtSpecNameFilter.Enter += new System.EventHandler(this.txtSpecNameFilter_Enter);
            this.txtSpecNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSpecNameFilter_KeyUp);
            this.txtSpecNameFilter.Leave += new System.EventHandler(this.txtSpecNameFilter_Leave);
            // 
            // lstSpecManagerAO
            // 
            this.lstSpecManagerAO.FormattingEnabled = true;
            this.lstSpecManagerAO.Location = new System.Drawing.Point(570, 3);
            this.lstSpecManagerAO.Name = "lstSpecManagerAO";
            this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
            this.lstSpecManagerAO.TabIndex = 31;
            this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecUserFilter
            // 
            this.lstSpecUserFilter.FormattingEnabled = true;
            this.lstSpecUserFilter.Location = new System.Drawing.Point(443, 3);
            this.lstSpecUserFilter.Name = "lstSpecUserFilter";
            this.lstSpecUserFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecUserFilter.TabIndex = 33;
            this.lstSpecUserFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(316, 3);
            this.lstSpecHasFillingFilter.Name = "lstSpecHasFillingFilter";
            this.lstSpecHasFillingFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecHasFillingFilter.TabIndex = 30;
            this.lstSpecHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecTypeFilter
            // 
            this.lstSpecTypeFilter.FormattingEnabled = true;
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(160, 3);
            this.lstSpecTypeFilter.Name = "lstSpecTypeFilter";
            this.lstSpecTypeFilter.Size = new System.Drawing.Size(150, 21);
            this.lstSpecTypeFilter.TabIndex = 29;
            this.lstSpecTypeFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // SpecList_ShowFolder
            // 
            this.SpecList_ShowFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpecList_ShowFolder.AutoSize = true;
            this.SpecList_ShowFolder.Checked = true;
            this.SpecList_ShowFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpecList_ShowFolder.Location = new System.Drawing.Point(255, 522);
            this.SpecList_ShowFolder.Name = "SpecList_ShowFolder";
            this.SpecList_ShowFolder.Size = new System.Drawing.Size(58, 17);
            this.SpecList_ShowFolder.TabIndex = 37;
            this.SpecList_ShowFolder.Tag = "dgv_S_btn_folder";
            this.SpecList_ShowFolder.Text = "Папки";
            this.SpecList_ShowFolder.UseVisualStyleBackColor = true;
            // 
            // SpecList_ShowManagerAO
            // 
            this.SpecList_ShowManagerAO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpecList_ShowManagerAO.AutoSize = true;
            this.SpecList_ShowManagerAO.Checked = true;
            this.SpecList_ShowManagerAO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpecList_ShowManagerAO.Location = new System.Drawing.Point(98, 522);
            this.SpecList_ShowManagerAO.Name = "SpecList_ShowManagerAO";
            this.SpecList_ShowManagerAO.Size = new System.Drawing.Size(123, 17);
            this.SpecList_ShowManagerAO.TabIndex = 38;
            this.SpecList_ShowManagerAO.Tag = "dgv_SManagerAO";
            this.SpecList_ShowManagerAO.Text = "Ответственный АО";
            this.SpecList_ShowManagerAO.UseVisualStyleBackColor = true;
            // 
            // SpecList_ShowType
            // 
            this.SpecList_ShowType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpecList_ShowType.AutoSize = true;
            this.SpecList_ShowType.Checked = true;
            this.SpecList_ShowType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpecList_ShowType.Location = new System.Drawing.Point(47, 522);
            this.SpecList_ShowType.Name = "SpecList_ShowType";
            this.SpecList_ShowType.Size = new System.Drawing.Size(45, 17);
            this.SpecList_ShowType.TabIndex = 39;
            this.SpecList_ShowType.Tag = "dgv_STName";
            this.SpecList_ShowType.Text = "Тип";
            this.SpecList_ShowType.UseVisualStyleBackColor = true;
            // 
            // SpecList_ShowID
            // 
            this.SpecList_ShowID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpecList_ShowID.AutoSize = true;
            this.SpecList_ShowID.Checked = true;
            this.SpecList_ShowID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpecList_ShowID.Location = new System.Drawing.Point(4, 522);
            this.SpecList_ShowID.Name = "SpecList_ShowID";
            this.SpecList_ShowID.Size = new System.Drawing.Size(37, 17);
            this.SpecList_ShowID.TabIndex = 40;
            this.SpecList_ShowID.Tag = "dgv_SId";
            this.SpecList_ShowID.Text = "ID";
            this.SpecList_ShowID.UseVisualStyleBackColor = true;
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.AllowUserToResizeRows = false;
            this.dgvSpec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SId,
            this.dgv_STName,
            this.dgv_SVName,
            this.dgv_SManagerAO,
            this.dgv_S_btn_folder,
            this.dgv_SState});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpec.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSpec.Location = new System.Drawing.Point(3, 29);
            this.dgvSpec.MultiSelect = false;
            this.dgvSpec.Name = "dgvSpec";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSpec.RowHeadersVisible = false;
            this.dgvSpec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpec.Size = new System.Drawing.Size(313, 487);
            this.dgvSpec.TabIndex = 36;
            this.dgvSpec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellClick);
            this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            this.dgvSpec.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellValueChanged);
            this.dgvSpec.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvSpec_RowPrePaint);
            // 
            // dgv_SId
            // 
            this.dgv_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SId.DataPropertyName = "SId";
            this.dgv_SId.FillWeight = 32F;
            this.dgv_SId.HeaderText = "Id";
            this.dgv_SId.Name = "dgv_SId";
            this.dgv_SId.Width = 32;
            // 
            // dgv_STName
            // 
            this.dgv_STName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_STName.DataPropertyName = "STName";
            this.dgv_STName.HeaderText = "Тип";
            this.dgv_STName.Name = "dgv_STName";
            this.dgv_STName.Width = 51;
            // 
            // dgv_SVName
            // 
            this.dgv_SVName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_SVName.DataPropertyName = "SVName";
            this.dgv_SVName.HeaderText = "Шифр";
            this.dgv_SVName.Name = "dgv_SVName";
            // 
            // dgv_SManagerAO
            // 
            this.dgv_SManagerAO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_SManagerAO.DataPropertyName = "ManagerAO";
            this.dgv_SManagerAO.FillWeight = 50F;
            this.dgv_SManagerAO.HeaderText = "Отв. АО";
            this.dgv_SManagerAO.Name = "dgv_SManagerAO";
            this.dgv_SManagerAO.Width = 72;
            // 
            // dgv_S_btn_folder
            // 
            this.dgv_S_btn_folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_S_btn_folder.FillWeight = 28F;
            this.dgv_S_btn_folder.HeaderText = "0";
            this.dgv_S_btn_folder.Image = global::SmuOk.Properties.Resources.shared;
            this.dgv_S_btn_folder.Name = "dgv_S_btn_folder";
            this.dgv_S_btn_folder.Width = 28;
            // 
            // dgv_SState
            // 
            this.dgv_SState.DataPropertyName = "SState";
            this.dgv_SState.HeaderText = "Статус";
            this.dgv_SState.Name = "dgv_SState";
            this.dgv_SState.Visible = false;
            // 
            // dgvSpecFill
            // 
            this.dgvSpecFill.AllowUserToAddRows = false;
            this.dgvSpecFill.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvSpecFill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSpecFill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecFill.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSpecFill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecFill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SFNo,
            this.dgv_SFNo2,
            this.dgv_BFCode,
            this.dgv_SFName,
            this.dgv_NameFromSmth,
            this.dgv_Unit,
            this.dgv__ProjQty,
            this.dgv_BFKoeff,
            this.dgv_BFPrc,
            this.dgv_ICK,
            this.dgv_PrcFromSmth,
            this.dgv_MinCostReason});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpecFill.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSpecFill.Location = new System.Drawing.Point(322, 49);
            this.dgvSpecFill.Name = "dgvSpecFill";
            this.dgvSpecFill.RowHeadersVisible = false;
            this.dgvSpecFill.Size = new System.Drawing.Size(1614, 467);
            this.dgvSpecFill.TabIndex = 41;
            // 
            // dgv_SFNo
            // 
            this.dgv_SFNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dgv_SFNo.DataPropertyName = "SFNo";
            this.dgv_SFNo.FillWeight = 60F;
            this.dgv_SFNo.HeaderText = "№ (спц.)";
            this.dgv_SFNo.MinimumWidth = 40;
            this.dgv_SFNo.Name = "dgv_SFNo";
            this.dgv_SFNo.ToolTipText = "(требуется)";
            this.dgv_SFNo.Width = 40;
            // 
            // dgv_SFNo2
            // 
            this.dgv_SFNo2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFNo2.DataPropertyName = "SFNo2";
            this.dgv_SFNo2.FillWeight = 60F;
            this.dgv_SFNo2.HeaderText = "№ (2)";
            this.dgv_SFNo2.MinimumWidth = 40;
            this.dgv_SFNo2.Name = "dgv_SFNo2";
            this.dgv_SFNo2.Width = 40;
            // 
            // dgv_BFCode
            // 
            this.dgv_BFCode.DataPropertyName = "BFCode";
            this.dgv_BFCode.HeaderText = "Код материала по ТСН";
            this.dgv_BFCode.Name = "dgv_BFCode";
            // 
            // dgv_SFName
            // 
            this.dgv_SFName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFName.DataPropertyName = "SFName";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFName.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_SFName.FillWeight = 75F;
            this.dgv_SFName.HeaderText = "Наименование";
            this.dgv_SFName.MinimumWidth = 75;
            this.dgv_SFName.Name = "dgv_SFName";
            this.dgv_SFName.Width = 113;
            // 
            // dgv_NameFromSmth
            // 
            this.dgv_NameFromSmth.DataPropertyName = "NameFromSmth";
            this.dgv_NameFromSmth.HeaderText = "Наименование по счету/УПД";
            this.dgv_NameFromSmth.Name = "dgv_NameFromSmth";
            // 
            // dgv_Unit
            // 
            this.dgv_Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_Unit.DataPropertyName = "Unit";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Unit.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Unit.FillWeight = 35F;
            this.dgv_Unit.HeaderText = "Ед.";
            this.dgv_Unit.MinimumWidth = 35;
            this.dgv_Unit.Name = "dgv_Unit";
            this.dgv_Unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Unit.ToolTipText = "(требуется)";
            this.dgv_Unit.Width = 35;
            // 
            // dgv__ProjQty
            // 
            this.dgv__ProjQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv__ProjQty.DataPropertyName = "ProjQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N";
            this.dgv__ProjQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv__ProjQty.HeaderText = "Кол-во";
            this.dgv__ProjQty.MinimumWidth = 40;
            this.dgv__ProjQty.Name = "dgv__ProjQty";
            this.dgv__ProjQty.ToolTipText = "(требуется)";
            this.dgv__ProjQty.Width = 40;
            // 
            // dgv_BFKoeff
            // 
            this.dgv_BFKoeff.DataPropertyName = "BFKoeff";
            this.dgv_BFKoeff.HeaderText = "Коэффициент от сметы к проекту";
            this.dgv_BFKoeff.Name = "dgv_BFKoeff";
            // 
            // dgv_BFPrc
            // 
            this.dgv_BFPrc.DataPropertyName = "BFPrc";
            this.dgv_BFPrc.HeaderText = "Цена сметная за ед.";
            this.dgv_BFPrc.Name = "dgv_BFPrc";
            // 
            // dgv_ICK
            // 
            this.dgv_ICK.DataPropertyName = "ICK";
            this.dgv_ICK.HeaderText = "К. перевода от закупки к спецификации";
            this.dgv_ICK.Name = "dgv_ICK";
            // 
            // dgv_PrcFromSmth
            // 
            this.dgv_PrcFromSmth.DataPropertyName = "PrcFromSmth";
            this.dgv_PrcFromSmth.HeaderText = "Минимальная стоимость закупки";
            this.dgv_PrcFromSmth.Name = "dgv_PrcFromSmth";
            // 
            // dgv_MinCostReason
            // 
            this.dgv_MinCostReason.DataPropertyName = "MinCostReason";
            this.dgv_MinCostReason.HeaderText = "Обоснование мин. стоимости";
            this.dgv_MinCostReason.Name = "dgv_MinCostReason";
            // 
            // chkDoneType
            // 
            this.chkDoneType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoneType.AutoSize = true;
            this.chkDoneType.Checked = true;
            this.chkDoneType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoneType.Location = new System.Drawing.Point(1891, 30);
            this.chkDoneType.Name = "chkDoneType";
            this.chkDoneType.Size = new System.Drawing.Size(45, 17);
            this.chkDoneType.TabIndex = 43;
            this.chkDoneType.Text = "Вид";
            this.chkDoneType.UseVisualStyleBackColor = true;
            this.chkDoneType.CheckedChanged += new System.EventHandler(this.chkDoneType_CheckedChanged);
            // 
            // chkDoneSubcode
            // 
            this.chkDoneSubcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoneSubcode.AutoSize = true;
            this.chkDoneSubcode.Checked = true;
            this.chkDoneSubcode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoneSubcode.Location = new System.Drawing.Point(1822, 30);
            this.chkDoneSubcode.Name = "chkDoneSubcode";
            this.chkDoneSubcode.Size = new System.Drawing.Size(63, 17);
            this.chkDoneSubcode.TabIndex = 44;
            this.chkDoneSubcode.Text = "шифр-2";
            this.chkDoneSubcode.UseVisualStyleBackColor = true;
            this.chkDoneSubcode.CheckedChanged += new System.EventHandler(this.chkDoneSubcode_CheckedChanged);
            // 
            // SpecInfo
            // 
            this.SpecInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SpecInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SpecInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SpecInfo.Location = new System.Drawing.Point(322, 30);
            this.SpecInfo.Name = "SpecInfo";
            this.SpecInfo.Size = new System.Drawing.Size(478, 13);
            this.SpecInfo.TabIndex = 42;
            this.SpecInfo.Text = "(подробно)";
            // 
            // chkDoneMultiline
            // 
            this.chkDoneMultiline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDoneMultiline.AutoSize = true;
            this.chkDoneMultiline.Checked = true;
            this.chkDoneMultiline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoneMultiline.Location = new System.Drawing.Point(322, 522);
            this.chkDoneMultiline.Name = "chkDoneMultiline";
            this.chkDoneMultiline.Size = new System.Drawing.Size(209, 17);
            this.chkDoneMultiline.TabIndex = 47;
            this.chkDoneMultiline.Text = "мнострочное название и тип/марка";
            this.chkDoneMultiline.UseVisualStyleBackColor = true;
            this.chkDoneMultiline.CheckedChanged += new System.EventHandler(this.chkDoneMultiline_CheckedChanged);
            // 
            // lblPb
            // 
            this.lblPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPb.AutoSize = true;
            this.lblPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPb.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPb.Location = new System.Drawing.Point(1626, 3);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 49;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(1631, 22);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(307, 5);
            this.pb.TabIndex = 48;
            this.pb.Tag = "lblPb";
            this.pb.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Image = global::SmuOk.Properties.Resources.report_excel;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1791, 522);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "Сформировать заявку";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnExportChecked_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button2.Image = global::SmuOk.Properties.Resources.open;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1701, 522);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 51;
            this.button2.Text = "Обновить";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnImportMany_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1089, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 82;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(884, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Удаление записи";
            this.label1.Visible = false;
            // 
            // SFBId
            // 
            this.SFBId.Location = new System.Drawing.Point(983, 4);
            this.SFBId.Name = "SFBId";
            this.SFBId.Size = new System.Drawing.Size(100, 20);
            this.SFBId.TabIndex = 80;
            this.SFBId.Visible = false;
            // 
            // ExportExcluded
            // 
            this.ExportExcluded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportExcluded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExportExcluded.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ExportExcluded.FlatAppearance.BorderSize = 0;
            this.ExportExcluded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportExcluded.ForeColor = System.Drawing.Color.Green;
            this.ExportExcluded.Image = global::SmuOk.Properties.Resources.report_excel;
            this.ExportExcluded.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportExcluded.Location = new System.Drawing.Point(1513, 522);
            this.ExportExcluded.Name = "ExportExcluded";
            this.ExportExcluded.Size = new System.Drawing.Size(160, 23);
            this.ExportExcluded.TabIndex = 83;
            this.ExportExcluded.Text = "Выгрузить исключенные";
            this.ExportExcluded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportExcluded.UseVisualStyleBackColor = true;
            this.ExportExcluded.Visible = false;
            this.ExportExcluded.Click += new System.EventHandler(this.ExportExcluded_Click);
            // 
            // PriceApprovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ExportExcluded);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SFBId);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.chkDoneMultiline);
            this.Controls.Add(this.chkDoneType);
            this.Controls.Add(this.chkDoneSubcode);
            this.Controls.Add(this.SpecInfo);
            this.Controls.Add(this.dgvSpecFill);
            this.Controls.Add(this.SpecList_ShowFolder);
            this.Controls.Add(this.SpecList_ShowManagerAO);
            this.Controls.Add(this.SpecList_ShowType);
            this.Controls.Add(this.SpecList_ShowID);
            this.Controls.Add(this.dgvSpec);
            this.Controls.Add(this.txtSpecNameFilter);
            this.Controls.Add(this.lstSpecManagerAO);
            this.Controls.Add(this.lstSpecUserFilter);
            this.Controls.Add(this.lstSpecHasFillingFilter);
            this.Controls.Add(this.lstSpecTypeFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PriceApprovement";
            this.Size = new System.Drawing.Size(1945, 551);
            this.Load += new System.EventHandler(this.PriceApprovement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecFill)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSpecNameFilter;
        private System.Windows.Forms.ComboBox lstSpecManagerAO;
        private System.Windows.Forms.ComboBox lstSpecUserFilter;
        private System.Windows.Forms.ComboBox lstSpecHasFillingFilter;
        private System.Windows.Forms.ComboBox lstSpecTypeFilter;
        private System.Windows.Forms.CheckBox SpecList_ShowFolder;
        private System.Windows.Forms.CheckBox SpecList_ShowManagerAO;
        private System.Windows.Forms.CheckBox SpecList_ShowType;
        private System.Windows.Forms.CheckBox SpecList_ShowID;
        private System.Windows.Forms.DataGridView dgvSpec;
        private System.Windows.Forms.DataGridView dgvSpecFill;
        private System.Windows.Forms.CheckBox chkDoneType;
        private System.Windows.Forms.CheckBox chkDoneSubcode;
        private System.Windows.Forms.TextBox SpecInfo;
        private System.Windows.Forms.CheckBox chkDoneMultiline;
        private System.Windows.Forms.Label lblPb;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SManagerAO;
        private System.Windows.Forms.DataGridViewImageColumn dgv_S_btn_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SState;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SFBId;
        private System.Windows.Forms.Button ExportExcluded;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BFCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_NameFromSmth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv__ProjQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BFKoeff;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_BFPrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ICK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PrcFromSmth;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_MinCostReason;
    }
}
