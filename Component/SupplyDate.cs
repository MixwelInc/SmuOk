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
      MyFillList(lstExecFilter, "select EId, EName from Executor order by case when ESmuDept = 0 then 999999 else ESmuDept end;");
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
      string q = "select SFEId,SFId SFEFill, SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFEQty,cnt.AmountOrdered as AmountOrdered,SFEOStartDate,SFEOQty " +
        " from SpecFill left join SpecFillExec sfe on SFId=SFEFill left join SpecFillExecOrder on SFEId=SFEOSpecFillExec " +
        " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt " +
        " where SFSpecVer=" + SpecVer.ToString() +
        " and SFEExec=" + lstExecFilter.GetLstVal() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, " +
        " case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end,SFEOStartDate ";

            string qqq = "select max(SOOrderNumPref)" +
        " from SupplyOrder left join" +
        " SpecFill sf on sf.SFId = SOFill " +
        " left join SpecFillExec on SFId=SFEFill left join SpecFillExecOrder on SFEId=SFEOSpecFillExec " +
        " where SOFill in (" +
        " select SFId" +
        " from SpecFill left join SpecFillExec on SFId=SFEFill left join SpecFillExecOrder on SFEId=SFEOSpecFillExec " +
        " where SFSpecVer=" + SpecVer.ToString() +
        " and SFEExec=" + lstExecFilter.GetLstVal() + ");";
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

      if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
      if (bNoError) MyExcelUnmerge(oSheet);

      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);
      if (bNoError) bNoError = FillingImportCheckSpecName(oSheet, sSpecName);
      if (bNoError) bNoError = FillingImportCheckExecName(oSheet, lstExecFilter.GetLstText());
      if (bNoError) bNoError = FillingImportCheckSFEIds(oSheet, SpecVer, lstExecFilter.GetLstVal());
      if (bNoError) bNoError = FillingImportCheckSFEOIds(oSheet);
      //if (bNoError) bNoError = FillingImportCheckSumElements(oSheet);

      if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet);

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
      int iId;
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFEId
      int c2 = 2; // 1-based SFEOId
      string sSum;
      decimal d;
      int cqty = 12; // 1-based SFEOQty
      if (rows == 1) return true;

      string SFEOId;

      Dictionary<int, decimal> sums_addandupdate = new Dictionary<int, decimal>();
      //Dictionary<int, decimal> sums_update = new Dictionary<int, decimal>();
      Dictionary<int, string> ids_update = new Dictionary<int, string>();
      List<int> sum_errors = new List<int>();
      List<int> exist_errors = new List<int>();

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 60 + 5 * r / rows, "Проверка суммы по нескольким поставкам");
        iId = int.Parse(oSheet.Cells(r, c).Value.ToString());
        sSum = oSheet.Cells(r, cqty).Value.ToString();
        d = decimal.Parse(sSum);

        // собираем суммы дляя проверки
        try { sums_addandupdate[iId] += d; }
        catch { sums_addandupdate[iId] = d; }

        // айдишники для обновлений: надо поймать те, которые обновляем 
        // пере обновлением будем по каждому SFEId проверять сумму из
        // а). того, что обновляем и добавляем и
        // б). того, что не затронуто обновлениями,
        // поскольку б) может остутствовать в проверяемом файле и мы добавим лишнее количество в сужетсвующие или новые SFEOId
        if ((oSheet.Cells(r, c2).Value?.ToString() ?? "") != "") SFEOId = oSheet.Cells(r, c2).Value?.ToString();
        else SFEOId = "0"; // это немного костылим для строк, которые с базе есть, а в файле — только добавление
        // а сюда — для обновления SpecFillExecOrder
        try { ids_update[iId] += "," + SFEOId; }
        catch { ids_update[iId] = SFEOId; }
        
      }

      Dictionary<int, decimal> tmp = new Dictionary<int, decimal>(sums_addandupdate);

      foreach (KeyValuePair<int, string> ss in ids_update)
      {
        //string SFEOId_exists = "";
        //if (ids_update.TryGetValue(ss.Key, out SFEOId_exists))
        {
          d = Convert.ToDecimal(MyGetOneValue("select isnull(sum(SFEOQty),0)s from SpecFillExecOrder where SFEOSpecFillExec=" + ss.Key + " and SFEOId not in (" + ss.Value + ")"));
          sums_addandupdate[ss.Key] += d;
        }
      }

      foreach (KeyValuePair<int, decimal> ss in sums_addandupdate)
      {
        d = Convert.ToDecimal(MyGetOneValue("select sum(SFEQty) s from SpecFillExec where SFEId=" + MyES(ss.Key))); //нашлось?
        if (d != ss.Value) sum_errors.Add(ss.Key);
      }

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 65 + 5 * r / rows, "Проверка суммы по нескольким поставкам");
        iId = int.Parse(oSheet.Cells(r, c).Value.ToString());
        if (sum_errors.FindIndex(x => x == iId) != -1)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, cqty).Interior.Color = 0;
          oSheet.Cells(r, cqty).Font.Color = -16776961;
        }
      }

      if (e) MsgBox("Количество к поставке указано не полностью либо излишне (см. столбец <L>).", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void FillingImportData(dynamic oSheet, string ExecName)
    {
      long iId = 0;
      long iParent = 0;
      decimal dQty = 0;
      DateTime dt;
      string q = "";
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      string updq = "";
      string selq = "";
      List<string> sid_lst = new List<string>();
      string fill = "";
      string newPost = "";
            if(post == "")
            {
                newPost = "1";
            }
            else
            {
                int intpost = Int32.Parse(post) + 1;
                newPost = intpost.ToString();
            }
      List<string> fills = new List<string>();
      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Формирование запросов");
        //столбцы константами, сорри
        iParent = (long)oSheet.Cells(r, 1).Value;
        iId = (long)(oSheet.Cells(r, 2).Value ?? -1);
        dt = (DateTime)oSheet.Cells(r, 14).Value;
        dQty = (decimal)oSheet.Cells(r, 13).Value;
        fill = oSheet.Cells(r, 15).Value.ToString();
        q += "exec uspUpdateSpecFillExecOrder " + iId + "," +iParent + "," + MyES(dt) + "," + MyES(dQty) +";\n";
                if(r<rows)
                {
                    sid_lst.Add(iParent + ",");
                    fills.Add(fill + ",");
                }
                else
                {
                    sid_lst.Add(iParent + "");
                    fills.Add(fill);
                }
        q+= "if not exists(select * from SupplyOrder where SOFill  = " + fill +
                    ") begin insert into SupplyOrder(SOId,SOFill,SOOrderNumPref,SOOrderDate) values(" + fill+"0," + fill +"," + newPost + ",'" + DateTime.Now +"') end; \n"; //этот пиздец надо убирать
      }
      q = q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
            /*string newPost = "";
            if(post == "")
            {
                newPost = "1";
            }
            else
            {
                int intpost = Int32.Parse(post) + 1;
                newPost = intpost.ToString();
            }
            selq = "select count(*) from SpecFillExec where ";*/
            updq = "update SpecFillExecOrder set SFEONum = '" + EntityId.ToString() + "-" + newPost +
                "' where SFEOSpecFillExec in (";
            foreach(string prt in sid_lst)
            {
                updq += prt;
            }
            updq += ");";
            updq += " update SupplyOrder " +
                "set SOOrderNum = '" + EntityId.ToString() + "-" + newPost +
                "' , SOOrderNumPref = '" + newPost +
                "' , SOOrderDate = '"+ DateTime.Now + "' where SOFill in (";
            foreach (string prt in fills)
            {
                updq += prt;
            }
            updq += ")";
            MyExecute(updq);
      /*updq = "insert into SupplyOrder (SOOrderNumPref)" +
                " values ( concat("+ sid + ",'-'," + post+1 + ")" +
                " where SOFill in (select sfefill" +
                " from SpecFillExec";*/
      MyLog(uid, "SupplyDate", 90, SpecVer, EntityId, ExecName); ////////////////////////////////////////////////тут остановился
      return;
    }
    /*
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
    */
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
      if (e) MsgBox("Исполнитель в файле (см. столбец <I>) не совпадает с выбранным, «" + ExecName + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      string q = "select SFEId,SFEOId,SVName,SFSubcode,SFNo,SFNo2,SFName,SFMark,SFUnit,EName,SFEQty,cnt.AmountOrdered as AmountOrdered, SFEOQty,convert(nvarchar(10),SFEOStartDate,104) SFEOStartDate, sfefill " +
              " from SpecVer inner join SpecFill on SVId=SFSpecVer inner join SpecFillExec sfe on SFId=SFEFill inner join Executor on SFEExec=EId left join SpecFillExecOrder on SFEOSpecFillExec=SFEId " +
              " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt " +
              " where SFSpecVer=" + SpecVer.ToString() +
              " and SFEExec=" + lstExecFilter.GetLstVal();

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }
      q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";

      MyExcel(q, FillingReportStructure, true, new decimal[] { 7, 7, 17, 17, 5, 5, 80, 80, 11, 15, 15, 12, 12, 9.43M }, new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15});

      MyLog(uid, "SupplyDate", 1090, SpecVer, EntityId, lstExecFilter.GetLstText());
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

  }
}
