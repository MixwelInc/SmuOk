namespace SmuOk.Component
{
  partial class M15
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
            this.chkDoneMultiline = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvSpecFill = new System.Windows.Forms.DataGridView();
            this.dgv_id_M15Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgv_PID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_AFNNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_AFNDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ABKNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_AFNRowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_AFNQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Reciever = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_LandingPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15RowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecInfo = new System.Windows.Forms.TextBox();
            this.chkDoneType = new System.Windows.Forms.CheckBox();
            this.chkDoneSubcode = new System.Windows.Forms.CheckBox();
            this.lblPb = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.txtFilter1 = new System.Windows.Forms.TextBox();
            this.txtFilter2 = new System.Windows.Forms.TextBox();
            this.filter1 = new System.Windows.Forms.ComboBox();
            this.filter2 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.M15Id = new System.Windows.Forms.TextBox();
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
            this.dgv_S_btn_folder});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpec.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSpec.Location = new System.Drawing.Point(3, 91);
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
            this.dgvSpec.RowHeadersWidth = 51;
            this.dgvSpec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpec.Size = new System.Drawing.Size(313, 425);
            this.dgvSpec.TabIndex = 36;
            this.dgvSpec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellClick);
            this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            this.dgvSpec.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellValueChanged);
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
            this.btnImport.Location = new System.Drawing.Point(1427, 526);
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
            this.btnExport.Location = new System.Drawing.Point(1516, 526);
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
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvSpecFill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSpecFill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecFill.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSpecFill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecFill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_id_M15Id,
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
            this.dgv_PID2,
            this.dgv_AFNNum,
            this.dgv_AFNDate,
            this.dgv_ABKNum,
            this.dgv_AFNRowNo,
            this.dgv_AFNQty,
            this.dgv_Reciever,
            this.dgv_LandingPlace,
            this.dgv_M15Num,
            this.dgv_M15Date,
            this.dgv_M15RowNo,
            this.dgv_M15Qty});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpecFill.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSpecFill.Location = new System.Drawing.Point(325, 54);
            this.dgvSpecFill.Name = "dgvSpecFill";
            this.dgvSpecFill.ReadOnly = true;
            this.dgvSpecFill.RowHeadersVisible = false;
            this.dgvSpecFill.RowHeadersWidth = 51;
            this.dgvSpecFill.Size = new System.Drawing.Size(1282, 466);
            this.dgvSpecFill.TabIndex = 42;
            this.dgvSpecFill.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvSpecFill_ColumnWidthChanged);
            // 
            // dgv_id_M15Id
            // 
            this.dgv_id_M15Id.DataPropertyName = "M15Id";
            this.dgv_id_M15Id.FillWeight = 20F;
            this.dgv_id_M15Id.HeaderText = "id";
            this.dgv_id_M15Id.MinimumWidth = 6;
            this.dgv_id_M15Id.Name = "dgv_id_M15Id";
            this.dgv_id_M15Id.ReadOnly = true;
            this.dgv_id_M15Id.Width = 20;
            // 
            // dgv__SFId
            // 
            this.dgv__SFId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv__SFId.DataPropertyName = "SFId";
            this.dgv__SFId.FillWeight = 20F;
            this.dgv__SFId.HeaderText = "SFId";
            this.dgv__SFId.MinimumWidth = 25;
            this.dgv__SFId.Name = "dgv__SFId";
            this.dgv__SFId.ReadOnly = true;
            this.dgv__SFId.Visible = false;
            // 
            // dgv_SOOrderId
            // 
            this.dgv_SOOrderId.DataPropertyName = "SOOrderId";
            this.dgv_SOOrderId.HeaderText = "ID позиции заявки";
            this.dgv_SOOrderId.Name = "dgv_SOOrderId";
            this.dgv_SOOrderId.ReadOnly = true;
            // 
            // dgv_SFSubcode
            // 
            this.dgv_SFSubcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFSubcode.DataPropertyName = "SFSubcode";
            this.dgv_SFSubcode.HeaderText = "Шифр (2)";
            this.dgv_SFSubcode.MinimumWidth = 6;
            this.dgv_SFSubcode.Name = "dgv_SFSubcode";
            this.dgv_SFSubcode.ReadOnly = true;
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
            this.dgv_SFType.ReadOnly = true;
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
            this.dgv_SFNo.ReadOnly = true;
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
            this.dgv_SFNo2.ReadOnly = true;
            this.dgv_SFNo2.Width = 40;
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
            this.dgv_SFName.ReadOnly = true;
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
            this.dgv_SFMark.ReadOnly = true;
            this.dgv_SFMark.Width = 60;
            // 
            // dgv_SFUnit
            // 
            this.dgv_SFUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFUnit.DataPropertyName = "SFUnit";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_SFUnit.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_SFUnit.FillWeight = 35F;
            this.dgv_SFUnit.HeaderText = "Ед.";
            this.dgv_SFUnit.MinimumWidth = 35;
            this.dgv_SFUnit.Name = "dgv_SFUnit";
            this.dgv_SFUnit.ReadOnly = true;
            this.dgv_SFUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFUnit.ToolTipText = "(требуется)";
            this.dgv_SFUnit.Width = 35;
            // 
            // dgv_QtyBuy
            // 
            this.dgv_QtyBuy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_QtyBuy.DataPropertyName = "QtyBuy";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N";
            this.dgv_QtyBuy.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_QtyBuy.HeaderText = "Кол-во";
            this.dgv_QtyBuy.MinimumWidth = 40;
            this.dgv_QtyBuy.Name = "dgv_QtyBuy";
            this.dgv_QtyBuy.ReadOnly = true;
            this.dgv_QtyBuy.ToolTipText = "(требуется)";
            this.dgv_QtyBuy.Width = 40;
            // 
            // dgv_SFExecutor
            // 
            this.dgv_SFExecutor.DataPropertyName = "SExecutor";
            this.dgv_SFExecutor.HeaderText = "Исполнитель";
            this.dgv_SFExecutor.MinimumWidth = 6;
            this.dgv_SFExecutor.Name = "dgv_SFExecutor";
            this.dgv_SFExecutor.ReadOnly = true;
            this.dgv_SFExecutor.Width = 125;
            // 
            // dgv_PID
            // 
            this.dgv_PID.DataPropertyName = "PID";
            this.dgv_PID.HeaderText = "PID";
            this.dgv_PID.MinimumWidth = 6;
            this.dgv_PID.Name = "dgv_PID";
            this.dgv_PID.ReadOnly = true;
            this.dgv_PID.Width = 125;
            // 
            // dgv_PID2
            // 
            this.dgv_PID2.DataPropertyName = "PID2";
            this.dgv_PID2.HeaderText = "PID2";
            this.dgv_PID2.MinimumWidth = 6;
            this.dgv_PID2.Name = "dgv_PID2";
            this.dgv_PID2.ReadOnly = true;
            this.dgv_PID2.Width = 125;
            // 
            // dgv_AFNNum
            // 
            this.dgv_AFNNum.DataPropertyName = "AFNNum";
            this.dgv_AFNNum.HeaderText = "№ АФН";
            this.dgv_AFNNum.MinimumWidth = 6;
            this.dgv_AFNNum.Name = "dgv_AFNNum";
            this.dgv_AFNNum.ReadOnly = true;
            this.dgv_AFNNum.Width = 125;
            // 
            // dgv_AFNDate
            // 
            this.dgv_AFNDate.DataPropertyName = "AFNDate";
            this.dgv_AFNDate.HeaderText = "Дата АФН";
            this.dgv_AFNDate.MinimumWidth = 6;
            this.dgv_AFNDate.Name = "dgv_AFNDate";
            this.dgv_AFNDate.ReadOnly = true;
            this.dgv_AFNDate.Width = 125;
            // 
            // dgv_ABKNum
            // 
            this.dgv_ABKNum.DataPropertyName = "ABKNum";
            this.dgv_ABKNum.HeaderText = "№ АВК";
            this.dgv_ABKNum.Name = "dgv_ABKNum";
            this.dgv_ABKNum.ReadOnly = true;
            // 
            // dgv_AFNRowNo
            // 
            this.dgv_AFNRowNo.DataPropertyName = "AFNRowNo";
            this.dgv_AFNRowNo.HeaderText = "№ п/п в АФН";
            this.dgv_AFNRowNo.MinimumWidth = 6;
            this.dgv_AFNRowNo.Name = "dgv_AFNRowNo";
            this.dgv_AFNRowNo.ReadOnly = true;
            this.dgv_AFNRowNo.Width = 125;
            // 
            // dgv_AFNQty
            // 
            this.dgv_AFNQty.DataPropertyName = "AFNQty";
            this.dgv_AFNQty.HeaderText = "Кол-во по АФН";
            this.dgv_AFNQty.Name = "dgv_AFNQty";
            this.dgv_AFNQty.ReadOnly = true;
            // 
            // dgv_Reciever
            // 
            this.dgv_Reciever.DataPropertyName = "Reciever";
            this.dgv_Reciever.HeaderText = "Кто получил";
            this.dgv_Reciever.MinimumWidth = 6;
            this.dgv_Reciever.Name = "dgv_Reciever";
            this.dgv_Reciever.ReadOnly = true;
            this.dgv_Reciever.Width = 125;
            // 
            // dgv_LandingPlace
            // 
            this.dgv_LandingPlace.DataPropertyName = "LandingPlace";
            this.dgv_LandingPlace.HeaderText = "Место отгрузки";
            this.dgv_LandingPlace.MinimumWidth = 6;
            this.dgv_LandingPlace.Name = "dgv_LandingPlace";
            this.dgv_LandingPlace.ReadOnly = true;
            this.dgv_LandingPlace.Width = 125;
            // 
            // dgv_M15Num
            // 
            this.dgv_M15Num.DataPropertyName = "M15Num";
            this.dgv_M15Num.HeaderText = "№ М-15";
            this.dgv_M15Num.MinimumWidth = 6;
            this.dgv_M15Num.Name = "dgv_M15Num";
            this.dgv_M15Num.ReadOnly = true;
            this.dgv_M15Num.Width = 125;
            // 
            // dgv_M15Date
            // 
            this.dgv_M15Date.DataPropertyName = "M15Date";
            this.dgv_M15Date.HeaderText = "Дата М-15";
            this.dgv_M15Date.MinimumWidth = 6;
            this.dgv_M15Date.Name = "dgv_M15Date";
            this.dgv_M15Date.ReadOnly = true;
            this.dgv_M15Date.Width = 125;
            // 
            // dgv_M15RowNo
            // 
            this.dgv_M15RowNo.DataPropertyName = "M15RowNo";
            this.dgv_M15RowNo.HeaderText = "№ п/п в М15";
            this.dgv_M15RowNo.MinimumWidth = 6;
            this.dgv_M15RowNo.Name = "dgv_M15RowNo";
            this.dgv_M15RowNo.ReadOnly = true;
            this.dgv_M15RowNo.Width = 125;
            // 
            // dgv_M15Qty
            // 
            this.dgv_M15Qty.DataPropertyName = "M15Qty";
            this.dgv_M15Qty.HeaderText = "Кол-во по М-15";
            this.dgv_M15Qty.Name = "dgv_M15Qty";
            this.dgv_M15Qty.ReadOnly = true;
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
            this.chkDoneType.Location = new System.Drawing.Point(1558, 30);
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
            this.chkDoneSubcode.Location = new System.Drawing.Point(1490, 30);
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
            this.lblPb.Location = new System.Drawing.Point(1292, 0);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 49;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(1297, 19);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(307, 5);
            this.pb.TabIndex = 48;
            this.pb.Tag = "lblPb";
            this.pb.Visible = false;
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
            this.button3.Location = new System.Drawing.Point(1324, 527);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 21);
            this.button3.TabIndex = 52;
            this.button3.Text = "Заявка в 1С";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1202, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 82;
            this.button1.Text = "Очистить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(966, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Очистить данные по id";
            // 
            // M15Id
            // 
            this.M15Id.Location = new System.Drawing.Point(1096, 4);
            this.M15Id.Name = "M15Id";
            this.M15Id.Size = new System.Drawing.Size(100, 20);
            this.M15Id.TabIndex = 80;
            // 
            // M15
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.M15Id);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.filter2);
            this.Controls.Add(this.filter1);
            this.Controls.Add(this.txtFilter2);
            this.Controls.Add(this.txtFilter1);
            this.Controls.Add(this.button3);
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
            this.Name = "M15";
            this.Size = new System.Drawing.Size(1607, 551);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtFilter1;
        private System.Windows.Forms.TextBox txtFilter2;
        private System.Windows.Forms.ComboBox filter1;
        private System.Windows.Forms.ComboBox filter2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SManagerAO;
        private System.Windows.Forms.DataGridViewImageColumn dgv_S_btn_folder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox M15Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id_M15Id;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_AFNNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_AFNDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ABKNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_AFNRowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_AFNQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Reciever;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_LandingPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15RowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15Qty;
    }
}
