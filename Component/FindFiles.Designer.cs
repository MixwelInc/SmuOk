namespace SmuOk.Component
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.txtFindFiles = new System.Windows.Forms.TextBox();
      this.dgvFiles = new System.Windows.Forms.DataGridView();
      this.dgvPTOPathFile = new System.Windows.Forms.DataGridViewImageColumn();
      this.dgvPTOPathFolder = new System.Windows.Forms.DataGridViewImageColumn();
      this.dgvPTOPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dgvPTOPathCopy = new System.Windows.Forms.DataGridViewImageColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
      this.SuspendLayout();
      // 
      // txtFindFiles
      // 
      this.txtFindFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFindFiles.ForeColor = System.Drawing.Color.Gray;
      this.txtFindFiles.Location = new System.Drawing.Point(3, 3);
      this.txtFindFiles.Name = "txtFindFiles";
      this.txtFindFiles.Size = new System.Drawing.Size(808, 20);
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
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvPTOPathFile,
            this.dgvPTOPathFolder,
            this.dgvPTOPath,
            this.dgvPTOPathCopy});
      this.dgvFiles.Cursor = System.Windows.Forms.Cursors.Default;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvFiles.DefaultCellStyle = dataGridViewCellStyle3;
      this.dgvFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.dgvFiles.Location = new System.Drawing.Point(3, 29);
      this.dgvFiles.MultiSelect = false;
      this.dgvFiles.Name = "dgvFiles";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.dgvFiles.RowHeadersVisible = false;
      this.dgvFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvFiles.Size = new System.Drawing.Size(808, 717);
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
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvPTOPath.DefaultCellStyle = dataGridViewCellStyle2;
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
      // FindFiles
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtFindFiles);
      this.Controls.Add(this.dgvFiles);
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "FindFiles";
      this.Size = new System.Drawing.Size(814, 749);
      this.Load += new System.EventHandler(this.FindFiles_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
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
  }
}
