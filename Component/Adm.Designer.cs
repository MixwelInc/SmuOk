namespace SmuOk.Component
{
  partial class Adm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lstUser = new System.Windows.Forms.ComboBox();
            this.lstRole = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIndexFolders = new System.Windows.Forms.Button();
            this.gbIndexFolders = new System.Windows.Forms.GroupBox();
            this.lstFolder = new System.Windows.Forms.ListBox();
            this.btnGetFolders = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolderSearch = new System.Windows.Forms.TextBox();
            this.btnAllData = new System.Windows.Forms.Button();
            this.btnCountData = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lstExec = new System.Windows.Forms.CheckedListBox();
            this.lstScreenshot = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtExecutor = new System.Windows.Forms.TextBox();
            this.lstExecutor = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnVerAdd = new System.Windows.Forms.Label();
            this.dgvActiveUsers = new System.Windows.Forms.DataGridView();
            this.user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.UserDept = new System.Windows.Forms.ComboBox();
            this.dgv_engDept = new System.Windows.Forms.DataGridView();
            this.dgv_id_EDId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv__EDName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Head = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_btn_save = new System.Windows.Forms.DataGridViewImageColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDptAdd = new System.Windows.Forms.Label();
            this.grUser = new System.Windows.Forms.GroupBox();
            this.UserIsDeptHead = new System.Windows.Forms.CheckBox();
            this.UserO = new System.Windows.Forms.TextBox();
            this.UserI = new System.Windows.Forms.TextBox();
            this.UserF = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnShowScreens = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnScShTo = new System.Windows.Forms.Button();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SpecId = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvBlockedSpecs = new System.Windows.Forms.DataGridView();
            this.FindSpec_btn = new System.Windows.Forms.Button();
            this.SpecID_txtBox = new System.Windows.Forms.TextBox();
            this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Block = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgv_Unblock = new System.Windows.Forms.DataGridViewButtonColumn();
            this.gbIndexFolders.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_engDept)).BeginInit();
            this.grUser.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlockedSpecs)).BeginInit();
            this.SuspendLayout();
            // 
            // lstUser
            // 
            this.lstUser.FormattingEnabled = true;
            this.lstUser.Location = new System.Drawing.Point(106, 3);
            this.lstUser.Name = "lstUser";
            this.lstUser.Size = new System.Drawing.Size(237, 21);
            this.lstUser.TabIndex = 1;
            this.lstUser.SelectedIndexChanged += new System.EventHandler(this.lstUser_SelectedIndexChanged);
            this.lstUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstUser_KeyUp);
            // 
            // lstRole
            // 
            this.lstRole.CheckOnClick = true;
            this.lstRole.FormattingEnabled = true;
            this.lstRole.Location = new System.Drawing.Point(59, 117);
            this.lstRole.Name = "lstRole";
            this.lstRole.Size = new System.Drawing.Size(276, 124);
            this.lstRole.TabIndex = 2;
            this.lstRole.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstRole_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Роли";
            // 
            // btnIndexFolders
            // 
            this.btnIndexFolders.Location = new System.Drawing.Point(159, 19);
            this.btnIndexFolders.Name = "btnIndexFolders";
            this.btnIndexFolders.Size = new System.Drawing.Size(120, 23);
            this.btnIndexFolders.TabIndex = 3;
            this.btnIndexFolders.Text = "Проиндексировать";
            this.btnIndexFolders.UseVisualStyleBackColor = true;
            this.btnIndexFolders.Click += new System.EventHandler(this.btnIndexFolders_Click);
            // 
            // gbIndexFolders
            // 
            this.gbIndexFolders.Controls.Add(this.lstFolder);
            this.gbIndexFolders.Controls.Add(this.btnGetFolders);
            this.gbIndexFolders.Controls.Add(this.label3);
            this.gbIndexFolders.Controls.Add(this.label2);
            this.gbIndexFolders.Controls.Add(this.txtFolderSearch);
            this.gbIndexFolders.Controls.Add(this.btnIndexFolders);
            this.gbIndexFolders.Location = new System.Drawing.Point(6, 553);
            this.gbIndexFolders.Name = "gbIndexFolders";
            this.gbIndexFolders.Size = new System.Drawing.Size(623, 232);
            this.gbIndexFolders.TabIndex = 4;
            this.gbIndexFolders.TabStop = false;
            this.gbIndexFolders.Text = "Папки";
            // 
            // lstFolder
            // 
            this.lstFolder.FormattingEnabled = true;
            this.lstFolder.Location = new System.Drawing.Point(20, 94);
            this.lstFolder.Name = "lstFolder";
            this.lstFolder.Size = new System.Drawing.Size(597, 121);
            this.lstFolder.TabIndex = 7;
            this.lstFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFolder_MouseDoubleClick);
            // 
            // btnGetFolders
            // 
            this.btnGetFolders.Location = new System.Drawing.Point(204, 55);
            this.btnGetFolders.Name = "btnGetFolders";
            this.btnGetFolders.Size = new System.Drawing.Size(75, 23);
            this.btnGetFolders.TabIndex = 6;
            this.btnGetFolders.Text = "Найти";
            this.btnGetFolders.UseVisualStyleBackColor = true;
            this.btnGetFolders.Click += new System.EventHandler(this.btnGetFolders_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(19, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Используйте пробел для пропуска любых символов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Поиск";
            // 
            // txtFolderSearch
            // 
            this.txtFolderSearch.Location = new System.Drawing.Point(61, 57);
            this.txtFolderSearch.Name = "txtFolderSearch";
            this.txtFolderSearch.Size = new System.Drawing.Size(134, 20);
            this.txtFolderSearch.TabIndex = 4;
            this.txtFolderSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFolderSearch_KeyUp);
            // 
            // btnAllData
            // 
            this.btnAllData.Location = new System.Drawing.Point(434, 492);
            this.btnAllData.Name = "btnAllData";
            this.btnAllData.Size = new System.Drawing.Size(196, 23);
            this.btnAllData.TabIndex = 5;
            this.btnAllData.Text = "Наполнение по всем шифрам";
            this.btnAllData.UseVisualStyleBackColor = true;
            this.btnAllData.Click += new System.EventHandler(this.btnAllData_Click);
            // 
            // btnCountData
            // 
            this.btnCountData.Location = new System.Drawing.Point(433, 521);
            this.btnCountData.Name = "btnCountData";
            this.btnCountData.Size = new System.Drawing.Size(196, 23);
            this.btnCountData.TabIndex = 5;
            this.btnCountData.Text = "Количество позиций по шифрам";
            this.btnCountData.UseVisualStyleBackColor = true;
            this.btnCountData.Click += new System.EventHandler(this.btnCountData_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Исполнители";
            // 
            // lstExec
            // 
            this.lstExec.CheckOnClick = true;
            this.lstExec.FormattingEnabled = true;
            this.lstExec.Location = new System.Drawing.Point(430, 117);
            this.lstExec.Name = "lstExec";
            this.lstExec.Size = new System.Drawing.Size(175, 124);
            this.lstExec.TabIndex = 2;
            this.lstExec.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstExec_ItemCheck);
            // 
            // lstScreenshot
            // 
            this.lstScreenshot.FormattingEnabled = true;
            this.lstScreenshot.Location = new System.Drawing.Point(470, 85);
            this.lstScreenshot.Name = "lstScreenshot";
            this.lstScreenshot.Size = new System.Drawing.Size(108, 21);
            this.lstScreenshot.TabIndex = 6;
            this.lstScreenshot.SelectedIndexChanged += new System.EventHandler(this.lstScreenshot_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Скриншот каждые";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtExecutor);
            this.groupBox1.Controls.Add(this.lstExecutor);
            this.groupBox1.Location = new System.Drawing.Point(7, 466);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 52);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исполнители";
            // 
            // txtExecutor
            // 
            this.txtExecutor.Location = new System.Drawing.Point(205, 19);
            this.txtExecutor.Name = "txtExecutor";
            this.txtExecutor.Size = new System.Drawing.Size(157, 20);
            this.txtExecutor.TabIndex = 7;
            this.txtExecutor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExecutor_KeyDown);
            // 
            // lstExecutor
            // 
            this.lstExecutor.FormattingEnabled = true;
            this.lstExecutor.Location = new System.Drawing.Point(6, 19);
            this.lstExecutor.Name = "lstExecutor";
            this.lstExecutor.Size = new System.Drawing.Size(190, 21);
            this.lstExecutor.TabIndex = 6;
            this.lstExecutor.SelectedIndexChanged += new System.EventHandler(this.lstExecutor_SelectedIndexChanged);
            this.lstExecutor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstExecutor_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnVerAdd);
            this.groupBox2.Controls.Add(this.dgvActiveUsers);
            this.groupBox2.Location = new System.Drawing.Point(656, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 777);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Активные поьзователи";
            // 
            // btnVerAdd
            // 
            this.btnVerAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerAdd.AutoSize = true;
            this.btnVerAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnVerAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnVerAdd.Location = new System.Drawing.Point(419, 16);
            this.btnVerAdd.Name = "btnVerAdd";
            this.btnVerAdd.Size = new System.Drawing.Size(54, 13);
            this.btnVerAdd.TabIndex = 5;
            this.btnVerAdd.Text = "обновить";
            this.btnVerAdd.Click += new System.EventHandler(this.btnVerAdd_Click);
            // 
            // dgvActiveUsers
            // 
            this.dgvActiveUsers.AllowUserToAddRows = false;
            this.dgvActiveUsers.AllowUserToDeleteRows = false;
            this.dgvActiveUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvActiveUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.user,
            this.comp});
            this.dgvActiveUsers.Location = new System.Drawing.Point(6, 32);
            this.dgvActiveUsers.Name = "dgvActiveUsers";
            this.dgvActiveUsers.ReadOnly = true;
            this.dgvActiveUsers.RowHeadersVisible = false;
            this.dgvActiveUsers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvActiveUsers.Size = new System.Drawing.Size(467, 739);
            this.dgvActiveUsers.TabIndex = 0;
            // 
            // user
            // 
            this.user.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.user.DataPropertyName = "dgv_user";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.user.DefaultCellStyle = dataGridViewCellStyle1;
            this.user.HeaderText = "Пользователь";
            this.user.Name = "user";
            this.user.ReadOnly = true;
            // 
            // comp
            // 
            this.comp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.comp.DataPropertyName = "dgv_comp";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.comp.DefaultCellStyle = dataGridViewCellStyle2;
            this.comp.HeaderText = "Компьютер";
            this.comp.Name = "comp";
            this.comp.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Отдел";
            // 
            // UserDept
            // 
            this.UserDept.FormattingEnabled = true;
            this.UserDept.Location = new System.Drawing.Point(430, 32);
            this.UserDept.Name = "UserDept";
            this.UserDept.Size = new System.Drawing.Size(148, 21);
            this.UserDept.TabIndex = 1;
            this.UserDept.SelectedIndexChanged += new System.EventHandler(this.UserDept_SelectedIndexChanged);
            // 
            // dgv_engDept
            // 
            this.dgv_engDept.AllowUserToAddRows = false;
            this.dgv_engDept.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_engDept.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_engDept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_engDept.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_id_EDId,
            this.dgv__EDName,
            this.dgv_Head,
            this.dgv_btn_save});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_engDept.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_engDept.Location = new System.Drawing.Point(13, 296);
            this.dgv_engDept.Name = "dgv_engDept";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_engDept.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_engDept.RowHeadersVisible = false;
            this.dgv_engDept.Size = new System.Drawing.Size(293, 155);
            this.dgv_engDept.TabIndex = 38;
            this.dgv_engDept.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_engDept_CellContentClick);
            this.dgv_engDept.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_engDept_CellValueChanged);
            this.dgv_engDept.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_engDept_DataError);
            // 
            // dgv_id_EDId
            // 
            this.dgv_id_EDId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_id_EDId.DataPropertyName = "EDId";
            this.dgv_id_EDId.HeaderText = "id";
            this.dgv_id_EDId.Name = "dgv_id_EDId";
            this.dgv_id_EDId.Visible = false;
            // 
            // dgv__EDName
            // 
            this.dgv__EDName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv__EDName.DataPropertyName = "EDName";
            dataGridViewCellStyle4.Format = "d";
            this.dgv__EDName.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv__EDName.HeaderText = "Отдел";
            this.dgv__EDName.Name = "dgv__EDName";
            this.dgv__EDName.ToolTipText = "(требуется)";
            // 
            // dgv_Head
            // 
            this.dgv_Head.DataPropertyName = "Head";
            this.dgv_Head.HeaderText = "Руководитель";
            this.dgv_Head.Name = "dgv_Head";
            this.dgv_Head.ReadOnly = true;
            // 
            // dgv_btn_save
            // 
            this.dgv_btn_save.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_btn_save.HeaderText = "[ok]";
            this.dgv_btn_save.Image = global::SmuOk.Properties.Resources.dot;
            this.dgv_btn_save.MinimumWidth = 28;
            this.dgv_btn_save.Name = "dgv_btn_save";
            this.dgv_btn_save.Width = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Отделы";
            // 
            // btnDptAdd
            // 
            this.btnDptAdd.AutoSize = true;
            this.btnDptAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDptAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDptAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDptAdd.ForeColor = System.Drawing.Color.Green;
            this.btnDptAdd.Location = new System.Drawing.Point(252, 280);
            this.btnDptAdd.Name = "btnDptAdd";
            this.btnDptAdd.Size = new System.Drawing.Size(54, 13);
            this.btnDptAdd.TabIndex = 39;
            this.btnDptAdd.Text = "добавить";
            this.btnDptAdd.Click += new System.EventHandler(this.btnDptAdd_Click);
            // 
            // grUser
            // 
            this.grUser.Controls.Add(this.UserIsDeptHead);
            this.grUser.Controls.Add(this.UserO);
            this.grUser.Controls.Add(this.UserI);
            this.grUser.Controls.Add(this.UserF);
            this.grUser.Controls.Add(this.lstExec);
            this.grUser.Controls.Add(this.label4);
            this.grUser.Controls.Add(this.label10);
            this.grUser.Controls.Add(this.label9);
            this.grUser.Controls.Add(this.label8);
            this.grUser.Controls.Add(this.lstRole);
            this.grUser.Controls.Add(this.label6);
            this.grUser.Controls.Add(this.label1);
            this.grUser.Controls.Add(this.UserDept);
            this.grUser.Controls.Add(this.lstScreenshot);
            this.grUser.Controls.Add(this.label5);
            this.grUser.Controls.Add(this.btnShowScreens);
            this.grUser.Location = new System.Drawing.Point(8, 5);
            this.grUser.Name = "grUser";
            this.grUser.Size = new System.Drawing.Size(621, 247);
            this.grUser.TabIndex = 40;
            this.grUser.TabStop = false;
            this.grUser.Text = "Пользователь";
            // 
            // UserIsDeptHead
            // 
            this.UserIsDeptHead.AutoSize = true;
            this.UserIsDeptHead.Location = new System.Drawing.Point(438, 58);
            this.UserIsDeptHead.Name = "UserIsDeptHead";
            this.UserIsDeptHead.Size = new System.Drawing.Size(84, 17);
            this.UserIsDeptHead.TabIndex = 36;
            this.UserIsDeptHead.Text = "нач. отдела";
            this.UserIsDeptHead.UseVisualStyleBackColor = true;
            this.UserIsDeptHead.CheckedChanged += new System.EventHandler(this.UserIsDeptHead_CheckedChanged);
            // 
            // UserO
            // 
            this.UserO.Location = new System.Drawing.Point(110, 85);
            this.UserO.Name = "UserO";
            this.UserO.Size = new System.Drawing.Size(225, 20);
            this.UserO.TabIndex = 7;
            this.UserO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserFIO_KeyDown);
            // 
            // UserI
            // 
            this.UserI.Location = new System.Drawing.Point(110, 59);
            this.UserI.Name = "UserI";
            this.UserI.Size = new System.Drawing.Size(225, 20);
            this.UserI.TabIndex = 7;
            this.UserI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserFIO_KeyDown);
            // 
            // UserF
            // 
            this.UserF.Location = new System.Drawing.Point(110, 33);
            this.UserF.Name = "UserF";
            this.UserF.Size = new System.Drawing.Size(225, 20);
            this.UserF.TabIndex = 7;
            this.UserF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserFIO_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Отчество";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Имя";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Фамилия";
            // 
            // btnShowScreens
            // 
            this.btnShowScreens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowScreens.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnShowScreens.FlatAppearance.BorderSize = 0;
            this.btnShowScreens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowScreens.ForeColor = System.Drawing.Color.Black;
            this.btnShowScreens.Image = global::SmuOk.Properties.Resources.shared;
            this.btnShowScreens.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShowScreens.Location = new System.Drawing.Point(580, 85);
            this.btnShowScreens.Name = "btnShowScreens";
            this.btnShowScreens.Size = new System.Drawing.Size(25, 23);
            this.btnShowScreens.TabIndex = 35;
            this.btnShowScreens.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowScreens.UseVisualStyleBackColor = true;
            this.btnShowScreens.Click += new System.EventHandler(this.btnShowScreens_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "[ok]";
            this.dataGridViewImageColumn1.Image = global::SmuOk.Properties.Resources.dot;
            this.dataGridViewImageColumn1.MinimumWidth = 28;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 28;
            // 
            // btnScShTo
            // 
            this.btnScShTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScShTo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnScShTo.FlatAppearance.BorderSize = 0;
            this.btnScShTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScShTo.ForeColor = System.Drawing.Color.Black;
            this.btnScShTo.Image = global::SmuOk.Properties.Resources.select_folder;
            this.btnScShTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScShTo.Location = new System.Drawing.Point(433, 466);
            this.btnScShTo.Name = "btnScShTo";
            this.btnScShTo.Size = new System.Drawing.Size(196, 23);
            this.btnScShTo.TabIndex = 35;
            this.btnScShTo.Text = "Выбрать папку для скриншотов";
            this.btnScShTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScShTo.UseVisualStyleBackColor = true;
            this.btnScShTo.Visible = false;
            this.btnScShTo.Click += new System.EventHandler(this.btnScShTo_Click);
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtNameFilter.Location = new System.Drawing.Point(350, 3);
            this.txtNameFilter.Margin = new System.Windows.Forms.Padding(0);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(274, 20);
            this.txtNameFilter.TabIndex = 41;
            this.txtNameFilter.Tag = "фамилия / имя / отчество / логин / отдел";
            this.txtNameFilter.Enter += new System.EventHandler(this.txtNameFilter_Enter);
            this.txtNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNameFilter_KeyUp);
            this.txtNameFilter.Leave += new System.EventHandler(this.txtNameFilter_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(332, 309);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 42;
            this.label11.Text = "ID шифра";
            // 
            // SpecId
            // 
            this.SpecId.Location = new System.Drawing.Point(393, 306);
            this.SpecId.Name = "SpecId";
            this.SpecId.Size = new System.Drawing.Size(157, 20);
            this.SpecId.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Удалить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(359, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Удаление шифра";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label13.Location = new System.Drawing.Point(323, 330);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(318, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Для удаления нескольких шифров - писать ID через запятую";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label14.Location = new System.Drawing.Point(10, 521);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(308, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "Для создания нового исполнителя выберите new_Executor";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label15.Location = new System.Drawing.Point(10, 537);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(209, 13);
            this.label15.TabIndex = 46;
            this.label15.Text = "и введите наименование в поле справа";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FindSpec_btn);
            this.groupBox3.Controls.Add(this.SpecID_txtBox);
            this.groupBox3.Controls.Add(this.dgvBlockedSpecs);
            this.groupBox3.Location = new System.Drawing.Point(1141, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(521, 777);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Блокировка шифров";
            // 
            // dgvBlockedSpecs
            // 
            this.dgvBlockedSpecs.AllowUserToAddRows = false;
            this.dgvBlockedSpecs.AllowUserToDeleteRows = false;
            this.dgvBlockedSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBlockedSpecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBlockedSpecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SId,
            this.dgv_SVName,
            this.dgv_SState,
            this.dgv_Block,
            this.dgv_Unblock});
            this.dgvBlockedSpecs.Location = new System.Drawing.Point(6, 55);
            this.dgvBlockedSpecs.Name = "dgvBlockedSpecs";
            this.dgvBlockedSpecs.ReadOnly = true;
            this.dgvBlockedSpecs.RowHeadersVisible = false;
            this.dgvBlockedSpecs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBlockedSpecs.Size = new System.Drawing.Size(509, 716);
            this.dgvBlockedSpecs.TabIndex = 0;
            this.dgvBlockedSpecs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBlockedSpecs_CellContentClick);
            // 
            // FindSpec_btn
            // 
            this.FindSpec_btn.Location = new System.Drawing.Point(149, 27);
            this.FindSpec_btn.Name = "FindSpec_btn";
            this.FindSpec_btn.Size = new System.Drawing.Size(75, 23);
            this.FindSpec_btn.TabIndex = 8;
            this.FindSpec_btn.Text = "Найти";
            this.FindSpec_btn.UseVisualStyleBackColor = true;
            this.FindSpec_btn.Click += new System.EventHandler(this.FindSpec_btn_Click);
            // 
            // SpecID_txtBox
            // 
            this.SpecID_txtBox.Location = new System.Drawing.Point(6, 29);
            this.SpecID_txtBox.Name = "SpecID_txtBox";
            this.SpecID_txtBox.Size = new System.Drawing.Size(134, 20);
            this.SpecID_txtBox.TabIndex = 7;
            // 
            // dgv_SId
            // 
            this.dgv_SId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_SId.DataPropertyName = "SId";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgv_SId.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_SId.FillWeight = 36.02674F;
            this.dgv_SId.HeaderText = "ID шифра";
            this.dgv_SId.Name = "dgv_SId";
            this.dgv_SId.ReadOnly = true;
            this.dgv_SId.Width = 50;
            // 
            // dgv_SVName
            // 
            this.dgv_SVName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_SVName.DataPropertyName = "SVName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgv_SVName.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_SVName.FillWeight = 8.406239F;
            this.dgv_SVName.HeaderText = "Наименование";
            this.dgv_SVName.Name = "dgv_SVName";
            this.dgv_SVName.ReadOnly = true;
            // 
            // dgv_SState
            // 
            this.dgv_SState.DataPropertyName = "SState";
            this.dgv_SState.HeaderText = "Статус";
            this.dgv_SState.Name = "dgv_SState";
            this.dgv_SState.ReadOnly = true;
            // 
            // dgv_Block
            // 
            this.dgv_Block.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgv_Block.DataPropertyName = "Block";
            this.dgv_Block.FillWeight = 70F;
            this.dgv_Block.HeaderText = "Заблокировать";
            this.dgv_Block.Name = "dgv_Block";
            this.dgv_Block.ReadOnly = true;
            this.dgv_Block.Width = 91;
            // 
            // dgv_Unblock
            // 
            this.dgv_Unblock.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dgv_Unblock.DataPropertyName = "Unblock";
            this.dgv_Unblock.FillWeight = 70F;
            this.dgv_Unblock.HeaderText = "Разблокировать";
            this.dgv_Unblock.Name = "dgv_Unblock";
            this.dgv_Unblock.ReadOnly = true;
            this.dgv_Unblock.Width = 97;
            // 
            // Adm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SpecId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtNameFilter);
            this.Controls.Add(this.btnDptAdd);
            this.Controls.Add(this.dgv_engDept);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnScShTo);
            this.Controls.Add(this.btnCountData);
            this.Controls.Add(this.btnAllData);
            this.Controls.Add(this.gbIndexFolders);
            this.Controls.Add(this.lstUser);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.grUser);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Adm";
            this.Size = new System.Drawing.Size(1683, 785);
            this.Load += new System.EventHandler(this.Adm_Load);
            this.gbIndexFolders.ResumeLayout(false);
            this.gbIndexFolders.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_engDept)).EndInit();
            this.grUser.ResumeLayout(false);
            this.grUser.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlockedSpecs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.ComboBox lstUser;
    private System.Windows.Forms.CheckedListBox lstRole;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnIndexFolders;
    private System.Windows.Forms.GroupBox gbIndexFolders;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtFolderSearch;
    private System.Windows.Forms.ListBox lstFolder;
    private System.Windows.Forms.Button btnGetFolders;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnAllData;
    private System.Windows.Forms.Button btnCountData;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckedListBox lstExec;
    private System.Windows.Forms.ComboBox lstScreenshot;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnScShTo;
    private System.Windows.Forms.Button btnShowScreens;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtExecutor;
    private System.Windows.Forms.ComboBox lstExecutor;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.DataGridView dgvActiveUsers;
    private System.Windows.Forms.Label btnVerAdd;
    private System.Windows.Forms.DataGridViewTextBoxColumn user;
    private System.Windows.Forms.DataGridViewTextBoxColumn comp;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox UserDept;
    private System.Windows.Forms.DataGridView dgv_engDept;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label btnDptAdd;
    private System.Windows.Forms.GroupBox grUser;
    private System.Windows.Forms.TextBox UserO;
    private System.Windows.Forms.TextBox UserI;
    private System.Windows.Forms.TextBox UserF;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
    private System.Windows.Forms.CheckBox UserIsDeptHead;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id_EDId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv__EDName;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Head;
    private System.Windows.Forms.DataGridViewImageColumn dgv_btn_save;
    private System.Windows.Forms.TextBox txtNameFilter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox SpecId;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvBlockedSpecs;
        private System.Windows.Forms.Button FindSpec_btn;
        private System.Windows.Forms.TextBox SpecID_txtBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SState;
        private System.Windows.Forms.DataGridViewButtonColumn dgv_Block;
        private System.Windows.Forms.DataGridViewButtonColumn dgv_Unblock;
    }
}
