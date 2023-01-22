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
  public partial class NZP : UserControl// MyComponent
  {
    public NZP()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "NZP";
    private string FormSqlPref = "NZP";
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;

    private void Done_Load(object sender, EventArgs e)
    {
      LoadMe();
      fill_dgv();
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
      string q = " select distinct SId,STName,SVName,ManagerAO,SState from vwSpec ";

      if (lstExecFilter.GetLstVal() > 0)
      {
        q += "\n inner join (select SESpec from SpecExec where SEExec=" + lstExecFilter.GetLstVal() + ")se on SESpec=SId";
      }

      string sName = txtSpecNameFilter.Text;
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
      {
        q += " inner join (select SVSpec svs from SpecVer left join KS2Doc d on d.KSSpecId = SVSpec" +
              " where SVName like " + MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              " or KSId = " + MyDigitsId(sName) +
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
            /*has to be deleted*/
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
      FillBudges();
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

        public void FillBudges()
        {
            string q = "select BId, BNumber, BStage from budget where BSid = " + EntityId + 
                " order by BId ";
            MyFillDgv(dgv_Budg, q);
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
            if (bNoError && !CheckKoeffs(oSheet)) bNoError = false;



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

        private bool CheckKoeffs(dynamic oSheet)
        {
            string KS2Date, nzpType;
            decimal downKoefSMRPNR, downKoefTMC, koeffDB, koefCheck;

            nzpType = oSheet.Cells(7, 10).Value?.ToString() ?? "";
            downKoefSMRPNR = decimal.Parse(oSheet.Cells(5, 10).Value?.ToString() ?? 0);
            downKoefTMC = decimal.Parse(oSheet.Cells(6, 10).Value?.ToString() ?? 0);

            KS2Date = oSheet.Cells(4, 10).Value?.ToString() ?? "";
            //KS3Date = oSheet.Cells(5, 10).Value?.ToString() ?? "";

            if (KS2Date == "")
            {
                MsgBox("Необходимо заполнить датy!");
                oSheet.Cells(4, 12).Interior.Color = 0;
                oSheet.Cells(4, 12).Font.Color = -16776961;
                return false;
            }

            if (nzpType != "") return true;

            if (MyGetOneValue(" select downKoefSMRPNR + downKoefTMC from NZPDoc where SpecId = " + EntityId) is null) koeffDB = 0;
            else koeffDB = decimal.Parse(MyGetOneValue(" select downKoefSMRPNR + downKoefTMC from NZPDoc where SpecId = " + EntityId).ToString() ?? "0");
            koefCheck = downKoefSMRPNR + downKoefTMC;
            if (koeffDB != 0 && koeffDB != koefCheck)
            {
                MsgBox("Изменение коэффициентов невозможно!");
                oSheet.Cells(5, 10).Interior.Color = 16776961;
                oSheet.Cells(5, 10).Font.Color = -16776961;
                oSheet.Cells(6, 10).Interior.Color = 0;
                oSheet.Cells(6, 10).Font.Color = -16776961;
                return false;
            }

            long budgId = long.Parse(oSheet.Cells(7, 14).Value?.ToString() ?? "0");
            if (budgId == 0)
            {
                MsgBox("Необходимо указать ID сметы!");
                oSheet.Cells(7, 14).Interior.Color = 0;
                oSheet.Cells(7, 14).Font.Color = -16776961;
                return false;
            }
            string tmp, tmp2;
            int r = 25;
            tmp = oSheet.Cells(r, 3).Value?.ToString() ?? "";
            while ((oSheet.Cells(r, 3).Value?.ToString() ?? "") != "") //до пустой строки
            {
                tmp2 = oSheet.Cells(r, 3).Value?.ToString() ?? "";
                if (tmp != tmp2)
                {
                    MsgBox("Загрузка возможна только по одному исполнителю!");
                    oSheet.Cells(r, 3).Interior.Color = 0;
                    oSheet.Cells(r, 3).Font.Color = -16776961;
                    return false;
                }
                    r++;
            }
                return true;
        }
    private bool ImportDone_Check(dynamic oSheet)
    {

      string s, sss;
      bool b=true;

      //номер
      s = oSheet.Cells(4,10).Value?.ToString() ?? "";
      sss = oSheet.Cells(5, 10).Value?.ToString() ?? "";
      if (s == "" || sss == "")
      {
        oSheet.Cells(4, 10).Font.Color = -16776961;
        oSheet.Cells(4, 10).Interior.Color = 0;
        MsgBox("Отсутствует номер расчета НЗП");
        return false;
      }
      //шифр + версия
      s = oSheet.Cells(1, 10).Value?.ToString() ?? "";
      string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + EntityId).ToString();
      if (s != sSpecInfo)
      {
        oSheet.Cells(1, 10).Font.Color = -16776961;
        oSheet.Cells(1, 10).Interior.Color = 0;
        MsgBox("Шифр или версия указан не верно.\n\nДолжно быть: " + sSpecInfo);
        return false;
      }

      int r = 24;

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
        for (int i = 0; i < 1; i++) //первые столбцы должны быть заполнены
        {
          if (i != 5)
          {
            if (ss[i] == "")
            {
              b = false;
              oSheet.Cells(r, i+1).Font.Color = -16776961;
              oSheet.Cells(r, i+1).Interior.Color = 0;
            }
          }
        }

        r++;
        GetDataRowFromFile(oSheet, r, out ss, out qty_total, out qty_new);
      }
      if (!b) MsgBox("Данные не соответствуют выбранному шифру и исполнителю.\n\nКрасным шрифтом выделены строки с ошибками,\nчерный фон указывает на необходимость внести данные.");
      return b;
    }
        private void btnExport_Click(object sender, EventArgs e)
        {
            MyExcelNZPReport(EntityId);
            return;
        }

        private void GetDataRowFromFile(dynamic oSheet, int r, out string[] ss, out decimal qty_total, out decimal qty_new)
    {
      ss = new string[6];
      for (int i = 0; i < 6; i++) {
        ss[i] = oSheet.Cells(r, i+1).Value?.ToString() ?? "";
      }

      qty_total = 0;
      //string s = oSheet.Cells(r, 7).Value?.ToString() ?? 0;
      try { qty_total = decimal.Parse(oSheet.Cells(r, 13).Value?.ToString() ?? 0); }
      catch { }

      qty_new = 0;
      //s = oSheet.Cells(r, 9).Value?.ToString() ?? 0;
      try { qty_new = decimal.Parse(oSheet.Cells(r, 14).Value?.ToString() ?? 0); }
      catch { }

      return;
    }

    private void FillingImportData(dynamic oSheet)
    {
            long specFillExec, iId;
            long budgId = long.Parse(oSheet.Cells(7, 14).Value?.ToString() ?? "0");
            decimal dQty;
            string CalcNZPNum,MntMaster,note,docIns,nzpType;
            DateTime CalcNZPDate;
            decimal ZP, EM, ZPm, TMC, DTMC, HPotZP, SPotZP, HPandSPotZPm, ZTR, downKoefSMRPNR, downKoefTMC;
            int r = 24; //the row where input data begins
            DateTime dt;
            string specName;
            nzpType = oSheet.Cells(7, 10).Value?.ToString() ?? "";
            try
            {
                CalcNZPDate = DateTime.Parse(oSheet.Cells(4, 12).Value?.ToString() ?? 0);
            }
            catch
            {
                CalcNZPDate = DateTime.Now;
            }
            specName = oSheet.Cells(1, 10).Value.ToString();

            if (nzpType == "")
            {
                ZP = decimal.Parse(oSheet.Cells(12, 10).Value?.ToString() ?? "1");
                EM = decimal.Parse(oSheet.Cells(13, 10).Value?.ToString() ?? "1");
                ZPm = decimal.Parse(oSheet.Cells(14, 10).Value?.ToString() ?? "1");
                TMC = decimal.Parse(oSheet.Cells(15, 10).Value?.ToString() ?? "1");
                DTMC = decimal.Parse(oSheet.Cells(16, 10).Value?.ToString() ?? "1");
                HPotZP = decimal.Parse(oSheet.Cells(17, 10).Value?.ToString() ?? "1");
                SPotZP = decimal.Parse(oSheet.Cells(18, 10).Value?.ToString() ?? "1");
                HPandSPotZPm = decimal.Parse(oSheet.Cells(19, 10).Value?.ToString() ?? "1");
                ZTR = decimal.Parse(oSheet.Cells(20, 10).Value?.ToString() ?? "1");
                downKoefSMRPNR = decimal.Parse(oSheet.Cells(5, 10).Value?.ToString() ?? "1");
                downKoefTMC = decimal.Parse(oSheet.Cells(6, 10).Value?.ToString() ?? "1");
                CalcNZPNum = oSheet.Cells(4, 10).Value?.ToString() ?? "";
                MntMaster = oSheet.Cells(5, 12).Value?.ToString() ?? "";
                docIns = " insert into NZPDoc ( ZP, EM, ZPm, TMC, DTMC, HPotZP, SPotZP, HPandSPotZPm, ZTR, downKoefSMRPNR, downKoefTMC" +
                ", CalcNZPNum, CalcNZPDate, SpecId, BudgID, MntMaster) " +
                " values (" + MyES(ZP) + "," + MyES(EM) + "," + MyES(ZPm) + "," + MyES(TMC) + "," + MyES(DTMC) + ","
                + MyES(HPotZP) + "," + MyES(SPotZP) + "," + MyES(HPandSPotZPm) + "," + MyES(ZTR) + "," + MyES(downKoefSMRPNR) + ","
                + MyES(downKoefTMC) + "," + MyES(CalcNZPNum) + ","
                + MyES(CalcNZPDate) + "," + MyES(EntityId) + "," + MyES(budgId) + "," + MyES(MntMaster) + ");  " +
                " Select SCOPE_IDENTITY() as new_id; ";
                long newId = long.Parse(MyGetOneValue(docIns).ToString());
                string fillIns = " insert into NZPFill (NZPId, SpecFillId, NFSum, SpecFillExecId, NFNote) Values\n";

                while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
                {
                    dQty = 0;
                    //kost = 
                    MyProgressUpdate(pb, 80, "Формирование запросов");
                    iId = long.Parse(oSheet.Cells(r, 1).Value);
                    specFillExec = long.Parse(oSheet.Cells(r, 2).Value);
                    note = oSheet.Cells(r, 16).Value?.ToString() ?? "";
                    try { dQty = decimal.Parse(oSheet.Cells(r, 14).Value?.ToString() ?? 0); }
                    catch { }
                    fillIns += "(" + newId + "," + iId + "," + MyES(dQty) + "," + specFillExec + ",'" + note + "') ,";
                    r++;
                }

                fillIns = fillIns.Substring(0, fillIns.Length - 1);
                MyProgressUpdate(pb, 95, "Импорт данных");
                MyExecute(fillIns);
                //ДОБАВИТЬ ЛОГИРОВНИЕ!!!!!!!
                return;
            }
            else if(nzpType == "M" || nzpType == "m" || nzpType == "М" || nzpType == "м") //на всякий на ру и анг
            {
                CalcNZPNum = oSheet.Cells(4, 10).Value?.ToString() ?? "";
                MyExecute("NZP_minus_doc " + CalcNZPNum + ",'" + CalcNZPDate + "'");
                return;
            }
            else if(nzpType == "C" || nzpType == "c" || nzpType == "С" || nzpType == "с") //на всякий на ру и анг
            {
                ZP = decimal.Parse(oSheet.Cells(12, 10).Value?.ToString() ?? "1");
                EM = decimal.Parse(oSheet.Cells(13, 10).Value?.ToString() ?? "1");
                ZPm = decimal.Parse(oSheet.Cells(14, 10).Value?.ToString() ?? "1");
                TMC = decimal.Parse(oSheet.Cells(15, 10).Value?.ToString() ?? "1");
                DTMC = decimal.Parse(oSheet.Cells(16, 10).Value?.ToString() ?? "1");
                HPotZP = decimal.Parse(oSheet.Cells(17, 10).Value?.ToString() ?? "1");
                SPotZP = decimal.Parse(oSheet.Cells(18, 10).Value?.ToString() ?? "1");
                HPandSPotZPm = decimal.Parse(oSheet.Cells(19, 10).Value?.ToString() ?? "1");
                ZTR = decimal.Parse(oSheet.Cells(20, 10).Value?.ToString() ?? "1");
                CalcNZPNum = oSheet.Cells(4, 10).Value?.ToString() ?? "";
                string updq = "update NZPDoc SET " +
                              "  ZP = " + ZP +
                              ", EM = " + EM +
                              ", ZPm = " + ZPm +
                              ", TMC = " + TMC +
                              ", DTMC = " + DTMC +
                              ", HPotZP = " + HPotZP +
                              ", SPotZP = " + SPotZP +
                              ", HPandSPotZPm = " + HPandSPotZPm +
                              ", ZTR = " + ZTR +
                              " where CalcNZPNum = '" + CalcNZPNum + "'";
                MyExecute(updq);
                return;
            }
            else if (nzpType == "K" || nzpType == "k" || nzpType == "К" || nzpType == "к")
            {
                ZP = decimal.Parse(oSheet.Cells(12, 10).Value?.ToString() ?? "1");
                EM = decimal.Parse(oSheet.Cells(13, 10).Value?.ToString() ?? "1");
                ZPm = decimal.Parse(oSheet.Cells(14, 10).Value?.ToString() ?? "1");
                TMC = decimal.Parse(oSheet.Cells(15, 10).Value?.ToString() ?? "1");
                DTMC = decimal.Parse(oSheet.Cells(16, 10).Value?.ToString() ?? "1");
                HPotZP = decimal.Parse(oSheet.Cells(17, 10).Value?.ToString() ?? "1");
                SPotZP = decimal.Parse(oSheet.Cells(18, 10).Value?.ToString() ?? "1");
                HPandSPotZPm = decimal.Parse(oSheet.Cells(19, 10).Value?.ToString() ?? "1");
                ZTR = decimal.Parse(oSheet.Cells(20, 10).Value?.ToString() ?? "1");
                CalcNZPNum = oSheet.Cells(4, 10).Value?.ToString() ?? "";
                downKoefSMRPNR = decimal.Parse(oSheet.Cells(5, 10).Value?.ToString() ?? "1");
                downKoefTMC = decimal.Parse(oSheet.Cells(6, 10).Value?.ToString() ?? "1");
                MntMaster = oSheet.Cells(5, 12).Value?.ToString() ?? "";
                ZP *= -1;
                EM *= -1;
                ZPm *= -1;
                TMC *= -1;
                DTMC *= -1;
                HPotZP *= -1;
                SPotZP *= -1;
                HPandSPotZPm *= -1;
                ZTR *= -1;
                docIns = " insert into NZPDoc ( ZP, EM, ZPm, TMC, DTMC, HPotZP, SPotZP, HPandSPotZPm, ZTR, downKoefSMRPNR, downKoefTMC" +
                ", CalcNZPNum, CalcNZPDate, SpecId, BudgID, MntMaster) " +
                " values (" + MyES(ZP) + "," + MyES(EM) + "," + MyES(ZPm) + "," + MyES(TMC) + "," + MyES(DTMC) + ","
                + MyES(HPotZP) + "," + MyES(SPotZP) + "," + MyES(HPandSPotZPm) + "," + MyES(ZTR) + "," + MyES(downKoefSMRPNR) + ","
                + MyES(downKoefTMC) + "," + MyES("K-" + CalcNZPNum) + ","
                + MyES(CalcNZPDate) + "," + MyES(EntityId) + "," + MyES(budgId) + "," + MyES(MntMaster) + ");  " +
                " Select SCOPE_IDENTITY() as new_id; ";
                long newId = long.Parse(MyGetOneValue(docIns).ToString());
                
                long DoneHeaderId = long.Parse(MyGetOneValue("insert into DoneHeader (DHDate,DHSpecTitle) values (" + MyES(CalcNZPDate) + "," + MyES(specName) + "); Select SCOPE_IDENTITY() as new_id;").ToString());

                string fillDoneRowsq = "insert into Done (DSpecExecFill,DQty,DDate,DHeader,DCaption) Values\n";
                string fillIns = " insert into NZPFill (NZPId, SpecFillId, NFSum, SpecFillExecId, NFNote) Values\n";

                while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
                {
                    dQty = 0;
                    //kost = 
                    MyProgressUpdate(pb, 80, "Формирование запросов");
                    iId = long.Parse(oSheet.Cells(r, 1).Value);
                    specFillExec = long.Parse(oSheet.Cells(r, 2).Value);
                    note = oSheet.Cells(r, 16).Value?.ToString() ?? "";
                    try { dQty = decimal.Parse(oSheet.Cells(r, 14).Value?.ToString() ?? 0); dQty *= -1; } //make dQty negative
                    catch { }
                    fillIns += "(" + newId + "," + iId + "," + MyES(dQty) + "," + specFillExec + ",'" + note + "') ,";

                    fillDoneRowsq += "(" + specFillExec + "," + MyES(dQty) + "," + MyES(CalcNZPDate) + "," + DoneHeaderId + ",'')\n,";
                    r++;
                }

                fillIns = fillIns.Substring(0, fillIns.Length - 1);
                fillDoneRowsq = fillDoneRowsq.Substring(0, fillDoneRowsq.Length - 1);
                MyProgressUpdate(pb, 95, "Импорт данных");
                MyExecute(fillIns);
                MyExecute(fillDoneRowsq);
                //ДОБАВИТЬ ЛОГИРОВНИЕ!!!!!!!
                return;
            }
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

        private void button2_Click(object sender, EventArgs e)//import
        {
            OpenFileDialog ofd = new OpenFileDialog();

            bool bNoError = true;
            var f = string.Empty;
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
            /*if (bNoError && !ImportDone_Check(oSheet)) bNoError = false;
            if (bNoError) MyExcelUnmerge(oSheet);
            if (bNoError && !MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb)) bNoError = false;
            if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
            if (bNoError && !FillingImportCheckExecName(oSheet, lstExecFilter.GetLstText())) bNoError = false;
            if (bNoError && !FillingImportCheckSFEIds(oSheet, SpecVer,lstExecFilter.GetLstVal())) bNoError = false;
            if (bNoError && !FillingImportCheckIdsUniq(oSheet)) bNoError = false;
            if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false;
            if (bNoError && !CheckKoeffs(oSheet)) bNoError = false;*/



            if (bNoError)
            {
                if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
                    , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FillingImportDocData(oSheet);
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

        private void FillingImportDocData(dynamic oSheet)
        {
            string NZPNote;

            int r = 7;
            while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
            {
                string docUpd;
                string NZPId = oSheet.Cells(r, 1).Value.ToString();
                NZPNote = oSheet.Cells(r, 24).Value?.ToString() ?? "";
                docUpd = " update NZPDoc " +
                         " set Note = " + NZPNote +"" +
                         " where NZPId = " + NZPId + ";";
                MyExecute(docUpd);
                r++;
            }
            MyProgressUpdate(pb, 95, "Импорт данных");
            //MyLog(uid, "KS2", 120, DoneHeaderId, EntityId); ДОБАВИТЬ ЛОГИРОВНИЕ!!!!!!!
            return;
        }

        private void button1_Click(object sender, EventArgs e) //export
        {
            MyExecNZPDocReport();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MsgBox("Инструкция:\n Поле 'Тип НЗП' является нееобязательным для заполнения и используется в следующих случаях:\n 1. Для обнуления ранее загруженного НЗП (необходимо ввести номер НЗП и букву М в поле с типом); \n " +
                "2. Для корректировки ранее загруженного НЗП (необходимо ввести номер НЗП, букву К в поле с типом, объемы в позиции, которые необходимо скорректировать, также возможно внесение цифр в шапке формы); \n " +
                "3. Для корректировки цифр в шапке ранее загруженного НЗП (необходимо ввести номер НЗП, букву С в поле с типом и корректные цифры в шапке формы); \n" +
                "P.S.: Все цифры пишутся положительными, при инспользовании 1 и 2 пунктов возможно введение даты, если дата пустая - будет назначена текущая дата.");
            return;
        }
    }
}
