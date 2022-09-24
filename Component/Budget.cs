using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;
using static SmuOk.Common.SmuOKData;

namespace SmuOk.Component
{
  public partial class Budget : UserControl// SmuOk.Common.MyComponent
  {

    private bool FormIsUpdating = true;
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;

    public Budget()
    {
      InitializeComponent();
    }

    private void Budget_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("Budget");
      FillFilter();

      SpecList_RestoreColunns(dgvSpec);

      dgvSpecBudgetFill.MyRestoreColWidthForUser(uid);

      int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneMultiline' and EUIOVaue=1");
      chkDoneMultiline.Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkBudgetType' and EUIOVaue=1");
      chkBudgetType.Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkBudgetSubcode' and EUIOVaue=1");
      chkBudgetSubcode.Checked = i == 1;

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
      filterType.Text = "(тип фильтра)";
      ////if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO from vwSpec ";

      if (lstExecFilter.GetLstVal() > 0)
      {
        q += "\n inner join (select SESpec from SpecExec where SEExec=" + lstExecFilter.GetLstVal() + ")se on SESpec=SId";
      }

      string sName = txtSpecNameFilter.Text;
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString() && (filterType.Text == "ID спецификации" || filterType.Text == "Шифр"))
      {
        q += " inner join (select SVSpec svs from SpecVer left join Budget b on b.BSId = SVSpec " +
              " where SVName like " + MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) + 
              ")q on svs=SId ";
      }
      else if (sName != "" && sName != txtSpecNameFilter.Tag.ToString() && filterType.Text == "ID сметы")
            {
                q += " inner join (select SVSpec svs from SpecVer left join Budget b on b.BSId = SVSpec " +
                " where " +
                " BId = " + MyDigitsId(sName) +
                ")q on svs=SId ";
            }

      q += " where pto_block=1 ";

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
      btnImportAndUpdate.Enabled = false;
    }

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
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

    private void txtSpecNameFilter_Leave(object sender, EventArgs e)
    {
      if (txtSpecNameFilter.Text == "")
      {
        txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void txtSpecNameFilter_Enter(object sender=null, EventArgs e=null)
    {
      if (txtSpecNameFilter.Text == txtSpecNameFilter.Tag.ToString())
      {
        txtSpecNameFilter.Text = "";
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void dgvSpec_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      if (e.RowIndex >= 0)
      {
        EntityId = (long)dgvSpec.Rows[e.RowIndex].Cells["dgv_SId"].Value;
        btnImportAndUpdate.Enabled = true;
        FillSpec();
      }
    }

    private void FillAdtInfo()
    {
      // Вер.: , Получено: , строк
      string q = "select SVName + ' :: версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: ' +  cast(NewestFillingCount as nvarchar(max)) " +
          " from vwSpec Where SVId= " + SpecVer.ToString();
      string s = (string)MyGetOneValue(q);
      SpecInfo.Text = s;
    }

    private void FillSpec()
    {
      Cursor = Cursors.WaitCursor;
      SpecVer = (long)(MyGetOneValue("select SVId from vwSpec where SId=" + EntityId) ?? -1);
      FillBudges();
      FillAdtInfo();
      FillFilling();
      Cursor = Cursors.Default;
      return;
    }

    public void FillFilling()
    {
      string q = "select SFId,SFSource,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQty, /*SFQtyBuy,SFQtyGnT,SFQtyWarehouse,SFQtySub,SFSupplyPID*/ " +
        " SFBudgetType, SFBudget, SFBudgetNo, SFBudgetSmrNo, SFBudgetCode, SFBudgetName, SFBudgetUnit, SFBudgetK, SFBudgetQty, SFBudgetPrc" +
        " from SpecFill " +
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
      MyFillDgv(dgvSpecBudgetFill, q);
    }

        public void FillBudges()
        {
            string q = "select BId, BNumber, BStage from budget where BSid = " + EntityId + 
                " order by BId ";
            MyFillDgv(dgv_Budg, q);
        }

    private void btnImportAndUpdate_Click(object sender, EventArgs e)
    {
      //using (var fbd = new FolderBrowserDialog())
      //{
      //  DialogResult result = fbd.ShowDialog();
      //if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
      //{
      // string path fbd.SelectedPath
      string path = "D:\\~project\\94. СМУ 24\\Материалы\\Сметы\\2 to 6";
      List<string> files = new List<string>(Directory.GetFiles(path, "*.xlsx"));
      files.RemoveAll((x) => x.StartsWith(path + "\\~"));
      if (files.Count == 0)
      {
        MsgBox("Файлы не найдены.");
        return;
      }
      MessageBox.Show("Files found: " + files.Count.ToString(), "Message");
        //}
      //}
    }

    private void btnImportAndUpdate_MouseHover(object sender, EventArgs e)
    {
      btnImportAndUpdate.FlatAppearance.BorderSize = 1;
      btnImportAndUpdate.FlatAppearance.BorderColor = Color.Gray;
    }

    private void btnImportAndUpdate_MouseLeave(object sender, EventArgs e)
    {
      btnImportAndUpdate.FlatAppearance.BorderSize = 0;
    }

    private void chkBudgetSubcode_CheckedChanged(object sender, EventArgs e)
    {
      dgvSpecBudgetFill.Columns["dgv_SFSubcode"].Visible = chkBudgetSubcode.Checked;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkBudgetSubcode.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void chkBudgetType_CheckedChanged(object sender, EventArgs e)
    {
      dgvSpecBudgetFill.Columns["dgv_SFType"].Visible = chkBudgetType.Checked;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkBudgetType.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void chkDoneMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkDoneMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecBudgetFill.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvSpecBudgetFill.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;//  "chkDoneMultiline";
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
            int i = (int)(MyGetOneValue("select count(*) from SpecVer where SVPtoDone is not null and SVSpec=" + EntityId));
            if (i == 0)
            {
                //MsgBox("История формируется по версиям после завершения редактирования наполнения.\n\nНи одной такой версии для шифра не найдено.", PTOSpecName.Text + " – история", MessageBoxIcon.Exclamation);
                return;
            }

            string q;
            MyExportExcel mee;
            List<MyExportExcel> reports_data = new List<MyExportExcel>();

            //uspReport_SpecFillHistory
            mee = new MyExportExcel();
            mee.ReportTitle = "Сравнение";
            mee.sQuery = "exec [uspReport_SpecFillBudgetHistory_v3.0] " + EntityId;
            //mee.ssTitle = tt.ToArray();
            mee.Title2Rows = true;
            mee.colsWidth = new decimal[] { 10, 10, 18, 18, 18, 18, 9, 9, 50,30,10,10,10,18,10,10,18,18,18,50,18,18,10,10};
            mee.AfterFormat = "SpecFillBudgetHistory";
            mee.GrayColIDs = new int[] {4,5,6,7,8,9,10,11,12,13,14,15};
            reports_data.Add(mee);
            if (reports_data.Count == 0)
            {
                MsgBox("Нет наполнения.");
                return;
            }
            MyExcel(reports_data);

        }

    private void btnImport_Click(object sender, EventArgs e)
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

      if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb, budg: true);
      //MyExcelUnmerge(oSheet);
      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);
      //if (bNoError) bNoError = FillingImportCheckSpecName(oSheet, sSpecName);
      //if (bNoError) bNoError = FillingImportCheckSFIds(oSheet, SpecVer, lstExecFilter.GetLstVal());
      if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet);
      /*if (bNoError) bNoError = FillingImportCheckSums(oSheet);
      */

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet, FillingReportStructure, pb);
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

    private void FillingImportData(dynamic oSheet, List<MyXlsField> ReportStructure, object pb)
    {
      long db_id = 0;
      string q = "";
      string myq = "";//сюда буду писать апдейт в specfillhistory
      long rl=0; decimal rd=0; bool b;
      string s="";
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int[] cc = new int[] { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
      string[] db_cols = new string[] { "SFBudgetType", "SFBudget", "SFBudgetNo", "SFBudgetSmrNo", "SFBudgetCode", "SFBudgetName", "SFBudgetUnit", "SFBudgetK", "SFBudgetQty", "SFBudgetPrc" };
            string[] newdb_cols = new string[] { "SFHBudgetType", "SFHBudget", "SFHBudgetNo", "SFHBudgetSmrNo", "SFHBudgetCode", "SFHBudgetName", "SFHBudgetUnit", "SFHBudgetK", "SFHBudgetQty", "SFHBudgetPrc" };//здесь закончил, надо сделать импорт в другую таблицу, оттуда и экспорт будет норм

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Формирование запросов");
                db_id = (long)oSheet.Cells(r, 1).Value;
        q += "update SpecFill set "; 
        for (int i = 0; i < db_cols.Count(); i++)
        {
          switch (ReportStructure[cc[i] - 1].DataType)
          {
            case "long":
              s = oSheet.Cells(r, cc[i]).Value?.ToString() ?? "0";
              long.TryParse(s, out rl);
              s = MyES(rl);
              break;
            case "decimal":
              s = oSheet.Cells(r, cc[i]).Value?.ToString() ?? "0";
              decimal.TryParse(s, out rd);
              s = MyES(rd);
              break;
            case "string":
              s = oSheet.Cells(r, cc[i]).Value?.ToString() ?? "";
              s = MyES(s);
              //if (ReportStructure[i - 1].Nulable) s = s == "" ? "null" : MyES(s);
              break;
            case "date":
              break;
        }
          //d = (decimal)(oSheet.Cells(r, cc[i]).Value ?? 0);
          //s = d == 0 ? "null" : MyES(d);
          q += " " + db_cols[i] + "=" + s + ",";
        }
        q = q.Substring(0, q.Length - 1);
        q += " where SFId=" + db_id + ";\n";
      }
      q = q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "Budget", 70, SpecVer, EntityId);
      return;
    }

    private bool FillingImportCheckIdsUniq(dynamic oSheet)
    {
      string sErr = "";
      string s;
      HashSet<string> ssuniq = new HashSet<string>();
      List<string> errs = new List<string>();
      long z;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFEId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 60 + 10 * r / rows, "Проверка единичности идентификаторов строк.");
        s = oSheet.Cells(r, c).Value.ToString();
        if (!ssuniq.Add(s)) errs.Add(s);
      }

      if (errs.Count() > 0)
      {
        oSheet.Columns(1).FormatConditions.AddUniqueValues();
        oSheet.Columns(1).FormatConditions(1).DupeUnique = 1;// xlDuplicate;
        oSheet.Columns(1).FormatConditions(1).Font.Color = -16776961;
        oSheet.Columns(1).FormatConditions(1).Interior.Color = 0;
        oSheet.Columns(1).FormatConditions(1).StopIfTrue = 0;
        sErr += "\nВ файле для загрузки идентификаторы строк не уникальны.";
      }

      if (sErr != "") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      return sErr == "";
    }

    private bool FillingImportCheckSFIds(dynamic oSheet, long verid, long execid)
    {
      string sErr = "";
      object o_s;
      string s;
      List<string> ss = new List<string>();
      long z;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 10 * r / rows, "Проверка идентификаторов строк.");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (s == "") z = 0; // ващет надо было это раньше найти
        else if (!long.TryParse(s, out z)) z = 0; //не число
        else if (z.ToString() != s || z < 0) z = 0; //не положительное целое, еще что-то не так
        if (z > 0)
        {
          z = Convert.ToInt64(MyGetOneValue("select count(SFId) from SpecFill " +
            " inner join SpecVer on SFSpecVer = SVId " +
            " where SFId = " + MyES(z) + " and SVId = '" + verid + "';"));
        }

        if (z == 0)
        {
          ss.Add(s);
          oSheet.Cells(r, 1).Interior.Color = 0;
          oSheet.Cells(r, 1).Font.Color = -16776961;
        }
        else if (z > 1) throw new Exception();
      }

      z = ss.ToArray().Distinct().Count();
      if (z > 0) sErr += "\nВ файле для загрузки не найдена часть идентификаторов строк (" + z + ").";
      if (sErr != "") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      return z == 0;
    }

    private bool FillingImportCheckSpecName(dynamic oSheet, string SpecName)
    {
      object o_s;
      string s;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 3; // 1-based SpecCodeCol
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 30 + 10 * r / rows, "Проверка шифра проекта");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if ((!FillingReportStructure[c - 1].Nulable && s == "") || s != SpecName)
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

    private void btnAddRows_Click(object sender, EventArgs e)
    {
      AddRowsToSpecFill(uid, SpecVer, "Сметный", pb);
      MyLog(uid, "Budget", 75, SpecVer, EntityId);
      FillSpec();
    }

    private void dgvSpecBudgetFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      ((DataGridView)sender).MySaveColWidthForUser(uid, e);
    }

    private void SpecList_CheckedChanged(object sender, EventArgs e)
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
