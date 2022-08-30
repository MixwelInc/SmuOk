namespace SmuOk
{
  partial class Report
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
      this.wb = new System.Windows.Forms.WebBrowser();
      this.SuspendLayout();
      // 
      // wb
      // 
      this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
      this.wb.Location = new System.Drawing.Point(0, 0);
      this.wb.Margin = new System.Windows.Forms.Padding(0);
      this.wb.MinimumSize = new System.Drawing.Size(20, 20);
      this.wb.Name = "wb";
      this.wb.Size = new System.Drawing.Size(769, 712);
      this.wb.TabIndex = 0;
      this.wb.Url = new System.Uri("http://server-smuok/", System.UriKind.Absolute);
      this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
      this.wb.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wb_Navigating);
      // 
      // Report
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.wb);
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "Report";
      this.Size = new System.Drawing.Size(769, 712);
      this.Load += new System.EventHandler(this.Report_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.WebBrowser wb;
  }
}
