using System;
using System.Windows.Forms;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;
using static SmuOk.Common.MyConst;


namespace SmuOk
{
  public partial class Report : UserControl
  {
    public Report()
    {
      InitializeComponent();
    }

    private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
    }

    private void Report_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      string sDomain = IsDebugComputer() ? "http://localhost/?uid="+uid : "http://SERVER-SMUOK/?uid="+uid;

      /*if (!IsDebugComputer())
      {
        RegistryKey rk_base, rk_parent, rk;

        rk_base = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap");
        rk_parent = rk_base.OpenSubKey("Domains");
        if (rk_parent == null) rk_parent = rk_base.CreateSubKey("Domains");
        rk = rk_parent.OpenSubKey(sDomain);
        if (rk == null) rk = rk_parent.CreateSubKey(sDomain);
        rk.SetValue(" * ", 2, RegistryValueKind.DWord);

        rk_base = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap");
        rk_parent = rk_base.OpenSubKey("ExtDomains");
        if (rk_parent == null) rk_parent = rk_base.CreateSubKey("ExtDomains");
        rk = rk_parent.OpenSubKey(sDomain);
        if (rk == null) rk = rk_parent.CreateSubKey(sDomain);
        rk.SetValue(" * ", 2, RegistryValueKind.DWord);
      }*/
      wb.ObjectForScripting = new MyScriptInterface();
      wb.Url = new System.Uri(sDomain);
      //wb.Version.Major;
    }

    private void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
    {
      ;
    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class MyScriptInterface
    {
      public void callMe()
      {
        string q = "exec uspReport_SpecAllWeb";
        MyExcelIns(q);
        //,null, true, new decimal[] { 7, 7, 17, 17, 5, 5, 80, 80, 11, 15, 6, 12, 9.43M }, new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11 }
      }
      public void spec_done(string s)
      {
        string q = "exec uspReport_SpecDoneWeb " + s;
        MyExcel(q, null, true, new decimal[] { 15, 7, 80, 40, 7, 7, 14, 14, 14, 14, 14, 14, 14 }, new int[] { },false);
      }

      public void pto_done(string s)
      {
        string q = "exec uspReport_SpecDoneWeb " + s;
        MyExcel(q, null, true, new decimal[] { 15, 7, 80, 40, 7, 7, 14, 14, 14, 14, 14, 14, 14 }, new int[] { }, false);
      }
    }
  }
}
