  using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;

namespace SmuOk.Common
{
  public partial class SupplyRMType : UserControl
  {
    public SupplyRMType()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;

    private void SupplyRMType_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

        private void dgvSpec_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.ToInt32(dgvSpec.Rows[e.RowIndex].Cells["dgv_SState"].Value) == 1)
            {
                dgvSpec.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
            }
        }
        public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("SupplyRMTypes");
      FillFilter();
      SpecList_RestoreColunns(dgvSpec);

      dgvSpecSupplyRMTypeFill.MyRestoreColWidthForUser(uid);
      int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneMultiline' and EUIOVaue=1");
      chkDoneMultiline.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneType' and EUIOVaue=1");
      chkDoneType.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneSubcode' and EUIOVaue=1");
      chkDoneSubcode.Checked = i == 1;

      FormIsUpdating = false;
      fill_dgv();
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      MyFillList(lstExecFilter, "select EId, EName from Executor order by case when ESmuDept = 0 then 999999 else ESmuDept end;","(исполнитель)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;

    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO, SState from vwSpec ";

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

      q += " where /*1=1*/ pto_block=1 and SType != 6 ";

      long f = lstSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and STId=" + f;

      if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstSpecUserFilter.GetLstVal() > 0) q += "and SUser=" + lstSpecUserFilter.GetLstVal();
      else if (lstSpecUserFilter.GetLstVal() == -1) q += "and SUser=0";

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      MyFillDgv(dgvSpec, q);
      if (dgvSpec.Rows.Count == 0) NewEntity();
      else dgvSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));
      return;
    }

    private void NewEntity()
    {
      /*MyClearForm(this, "CuratorSpec");
      lstAcc.Enabled = false;
      lstCIW.Enabled = false;
      lstExecAcc.Enabled = false;
      lstExecCIW.Enabled = false;
      btnAddExecAcc.Enabled = false;
      btnAddExecCIW.Enabled = false;
      dgvPTODoc.MyClearRows();
      dgvSpecPerf.MyClearRows();
      btnSpecSave.Enabled = false;
      CuratorSpecName.Enabled = true;
      CuratorSpecName.Focus();*/
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
      FillAdtInfo();

      FillFilling();

      Cursor = Cursors.Default;
      return;
    }

    public void FillFilling()
    {
      string q = "select SFId,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQty,SFQtyBuy,SFQtyGnT,SFQtyWarehouse,SFQtyWorkshop,SFQtySub,SFSupplyPID," +
                "M15Num, M15Date, M15Qty, M15Price, M15Name, q.Qty " +
        " from SpecFill sf" +
        " left join M15 m on m.FillId = sf.SFId" +
        " outer apply(select DQty as Qty from Done left join SpecFillExec sfe on sfe.SFEId = DSpecExecFIll where sfe.SFEFill = sf.SFId)q" +
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
      MyFillDgv(dgvSpecSupplyRMTypeFill, q);
    }

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }


    private void dgvSpec_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      if (e.RowIndex >= 0)
      {
        EntityId = (long)dgvSpec.Rows[e.RowIndex].Cells["dgv_SId"].Value;
        FillSpec();
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

    private void chkDoneSubcode_CheckedChanged(object sender, EventArgs e)
    {
      dgvSpecSupplyRMTypeFill.Columns["dgv_SFSubcode"].Visible = chkDoneSubcode.Checked;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneSubcode.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void chkDoneType_CheckedChanged(object sender, EventArgs e)
    {
      dgvSpecSupplyRMTypeFill.Columns["dgv_SFType"].Visible = chkDoneType.Checked;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneType.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
            if(dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral)
            {
                MsgBox("Запрещено вносить изменения по заблокированным шифрам!");
                return;
            }
      OpenFileDialog ofd = new OpenFileDialog();

      string sSpecName = (string)MyGetOneValue("select IsNull(SVName,'') from SpecVer Where SVId=" + SpecVer.ToString());
      if (sSpecName == "")
      {
        MsgBox("Название шифра не должно быть пустым!");
        return;
      }// ssn == null ? "" : ssn.ToString();


      bool bNoError = true;
      var f = string.Empty;
      //ofd.InitialDirectory = "c:\\";
      ofd.Filter = "MS Excel files (*.xlsx)|*.xlsx";
      ofd.RestoreDirectory = true;

      if (ofd.ShowDialog() != DialogResult.OK) return;
      f = ofd.FileName;

      Application.UseWaitCursor = true;
      Application.DoEvents();
      Type ExcelType = Type.GetTypeFromProgID("Excel.Application");
      dynamic oApp = Activator.CreateInstance(ExcelType);
      oApp.Visible = false;
      oApp.ScreenUpdating = false;
      oApp.DisplayAlerts = false;

      MyProgressUpdate(pb, 5, "Настройка Excel");

      try
      {
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Office\\14.0\\Excel\\Security", true);
        rk.SetValue("AccessVBOM", 1, RegistryValueKind.DWord);
        rk.SetValue("Level", 1, RegistryValueKind.DWord);
        rk.SetValue("VBAWarnings", 1, RegistryValueKind.DWord);
      }
      catch
      {
        oApp.ScreenUpdating = true;
        oApp.DisplayAlerts = true;
        throw;
      }

      MyProgressUpdate(pb, 8, "Открываем файл");

      dynamic oBook = oApp.Workbooks.Add();

      // макросом обходим проблему с именованным диапазоним при наличии в файле автофильтра
      var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
      var codeModule = oModule.CodeModule;
      var lineNum = codeModule.CountOfLines + 1;
      string sCode = "Public Sub myop1()\r\n";
      sCode += "  'MsgBox \"Hi from Excel\"" + "\r\n";
      sCode += "  Workbooks.Open \"" + f + "\"\r\n";
      sCode += "End Sub";

      MyProgressUpdate(pb, 10, "Открываем файл");

      codeModule.InsertLines(lineNum, sCode);
      oApp.Run(oBook.Worksheets[1].Name + ".myop1");

      //oBook = oApp.Workbooks.Open(f);
      oApp.Workbooks[1].Close();
      oBook = oApp.Workbooks[1];
      if (oBook.Worksheets.Count > 1)
      {
        MsgBox("В книге более 1 листа.", "Ошибка", MessageBoxIcon.Warning);
        bNoError = false;
      }

      dynamic oSheet = oBook.Worksheets(1);
      if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb)) bNoError = false;
      MyExcelUnmerge(oSheet);
      if (bNoError && !MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb)) bNoError = false;
      if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
      if (bNoError && !FillingImportCheckSFIds(oSheet, SpecVer, lstExecFilter.GetLstVal())) bNoError = false;
      if (bNoError && !FillingImportCheckIdsUniq(oSheet)) bNoError = false;
      if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false;

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet);
          FillFilling();
          MsgBox("Ok");
        }
        oApp.ScreenUpdating = true;
        oApp.DisplayAlerts = true;
        oApp.Quit();
      }
      else
      {
        oApp.ScreenUpdating = true;
        oApp.DisplayAlerts = true;
        oApp.Visible = true;
        oApp.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private void FillingImportData(dynamic oSheet)
    {
      long db_id = 0;
      string q = "";
      decimal d = 0;
      string s;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int[] cc = new int[] {10,11,12,13,14,15 };
      string[] db_cols = new string[] { "SFQtyGnT", "SFQtyBuy", "SFQtyWarehouse", "SFQtyWorkshop", "SFQtySub", "SFSupplyPID" };


      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Формирование запросов");
        q += "update SpecFill set ";
        db_id = (long)oSheet.Cells(r, 1).Value;
        for (int i=0; i< db_cols.Count(); i++)
        {
          d = decimal.Parse(oSheet.Cells(r, cc[i]).Value?.ToString() ?? "0");
          s = d==0 ? "null" : MyES(d);
          q += " " + db_cols[i] + "=" + s + ",";
        }
        q = q.Substring(0, q.Length - 1);
        q += " where SFId=" + db_id + ";\n";
      }
      q = q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "SupplyRMType", 60, SpecVer, EntityId);
      return;
    }

    private bool FillingImportCheckSums(dynamic oSheet)
    {
      string sErr = "";
      string s;
      long i;
      decimal qty_db;
      decimal qty_xl = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      List<int> cc = new List<int> { 10, 11, 12, 13 }; // 1-based DQty
      if (rows == 1) return true;
      int err_count = 0;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 70 + 10 * r / rows, "Проверка кол-ва.");
        i = (long)oSheet.Cells(r, 1).Value;
        qty_xl = 0;
        foreach (int c in cc)
        {
          s = oSheet.Cells(r, c).Value?.ToString() ?? "0";
          qty_xl += decimal.Parse(s);
        }

        qty_db = Convert.ToDecimal(MyGetOneValue("select SFQty from SpecFill where SFId =" + i ));

        if (qty_db != qty_xl)
        {
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          foreach (int c in cc){
            oSheet.Cells(r, c).Interior.Color = 0;
            oSheet.Cells(r, c).Font.Color = -16776961;
          }
          err_count++;
        }
      }

      if (err_count > 0) sErr += "\nВ части строк количество не соответствует общему (" + err_count + ").";
      if (sErr != "") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      return err_count == 0;
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
      int c = 2; // 1-based SpecCodeCol
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

    private void btnExport_Click(object sender, EventArgs e)
    {
      // заголовки
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure) tt.Add(f.Title);
            string q = "select SFId,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQty,SFQtyGnT,SFQtyBuy,SFQtyWarehouse,SFQtyWorkshop,SFQtySub,SFSupplyPID," +
                "M15Num, convert(varchar(10), M15Date, 120) as M15Date, M15Qty, M15Price, M15Name, q.Qty " +
        " from SpecFill sf" +
        " left join M15 m on m.FillId = sf.SFId" +
        " outer apply(select DQty as Qty from Done left join SpecFillExec sfe on sfe.SFEId = DSpecExecFIll where sfe.SFEFill = sf.SFId)q" +
        " where SFSpecVer=" + SpecVer.ToString();
        //" order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
            /*string q = "select SFId,SVName,SFSubcode,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQty,SFQtyGnT,SFQtyBuy,SFQtyWarehouse,SFQtyWorkshop,SFQtySub,SFSupplyPID  " +
              " from SpecFill sf  left join SpecVer on SVId=SFSpecVer " +
              " left join M15 m on m.FillId = sf.SFId" +
              " outer apply(select DQty as Qty from Done left join SpecFillExec sfe on sfe.SFEId = DSpecExecFIll where sfe.SFEFill = sf.SFId)q" +
              " where SFSpecVer=" + SpecVer.ToString();*/

            int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }
      q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";

      MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 17, 17, 5, 5, 80, 50, 11, 8.14M, 8.14M, 8.14M, 8.14M, 8.14M, 8.14M, 9, 9, 12, 9, 9, 80, 15}, new int[] { 3, 4, 5, 6, 7, 8, 9, 16, 17, 18, 19, 20, 21 });
      MyLog(uid, "SupplyRMType", 1060, SpecVer, EntityId);
    }

    private void chkDoneMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkDoneMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecSupplyRMTypeFill.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvSpecSupplyRMTypeFill.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;//  "chkDoneMultiline";
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void dgvSpecDoneFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      if (FormIsUpdating) return;
      dgvSpecSupplyRMTypeFill.MySaveColWidthForUser(uid, e);
    }

    private void dgvSpecSupplyRMTypeFill_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      dgvSpecSupplyRMTypeFill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
      e.ThrowException = false;
    }

    private void dgvSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0) return;
      if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "dgv_S_btn_folder") MyOpenSpecFolder(EntityId);
    }

    public void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
    }
  }
}
