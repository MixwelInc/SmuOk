using System;
using System.Windows.Forms;
using static SmuOk.Common.DB;

namespace SmuOk.Component
{
  public partial class VersionHistory : Form
  {
    public VersionHistory()
    {
      InitializeComponent();
    }

    private void VersionHistory_Load(object sender, EventArgs e)
    {
      string q = "select EVHVer,EVHIsNew,EVHText from _engVersionHistory order by EVHVer, EVHIsNew desc;";
      MyFillDgv(dgv, q);
    }

    private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      for (int i = 0; i<e.RowCount; i++)
      {
        int iRowIndex = e.RowIndex+i;
        if (dgv.Rows[iRowIndex].Cells["EVHIsNew"].Value.ToString() == "1")
        {
          dgv.Rows[iRowIndex].Cells["EVH_img"].Value = Properties.Resources.plus_small;
        }
        else
        {
          dgv.Rows[iRowIndex].Cells["EVH_img"].Value = Properties.Resources.exclamation_small;
        }
      }
    }

    private void dgv_RowHeadersWidthChanged(object sender, EventArgs e)
    {

    }
  }
}
