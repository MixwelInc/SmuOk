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
  public partial class KS2 : UserControl// MyComponent
  {
    public KS2()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "KS2";
    private string FormSqlPref = "KS";
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
            string KS2Date, KS3Date;
            decimal downKoefSMRPNR, downKoefTMC, downKoefVZIS, subDownKoefSMRPNR, subDownKoefTMC, koeffDB, koefCheck;
            downKoefSMRPNR = decimal.Parse(oSheet.Cells(6, 10).Value?.ToString() ?? 0);
            downKoefTMC = decimal.Parse(oSheet.Cells(7, 10).Value?.ToString() ?? 0);
            downKoefVZIS = decimal.Parse(oSheet.Cells(8, 10).Value?.ToString() ?? 0);

            if (MyGetOneValue(" select downKoefSMRPNR + downKoefTMC + downKoefVZIS from KS2Doc where KSSpecId = " + EntityId) is null) koeffDB = 0;
            else koeffDB = decimal.Parse(MyGetOneValue(" select downKoefSMRPNR + downKoefTMC + downKoefVZIS from KS2Doc where KSSpecId = " + EntityId).ToString() ?? "0");
            koefCheck = downKoefSMRPNR + downKoefTMC + downKoefVZIS;
            if (koeffDB != 0 && koeffDB != koefCheck)
            {
                MsgBox("Изменение коэффициентов невозможно!");
                oSheet.Cells(6, 10).Interior.Color = 16776961;
                oSheet.Cells(6, 10).Font.Color = -16776961;
                oSheet.Cells(7, 10).Interior.Color = 0;
                oSheet.Cells(7, 10).Font.Color = -16776961;
                oSheet.Cells(8, 10).Interior.Color = 0;
                oSheet.Cells(8, 10).Font.Color = -16776961;
                return false;
            }

            KS2Date = oSheet.Cells(4, 10).Value?.ToString() ?? "";
            KS3Date = oSheet.Cells(5, 10).Value?.ToString() ?? "";

            if(KS2Date == "" || KS3Date == "")
            {
                MsgBox("Необходимо заполнить даты!");
                oSheet.Cells(4, 12).Interior.Color = 0;
                oSheet.Cells(4, 12).Font.Color = -16776961;
                oSheet.Cells(5, 12).Interior.Color = 0;
                oSheet.Cells(5, 12).Font.Color = -16776961;
                return false;
            }
            long budgId = long.Parse(oSheet.Cells(8, 14).Value?.ToString() ?? "0");
            if (budgId == 0)
            {
                MsgBox("Необходимо указать ID сметы!");
                oSheet.Cells(8, 12).Interior.Color = 0;
                oSheet.Cells(8, 12).Font.Color = -16776961;
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
      s = oSheet.Cells(4, 10).Value?.ToString() ?? "";
      sss = oSheet.Cells(5, 10).Value?.ToString() ?? "";
      if (s == "" || sss == "")
      {
        oSheet.Cells(4, 10).Font.Color = -16776961;
        oSheet.Cells(4, 10).Interior.Color = 0;
        oSheet.Cells(5, 10).Font.Color = -16776961;
        oSheet.Cells(5, 10).Interior.Color = 0;
        MsgBox("Отсутствует номер КС2 или КС3");
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

      int r = 25;

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
            MyExcelKS2Report_Done(EntityId);
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
      long iId;
            long specFillExec;
      decimal dQty;
      DateTime dt;
            string s, q;
            string KS2Num, KS3Num, subSMRPNR, subTMC;
            DateTime KS2Date, KS3Date;
            decimal KS2withKeq1, ZP, EM, ZPm, TMC, DTMC, HPotZP, SPotZP, HPandSPotZPm, KZPandZPM, VZIS, downKoefSMRPNR, downKoefTMC, downKoefVZIS, subDownKoefSMRPNR, subDownKoefTMC, koefCheck, koeffDB;
            //KS2withKeq1 = decimal.Parse(oSheet.Cells(11, 6).Value?.ToString() ?? 0);
            ZP = decimal.Parse(oSheet.Cells(13, 10).Value?.ToString() ?? 0);
            EM = decimal.Parse(oSheet.Cells(14, 10).Value?.ToString() ?? 0);
            ZPm = decimal.Parse(oSheet.Cells(15, 10).Value?.ToString() ?? 0);
            TMC = decimal.Parse(oSheet.Cells(16, 10).Value?.ToString() ?? 0);
            DTMC = decimal.Parse(oSheet.Cells(17, 10).Value?.ToString() ?? 0);
            HPotZP = decimal.Parse(oSheet.Cells(18, 10).Value?.ToString() ?? 0);
            SPotZP = decimal.Parse(oSheet.Cells(19, 10).Value?.ToString() ?? 0);
            HPandSPotZPm = decimal.Parse(oSheet.Cells(20, 10).Value?.ToString() ?? 0);
            KZPandZPM = (ZP + ZPm) * 0.15m;
            VZIS = decimal.Parse(oSheet.Cells(21, 10).Value?.ToString() ?? 0);
            downKoefSMRPNR = decimal.Parse(oSheet.Cells(6, 10).Value?.ToString() ?? 0);
            downKoefTMC = decimal.Parse(oSheet.Cells(7, 10).Value?.ToString() ?? 0);
            downKoefVZIS = decimal.Parse(oSheet.Cells(8, 10).Value?.ToString() ?? 0);
            subSMRPNR = oSheet.Cells(6, 14).Value?.ToString() ?? "";
            subTMC = oSheet.Cells(7, 14).Value?.ToString() ?? "";
            KS2withKeq1 = ZP + EM + TMC + HPotZP + SPotZP + HPandSPotZPm;
            if (subSMRPNR == "") subDownKoefSMRPNR = 0;
            else subDownKoefSMRPNR = decimal.Parse(subSMRPNR);
            if (subTMC == "") subDownKoefTMC = 0;
            else subDownKoefTMC = decimal.Parse(subTMC);
            KS2Num = oSheet.Cells(4, 10).Value?.ToString() ?? 0;
            KS3Num = oSheet.Cells(5, 10).Value?.ToString() ?? 0;
            KS2Date = DateTime.Parse(oSheet.Cells(4, 12).Value?.ToString() ?? 0);
            KS3Date = DateTime.Parse(oSheet.Cells(5, 12).Value?.ToString() ?? 0);/////////////////////////
            long budgId = long.Parse(oSheet.Cells(8, 14).Value?.ToString() ?? "0");
            long KSExec = long.Parse(MyGetOneValue("Select EId from Executor e where e.EName='"+ oSheet.Cells(25, 3).Value.ToString() + "'").ToString());
            string docIns;

                docIns = " insert into KS2Doc ( KS2withKeq1, ZP, EM, ZPm, TMC, DTMC, HPotZP, SPotZP, HPandSPotZPm, KZPandZPM, VZIS, downKoefSMRPNR, downKoefTMC, downKoefVZIS, subDownKoefSMRPNR, subDownKoefTMC, KS2Num, KS3Num, KS2Date, KS3Date, KSSpecId, KSBudgID, KSExec) " +
                " values (" + MyES(KS2withKeq1) + "," + MyES(ZP) + "," + MyES(EM) + "," + MyES(ZPm) + "," + MyES(TMC) + "," + MyES(DTMC) + "," + MyES(HPotZP) + "," + MyES(SPotZP) + "," + MyES(HPandSPotZPm) +
                "," + MyES(KZPandZPM) + "," + MyES(VZIS) + "," + MyES(downKoefSMRPNR) + "," + MyES(downKoefTMC) + "," + MyES(downKoefVZIS) + "," + MyES(subDownKoefSMRPNR) + "," + MyES(subDownKoefTMC) + ",'" + KS2Num + "','" + KS3Num + "','" + KS2Date + "','" + KS3Date + "'," + EntityId + "," + budgId + ","+ KSExec +");  " +
                " Select SCOPE_IDENTITY() as new_id; ";
            long newId = long.Parse(MyGetOneValue(docIns).ToString());
            MyLog(uid, "KS2", 2021, newId, EntityId);
            string fillIns = " insert into KS2 (KSId, KSSpecFillId, KSSum, KSTotal, KSSpecFillExec, KSNum) Values\n";

      int r = 25;
      while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
      {
                dQty = 0;
                //kost = 
                MyProgressUpdate(pb, 80, "Формирование запросов");
                iId = long.Parse(oSheet.Cells(r, 1).Value);
                specFillExec = long.Parse(oSheet.Cells(r, 2).Value);
                try { dQty = decimal.Parse(oSheet.Cells(r, 14).Value?.ToString() ?? 0); }
                catch { }
                fillIns += "(" + newId + "," + iId + "," + MyES(dQty) + "," + MyES(KS2withKeq1) + "," + specFillExec + ",'" + KS2Num + "') ,";
                r++;
                MyLog(uid, "KS2", 2022, iId, newId);
      }

      fillIns=fillIns.Substring(0, fillIns.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(fillIns);
      //MyLog(uid, "KS2", 120, DoneHeaderId, EntityId); ДОБАВИТЬ ЛОГИРОВНИЕ!!!!!!!
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
            long ksId = 0;
            string KS3VahtNum, KS3ImportNum, SubContractNum, month;
            decimal vahtSum;
            string docUpd = " insert into KS2Doc (KSId, KS3VahtNum, KSVahtSum, subMonth, KS3ImportNum, SubContractNum) Values\n";
            string qdel = "";

            int r = 6;
            while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
            {
                docUpd = "";
                vahtSum = 0;
                ksId = long.Parse(oSheet.Cells(r, 1).Value.ToString());
                KS3VahtNum = oSheet.Cells(r, 25).Value?.ToString() ?? "";
                vahtSum = decimal.Parse(oSheet.Cells(r, 26).Value?.ToString() ?? "0");
                month = oSheet.Cells(r, 28).Value?.ToString() ?? ""; 
                KS3ImportNum = oSheet.Cells(r, 29).Value?.ToString() ?? ""; 
                SubContractNum = oSheet.Cells(r, 30).Value?.ToString() ?? "";
                MyProgressUpdate(pb, 80, "Формирование запросов");
                try { vahtSum = decimal.Parse(oSheet.Cells(r, 26).Value?.ToString() ?? "0"); }
                catch { }
                //qdel = "delete from KS2Doc where KSId = " + ksId;
                //MyExecute(qdel);
                docUpd = " update KS2Doc set " +
                    " KS3VahtNum = '" + KS3VahtNum +
                    "' ,KSVahtSum = " + MyES(vahtSum) +
                    " ,subMonth = '" + month +
                    "' ,KS3ImportNum = '" + KS3ImportNum +
                    "' ,SubContractNum = '" + SubContractNum + "' where KSId = " + ksId + ";";
                MyExecute(docUpd);
                r++;
            }
            MyProgressUpdate(pb, 95, "Импорт данных");
            //MyLog(uid, "KS2", 120, DoneHeaderId, EntityId); ДОБАВИТЬ ЛОГИРОВНИЕ!!!!!!!
            return;
        }

        private void button1_Click(object sender, EventArgs e) //export
        {
            MyExecKS2DocReport();
        }

    }
}
