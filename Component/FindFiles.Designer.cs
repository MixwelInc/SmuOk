﻿namespace SmuOk.Component
{
  partial class FindFiles
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtFindFiles = new System.Windows.Forms.TextBox();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.dgvPTOPathFile = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvPTOPathFolder = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvPTOPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPTOPathCopy = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvPIDSearch = new System.Windows.Forms.DataGridView();
            this.SearchByPID_btn = new System.Windows.Forms.Button();
            this.txtPIDFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_SId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SVName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_SFUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_M15Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPIDSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFindFiles
            // 
            this.txtFindFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFindFiles.ForeColor = System.Drawing.Color.Gray;
            this.txtFindFiles.Location = new System.Drawing.Point(3, 3);
            this.txtFindFiles.Name = "txtFindFiles";
            this.txtFindFiles.Size = new System.Drawing.Size(807, 20);
            this.txtFindFiles.TabIndex = 3;
            this.txtFindFiles.Tag = "Имя файла или папки (пробел для любых сомволов)";
            this.txtFindFiles.Text = "Имя файла или папки (пробел для любых сомволов)";
            this.txtFindFiles.Enter += new System.EventHandler(this.txtFindFiles_Enter);
            this.txtFindFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFindFiles_KeyUp);
            this.txtFindFiles.Leave += new System.EventHandler(this.txtFindFiles_Leave);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToResizeRows = false;
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiles.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvPTOPathFile,
            this.dgvPTOPathFolder,
            this.dgvPTOPath,
            this.dgvPTOPathCopy});
            this.dgvFiles.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFiles.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFiles.Location = new System.Drawing.Point(3, 29);
            this.dgvFiles.MultiSelect = false;
            this.dgvFiles.Name = "dgvFiles";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.Size = new System.Drawing.Size(807, 451);
            this.dgvFiles.TabIndex = 2;
            this.dgvFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFiles_CellContentClick);
            this.dgvFiles.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFiles_CellMouseMove);
            // 
            // dgvPTOPathFile
            // 
            this.dgvPTOPathFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvPTOPathFile.FillWeight = 24F;
            this.dgvPTOPathFile.HeaderText = "2";
            this.dgvPTOPathFile.Image = global::SmuOk.Properties.Resources.document;
            this.dgvPTOPathFile.MinimumWidth = 28;
            this.dgvPTOPathFile.Name = "dgvPTOPathFile";
            this.dgvPTOPathFile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPTOPathFile.Width = 28;
            // 
            // dgvPTOPathFolder
            // 
            this.dgvPTOPathFolder.FillWeight = 24F;
            this.dgvPTOPathFolder.HeaderText = "0";
            this.dgvPTOPathFolder.MinimumWidth = 28;
            this.dgvPTOPathFolder.Name = "dgvPTOPathFolder";
            this.dgvPTOPathFolder.Width = 28;
            // 
            // dgvPTOPath
            // 
            this.dgvPTOPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPTOPath.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPTOPath.HeaderText = "Путь";
            this.dgvPTOPath.Name = "dgvPTOPath";
            this.dgvPTOPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvPTOPathCopy
            // 
            this.dgvPTOPathCopy.FillWeight = 24F;
            this.dgvPTOPathCopy.HeaderText = "2";
            this.dgvPTOPathCopy.Image = global::SmuOk.Properties.Resources.copy;
            this.dgvPTOPathCopy.MinimumWidth = 24;
            this.dgvPTOPathCopy.Name = "dgvPTOPathCopy";
            this.dgvPTOPathCopy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPTOPathCopy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvPTOPathCopy.Width = 24;
            // 
            // dgvPIDSearch
            // 
            this.dgvPIDSearch.AllowUserToAddRows = false;
            this.dgvPIDSearch.AllowUserToDeleteRows = false;
            this.dgvPIDSearch.AllowUserToResizeRows = false;
            this.dgvPIDSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPIDSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPIDSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvPIDSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPIDSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_SId,
            this.dgv_SVName,
            this.dgv_SFName,
            this.dgv_SFMark,
            this.dgv_SFUnit,
            this.dgv_M15Num,
            this.dgv_M15Date,
            this.dgv_M15Price});
            this.dgvPIDSearch.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPIDSearch.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvPIDSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPIDSearch.Location = new System.Drawing.Point(3, 547);
            this.dgvPIDSearch.MultiSelect = false;
            this.dgvPIDSearch.Name = "dgvPIDSearch";
            this.dgvPIDSearch.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPIDSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvPIDSearch.RowHeadersVisible = false;
            this.dgvPIDSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPIDSearch.Size = new System.Drawing.Size(807, 199);
            this.dgvPIDSearch.TabIndex = 4;
            // 
            // SearchByPID_btn
            // 
            this.SearchByPID_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchByPID_btn.BackColor = System.Drawing.SystemColors.Window;
            this.SearchByPID_btn.Location = new System.Drawing.Point(154, 524);
            this.SearchByPID_btn.Name = "SearchByPID_btn";
            this.SearchByPID_btn.Size = new System.Drawing.Size(150, 23);
            this.SearchByPID_btn.TabIndex = 59;
            this.SearchByPID_btn.Text = "Поиск";
            this.SearchByPID_btn.UseVisualStyleBackColor = false;
            this.SearchByPID_btn.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtPIDFilter
            // 
            this.txtPIDFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPIDFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtPIDFilter.Location = new System.Drawing.Point(0, 524);
            this.txtPIDFilter.Margin = new System.Windows.Forms.Padding(0);
            this.txtPIDFilter.Name = "txtPIDFilter";
            this.txtPIDFilter.Size = new System.Drawing.Size(151, 20);
            this.txtPIDFilter.TabIndex = 58;
            this.txtPIDFilter.Tag = "PID";
            this.txtPIDFilter.Enter += new System.EventHandler(this.txtPIDFilter_Enter);
            this.txtPIDFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPIDFilter_KeyUp);
            this.txtPIDFilter.Leave += new System.EventHandler(this.txtPIDFilter_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 500);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "Поиск позиции по PID";
            // 
            // dgv_SId
            // 
            this.dgv_SId.DataPropertyName = "SId";
            this.dgv_SId.HeaderText = "ID шифра";
            this.dgv_SId.Name = "dgv_SId";
            this.dgv_SId.ReadOnly = true;
            // 
            // dgv_SVName
            // 
            this.dgv_SVName.DataPropertyName = "SVName";
            this.dgv_SVName.HeaderText = "Шифр";
            this.dgv_SVName.Name = "dgv_SVName";
            this.dgv_SVName.ReadOnly = true;
            // 
            // dgv_SFName
            // 
            this.dgv_SFName.DataPropertyName = "SFName";
            this.dgv_SFName.HeaderText = "Наименование по шифру";
            this.dgv_SFName.Name = "dgv_SFName";
            this.dgv_SFName.ReadOnly = true;
            // 
            // dgv_SFMark
            // 
            this.dgv_SFMark.DataPropertyName = "SFMark";
            this.dgv_SFMark.HeaderText = "Тех. характеристики";
            this.dgv_SFMark.Name = "dgv_SFMark";
            this.dgv_SFMark.ReadOnly = true;
            // 
            // dgv_SFUnit
            // 
            this.dgv_SFUnit.DataPropertyName = "SFUnit";
            this.dgv_SFUnit.HeaderText = "Ед. изм.";
            this.dgv_SFUnit.Name = "dgv_SFUnit";
            this.dgv_SFUnit.ReadOnly = true;
            // 
            // dgv_M15Num
            // 
            this.dgv_M15Num.DataPropertyName = "M15Num";
            this.dgv_M15Num.HeaderText = "№ М15";
            this.dgv_M15Num.Name = "dgv_M15Num";
            this.dgv_M15Num.ReadOnly = true;
            // 
            // dgv_M15Date
            // 
            this.dgv_M15Date.DataPropertyName = "M15Date";
            this.dgv_M15Date.HeaderText = "Дата М15";
            this.dgv_M15Date.Name = "dgv_M15Date";
            this.dgv_M15Date.ReadOnly = true;
            // 
            // dgv_M15Price
            // 
            this.dgv_M15Price.DataPropertyName = "M15Price";
            this.dgv_M15Price.HeaderText = "Цена по М15";
            this.dgv_M15Price.Name = "dgv_M15Price";
            this.dgv_M15Price.ReadOnly = true;
            // 
            // FindFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchByPID_btn);
            this.Controls.Add(this.txtPIDFilter);
            this.Controls.Add(this.dgvPIDSearch);
            this.Controls.Add(this.txtFindFiles);
            this.Controls.Add(this.dgvFiles);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FindFiles";
            this.Size = new System.Drawing.Size(813, 749);
            this.Load += new System.EventHandler(this.FindFiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPIDSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtFindFiles;
    private System.Windows.Forms.DataGridView dgvFiles;
    private System.Windows.Forms.DataGridViewImageColumn dgvPTOPathFile;
    private System.Windows.Forms.DataGridViewImageColumn dgvPTOPathFolder;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgvPTOPath;
    private System.Windows.Forms.DataGridViewImageColumn dgvPTOPathCopy;
        private System.Windows.Forms.DataGridView dgvPIDSearch;
        private System.Windows.Forms.Button SearchByPID_btn;
        private System.Windows.Forms.TextBox txtPIDFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SVName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_SFUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_M15Price;
    }
}
