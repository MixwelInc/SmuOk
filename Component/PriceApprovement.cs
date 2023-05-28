using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;
using Microsoft.Win32;

namespace SmuOk.Component
{
  public partial class PriceApprovement : UserControl
  {
    public PriceApprovement()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private long EntityId = -1;
    private long SpecVer = -1;
        int shit = 0;
        bool flag = false;
        private List<MyXlsField> FillingReportStructure;
    List<long> ImportLst_SId = new List<long>();
    List<long> ImportLst_SpecVer = new List<long>();

    private void PriceApprovement_Load(object sender, EventArgs e)
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
            else if (Convert.ToInt32(dgvSpec.Rows[e.RowIndex].Cells["dgv_SState"].Value) == 2)
            {
                dgvSpec.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }
        }

        public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("PriceApprovement");
      FillFilter();

      SpecList_RestoreColunns(dgvSpec);

      FormIsUpdating = false;
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      //MyFillList(lstExecFilter, "select eid, ename from (select -1 eid,'<не выбран>' ename, -1 ESmuDept union select EId, EName, ESmuDept from Executor)s order by case when ESmuDept=0 then 999999 else ESmuDept end;", "(исполнитель)");
      //MyFillList(lstExecFilter, "select EId, EName from Executor inner join UserExec on UEExec = EId where UEUser = " + (IsDebugComputer() ? 1 : uid) + " order by case when ESmuDept = 0 then 999999 else ESmuDept end;");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }
        private bool check_is_lst(string str)
        {
            int n;
            foreach (char c in str)
            {
                if (c == ',')
                {
                    return true;
                }
            }
            bool isNumeric = int.TryParse(str, out n);
            if (isNumeric)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    private void fill_dgv()
    {
      string q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO,vws.SState from vwSpec vws  inner join vwSpecFill vwsf on vwsf.SId = vws.SId " +
                 "left join BudgetFill bf on bf.SpecFillId = vwsf.SFId " +
                 "left join InvCfm ic on ic.ICFill = vwsf.SFId " +
                 "left join InvDoc id on id.InvId = ic.InvDocId " +
                 "left join Budget b on b.BId = bf.BudgId " +
                 "left join SpecFillBol sfb2 on sfb2.SFBFill = vwsf.SFId ";
      string sName;

      /*if (lstExecFilter.GetLstVal() > 0)
      {
        q += "\n inner join (select SESpec from SpecExec where SEExec=" + lstExecFilter.GetLstVal() + ")se on SESpec=SId";
      }*/

      sName = txtSpecNameFilter.Text;
        if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
        {
            bool is_lst = check_is_lst(sName);
            if (is_lst)
            {
                q += " inner join (select SVSpec svs from SpecVer " +
                    " where SVSpec in (" + sName +
                    "))q on svs=vws.SId";
            }
            else
            {
                q += " inner join (select SVSpec svs from SpecVer " +
                    " where SVName like " + MyES(sName, true) + ")q on svs=vws.SId";
            }
        }

      q += " where pto_block=1 and vws.SType != 6 and (ic.ICId is not NULL or sfb2.SFBId is not NULL) ";

      long f = lstSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and vws.STId=" + f;

      if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstSpecUserFilter.GetLstVal() > 0) q += "and SUser=" + lstSpecUserFilter.GetLstVal();
      else if (lstSpecUserFilter.GetLstVal() == -1) q += "and SUser=0";

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      MyFillDgv(dgvSpec, q);
            //FormIsUpdating = true;
            /*foreach (DataGridViewRow row in dgvSpec.Rows)
                if (Convert.ToInt32(row.Cells["dgv_SState"].Value) == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }

            foreach(DataGridViewRow row in dgvSpec.Rows)
                if (row.DefaultCellStyle.BackColor == Color.LightCoral)
                {
                    MsgBox("kek");
                }*/
            //FormIsUpdating = false;
            /*shit = ImportLst_SId.Count;
            foreach (DataGridViewRow item in dgvSpec.Rows)
            {
                //if (item.Cells[1].Value.GetType == Type.) continue;
                if (ImportLst_SId.Contains((long)item.Cells["dgv_SId"].Value))
                {
                    shit--;
                    flag = true;
                    item.Cells[0].Value = true;
                }
                else continue;
            }
            if (dgvSpec.Rows.Count == 0) NewEntity();
      else dgvSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));*/
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

    private void dgvSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0) return;
      if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "dgv_S_btn_folder") MyOpenSpecFolder(EntityId);
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
      string q = "select distinct cast(vwsf.SFNo as bigint) as SFNo, cast(vwsf.SFNo2 as bigint) as SFNo2, " +
                /*"select distinct case IsNumeric(vwsf.SFNo) when 1 then Replicate('0', 10 - Len(vwsf.SFNo)) + vwsf.SFNo else vwsf.SFNo end as SFNo, " +
                "case IsNumeric(vwsf.SFNo2) when 1 then Replicate('0', 10 - Len(vwsf.SFNo2)) + vwsf.SFNo2 else vwsf.SFNo2 end SFNo2, " +*/
                "isnull(BFCode, null) BFCode, vwsf.SFName as SFName, coalesce(sfb2.SFBName,ICName) as NameFromSmth, vwsf.SFUnit as Unit, vwsf.SFQty as ProjQty, " +
                    "BFKoeff, case when isnull(BFQty,0) != 0 then cast(BFSum / BFQty as decimal(18, 4)) else null end as BFPrc, ICK, coalesce(sfb2.SFBPriceWONDS, ic.ICPrc) PrcFromSmth, " +
                    "'№ ' + coalesce(sfb2.SFBBoLNoFromTSK, InvNum) + ' от ' + cast(convert(date, coalesce(sfb2.SFBBoLDateFromTSK, InvDate), 106) as nvarchar) MinCostReason " +
                    "from vwSpecFill vwsf " +
                    "left join BudgetFill bf on bf.SpecFillId = vwsf.SFId " +
                    "left join InvCfm ic on ic.ICFill = vwsf.SFId " +
                    "left join InvDoc id on id.InvId = ic.InvDocId " +
                    "left join Budget b on b.BId = bf.BudgId " +
                    "left join SpecFillBol sfb2 on sfb2.SFBFill = vwsf.SFId " +
                    "where 1 = 1 and (ic.ICId is not NULL or sfb2.SFBId is not NULL) and vwsf.SVId=" + SpecVer.ToString() +
                    " order by cast(vwsf.SFNo as bigint), cast(vwsf.SFNo2 as bigint) ";
            MyFillDgv(dgvSpecFill, q);
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
          /*"	where SFEExec=" + lstExecFilter.GetLstVal() +*/
          "	group by SFSpecVer " +
          ")ff " +
          "	on SFSpecVer=SVId " +
          "	Where SVId= " + SpecVer.ToString();
      string s = (string)MyGetOneValue(q);
      SpecInfo.Text = s;
    }

    private bool FillingImportCheckValues(dynamic oSheet)
    {
      string s;
      long rl; decimal rd; DateTime dt; bool b;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
            //List<object> fr = new List<object>();
            //List<object> srr = new List<object>();
            bool sr = false;
            bool fr = false;
            string letterNum = "";
            string letterDate = "";
            //int c = range.Columns.Count;
      MyProgressUpdate(pb, 20, "Проверка данных");
      if (rows > 1000)
      {
        if (MessageBox.Show("Файл содержит " + rows.ToString() + " строк. Продолжить?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        {
          return false;
        }
      }

      if (rows == 1)
      {
        MsgBox("Файл нужно заполнить.");
        return false;
      }

      //oSheet.Cells.Interior.Color = xlWhite;
      //oSheet.Cells.Font.Color = xlBlack;

      for (int r = 2; r < rows + 1; r++)
      {
                b = true;
        MyProgressUpdate(pb, 20 + 10 * r / rows, "Проверка данных");
                s = oSheet.Cells(r, 5).Value?.ToString() ?? "";
                if (s == "Исходящий номер письма")
                {
                    letterNum = oSheet.Cells(r, 7).Value?.ToString() ?? "";
                    letterDate = oSheet.Cells(r + 1, 7).Value?.ToString() ?? "";
                    if (letterNum == "нет" || letterNum == "Нет")
                    {
                        break;
                    }
                    else if (letterNum == "")
                    {
                        b = false;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }

          if (!b)
          {
            e = true;
            oSheet.Cells(r, 7).Interior.Color = xlPink;
            oSheet.Cells(r, 7).Font.Color = xlRed;
          }
      }
      if (e) MsgBox("Не заданы корректные значения для обязательных полей.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

        private void dgvSpec_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dgvSpec.Rows.Count; i++)
            {
                long val = (long)dgvSpec.Rows[i].Cells["dgv_SId"].Value;
                if (!ImportLst_SId.Contains(val))
                {
                    if (dgvSpec.Rows[i].Cells[0].Value == "true")
                    {
                        ImportLst_SId.Add(val);
                        ImportLst_SpecVer.Add((long)(MyGetOneValue("select SVId from vwSpec where SId=" + val) ?? -1));
                        flag = false;
                    }
                }
                if (ImportLst_SId.Contains(val) && dgvSpec.Rows[i].Cells[0].Value != "true" && shit == 0 && !flag)
                {
                    ImportLst_SId.Remove(val);
                    ImportLst_SpecVer.Remove((long)(MyGetOneValue("select SVId from vwSpec where SId=" + val) ?? -1));
                }
            }
            return;
        }

        private void btnImportMany_Click(object sender, EventArgs e)
        {
            if (dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral || dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
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

            MyProgressUpdate(pb, 10, "Открываем файл");

            dynamic oSheet = oBook.Worksheets(1);
            /*if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb)) bNoError = false;   //FillingImportCheckTitle(oSheet)) bNoError = false;
            MyExcelUnmerge(oSheet);*/
            if (bNoError && !FillingImportCheckValues(oSheet)) bNoError = false;
            //if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
            //if (bNoError && !FillingImportCheckSVIds(oSheet, SpecVer)) bNoError = false;
            //if (bNoError && !FillingImportCheckSums(oSheet, SpecVer)) bNoError = false;

            if (bNoError)
            {
                if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
                    , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FillingImportManyData(oSheet);
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

        private void FillingImportManyData(dynamic oSheet)
        {
            long iId;
            string q, ICId, sfbid, bfid, code, budg_price_str, fin_str_num;

            q = "";
            string s;
            string letterNum = "";
            string letterDate = "";
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;

            for (int k = 2; k < rows + 1; k++)
            {
                s = oSheet.Cells(k, 5).Value?.ToString() ?? "";
                if (s == "Исходящий номер письма")
                {
                    letterNum = oSheet.Cells(k, 7).Value?.ToString() ?? "";
                    letterDate = oSheet.Cells(k + 1, 7).Value?.ToString() ?? "";
                    break;
                }
                else
                {
                    continue;
                }

            }

            if(letterNum != "" && letterNum != "нет")
            {
                fin_str_num = letterNum + " от " + letterDate;
            }
            else
            {
                fin_str_num = "";
            }

            int r = 19;

            while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
            {
                MyProgressUpdate(pb, 80, "Формирование запросов");
                q = "insert into PriceApprovement (FillId, ICId, BoLID, BFId, ItemCode, FactMinCost, LetterNumOut) Values\n";
                iId = long.Parse(oSheet.Cells(r, 1).Value.ToString());
                ICId = oSheet.Cells(r, 2).Value?.ToString() ?? "";
                sfbid = oSheet.Cells(r, 3).Value?.ToString() ?? "";
                bfid = oSheet.Cells(r, 4).Value?.ToString() ?? "";
                code = oSheet.Cells(r, 6).Value?.ToString() ?? "";
                budg_price_str = oSheet.Cells(r, 11).Value?.ToString() ?? "";
                if (!decimal.TryParse(budg_price_str, out decimal budg_price)) budg_price = 0;

                q += "(" + iId + "," + MyES(ICId) + "," + MyES(sfbid) + "," + MyES(bfid) + "," + MyES(code) + "," + MyES(budg_price) + ","
                            + MyES(fin_str_num) + "); select cast(scope_identity() as bigint) new_id; ";
                long new_id = long.Parse(MyGetOneValue(q).ToString());
                MyLog(uid, "PA", 20010, new_id, EntityId);
                r++;
            }
            return;
        }

        private void btnExportChecked_Click(object sender, EventArgs e)
        {
            MyExcelPriceApprovementReport(SpecVer, uid);
            return;
            //MyLog(uid, "BoL", 1130, SpecVer, EntityId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string delq = "delete from SpecFillBoL where SFBId in (" + SFBId.Text + ")";
            
            MyExecute(delq);
            MsgBox("ОК");
            FillFilling();
            MyLog(uid, "BoL", 11118, SpecVer, EntityId);
        }

        private void ExportExcluded_Click(object sender, EventArgs e)
        {
            string q = "select ";
            List<string> tt = new List<string>();
            foreach (MyXlsField f in FillingReportStructure)
            {
                q += f.SqlName + ",";
                tt.Add(f.Title);
            }
            q = q.Substring(0, q.Length - 1);

            q += " FROM PriceApprovement pa " +
                 " inner join vwSpecFill vwsf on vwsf.SFId = pa.FillId " +
                 " inner join Spec s on s.SId = vwsf.SId " +
                 " left join BudgetFill bf on bf.SpecFillId = vwsf.SFId " +
                 " left join Budget b on b.BId = bf.BudgId  " +
                 " left join SpecFillBol sfb2 on sfb2.SFBFill = vwsf.SFId " +
                 " left join InvCfm ic on ic.ICFill = vwsf.SFId and sfb2.SFBId is null " +
                 " left join InvDoc id on id.InvId = ic.InvDocId  ";
            
            int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
            if (c == 0)
            {
                MsgBox("Нет наполнения, нечего выгружать.");
                return;
            }

            q += " order by vwsf.SId, cast(vwsf.SFNo as bigint), cast(vwsf.SFNo2 as bigint)";
            MyExcelIns(q, tt.ToArray(), true, new decimal[] { 17,17, 17, 17, 17, 25, 17, 17, 47, 55, 10, 10, 17, 17, 17, 30, 40, 17, 30, 20, 50, 21, 17, 17, 17, 17, 17, 17, 17, 40 }, new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 });
            //MyLog(uid, "BoL", 1130, SpecVer, EntityId);
        }

        private void SumsUpload_Click(object sender, EventArgs e)
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

            MyProgressUpdate(pb, 10, "Открываем файл");

            dynamic oSheet = oBook.Worksheets(1);
            if (bNoError && !FillingImportCheckValues(oSheet)) bNoError = false;

            if (bNoError)
            {
                if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
                    , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FillingImportSums(oSheet);
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

        private void FillingImportSums(dynamic oSheet)
        {
            long iId;
            string q;
            string ResponseLetter, MKEorTSN, PAUnit, ApprovedPrice_str, TotalCost_str, DiffCost_str, SecondLetterNum, ResponseFromTSKNum, ReqsFromFixDoc, CostInFixDoc_str, Comment;
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;
            int r = 2;

            while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
            {
                MyProgressUpdate(pb, 80, "Формирование запросов");
                q = "update PriceApprovement set ";
                iId = long.Parse(oSheet.Cells(r, 1).Value.ToString());
                ResponseLetter = oSheet.Cells(r, 20).Value?.ToString() ?? "";
                MKEorTSN = oSheet.Cells(r, 21).Value?.ToString() ?? "";
                PAUnit = oSheet.Cells(r, 22).Value?.ToString() ?? "";
                ApprovedPrice_str = oSheet.Cells(r, 23).Value?.ToString() ?? "";
                TotalCost_str = oSheet.Cells(r, 24).Value?.ToString() ?? "";
                DiffCost_str = oSheet.Cells(r, 25).Value?.ToString() ?? "";
                SecondLetterNum = oSheet.Cells(r, 26).Value?.ToString() ?? "";
                ResponseFromTSKNum = oSheet.Cells(r, 27).Value?.ToString() ?? "";
                ReqsFromFixDoc = oSheet.Cells(r, 28).Value?.ToString() ?? "";
                CostInFixDoc_str = oSheet.Cells(r, 29).Value?.ToString() ?? "";
                Comment = oSheet.Cells(r, 30).Value?.ToString() ?? "";
                if (!decimal.TryParse(ApprovedPrice_str, out decimal ApprovedPrice)) ApprovedPrice = 0;
                if (!decimal.TryParse(TotalCost_str, out decimal TotalCost)) TotalCost = 0;
                if (!decimal.TryParse(DiffCost_str, out decimal DiffCost)) DiffCost = 0;
                if (!decimal.TryParse(CostInFixDoc_str, out decimal CostInFixDoc)) CostInFixDoc = 0;

                q += "" +
                " ResponseLetter = " + MyES(ResponseLetter) +
                " ,MKEorTSN = " + MyES(MKEorTSN) +
                " ,PAUnit = " + MyES(PAUnit) +
                " ,ApprovedPrice = " + MyES(ApprovedPrice) +
                " ,TotalCost = " + MyES(TotalCost) +
                " ,DiffCost = " + MyES(DiffCost) +
                " ,SecondLetterNum = " + MyES(SecondLetterNum) +
                " ,ResponseFromTSKNum = " + MyES(ResponseFromTSKNum) +
                " ,ReqsFromFixDoc = " + MyES(ReqsFromFixDoc) +
                " ,CostInFixDoc = " + MyES(CostInFixDoc) +
                " ,Comment = " + MyES(Comment) +
                " where PAId = " + MyES(iId);
                MyExecute(q);
                MyLog(uid, "PA", 20011, iId, EntityId);
                r++;
            }
            return;
        }
    }
}

