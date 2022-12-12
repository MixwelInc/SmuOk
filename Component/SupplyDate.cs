using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;

namespace SmuOk.Component
{
  public partial class SupplyDate : UserControl
  {
    public SupplyDate()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "Done";
    private string FormSqlPref = "D";
    private long EntityId = -1;//sid
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;
    private int sid = 0;
    private string post = "";

    private void SupplyDate_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("SupplyDate");
      FillFilter();
      SpecList_RestoreColunns(dgvSpec);

      dgvSpecSupplyDateFill.MyRestoreColWidthForUser(uid);
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
      //MyFillList(lstExecFilter, "select eid, ename from (select -1 eid,'<не выбран>' ename, -1 ESmuDept union select EId, EName, ESmuDept from Executor)s order by case when ESmuDept=0 then 999999 else ESmuDept end;", "(исполнитель)");
      //MyFillList(lstExecFilter, "select EId, EName from Executor inner join UserExec on UEExec = EId where UEUser = " + (IsDebugComputer()?1:uid) + " order by case when ESmuDept = 0 then 999999 else ESmuDept end;");
      MyFillList(lstExecFilter, "select EId, EName from Executor order by case when ESmuDept = 0 then 999999 else ESmuDept end;", "(исполнитель)");
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

      q += " where pto_block=1 and SType != 6 ";

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
            string q = "select SVName + ' :: версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: ' " +
                "  +isnull(cc,0) " +
                "	+' ('+case when NewestFillingCount = 0 then 'нет' else convert(nvarchar, NewestFillingCount) end +')' f " +
                "  from vwSpec " +
                "	left join ( " +
                "	select SFSpecVer, cast(count(SFEId) as nvarchar) cc from SpecFill left join SpecFillExec on SFId=SFEFill " +
                "	where 1=1";
            if (lstExecFilter.GetLstVal() > 0)
            {
                q += " and SFEExec = " + lstExecFilter.GetLstVal() +
                    "	group by SFSpecVer " +
                    ")ff " +
                    "	on SFSpecVer=SVId " +
                    "	Where SVId= " + SpecVer.ToString();
            }
            else
            {
                q += "	group by SFSpecVer " +
                    ")ff " +
                    "	on SFSpecVer=SVId " +
                    "	Where SVId= " + SpecVer.ToString();
            }

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
            string q = "select SFEId,SFId SFEFill, SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFEQty,cnt.AmountOrdered as AmountOrdered,SFEOStartDate,SFEOQty " +
              " from SpecFill left join SpecFillExec sfe on SFId=SFEFill left join SpecFillExecOrder on SFEId=SFEOSpecFillExec " +
              " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt " +
              " where SFSpecVer=" + SpecVer.ToString();
            if (lstExecFilter.GetLstVal() > 0)
            {
               q += " and SFEExec=" + lstExecFilter.GetLstVal() +
                    " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, " +
                    " case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end,SFEOStartDate ";
            }
            else
            {
                q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, " +
                    " case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end,SFEOStartDate ";
            }

            string qqq = "select max(SOOrderNumPref)" +
        " from SupplyOrder left join" +
        " SpecFill sf on sf.SFId = SOFill " +
        " left join SpecFillExec on SFId=SFEFill left join SpecFillExecOrder on SFEId=SFEOSpecFillExec " +
        " where SOFill in (" +
        " select SFId" +
        " from SpecFill left join SpecFillExec on SFId=SFEFill left join SpecFillExecOrder on SFEId=SFEOSpecFillExec " +
        " where SFSpecVer=" + SpecVer.ToString();
            if (lstExecFilter.GetLstVal() > 0)
            {
                qqq += " and SFEExec=" + lstExecFilter.GetLstVal() + ");";
            }
            else
            {
                qqq += ");";
            }
      //sid = (int)MyGetOneValue(qq);//получение id спеки
      post = MyGetOneValue(qqq).ToString();//получение максимального постфикса
      MyFillDgv(dgvSpecSupplyDateFill, q);
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
      dgvSpecSupplyDateFill.Columns["dgv_SFSubcode"].Visible = chkDoneSubcode.Checked;
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
      dgvSpecSupplyDateFill.Columns["dgv_SFType"].Visible = chkDoneType.Checked;
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

