using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;

namespace SmuOk
{
  public partial class Main : Form
  {
    public Main()
    {
      InitializeComponent();
    }

    int screenshotstimeout = 0;
    SmuOk.Component.VersionHistory vh = new Component.VersionHistory();
    DateTime dtRecordTo = DateTime.MinValue;
    //bool IsArchive = false;

    private void Main_Load(object sender = null, EventArgs e = null)
    {
      timer1.Stop();
      this.WindowState = FormWindowState.Maximized;
      //пишем в заголовок окна версию и пользователя

      long lVer = 0;
      string sVer;
      string sTest = "";
      try
      {
        sVer = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
        this.Text = sVer;
      }
      catch (Exception)
      {
        sVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        this.Text = "test, v." + sVer;
        sTest = "test";
      }
      string[] ss = sVer.Split('.');
      for (int i = 0; i<ss.Length; i++)
      {
       lVer += Convert.ToInt64(Math.Pow(1000, ss.Length - i - 1) * int.Parse(ss[i]));
      }
      /*
        0 -> (9) 1 000 000 000
        1 -> (6) 1 000 000
        2 -> (3) 1 000
        3 -> 1   1
      */

      uname = Environment.UserDomainName + "\\" + Environment.UserName;
      this.Text += " :: " + uname;

      //if (SmuOk.Common.MyConst.ConStr_IsArchive) this.Text += " (архив)";

      // тестируем подключение к базе

      // проверяем права и запоминаем пользователя
      Object TryUser = MyGetOneValue("Select EUId From [_engUser] where EULogin='" + uname + "'");
      if (TryUser == null)
      {
        MsgBox("У текущего пользователя " + uname + " нет доступа к серверу баз данных.\n\n" +
          "Пожалуйста, обратитесь к системному администрутору.",
          "SmuOk: подключение...",
          MessageBoxIcon.Exclamation);
        Application.Exit();
      }
      uid = (long)TryUser;

      screenshotstimeout = (int)MyGetOneValue("select EUScreenshot from _engUser where euid=" + uid);

      // получаем список форм
      List<String> f = MyGetOneCol("select distinct ERModule from _engRole inner join _engUserRole on EURRole=ERId Where ERModule<>'-' and EURUser=" + uid.ToString());
      // прячем лишние
      TabControl hiddenPages = new TabControl();
      bool bDelMe;
      foreach (TabPage t in tb.TabPages)
      {
        bDelMe = true;
        foreach (string s in f)
        {
          if (t.Name == "tb" + s)
          {
            bDelMe = false;
            break;
          }
        }
        if (bDelMe)
        {
          hiddenPages.TabPages.Add(t);
          tb.TabPages.Remove(t);
        }
      }
      //TechLog("Login.");
      if (sender == null)
      {
        lblArchive.ForeColor = Common.MyConst.ConStr_IsArchive? Color.Red : Color.Gray;
        foreach (TabPage t in tb.TabPages)
        {
          ((SmuOk.Common.MyComponent)t.Controls[0]).LoadMe();
        }
      }
      MyLog(uid, "Main", 1, lVer, 0, "", sTest);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      //return;
      //screenshotstimeout = (int)MyGetOneValue("select EUScreenshot from _engUser where euid=" + uid);
      if (dtRecordTo!=DateTime.MinValue)
      {
        timer1.Interval = 5000;
        string s = MyGetOneValue("select EOValue from _engOptions where EOName='ScreensFolder'").ToString();
        if (!s.EndsWith("\\")) s += "\\";

        string s_domain_user = Environment.UserDomainName + "\\" + Environment.UserName;
        s_domain_user = s_domain_user.Replace('/', '_').Replace('\\', '_');

        s += s_domain_user + "\\";
        if (!System.IO.Directory.Exists(s)) System.IO.Directory.CreateDirectory(s);
        MyMakeScreenshot(s + DateTime.Now.ToString("yyyy.MM.dd hh.mm.ss") + "_by_user_demand.png");
        TimeSpan span = (dtRecordTo - DateTime.Now);
        //lblRecord.Font.Italic = true;

        lblRecord.Text = "Запись: "+span.Minutes.ToString() + ":" + span.Seconds.ToString();
        if (dtRecordTo < DateTime.Now)
        {
          dtRecordTo = DateTime.MinValue;
          lblRec.Visible = false;
          lblRecord.Enabled = true;
          lblRecord.Text = "Запись";
          Font fi = new Font(lblRecord.Font.Name, lblRecord.Font.Size, FontStyle.Regular);
          lblRecord.Font = fi;
          lblRecord.Font = fi;
        }
      }
      /*
       * else if (screenshotstimeout==0)
      {
        timer1.Interval = 30000;
      }
      else
      {
        string s = MyGetOneValue("select EOValue from _engOptions where EOName='ScreensFolder'").ToString();
        if (!s.EndsWith("\\")) s += "\\";
        string s_domain_user = Environment.UserDomainName + "\\" + Environment.UserName;
        s_domain_user = s_domain_user.Replace('/', '_').Replace('\\', '_');
        s += s_domain_user + "\\";
        if (!System.IO.Directory.Exists(s)) System.IO.Directory.CreateDirectory(s);
        MyMakeScreenshot(s + 
          DateTime.Now.ToString("yyyy.MM.dd hh.mm.ss") + ".png");
        timer1.Interval = screenshotstimeout;
      }
      */
    }

    private void lblVH_Click(object sender, EventArgs e)
    {
      vh.ShowDialog();
    }

    private void lblRecord_Click(object sender, EventArgs e)
    {
      if(MsgBox("Включить запись видео с экрана на следующие 120 секунд для автоматической отправки системному администратору?","",
        MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        lblRec.Visible = true;
        lblRecord.Enabled = false;
        Font fi = new Font(lblRecord.Font.Name, lblRecord.Font.Size, FontStyle.Italic);
        lblRecord.Font = fi;
        dtRecordTo = DateTime.Now.AddSeconds(120);
        MyLog(uid, "Main", 8, 0, 0);
        timer1.Start();
        timer1_Tick(null, null);
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
      long l = MyLog(uid, "Main", 2, 0, 0);
      //e.Cancel = false;
    }

    private void lblArchive_Click(object sender, EventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      SmuOk.Common.MyConst.ConStr_IsArchive = !SmuOk.Common.MyConst.ConStr_IsArchive;
      Main_Load();
      Cursor = Cursors.Default;
    }
  }
}
