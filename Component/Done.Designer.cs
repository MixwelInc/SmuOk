namespace SmuOk.Component
{
  partial class Done
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
            this.dgvSpec = new System.Windows.Forms.DataGridView();
            this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SManagerAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_S_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.lstSpecUserFilter = new System.Windows.Forms.ComboBox();
            this.lstSpecHasFillingFilter = new System.Windows.Forms.ComboBox();
            this.lstSpecTypeFilter = new System.Windows.Forms.ComboBox();
            this.SpecInfo = new System.Windows.Forms.TextBox();
            this.dgvSpecFill = new System.Windows.Forms.DataGridView();
            this.dgv_id_SFEId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__SFEFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFSubcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__SFEQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoneQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstExecFilter = new System.Windows.Forms.ComboBox();
            this.chkDoneType = new System.Windows.Forms.CheckBox();
            this.chkDoneSubcode = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.chkDoneMultiline = new System.Windows.Forms.CheckBox();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.lblPb = new System.Windows.Forms.Label();
            this.lstSpecManagerAO = new System.Windows.Forms.ComboBox();
            this.SpecList_ShowID = new System.Windows.Forms.CheckBox();
            this.SpecList_ShowManagerAO = new System.Windows.Forms.CheckBox();
            this.SpecList_ShowFolder = new System.Windows.Forms.CheckBox();
            this.SpecList_ShowType = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.txtSpecNameFilter.TabIndex = 28;
            this.txtSpecNameFilter.Tag = "Шифр...";
            this.txtSpecNameFilter.Enter += new System.EventHandler(this.txtSpecNameFilter_Enter);
            this.txtSpecNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSpecNameFilter_KeyUp);
            this.txtSpecNameFilter.Leave += new System.EventHandler(this.txtSpecNameFilter_Leave);
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.AllowUserToResizeRows = false;
            this.dgvSpec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SId,
            this.dgv_STName,
            this.dgv_SVName,
            this.dgv_SManagerAO,
            this.dgv_S_btn_folder});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpec.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvSpec.Location = new System.Drawing.Point(3, 29);
            this.dgvSpec.MultiSelect = false;
            this.dgvSpec.Name = "dgvSpec";
            this.dgvSpec.ReadOnly = true;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvSpec.RowHeadersVisible = false;
            this.dgvSpec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpec.Size = new System.Drawing.Size(313, 487);
            this.dgvSpec.TabIndex = 27;
            this.dgvSpec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellClick);
            this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            // 
            // dgv_SId
            // 
            this.dgv_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SId.DataPropertyName = "SId";
            this.dgv_SId.FillWeight = 32F;
            this.dgv_SId.HeaderText = "Id";
            this.dgv_SId.Name = "dgv_SId";
            this.dgv_SId.ReadOnly = true;
            this.dgv_SId.Width = 32;
            // 
            // dgv_STName
            // 
            this.dgv_STName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_STName.DataPropertyName = "STName";
            this.dgv_STName.HeaderText = "Тип";
            this.dgv_STName.Name = "dgv_STName";
            this.dgv_STName.ReadOnly = true;
            this.dgv_STName.Width = 51;
            // 
            // dgv_SVName
            // 
            this.dgv_SVName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_SVName.DataPropertyName = "SVName";
            this.dgv_SVName.HeaderText = "Шифр";
            this.dgv_SVName.Name = "dgv_SVName";
            this.dgv_SVName.ReadOnly = true;
            // 
            // dgv_SManagerAO
            // 
            this.dgv_SManagerAO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_SManagerAO.DataPropertyName = "ManagerAO";
            this.dgv_SManagerAO.FillWeight = 50F;
            this.dgv_SManagerAO.HeaderText = "Отв. АО";
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
            this.dgv_S_btn_folder.Name = "dgv_S_btn_folder";
            this.dgv_S_btn_folder.ReadOnly = true;
            this.dgv_S_btn_folder.Width = 28;
            // 
            // lstSpecUserFilter
            // 
            this.lstSpecUserFilter.FormattingEnabled = true;
            this.lstSpecUserFilter.Location = new System.Drawing.Point(443, 3);
            this.lstSpecUserFilter.Name = "lstSpecUserFilter";
            this.lstSpecUserFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecUserFilter.TabIndex = 26;
            this.lstSpecUserFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(316, 3);
            this.lstSpecHasFillingFilter.Name = "lstSpecHasFillingFilter";
            this.lstSpecHasFillingFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecHasFillingFilter.TabIndex = 25;
            this.lstSpecHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecTypeFilter
            // 
            this.lstSpecTypeFilter.FormattingEnabled = true;
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(160, 3);
            this.lstSpecTypeFilter.Name = "lstSpecTypeFilter";
            this.lstSpecTypeFilter.Size = new System.Drawing.Size(150, 21);
            this.lstSpecTypeFilter.TabIndex = 24;
            this.lstSpecTypeFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // SpecInfo
            // 
            this.SpecInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SpecInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SpecInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SpecInfo.Location = new System.Drawing.Point(322, 30);
            this.SpecInfo.Name = "SpecInfo";
            this.SpecInfo.Size = new System.Drawing.Size(478, 13);
            this.SpecInfo.TabIndex = 29;
            this.SpecInfo.Text = "(подробно)";
            // 
            // dgvSpecFill
            // 
            this.dgvSpecFill.AllowUserToAddRows = false;
            this.dgvSpecFill.AllowUserToDeleteRows = false;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            this.dgvSpecFill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvSpecFill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecFill.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSpecFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSpecFill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecFill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_id_SFEId,
            this.dgv__SFEFill,
            this.dgv_SFSubcode,
            this.dgv_SFType,
            this.dgv_SFNo,
            this.dgv_SFNo2,
            this.dgv_SFName,
            this.dgv_SFMark,
            this.dgv_SFUnit,
            this.dgv__SFEQty,
            this.DoneQty,
            this.ToDo});
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpecFill.DefaultCellStyle = dataGridViewCellStyle30;
            this.dgvSpecFill.Location = new System.Drawing.Point(322, 49);
            this.dgvSpecFill.Name = "dgvSpecFill";
            this.dgvSpecFill.RowHeadersVisible = false;
            this.dgvSpecFill.Size = new System.Drawing.Size(954, 467);
            this.dgvSpecFill.TabIndex = 30;
            this.dgvSpecFill.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvSpecFill_ColumnWidthChanged);
            // 
            // dgv_id_SFEId
            // 
            this.dgv_id_SFEId.DataPropertyName = "SFEId";
            this.dgv_id_SFEId.FillWeight = 20F;
            this.dgv_id_SFEId.HeaderText = "id";
            this.dgv_id_SFEId.Name = "dgv_id_SFEId";
            this.dgv_id_SFEId.Visible = false;
            this.dgv_id_SFEId.Width = 20;
            // 
            // dgv__SFEFill
            // 
            this.dgv__SFEFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv__SFEFill.DataPropertyName = "SFEFill";
            this.dgv__SFEFill.FillWeight = 20F;
            this.dgv__SFEFill.HeaderText = "SFEFill";
            this.dgv__SFEFill.MinimumWidth = 25;
            this.dgv__SFEFill.Name = "dgv__SFEFill";
            this.dgv__SFEFill.Visible = false;
            // 
            // dgv_SFSubcode
            // 
            this.dgv_SFSubcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SFSubcode.DataPropertyName = "SFSubcode";
            this.dgv_SFSubcode.HeaderText = "Шифр (2)";
            this.dgv_SFSubcode.Name = "dgv_SFSubcode";
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
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFName.DefaultCellStyle = dataGridViewCellStyle25;
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
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_SFUnit.DefaultCellStyle = dataGridViewCellStyle26;
            this.dgv_SFUnit.FillWeight = 35F;
            this.dgv_SFUnit.HeaderText = "Ед.";
            this.dgv_SFUnit.MinimumWidth = 35;
            this.dgv_SFUnit.Name = "dgv_SFUnit";
            this.dgv_SFUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SFUnit.ToolTipText = "(требуется)";
            this.dgv_SFUnit.Width = 35;
            // 
            // dgv__SFEQty
            // 
            this.dgv__SFEQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv__SFEQty.DataPropertyName = "SFEQty";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle27.Format = "N";
            this.dgv__SFEQty.DefaultCellStyle = dataGridViewCellStyle27;
            this.dgv__SFEQty.HeaderText = "Кол-во";
            this.dgv__SFEQty.MinimumWidth = 40;
            this.dgv__SFEQty.Name = "dgv__SFEQty";
            this.dgv__SFEQty.ToolTipText = "(требуется)";
            this.dgv__SFEQty.Width = 40;
            // 
            // DoneQty
            // 
            this.DoneQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DoneQty.DataPropertyName = "DSumQty";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle28.Format = "N";
            this.DoneQty.DefaultCellStyle = dataGridViewCellStyle28;
            this.DoneQty.HeaderText = "Сделано";
            this.DoneQty.Name = "DoneQty";
            // 
            // ToDo
            // 
            this.ToDo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ToDo.DataPropertyName = "DRestQty";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle29.Format = "N";
            this.ToDo.DefaultCellStyle = dataGridViewCellStyle29;
            this.ToDo.HeaderText = "Ост.";
            this.ToDo.Name = "ToDo";
            // 
            // lstExecFilter
            // 
            this.lstExecFilter.FormattingEnabled = true;
            this.lstExecFilter.Location = new System.Drawing.Point(570, 3);
            this.lstExecFilter.Name = "lstExecFilter";
            this.lstExecFilter.Size = new System.Drawing.Size(121, 21);
            this.lstExecFilter.TabIndex = 26;
            this.lstExecFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // chkDoneType
            // 
            this.chkDoneType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoneType.AutoSize = true;
            this.chkDoneType.Checked = true;
            this.chkDoneType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDoneType.Location = new System.Drawing.Point(1231, 30);
            this.chkDoneType.Name = "chkDoneType";
            this.chkDoneType.Size = new System.Drawing.Size(45, 17);
            this.chkDoneType.TabIndex = 31;
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
            this.chkDoneSubcode.TabIndex = 32;
            this.chkDoneSubcode.Text = "шифр-2";
            this.chkDoneSubcode.UseVisualStyleBackColor = true;
            this.chkDoneSubcode.CheckedChanged += new System.EventHandler(this.chkDoneSubcode_CheckedChanged);
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
            this.btnImport.Location = new System.Drawing.Point(1102, 518);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(83, 23);
            this.btnImport.TabIndex = 33;
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
            this.btnExport.Location = new System.Drawing.Point(1191, 518);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 23);
            this.btnExport.TabIndex = 34;
            this.btnExport.Text = "Выгрузить";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
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
            this.chkDoneMultiline.TabIndex = 35;
            this.chkDoneMultiline.Text = "мнострочное название и тип/марка";
            this.chkDoneMultiline.UseVisualStyleBackColor = true;
            this.chkDoneMultiline.CheckedChanged += new System.EventHandler(this.chkDoneMultiline_CheckedChanged);
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(969, 18);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(307, 5);
            this.pb.TabIndex = 36;
            this.pb.Tag = "lblPb";
            this.pb.Visible = false;
            // 
            // lblPb
            // 
            this.lblPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPb.AutoSize = true;
            this.lblPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPb.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPb.Location = new System.Drawing.Point(966, 2);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 37;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // lstSpecManagerAO
            // 
            this.lstSpecManagerAO.FormattingEnabled = true;
            this.lstSpecManagerAO.Location = new System.Drawing.Point(697, 3);
            this.lstSpecManagerAO.Name = "lstSpecManagerAO";
            this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
            this.lstSpecManagerAO.TabIndex = 26;
            this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
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
            this.SpecList_ShowID.TabIndex = 35;
            this.SpecList_ShowID.Tag = "dgv_SId";
            this.SpecList_ShowID.Text = "ID";
            this.SpecList_ShowID.UseVisualStyleBackColor = true;
            this.SpecList_ShowID.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.SpecList_ShowManagerAO.TabIndex = 35;
            this.SpecList_ShowManagerAO.Tag = "dgv_SManagerAO";
            this.SpecList_ShowManagerAO.Text = "Ответственный АО";
            this.SpecList_ShowManagerAO.UseVisualStyleBackColor = true;
            this.SpecList_ShowManagerAO.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.SpecList_ShowFolder.TabIndex = 35;
            this.SpecList_ShowFolder.Tag = "dgv_S_btn_folder";
            this.SpecList_ShowFolder.Text = "Папки";
            this.SpecList_ShowFolder.UseVisualStyleBackColor = true;
            this.SpecList_ShowFolder.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.SpecList_ShowType.TabIndex = 35;
            this.SpecList_ShowType.Tag = "dgv_STName";
            this.SpecList_ShowType.Text = "Тип";
            this.SpecList_ShowType.UseVisualStyleBackColor = true;
            this.SpecList_ShowType.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
            this.button1.Location = new System.Drawing.Point(959, 518);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Сформировать ВСО";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Done
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.SpecList_ShowFolder);
            this.Controls.Add(this.SpecList_ShowManagerAO);
            this.Controls.Add(this.SpecList_ShowType);
            this.Controls.Add(this.SpecList_ShowID);
            this.Controls.Add(this.chkDoneMultiline);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.chkDoneType);
            this.Controls.Add(this.chkDoneSubcode);
            this.Controls.Add(this.dgvSpecFill);
            this.Controls.Add(this.SpecInfo);
            this.Controls.Add(this.txtSpecNameFilter);
            this.Controls.Add(this.dgvSpec);
            this.Controls.Add(this.lstSpecManagerAO);
            this.Controls.Add(this.lstExecFilter);
            this.Controls.Add(this.lstSpecUserFilter);
            this.Controls.Add(this.lstSpecHasFillingFilter);
            this.Controls.Add(this.lstSpecTypeFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Done";
            this.Size = new System.Drawing.Size(1279, 551);
            this.Load += new System.EventHandler(this.Done_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecFill)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtSpecNameFilter;
    private System.Windows.Forms.DataGridView dgvSpec;
    private System.Windows.Forms.ComboBox lstSpecUserFilter;
    private System.Windows.Forms.ComboBox lstSpecHasFillingFilter;
    private System.Windows.Forms.ComboBox lstSpecTypeFilter;
    private System.Windows.Forms.TextBox SpecInfo;
    private System.Windows.Forms.DataGridView dgvSpecFill;
    private System.Windows.Forms.ComboBox lstExecFilter;
    private System.Windows.Forms.CheckBox chkDoneType;
    private System.Windows.Forms.CheckBox chkDoneSubcode;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.CheckBox chkDoneMultiline;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id_SFEId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv__SFEFill;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFSubcode;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFType;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFNo;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFNo2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFMark;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFUnit;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv__SFEQty;
    private System.Windows.Forms.DataGridViewTextBoxColumn DoneQty;
    private System.Windows.Forms.DataGridViewTextBoxColumn ToDo;
    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.Label lblPb;
    private System.Windows.Forms.ComboBox lstSpecManagerAO;
    private System.Windows.Forms.CheckBox SpecList_ShowID;
    private System.Windows.Forms.CheckBox SpecList_ShowManagerAO;
    private System.Windows.Forms.CheckBox SpecList_ShowFolder;
    private System.Windows.Forms.CheckBox SpecList_ShowType;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SManagerAO;
    private System.Windows.Forms.DataGridViewImageColumn dgv_S_btn_folder;
        private System.Windows.Forms.Button button1;
    }
}
