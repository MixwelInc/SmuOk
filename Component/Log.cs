using System;
using System.Windows.Forms;
using static SmuOk.Common.DB;

namespace SmuOk.Component
{
  public partial class Log : Form
  {
    public long uid;
    public long SpecId;

    public Log()
    {
      InitializeComponent();
    }

    private void Log_Load(object sender, EventArgs e)
    {
      string q = "select SVName from vwSpec where SId=" + SpecId;
      string s = (string)MyGetOneValue(q);
      lblTitle.Text = s;
      filldgv();
    }
    private void filldgv()
    {
      string q = "select dt,usr,caption from vwLog where ELSpec=" + SpecId +" order by dt";
      MyFillDgv(dgv, q);
    }

  }
}
