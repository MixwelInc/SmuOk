using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SmuOk.Common.MyReport;

namespace SmuOk.Common
{
  public partial class MultilineInput : Form
  {
    public MultilineInput()
    {
      InitializeComponent();
    }

    private void MultilineInput_Load(object sender, EventArgs e)
    {
      
    }

    private void btnReportF7_Click(object sender, EventArgs e)
    {
      string s = txtSIds.Text;

      StringBuilder sb = new StringBuilder(s.Length);
      foreach (char ch in s)
      {
        if (ch >= '0' && ch <= '9')
        {
          sb.Append(ch);
        }
        else
        {
          sb.Append(' ');
        }
      }
      s = sb.ToString();
      while (s.Contains("  ")) s = s.Replace("  ", " ");
      s = s.Trim();
      s = s.Replace(' ', ',');

      MyExcelCustomReport_F7(s);
      return;
    }
  }
}
