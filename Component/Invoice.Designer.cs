namespace SmuOk.Component
{
  partial class Invoice
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      this.lstExecFilter = new System.Windows.Forms.ComboBox();
      this.lstSpecUserFilter = new System.Windows.Forms.ComboBox();
      this.lstSpecTypeFilter = new System.Windows.Forms.ComboBox();
      this.SpecInfo = new System.Windows.Forms.TextBox();
      this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
      this.dgvSpec = new System.Windows.Forms.DataGridView();
      this.lblPb = new System.Windows.Forms.Label();
      this.pb = new System.Windows.Forms.ProgressBar();
      this.btnImport = new System.Windows.Forms.Button();
      this.btnExport = new System.Windows.Forms.Button();
      this.lstSpecHasFillingFilter = new System.Windows.Forms.ComboBox();
      this.dgvInvoiceFill = new System.Windows.Forms.DataGridView();
      this.dgv_OId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_O1sId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_ONo = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OArt = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvINN = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvK = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_OInvPrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lstSpecManagerAO = new System.Windows.Forms.ComboBox();
      this.SpecList_ShowFolder = new System.Windows.Forms.CheckBox();
      this.SpecList_ShowManagerAO = new System.Windows.Forms.CheckBox();
      this.SpecList_ShowType = new System.Windows.Forms.CheckBox();
      this.SpecList_ShowID = new System.Windows.Forms.CheckBox();
      this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_SManagerAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_S_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceFill)).BeginInit();
      this.SuspendLayout();
      // 
      // lstExecFilter
      // 
      this.lstExecFilter.FormattingEnabled = true;
      this.lstExecFilter.Location = new System.Drawing.Point(570, 3);
      this.lstExecFilter.Name = "lstExecFilter";
      this.lstExecFilter.Size = new System.Drawing.Size(121, 21);
      this.lstExecFilter.TabIndex = 55;
      this.lstExecFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // lstSpecUserFilter
      // 
      this.lstSpecUserFilter.FormattingEnabled = true;
      this.lstSpecUserFilter.Location = new System.Drawing.Point(443, 3);
      this.lstSpecUserFilter.Name = "lstSpecUserFilter";
      this.lstSpecUserFilter.Size = new System.Drawing.Size(121, 21);
      this.lstSpecUserFilter.TabIndex = 56;
      this.lstSpecUserFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // lstSpecTypeFilter
      // 
      this.lstSpecTypeFilter.FormattingEnabled = true;
      this.lstSpecTypeFilter.Location = new System.Drawing.Point(160, 3);
      this.lstSpecTypeFilter.Name = "lstSpecTypeFilter";
      this.lstSpecTypeFilter.Size = new System.Drawing.Size(150, 21);
      this.lstSpecTypeFilter.TabIndex = 53;
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
      this.SpecInfo.TabIndex = 59;
      this.SpecInfo.Text = "(подробно)";
      // 
      // txtSpecNameFilter
      // 
      this.txtSpecNameFilter.ForeColor = System.Drawing.Color.Gray;
      this.txtSpecNameFilter.Location = new System.Drawing.Point(3, 3);
      this.txtSpecNameFilter.Name = "txtSpecNameFilter";
      this.txtSpecNameFilter.Size = new System.Drawing.Size(151, 20);
      this.txtSpecNameFilter.TabIndex = 58;
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
      this.dgvSpec.Location = new System.Drawing.Point(3, 29);
      this.dgvSpec.MultiSelect = false;
      this.dgvSpec.Name = "dgvSpec";
      this.dgvSpec.ReadOnly = true;
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
      this.dgvSpec.Size = new System.Drawing.Size(313, 666);
      this.dgvSpec.TabIndex = 57;
      this.dgvSpec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellClick);
      this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
      // 
      // lblPb
      // 
      this.lblPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblPb.AutoSize = true;
      this.lblPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblPb.ForeColor = System.Drawing.SystemColors.ControlDark;
      this.lblPb.Location = new System.Drawing.Point(1327, 2);
      this.lblPb.Name = "lblPb";
      this.lblPb.Size = new System.Drawing.Size(67, 13);
      this.lblPb.TabIndex = 67;
      this.lblPb.Text = "==========";
      this.lblPb.Visible = false;
      // 
      // pb
      // 
      this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pb.Location = new System.Drawing.Point(1330, 18);
      this.pb.Name = "pb";
      this.pb.Size = new System.Drawing.Size(307, 5);
      this.pb.TabIndex = 66;
      this.pb.Tag = "lblPb";
      this.pb.Visible = false;
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
      this.btnImport.Location = new System.Drawing.Point(1463, 697);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(83, 23);
      this.btnImport.TabIndex = 63;
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
      this.btnExport.Location = new System.Drawing.Point(1552, 697);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(88, 23);
      this.btnExport.TabIndex = 64;
      this.btnExport.Text = "Выгрузить";
      this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // lstSpecHasFillingFilter
      // 
      this.lstSpecHasFillingFilter.FormattingEnabled = true;
      this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(316, 3);
      this.lstSpecHasFillingFilter.Name = "lstSpecHasFillingFilter";
      this.lstSpecHasFillingFilter.Size = new System.Drawing.Size(121, 21);
      this.lstSpecHasFillingFilter.TabIndex = 54;
      this.lstSpecHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // dgvInvoiceFill
      // 
      this.dgvInvoiceFill.AllowUserToAddRows = false;
      this.dgvInvoiceFill.AllowUserToDeleteRows = false;
      dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
      this.dgvInvoiceFill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
      this.dgvInvoiceFill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvInvoiceFill.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.dgvInvoiceFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dgvInvoiceFill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvInvoiceFill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_OId,
            this.dgv_O1sId,
            this.dgv_ONo,
            this.dgv_OArt,
            this.dgv_OName,
            this.dgv_OUnit,
            this.dgv_OQty,
            this.dgv_OInvINN,
            this.dgv_OInvNo,
            this.dgv_OInvDate,
            this.dgv_OInvNo2,
            this.dgv_OInvName,
            this.dgv_OInvUnit,
            this.dgv_OInvK,
            this.dgv_OInvQty,
            this.dgv_OInvPrc});
      dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvInvoiceFill.DefaultCellStyle = dataGridViewCellStyle9;
      this.dgvInvoiceFill.Location = new System.Drawing.Point(322, 49);
      this.dgvInvoiceFill.Name = "dgvInvoiceFill";
      this.dgvInvoiceFill.RowHeadersVisible = false;
      this.dgvInvoiceFill.Size = new System.Drawing.Size(1314, 646);
      this.dgvInvoiceFill.TabIndex = 77;
      this.dgvInvoiceFill.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvInvoiceFill_ColumnWidthChanged);
      // 
      // dgv_OId
      // 
      this.dgv_OId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OId.DataPropertyName = "OId";
      this.dgv_OId.FillWeight = 40F;
      this.dgv_OId.HeaderText = "OId";
      this.dgv_OId.Name = "dgv_OId";
      this.dgv_OId.Visible = false;
      this.dgv_OId.Width = 40;
      // 
      // dgv_O1sId
      // 
      this.dgv_O1sId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_O1sId.DataPropertyName = "O1sId";
      this.dgv_O1sId.HeaderText = "№ планирования";
      this.dgv_O1sId.Name = "dgv_O1sId";
      // 
      // dgv_ONo
      // 
      this.dgv_ONo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_ONo.DataPropertyName = "ONo";
      this.dgv_ONo.FillWeight = 35F;
      this.dgv_ONo.HeaderText = "№ п/п в планировании";
      this.dgv_ONo.Name = "dgv_ONo";
      this.dgv_ONo.Width = 102;
      // 
      // dgv_OArt
      // 
      this.dgv_OArt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OArt.DataPropertyName = "OArt";
      this.dgv_OArt.HeaderText = "Артикул 1C";
      this.dgv_OArt.Name = "dgv_OArt";
      // 
      // dgv_OName
      // 
      this.dgv_OName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OName.DataPropertyName = "OName";
      this.dgv_OName.HeaderText = "Наименование 1С";
      this.dgv_OName.Name = "dgv_OName";
      // 
      // dgv_OUnit
      // 
      this.dgv_OUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OUnit.DataPropertyName = "OUnit";
      this.dgv_OUnit.HeaderText = "Ед. изм. 1C";
      this.dgv_OUnit.Name = "dgv_OUnit";
      // 
      // dgv_OQty
      // 
      this.dgv_OQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OQty.DataPropertyName = "OQty";
      this.dgv_OQty.FillWeight = 70F;
      this.dgv_OQty.HeaderText = "К-во 1С";
      this.dgv_OQty.Name = "dgv_OQty";
      this.dgv_OQty.Width = 70;
      // 
      // dgv_OInvINN
      // 
      this.dgv_OInvINN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvINN.DataPropertyName = "OInvINN";
      this.dgv_OInvINN.HeaderText = "ИНН";
      this.dgv_OInvINN.Name = "dgv_OInvINN";
      // 
      // dgv_OInvNo
      // 
      this.dgv_OInvNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvNo.DataPropertyName = "OInvNo";
      this.dgv_OInvNo.HeaderText = "№ счета";
      this.dgv_OInvNo.Name = "dgv_OInvNo";
      // 
      // dgv_OInvDate
      // 
      this.dgv_OInvDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvDate.DataPropertyName = "OInvDate";
      dataGridViewCellStyle5.Format = "d";
      this.dgv_OInvDate.DefaultCellStyle = dataGridViewCellStyle5;
      this.dgv_OInvDate.HeaderText = "Дата счета";
      this.dgv_OInvDate.Name = "dgv_OInvDate";
      // 
      // dgv_OInvNo2
      // 
      this.dgv_OInvNo2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvNo2.DataPropertyName = "OInvNo2";
      this.dgv_OInvNo2.HeaderText = "№ п/п в счете";
      this.dgv_OInvNo2.Name = "dgv_OInvNo2";
      // 
      // dgv_OInvName
      // 
      this.dgv_OInvName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvName.DataPropertyName = "OInvName";
      this.dgv_OInvName.HeaderText = "Наименование";
      this.dgv_OInvName.Name = "dgv_OInvName";
      // 
      // dgv_OInvUnit
      // 
      this.dgv_OInvUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvUnit.DataPropertyName = "OInvUnit";
      this.dgv_OInvUnit.HeaderText = "Ед. изм.";
      this.dgv_OInvUnit.Name = "dgv_OInvUnit";
      // 
      // dgv_OInvK
      // 
      this.dgv_OInvK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvK.DataPropertyName = "OInvK";
      dataGridViewCellStyle6.Format = "n4";
      this.dgv_OInvK.DefaultCellStyle = dataGridViewCellStyle6;
      this.dgv_OInvK.FillWeight = 50F;
      this.dgv_OInvK.HeaderText = "К перев.";
      this.dgv_OInvK.Name = "dgv_OInvK";
      this.dgv_OInvK.Width = 50;
      // 
      // dgv_OInvQty
      // 
      this.dgv_OInvQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvQty.DataPropertyName = "OInvQty";
      dataGridViewCellStyle7.Format = "n4";
      this.dgv_OInvQty.DefaultCellStyle = dataGridViewCellStyle7;
      this.dgv_OInvQty.HeaderText = "К-во по счету";
      this.dgv_OInvQty.Name = "dgv_OInvQty";
      // 
      // dgv_OInvPrc
      // 
      this.dgv_OInvPrc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dgv_OInvPrc.DataPropertyName = "OInvPrc";
      dataGridViewCellStyle8.Format = "n2";
      this.dgv_OInvPrc.DefaultCellStyle = dataGridViewCellStyle8;
      this.dgv_OInvPrc.HeaderText = "Цена за 1 ед.";
      this.dgv_OInvPrc.Name = "dgv_OInvPrc";
      // 
      // lstSpecManagerAO
      // 
      this.lstSpecManagerAO.FormattingEnabled = true;
      this.lstSpecManagerAO.Location = new System.Drawing.Point(697, 3);
      this.lstSpecManagerAO.Name = "lstSpecManagerAO";
      this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
      this.lstSpecManagerAO.TabIndex = 55;
      this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // SpecList_ShowFolder
      // 
      this.SpecList_ShowFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.SpecList_ShowFolder.AutoSize = true;
      this.SpecList_ShowFolder.Checked = true;
      this.SpecList_ShowFolder.CheckState = System.Windows.Forms.CheckState.Checked;
      this.SpecList_ShowFolder.Location = new System.Drawing.Point(256, 701);
      this.SpecList_ShowFolder.Name = "SpecList_ShowFolder";
      this.SpecList_ShowFolder.Size = new System.Drawing.Size(58, 17);
      this.SpecList_ShowFolder.TabIndex = 78;
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
      this.SpecList_ShowManagerAO.Location = new System.Drawing.Point(99, 701);
      this.SpecList_ShowManagerAO.Name = "SpecList_ShowManagerAO";
      this.SpecList_ShowManagerAO.Size = new System.Drawing.Size(123, 17);
      this.SpecList_ShowManagerAO.TabIndex = 79;
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
      this.SpecList_ShowType.Location = new System.Drawing.Point(48, 701);
      this.SpecList_ShowType.Name = "SpecList_ShowType";
      this.SpecList_ShowType.Size = new System.Drawing.Size(45, 17);
      this.SpecList_ShowType.TabIndex = 80;
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
      this.SpecList_ShowID.Location = new System.Drawing.Point(5, 701);
      this.SpecList_ShowID.Name = "SpecList_ShowID";
      this.SpecList_ShowID.Size = new System.Drawing.Size(37, 17);
      this.SpecList_ShowID.TabIndex = 81;
      this.SpecList_ShowID.Tag = "dgv_SId";
      this.SpecList_ShowID.Text = "ID";
      this.SpecList_ShowID.UseVisualStyleBackColor = true;
      this.SpecList_ShowID.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
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
      // Invoice
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.SpecList_ShowFolder);
      this.Controls.Add(this.SpecList_ShowManagerAO);
      this.Controls.Add(this.SpecList_ShowType);
      this.Controls.Add(this.SpecList_ShowID);
      this.Controls.Add(this.dgvInvoiceFill);
      this.Controls.Add(this.lstSpecManagerAO);
      this.Controls.Add(this.lstExecFilter);
      this.Controls.Add(this.lstSpecUserFilter);
      this.Controls.Add(this.lstSpecTypeFilter);
      this.Controls.Add(this.SpecInfo);
      this.Controls.Add(this.txtSpecNameFilter);
      this.Controls.Add(this.dgvSpec);
      this.Controls.Add(this.lblPb);
      this.Controls.Add(this.pb);
      this.Controls.Add(this.btnImport);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.lstSpecHasFillingFilter);
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "Invoice";
      this.Size = new System.Drawing.Size(1640, 730);
      this.Load += new System.EventHandler(this.Invoice_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceFill)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox lstExecFilter;
    private System.Windows.Forms.ComboBox lstSpecUserFilter;
    private System.Windows.Forms.ComboBox lstSpecTypeFilter;
    private System.Windows.Forms.TextBox SpecInfo;
    private System.Windows.Forms.TextBox txtSpecNameFilter;
    private System.Windows.Forms.DataGridView dgvSpec;
    private System.Windows.Forms.Label lblPb;
    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.ComboBox lstSpecHasFillingFilter;
    private System.Windows.Forms.DataGridView dgvInvoiceFill;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_O1sId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ONo;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OArt;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OUnit;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OQty;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvINN;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvNo;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvNo2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvUnit;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvK;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvQty;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_OInvPrc;
    private System.Windows.Forms.ComboBox lstSpecManagerAO;
    private System.Windows.Forms.CheckBox SpecList_ShowFolder;
    private System.Windows.Forms.CheckBox SpecList_ShowManagerAO;
    private System.Windows.Forms.CheckBox SpecList_ShowType;
    private System.Windows.Forms.CheckBox SpecList_ShowID;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_STName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SManagerAO;
    private System.Windows.Forms.DataGridViewImageColumn dgv_S_btn_folder;
  }
}
