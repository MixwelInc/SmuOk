namespace SmuOk.Component
{
  partial class Log
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv = new System.Windows.Forms.DataGridView();
      this.btnFillClear = new System.Windows.Forms.Label();
      this.btnFillAdd = new System.Windows.Forms.Label();
      this.lblTitle = new System.Windows.Forms.Label();
      this.dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Сотредник = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Capt = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // dgv
      // 
      this.dgv.AllowUserToAddRows = false;
      this.dgv.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dt,
            this.Сотредник,
            this.Capt});
      this.dgv.Location = new System.Drawing.Point(0, 21);
      this.dgv.Name = "dgv";
      this.dgv.ReadOnly = true;
      this.dgv.RowHeadersVisible = false;
      this.dgv.RowHeadersWidth = 36;
      this.dgv.RowTemplate.Height = 18;
      this.dgv.Size = new System.Drawing.Size(800, 429);
      this.dgv.TabIndex = 2;
      // 
      // btnFillClear
      // 
      this.btnFillClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFillClear.AutoSize = true;
      this.btnFillClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnFillClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnFillClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnFillClear.ForeColor = System.Drawing.Color.Blue;
      this.btnFillClear.Location = new System.Drawing.Point(621, 5);
      this.btnFillClear.Name = "btnFillClear";
      this.btnFillClear.Size = new System.Drawing.Size(93, 13);
      this.btnFillClear.TabIndex = 5;
      this.btnFillClear.Text = "история объекта";
      this.btnFillClear.Visible = false;
      // 
      // btnFillAdd
      // 
      this.btnFillAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFillAdd.AutoSize = true;
      this.btnFillAdd.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnFillAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.btnFillAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnFillAdd.ForeColor = System.Drawing.Color.Green;
      this.btnFillAdd.Location = new System.Drawing.Point(720, 5);
      this.btnFillAdd.Name = "btnFillAdd";
      this.btnFillAdd.Size = new System.Drawing.Size(77, 13);
      this.btnFillAdd.TabIndex = 6;
      this.btnFillAdd.Text = "мои действия";
      this.btnFillAdd.Visible = false;
      // 
      // lblTitle
      // 
      this.lblTitle.AutoSize = true;
      this.lblTitle.Location = new System.Drawing.Point(2, 5);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(28, 13);
      this.lblTitle.TabIndex = 7;
      this.lblTitle.Text = "<...>";
      // 
      // dt
      // 
      this.dt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.dt.DataPropertyName = "dt";
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      this.dt.DefaultCellStyle = dataGridViewCellStyle2;
      this.dt.HeaderText = "Время";
      this.dt.Name = "dt";
      this.dt.ReadOnly = true;
      this.dt.Width = 65;
      // 
      // Сотредник
      // 
      this.Сотредник.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.Сотредник.DataPropertyName = "usr";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      this.Сотредник.DefaultCellStyle = dataGridViewCellStyle3;
      this.Сотредник.HeaderText = "Сотрудник";
      this.Сотредник.Name = "Сотредник";
      this.Сотредник.ReadOnly = true;
      this.Сотредник.Width = 85;
      // 
      // Capt
      // 
      this.Capt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Capt.DataPropertyName = "caption";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      this.Capt.DefaultCellStyle = dataGridViewCellStyle4;
      this.Capt.HeaderText = "Операция";
      this.Capt.Name = "Capt";
      this.Capt.ReadOnly = true;
      // 
      // Log
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.lblTitle);
      this.Controls.Add(this.btnFillClear);
      this.Controls.Add(this.btnFillAdd);
      this.Controls.Add(this.dgv);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MinimizeBox = false;
      this.Name = "Log";
      this.ShowInTaskbar = false;
      this.Text = "Журнал";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.Log_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv;
    private System.Windows.Forms.Label btnFillClear;
    private System.Windows.Forms.Label btnFillAdd;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.DataGridViewTextBoxColumn dt;
    private System.Windows.Forms.DataGridViewTextBoxColumn Сотредник;
    private System.Windows.Forms.DataGridViewTextBoxColumn Capt;
  }
}