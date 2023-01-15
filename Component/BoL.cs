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
  public partial class BoL : UserControl
  {
    public BoL()
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

    private void BoL_Load(object sender, EventArgs e)
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
      FillingReportStructure = FillReportData("BoL");
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

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO,SState from vwSpec ";

      /*if (lstExecFilter.GetLstVal() > 0)
      {
        q += "\n inner join (select SESpec from SpecExec where SEExec=" + lstExecFilter.GetLstVal() + ")se on SESpec=SId";
      }*/

      string sName = txtSpecNameFilter.Text;
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
      {
        q += " inner join (select SVSpec svs from SpecVer " +
              " where SVName like " + MyES(sName, true) +
              " or SVSpec in(" + sName +
              "))q on svs=SId";
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
      string q = "select SFId, SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQtyBuy, BoLQtySum, SFQtyBuy-(IsNull(BoLQtySum,0)) BRestQty, sfe.SFEId" +
        " from SpecFill " +
        " left join (select SFBFill, sum(SFBQtyForTSK) BoLQtySum from SpecFillBoL group by SFBFill)d on SFBFill = SFId " +
        " left join SpecFillExec sfe on sfe.SFEFIll = SFId" +
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
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

    /*private void btnExport_Click(object sender, EventArgs e)
    {
      string q = "select ";
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        q += f.SqlName + ",";
        tt.Add(f.Title);
      }
      q = q.Substring(0, q.Length - 1);
      q += "\n from SpecVer inner join SpecFill on svid=SFSpecVer left join (select SFBFill, sum(SFBQtyForTSK) BoLQtySum from SpecFillBoL group by SFBFill)d on d.SFBFill = SFId ";
      q += "\n left join SpecFillBol sfb on sfb.SFBFill = SFId left join InvCfm ic on ic.ICFill = sfid " ;
      q += "\n where IsNull(SFQtyBuy,0)>0 and SFSpecVer=" + SpecVer.ToString();

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end";
      MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7,17,17,15,5,5,25,25,25,20,11,11,10,10,14,7,11,11,10,12,10,8,8,8 }, new int[] { 2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26 });
      MyLog(uid, "BoL", 1130, SpecVer, EntityId);
    }*/

    /*private void btnImport_Click(object sender, EventArgs e)
    {
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
      if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb)) bNoError = false;   //FillingImportCheckTitle(oSheet)) bNoError = false;
      MyExcelUnmerge(oSheet);
      if (bNoError && !FillingImportCheckValues(oSheet)) bNoError = false;
      if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
      if (bNoError && !FillingImportCheckSVIds(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckSums(oSheet, SpecVer)) bNoError = false;

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
    }*/

    /*private void FillingImportData(dynamic oSheet)
    {
      long iId;
      long BoLNoForTSK;
      DateTime BoLDtForTSK;

      long RowInBoLForTSK;
      string sUnitForTSK;
      decimal qtyForTSK;

            long BoLNoFromTSK;
            DateTime BoLDtFromTSK;

            long RowInBoLFromTSK;
            string sUnitFromTSK;
            decimal qtyFromTSK;
            string recipient, shipmentPlace;
            string q, sCaption;
            long newTitleId;
            long oldTitleId;
            long titleId = 0;
            
      
      //s = oSheet.Cells(5, 5).Value.ToString();
      //long TitleId = long.Parse(MyGetOneValue("insert into SpecFillBoLTitle default values; Select SCOPE_IDENTITY() as new_id;").ToString());

      q = "insert into SpecFillBoL (SFBId,SFBFill, SFBBoLNoForTSK, SFBBoLDateForTSK, SFBNoForTSK, SFBUnitForTSK, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, " +
                "SFBBoLNoFromTSK, SFBBoLDateFromTSK, SFBNoFromTSK, SFBUnitFromTSK, SFBQtyFromTSK, SFBTitle) Values\n";

      int r = 2;
      string delq = "";
      while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
      {
        MyProgressUpdate(pb, 80, "Формирование запросов");
        iId = long.Parse(oSheet.Cells(r, 1).Value.ToString());
        string selq = "select count(*) from SpecFillBol where SFBFill = " + iId;
       
        bool newPos;
        long ans = long.Parse(MyGetOneValue(selq).ToString());
                BoLNoForTSK = long.Parse(oSheet.Cells(r, 27).Value.ToString());
                BoLDtForTSK = (DateTime)oSheet.Cells(r, 28).Value;
                RowInBoLForTSK = long.Parse(oSheet.Cells(r, 29).Value.ToString());
                sUnitForTSK = oSheet.Cells(r, 30).Value.ToString();
                qtyForTSK = (decimal)oSheet.Cells(r, 31).Value;
                recipient = oSheet.Cells(r, 32).Value.ToString();
                shipmentPlace = oSheet.Cells(r, 33).Value.ToString();
                BoLNoFromTSK = long.Parse(oSheet.Cells(r, 34).Value.ToString());
                BoLDtFromTSK = (DateTime)oSheet.Cells(r, 35).Value;
                RowInBoLFromTSK = long.Parse(oSheet.Cells(r, 36).Value.ToString());
                sUnitFromTSK = oSheet.Cells(r, 37).Value.ToString();
                qtyFromTSK = (decimal)oSheet.Cells(r, 38).Value;


                if (ans == 0)
                {
                    //newPos = true;
                    //TitleId = long.Parse(MyGetOneValue("insert into SpecFillBoLTitle default values; Select SCOPE_IDENTITY() as new_id;").ToString());
                    newTitleId = long.Parse(MyGetOneValue("insert into SpecFillBoLTitle default values; Select SCOPE_IDENTITY() as new_id;").ToString());
                    q += "(" + iId + "," + iId + "," + MyES(BoLNoForTSK) + "," + MyES(BoLDtForTSK) + "," + RowInBoLForTSK + "," + MyES(sUnitForTSK) + "," + MyES(qtyForTSK) + ","
                        + MyES(recipient) + "," + MyES(shipmentPlace)  + ","
                        + MyES(BoLNoFromTSK) + "," + MyES(BoLDtFromTSK) + "," + RowInBoLFromTSK + "," + MyES(sUnitFromTSK) + "," + MyES(qtyFromTSK) + "," + MyES(newTitleId) + ")\n,";
                    titleId = newTitleId;
                    MyLog(uid, "BoL", 130, titleId, EntityId);
                }
                else
                {
                    //newPos = false;
                    oldTitleId = long.Parse(MyGetOneValue("select distinct(SFBTId) from SpecFillBol left join SpecFillBol on SFBTId = SFBTitle").ToString());
                    q += "(" + iId + "," + iId + "," + MyES(BoLNoForTSK) + "," + MyES(BoLDtForTSK) + "," + RowInBoLForTSK + "," + MyES(sUnitForTSK) + "," + MyES(qtyForTSK) + ","
                        + MyES(recipient) + "," + MyES(shipmentPlace) + ","
                        + MyES(BoLNoFromTSK) + "," + MyES(BoLDtFromTSK) + "," + RowInBoLFromTSK + "," + MyES(sUnitFromTSK) + "," + MyES(qtyFromTSK) + "," + MyES(oldTitleId) + ")\n,";
                    titleId = oldTitleId;
                }
                delq += "delete from SpecFillBol where SFBFill = " + iId + "; \n";
                //q += "(" + iId + "," + MyES(BoLNo) + "," + MyES(BoLDt) + "," + RowInBoL + "," + MyES(sUnit) + "," + MyES(qty) + "," + MyES(TitleId) + ")\n,";
                r++;
      }
      MyExecute(delq);//запуск удаления
      q = q.Substring(0, q.Length - 1);
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "BoL", 130, titleId, EntityId);
      return;
    }*/

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
        MyProgressUpdate(pb, 20 + 10 * r / rows, "Проверка данных");
        for (int c = 1; c < FillingReportStructure.Count() + 1; c++)
        {
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          b = true;
          if (c >= 3 && c <= 26) continue;
                    if(c >= 28 && c <= 34)
                    {
                        sr = false;
                        if (s != "") fr = true;//b = false;
                        if(fr)
                        {
                            for (int k = 28; k <= 34; k++)
                            {
                                string val = oSheet.Cells(r, k).Value?.ToString() ?? "";
                                if (val == "" || val == null) b = false;
                            }
                        }
                    }
                    if(c >= 35 && c <= 39)
                    {
                        fr = false;
                        if (s != "") sr = true;
                        if (sr)
                        {
                            for(int k = 35; k <= 39; k++)
                            {
                                string val = oSheet.Cells(r, k).Value?.ToString() ?? "";
                                if (val == "" || val == null) b = false;
                            }
                        }
                    }
          if (!FillingReportStructure[c - 1].Nulable && s == "") b = false;
          if(!fr)
          if (b && s != "")
          {
            if (FillingReportStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            if (FillingReportStructure[c - 1].DataType == "decimal") b = decimal.TryParse(s, out rd) && rd > 0;
            if (FillingReportStructure[c - 1].DataType == "date") b = DateTime.TryParse(s, out dt);
          }
          if (!b)
          {
            e = true;
            oSheet.Cells(r, 1).Interior.Color = xlPink;
            oSheet.Cells(r, 1).Font.Color = xlRed;
            oSheet.Cells(r, c).Interior.Color = xlDimGray;
            oSheet.Cells(r, c).Font.Color = xlRed;
          }
        }
      }
      if (e) MsgBox("Не заданы корректные значения для обязательных столбцов.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    /*private bool FillingImportCheckSpecName(dynamic oSheet, string SpecCode)
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
        if (FillingReportStructure[c - 1].Nulable == false && s != SpecCode)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Шифр проекта в файле (см. столбец <B>) не совпадает с шифром проекта в изменяемой версии (изменении), «" + SpecCode + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckSVIds(dynamic oSheet, long svid)
    {
      string sErr = "";
      object o_s;
      string s;
      List<string> ss = new List<string>();
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка идентификаторов строк.");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (s == "") z = 0; // ващет надо было это раньше найти
        else if (!long.TryParse(s, out z)) z = 0; //не число
        else if (z.ToString() != s || z < 0) z = 0; //не положительное целое
        else
        {
          z = Convert.ToInt64(MyGetOneValue("select count (*) c from SpecFill where SFSpecVer=" + svid.ToString() + " and SFId=" + MyES(s))); //нашлось?
          ss.Add(s);
        }
        if (z == 0)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
        else if (z != 1) MsgBox("Так не должно быть! Обязательно пошлите скриншот этого окна разработчику.\n\nДля отладки: Curator.FillingImportCheckSVIds, SFSpecVer: " + svid + ", SFId=" + MyES(s));//нашлось >1, странное дело
        //s_to_del += o_s + ",";
      }

      if (e) sErr = "Идентификатор(ы) строк в файле (см. столбец <A>) не найдены в базе для этого шифра.\n";

      if (sErr != "") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckSums(dynamic oSheet, long svid)
    {
      string sId;
      decimal z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      string sSum;
      decimal d;
      int cs = 31; // 1-based SFQty
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 10 * r / rows, "Проверка количества.");
        sId = oSheet.Cells(r, c).Value.ToString();
        sSum = oSheet.Cells(r, cs).Value?.ToString() ?? "";
        if(sSum == "") continue;
        d = decimal.Parse(sSum);

        z = Convert.ToDecimal(
            MyGetOneValue("select SFQtyBuy-(IsNull(BoLQtySum,0)) BRestQty from SpecFill left join (select SFBFill, sum(SFBQtyForTSK) BoLQtySum from SpecFillBoL group by SFBFill)d on SFBFill = SFId " +
          " where SFId=" + MyES(sId)).ToString()); //нашлось?

        if (z < d)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, cs).Interior.Color = 0;
          oSheet.Cells(r, cs).Font.Color = -16776961;
        }
        //s_to_del += o_s + ",";
      }
      if (e) MsgBox("Количество, поставленное по УПД, в файле (см. столбец <X>) указана неверно.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }*/

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
            if (dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral)
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
            if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb)) bNoError = false;   //FillingImportCheckTitle(oSheet)) bNoError = false;
            MyExcelUnmerge(oSheet);
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
            string BoLNoForTSK;
            string BoLDtForTSK;

            string RowInBoLForTSK;
            string sUnitForTSK;
            string qtyForTSK;

            string BoLNoFromTSK;
            string BoLDtFromTSK;

            string RowInBoLFromTSK;
            string sUnitFromTSK;
            string qtyFromTSK;
            string recipient, shipmentPlace, soid;
            string q, sfeid, sfbid;
            long newTitleId;
            long oldTitleId;
            long titleId = 0;


            //s = oSheet.Cells(5, 5).Value.ToString();
            //long TitleId = long.Parse(MyGetOneValue("insert into SpecFillBoLTitle default values; Select SCOPE_IDENTITY() as new_id;").ToString());

            q = "";

            int r = 2;
            while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
            {
                MyProgressUpdate(pb, 80, "Формирование запросов");
                iId = long.Parse(oSheet.Cells(r, 1).Value.ToString());
                string selq = "select count(*) from SpecFillBol where SFBFill = " + iId;

                long ans = long.Parse(MyGetOneValue(selq).ToString());
                string bols = oSheet.Cells(r, 28).Value?.ToString() ?? "";
                string hols = oSheet.Cells(r, 35).Value?.ToString() ?? "";
                long new_id;
                if (hols == "" && bols == "")
                {
                    r++;
                    continue;
                }
                BoLNoForTSK = oSheet.Cells(r, 28).Value?.ToString() ?? "";
                BoLDtForTSK = oSheet.Cells(r, 29).Value?.ToString() ?? "";
                RowInBoLForTSK = oSheet.Cells(r, 30).Value?.ToString() ?? "";
                sUnitForTSK = oSheet.Cells(r, 31).Value?.ToString() ?? "";
                qtyForTSK = oSheet.Cells(r, 32).Value?.ToString() ?? "";
                recipient = oSheet.Cells(r, 33).Value?.ToString() ?? "";
                shipmentPlace = oSheet.Cells(r, 34).Value?.ToString() ?? "";
                BoLNoFromTSK = oSheet.Cells(r, 35).Value?.ToString() ?? "";
                BoLDtFromTSK = oSheet.Cells(r, 36).Value?.ToString() ?? "";
                RowInBoLFromTSK = oSheet.Cells(r, 37).Value?.ToString() ?? "";
                sUnitFromTSK = oSheet.Cells(r, 38).Value?.ToString() ?? "";
                qtyFromTSK = oSheet.Cells(r, 39).Value?.ToString() ?? "";
                sfeid = oSheet.Cells(r, 40).Value?.ToString() ?? "";
                sfbid = oSheet.Cells(r, 27).Value?.ToString() ?? "";
                soid = oSheet.Cells(r, 41).Value?.ToString() ?? "";
                if (sfbid == "")
                {
                    q = "insert into SpecFillBoL (SFBSpecExecFill, SFBFill, SFBBoLNoForTSK, SFBBoLDateForTSK, SFBNoForTSK, SFBUnitForTSK, SFBQtyForTSK, SFBRecipient, SFBShipmentPlace, " +
                          "SFBBoLNoFromTSK, SFBBoLDateFromTSK, SFBNoFromTSK, SFBUnitFromTSK, SFBQtyFromTSK, SFBTitle, SOId) Values\n";
                    if (ans == 0)
                    {
                        newTitleId = long.Parse(MyGetOneValue("insert into SpecFillBoLTitle default values; Select SCOPE_IDENTITY() as new_id;").ToString());
                        q += "(" + sfeid + "," + iId + "," + MyES(BoLNoForTSK, mak: true) + "," + MyES(BoLDtForTSK, mak: true) + "," + MyES(RowInBoLForTSK, mak: true) + "," + MyES(sUnitForTSK, mak: true) + "," + MyES(qtyForTSK, mak: true) + ","
                            + MyES(recipient, mak: true) + "," + MyES(shipmentPlace, mak: true) + ","
                            + MyES(BoLNoFromTSK, mak: true) + "," + MyES(BoLDtFromTSK, mak: true) + "," + MyES(RowInBoLFromTSK, mak: true) + "," + MyES(sUnitFromTSK, mak: true) + "," + MyES(qtyFromTSK, mak: true) + "," + MyES(newTitleId, mak: true) + ","
                            + MyES(soid) + ");" +
                            " select SCOPE_IDENTITY();";
                        titleId = newTitleId;
                        sfbid = MyGetOneValue(q).ToString();
                        MyLog(uid, "BoL", 2000, long.Parse(sfbid), EntityId);

                    }
                    else
                    {
                        //newPos = false;
                        oldTitleId = long.Parse(MyGetOneValue("select distinct(SFBTId) from SpecFillBolTitle left join SpecFillBol on SFBTId = SFBTitle").ToString());
                        q += "(" + sfeid + "," + iId + "," + MyES(BoLNoForTSK, mak: true) + "," + MyES(BoLDtForTSK, mak: true) + "," + MyES(RowInBoLForTSK, mak: true) + "," + MyES(sUnitForTSK, mak: true) + "," + MyES(qtyForTSK, mak: true) + ","
                            + MyES(recipient, mak: true) + "," + MyES(shipmentPlace, mak: true) + ","
                            + MyES(BoLNoFromTSK, mak: true) + "," + MyES(BoLDtFromTSK, mak: true) + "," + MyES(RowInBoLFromTSK, mak: true) + "," + MyES(sUnitFromTSK, mak: true) + "," + MyES(qtyFromTSK, mak: true) + "," + MyES(oldTitleId, mak: true) + ","
                            + MyES(soid) + ");" +
                            " select SCOPE_IDENTITY();";
                        titleId = oldTitleId;
                        sfbid = MyGetOneValue(q).ToString();
                        MyLog(uid, "BoL", 2000, long.Parse(sfbid), EntityId);
                    }
                }
                else
                {
                    q = "update SpecFillBoL set ";
                    if (ans == 0)
                    {
                        newTitleId = long.Parse(MyGetOneValue("insert into SpecFillBoLTitle default values; Select SCOPE_IDENTITY() as new_id;").ToString());
                        q += "SFBSpecExecFill = " + sfeid +
                            ",SFBFill = " + iId +
                            ",SFBBoLNoForTSK = " + MyES(BoLNoForTSK, mak: true) +
                            ",SFBBoLDateForTSK = " + MyES(BoLDtForTSK, mak: true) +
                            ",SFBNoForTSK = " + MyES(RowInBoLForTSK, mak: true) +
                            ",SFBUnitForTSK = " + MyES(sUnitForTSK, mak: true) +
                            ",SFBQtyForTSK = " + MyES(qtyForTSK, mak: true) +
                            ",SFBRecipient = " + MyES(recipient, mak: true) +
                            ",SFBShipmentPlace = " + MyES(shipmentPlace, mak: true) +
                            ",SFBBoLNoFromTSK = " + MyES(BoLNoFromTSK, mak: true) +
                            ",SFBBoLDateFromTSK = " + MyES(BoLDtFromTSK, mak: true) +
                            ",SFBNoFromTSK = " + MyES(RowInBoLFromTSK, mak: true) +
                            ",SFBUnitFromTSK = " + MyES(sUnitFromTSK, mak: true) +
                            ",SFBQtyFromTSK = " + MyES(qtyFromTSK, mak: true) +
                            ",SFBTitle = " + MyES(newTitleId, mak: true) +
                            ",SOId = " + MyES(soid) +
                            " where SFBId = " + sfbid;
                        titleId = newTitleId;
                        MyExecute(q);
                        MyLog(uid, "BoL", 2001, long.Parse(sfbid), EntityId);

                    }
                    else
                    {
                        //newPos = false;
                        oldTitleId = long.Parse(MyGetOneValue("select distinct(SFBTId) from SpecFillBolTitle left join SpecFillBol on SFBTId = SFBTitle").ToString());
                        q += "SFBSpecExecFill = " + sfeid +
                            ",SFBFill = " + iId +
                            ",SFBBoLNoForTSK = " + MyES(BoLNoForTSK, mak: true) +
                            ",SFBBoLDateForTSK = " + MyES(BoLDtForTSK, mak: true) +
                            ",SFBNoForTSK = " + MyES(RowInBoLForTSK, mak: true) +
                            ",SFBUnitForTSK = " + MyES(sUnitForTSK, mak: true) +
                            ",SFBQtyForTSK = " + MyES(qtyForTSK, mak: true) +
                            ",SFBRecipient = " + MyES(recipient, mak: true) +
                            ",SFBShipmentPlace = " + MyES(shipmentPlace, mak: true) +
                            ",SFBBoLNoFromTSK = " + MyES(BoLNoFromTSK, mak: true) +
                            ",SFBBoLDateFromTSK = " + MyES(BoLDtFromTSK, mak: true) +
                            ",SFBNoFromTSK = " + MyES(RowInBoLFromTSK, mak: true) +
                            ",SFBUnitFromTSK = " + MyES(sUnitFromTSK, mak: true) +
                            ",SFBQtyFromTSK = " + MyES(qtyFromTSK, mak: true) +
                            ",SFBTitle = " + MyES(oldTitleId, mak: true) +
                            ",SOId = " + MyES(soid) +
                            " where SFBId = " + sfbid;
                        MyExecute(q);
                        MyLog(uid, "BoL", 2001, long.Parse(sfbid), EntityId);
                    }
                }
                
                r++;
            }
            MyProgressUpdate(pb, 95, "Импорт данных");
            MyLog(uid, "BoL", 130, titleId, EntityId);
            return;
        }

        private void btnExportChecked_Click(object sender, EventArgs e)
        {
            List<long> ExportLst_SId = new List<long>();
            List<long> ExportLst_SpecVer = new List<long>();
            int k = 1;

           /*for (int i = 0; i < dgvSpec.Rows.Count; i++)
            {
                if (dgvSpec.Rows[i].Cells[0].Value == "true")
                {
                    ExportLst_SId.Add((long)dgvSpec.Rows[i].Cells["dgv_SId"].Value);
                    ExportLst_SpecVer.Add((long)(MyGetOneValue("select SVId from vwSpec where SId=" + (long)dgvSpec.Rows[i].Cells["dgv_SId"].Value) ?? -1));
                }
            }*/
            string q = "select ";
            List<string> tt = new List<string>();
            foreach (MyXlsField f in FillingReportStructure)
            {
                q += f.SqlName + ",";
                tt.Add(f.Title);
            }
            q = q.Substring(0, q.Length - 1);
            q += " from SpecVer inner join SpecFill on svid = SFSpecVer " +
                 " left join(select SFBFill, sum(SFBQtyForTSK) BoLQtySum from SpecFillBoL group by SFBFill)d on d.SFBFill = SFId" +
                 " left join SpecFillExec sfe on sfe.SFEFIll = SFId" +
                 " left join InvCfm ic on ic.ICFill = sfid" +
                 " left join SpecFillBol sfb on sfb.SOid = ic.SOId" +
                 " left join SpecFillBol sfb2 on sfb2.SFBFill = SFId and sfb.SOId is null" +
                 " where IsNull(SFQtyBuy,0)> 0 and SFSpecVer in (";
            if (txtSpecNameFilter.Text.ToString() == "" || txtSpecNameFilter.Text.ToString() == txtSpecNameFilter.Tag.ToString())
            {
                q += SpecVer.ToString();
                MyLog(uid, "BoL", 1130, SpecVer, EntityId);
            }
            else
            {
                string selq = "select SVId from vwSpec where SId in (";
                List<string> specver = txtSpecNameFilter.Text.ToString().Split(',').ToList<string>();
                foreach(string sv in specver)
                {
                    selq += sv + ",";
                }
                selq = selq.TrimEnd(',');
                selq += ")";
                specver = MyGetOneCol(selq);
                foreach(string sv in specver)
                {
                    q += sv + ",";
                    MyLog(uid, "BoL", 1130, long.Parse(sv), EntityId);
                }
                q = q.TrimEnd(',');
            }

            q += ")";

            int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
            if (c == 0)
            {
                MsgBox("Нет наполнения, нечего выгружать.");
                return;
            }

            q += " order by SVSpec, case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end";
            MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 17, 17, 15, 5, 5, 25, 25, 25, 20, 11, 11, 10, 10, 14, 7, 11, 11, 10, 12, 10, 8, 8, 8 }, new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 40, 41});
            //MyLog(uid, "BoL", 1130, SpecVer, EntityId);
        }
    }
}
