namespace SmuOk.Component
{
  partial class VersionHistory
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionHistory));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv = new System.Windows.Forms.DataGridView();
      this.EVHVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.EVH_img = new System.Windows.Forms.DataGridViewImageColumn();
      this.EVHIsNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.EVHText = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // dgv
      // 
      this.dgv.AllowUserToAddRows = false;
      this.dgv.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EVHVer,
            this.EVH_img,
            this.EVHIsNew,
            this.EVHText});
      this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgv.Location = new System.Drawing.Point(0, 0);
      this.dgv.Name = "dgv";
      this.dgv.ReadOnly = true;
      this.dgv.RowHeadersVisible = false;
      this.dgv.RowHeadersWidth = 36;
      this.dgv.RowTemplate.Height = 18;
      this.dgv.Size = new System.Drawing.Size(800, 450);
      this.dgv.TabIndex = 1;
      this.dgv.RowHeadersWidthChanged += new System.EventHandler(this.dgv_RowHeadersWidthChanged);
      this.dgv.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
      // 
      // EVHVer
      // 
      this.EVHVer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.EVHVer.DataPropertyName = "EVHVer";
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.EVHVer.DefaultCellStyle = dataGridViewCellStyle2;
      this.EVHVer.FillWeight = 30F;
      this.EVHVer.HeaderText = "Вер";
      this.EVHVer.Name = "EVHVer";
      this.EVHVer.ReadOnly = true;
      this.EVHVer.Width = 30;
      // 
      // EVH_img
      // 
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
      dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
      this.EVH_img.DefaultCellStyle = dataGridViewCellStyle3;
      this.EVH_img.FillWeight = 28F;
      this.EVH_img.HeaderText = "*";
      this.EVH_img.Name = "EVH_img";
      this.EVH_img.ReadOnly = true;
      this.EVH_img.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.EVH_img.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.EVH_img.Width = 28;
      // 
      // EVHIsNew
      // 
      this.EVHIsNew.DataPropertyName = "EVHIsNew";
      this.EVHIsNew.HeaderText = "EVHIsNew";
      this.EVHIsNew.Name = "EVHIsNew";
      this.EVHIsNew.ReadOnly = true;
      this.EVHIsNew.Visible = false;
      // 
      // EVHText
      // 
      this.EVHText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.EVHText.DataPropertyName = "EVHText";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.EVHText.DefaultCellStyle = dataGridViewCellStyle4;
      this.EVHText.HeaderText = "Описание";
      this.EVHText.Name = "EVHText";
      this.EVHText.ReadOnly = true;
      // 
      // VersionHistory
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.dgv);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MinimizeBox = false;
      this.Name = "VersionHistory";
      this.ShowInTaskbar = false;
      this.Text = "История версий";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.VersionHistory_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv;
    private System.Windows.Forms.DataGridViewTextBoxColumn EVHVer;
    private System.Windows.Forms.DataGridViewImageColumn EVH_img;
    private System.Windows.Forms.DataGridViewTextBoxColumn EVHIsNew;
    private System.Windows.Forms.DataGridViewTextBoxColumn EVHText;
  }
}