      //if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
      //if (bNoError) MyExcelUnmerge(oSheet);

      //if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);
      //if (bNoError) bNoError = FillingImportCheckSpecName(oSheet, sSpecName);
      //if (bNoError) bNoError = FillingImportCheckExecName(oSheet, lstExecFilter.GetLstText());
      //if (bNoError) bNoError = FillingImportCheckSFEIds(oSheet, SpecVer, lstExecFilter.GetLstVal());
      //if (bNoError) bNoError = FillingImportCheckSFEOIds(oSheet);
      if (bNoError) bNoError = FillingImportCheckSumElements(oSheet);

      //if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet);

      //if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false;

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet, lstExecFilter.GetLstText());
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

    private bool FillingImportCheckSumElements(dynamic oSheet)
    {
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 14; // 14-based SFEId
      int c2 = 15; // 15-based SFEOId
      decimal d;
      decimal M15Qty, BoLQty, OrderedQty, OrderQty, TotalQty, prevQty;
      string M15Qtystr, BoLQtystr, OrderedQtystr, OrderQtystr, TotalQtystr;
      if (rows == 1) return true;

      string SFEOId;

      for (int r = 14; r < rows - 7; r++)
      {
        MyProgressUpdate(pb, 65 + 5 * r / rows, "Проверка суммы по нескольким поставкам");
                M15Qtystr = oSheet.Cells(r, 13).Value?.ToString() ?? "0";
                M15Qty = Convert.ToDecimal(M15Qtystr);
                BoLQtystr = oSheet.Cells(r, 12).Value?.ToString() ?? "0";
                BoLQty = Convert.ToDecimal(BoLQtystr);
                OrderedQtystr = oSheet.Cells(r, 11).Value?.ToString() ?? "0";
                OrderedQty = Convert.ToDecimal(OrderedQtystr);
                OrderQtystr = oSheet.Cells(r, 11).Value?.ToString() ?? "0";
                OrderQty = Convert.ToDecimal(OrderQtystr);
                TotalQtystr = oSheet.Cells(r, 5).Value?.ToString() ?? "0";
                TotalQty = Convert.ToDecimal(TotalQtystr);
                if ((oSheet.Cells(r, c2).Value?.ToString() ?? "") != "") SFEOId = oSheet.Cells(r, c2).Value?.ToString();
                else SFEOId = "0";
                if(SFEOId == "0")
                {
                    d = TotalQty - M15Qty - BoLQty - OrderedQty - OrderQty;
                    if(d < 0)
                    {
                        e = true;
                        oSheet.Cells(r, 1).Interior.Color = 13421823;
                        oSheet.Cells(r, 1).Font.Color = -16776961;
                        oSheet.Cells(r, 7).Interior.Color = 0;
                        oSheet.Cells(r, 7).Font.Color = -16776961;
                    }
                }
                else
                {
                    string checkq = "select SFEOQty from SpecFillExecOrder where SFEOId = " + SFEOId;
                    prevQty = Convert.ToDecimal(MyGetOneValue(checkq));
                    if (prevQty == OrderQty) continue;
                    else
                    {
                        d = TotalQty - M15Qty - BoLQty - (OrderedQty - prevQty) - OrderQty; //- amount that we have ordered and - current amount instead
                        if(d < 0)
                        {
                            e = true;
                            oSheet.Cells(r, 1).Interior.Color = 13421823;
                            oSheet.Cells(r, 1).Font.Color = -16776961;
                            oSheet.Cells(r, 7).Interior.Color = 0;
                            oSheet.Cells(r, 7).Font.Color = -16776961;
                        }
                    }

                }
      }

      if (e) MsgBox("Количество к поставке указано не полностью либо излишне (см. столбец <L>).", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void FillingImportData(dynamic oSheet, string ExecName)
    {
      decimal dQty;
      string dt;
      string q = "";
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      string fill,newPost,address,response, iParent, iId;
            if(post == "")
            {
                newPost = "1";
            }
            else
            {
                int intpost = Int32.Parse(post) + 1;
                newPost = intpost.ToString();
            }
      for (int r = 14; r < rows-7; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Формирование запросов");
        //столбцы константами, сорри
        iParent = oSheet.Cells(r, 14).Value?.ToString() ?? "";
        iId = oSheet.Cells(r, 15).Value?.ToString() ?? "-1";
        dt = oSheet.Cells(r, 8).Value?.ToString() ?? "";
        dQty = (decimal)oSheet.Cells(r, 7).Value;
        fill = oSheet.Cells(r, 16).Value.ToString();
        address = oSheet.Cells(r, 9).Value?.ToString() ?? "";
        response = oSheet.Cells(r, 10).Value?.ToString() ?? "";
                q += "exec uspUpdateSpecFillExecOrder "+ EntityId + "," + iId + "," + iParent + "," + MyES(dt) + "," + MyES(dQty) + "," + newPost + "," + fill + "," +
                    "'" + address + "'" + "," + "'" + response + "'" + "\n";
                MyLog(uid, "SupplyDate", 2011, SpecVer, long.Parse(iParent), ExecName);
      }
      q = q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "SupplyDate", 90, SpecVer, EntityId, ExecName); ////////////////////////////////////////////////старое логирование
      return;
    }

    private bool FillingImportCheckIdsUniq(dynamic oSheet)
    {
      string sErr = "";
      long s;
      HashSet<long> ssuniq = new HashSet<long>();
      List<long> errs = new List<long>();
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 2; // 1-based SFEOId: уникальными должны быть редактируемые
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 60 + 10 * r / rows, "Проверка единичности идентификаторов существующих заявок.");
        s = Convert.ToInt64((oSheet.Cells(r, c).Value ?? 0).ToString());
        if (s>0 && !ssuniq.Add(s)) errs.Add(s);
      }

