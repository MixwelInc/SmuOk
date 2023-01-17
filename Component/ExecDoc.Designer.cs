namespace SmuOk.Component
{
  partial class ExecDoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDocSave = new System.Windows.Forms.Button();
            this.dgvPTODoc = new System.Windows.Forms.DataGridView();
            this.btnAddExecAcc = new System.Windows.Forms.Button();
            this.btnAddExecCIW = new System.Windows.Forms.Button();
            this.lstExecAcc = new System.Windows.Forms.ListBox();
            this.lstExecCIW = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSpecSave = new System.Windows.Forms.Button();
            this.lstAcc = new System.Windows.Forms.ComboBox();
            this.lstCIW = new System.Windows.Forms.ComboBox();
            this.CuratorSpecName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvSpec = new System.Windows.Forms.DataGridView();
            this.lstSpecUserFilter = new System.Windows.Forms.ComboBox();
            this.lstSpecHasFillingFilter = new System.Windows.Forms.ComboBox();
            this.lstSpecTypeFilter = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.dgv_SState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPTODoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSpecNameFilter
            // 
            this.txtSpecNameFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtSpecNameFilter.Location = new System.Drawing.Point(3, 3);
            this.txtSpecNameFilter.Name = "txtSpecNameFilter";
            this.txtSpecNameFilter.Size = new System.Drawing.Size(151, 20);
            this.txtSpecNameFilter.TabIndex = 51;
            this.txtSpecNameFilter.Tag = "Шифр...";
            this.txtSpecNameFilter.Enter += new System.EventHandler(this.txtSpecNameFilter_Enter);
            this.txtSpecNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSpecNameFilter_KeyUp);
            this.txtSpecNameFilter.Leave += new System.EventHandler(this.txtSpecNameFilter_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDocSave);
            this.groupBox1.Controls.Add(this.dgvPTODoc);
            this.groupBox1.Location = new System.Drawing.Point(545, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 497);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исполнительная документация";
            // 
            // btnDocSave
            // 
            this.btnDocSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDocSave.Location = new System.Drawing.Point(523, 468);
            this.btnDocSave.Name = "btnDocSave";
            this.btnDocSave.Size = new System.Drawing.Size(96, 23);
            this.btnDocSave.TabIndex = 2;
            this.btnDocSave.Text = "Сохранить";
            this.btnDocSave.UseVisualStyleBackColor = true;
            this.btnDocSave.Click += new System.EventHandler(this.btnDocSave_Click);
            // 
            // dgvPTODoc
            // 
            this.dgvPTODoc.AllowUserToAddRows = false;
            this.dgvPTODoc.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvPTODoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPTODoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPTODoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPTODoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPTODoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPTODoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPTODoc.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPTODoc.Location = new System.Drawing.Point(7, 19);
            this.dgvPTODoc.Name = "dgvPTODoc";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPTODoc.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPTODoc.RowHeadersVisible = false;
            this.dgvPTODoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPTODoc.Size = new System.Drawing.Size(612, 443);
            this.dgvPTODoc.TabIndex = 0;
            // 
            // btnAddExecAcc
            // 
            this.btnAddExecAcc.Enabled = false;
            this.btnAddExecAcc.Image = global::SmuOk.Properties.Resources.hard_hat__plus;
            this.btnAddExecAcc.Location = new System.Drawing.Point(163, 167);
            this.btnAddExecAcc.Name = "btnAddExecAcc";
            this.btnAddExecAcc.Size = new System.Drawing.Size(27, 22);
            this.btnAddExecAcc.TabIndex = 41;
            this.btnAddExecAcc.UseVisualStyleBackColor = true;
            this.btnAddExecAcc.Click += new System.EventHandler(this.btnAddExecAcc_Click);
            // 
            // btnAddExecCIW
            // 
            this.btnAddExecCIW.Enabled = false;
            this.btnAddExecCIW.Image = global::SmuOk.Properties.Resources.hard_hat__plus;
            this.btnAddExecCIW.Location = new System.Drawing.Point(164, 20);
            this.btnAddExecCIW.Name = "btnAddExecCIW";
            this.btnAddExecCIW.Size = new System.Drawing.Size(27, 22);
            this.btnAddExecCIW.TabIndex = 42;
            this.btnAddExecCIW.UseVisualStyleBackColor = true;
            this.btnAddExecCIW.Click += new System.EventHandler(this.btnAddExecCIW_Click);
            // 
            // lstExecAcc
            // 
            this.lstExecAcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstExecAcc.FormattingEnabled = true;
            this.lstExecAcc.Location = new System.Drawing.Point(47, 195);
            this.lstExecAcc.Name = "lstExecAcc";
            this.lstExecAcc.Size = new System.Drawing.Size(143, 106);
            this.lstExecAcc.TabIndex = 48;
            this.lstExecAcc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstExecAcc_KeyUp);
            // 
            // lstExecCIW
            // 
            this.lstExecCIW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstExecCIW.FormattingEnabled = true;
            this.lstExecCIW.Location = new System.Drawing.Point(47, 48);
            this.lstExecCIW.Name = "lstExecCIW";
            this.lstExecCIW.Size = new System.Drawing.Size(143, 106);
            this.lstExecCIW.TabIndex = 49;
            this.lstExecCIW.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstExecCIW_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "ПНР";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "СМР";
            // 
            // btnSpecSave
            // 
            this.btnSpecSave.Location = new System.Drawing.Point(16, 312);
            this.btnSpecSave.Name = "btnSpecSave";
            this.btnSpecSave.Size = new System.Drawing.Size(175, 23);
            this.btnSpecSave.TabIndex = 45;
            this.btnSpecSave.Text = "Сохранить";
            this.btnSpecSave.UseVisualStyleBackColor = true;
            this.btnSpecSave.Click += new System.EventHandler(this.btnSpecSave_Click);
            // 
            // lstAcc
            // 
            this.lstAcc.FormattingEnabled = true;
            this.lstAcc.Location = new System.Drawing.Point(47, 168);
            this.lstAcc.Name = "lstAcc";
            this.lstAcc.Size = new System.Drawing.Size(114, 21);
            this.lstAcc.TabIndex = 43;
            this.lstAcc.Tag = "";
            this.lstAcc.SelectedIndexChanged += new System.EventHandler(this.lstAcc_SelectedIndexChanged);
            // 
            // lstCIW
            // 
            this.lstCIW.FormattingEnabled = true;
            this.lstCIW.Location = new System.Drawing.Point(47, 21);
            this.lstCIW.Name = "lstCIW";
            this.lstCIW.Size = new System.Drawing.Size(113, 21);
            this.lstCIW.TabIndex = 44;
            this.lstCIW.Tag = "";
            this.lstCIW.SelectedIndexChanged += new System.EventHandler(this.lstCIW_SelectedIndexChanged);
            // 
            // CuratorSpecName
            // 
            this.CuratorSpecName.Location = new System.Drawing.Point(395, 43);
            this.CuratorSpecName.Name = "CuratorSpecName";
            this.CuratorSpecName.Size = new System.Drawing.Size(345, 20);
            this.CuratorSpecName.TabIndex = 40;
            this.CuratorSpecName.Text = "новая спецификация (шифр)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(333, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Название";
            // 
            // dgvSpec
            // 
            this.dgvSpec.AllowUserToAddRows = false;
            this.dgvSpec.AllowUserToDeleteRows = false;
            this.dgvSpec.AllowUserToResizeRows = false;
            this.dgvSpec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSpec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SId,
            this.dgv_STName,
            this.dgv_SVName,
            this.dgv_SManagerAO,
            this.dgv_S_btn_folder,
            this.dgv_SState});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpec.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSpec.Location = new System.Drawing.Point(3, 29);
            this.dgvSpec.MultiSelect = false;
            this.dgvSpec.Name = "dgvSpec";
            this.dgvSpec.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpec.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSpec.RowHeadersVisible = false;
            this.dgvSpec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSpec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpec.Size = new System.Drawing.Size(313, 502);
            this.dgvSpec.TabIndex = 37;
            this.dgvSpec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellClick);
            this.dgvSpec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            // 
            // lstSpecUserFilter
            // 
            this.lstSpecUserFilter.FormattingEnabled = true;
            this.lstSpecUserFilter.Location = new System.Drawing.Point(443, 3);
            this.lstSpecUserFilter.Name = "lstSpecUserFilter";
            this.lstSpecUserFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecUserFilter.TabIndex = 36;
            this.lstSpecUserFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecHasFillingFilter
            // 
            this.lstSpecHasFillingFilter.FormattingEnabled = true;
            this.lstSpecHasFillingFilter.Location = new System.Drawing.Point(316, 3);
            this.lstSpecHasFillingFilter.Name = "lstSpecHasFillingFilter";
            this.lstSpecHasFillingFilter.Size = new System.Drawing.Size(121, 21);
            this.lstSpecHasFillingFilter.TabIndex = 35;
            this.lstSpecHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // lstSpecTypeFilter
            // 
            this.lstSpecTypeFilter.FormattingEnabled = true;
            this.lstSpecTypeFilter.Location = new System.Drawing.Point(160, 3);
            this.lstSpecTypeFilter.Name = "lstSpecTypeFilter";
            this.lstSpecTypeFilter.Size = new System.Drawing.Size(150, 21);
            this.lstSpecTypeFilter.TabIndex = 34;
            this.lstSpecTypeFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lstCIW);
            this.groupBox2.Controls.Add(this.lstAcc);
            this.groupBox2.Controls.Add(this.btnAddExecAcc);
            this.groupBox2.Controls.Add(this.btnSpecSave);
            this.groupBox2.Controls.Add(this.btnAddExecCIW);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lstExecAcc);
            this.groupBox2.Controls.Add(this.lstExecCIW);
            this.groupBox2.Location = new System.Drawing.Point(336, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 347);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Исполнители";
            // 
            // lstSpecManagerAO
            // 
            this.lstSpecManagerAO.FormattingEnabled = true;
            this.lstSpecManagerAO.Location = new System.Drawing.Point(570, 3);
            this.lstSpecManagerAO.Name = "lstSpecManagerAO";
            this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
            this.lstSpecManagerAO.TabIndex = 36;
            this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
            // 
            // SpecList_ShowFolder
            // 
            this.SpecList_ShowFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpecList_ShowFolder.AutoSize = true;
            this.SpecList_ShowFolder.Checked = true;
            this.SpecList_ShowFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpecList_ShowFolder.Location = new System.Drawing.Point(255, 537);
            this.SpecList_ShowFolder.Name = "SpecList_ShowFolder";
            this.SpecList_ShowFolder.Size = new System.Drawing.Size(58, 17);
            this.SpecList_ShowFolder.TabIndex = 53;
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
            this.SpecList_ShowManagerAO.Location = new System.Drawing.Point(98, 537);
            this.SpecList_ShowManagerAO.Name = "SpecList_ShowManagerAO";
            this.SpecList_ShowManagerAO.Size = new System.Drawing.Size(123, 17);
            this.SpecList_ShowManagerAO.TabIndex = 54;
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
            this.SpecList_ShowType.Location = new System.Drawing.Point(47, 537);
            this.SpecList_ShowType.Name = "SpecList_ShowType";
            this.SpecList_ShowType.Size = new System.Drawing.Size(45, 17);
            this.SpecList_ShowType.TabIndex = 55;
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
            this.SpecList_ShowID.Location = new System.Drawing.Point(4, 537);
            this.SpecList_ShowID.Name = "SpecList_ShowID";
            this.SpecList_ShowID.Size = new System.Drawing.Size(37, 17);
            this.SpecList_ShowID.TabIndex = 56;
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
            this.dgv_SManagerAO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
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
            // dgv_SState
            // 
            this.dgv_SState.DataPropertyName = "SState";
            this.dgv_SState.HeaderText = "Статус";
            this.dgv_SState.Name = "dgv_SState";
            this.dgv_SState.ReadOnly = true;
            this.dgv_SState.Visible = false;
            // 
            // ExecDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SpecList_ShowFolder);
            this.Controls.Add(this.SpecList_ShowManagerAO);
            this.Controls.Add(this.SpecList_ShowType);
            this.Controls.Add(this.SpecList_ShowID);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSpecNameFilter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CuratorSpecName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvSpec);
            this.Controls.Add(this.lstSpecManagerAO);
            this.Controls.Add(this.lstSpecUserFilter);
            this.Controls.Add(this.lstSpecHasFillingFilter);
            this.Controls.Add(this.lstSpecTypeFilter);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ExecDoc";
            this.Size = new System.Drawing.Size(1173, 566);
            this.Load += new System.EventHandler(this.ExecDoc_Load);
            this.dgvSpec.RowPrePaint
            += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(
            this.dgvSpec_RowPrePaint);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPTODoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox txtSpecNameFilter;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnDocSave;
    private System.Windows.Forms.DataGridView dgvPTODoc;
    private System.Windows.Forms.Button btnAddExecAcc;
    private System.Windows.Forms.Button btnAddExecCIW;
    private System.Windows.Forms.ListBox lstExecAcc;
    private System.Windows.Forms.ListBox lstExecCIW;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btnSpecSave;
    private System.Windows.Forms.ComboBox lstAcc;
    private System.Windows.Forms.ComboBox lstCIW;
    private System.Windows.Forms.TextBox CuratorSpecName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.DataGridView dgvSpec;
    private System.Windows.Forms.ComboBox lstSpecUserFilter;
    private System.Windows.Forms.ComboBox lstSpecHasFillingFilter;
    private System.Windows.Forms.ComboBox lstSpecTypeFilter;
    private System.Windows.Forms.GroupBox groupBox2;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SState;
    }
}
