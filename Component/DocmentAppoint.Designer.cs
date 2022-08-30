namespace SmuOk.Component
{
  partial class DocumentAppoint
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
      this.txtNameFilter = new System.Windows.Forms.TextBox();
      this.dgvSpec = new System.Windows.Forms.DataGridView();
      this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_STName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_SManagerAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_S_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
      this.lstUserFilter = new System.Windows.Forms.ComboBox();
      this.lstHasFillingFilter = new System.Windows.Forms.ComboBox();
      this.lstTypeFilter = new System.Windows.Forms.ComboBox();
      this.dgvDoc = new System.Windows.Forms.DataGridView();
      this.dgv_DTId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_DTName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.chkAdd = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lstAppointTo = new System.Windows.Forms.ComboBox();
      this.dgvInWork = new System.Windows.Forms.DataGridView();
      this.dgv_Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_Doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_AppointFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_ApppointTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_AppointDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_ReadyDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgv_Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.AppSpecInfo = new System.Windows.Forms.TextBox();
      this.btnSaveDoc = new System.Windows.Forms.Button();
      this.chkReady = new System.Windows.Forms.ComboBox();
      this.chkBy = new System.Windows.Forms.ComboBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.scAppoint = new System.Windows.Forms.SplitContainer();
      this.lstSpecManagerAO = new System.Windows.Forms.ComboBox();
      this.SpecList_ShowFolder = new System.Windows.Forms.CheckBox();
      this.SpecList_ShowManagerAO = new System.Windows.Forms.CheckBox();
      this.SpecList_ShowType = new System.Windows.Forms.CheckBox();
      this.SpecList_ShowID = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvDoc)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvInWork)).BeginInit();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.scAppoint)).BeginInit();
      this.scAppoint.Panel1.SuspendLayout();
      this.scAppoint.Panel2.SuspendLayout();
      this.scAppoint.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtNameFilter
      // 
      this.txtNameFilter.ForeColor = System.Drawing.Color.Gray;
      this.txtNameFilter.Location = new System.Drawing.Point(3, 3);
      this.txtNameFilter.Name = "txtNameFilter";
      this.txtNameFilter.Size = new System.Drawing.Size(151, 20);
      this.txtNameFilter.TabIndex = 28;
      this.txtNameFilter.Tag = "Шифр...";
      this.txtNameFilter.Enter += new System.EventHandler(this.txtNameFilter_Enter);
      this.txtNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNameFilter_KeyUp);
      this.txtNameFilter.Leave += new System.EventHandler(this.txtNameFilter_Leave);
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
      this.dgvSpec.Size = new System.Drawing.Size(313, 506);
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
      // lstUserFilter
      // 
      this.lstUserFilter.FormattingEnabled = true;
      this.lstUserFilter.Location = new System.Drawing.Point(443, 3);
      this.lstUserFilter.Name = "lstUserFilter";
      this.lstUserFilter.Size = new System.Drawing.Size(121, 21);
      this.lstUserFilter.TabIndex = 26;
      this.lstUserFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // lstHasFillingFilter
      // 
      this.lstHasFillingFilter.FormattingEnabled = true;
      this.lstHasFillingFilter.Location = new System.Drawing.Point(316, 3);
      this.lstHasFillingFilter.Name = "lstHasFillingFilter";
      this.lstHasFillingFilter.Size = new System.Drawing.Size(121, 21);
      this.lstHasFillingFilter.TabIndex = 25;
      this.lstHasFillingFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // lstTypeFilter
      // 
      this.lstTypeFilter.FormattingEnabled = true;
      this.lstTypeFilter.Location = new System.Drawing.Point(160, 3);
      this.lstTypeFilter.Name = "lstTypeFilter";
      this.lstTypeFilter.Size = new System.Drawing.Size(150, 21);
      this.lstTypeFilter.TabIndex = 24;
      this.lstTypeFilter.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // dgvDoc
      // 
      this.dgvDoc.AllowUserToAddRows = false;
      this.dgvDoc.AllowUserToDeleteRows = false;
      dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
      this.dgvDoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
      this.dgvDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvDoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.dgvDoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvDoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
      this.dgvDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvDoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_DTId,
            this.dgv_DTName,
            this.chkAdd,
            this.Note});
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvDoc.DefaultCellStyle = dataGridViewCellStyle6;
      this.dgvDoc.Location = new System.Drawing.Point(3, 27);
      this.dgvDoc.Name = "dgvDoc";
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvDoc.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
      this.dgvDoc.RowHeadersVisible = false;
      this.dgvDoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgvDoc.Size = new System.Drawing.Size(394, 413);
      this.dgvDoc.TabIndex = 29;
      this.dgvDoc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoc_CellContentClick);
      // 
      // dgv_DTId
      // 
      this.dgv_DTId.DataPropertyName = "DTId";
      this.dgv_DTId.FillWeight = 35F;
      this.dgv_DTId.HeaderText = "DTId";
      this.dgv_DTId.Name = "dgv_DTId";
      this.dgv_DTId.Visible = false;
      this.dgv_DTId.Width = 35;
      // 
      // dgv_DTName
      // 
      this.dgv_DTName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_DTName.DataPropertyName = "DTName";
      this.dgv_DTName.HeaderText = "Тип документа";
      this.dgv_DTName.Name = "dgv_DTName";
      // 
      // chkAdd
      // 
      this.chkAdd.FillWeight = 32F;
      this.chkAdd.HeaderText = "V";
      this.chkAdd.Name = "chkAdd";
      this.chkAdd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.chkAdd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.chkAdd.Width = 32;
      // 
      // Note
      // 
      this.Note.HeaderText = "Примечание";
      this.Note.Name = "Note";
      // 
      // lstAppointTo
      // 
      this.lstAppointTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstAppointTo.FormattingEnabled = true;
      this.lstAppointTo.Location = new System.Drawing.Point(409, 64);
      this.lstAppointTo.Name = "lstAppointTo";
      this.lstAppointTo.Size = new System.Drawing.Size(314, 21);
      this.lstAppointTo.TabIndex = 26;
      this.lstAppointTo.SelectedIndexChanged += new System.EventHandler(this.lstAppointTo_SelectedIndexChanged);
      // 
      // dgvInWork
      // 
      this.dgvInWork.AllowUserToAddRows = false;
      this.dgvInWork.AllowUserToDeleteRows = false;
      dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
      this.dgvInWork.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
      this.dgvInWork.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvInWork.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.dgvInWork.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvInWork.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
      this.dgvInWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvInWork.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_Spec,
            this.dgv_Doc,
            this.dgv_AppointFrom,
            this.dgv_ApppointTo,
            this.dgv_AppointDT,
            this.dgv_ReadyDT,
            this.dgv_Note});
      dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvInWork.DefaultCellStyle = dataGridViewCellStyle10;
      this.dgvInWork.Location = new System.Drawing.Point(3, 27);
      this.dgvInWork.Name = "dgvInWork";
      dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvInWork.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
      this.dgvInWork.RowHeadersVisible = false;
      this.dgvInWork.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgvInWork.Size = new System.Drawing.Size(447, 442);
      this.dgvInWork.TabIndex = 29;
      // 
      // dgv_Spec
      // 
      this.dgv_Spec.DataPropertyName = "Spec";
      this.dgv_Spec.HeaderText = "Шифр";
      this.dgv_Spec.Name = "dgv_Spec";
      // 
      // dgv_Doc
      // 
      this.dgv_Doc.DataPropertyName = "Doc";
      this.dgv_Doc.HeaderText = "Документ";
      this.dgv_Doc.Name = "dgv_Doc";
      // 
      // dgv_AppointFrom
      // 
      this.dgv_AppointFrom.DataPropertyName = "AppointFrom";
      this.dgv_AppointFrom.HeaderText = "Поставил";
      this.dgv_AppointFrom.Name = "dgv_AppointFrom";
      // 
      // dgv_ApppointTo
      // 
      this.dgv_ApppointTo.DataPropertyName = "AppointTo";
      this.dgv_ApppointTo.HeaderText = "Исполнитель";
      this.dgv_ApppointTo.Name = "dgv_ApppointTo";
      // 
      // dgv_AppointDT
      // 
      this.dgv_AppointDT.DataPropertyName = "AppointDT";
      this.dgv_AppointDT.HeaderText = "Поставлено";
      this.dgv_AppointDT.Name = "dgv_AppointDT";
      // 
      // dgv_ReadyDT
      // 
      this.dgv_ReadyDT.DataPropertyName = "ReadyDT";
      this.dgv_ReadyDT.HeaderText = "Исполнение";
      this.dgv_ReadyDT.Name = "dgv_ReadyDT";
      // 
      // dgv_Note
      // 
      this.dgv_Note.DataPropertyName = "Note";
      this.dgv_Note.HeaderText = "Примечание";
      this.dgv_Note.Name = "dgv_Note";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(323, 67);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 13);
      this.label1.TabIndex = 30;
      this.label1.Text = "Исполнитель:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(167, 13);
      this.label2.TabIndex = 30;
      this.label2.Text = "Назначить ответственнным за:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 6);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(55, 13);
      this.label3.TabIndex = 30;
      this.label3.Text = "В работе:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(323, 39);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(39, 13);
      this.label4.TabIndex = 30;
      this.label4.Text = "Шифр:";
      // 
      // AppSpecInfo
      // 
      this.AppSpecInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AppSpecInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.AppSpecInfo.Location = new System.Drawing.Point(409, 36);
      this.AppSpecInfo.Name = "AppSpecInfo";
      this.AppSpecInfo.Size = new System.Drawing.Size(761, 20);
      this.AppSpecInfo.TabIndex = 31;
      // 
      // btnSaveDoc
      // 
      this.btnSaveDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveDoc.Location = new System.Drawing.Point(325, 446);
      this.btnSaveDoc.Name = "btnSaveDoc";
      this.btnSaveDoc.Size = new System.Drawing.Size(75, 23);
      this.btnSaveDoc.TabIndex = 32;
      this.btnSaveDoc.Text = "Сохранить";
      this.btnSaveDoc.UseVisualStyleBackColor = true;
      this.btnSaveDoc.Click += new System.EventHandler(this.btnSaveDoc_Click);
      // 
      // chkReady
      // 
      this.chkReady.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkReady.FormattingEnabled = true;
      this.chkReady.Location = new System.Drawing.Point(196, 3);
      this.chkReady.Name = "chkReady";
      this.chkReady.Size = new System.Drawing.Size(121, 21);
      this.chkReady.TabIndex = 26;
      this.chkReady.Visible = false;
      this.chkReady.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // chkBy
      // 
      this.chkBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkBy.FormattingEnabled = true;
      this.chkBy.Location = new System.Drawing.Point(332, 3);
      this.chkBy.Name = "chkBy";
      this.chkBy.Size = new System.Drawing.Size(121, 21);
      this.chkBy.TabIndex = 26;
      this.chkBy.Visible = false;
      this.chkBy.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.dgvInWork);
      this.panel1.Controls.Add(this.chkReady);
      this.panel1.Controls.Add(this.chkBy);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(453, 472);
      this.panel1.TabIndex = 34;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.dgvDoc);
      this.panel2.Controls.Add(this.btnSaveDoc);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Margin = new System.Windows.Forms.Padding(0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(400, 472);
      this.panel2.TabIndex = 34;
      // 
      // scAppoint
      // 
      this.scAppoint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.scAppoint.IsSplitterFixed = true;
      this.scAppoint.Location = new System.Drawing.Point(316, 91);
      this.scAppoint.Name = "scAppoint";
      // 
      // scAppoint.Panel1
      // 
      this.scAppoint.Panel1.Controls.Add(this.panel2);
      // 
      // scAppoint.Panel2
      // 
      this.scAppoint.Panel2.Controls.Add(this.panel1);
      this.scAppoint.Size = new System.Drawing.Size(854, 472);
      this.scAppoint.SplitterDistance = 400;
      this.scAppoint.SplitterWidth = 1;
      this.scAppoint.TabIndex = 25;
      this.scAppoint.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scAppoint_SplitterMoved);
      // 
      // lstSpecManagerAO
      // 
      this.lstSpecManagerAO.FormattingEnabled = true;
      this.lstSpecManagerAO.Location = new System.Drawing.Point(570, 3);
      this.lstSpecManagerAO.Name = "lstSpecManagerAO";
      this.lstSpecManagerAO.Size = new System.Drawing.Size(154, 21);
      this.lstSpecManagerAO.TabIndex = 26;
      this.lstSpecManagerAO.SelectedIndexChanged += new System.EventHandler(this.SpecTypeFilter);
      // 
      // SpecList_ShowFolder
      // 
      this.SpecList_ShowFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.SpecList_ShowFolder.AutoSize = true;
      this.SpecList_ShowFolder.Checked = true;
      this.SpecList_ShowFolder.CheckState = System.Windows.Forms.CheckState.Checked;
      this.SpecList_ShowFolder.Location = new System.Drawing.Point(256, 541);
      this.SpecList_ShowFolder.Name = "SpecList_ShowFolder";
      this.SpecList_ShowFolder.Size = new System.Drawing.Size(58, 17);
      this.SpecList_ShowFolder.TabIndex = 36;
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
      this.SpecList_ShowManagerAO.Location = new System.Drawing.Point(99, 541);
      this.SpecList_ShowManagerAO.Name = "SpecList_ShowManagerAO";
      this.SpecList_ShowManagerAO.Size = new System.Drawing.Size(123, 17);
      this.SpecList_ShowManagerAO.TabIndex = 37;
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
      this.SpecList_ShowType.Location = new System.Drawing.Point(48, 541);
      this.SpecList_ShowType.Name = "SpecList_ShowType";
      this.SpecList_ShowType.Size = new System.Drawing.Size(45, 17);
      this.SpecList_ShowType.TabIndex = 38;
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
      this.SpecList_ShowID.Location = new System.Drawing.Point(5, 541);
      this.SpecList_ShowID.Name = "SpecList_ShowID";
      this.SpecList_ShowID.Size = new System.Drawing.Size(37, 17);
      this.SpecList_ShowID.TabIndex = 39;
      this.SpecList_ShowID.Tag = "dgv_SId";
      this.SpecList_ShowID.Text = "ID";
      this.SpecList_ShowID.UseVisualStyleBackColor = true;
      this.SpecList_ShowID.CheckedChanged += new System.EventHandler(this.SpecList_CheckedChanged);
      // 
      // DocumentAppoint
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.SpecList_ShowFolder);
      this.Controls.Add(this.SpecList_ShowManagerAO);
      this.Controls.Add(this.SpecList_ShowType);
      this.Controls.Add(this.SpecList_ShowID);
      this.Controls.Add(this.scAppoint);
      this.Controls.Add(this.AppSpecInfo);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtNameFilter);
      this.Controls.Add(this.dgvSpec);
      this.Controls.Add(this.lstAppointTo);
      this.Controls.Add(this.lstSpecManagerAO);
      this.Controls.Add(this.lstUserFilter);
      this.Controls.Add(this.lstHasFillingFilter);
      this.Controls.Add(this.lstTypeFilter);
      this.Name = "DocumentAppoint";
      this.Size = new System.Drawing.Size(1173, 566);
      this.Load += new System.EventHandler(this.DocmentAppoint_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvSpec)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvDoc)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvInWork)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.scAppoint.Panel1.ResumeLayout(false);
      this.scAppoint.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.scAppoint)).EndInit();
      this.scAppoint.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtNameFilter;
    private System.Windows.Forms.DataGridView dgvSpec;
    private System.Windows.Forms.ComboBox lstUserFilter;
    private System.Windows.Forms.ComboBox lstHasFillingFilter;
    private System.Windows.Forms.ComboBox lstTypeFilter;
    private System.Windows.Forms.DataGridView dgvDoc;
    private System.Windows.Forms.ComboBox lstAppointTo;
    private System.Windows.Forms.DataGridView dgvInWork;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox AppSpecInfo;
    private System.Windows.Forms.Button btnSaveDoc;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_DTId;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_DTName;
    private System.Windows.Forms.DataGridViewCheckBoxColumn chkAdd;
    private System.Windows.Forms.DataGridViewTextBoxColumn Note;
    private System.Windows.Forms.ComboBox chkReady;
    private System.Windows.Forms.ComboBox chkBy;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer scAppoint;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Spec;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Doc;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_AppointFrom;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ApppointTo;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_AppointDT;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ReadyDT;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Note;
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