      if (errs.Count() > 0)
      {
        oSheet.Columns(c).FormatConditions.AddUniqueValues();
        oSheet.Columns(c).FormatConditions(1).DupeUnique = 1;// xlDuplicate;
        oSheet.Columns(c).FormatConditions(1).Font.Color = -16776961;
        oSheet.Columns(c).FormatConditions(1).Interior.Color = 0;
        oSheet.Columns(c).FormatConditions(1).StopIfTrue = 0;
        sErr += "\nВ файле для загрузки идентификаторы существующих заявок не уникальны.";
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
        if (z > 0)
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
      return z == 0;
    }

    private bool FillingImportCheckSFEOIds(dynamic oSheet)
    {
      string sErr = "";
      string SFEId;
      string SFEOId;
      string s;
      List<string> ss = new List<string>();
      long z=0;
      int ErrCount = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFEId
      int c2 = 2; // 1-based SFEId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 10 * r / rows, "Проверка идентификаторов строк.");
        SFEId = oSheet.Cells(r, c).Value.ToString();
        SFEOId = oSheet.Cells(r, c2).Value?.ToString() ?? "";
        if (SFEOId == "") continue;
        z = Convert.ToInt64(MyGetOneValue("select count(*) from SpecFillExecOrder " +
          " where SFEOId = " + MyES(SFEOId) + " and SFEOSpecFillExec =" + MyES(SFEId)));
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
        if ((!FillingReportStructure[c - 1].Nulable && s=="") || s != SpecName)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Шифр проекта в файле (см. столбец <C>) не совпадает с шифром проекта в изменяемой версии (изменении), «" + SpecName + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckExecName(dynamic oSheet, string ExecName)
    {
      object o_s;
      string s;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 10; // 1-based Исполнитель
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка исполнителя");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if ((!FillingReportStructure[c - 1].Nulable && s == "") || s != ExecName)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Необходимо выбрать исполнителя на вкладке и загружать позиции по конкретному исполнителю (выбранному на вкладке)", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
            MyExcelSupplyDateReport(EntityId, SpecVer.ToString(), lstExecFilter.GetLstVal());
            return;
      /*string q = "select SFEId,SFEOId,SVName,SFSubcode,SFNo,SFNo2,SFName,SFMark,SFUnit,EName,SFEQty,cnt.AmountOrdered as AmountOrdered, SFEOQty,convert(nvarchar(10),SFEOStartDate,104) SFEOStartDate, sfefill " +
              " from SpecVer inner join SpecFill on SVId=SFSpecVer inner join SpecFillExec sfe on SFId=SFEFill inner join Executor on SFEExec=EId left join SpecFillExecOrder on SFEOSpecFillExec=SFEId " +
              " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt " +
              " where SFSpecVer=" + SpecVer.ToString() +
              " and SFEExec=" + lstExecFilter.GetLstVal();

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать. \n(выгрузка возможно только при указании исполнителя)");
        return;
      }
      q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";

      MyExcel(q, FillingReportStructure, true, new decimal[] { 7, 7, 17, 17, 5, 5, 80, 80, 11, 15, 15, 12, 12, 9.43M }, new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15});

      MyLog(uid, "SupplyDate", 1090, SpecVer, EntityId, lstExecFilter.GetLstText());// have to add logs to new export*/
    }

    private void chkDoneMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkDoneMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecSupplyDateFill.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvSpecSupplyDateFill.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;
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
      dgvSpecSupplyDateFill.MySaveColWidthForUser(uid, e);
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
            string q;
            q = " delete from SpecFillExec where SFEId in ( select SFEOSpecFillExec from SpecFillExecOrder where SFEOId in ( " + SFEOId.Text + " ));" +
                //" delete from SpecFillExecOrder where SFEOId in ( " + SFEOId.Text + "); " +
                " delete from BudgetFill where ICId in (select ICId from InvCfm where ICOrderId in (" + SFEOId.Text + ")); " + 
                " delete from InvCfm where ICOrderId in (" + SFEOId.Text + ");" +
                " delete from SupplyOrder where SOOrderId in (" + SFEOId.Text + ");";
            MyExecute(q);
            MyLog(uid, "SupplyDate", 2012, SpecVer, EntityId,sCaption: SFEOId.Text); //parasha
            fill_dgv();
            MsgBox("OK");
            SFEOId.Text = "";
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selq = "select SFEOId from SpecFillExec inner join SpecFillExecOrder on SFEId = SFEOSpecFillExec " +
                " where SFEFill in (select SFId from SpecFill where SFSpecVer = " + SpecVer.ToString() + ")";
            List<string> res = MyGetOneCol(selq);
            if (MessageBox.Show("Вы уверены, что хотите удалить данные по спецификации " + EntityId.ToString() +" ?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string delq;
                foreach (string pos in res)
                {
                    delq = " delete from SpecFillExec where SFEId in ( select SFEOSpecFillExec from SpecFillExecOrder where SFEOId in ( " + pos + " ));";
                    MyExecute(delq);
                    delq = " delete from SpecFillExecOrder where SFEOId in ( " + pos + "); ";
                    MyExecute(delq);
                    delq = " delete from BudgetFill where ICId in (select ICId from InvCfm where ICOrderId in (" + pos + ")); ";
                    MyExecute(delq);
                    delq = " delete from InvCfm where ICOrderId in (" + pos + ");";
                    MyExecute(delq);
                    delq = " delete from SupplyOrder where SOOrderId in (" + pos + ");";
                    MyExecute(delq);
                }
                MsgBox("Данные удалены");
                fill_dgv();
                MyLog(uid, "SupplyDate", 2013, SpecVer, EntityId);
            }
            else
            {
                MsgBox("Операция отменена");
            }
            return;
        }
    }
}
