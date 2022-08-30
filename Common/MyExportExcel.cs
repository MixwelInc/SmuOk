using System.Collections.Generic;

namespace SmuOk.Common
{
  public class MyExportExcel
  {
    public string sQuery;
    public string[] ssTitle = null;
    public bool Title2Rows = false;
    public decimal[] colsWidth = null;
    public int[] GrayColIDs = null;
    public bool nowrap = false;
    public List<int> DateValueColIDs = null;
    public string ReportTitle = "";
    public string AfterFormat = "";
    public int[] CenterColIDs = null;
  }
}
