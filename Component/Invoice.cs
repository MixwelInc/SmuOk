using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;

namespace SmuOk.Component
{
  public partial class Invoice : UserControl
  {
    public Invoice()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "Invoice";
    private string FormSqlPref = "OInv";
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;

    private void Invoice_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("Invoice");
      FillFilter();
      SpecList_RestoreColunns(dgvSpec);
      dgvInvoiceFill.MyRestoreColWidthForUser(uid);
      
      /*int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneMultiline' and EUIOVaue=1");
      chkDoneMultiline.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneType' and EUIOVaue=1");
      chkDoneType.Checked = i == 1;

      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkDoneSubcode' and EUIOVaue=1");
      chkDoneSubcode.Checked = i == 1;*/

      FormIsUpdating = false;
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

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
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

      q += " inner join vwSpecVer_hasOrder on SVId=sv_has_order ";

      q += " where pto_block=1 and SState != 1 ";

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

    private void dgvSpec_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      if (e.RowIndex >= 0)
      {
        EntityId = (long)dgvSpec.Rows[e.RowIndex].Cells["dgv_SId"].Value;
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

    public void FillFilling()
    {
      string q = "select OId, "+
        " O1sId,ONo,OArt,OName,OUnit,OQty, "+
	      " OInvINN,OInvNo,OInvDate,OInvNo2,OInvName,OInvUnit,OInvK,OInvQty,OInvPrc "+
        " from Ordr " +
        " left join SpecFillExecOrder on SFEOId = OSpecFillExecOrder "+
        " inner join SpecFillExec on SFEId = SFEOSpecFillExec "+
        " inner join SpecFill on SFId = SFEFill "+
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by ONo ";
      MyFillDgv(dgvInvoiceFill, q);
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

    private void btnExport_Click(object sender, EventArgs e)
    {
      /*string q = "select SFEOId,SVName,SFSubcode,SFNo,SFNo2,SFName,SFMark,SFUnit,EName, QtyToOrder,convert(nvarchar(10),SFEOStartDate,104) SFEOStartDate, " +
  " O1sId, ONo, OArt, OUnit, OK, OQty " +
  " from vwOrder " +
  " where SFSpecVer=" + SpecVer.ToString();*/

      string q = "select OId,SVName, " +
        " O1sId,ONo,OArt,OName,OUnit,OQty, " +
        " OInvINN,OInvNo,OInvDate,OInvNo2,OInvName,OInvUnit,OInvK,OInvQty,OInvPrc " +
        " from Ordr " +
        " inner join SpecFillExecOrder on SFEOId = OSpecFillExecOrder " +
        " inner join SpecFillExec on SFEId = SFEOSpecFillExec " +
        " inner join SpecFill on SFId = SFEFill " +
        " inner join SpecVer on SFSpecVer = SVId " +
        " where SVId=" + SpecVer.ToString();

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }
      //q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
      q += " order by ONo";
      MyExcel(q, FillingReportStructure, true, new decimal[] { 7, 11, 10, 9, 8, 80, 6, 11, 13, 10, 9.43M, 12, 12, 12, 12, 12, 12 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
      MyLog(uid, "Invoice", 1110, SpecVer, EntityId);
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

      if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
      if (bNoError) MyExcelUnmerge(oSheet);

      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);
      if (bNoError) bNoError = FillingImportCheckSpecName(oSheet, sSpecName);
      if (bNoError) bNoError = FillingImportCheckOIds(oSheet);
      //if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet);

      //if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false; // qty?

      oExcel.ScreenUpdating = true;
      oExcel.DisplayAlerts = true;

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet);
          FillFilling();
          MsgBox("Ok");
        }
        oExcel.Quit();
      }
      else
      {
        oExcel.Visible = true;
        oExcel.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private void FillingImportData(dynamic oSheet)
    {
      long OId;

      string OInvINN;
      string OInvNo;
      DateTime OInvDate;
      int OInvNo2;
      string OInvName;
      string OInvUnit;
      decimal OInvK;
      decimal OInvQty;
      decimal OInvPrc;

      /*long O1sId;
      long ONo;
      string OArt;
      string OName;
      string OUnit;
      decimal OK;
      decimal OQty;*/

      string q = "";
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;

      int c_base = 8; // num of cols before data cols
      int c;
      //int c_max = 17;


      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Формирование запросов");
        //столбцы константами, сорри

        c = c_base;
        OId = long.Parse(oSheet.Cells(r, 1).Value.ToString());

        OInvINN = oSheet.Cells(r, ++c).Value.ToString();
        OInvNo = oSheet.Cells(r, ++c).Value.ToString();
        OInvDate = (DateTime)oSheet.Cells(r, ++c).Value;
        OInvNo2 = int.Parse(oSheet.Cells(r, ++c).Value.ToString());
        OInvName = oSheet.Cells(r, ++c).Value.ToString();
        OInvUnit = oSheet.Cells(r, ++c).Value.ToString();
        OInvK = decimal.Parse(oSheet.Cells(r, ++c).Value.ToString());
        OInvQty = decimal.Parse(oSheet.Cells(r, ++c).Value.ToString());
        OInvPrc = decimal.Parse(oSheet.Cells(r, ++c).Value.ToString());

        q += "update Ordr set ";
        q += " OInvINN=" + MyES(OInvINN) + ", ";
        q += " OInvNo=" + MyES(OInvNo) + ", ";
        q += " OInvDate=" + MyES(OInvDate) + ", ";
        q += " OInvNo2=" + MyES(OInvNo2) + ", ";
        q += " OInvName=" + MyES(OInvName) + ", ";
        q += " OInvUnit=" + MyES(OInvUnit) + ", ";
        q += " OInvK=" + MyES(OInvK) + ", ";
        q += " OInvQty=" + MyES(OInvQty) + ", ";
        q += " OInvPrc=" + MyES(OInvPrc) ;
        q += " where OId=" + MyES(OId) + " \n";
      }
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "Invoice", 110, SpecVer, EntityId);
      return;
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

    private bool FillingImportCheckOIds(dynamic oSheet)
    {
      string sErr = "";
      long OId;
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
        OId = (long)oSheet.Cells(r, c).Value;
        z = Convert.ToInt64(MyGetOneValue("select count(*)c " +
            " from Ordr "+
            " inner join SpecFillExecOrder on OSpecFillExecOrder = SFEOId " +
            " inner join SpecFillExec on SFEOSpecFillExec = SFEId " +
            " inner join SpecFill on SFId = SFEFill " +
            " where SFSpecVer = " + SpecVer + " and OId = " + MyES(OId) + ";"));
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

    private void dgvInvoiceFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      ((DataGridView)sender).MySaveColWidthForUser(uid, e);
    }

    /*private void chkDoneSubcode_CheckedChanged(object sender, EventArgs e)
    {
      dgvInvoiceFill.Columns["dgv_SFSubcode"].Visible = chkDoneSubcode.Checked;
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
      dgvInvoiceFill.Columns["dgv_SFType"].Visible = chkDoneType.Checked;
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
      dgvInvoiceFill.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvInvoiceFill.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;//  "chkDoneMultiline";
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }*/

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
