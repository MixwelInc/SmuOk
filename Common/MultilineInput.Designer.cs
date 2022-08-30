namespace SmuOk.Common
{
  partial class MultilineInput
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
      this.txtSIds = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.btnReportF7 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtSIds
      // 
      this.txtSIds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSIds.Location = new System.Drawing.Point(12, 25);
      this.txtSIds.Multiline = true;
      this.txtSIds.Name = "txtSIds";
      this.txtSIds.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtSIds.Size = new System.Drawing.Size(356, 221);
      this.txtSIds.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(104, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Список ID шифров:";
      // 
      // comboBox1
      // 
      this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox1.Enabled = false;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(15, 253);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(353, 21);
      this.comboBox1.TabIndex = 2;
      this.comboBox1.Text = "Форма 7";
      // 
      // btnReportF7
      // 
      this.btnReportF7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnReportF7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnReportF7.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.btnReportF7.FlatAppearance.BorderSize = 0;
      this.btnReportF7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnReportF7.ForeColor = System.Drawing.Color.Blue;
      this.btnReportF7.Image = global::SmuOk.Properties.Resources.report_excel;
      this.btnReportF7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnReportF7.Location = new System.Drawing.Point(226, 281);
      this.btnReportF7.Name = "btnReportF7";
      this.btnReportF7.Size = new System.Drawing.Size(142, 23);
      this.btnReportF7.TabIndex = 33;
      this.btnReportF7.Text = "Сформировать отчет";
      this.btnReportF7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnReportF7.UseVisualStyleBackColor = true;
      this.btnReportF7.Click += new System.EventHandler(this.btnReportF7_Click);
      // 
      // MultilineInput
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(380, 316);
      this.Controls.Add(this.btnReportF7);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtSIds);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "MultilineInput";
      this.Text = "Отчеты по списку шифров";
      this.Load += new System.EventHandler(this.MultilineInput_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtSIds;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Button btnReportF7;
  }
}