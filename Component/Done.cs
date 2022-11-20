using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;
using SmuOk.Common;

namespace SmuOk.Component
{
  public partial class Done : UserControl// MyComponent
  {
    public Done()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "Done";
    private string FormSqlPref = "D";
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;

    private void Done_Load(object sender, EventArgs e)
    {
      LoadMe();
      fill_dgv();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("Done");
      FillFilter();

      SpecList_RestoreColunns(dgvSpec);

      dgvSpecFill.MyRestoreColWidthForUser(uid);
      int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneMultiline' and EUIOVaue=1");
      chkDoneMultiline.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneType' and EUIOVaue=1");
      chkDoneType.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneSubcode' and EUIOVaue=1");
      chkDoneSubcode.Checked = i == 1;

      FormIsUpdating = false;
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();  
      masterID.Text = masterID.Tag.ToString();
      slaveID.Text = slaveID.Tag.ToString();
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      MyFillList(lstExecFilter, "select eid, ename from (select -1 eid,'<не выбран>' ename, -1 ESmuDept union select EId, EName, ESmuDept from Executor)s order by case when ESmuDept=0 then 999999 else ESmuDept end;", "(исполнитель)");
      //MyFillList(lstExecFilter, "select EId, EName from Executor inner join UserExec on UEExec = EId where UEUser = "+ (IsDebugComputer()?1:uid)+" order by case when ESmuDept = 0 then 999999 else ESmuDept end;");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO from vwSpec ";

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
      if (dgvSpec.Rows.Count == 0) NewEntity();
      else dgvSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));
      return;
    }

    private void NewEntity()
    {
    }

    private void FillAdtInfo()
    {
      // Вер.: , Получено: , строк
      string q = "select SVName + ' :: версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: ' " +
          "  +isnull(cc,0) " +
          "	+' ('+case when NewestFillingCount = 0 then 'нет' else convert(nvarchar, NewestFillingCount) end +')' f " +
          "  from vwSpec " +
          "	left join ( " +
          "	select SFSpecVer, cast(count(SFEId) as nvarchar) cc from SpecFill left join SpecFillExec on SFId=SFEFill " +
          "	where SFEExec=" + lstExecFilter.GetLstVal() +
          "	group by SFSpecVer " +
          ")ff " +
          "	on SFSpecVer=SVId " +
          "	Where SVId= " + SpecVer.ToString();
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
      string q = "select SFEId,SFId SFEFill, SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFEQty, DSumQty, SFEQty-(IsNull(DSumQty,0)) DRestQty " +
        " from SpecFill left join SpecFillExec on SFId=SFEFill " +
        " left join (select DSpecExecFill, sum(DQty) DSumQty from Done group by DSpecExecFill)d on DSpecExecFill = SFEId " +
        " where SFSpecVer=" + SpecVer.ToString() +
        //" and SFEExec=" + lstExecFilter.GetLstVal() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
      MyFillDgv(dgvSpecFill, q);
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

    private void txtSpecNameFilter_Enter(object sender=null, EventArgs e=null)
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
      dgvSpecFill.Columns["dgv_SFSubcode"].Visible = chkDoneSubcode.Checked;
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
      dgvSpecFill.Columns["dgv_SFType"].Visible = chkDoneType.Checked;
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
      OpenFileDialog ofd = new OpenFileDialog();

      string sSpecName = (string)MyGetOneValue("select IsNull(SVName,'') from SpecVer Where SVId=" + SpecVer.ToString());
      if (sSpecName == "")
      {
        MsgBox("Название шифра не должно быть пустым!");
        return;
      }

      bool bNoError = true;
      var f = string.Empty;
      //ofd.InitialDirectory = "c:\\";
      ofd.Filter = "MS Excel files (*.xlsx)|*.xlsx";
      ofd.RestoreDirectory = true;

      if (ofd.ShowDialog() != DialogResult.OK) return;
      f = ofd.FileName;

      Application.UseWaitCursor = true;
      Type ExcelType = Type.GetTypeFromProgID("Excel.Application");
      dynamic oApp = Activator.CreateInstance(ExcelType);
      oApp.Visible = false;
      oApp.ScreenUpdating = false;
      oApp.DisplayAlerts = false;

      MyProgressUpdate(pb, 5, "Настройка Excel");

      /*try
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
      }*/

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
      if (bNoError && !ImportDone_Check(oSheet)) bNoError = false;
      /*if (bNoError) MyExcelUnmerge(oSheet);
      if (bNoError && !MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb)) bNoError = false;
      if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
      if (bNoError && !FillingImportCheckExecName(oSheet, lstExecFilter.GetLstText())) bNoError = false;
      if (bNoError && !FillingImportCheckSFEIds(oSheet, SpecVer,lstExecFilter.GetLstVal())) bNoError = false;
      if (bNoError && !FillingImportCheckIdsUniq(oSheet)) bNoError = false;
      if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false;*/



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
        oApp.Visible = true;
        oApp.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private bool ImportDone_Check(dynamic oSheet)
    {
      //проверяем валидность загружаевого ВОРа, он же "Акт мершейдерского замера"

      string s, q;
      bool b=true;

      //общий заголовок
      s = oSheet.Cells(1, 5).Value?.ToString() ?? "";
      if (!s.StartsWith("Акт маркшейдерского замера №"))
      {
        oSheet.Cells(1, 5).Font.Color = -16776961;
        oSheet.Cells(1, 5).Interior.Color = 0;
        MsgBox("Заголовок документа неверный.");
        return false;
      }

      //дата
      if(!MyExcel_ValueIsDate(oSheet.Cells(3, 7)))
      {
        oSheet.Cells(3, 7).Font.Color = -16776961;
        oSheet.Cells(3, 7).Interior.Color = 0;
        MsgBox("Дата акта не указана.");
        return false;
      }

      //шифр + версия
      s = oSheet.Cells(5, 7).Value?.ToString() ?? "";
      string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + EntityId).ToString();
      if (s != sSpecInfo)
      {
        oSheet.Cells(5, 7).Font.Color = -16776961;
        oSheet.Cells(5, 7).Interior.Color = 0;
        MsgBox("Шифр или версия указан не верно.\n\nДолжно быть: " + sSpecInfo);
        return false;
      }

      //строки:
      //  1. по наличию: шифр+версия, id задачи, исполнитель, №№ п/п, Наименование, Марка (если не пустая), ед., всего, осталось
      //  2. необязательная только марка
      //  3. количество: введено
      //  4. останавливаемся на 1+2+3=пусто
      int r = 10;

      string[] ss;
      decimal qty_total, qty_new;

      GetDataRowFromFile(oSheet, r, out ss, out qty_total, out qty_new);

      if (ss[0] == "" && ss[1] == "" && ss[2] == "")
      {
        MsgBox("Документ не заполнен.\n\nНачиная со строки 10 должны содержаться данные для загрузки.");
        return false;
      }

      while (!(ss[0]=="" && ss[1]=="" && ss[2]=="")) //до пустой строки
      {
        for (int i = 0; i < 6; i++) //первые столбцы должны быть заполнены
        {
          if (i != 4)
          {
            if (ss[i] == "")
            {
              b = false;
              oSheet.Cells(r, i+1).Font.Color = -16776961;
              oSheet.Cells(r, i+1).Interior.Color = 0;
            }
          }
        }
        if (qty_total == 0)
        {
          b = false;
          oSheet.Cells(r, 9).Font.Color = -16776961;
          oSheet.Cells(r, 9).Interior.Color = 0;
        }

        if (qty_new == 0)
        {
          b = false;
          oSheet.Cells(r, 11).Font.Color = -16776961;
          oSheet.Cells(r, 11).Interior.Color = 0;
        }

        q = "select" +
            "   count(*)/*SVId,EName,SFEId,SFNo,SFNo2,SFName,SFMark,SFUnit,IsNull(SFEQty,0)SFEQty, IsNull(SFEQty,0)-(IsNull(DSumQty,0)) DRestQty */ " +
            " from " +
            "   SpecVer " +
            "   inner join SpecFill on SVId = SFSpecVer" +
            "   inner join SpecFillExec on SFId = SFEFill" +
            "   inner join Executor on SFEExec = EId " +
            "   left join (select DSpecExecFill, sum(DQty) DSumQty from Done group by DSpecExecFill)d on DSpecExecFill = SFEId" +
            " where " +
            "   SVId = " + MyES(SpecVer) +
            "   and EName = " + MyES(ss[1]) +
            "   and SFEId = " + MyES(ss[0]) +
            "   and SFNo+'.' + SFNo2 = " + MyES(ss[4]) +
            "   and SFName = " + MyES(ss[5]) + 
            "   and SFUnit = " + MyES(ss[7]) +
            "   and SFEQty = " + MyES(qty_total) +
            "   and IsNull(SFEQty,0)-(IsNull(DSumQty, 0)) >= " + MyES(qty_new) +
            ";";
        int rez = (int)MyGetOneValue(q);
        if (rez == 0)
        {
          oSheet.Rows(r).Font.Color = -16776961;
          b = false;
        }

        r++;
        GetDataRowFromFile(oSheet, r, out ss, out qty_total, out qty_new);
      }
      if (!b) MsgBox("Данные не соответствуют выбранному шифру и исполнителю (либо превышено количество).\n\nКрасным шрифтом выделены строки с ошибками,\nчерный фон указывает на необходимость внести данные.");
      return b;
    }

    private void GetDataRowFromFile(dynamic oSheet, int r, out string[] ss, out decimal qty_total, out decimal qty_new)
    {
      ss = new string[8];
      for (int i = 0; i < 8; i++) {
        ss[i] = oSheet.Cells(r, i+1).Value?.ToString() ?? "";
      }

      qty_total = 0;
      //string s = oSheet.Cells(r, 7).Value?.ToString() ?? 0;
      try { qty_total = decimal.Parse(oSheet.Cells(r, 9).Value?.ToString() ?? 0); }
      catch { }

      qty_new = 0;
      //s = oSheet.Cells(r, 9).Value?.ToString() ?? 0;
      try { qty_new = decimal.Parse(oSheet.Cells(r, 11).Value?.ToString() ?? 0); }
      catch { }

      return;
    }

    private void FillingImportData(dynamic oSheet)
    {
      long iId;
      decimal dQty;
      DateTime dt;
      string s, q, sCaption;
      //dynamic range = oSheet.UsedRange;
      //int rows = range.Rows.Count;

      dt = (DateTime)oSheet.Cells(3, 7).Value;
      s = oSheet.Cells(5, 7).Value.ToString();
      long DoneHeaderId = long.Parse(MyGetOneValue("insert into DoneHeader (DHDate,DHSpecTitle) values (" + MyES(dt) + ","+ MyES(s) +"); Select SCOPE_IDENTITY() as new_id;").ToString());

      q = "insert into Done (DSpecExecFill,DQty,DDate,DHeader,DCaption) Values\n";

      int r = 10;
      while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
      {
        MyProgressUpdate(pb, 80, "Формирование запросов");
        iId = long.Parse(oSheet.Cells(r, 1).Value);
        dQty = (decimal) oSheet.Cells(r, 11).Value;
        sCaption = oSheet.Cells(r, 13).Value?.ToString() ?? "";
        q += "(" + iId + "," + MyES(dQty) + "," + MyES(dt) + ","+ DoneHeaderId +","+ MyES(sCaption) + ")\n,";
        r++;
      }

      q=q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "Done", 120, DoneHeaderId, EntityId);
      return;
    }

    private bool FillingImportCheckSums(dynamic oSheet)
    {
      string sErr = "";
      string s;
      List<string> ss = new List<string>();
      long i;
      decimal z;
      decimal v;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 13; // 1-based DQty
      if (rows == 1) return true;
      int err_count = 0;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 70 + 10 * r / rows, "Проверка кол-ва, исполнено.");
        i = (long)oSheet.Cells(r, 1).Value;
        s = oSheet.Cells(r, c).Value.ToString();
        v = decimal.Parse(s);
        z = Convert.ToDecimal(MyGetOneValue("select SFEQty - Sum(IsNull(DQty, 0))DQty_rest from SpecFillExec " +
          " inner join SpecFill on SFEFill = SFId " +
          " inner join SpecVer on SFSpecVer = SVId " +
          " left join Done on DSpecExecFill = SFEId " +
          " where SFEId =" + i +
          " Group by SFEQty"));

        if (v > z)
        {
          ss.Add(s);
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
          err_count++;
        }
      }

      if (err_count > 0) sErr += "\nВ части строк количество к выполнению превышает требуемое (" + err_count + ").";
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
        if(!ssuniq.Add(s)) errs.Add(s);

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

    private bool FillingImportCheckSFEIds(dynamic oSheet, long verid, long execid)
    {
      string sErr = "";
      object o_s;
      string s;
      List<string> ss = new List<string>();
      long z;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFEId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 10 * r / rows, "Проверка идентификаторов строк.");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (s == "") z = 0; // ващет надо было это раньше найти
        else if (!long.TryParse(s, out z)) z = 0; //не число
        else if (z.ToString() != s || z < 0) z = 0; //не положительное целое, еще что-то не так
        if (z>0)
        {
          z = Convert.ToInt64(MyGetOneValue("select count(SFEId) from SpecFillExec " +
            " inner join SpecFill on SFEFill = SFId " +
            " inner join SpecVer on SFSpecVer = SVId " +
            " where SFEId = " + MyES(z) + " and SVId = '" + verid + "' and SFEExec = '" + execid + "'"));
        }

        if (z == 0)
        {
          ss.Add(s);
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, 2).Interior.Color = 0;
          oSheet.Cells(r, 2).Font.Color = -16776961;
          oSheet.Cells(r, 9).Interior.Color = 0;
          oSheet.Cells(r, 9).Font.Color = -16776961;
        }
        else if (z > 1) throw new Exception();
      }

      z = ss.ToArray().Distinct().Count();
      if (z > 0) sErr += "\nВ файле для загрузки не найдена часть идентификаторов строк (" + z + ").";
      if (sErr != "") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      return z==0;
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
        if (FillingReportStructure[c - 1].Nulable == false && s != SpecName)
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

    private bool FillingImportCheckExecName(dynamic oSheet, string ExecName)
    {
      object o_s;
      string s;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 9; // 1-based Исполнитель
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка исполнителя");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (FillingReportStructure[c - 1].Nulable == false && s != ExecName)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Исполнитель в файле (см. столбец <I>) не совпадает с выбранным, «" + ExecName + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      MyExcelCustomReport_Done(EntityId);
      return;
    }

    private void chkDoneMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkDoneMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecFill.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvSpecFill.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;//  "chkDoneMultiline";
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void dgvSpecFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      if(FormIsUpdating) return;
      dgvSpecFill.MySaveColWidthForUser(uid, e);
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

        private void button1_Click(object sender, EventArgs e)
        {
            MyExcelSuperCustomReport_Done(EntityId);
            return;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MsgBox("Инструкция:\n 1.Ввести в соответствующие поля id из колонки 'ID распределения по исполнителям'; \n 2. Нажать кнопку 'Перенести';");
            return;
        }

        private void Exchange_btn_Click(object sender, EventArgs e)
        {
            if(masterID.Text.ToString() == "" || slaveID.Text.ToString() == "" ||
                masterID.Text.ToString() == masterID.Tag.ToString() || slaveID.Text.ToString() == slaveID.Tag.ToString())
            {
                MsgBox("Пожалуйста введите ID в поля \n (см. инструкцию)");
            }
            else
            {
                if (MessageBox.Show("Вы хотите перенести объемы по работе с ID: " + masterID.Text + " в работу с ID: " + slaveID.Text +  ". \nПродолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string exchq = "update Done set DSpecExecFill = " + slaveID.Text + " where DSpecExecFill = " + masterID.Text;
                    MyExecute(exchq);
                    MsgBox("Перенос объемов произошел успешно");
                    return;
                }
            }
        }

        private void masterID_Enter(object sender, EventArgs e)
        {
            if (masterID.Text == masterID.Tag.ToString())
            {
                masterID.Text = "";
            }
            masterID.ForeColor = Color.FromKnownColor(KnownColor.Black);
        }

        private void slaveID_Enter(object sender, EventArgs e)
        {
            if (slaveID.Text == slaveID.Tag.ToString())
            {
                slaveID.Text = "";
            }
            slaveID.ForeColor = Color.FromKnownColor(KnownColor.Black);
        }

        private void masterID_Leave(object sender = null, EventArgs e = null)
        {
            if (masterID.Text == "")
            {
                masterID.Text = masterID.Tag.ToString();
            }
            masterID.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
        }

        private void slaveID_Leave(object sender = null, EventArgs e = null)
        {
            if (slaveID.Text == "")
            {
                slaveID.Text = slaveID.Tag.ToString();
            }
            slaveID.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
        }

        private void masterID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                masterID.Text = masterID.Tag.ToString();
                slaveID.Text = slaveID.Tag.ToString();
                masterID_Leave();
                slaveID_Leave();
                return;
            }
        }

        private void slaveID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                masterID.Text = masterID.Tag.ToString();
                slaveID.Text = slaveID.Tag.ToString();
                masterID_Leave();
                slaveID_Leave();
                return;
            }
        }
    }
}
