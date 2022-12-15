using System;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.Win32;
using static SmuOk.Common.MyReport;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace SmuOk.Component
{
  public partial class Adm : UserControl
  {
    private bool FormIsUpdating = true;
    private bool UserIsUpdating = true;
    
    public Adm()
    {
      InitializeComponent();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;

      txtNameFilter.Text = txtNameFilter.Tag.ToString();
      fill_users();
      
      MyFillList(lstScreenshot, "select cast (0 as bigint) i, 'не сохр.' txt union " +
          " select 5, '5 сек.' union " +
          " select 30, '30 сек.' union " +
          " select 120, '2 мин.' union " +
          " select 300, '5 мин.'");
      MyFillList(lstExecutor, "select EId,EName from Executor order by case when ESmuDept=0 then 1000 else ESmuDept end,EName");
      UpdateActiveUsers();
      UpdateBlockedSpecs();
      FillDgvDept();
      FormIsUpdating = false;
      lstUser_SelectedIndexChanged();
    }

    private void FillDgvDept()
    {
       MyFillDgv(dgv_engDept, "select _engDept.EDId, EDName, IsNull(UFIO,'') Head from _engDept left join vwUser on EDHead = UId order by EDName;");
    }

    private void UpdateActiveUsers()
    {
      MyFillDgv(dgvActiveUsers, "select hostname dgv_comp, loginame dgv_user from vwActiveUsers");
    }

    private void UpdateBlockedSpecs()
    {
      MyFillDgv(dgvBlockedSpecs, "select vw.SId, vw.SVName, case when s.SState = 1 then 'Заблокирован' else 'Активен' end as SState " +
          " from vwSpec vw inner join Spec s on s.SId = vw.SId ");
    }

    private void Adm_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    private void lstUser_SelectedIndexChanged(object sender=null, EventArgs e=null)
    {
      if (FormIsUpdating) return;
      long user_id = lstUser.GetLstVal();
      if (user_id == 0)
      {
        UserIsUpdating = true;
        lstRole.DataSource = null;
        lstRole.Items.Clear();
        lstExec.DataSource = null;
        lstExec.Items.Clear();
        MyClearForm(this.grUser, "User");
        MyEnableForm(this.grUser, "User", false);
        UserIsUpdating = false;
        return;
      }
      else MyEnableForm(this.grUser, "User",true);

      UserIsUpdating = true;
      MyFillList(lstRole, "select ERId,ERName,case when IsNull(EURRole,0)>0 then 1 else 0 end ch"+
          " from _engRole Left Join(select EURRole from _engUserRole where EURUser = "+ user_id + ")q on ERId = EURRole order by ERName");
      MyFillList(lstExec, "select EId,EName,case when IsNull(UEExec,0)>0 then 1 else 0 end ch" +
          " from Executor Left Join(select UEExec from UserExec where UEUser = " + user_id + ")q on EId = UEExec");
      MyFillList(UserDept, "select 0 EDId,'<не выбран>' EDName union select EDId, EDName from _engDept order by EDName;");
      SmuOk.Common.DB.SelectItemByValue(lstScreenshot, MyGetOneValue("select EUScreenshot/1000 from _engUser where euid=" + user_id));// (MyGetOneValue);
      btnShowScreens.Tag = MyGetOneValue("select EOValue from _engOptions where EOName='ScreensFolder'") + user_id.ToString();
      
      string q = "select IsNull(EUF,'') EUF, IsNull(EUI,'')EUI, IsNull(EUO,'')EUO, EUDept " +
        " from _engUser Where EUId=" + user_id;
      MyFillForm(this.grUser, "User", "EU", q);

      int i = (int)MyGetOneValue("select count(*) from _engDept where EDHead="+ user_id);
      UserIsDeptHead.Checked = Convert.ToBoolean(i);

      UserIsUpdating = false;
    }

    private void fill_users()
    {
      string q = "select distinct EUId,lower(EULogin) l from _engUser ";

      q += " left join _engDept on EUDept=EDId ";

      string sName = txtNameFilter.Text;
      if (sName != "" && sName != txtNameFilter.Tag.ToString())
        q += " where (" +
              " EUF like " + MyES(sName, true) +
              " or EUI like " + MyES(sName, true) +
              " or EUO like " + MyES(sName, true) +
              " or EULogin like " + MyES(sName, true) +
              " or EDName like " + MyES(sName, true) +
              " ) ";

      q += " order by lower(EULogin)";

      MyFillList(lstUser, q, "(пользователь)");
    }

    private void lstUser_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter) return;
      if (lstUser.SelectedItem!=null) return;
      string NewUserId = lstUser.Text;
      if (NewUserId == "") return;
      if (MessageBox.Show("Пользователь "+ NewUserId + " не найден.\n\nНайти и добавить?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
      Cursor = Cursors.WaitCursor;

      // Проверяем в домене
      bool IsAd = MyUserExistsAd(NewUserId);
      bool IsLocal=true;
      if (!IsAd)
      {
        IsLocal = MyUserExistsLocal(NewUserId);
      }

      if (!IsLocal)
      {
        MsgBox("Пользователь не найден в домене. И локально тоже.");
        Cursor = Cursors.Default;
        return;
      }

      // проверяем в базе - в таблице
      long u = 0;
      if (MyGetOneValue("select EUid from _engUser where EULogin=" + MyES(NewUserId)) == null)
      {
        u = MyExecute("Insert Into _engUser (EULogin) values ("+ MyES(NewUserId) +")");
        if (u == 1) u = (long)MyGetOneValue("select EUid from _engUser where EULogin=" + MyES(NewUserId));
      }

      // в именах входа
      MyExecute("IF SUSER_ID('"+ NewUserId + "') IS NULL CREATE LOGIN[" + NewUserId + "] FROM WINDOWS WITH DEFAULT_DATABASE =[SmuOk], DEFAULT_LANGUAGE =[русский];");
      MyExecute("ALTER SERVER ROLE[sysadmin] ADD MEMBER[" + NewUserId + "];");
      MyExecute("EXEC sp_addrolemember N'db_owner', "+MyES(NewUserId));

      // обновляем список, выбираем нового
      FormIsUpdating = true;
      MyFillList(lstUser, "select distinct EUId,EULogin from _engUser", "(пользователь)");
      FormIsUpdating = false;
      lstUser.SelectedValue = u;
      MsgBox("Пользователь добавлен.");
      Cursor = Cursors.Default;
    }

    private void lstRole_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (UserIsUpdating) return;
      long r = lstRole.GetLstVal();
      long u = lstUser.GetLstVal();
      string q;
      if(e.NewValue == CheckState.Checked)
        q = "uspUpdateUserRole " + u + "," + r;
      else
        q = "delete from _engUserRole Where EURUser=" + u + " and EURRole="+r;
      MyExecute(q);
      if (r == 3) // Куратор для станции, от участка
      {
        MyExecute("update _engUser set EUIsCurator=" + (e.NewValue == CheckState.Checked ? 1 : 0)+ " where EUId=" + u);
      }
    }

    private void btnIndexFolders_Click(object sender, EventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      MyExecute("delete from _engDirectoryIndex");

      List<string> ff = MyGetOneCol("select EDTIName from _engDirectoryToIndex;");
      foreach(string d in ff)
      {
        List<string> dd = new List<string>();
        MyGetDirs(ref dd, d, 1);
      }

      /*string d = "H:\\проекты\\";
      MyGetDirs(ref dd, d, 1);

      dd = new List<string>();
      d = "H:\\Сметы\\";
      MyGetDirs(ref dd, d, 2);*/

      MyExecute("exec uspUpdateFoldersForSpec");
      Cursor = Cursors.Default;
    }

    private void MyGetDirs(ref List<string> dd, string d, int iType /* 1 for проекты, 2 for сметы*/)
    {
      dd.Add(d);
      MyExecute("insert into _engDirectoryIndex (EDIDir,EDIType) values(" + MyES(d) +","+ iType + ")");
      foreach(string s in Directory.EnumerateDirectories(d, "*", SearchOption.AllDirectories))
      {
        dd.Add(s);
        MyExecute("insert into _engDirectoryIndex (EDIDir,EDIType) values(" + MyES(s) + "," + iType + ")");
      }

      //Directory.EnumerateFiles
    }

    private void btnGetFolders_Click(object sender, EventArgs e)
    {
      string q = "Select EDIId, EDIDir From _engDirectoryIndex where EDIDir like " + MyES(txtFolderSearch.Text,true);
      MyFillList(lstFolder, q);
    }

    private void lstFolder_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      try
      {
        Process.Start(((MyItem)((ListBox)lstFolder).SelectedItem).Text);
      }
      catch (Exception ex)
      {
        MsgBox("Что-то не открывается.");
      }
    }

    private void txtFolderSearch_KeyUp(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
      {
        btnGetFolders_Click(null, null);
      }
    }

    private void btnAllData_Click(object sender, EventArgs e)
    {
      string q = "select vwSpec.NewestFillingCount, vwSpecFill.* from vwSpecFill inner join vwspec on vwSpecFill.svid=vwspec.svid";
      q += " ORDER BY vwSpecFill.SId, SVNo, CASE IsNumeric(vwSpecFill.SFNo) WHEN 1 THEN Replicate('0', 10 - Len(SFNo)) + SFNo ELSE SFNo END, CASE IsNumeric(SFNo2) WHEN 1 THEN Replicate('0', 10 - Len(SFNo2)) +SFNo2 ELSE SFNo2 END";
      MyExcel(q,null,false,null,null,true);
    }

    private void btnCountData_Click(object sender, EventArgs e)
    {
      string q = "select distinct SId,STName,SVId,SVName,NewestFillingCount from vwSpec";
      MyExcel(q, null, false, null, null, true);
    }

    private void lstExec_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (UserIsUpdating) return;
      long x = lstExec.GetLstVal();
      long u = lstUser.GetLstVal();
      string q;
      if (e.NewValue == CheckState.Checked)
        q = "uspUpdateUserExec " + u + "," + x;
      else
        q = "delete from UserExec Where UEUser=" + u + " and UEExec=" + x;
      MyExecute(q);
    }

    private void lstScreenshot_SelectedIndexChanged(object sender, EventArgs e)
    {
      string q = "update _engUser set EUScreenshot = " + 1000 * lstScreenshot.GetLstVal() + " where euid = " + lstUser.GetLstVal();
      MyExecute(q);
    }

    private void btnShowScreens_Click(object sender, EventArgs e)
    {
      string s = btnShowScreens.Tag?.ToString();
      if (s != "" && System.IO.Directory.Exists(s)) Process.Start(s);
      else
      {
        try
        {
          System.IO.Directory.CreateDirectory(s);
          Process.Start(s);
        }
        catch { }
      }
    }

    private void btnScShTo_Click(object sender, EventArgs e)
    {

    }

    private void txtExecutor_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter){ 
        if (txtExecutor.Text == "")
        {
          MsgBox("Если оставить пустым, потом запутаемся.", "Ой!", MessageBoxIcon.Exclamation);
          return;
        }
        int iAlreadyHasThisName = (int)MyGetOneValue("select count(*)c from Executor where EName=" + MyES(txtExecutor.Text));
        if (iAlreadyHasThisName>0)
        {
          MsgBox("Такой уже есть, запутаемся!", "Ой.", MessageBoxIcon.Exclamation);
          return;
        }
        if (lstExecutor.Text == "new_Executor")
                {
                    MyExecute("insert into Executor (EName,ESmuDept) values('" + txtExecutor.Text + "',0)");
                    MsgBox("OK");
                }
        else
        {
          int si = lstExecutor.SelectedIndex;
          MyExecute("update Executor set EName="+ MyES(txtExecutor.Text)+ " where EId=" + lstExecutor.GetLstVal());
          MyFillList(lstExec, "select EId,EName,case when IsNull(UEExec,0)>0 then 1 else 0 end ch" +
            " from Executor Left Join(select UEExec from UserExec where UEUser = " + lstUser.GetLstVal() + ")q on EId = UEExec");
          MyFillList(lstExecutor, "select EId,EName from Executor order by case when ESmuDept=0 then 1000 else ESmuDept end,EName");
          lstExecutor.SelectedIndex = si;
        }
                MyFillList(lstExecutor, "select EId,EName from Executor order by case when ESmuDept=0 then 1000 else ESmuDept end,EName");
            }
    }

    private void lstExecutor_SelectedIndexChanged(object sender, EventArgs e)
    {
      txtExecutor.Text = (string)MyGetOneValue("select EName from Executor where EId=" + MyES(lstExecutor.GetLstVal()));
    }

    private void btnVerAdd_Click(object sender, EventArgs e)
    {
      Application.UseWaitCursor = true;
      UpdateActiveUsers();
      Application.UseWaitCursor = false;
    }

    private void lstExecutor_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        string s = lstExecutor.Text;
        int i = int.Parse(MyGetOneValue("select count(*)c from Executor where EName=" + MyES(s)).ToString());
        if (i == 0)
        {
          long rez;
          rez=long.Parse(MyGetOneValue("insert into Executor (EName) values ("+ MyES(s) + "); Select SCOPE_IDENTITY() as new_id;").ToString());
          MyFillList(lstExecutor, "select EId,EName from Executor order by case when ESmuDept=0 then 1000 else ESmuDept end,EName");
          lstExecutor.SelectItemByValue(rez);
        }
      }
    }

    private void dgv_engDept_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      MyCellContentClick(sender, e, true);
      string s = "";
      string sCol = ((DataGridView)sender).Columns[e.ColumnIndex].Name;
      switch (sCol)
      {
        case "dgv_btn_save":
          UserDeptRefresh();
          break;
      }
    }

    private void UserDeptRefresh()
    {
      bool b = !UserDept.Enabled;
      if (b) UserDept.Enabled = true;
      //if (UserDept.SelectedIndex == -1) UserDept.SelectedIndex = 1;
      long i = UserDept.SelectedIndex == -1 ? 0 : UserDept.GetLstVal();
      MyFillList(UserDept, "select 0 EDId,'<не выбран>' EDName union select EDId, EDName from _engDept order by EDName;");
      UserDept.SelectItemByValue(i);
      if (b) UserDept.Enabled = false;
    }

    private void dgv_engDept_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      MyCellValueChanged(sender, e, ref FormIsUpdating);
    }

    private void dgv_engDept_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      sender.MyDataError(e);
    }

    private void btnDptAdd_Click(object sender, EventArgs e)
    {
      dgv_engDept.MyRowAdd();
    }

    /*private void btnDptRefresh_Click(object sender=null, EventArgs e=null)
    {
      long i = UserDept.GetLstVal();
      MyFillList(UserDept, "select 0 EDId,'<не выбран>' EDName union select EDId, EDName from _engDept order by EDName;");
      UserDept.SelectItemByValue(i);
    }*/

    private void UserDept_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (UserIsUpdating) return;
      long user_id = lstUser.GetLstVal();
      MyExecute("update _engUser set EUDept=" + UserDept.GetLstVal() + " where EUId=" + user_id);
    }

    private void UserFIO_KeyDown(object sender, KeyEventArgs e)
    {
      TextBox tb = (TextBox)sender;
      if (e.KeyCode == Keys.Enter)
      {
        if (tb.Text == "")
        {
          MsgBox("Надо заполнить.", "Ой!", MessageBoxIcon.Exclamation);
          return;
        }
        else
        {
          long user_id = lstUser.GetLstVal();
          string fld = "EU" + tb.Name.Substring(4);
          MyExecute("update _engUser set "+fld+"=" + MyES(tb.Text) + " where EUId=" + user_id);
          if (UserIsDeptHead.Checked) FillDgvDept();
          lstUser.Focus();
        }
      }
    }

    private void UserIsDeptHead_CheckedChanged(object sender, EventArgs e)
    {
      if (UserIsUpdating) return;
      long u;
      long dpt = (long)MyGetOneValue("select EUDept from _engUser Where EUId=" + lstUser.GetLstVal());
      if (UserIsDeptHead.Checked){
        u = lstUser.GetLstVal();
      }
      else
      {
        u = 0;
      }
      string q;
      q = "update _engDept set EDHead="+u+" where EDId=" + dpt;
      MyExecute(q);
      FillDgvDept();
    }

    private void txtNameFilter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtNameFilter.Text = "";
        txtNameFilter_Enter();
        UserFilter();
      }
      if (e.KeyCode == Keys.Enter)
      {
        UserFilter();
      }
    }

    private void UserFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_users();
    }

    private void txtNameFilter_Leave(object sender, EventArgs e)
    {
      if (txtNameFilter.Text == "")
      {
        txtNameFilter.Text = txtNameFilter.Tag.ToString();
      }
      txtNameFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void txtNameFilter_Enter(object sender = null, EventArgs e = null)
    {
      if (txtNameFilter.Text == txtNameFilter.Tag.ToString())
      {
        txtNameFilter.Text = "";
      }
      txtNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

        private void button1_Click(object sender, EventArgs e)
        {
            string idSpec = SpecId.Text;
            string q = "delete from Spec where SId in (" + idSpec + ");";
            MyExecute(q);
            MsgBox("OK");
            return;
        }

        private void FindSpec_btn_Click(object sender, EventArgs e)
        {
            string selq = "select s.SId, SVName, case when s.SState = 1 then 'Заблокирован' else 'Активен' end as SState " +
                " from Spec s inner join vwSpecVer vw on vw.SId = s.SId " +
                " where SId in (" + SpecID_txtBox.Text.ToString() + ") ";
            MyFillDgv(dgvBlockedSpecs, selq);
            return;
            /*if(MessageBox.Show("Заблокировать шифры с идентификаторами: " + SpecID_txtBox.Text.ToString() + " ?"
                , "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MyExecute(updq);
                MsgBox("Ok");
                return;
            }
            else
            {
                MsgBox("Шифры не были заблокированы");
                return;
            }*/
        }

        private void dgvBlockedSpecs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string sId = dgvBlockedSpecs.CurrentRow.Cells["dgv_SId"].Value.ToString();
            string updq;
            if (dgvBlockedSpecs.CurrentCell.ColumnIndex == 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите заблокировать шифр: " + sId + " ?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    updq = "update Spec set SState = 1 where SId = " + sId; //1 - заблокирован
                    MyExecute(updq);
                    MsgBox("Шифр был заблокирован");
                    UpdateBlockedSpecs();
                }
                else
                {
                    MsgBox("Шифр не был заблокирован");
                }
            }
            else if (dgvBlockedSpecs.CurrentCell.ColumnIndex == 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите разблокировать шифр: " + sId + " ?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    updq = "update Spec set SState = 0 where SId = " + sId; //0 - разблокирован
                    MyExecute(updq);
                    MsgBox("Шифр был разблокирован");
                    UpdateBlockedSpecs();
                }
                else
                {
                    MsgBox("Шифр не был разблокирован");
                }
            }
            return;
        }
    }
}
