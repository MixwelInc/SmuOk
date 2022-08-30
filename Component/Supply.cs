using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;

namespace SmuOk.Component
{
  public partial class Supply : UserControl
  {
    private bool FormIsUpdating = true;
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure_Buy;

    public Supply()
    {
      InitializeComponent();
    }

    private void btnActivate_Click(object sender, EventArgs e)
    {
      int n = int.Parse(((Control)sender).Tag.ToString());
      for (int i=0; i<5; i++)
      {
        if (i!=n) tbl.RowStyles[i].Height = 5;
        else tbl.RowStyles[n].Height = 80;
      }

      foreach (Control c in Controls)
      {
        if (c.Name.StartsWith("btnActivate"))
        {
          c.ForeColor = c.Name == ((Control)sender).Name ? Color.Blue : Color.Gray;
        }
      }
      
      foreach (Control c in tbl.Controls)
      {
        if (c.Name.StartsWith("panel"))
        {
          bool b = c.Name.EndsWith((n+1).ToString());
          foreach (Control cc in c.Controls)
          {
            cc.Enabled = b;
          }
        }
      }
    }

    private void Supply_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure_Buy = FillReportData("SupplyBuy");
      FillFilter();
      SpecList_RestoreColunns(dgvSpec);
      dgvSupplyBuy.MyRestoreColWidthForUser(uid);
      int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneMultiline' and EUIOVaue=1");
      chkDoneMultiline.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkBudgetType' and EUIOVaue=1");
      chkBudgetType.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkBudgetSubcode' and EUIOVaue=1");
      chkBudgetSubcode.Checked = i == 1;

      btnActivate_Click(this.btnActivateBuy, null);

