namespace SmuOk.Common
{
  partial class MyProgress
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
      this.pb = new System.Windows.Forms.ProgressBar();
      this.lblCaption = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // pb
      // 
      this.pb.Location = new System.Drawing.Point(12, 25);
      this.pb.MarqueeAnimationSpeed = 0;
      this.pb.Maximum = 150;
      this.pb.Name = "pb";
      this.pb.Size = new System.Drawing.Size(381, 12);
      this.pb.TabIndex = 0;
      // 
      // lblCaption
      // 
      this.lblCaption.AutoSize = true;
      this.lblCaption.Location = new System.Drawing.Point(12, 9);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(84, 13);
      this.lblCaption.TabIndex = 1;
      this.lblCaption.Text = "Запрос данных";
      // 
      // MyProgress
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(405, 46);
      this.ControlBox = false;
      this.Controls.Add(this.lblCaption);
      this.Controls.Add(this.pb);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "MyProgress";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Выгрузка...";
      this.Load += new System.EventHandler(this.MyProgress_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.Label lblCaption;
  }
}