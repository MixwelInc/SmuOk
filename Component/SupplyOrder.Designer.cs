namespace SmuOk.Component
{
  partial class SupplyOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SManagerAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_S_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.chkDoneMultiline = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvSpecFill = new System.Windows.Forms.DataGridView();
            this.dgv_id_SOId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__SFId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOOrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFSubcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_QtyBuy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFExecutor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOSupplierType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOOrderDocId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOResponsOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFEONum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TotalOrdered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFEOStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFEOQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOPlan1CNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SO1CPlanDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SOComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecInfo = new System.Windows.Forms.TextBox();
            this.chkDoneType = new System.Windows.Forms.CheckBox();
            this.chkDoneSubcode = new System.Windows.Forms.CheckBox();
            this.lblPb = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.filter1 = new System.Windows.Forms.ComboBox();
            this.filter2 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecFill)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSpecNameFilter
            // 
            this.txtSpecNameFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtSpecNameFilter.Location = new System.Drawing.Point(3, 3);
            this.txtSpecNameFilter.Margin = new System.Windows.Forms.Padding(0);
            this.txtSpecNameFilter.Name = "txtSpecNameFilter";
            this.txtSpecNameFilter.Size = new System.Drawing.Size(151, 20);
            this.txtSpecNameFilter.TabIndex = 33;
            this.txtSpecNameFilter.Tag = "Шифр...";
            this.txtSpecNameFilter.Enter += new System.EventHandler(this.txtSpecNameFilter_Enter);
            this.txtSpecNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSpecNameFilter_KeyUp);
            this.txtSpecNameFilter.Leave += new System.EventHandler(this.txtSpecNameFilter_Leave);
            // 
            // lstSpecManagerAO
            // 
            this.lstSpecManagerAO.FormattingEnabled = true;
            this.lstSpecManagerAO.Location = new System.Drawing.Point(735, 3);
            this.lstSpecManagerAO.Name = "lstSpecManagerAO";
            this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
            this.lstSpecManagerAO.TabIndex = 31;
            this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecUserFilter
            // 
            this.lstSpecUserFilter.FormattingEnabled = true;
            this.lstSpecUserFilter.Location = new System.Drawing.Point(608, 3);
            this.lstSpecUserFilter.Name = "lstSpecUserFilter";
            this.lstSpecUserFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecUserFilter.TabIndex = 32;
            this.lstSpecUserFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(481, 3);
            this.lstSpecHasFillingFilter.Name = "lstSpecHasFillingFilter";
            this.lstSpecHasFillingFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecHasFillingFilter.TabIndex = 30;
            this.lstSpecHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecTypeFilter
            // 
            this.lstSpecTypeFilter.FormattingEnabled = true;
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(325, 3);
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
            this.SpecList_ShowFolder.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.SpecList_ShowManagerAO.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.SpecList_ShowType.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.SpecList_ShowID.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.AllowUserToResizeRows = false;
            this.dgvSpec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox,
            this.dgv_SId,
            this.dgv_STName,
            this.dgv_SVName,
            this.dgv_SManagerAO,
            this.dgv_S_btn_folder});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpec.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvSpec.Location = new System.Drawing.Point(3, 91);
            this.dgvSpec.MultiSelect = false;
            this.dgvSpec.Name = "dgvSpec";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvSpec.RowHeadersVisible = false;
            this.dgvSpec.RowHeadersWidth = 51;
            this.dgvSpec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpec.Size = new System.Drawing.Size(313, 425);
            this.dgvSpec.TabIndex = 36;
            this.dgvSpec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellClick);
            this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            this.dgvSpec.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellValueChanged);
            // 
            // CheckBox
            // 
            this.CheckBox.DataPropertyName = "CheckBox";
            this.CheckBox.FalseValue = "false";
            this.CheckBox.HeaderText = "Выгрузить";
            this.CheckBox.IndeterminateValue = "false";
            this.CheckBox.MinimumWidth = 6;
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.TrueValue = "true";
            this.CheckBox.Width = 125;
            // 
            // dgv_SId
            // 
            this.dgv_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SId.DataPropertyName = "SId";
            this.dgv_SId.FillWeight = 32F;
            this.dgv_SId.HeaderText = "Id";
            this.dgv_SId.MinimumWidth = 6;
            this.dgv_SId.Name = "dgv_SId";
            this.dgv_SId.Width = 32;
            // 
            // dgv_STName
            // 
            this.dgv_STName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_STName.DataPropertyName = "STName";
            this.dgv_STName.HeaderText = "Тип";
            this.dgv_STName.MinimumWidth = 6;
            this.dgv_STName.Name = "dgv_STName";
            this.dgv_STName.ReadOnly = true;
            this.dgv_STName.Width = 51;
            // 
            // dgv_SVName
            // 
            this.dgv_SVName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_SVName.DataPropertyName = "SVName";
            this.dgv_SVName.HeaderText = "Шифр";
            this.dgv_SVName.MinimumWidth = 6;
            this.dgv_SVName.Name = "dgv_SVName";
            this.dgv_SVName.ReadOnly = true;
            // 
            // dgv_SManagerAO
            // 
            this.dgv_SManagerAO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_SManagerAO.DataPropertyName = "ManagerAO";
            this.dgv_SManagerAO.FillWeight = 50F;
            this.dgv_SManagerAO.HeaderText = "Отв. АО";
            this.dgv_SManagerAO.MinimumWidth = 6;
            this.dgv_SManagerAO.Name = "dgv_SManagerAO";
            this.dgv_SManagerAO.ReadOnly = true;
            this.dgv_SManagerAO.Width = 72;
            // 
            // dgv_S_btn_folder
            // 
            this.dgv_S_btn_folder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_S_btn_folder.FillWeight = 28F;
            this.dgv_S_btn_folder.HeaderText = "0";
            this.dgv_S_btn_folder.Image = global::SmuOk.Properties.Resources.shared;
            this.dgv_S_btn_folder.MinimumWidth = 6;
            this.dgv_S_btn_folder.Name = "dgv_S_btn_folder";
            this.dgv_S_btn_folder.ReadOnly = true;
            this.dgv_S_btn_folder.Width = 28;
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
            this.chkDoneMultiline.TabIndex = 45;
            this.chkDoneMultiline.Text = "мнострочное название и тип/марка";
            this.chkDoneMultiline.UseVisualStyleBackColor = true;
            this.chkDoneMultiline.CheckedChanged += new System.EventHandler(this.chkDoneMultiline_CheckedChanged);
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
            this.btnImport.Location = new System.Drawing.Point(1099, 526);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(83, 23);
            this.btnImport.TabIndex = 43;
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
            this.btnExport.Location = new System.Drawing.Point(1188, 526);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 23);
            this.btnExport.TabIndex = 44;
            this.btnExport.Text = "Выгрузить";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvSpecFill
            // 
            this.dgvSpecFill.AllowUserToAddRows = false;
            this.dgvSpecFill.AllowUserToDeleteRows = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            this.dgvSpecFill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvSpecFill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecFill.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSpecFill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecFill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_id_SOId,
            this.dgv__SFId,
            this.dgv_SOOrderId,
            this.dgv_SFSubcode,
            this.dgv_SFType,
            this.dgv_SFNo,
            this.dgv_SFNo2,
            this.dgv_SFName,
            this.dgv_SFMark,
            this.dgv_SFUnit,
            this.dgv_QtyBuy,
            this.dgv_SFExecutor,
            this.dgv_PID,
            this.dgv_SOSupplierType,
            this.dgv_SOOrderDocId,
            this.dgv_SOResponsOS,
            this.dgv_SFEONum,
            this.dgv_SOOrderDate,
            this.dgv_TotalOrdered,
            this.dgv_SFEOStartDate,
            this.dgv_SFEOQty,
            this.dgv_SOPlan1CNum,
            this.dgv_SO1CPlanDate,
            this.dgv_SOComment});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpecFill.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvSpecFill.Location = new System.Drawing.Point(325, 54);
            this.dgvSpecFill.Name = "dgvSpecFill";
            this.dgvSpecFill.RowHeadersVisible = false;
            this.dgvSpecFill.RowHeadersWidth = 51;
            this.dgvSpecFill.Size = new System.Drawing.Size(954, 466);
            this.dgvSpecFill.TabIndex = 42;
            this.dgvSpecFill.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvSpecFill_ColumnWidthChanged);
            // 
            // dgv_id_SOId
            // 
            this.dgv_id_SOId.DataPropertyName = "SOId";
            this.dgv_id_SOId.FillWeight = 20F;
            this.dgv_id_SOId.HeaderText = "id";
            this.dgv_id_SOId.MinimumWidth = 6;
            this.dgv_id_SOId.Name = "dgv_id_SOId";
            this.dgv_id_SOId.Visible = false;
            this.dgv_id_SOId.Width = 20;
            // 
            // dgv__SFId
            // 
            this.dgv__SFId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv__SFId.DataPropertyName = "SFId";
            this.dgv__SFId.FillWeight = 20F;
            this.dgv__SFId.HeaderText = "SFId";
            this.dgv__SFId.MinimumWidth = 25;
            this.dgv__SFId.Name = "dgv__SFId";
            this.dgv__SFId.Visible = false;
            // 
            // dgv_SOOrderId
            // 
            this.dgv_SOOrderId.DataPropertyName = "SOOrderId";
            this.dgv_SOOrderId.HeaderText = "ID позиции заявки";
            this.dgv_SOOrderId.Name = "dgv_SOOrderId";
            // 
            // dgv_SFSubcode
            // 
            this.dgv_SFSubcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFSubcode.DataPropertyName = "SFSubcode";
            this.dgv_SFSubcode.HeaderText = "Шифр (2)";
            this.dgv_SFSubcode.MinimumWidth = 6;
            this.dgv_SFSubcode.Name = "dgv_SFSubcode";
            this.dgv_SFSubcode.Width = 125;
            // 
            // dgv_SFType
            // 
            this.dgv_SFType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFType.DataPropertyName = "SFType";
            this.dgv_SFType.FillWeight = 50F;
            this.dgv_SFType.HeaderText = "Вид";
            this.dgv_SFType.MinimumWidth = 25;
            this.dgv_SFType.Name = "dgv_SFType";
            this.dgv_SFType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFType.ToolTipText = "(требуется)";
            this.dgv_SFType.Width = 51;
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
            // dgv_SFName
            // 
            this.dgv_SFName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFName.DataPropertyName = "SFName";
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFName.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_SFName.FillWeight = 75F;
            this.dgv_SFName.HeaderText = "Наименование";
            this.dgv_SFName.MinimumWidth = 75;
            this.dgv_SFName.Name = "dgv_SFName";
            this.dgv_SFName.Width = 113;
            // 
            // dgv_SFMark
            // 
            this.dgv_SFMark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFMark.DataPropertyName = "SFMark";
            this.dgv_SFMark.FillWeight = 60F;
            this.dgv_SFMark.HeaderText = "Тип / Марка / Обозн.";
            this.dgv_SFMark.MinimumWidth = 60;
            this.dgv_SFMark.Name = "dgv_SFMark";
            this.dgv_SFMark.Width = 60;
            // 
            // dgv_SFUnit
            // 
            this.dgv_SFUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFUnit.DataPropertyName = "SFUnit";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_SFUnit.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgv_SFUnit.FillWeight = 35F;
            this.dgv_SFUnit.HeaderText = "Ед.";
            this.dgv_SFUnit.MinimumWidth = 35;
            this.dgv_SFUnit.Name = "dgv_SFUnit";
            this.dgv_SFUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFUnit.ToolTipText = "(требуется)";
            this.dgv_SFUnit.Width = 35;
            // 
            // dgv_QtyBuy
            // 
            this.dgv_QtyBuy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_QtyBuy.DataPropertyName = "QtyBuy";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N";
            this.dgv_QtyBuy.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgv_QtyBuy.HeaderText = "Кол-во";
            this.dgv_QtyBuy.MinimumWidth = 40;
            this.dgv_QtyBuy.Name = "dgv_QtyBuy";
            this.dgv_QtyBuy.ToolTipText = "(требуется)";
            this.dgv_QtyBuy.Width = 40;
            // 
            // dgv_SFExecutor
            // 
            this.dgv_SFExecutor.DataPropertyName = "SExecutor";
            this.dgv_SFExecutor.HeaderText = "Исполнитель";
            this.dgv_SFExecutor.MinimumWidth = 6;
            this.dgv_SFExecutor.Name = "dgv_SFExecutor";
            this.dgv_SFExecutor.Width = 125;
            // 
            // dgv_PID
            // 
            this.dgv_PID.DataPropertyName = "PID";
            this.dgv_PID.HeaderText = "PID";
            this.dgv_PID.MinimumWidth = 6;
            this.dgv_PID.Name = "dgv_PID";
            this.dgv_PID.Width = 125;
            // 
            // dgv_SOSupplierType
            // 
            this.dgv_SOSupplierType.DataPropertyName = "SOSupplierType";
            this.dgv_SOSupplierType.HeaderText = "Чья поставка";
            this.dgv_SOSupplierType.MinimumWidth = 6;
            this.dgv_SOSupplierType.Name = "dgv_SOSupplierType";
            this.dgv_SOSupplierType.Width = 125;
            // 
            // dgv_SOOrderDocId
            // 
            this.dgv_SOOrderDocId.DataPropertyName = "SOOrderDocId";
            this.dgv_SOOrderDocId.HeaderText = "ID заявки";
            this.dgv_SOOrderDocId.MinimumWidth = 6;
            this.dgv_SOOrderDocId.Name = "dgv_SOOrderDocId";
            this.dgv_SOOrderDocId.Width = 125;
            // 
            // dgv_SOResponsOS
            // 
            this.dgv_SOResponsOS.DataPropertyName = "SOResponsOS";
            this.dgv_SOResponsOS.HeaderText = "Ответственный ОС";
            this.dgv_SOResponsOS.MinimumWidth = 6;
            this.dgv_SOResponsOS.Name = "dgv_SOResponsOS";
            this.dgv_SOResponsOS.Width = 125;
            // 
            // dgv_SFEONum
            // 
            this.dgv_SFEONum.DataPropertyName = "SFEONum";
            this.dgv_SFEONum.HeaderText = "№ заявки от участка/субчика";
            this.dgv_SFEONum.MinimumWidth = 6;
            this.dgv_SFEONum.Name = "dgv_SFEONum";
            this.dgv_SFEONum.Width = 125;
            // 
            // dgv_SOOrderDate
            // 
            this.dgv_SOOrderDate.DataPropertyName = "SOOrderDate";
            this.dgv_SOOrderDate.HeaderText = "Дата заявки";
            this.dgv_SOOrderDate.MinimumWidth = 6;
            this.dgv_SOOrderDate.Name = "dgv_SOOrderDate";
            this.dgv_SOOrderDate.Width = 125;
            // 
            // dgv_TotalOrdered
            // 
            this.dgv_TotalOrdered.DataPropertyName = "TotalOrdered";
            this.dgv_TotalOrdered.HeaderText = "Всего заказано";
            this.dgv_TotalOrdered.Name = "dgv_TotalOrdered";
            // 
            // dgv_SFEOStartDate
            // 
            this.dgv_SFEOStartDate.DataPropertyName = "SFEOStartDate";
            this.dgv_SFEOStartDate.HeaderText = "Желаемая дата поставки";
            this.dgv_SFEOStartDate.MinimumWidth = 6;
            this.dgv_SFEOStartDate.Name = "dgv_SFEOStartDate";
            this.dgv_SFEOStartDate.Width = 125;
            // 
            // dgv_SFEOQty
            // 
            this.dgv_SFEOQty.DataPropertyName = "SFEOQty";
            this.dgv_SFEOQty.HeaderText = "Кол-во заказано";
            this.dgv_SFEOQty.MinimumWidth = 6;
            this.dgv_SFEOQty.Name = "dgv_SFEOQty";
            this.dgv_SFEOQty.Width = 125;
            // 
            // dgv_SOPlan1CNum
            // 
            this.dgv_SOPlan1CNum.DataPropertyName = "SOPlan1CNum";
            this.dgv_SOPlan1CNum.HeaderText = "№ планирования 1С / письма в МИП";
            this.dgv_SOPlan1CNum.MinimumWidth = 6;
            this.dgv_SOPlan1CNum.Name = "dgv_SOPlan1CNum";
            this.dgv_SOPlan1CNum.Width = 125;
            // 
            // dgv_SO1CPlanDate
            // 
            this.dgv_SO1CPlanDate.DataPropertyName = "SO1CPlanDate";
            this.dgv_SO1CPlanDate.HeaderText = "дата планирования 1С / письма в МИП";
            this.dgv_SO1CPlanDate.MinimumWidth = 6;
            this.dgv_SO1CPlanDate.Name = "dgv_SO1CPlanDate";
            this.dgv_SO1CPlanDate.Width = 125;
            // 
            // dgv_SOComment
            // 
            this.dgv_SOComment.DataPropertyName = "SOComment";
            this.dgv_SOComment.HeaderText = "Комментарий";
            this.dgv_SOComment.MinimumWidth = 6;
            this.dgv_SOComment.Name = "dgv_SOComment";
            this.dgv_SOComment.Width = 125;
            // 
            // SpecInfo
            // 
            this.SpecInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SpecInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SpecInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SpecInfo.Location = new System.Drawing.Point(322, 30);
            this.SpecInfo.Name = "SpecInfo";
            this.SpecInfo.Size = new System.Drawing.Size(478, 13);
            this.SpecInfo.TabIndex = 41;
            this.SpecInfo.Text = "(подробно)";
            // 
            // chkDoneType
            // 
            this.chkDoneType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoneType.AutoSize = true;
            this.chkDoneType.Checked = true;
            this.chkDoneType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoneType.Location = new System.Drawing.Point(1230, 30);
            this.chkDoneType.Name = "chkDoneType";
            this.chkDoneType.Size = new System.Drawing.Size(45, 17);
            this.chkDoneType.TabIndex = 46;
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
            this.chkDoneSubcode.Location = new System.Drawing.Point(1162, 30);
            this.chkDoneSubcode.Name = "chkDoneSubcode";
            this.chkDoneSubcode.Size = new System.Drawing.Size(63, 17);
            this.chkDoneSubcode.TabIndex = 47;
            this.chkDoneSubcode.Text = "шифр-2";
            this.chkDoneSubcode.UseVisualStyleBackColor = true;
            this.chkDoneSubcode.CheckedChanged += new System.EventHandler(this.chkDoneSubcode_CheckedChanged);
            // 
            // lblPb
            // 
            this.lblPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPb.AutoSize = true;
            this.lblPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPb.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPb.Location = new System.Drawing.Point(964, 0);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 49;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(969, 19);
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
            this.button1.ForeColor = System.Drawing.Color.Gray;
            this.button1.Image = global::SmuOk.Properties.Resources.report_excel;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(947, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 21);
            this.button1.TabIndex = 50;
            this.button1.Text = "Выгрузить отмеченные";
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
            this.button2.ForeColor = System.Drawing.Color.Gray;
            this.button2.Image = global::SmuOk.Properties.Resources.open;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(800, 525);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 51;
            this.button2.Text = "Обновить отмеченные";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnImportMany_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Green;
            this.button3.Image = global::SmuOk.Properties.Resources.report_excel;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(703, 526);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 21);
            this.button3.TabIndex = 52;
            this.button3.Text = "Заявка в 1С";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Export1SBtn_Click);
            // 
            // txtFilter1
            // 
            this.txtFilter1.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter1.Location = new System.Drawing.Point(3, 28);
            this.txtFilter1.Margin = new System.Windows.Forms.Padding(0);
            this.txtFilter1.Name = "txtFilter1";
            this.txtFilter1.Size = new System.Drawing.Size(151, 20);
            this.txtFilter1.TabIndex = 53;
            this.txtFilter1.Tag = "Фильтр 1...";
            this.txtFilter1.Enter += new System.EventHandler(this.txtFilter1_Enter);
            this.txtFilter1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter1_KeyUp);
            this.txtFilter1.Leave += new System.EventHandler(this.txtFilter1_Leave);
            // 
            // txtFilter2
            // 
            this.txtFilter2.ForeColor = System.Drawing.Color.Gray;
            this.txtFilter2.Location = new System.Drawing.Point(3, 55);
            this.txtFilter2.Margin = new System.Windows.Forms.Padding(0);
            this.txtFilter2.Name = "txtFilter2";
            this.txtFilter2.Size = new System.Drawing.Size(151, 20);
            this.txtFilter2.TabIndex = 54;
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
            "Ответственный ОС",
            "№ планирования 1С / письма в ТСК"});
            this.filter1.Location = new System.Drawing.Point(157, 28);
            this.filter1.Name = "filter1";
            this.filter1.Size = new System.Drawing.Size(150, 21);
            this.filter1.TabIndex = 55;
            // 
            // filter2
            // 
            this.filter2.FormattingEnabled = true;
            this.filter2.Items.AddRange(new object[] {
            "(фильтр 2)",
            "Ответственный ОС",
            "№ планирования 1С / письма в ТСК"});
            this.filter2.Location = new System.Drawing.Point(157, 54);
            this.filter2.Name = "filter2";
            this.filter2.Size = new System.Drawing.Size(150, 21);
            this.filter2.TabIndex = 56;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(157, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 23);
            this.button4.TabIndex = 57;
            this.button4.Text = "Поиск";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // SupplyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.filter2);
            this.Controls.Add(this.filter1);
            this.Controls.Add(this.txtFilter2);
            this.Controls.Add(this.txtFilter1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.chkDoneType);
            this.Controls.Add(this.chkDoneSubcode);
            this.Controls.Add(this.chkDoneMultiline);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvSpecFill);
            this.Controls.Add(this.SpecInfo);
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
            this.Name = "SupplyOrder";
            this.Size = new System.Drawing.Size(1279, 551);
            this.Load += new System.EventHandler(this.SupplyOrder_Load);
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
    private System.Windows.Forms.CheckBox chkDoneMultiline;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.DataGridView dgvSpecFill;
    private System.Windows.Forms.TextBox SpecInfo;
    private System.Windows.Forms.CheckBox chkDoneType;
    private System.Windows.Forms.CheckBox chkDoneSubcode;
    private System.Windows.Forms.Label lblPb;
    private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SManagerAO;
        private System.Windows.Forms.DataGridViewImageColumn dgv_S_btn_folder;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.ComboBox filter1;
        private System.Windows.Forms.ComboBox filter2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id_SOId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv__SFId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOOrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFSubcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_QtyBuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFExecutor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOSupplierType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOOrderDocId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOResponsOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFEONum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TotalOrdered;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFEOStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFEOQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOPlan1CNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SO1CPlanDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SOComment;
    }
}