      FormIsUpdating = false;
      fill_dgv();
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      //MyFillList(lstExecFilter, "select eid, ename from (select -1 eid,'<не выбран>' ename, -1 ESmuDept union select EId, EName, ESmuDept from Executor)s order by case when ESmuDept=0 then 999999 else ESmuDept end;", "(исполнитель)");
      MyFillList(lstExecFilter, "select EId, EName from Executor order by case when ESmuDept = 0 then 999999 else ESmuDept end;", "(исполнитель)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO from vwSpec ";

      q += "inner join "+
        " (select distinct SFSpecVer from SpecFill " +
        "  where SFQtyGnT is not null or " +
        " SFQtyBuy is not null or " +
        " SFQtyWarehouse is not null or " +
        " SFQtyWorkshop is not null or " +
        " SFQtySub is not null or " +
        " SFSupplyPID is not null " +
        " )qqty on svid = SFSpecVer\n";
      q += "left join ( " +
         " select distinct SFSpecVer sfeo_nodate " +
         " from SpecFill inner join SpecFillExec on SFId = SFEFill " +
         " inner " +
         " join SpecFillExecOrder on SFEOSpecFillExec = SFEId " +
         " where SFEOStartDate is null " +
        " ) q_date on SVId = sfeo_nodate\n";

      if (lstExecFilter.GetLstVal() > 0)
      {
        q += "\n inner join (select SESpec from SpecExec where SEExec=" + lstExecFilter.GetLstVal() + ")se on SESpec=SId";
      }

      string sName = txtSpecNameFilter.Text;
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
      {
        q += " inner join (select SVSpec svs from SpecVer " +
              " where SVName like " + MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              ")q on svs=SId";
      }

      q += " where SType != 6 ";

      long f = lstSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and STId=" + f;

      if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstSpecUserFilter.GetLstVal() > 0) q += "and SUser=" + lstSpecUserFilter.GetLstVal();
      else if (lstSpecUserFilter.GetLstVal() == -1) q += "and SUser=0";

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      MyFillDgv(dgvSpec, q);
      if (dgvSpec.Rows.Count == 0) ClearEntity();
      else dgvSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));
      return;
    }

    private void ClearEntity()
    {
      //btnImportAndUpdate.Enabled = false;
    }

    private void dgvSpec_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      if (e.RowIndex >= 0)
      {
        EntityId = (long)dgvSpec.Rows[e.RowIndex].Cells["dgv_SId"].Value;
        //btnImportAndUpdate.Enabled = true;
        FillSpec();
      }
    }

    private void FillSpec()
    {
      Cursor = Cursors.WaitCursor;
      SpecVer = (long)(MyGetOneValue("select SVId from vwSpec where SId=" + EntityId) ?? -1);
      FillAdtInfo();
      FillFilling();
      Cursor = Cursors.Default;
      return;
    }

    private void FillAdtInfo()
    {
      // Вер.: , Получено: , строк
      string q = "select SVName + ' :: версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: ' +  cast(NewestFillingCount as nvarchar(max)) " +
          " from vwSpec Where SVId= " + SpecVer.ToString();
      string s = (string)MyGetOneValue(q);
      SpecInfo.Text = s;
    }

    public void FillFilling()
    {
      string q = "select SFEOId, SFSpecVer, SFSubcode, SFType, SFNo, SFNo2, "+
        " SFName, SFMark, SFUnit, SFEOStartDate, QtyToOrder, OId, "+
        " OSpecFillExecOrder, O1sId, ONo, OArt, OName, OUnit, OK, OQty " +
        " from vwOrder " +
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end,SFEOStartDate ";
      MyFillDgv(dgvSupplyBuy, q);
    }

    private void SpecTypeFilter(object sender=null, EventArgs e=null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void txtSpecNameFilter_Leave(object sender, EventArgs e)
    {
      if (txtSpecNameFilter.Text == "")
      {
        txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void txtSpecNameFilter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtSpecNameFilter.Text = "";
        txtSpecNameFilter_Enter();
        SpecTypeFilter();
      }
      if (e.KeyCode == Keys.Enter)
      {
        SpecTypeFilter();
      }
    }

    private void txtSpecNameFilter_Enter(object sender = null, EventArgs e = null)
    {
      if (txtSpecNameFilter.Text == txtSpecNameFilter.Tag.ToString())
      {
        txtSpecNameFilter.Text = "";
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void chkDoneMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkDoneMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSupplyBuy.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvSupplyBuy.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;

      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;//  "chkDoneMultiline";
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void btnExportBuy_Click(object sender, EventArgs e)
    {
      string q = "select SFEOId,SVName,SFSubcode,SFNo,SFNo2,SFName,SFMark,SFUnit,EName, QtyToOrder,convert(nvarchar(10),SFEOStartDate,104) SFEOStartDate, " +
        " O1sId, ONo, OArt, OName, OUnit, OK, OQty " +
        " from vwOrder " +
        " where SFSpecVer=" + SpecVer.ToString();

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }
      q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";

      MyExcel(q, FillingReportStructure_Buy, true, new decimal[] { 7, 11, 13.7M, 5, 5, 80, 80, 11, 13, 10, 9.43M, 12, 12, 12, 80, 12, 12, 12 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
      MyLog(uid, "Supply", 1100, SpecVer, EntityId);
    }

    private void btnImportBuy_Click(object sender, EventArgs e)
    {
      string sSpecName = (string)MyGetOneValue("select IsNull(SVName,'') from SpecVer Where SVId=" + SpecVer.ToString());
      if (sSpecName == "")
      {
        MsgBox("Название шифра не должно быть пустым!");
        return;
      }

      dynamic oExcel;
      dynamic oSheet;
      bool bNoError = MyExcelImportOpenDialog(out oExcel, out oSheet, "");

      if (!bNoError) return;

      if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure_Buy, pb);
      if (bNoError) MyExcelUnmerge(oSheet);

      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure_Buy, pb);
      if (bNoError) bNoError = FillingImportCheckSpecName(oSheet, sSpecName);
      if (bNoError) bNoError = FillingImportCheckSFEOIds(oSheet);
      //if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet);

      //if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false; // qty?

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData_Buy(oSheet);
          FillFilling();
          MsgBox("Ok");
        }
        oExcel.ScreenUpdating = true;
        oExcel.DisplayAlerts = true;
        oExcel.Quit();
      }
      else
      {
        oExcel.ScreenUpdating = true;
        oExcel.DisplayAlerts = true;
        oExcel.Visible = true;
        oExcel.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private bool FillingImportCheckSFEOIds(dynamic oSheet)
    {
      string sErr = "";
      long SFEOId;
      string s;
      List<string> ss = new List<string>();
      long z = 0;
      int ErrCount = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFEOId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 10 * r / rows, "Проверка идентификаторов строк.");
        SFEOId = (long)oSheet.Cells(r, c).Value;
        z = Convert.ToInt64(MyGetOneValue("select /*SFId,SFSpecVer,*/ count(*)c from " +
            " SpecFillExecOrder inner join SpecFillExec on SFEOSpecFillExec = SFEId " +
            " inner join SpecFill on SFId = SFEFill " +
            " where SFSpecVer = " + SpecVer + " and SFEOId = " + MyES(SFEOId) + ";"));
        if (z == 0)
        {
          ErrCount++;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, 2).Interior.Color = 0;
          oSheet.Cells(r, 2).Font.Color = -16776961;
        }
        else if (z > 1) throw new Exception();
      }

      if (ErrCount > 0)
      {
        sErr += "\nВ файле для загрузки часть идентификаторов заявок ошибочны (" + ErrCount + ").";
        MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      }
      return ErrCount == 0;
    }

    private bool FillingImportCheckSpecName(dynamic oSheet, string SpecName)
    {
      string s;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 2; // 1-based SpecCodeCol
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 30 + 10 * r / rows, "Проверка шифра проекта");
        s = oSheet.Cells(r, c).Value?.ToString() ?? "";
        if (s != SpecName)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Шифр проекта в файле (см. столбец <B>) не совпадает с шифром проекта в изменяемой версии (изменении), «" + SpecName + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void FillingImportData_Buy(dynamic oSheet)
    {
      long OSpecFillExecOrder;
      long O1sId;
      long ONo;
      string OArt;
      string OName;
      string OUnit;
      decimal OK;
      decimal OQty;

      string q = "";
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Формирование запросов");
        //столбцы константами, сорри

        OSpecFillExecOrder = long.Parse(oSheet.Cells(r, 1).Value.ToString());
        O1sId = long.Parse(oSheet.Cells(r, 12).Value.ToString());
        ONo = long.Parse(oSheet.Cells(r, 13).Value.ToString());
        OArt= oSheet.Cells(r, 14).Value.ToString();
        OName = oSheet.Cells(r, 15).Value.ToString();
        OUnit = oSheet.Cells(r, 16).Value.ToString();
        OK = decimal.Parse(oSheet.Cells(r, 17).Value.ToString());
        OQty = decimal.Parse(oSheet.Cells(r, 18).Value.ToString());

        q += "exec uspUpdateOrder " + OSpecFillExecOrder + "," + O1sId + "," + ONo + "," + MyES(OArt) + "," + MyES(OName) + "," + MyES(OUnit) + "," + MyES(OK) + "," + MyES(OQty) + ";\n";
      }
      q = q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      //!!! MyLog(uid, "SupplyDate", 90, SpecVer, EntityId);
      MyLog(uid, "Supply", 100, SpecVer, EntityId);
      return;
    }

    private void dgvSupplyBuy_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      ((DataGridView)sender).MySaveColWidthForUser(uid, e);
    }

    public void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
    }

    private void dgvSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0) return;
      if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "dgv_S_btn_folder") MyOpenSpecFolder(EntityId);
    }
  }
}
