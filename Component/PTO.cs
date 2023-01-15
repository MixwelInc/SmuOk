using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;
using System.Collections.Specialized;

namespace SmuOk.Component
{
  public partial class PTO : UserControl//MyComponent
  {
    public PTO()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "PTOSpec";
    private string FormSqlPref = "S";
    private long EntityId = -1;
    private long SpecVer = -1;
    private enum ExecType { all, CIW, Acc }

    private List<MyXlsField> FillingReportStructure;

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = new List<MyXlsField>();
      int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkPTOMultiline' and EUIOVaue=1");
      chkPTOMultiline.Checked = i == 1;
      dgvSpecVer.EnableHeadersVisualStyles = false;

      SpecList_RestoreColunns(dgvSpec);

      dgvSpecVer.Columns["dgv_btn_export_ver"].HeaderCell.Style.Font = new Font("Wingdings", 13);
      dgvSpecVer.Columns["dgv_btn_import_ver"].HeaderCell.Style.Font = new Font("Webdings", 11);
      dgvSpecVer.Columns["dgv_btn_save_ver"].HeaderCell.Style.Font = new Font("Wingdings", 11);

      dgvFiles.Columns["dgvPTOPathFile"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 11);
      dgvFiles.Columns["dgvPTOPathFolder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      dgvFiles.Columns["dgvPTOPathCopy"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings 2", 10);

      FillingReportStructure = FillReportData("SpecFilling");
      FillLists();
      FillFilter();
      FillFindFiles();

      FormIsUpdating = false;
      fill_dgv();
      //fill_test();
    }

        private void dgvSpec_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.ToInt32(dgvSpec.Rows[e.RowIndex].Cells["dgv_SState"].Value) == 1)
            {
                dgvSpec.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
            }
        }

        private void FillFilter()
    {
      txtPTOSpecNameFilter.Text = txtPTOSpecNameFilter.Tag.ToString();
      MyFillList(lstPTOSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstPTOSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstPTOSpecDoneFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='PTODone';", "(обработано)");
      MyFillList(lstPTOSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный ПТО)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void FillLists()
    {
      MyFillList(PTOSpecType, "select distinct STId,STName from SpecType", "(укажите тип)");
      MyFillListSimple(PTOSpecArea, "select distinct SArea from Spec order by 1");
      MyFillListSimple(PTOSpecObject, "select distinct SObject from Spec order by 1");
      MyFillListSimple(PTOSpecSystem, "select distinct SSystem from Spec order by 1");
      MyFillList(PTOSpecUser, "select UId,UFIO from vwUser order by UFIO", "(ответственный)");
      MyFillList(PTOSpecCurator, "select UId,UFIO from vwUser where EUIsCurator=1 order by UFIO", "(куратор)");
      //MyFillList((DataGridViewComboBoxColumn)dgvSpecFill.Columns["dgv__SFType"], "select SFTId, SFTName from SpecFillType");
      //MyFillList((DataGridViewComboBoxColumn)dgvSpecFill.Columns["dgv__SFUnit"], "select UId, UName from Unit order by UName;");
    }

    //private void FillExecutors(ExecType et=ExecType.all)
    //{
    //  if (EntityId < 0)
    //  {
    //    lstAcc.Items.Clear();
    //    lstCIW.Items.Clear();
    //    lstExecAcc.Items.Clear();
    //    lstExecCIW.Items.Clear();
    //  }
    //  else
    //  {
    //    if (et == ExecType.all || et == ExecType.CIW)
    //    {
    //      FillExecutorsType(1);
    //    }
    //    if (et == ExecType.all || et == ExecType.Acc)
    //    {
    //      FillExecutorsType(0);
    //    }
    //  }
    //}

    //private void FillExecutorsType(int iIsCiw)
    //{
    //  ListControl lstAll = iIsCiw == 1 ? lstCIW : lstAcc;
    //  ListControl lstExec = iIsCiw == 1 ? lstExecCIW : lstExecAcc;
    //  MyFillList(lstAll, "select EId,EName from Executor" +
    //        " left join(select SEId, EId e from SpecExec inner join Executor on SEExec = EId " +
    //        "           where SEIsCIW = "+ iIsCiw + " and SESpec = " + EntityId + ")q on EId = e" +
    //        " where SEId is null" +
    //        " order by case when ESmuDept=0 then 900000 else ESmuDept end, EName ","(исполнитель)");
    //      MyFillList(lstExec, "select SEExec,EName " +
    //        " from SpecExec inner join Executor on SEExec = EId " +
    //        " where SEIsCIW = "+ iIsCiw + " and SESpec = " + EntityId +
    //        " order by case when ESmuDept=0 then 900000 else ESmuDept end, EName ");
    //}

    private void PTO_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    private void fill_test()
    {
      dgvFiles.Rows.Add(new object[] { Properties.Resources.shared, "H:\\Сметы\\ТПК\\ТПК СЕВЕРО-ВОСТОЧНЫЙ УЧАСТОК\\ТП-11 (Этап 1.1.2 Камера съездов пл.5.2 до пл.18)\\ТП-11.1-ЭО4.3\\1. Актуальные сметы" });
      dgvFiles.Rows.Add(new object[] { Properties.Resources.shared, "H:\\проекты\\5) ТПК\\9. Электрозаводская_ПК209+11,6 - ПК207+05,2 (Рубцовская)\\9. ЭО\\ТП-11.1-ЭО4.3" });
      dgvFiles.Rows.Add(new object[] { Properties.Resources.document, "H:\\проекты\\5) ТПК\\9. Электрозаводская_ПК209+11,6 - ПК207+05,2 (Рубцовская)\\9. ЭО\\ТП-11.1-ЭО4.3\\ТП-11.1-ЭО4.3.pdf" });
      /*
      dgvFiles.Rows.Add(Properties.Resources.opened, "2020-11-16 14:02", "Новиков", "Нужен эскиз чата. Чтобы ОКИЛ или участок мог обратиться к ПТО, например, инициализируя изменения. И историю движа по шифру было видно.");
      dgvFiles.Rows.Add(Properties.Resources.dot, "2020-11-17 3:44", "Айгинин", "Добавил на ПТО, как тебе?");
      dgvFiles.Rows.Add(Properties.Resources.dot, "2020-11-17 9:35", "Новиков", "В целом нормально. Покажу заказчику. Время мелкое(((");
      dgvFiles.Rows.Add(Properties.Resources.dot, "2020-11-17 3:44", "Айгинин", "Будет сильно бесить — сделаю автоподбор размера.");
      dgvFiles.Rows.Add(Properties.Resources.opened, "2020-11-18 19:31", "Новиков", "Прикрути к шифру открывание папки, чтобы не искать по 10 минут в локалке. Задрало!!!");
      dgvFiles.Rows.Add(Properties.Resources.dot, "2020-11-19 5:24", "Айгинин", "Добавил к версии шифра, это логичнее. Заодно разобрался как в гриде менять шрифт заголовков, см. значки -- это не картинки, это тест.");
      */
      //(r.Item(0).ToString, "sp", My.Resources.empty
    }

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void fill_dgv()
    {
      //dgv_S_folder_spec
      //dgv_S_folder_budget
      /*
          string SpecVerName = dgvSpecVer.Rows[iRowIndex].Cells["dgv__SVName"].Value.ToString();
          SpecVerName = "N'%" + SpecVerName.Replace("'", "''") + "'";

          string sFolder = MyGetOneValue("select EDIDir from _engDirectoryIndex where EDIDir like " + SpecVerName)?.ToString() ?? "";
      */
      string q = " select distinct SId,STName,SVName,ManagerAO,dir_spec,dir_budget,pto_block, SState" +
        " from vwSpec ";

      string sName = txtPTOSpecNameFilter.Text;
      if (sName != "" && sName != txtPTOSpecNameFilter.Tag.ToString())
      {
        q += " inner join (select SVSpec svs from SpecVer " +
              " where SVName like " + MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              ")q on svs=SId";
      }

      q += " where 1=1 ";

      long f = lstPTOSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and STId=" + f;

      if (lstPTOSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstPTOSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstPTOSpecDoneFilter.Text == "в работе") q += " and pto_block=0 ";
      else if (lstPTOSpecDoneFilter.Text == "завершено") q += " and pto_block=1 ";
      //!!!
      // 0 в фильтрах = не использовать фильтр
      // 0 в значениях -- по умолчанию
      // противоречие, когда нужно отфильртровать записи по умолчанию, например, не обработанные
      // пока решаем фильтрами типа select -1, <не обработано> union select tid, tname from t
      // но надо подумать, т.к. предется переделывать простые и понятные MyFillList
      // разбивая на MyFillFilter и собственно MyFillList

      if (lstPTOSpecUserFilter.GetLstVal() > 0) q += "and SUser=" + lstPTOSpecUserFilter.GetLstVal();
      else if (lstPTOSpecUserFilter.GetLstVal() == -1) q += "and SUser=0";

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      MyFillDgv(dgvSpec, q);
      dgvSpecFill.MyRestoreColWidthForUser(uid);
      if (dgvSpec.Rows.Count == 0) NewEntity();
      else dgvPTOSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));

      lblCount.Text = dgvSpec.Rows.Count.ToString();
      return;
    }

    private void FillSpec(int iRow = -1)
    {
      Cursor = Cursors.WaitCursor;
      if (iRow >= 0)
      {
        EntityId = (long)dgvSpec.Rows[iRow].Cells["dgv_SId"].Value;
      }
      bool block = (bool)MyGetOneValue("select case when SPTODone is not null then cast(1 as bit) else cast(0 as bit) end d from Spec where SId=" + EntityId);
      //dgvSpecVer.Tag = block ? "block" : null;
      dgvSpecFill.Tag = block ? "block" : null;

      //btnVerAdd.Enabled = !block;
      btnFillAdd.Enabled = !block;
      btnFillClear.Enabled = !block;
      btnEditDone.Enabled = !block;
      btnEditUndone.Enabled = block;

      FillFields();
      //FillExecutors();
      FillVer();
      FormIsUpdating = true;
      FillProblem();
      FormIsUpdating = false;

      ////FillDoc();
      ////FillPerf();
      //lstAcc.Enabled = true;
      //lstCIW.Enabled = true;
      //lstExecAcc.Enabled = true;
      //lstExecCIW.Enabled = true;

      //btnVerAdd.Enabled = true;
      btnScanOpenFolder.Enabled = true;
      btnScanSelectFolder.Enabled = true;
      PTOSpecName.Enabled = false;
      btnSpecSave.Text = "Сохранить";
      PTOSpecName.Focus();
      Cursor = Cursors.Default;
      return;
    }

    private void FillProblem()
    {
      MyFillList(lstQuestion, "select SQTId,SQTName,case when IsNull(SQType,0)>0 then 1 else 0 end ch" +
      " from SpecQuestionType Left Join(select SQType from SpecQuestion where SQSpec = " + EntityId + ")q on SQTId = SQType order by SQTOrder");
    }

    private void FillFields()
    {
      if (EntityId < 1) MyClearForm((Control)this.pn, "PTOSpec");
      else
      {
        string q = "select SNo, SArea, SObject, SSystem, SVName SName, SType, SUser, IsNull(CONVERT(varchar(12), SGetDocsDate, 104), '') SGetDocsDate,SDog,SBudget,SBudgetTotal,SStation,SCurator " +
          " from vwSpec Where SId=" + EntityId.ToString();
        MyFillForm((Control)this.pn, FormControlPref, FormSqlPref, q);
      }
      return;
    }

    private void FillVer()
    {
      string q = "select " +
        " SVId,SVSpec,SVNo,SVDate,SVName,SVSmuLetterNo,SVSmuLetterDate,SVMgtLetterNo,SVMgtLetterDate,case when SVPtoDone is not null then cast(1 as tinyint) else cast(0 as tinyint) end sv_block " +
        " from SpecVer " +
        " where SVSpec=" + EntityId.ToString();
      MyFillDgv(dgvSpecVer, q);
      btnVerAdd.ForeColor = BtnColorNew;
      btnVerCancel.ForeColor = BtnColorDesabled;
      btnVerSave.ForeColor = BtnColorDesabled;
      FillFilling();
      return;
    }

    private void FillFilling()
    {
      string q = "select SFId,SFSpecVer,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFCode,SFMaker,SFUnit,SFQty,SFUnitWeight,SFNote " +
        " from SpecFill " +
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end";
      MyFillDgv(dgvSpecFill, q);
      if (dgvSpecFill.Tag?.ToString() == "block") btnFillAdd.Enabled = false;
      else btnFillAdd.Enabled = SpecVer > 0;
      return;
    }

    private void btnVerAdd_Click(object sender, EventArgs e)
    {
      ((DataTable)dgvSpecVer.DataSource).Rows.Add(null, EntityId);
      MyDgvMarkRow(dgvSpecVer, dgvSpecVer.Rows.Count - 1);
      dgvSpecVer.FirstDisplayedScrollingRowIndex = dgvSpecVer.Rows.Count - 1;
    }

    private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      DataGridView dgv = ((DataGridView)sender);
      if (dgv.Name == "dgvSpecVer" && dgv.Columns[e.ColumnIndex].Name== "dgv_btn_save_ver" && e.RowIndex >= 0 && dgv.Rows[e.RowIndex].Cells["dgv__SVNo"].Value.GetType().Name != "DBNull")
      {
        if (dgv.Rows[e.RowIndex].Cells["dgv_id_SVId"].Value.GetType().Name == "DBNull")
        {
          // новая версия
          decimal newver = decimal.Parse(dgv.Rows[e.RowIndex].Cells["dgv__SVNo"].Value.ToString());
          decimal maxprevver = decimal.Parse(MyGetOneValue("select max(SVNo) from SpecVer where SVSpec=" + EntityId).ToString());
          if (newver <= maxprevver)
          {
            MsgBox("Новая версия должно иметь номер больше, чем существующие.", "Сохранение отменено", MessageBoxIcon.Exclamation);
            return;
          }
        }
        else
        {
          int z = int.Parse(MyGetOneValue("select count (*) from SpecVer where SVSpec=" + EntityId).ToString());
          if (z > 1)
          { 
            // были предыдущие версии, надо сравнить новый номер с максимальным из предыдущих
            decimal svid = decimal.Parse(dgv.Rows[e.RowIndex].Cells["dgv_id_SVId"].Value.ToString());
            decimal newver = decimal.Parse(dgv.Rows[e.RowIndex].Cells["dgv__SVNo"].Value.ToString());
            decimal maxprevver = decimal.Parse(MyGetOneValue("select max(SVNo) from SpecVer where SVSpec=" + EntityId + " and SVId<>" + svid).ToString());
            if (newver <= maxprevver)
            {
              MsgBox("Новая версия должна иметь больший номер, чем предыдущие.", "Сохранение отменено", MessageBoxIcon.Exclamation);
              return;
            }
          }
          
          //MsgBox("Инфирмацию о старых версиях обновлять нельзя.", "Сохранение отменено", MessageBoxIcon.Exclamation);
          //return;
        }
      }
      if (!MyCellContentClick(sender, e, true)) return;

      if (dgv.Name == "dgvSpecVer")
      {
        switch (dgv.Columns[e.ColumnIndex].Name) {
          case "dgv_btn_export_ver":
            FillingExport((long)((DataGridView)sender).Rows[e.RowIndex].Cells["dgv_id_SVId"].Value);
            break;
          case "dgv_btn_import_ver":
            if ((dgv.Rows[e.RowIndex].Cells["dgv_btn_import_ver"].Tag?.ToString() ?? "") == "block") break;
            if ((dgv.Rows[e.RowIndex].Cells["dgv_btn_import_ver"].Tag?.ToString() ?? "") == "from_prev") FillingImportFromPrev();
            else FillingImportFirst();
            break;
          case "dgv_btn_save_ver":
            if (dgv.Tag?.ToString() == "block") break;
            MyExecute("Update Spec set SPTODone=null where sid="+ dgv.Rows[e.RowIndex].Cells["dgv__SVSpec"].Value.ToString());
            dgvSpecVer_CreateRowPathButton(e.RowIndex);
            dgvSpecVer_UpdateParentNameInList(e.RowIndex);
            break;
          /*case "dgv_btn_folder":
            string s = ((DataGridView)sender).Rows[e.RowIndex].Cells["dgv_btn_folder"].Tag.ToString();
            if (s != "" && System.IO.Directory.Exists(s)) Process.Start(s);
            break;*/
        }
        string sSVId = ((DataGridView)sender).Rows[e.RowIndex].Cells["dgv_id_SVId"].Value?.ToString() ?? "";
        SpecVer = sSVId == "" ? 0 : long.Parse(sSVId);
      }
    }

    private void FillingImportFromPrev()
    {
      string q;
      long svid_to = (long)dgvSpecVer.SelectedRows[0].Cells["dgv_id_SVId"].Value;
      long svid_prev = (long)MyGetOneValue("exec uspSpecVer_CanBeFilledFromPrev " + svid_to);
      if (svid_prev == 0)
      {
        MsgBox("Не найдена версия, из которой переносим данные.","Ошибка",MessageBoxIcon.Exclamation);
        return;
      }
      string sSpecName = MyGetOneValue("Select SVName from SpecVer Where SVId=" + svid_to).ToString();
      string sSpecPrevInfo = MyGetOneValue("Select SVName+', '+case when SVNo=0.0 then 'исходная версия' else 'версия № '+cast(SVNo as nvarchar) end +' от '+Convert(nvarchar,SVDate,104) inf from SpecVer Where SVId=" + svid_prev).ToString();
      string sSpecInfo = MyGetOneValue("Select SVName+', '+case when SVNo=0.0 then 'исходная версия' else 'версия № '+cast(SVNo as nvarchar) end +' от '+Convert(nvarchar,SVDate,104) inf from SpecVer Where SVId=" + svid_to).ToString();

      int ii = ((System.Drawing.Image)dgvSpecVer.SelectedRows[0].Cells["dgv_btn_save_ver"].Value).Size.Width;
      if (ii != 1)
      {
        MsgBox("Сначала сохраните строку в базу.");
        return;
      }

      if (MsgBox("Выбранный файл будет использован для перенесения наполнения в новую версию шифра.\n\n" +
        "Переносим\n   из <"+ sSpecPrevInfo + ">\n     в <" + sSpecInfo + ">\n\nПродолжить?", "", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.No) return;

      dynamic oExcel;
      dynamic oSheet;
      bool bNoError = MyExcelImportOpenDialog(out oExcel, out oSheet, "");

      if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
      if (bNoError) MyExcelUnmerge(oSheet);
      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);

      // считываем файл
      List<List<object>> ImportData = new List<List<object>>();
      if (bNoError) bNoError = MyExcelImport_GetData(oSheet, FillingReportStructure, out ImportData, pb);

      if (bNoError) bNoError = FillingImportCheckSpecName(ImportData, oSheet, sSpecName);

      //проверяем, что нет левых SVIds
      if (bNoError) bNoError = FillingImportCheckSFIds(ImportData, oSheet, svid_prev);

      if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet, ImportData);

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportDataFromPrev(ImportData, svid_prev, svid_to, pb);
          FillFilling();
          FillVer();

          q = "Update Spec set SPTODone=null where SId=" + EntityId;
          btnEditDone.Enabled = true;
          MsgBox("НАполнение версии успешно загружено.\n\nНе забудьте завершить редактирование.");
        }
        oExcel.ScreenUpdating = true;
        oExcel.DisplayAlerts = true;
        oExcel.Quit();
      }
      else
      {
        if (oExcel != null)
        {
          oExcel.ScreenUpdating = true;
          oExcel.DisplayAlerts = true;
          oExcel.Visible = true;
          oExcel.ActiveWindow.Activate();
        }
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private void FillingImportDataFromPrev(List<List<object>> ImportData, long svid_prev, long svid, object pb)
    {
      string q = "";
      string s = "";
      string s_id = "";
      string s_ids = "";
      int col_name = 6;
      int col_unit = 10;
      int col_qty = 11;

      string q_add_new = "";
      int count_new = 0;
      
      string q_move_entire_row="";
      q_move_entire_row += "update SpecFill set SFSpecVer=" + svid + " Where SFId in(0 ";

      int count_entire_row = 0;

      int count_non_qty = 0;

      string q_update_incr = "";
      int count_qty_incr = 0;

      string q_update_decr = "";
      int count_qty_decr = 0;

      string qty;

      for (int r = 0; r < ImportData.Count; r++)
      {
        MyProgressUpdate(pb, 80 + 20 * r / ImportData.Count, "Формирование запросов");

        s_id = ImportData[r][0].ToString();
        List<string> val_db = MyGetOneRow("select SFQty, SFName, SFUnit from SpecFill where SFId=" + (s_id == "" ? "0" : s_id));
        if (s_id == "" || val_db[1] != ImportData[r][col_name].ToString() || val_db[2] != ImportData[r][col_unit].ToString())
        {
          // строки без id - просто вставляем новые
          // аналогично для изменения сущностных столбцов - даже не проверяем количество, т.к. название и ед. изм. важнее
          // в базе останется висеть жизнь по части предыдущей версии спеки - надо будет везде отловить !!!
          q_add_new += "insert into SpecFill (SFSpecVer,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFCode,SFMaker,SFUnit,SFQty,SFUnitWeight,SFNote,SFDocs,SFSupplyPID,???SFQty???) \nValues (" + svid;
          for (int c = 2; c < ImportData[r].Count-1; c++) // в последнем столбце "чьи материалы", так что < count-1
          {
            s = ImportData[r][c].ToString();// oSheet.Cells(r, c).Value?.ToString() ?? "";
            if (FillingReportStructure[c].DataType == "decimal") s = s.Replace(",", ".");
            q_add_new += "," + MyES(s, false, FillingReportStructure[c].Nulable);
          }

          // тут разруливаем тему с тем, чьи материалы
          qty = ImportData[r][col_qty].ToString().Replace(",", ".");

          s = ImportData[r][16].ToString();
          switch (s)
          {
            case "заказчик":
              q_add_new = q_add_new.Replace("???SFQty???", "SFQtyGnT");
              q_add_new += "," + qty;
              break;
            case "подрядчик":
              q_add_new = q_add_new.Replace("???SFQty???", "SFQtyBuy");
              q_add_new += "," + qty;
              break;
            default:
              q_add_new = q_add_new.Replace(",???SFQty???", "");
              break;
          }

          q_add_new += ");\n";
          if (s_id == "") count_new++; // добавили просто новую
          else count_non_qty++; // добавили новую при сущностном изменении
        }
        else
        {
          // 1. id указан, ничего важного не изменилось -- просто переносим
          // обновлять ли "неважное"? Конечно, да! Но потом) !!!
          if (decimal.Parse(val_db[0]) == (decimal)ImportData[r][col_qty])
          {
            q_move_entire_row += "," + s_id;
            count_entire_row++;
          }
          // 2. Кoличество увеличилось -- переносим и обновляем
          else if (decimal.Parse(val_db[0]) < (decimal)ImportData[r][col_qty])
          {
            q_move_entire_row += "," + s_id;
            q_update_incr += "update SpecFill set SFQty=" + ImportData[r][col_qty].ToString().Replace(',', '.') + " where SFId=" + s_id + "\n";
            count_qty_incr++;
          }
          // 3. Количество уменьшилось -- создаем в старой новую строку на разницу,
          // в новую версию переносим не все, а только новое количество
          else if (decimal.Parse(val_db[0]) > (decimal)ImportData[r][col_qty])
          {
            q_move_entire_row += "," + s_id;
            q_update_decr += "update SpecFill set SFQty=" + ImportData[r][col_qty].ToString().Replace(',', '.') + " where SFId=" + s_id + "\n";

            q_add_new += "insert into SpecFill (SFSpecVer,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFCode,SFMaker,SFUnit,SFQty,SFUnitWeight,SFNote,SFDocs) \n" +
              " select " + svid_prev + "SFSpecVer,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFCode,SFMaker,SFUnit,/*SFQty*/" + MyES(decimal.Parse(val_db[0]) - (decimal)ImportData[r][col_qty]) + ",SFUnitWeight,SFNote,SFDocs from SpecFill where SFId=" + s_id;
            count_qty_decr++;
          }
          else { throw new DataException("Так не должно быть!"); }
        }
      }

      if (MsgBox("Строк без изменений: " + count_entire_row +
          "\nНовых: " + count_new +
          "\nИзменилось название или ед. изм.: " + count_non_qty +
          "\nКоличество увеличено: " + count_qty_incr +
          "\nКоличество уменьшено: " + count_qty_decr +
          "\n\nПродолжить?", "Анализ файла", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.No
        ) return;

      MyProgressUpdate(pb, 95, "Обновление данных");

      MyExecute(q_move_entire_row + ")");
      if (q_add_new != "") MyExecute(q_add_new);
      if (q_update_incr != "") MyExecute(q_update_incr);
      if (q_update_decr != "") MyExecute(q_update_decr);

      return;

    }    
    
    private bool FillingImportCheckIdsUniq(dynamic oSheet, List<List<object>> ImportData)
    {
      MyProgressUpdate(pb, 60 + 10, "Проверка единичности идентификаторов строк.");
      HashSet<string> ssuniq = new HashSet<string>();
      List<string> errs = new List<string>();

      int rows = ImportData.Count;
      if (rows == 0) return true;
      int c_data = 0; // 0-based SFId
      int c_xls = 1; // 1-based SFId
      string s;

      if (ImportData.Count == 0) return true;

      for (int r = 0; r < ImportData.Count; r++)
      {
        if (ImportData[r][c_data].ToString() == "") continue;
        s = ImportData[r][c_data].ToString();
        if (!ssuniq.Add(s)) errs.Add(s);
      }

      if (errs.Count > 0)
      {
        oSheet.Columns(1).FormatConditions.AddUniqueValues();
        oSheet.Columns(1).FormatConditions(1).DupeUnique = 1;// xlDuplicate;
        oSheet.Columns(1).FormatConditions(1).Font.Color = -16776961;
        oSheet.Columns(1).FormatConditions(1).Interior.Color = 0;
        oSheet.Columns(1).FormatConditions(1).StopIfTrue = 0;
        MsgBox("В файле для загрузки идентификаторы строк не уникальны.", "Ошибка", MessageBoxIcon.Warning);
        return false;
      }

      return true;
    }

    private void FillingImportFirst()
    {
      OpenFileDialog ofd = new OpenFileDialog();
      int z_add=0, z_upd = 0, z_del = 0;
      int ii = ((System.Drawing.Image)dgvSpecVer.SelectedRows[0].Cells["dgv_btn_save_ver"].Value).Size.Width;
      if (ii != 1)
      {
        MsgBox("Сначала сохраните строку в базу.");
        return;
      }

      object ssn = dgvSpecVer.SelectedRows[0].Cells["dgv__SVName"].Value;
      string sSpecName = ssn == null ? "" : ssn.ToString();
      if (sSpecName == "")
      {
        MsgBox("Название шифра не должно быть пустым!");
        return;
      }

      long svid = (long)dgvSpecVer.SelectedRows[0].Cells["dgv_id_SVId"].Value;

      Application.UseWaitCursor = true;

      dynamic oExcel;
      dynamic oSheet;
      bool bNoError = true;
      if(!MyExcelImportOpenDialog(out oExcel, out oSheet, "")) return;
      bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
      if (bNoError && !MyExcelImport_CheckValues(oSheet, FillingReportStructure,pb)) bNoError = false;
      if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
      if (bNoError && !FillingImportCheckSVIds(oSheet, svid, out z_add, out z_upd, out z_del)) bNoError = false;

      if (bNoError)
      {
        if (MessageBox.Show("Всего строк: " + (z_upd + z_add).ToString() + ", в т.ч.:\n" +
            "   будет добавлено: " + z_add + ",\n" +
            "   будет перезаписано: " + z_upd + ".\n\n" +
            "Будет удалено: " + z_del + "."
            , "",MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet, svid);
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
        oExcel.Visible = true;
        oExcel.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }
    
    private void FillingExport(long SpecVerId)
    {
      string q = "select ";
      List<string> tt =new List<string>();
      foreach (MyXlsField f in FillingReportStructure) {
        q += f.SqlName + ",";
        tt.Add(f.Title);
      }
      /*
            case "заказчик":
              q_add_new = q_add_new.Replace("???SFQty???", "SFQtyGnT");
              q_add_new += "," + qty;
              break;
            case "подрядчик":
              q_add_new = q_add_new.Replace("???SFQty???", "SFQtyBuy");
              q_add_new += "," + qty;
              break;
            default:
              q_add_new = q_add_new.Replace(",???SFQty???", "");
              break;
      */
      q = q.Replace("SFSupplyPreType", " case when IsNull(SFQtyGnT,0)>0 then 'заказчик' else 'подрядчик' end SFSupplyPreType"); // костыль, нет такого столбца. Харкод для значения по умолчанию -- плохо!!! А что делать...
      q = q.Substring(0, q.Length - 1);
      q += " \nfrom SpecFill inner join SpecVer on SVId = SFSpecVer Where SVId=" + SpecVerId;

      TechLog(q);
      int c = (int)MyGetOneValue("select count(*)c from \n("+q+")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения.");
        return;
      }

      MyExcelIns(q + " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end", tt.ToArray(), true, new decimal[] { 7, 12, 15.5M, 14, 4.5M, 5.5M,88,25,19,13,11,11,12,14,15,11});
      //TechLog("end export");
      return;
    }

    private void btnSpecHistoryReport_Click(object sender, EventArgs e)
    {
      int i = (int)(MyGetOneValue("select count(*) from SpecVer where SVPtoDone is not null and SVSpec=" + EntityId));
      if (i == 0)
      {
        MsgBox("История формируется по версиям после завершения редактирования наполнения.\n\nНи одной такой версии для шифра не найдено.",PTOSpecName.Text+" – история",MessageBoxIcon.Exclamation);
        return;
      }

      string q;
      MyExportExcel mee;
      List<MyExportExcel> reports_data = new List<MyExportExcel>();

      //uspReport_SpecFillHistory
      mee = new MyExportExcel();
      mee.ReportTitle = "Сравнение";
      mee.sQuery = "exec uspReport_SpecFillHistory " + EntityId;
      //mee.ssTitle = tt.ToArray();
      mee.Title2Rows = true;
      mee.colsWidth = new decimal[] { 10, 10, 18, 6, 7.8M, 50, 9, 9, 9 };
      mee.AfterFormat = "SpecFillHistory";
      reports_data.Add(mee);


      List<string> SpecVerIds;
      SpecVerIds = MyGetOneCol("select SVId from SpecVer where SVSpec=" + EntityId);
      foreach(string SpecVerId in SpecVerIds)
      {
        q = "select ";
        List<string> tt = new List<string>();
        foreach (MyXlsField f in FillingReportStructure)
        {
          q += f.SqlName.Replace("SF","SFH") + ",";
          tt.Add(f.Title);
        }
        q = q.Replace("SFHSupplyPreType", " case when IsNull(SFHQtyGnT,0)>0 then 'заказчик' else 'подрядчик' end SFHSupplyPreType");
        q = q.Substring(0, q.Length - 1);
        q += " \nfrom SpecFillHistory inner join SpecVer on SVId = SFHSpecVer Where SVId=" + SpecVerId; // SpecVer для выбора SVName

        int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
        if (c > 0)
        {
          mee = new MyExportExcel();
          mee.ReportTitle = "вер. " + MyGetOneValue("Select SVNo from SpecVer where SVId=" + SpecVerId);
          mee.sQuery = q + " order by case IsNumeric(SFHNo) when 1 then Replicate('0', 10 - Len(SFHNo)) + SFHNo else SFHNo end, case IsNumeric(SFHNo2) when 1 then Replicate('0', 10 - Len(SFHNo2)) + SFHNo2 else SFHNo2 end";
          mee.ssTitle = tt.ToArray();
          mee.Title2Rows = true;
          mee.colsWidth = new decimal[] { 7, 12, 15.5M, 14, 4.5M, 5.5M, 88, 25, 19, 13, 11, 11, 12, 14, 15, 8, 11 };
          reports_data.Add(mee);
        }
      }
      if (reports_data.Count == 0)
      {
        MsgBox("Нет наполнения.");
        return;
      }
      MyExcel(reports_data);
    }


    private void FillingImportData(dynamic oSheet,long svid)
    {
      string q = "";
      string s = "";
      string s_id = "";
      string s_ids = "";
      //string q_fillhistory;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      string qty;
      int qty_col = 12;
      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 30 * r / rows, "Формирование запросов");
        //проверяем id строки, insert или update
        s_id = oSheet.Cells(r, 1).Value?.ToString() ?? "";
        qty = oSheet.Cells(r, qty_col).Value.ToString().Replace(",", ".");
        if (s_id == "")
        {
          q += "\ninsert into SpecFill (SFSpecVer,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFCode,SFMaker,SFUnit,SFQty,SFUnitWeight,SFNote,SFDocs,SFSupplyPID,???SFQty???) \nValues (" + svid;
          for (int c = 3; c < 17; c++)
          {
            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
            if (FillingReportStructure[c - 1].DataType == "decimal") s = s.Replace(",",".");
            q += ","+MyES(s, false, FillingReportStructure[c - 1].Nulable);
            //;
          }
          // заполняем, если надо, тип получения материалов -- insert
          s = oSheet.Cells(r, 17).Value?.ToString() ?? "";
          switch (s)
          {
            case "заказчик":
              q = q.Replace("???SFQty???", "SFQtyGnT");
              q += "," + qty;
              break;
            case "подрядчик":
              q = q.Replace("???SFQty???", "SFQtyBuy");
              q += "," + qty;
              break;
            default:
              q = q.Replace(",???SFQty???", "");
              break;
          }
          q += ");\n\n";
        }//[SFType],,[SFUnitText]
        else {
          s_ids += s_id + ",";
          q += "update SpecFill set ";
          for (int c = 3; c < 17; c++)
          {
            q += FillingReportStructure[c - 1].SqlName+"=";
            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
            if (FillingReportStructure[c - 1].DataType == "long" || FillingReportStructure[c - 1].DataType == "decimal") s = s.Replace(",",".");
            q += MyES(s, false, FillingReportStructure[c - 1].Nulable)+",";
          }
          // заполняем, если надо, тип получения материалов -- update для распределения количества
          s = oSheet.Cells(r, 17).Value?.ToString() ?? "";
          switch (s)
          {
            case "заказчик":
              q += "SFQtyGnT=" + qty + ",";
              q += "SFQtyBuy=null,SFQtyWarehouse=null,SFQtyWorkshop=null,SFQtySub=null,";
              break;
            case "подрядчик":
              q += "SFQtyBuy=" + qty + ",";
              q += "SFQtyGnT=null,SFQtyWarehouse=null,SFQtyWorkshop=null,SFQtySub=null,";
              break;
          }
          q = q.Substring(0, q.Length - 1);
          q += " Where SFId="+ s_id + ";\n\n";
        }
      }
      if(s_ids!="") s_ids= "delete from SpecFill where SFSpecVer=" + svid + " and SFId not in("+ s_ids.Substring(0, s_ids.Length - 1) +");\n\n";
      else s_ids = "delete from SpecFill where SFSpecVer=" + svid + ";\n\n";
      //q_fillhistory = "delete from ";
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(s_ids+q);
      return;
    }

    private bool FillingImportCheckSFIds(List<List<object>> ImportData, dynamic oSheet, long svid/*, out int z_add, out int z_upd, out int z_del*/)
    {
      string s;
      long z;
      bool e = false;
      int rows = ImportData.Count;
      int c_data = 0; // 0-based SFId
      int c_xls = 1; // 1-based SpecCodeCol
      if (rows == 0) return true;

      if (ImportData.Count == 0) return true;

      for (int r = 0; r < ImportData.Count; r++)
      {
        MyProgressUpdate(pb, 30 + 10 * r / ImportData.Count, "Проверка идентификаторов строк");
        if (ImportData[r][c_data].ToString() == "") continue;
        z = Convert.ToInt64(MyGetOneValue("select count (*) c from SpecFill where SFSpecVer=" + svid.ToString() + " and SFId=" + ImportData[r][c_data].ToString())); //нашлось?

        if (z==0)
        {
          e = true;
          oSheet.Cells(r + 2, c_xls).Interior.Color = 0;
          oSheet.Cells(r + 2, c_xls).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Идентификатор(ы) строк в файле (см. столбец <A>) не найдены в базе».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckSVIds(dynamic oSheet, long svid, out int z_add, out int z_upd, out int z_del)
    {
      z_add = 0;
      z_upd = 0;
      z_del = 0;
      object o_s;
      string s;
      string s_to_del="";
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Разделение строк для добавления, изменения и удаления");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (s != "")
        {
          if (!long.TryParse(s, out z)) z = 0; //не число
          else if (z.ToString() != s || z < 0) z = 0; //не положительное целое
          else
          {
            z = Convert.ToInt64(MyGetOneValue("select count (*) c from SpecFill where SFSpecVer=" + svid.ToString() + " and SFId=" + MyES(s))); //нашлось?
          }
          if (z == 0)
          {
            e = true;
            oSheet.Cells(r, 1).Interior.Color = 13421823;
            oSheet.Cells(r, 1).Font.Color = -16776961;
            oSheet.Cells(r, c).Interior.Color = 0;
            oSheet.Cells(r, c).Font.Color = -16776961;
          }
          else if (z != 1) MsgBox("Так не должно быть! Обязательно пошлите скриншот этого окна разработчику.\n\nДля отладки: FillingImportCheckSVIds, SFSpecVer: " + svid + ", SFId=" + MyES(s));//нашлось >1, странное дело
          z_upd++;
          s_to_del += o_s + ",";
        }
        else
        {
          z_add++; 
        }
      }
      if (s_to_del != "")
      {
        s_to_del = s_to_del.Substring(0, s_to_del.Length - 1);
        z_del = (int)MyGetOneValue("select count (*) c from SpecFill where SFSpecVer=" + svid.ToString() + " and SFId not in (" + s_to_del + ");");
      }
      else
      {
        z_del = (int)MyGetOneValue("select count (*) c from SpecFill where SFSpecVer=" + svid.ToString() + ";");
      }
      if (e) MsgBox("Идентификатор(ы) строк в файле (см. столбец <A>) не найдены в базе для этого шифра.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckSpecName(List<List<object>> ImportData, dynamic oSheet, string SpecCode)
    {
      string s;
      bool e = false;
      int c_data = 1; // 0-based SpecCodeCol
      int c_xls = 2; // 1-based SpecCodeCol
      if (ImportData.Count == 0) return true;

      for (int r = 0; r < ImportData.Count; r++)
      {
        MyProgressUpdate(pb, 30 + 10 * r / ImportData.Count, "Проверка шифра проекта");
        
        if (ImportData[r][c_data].ToString() != SpecCode)
        {
          e = true;
          oSheet.Cells(r+2, 1).Interior.Color = 13421823;
          oSheet.Cells(r+2, 1).Font.Color = -16776961;
          oSheet.Cells(r+2, c_xls).Interior.Color = 0;
          oSheet.Cells(r+2, c_xls).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Шифр проекта в файле (см. столбец <B>) не совпадает с шифром проекта в новой версии (изменении), «" + SpecCode + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckSpecName(dynamic oSheet, string SpecCode)
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
      if (e) MsgBox("Шифр проекта в файле (см. столбец <B>) не совпадает с шифром проекта в изменяемой версии (изменении), «"+ SpecCode +"».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    /*private bool FillingImportCheckValues(dynamic oSheet)
    {
      string s;
      long rl; decimal rd; bool b;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      //int c = range.Columns.Count;
      MyProgressUpdate(pb, 20, "Проверка данных");
      if (rows > 1000)
      {
        if(MessageBox.Show("Файл содержит "+ rows.ToString() + " строк. Продолжить?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
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

      for (int r = 2; r < rows+1; r++)
      {
        MyProgressUpdate(pb, 20+10*r/rows, "Проверка данных");
        for (int c = 1; c < 18; c++)
        {
          b = true;
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          if(!FillingReportStructure[c - 1].Nulable && s == "") b = false;
          if(b && s != "") 
          {
            // Можно добавить дату. Упрощать не надо!
            if(FillingReportStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            if(FillingReportStructure[c - 1].DataType == "decimal") b = decimal.TryParse(s, out rd) && rd > 0;
            if(FillingReportStructure[c - 1].DataType == "vals") b = FillingReportStructure[c - 1].Vals.Contains(s);
          }
          if(!b)
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

    private bool FillingImportCheckTitle(dynamic oSheet)
    {
      MyProgressUpdate(pb, 10, "Проверка заголовков");
      string s;
      bool e = false;
      for(int i=1;i <= FillingReportStructure.Count; i++)
      {// MyXlsField f in FillingReportStructure)
        MyProgressUpdate(pb, 10+i*.5, "Проверка заголовков");
        s = oSheet.Cells(1, i).Value?.ToString() ?? "";
        if (s != FillingReportStructure[i-1].Title)
        {
          e = true;
          oSheet.Cells(1, 1).Interior.Color = 13421823;
          oSheet.Cells(1, 1).Font.Color = -16776961;
          oSheet.Cells(1, i).Interior.Color = 0;
          oSheet.Cells(1, i).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Заголовки столбцов не соответствуют ожидаемым.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }*/

    private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      sender.MyDataError(e);
    }

    private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      MyCellValueChanged(sender, e, ref FormIsUpdating);
    }

    private void btnSpecSave_Click(object sender, EventArgs e)
    {
      string q = "";
      if (PTOSpecType.GetLstVal() <= 0)
      {
        MsgBox("Выберите тип.");
        return;
      }
      switch (btnSpecSave.Text)
      {
        case "Сохранить":
          q = "update Spec set" +
            " SArea=" + MyES(PTOSpecArea) +
            " ,SNo=" + MyES(PTOSpecNo) +
            " ,SObject=" + MyES(PTOSpecObject) +
            " ,SSystem=" + MyES(PTOSpecSystem) +
            " ,SType=" + MyES(PTOSpecType) +
            " ,SUser=" + MyES(PTOSpecUser) +
            " ,SGetDocsDate=" + MyES(PTOSpecGetDocsDate,false,true) +
            " ,SDog=" + MyES(PTOSpecDog) +
            " ,SBudget=" + MyES(PTOSpecBudget) +
            " ,SBudgetTotal=" + MyES(PTOSpecBudgetTotal) +
            " ,SStation=" + MyES(PTOSpecStation) +
            " ,SCurator=" + MyES(PTOSpecCurator) +
            " ,SDogFolder=0" +
            " ,SBudgetFolder=0" +
            " Where SId=" + EntityId;
          MyExecute(q);
          MyLog(uid, "ПТО", 12, EntityId, EntityId);
          break;
        case "Создать":
          q = "insert into Spec (SArea,SNo,SObject,SSystem,SType) Values (" +
            " " + MyES(PTOSpecArea) +
            " ," + MyES(PTOSpecNo) +
            " ," + MyES(PTOSpecObject) +
            " ," + MyES(PTOSpecSystem) +
            " ," + MyES(PTOSpecType) +
            " );  select cast(scope_identity() as bigint) new_id;";
          EntityId = (long)MyGetOneValue(q);
          q = "insert into SpecVer (SVSpec,SVName,SVNo,SVDate) values ("+ EntityId + ","+ MyES(PTOSpecName) + ",0,GetDate());";
          MyExecute(q);
          MyLog(uid, "ПТО", 11, EntityId, EntityId);
          FillSpec();
          break;
      }
      q = "update Spec set SDogFolder=IsNull(EDIId,0) from Spec left join _engDirectoryIndex on SDog=EDILastLeaf where SId=" + EntityId + ";\n";
      q += "update Spec set SBudgetFolder=IsNull(EDIId,0) from Spec left join _engDirectoryIndex on SBudget = EDILastLeaf where SBudget<>'' and SId=" + EntityId + ";\n";
      MyExecute(q);
      FormIsUpdating = true;
      FillLists();
      FillSpec();
      FormIsUpdating = false;
    }

    private void NewEntity()
    {
      MyClearForm((Control)this.pn, "Spec");
      btnVerAdd.Enabled = false;
      dgvSpecFill.MyClearRows();
      dgvSpecVer.MyClearRows();
      btnSpecSave.Text = "Создать";
      PTOSpecUser.SelectedValue = uid;
      btnScanOpenFolder.Enabled = false;
      btnScanSelectFolder.Enabled = false;
      PTOSpecStation.Text = "";
      PTOSpecCurator.SelectedValue = -1;
      PTOSpecName.Enabled = true;
      PTOSpecName.Focus();
    }

    private void btnSpecAdd_Click(object sender, EventArgs e)
    {
     NewEntity();
    }

    private void PTOSpecGetDocsDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (PTOSpecGetDocsDate.Text == "")
      {
        e.Cancel = false;
        PTOSpecGetDocsDate.BackColor = Color.White;
        return;
      }
      DateTime dt = new DateTime(0);
      DateTime.TryParse(PTOSpecGetDocsDate.Text, out dt);
      if (dt == new DateTime(0))
      {
        e.Cancel = true;
        PTOSpecGetDocsDate.BackColor = Color.LightPink;
      }
      else
      {
        PTOSpecGetDocsDate.BackColor = Color.White;
        PTOSpecGetDocsDate.Text = dt.ToString("dd-MM-yyyy");
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      string[] ss = new string[2] { "1", "222" };
      MyExcelIns("Select * from _engRole", ss);
      MyExcelIns("Select * from _engRole");
    }

    private void btnFillAdd_Click(object sender, EventArgs e)
    {
      ((DataTable)dgvSpecFill.DataSource).Rows.Add(null, SpecVer);
      MyDgvMarkRow(dgvSpecFill, dgvSpecFill.Rows.Count - 1);
      dgvSpecFill.FirstDisplayedScrollingRowIndex = dgvSpecFill.Rows.Count - 1;
    }

    private void dgvPTOFill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      MyCellValueChanged(sender, e,ref FormIsUpdating);
    }

    private void chkPTOMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkPTOMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecFill.Columns["dgv__SFName"].DefaultCellStyle.WrapMode = c;
      dgvSpecFill.Columns["dgv__SFMark"].DefaultCellStyle.WrapMode = c;
      dgvSpecFill.Columns["dgv__SFNote"].DefaultCellStyle.WrapMode = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkPTOMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void btnSortByEnter_Click(object sender, EventArgs e)
    {
      dgvSpecFill.Sort(dgvSpecFill.Columns["dgv_id_SFId"], ListSortDirection.Ascending);
    }

    private void dgvSpecFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      dgvSpecFill.MySaveColWidthForUser(uid,e);
    }

    private void btnScanSelectFolder_Click(object sender, EventArgs e)
    {
      if (EntityId < 0) return;
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      if (fbd.ShowDialog() == DialogResult.OK)
      {
        MyExecute("update Spec set SFolder=" + MyES(fbd.SelectedPath) + " where SId=" + EntityId);
      }
    }

    private void btnScanOpenFolder_Click(object sender, EventArgs e)
    {
      if (EntityId < 0) return;
      string s = MyGetOneValue("select IsNull(SFolder,'') from Spec where sid=" + EntityId).ToString();
      if (s == "")
      {
        btnScanSelectFolder_Click(sender, e);
      }
      else
      {
        try{
          Process.Start(s);
        }
        catch(Exception ex)
        {
          btnScanSelectFolder_Click(sender, e);
        }
      }
    }

    private void dgvSpecVer_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      Cursor = Cursors.WaitCursor;
      int iRow = e.RowIndex;
      SpecVer = -1;
      if (iRow>= 0 && dgvSpecVer.Rows[iRow].Cells["dgv_id_SVId"].Value.ToString()!="")
      {
        SpecVer = (long)dgvSpecVer.Rows[iRow].Cells["dgv_id_SVId"].Value;
      }
      FillFilling();
      Cursor = Cursors.Default;
    }

    private void dgvSpecVer_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex<0) return;
      string ColName = dgvSpecVer.Columns[e.ColumnIndex].Name;
      if (ColName == "dgv_btn_export_ver" || ColName == "dgv_btn_import_ver"/* || ColName == "dgv_btn_folder"*/) Cursor = Cursors.Hand;
      else Cursor = Cursors.Default;
    }

    private void dgvSpecVer_MouseHover(object sender, EventArgs e)
    {
      Cursor = Cursors.Default;
    }

    private void dgvSpecVer_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
    {
      Cursor = Cursors.Default;
    }

    private void dgvSpecVer_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
        dgvSpecVer_CreateRowPathButton(i);
    }

    private void dgvSpecVer_UpdateParentNameInList(int iRowIndex)
    {
      FormIsUpdating = true;

      //dgv__SVSpec
      string SpecVerName = dgvSpecVer.Rows[iRowIndex].Cells["dgv__SVName"].Value.ToString();
      string SpecBaseName = MyGetOneValue("select SVName from vwSpec where SId=" + EntityId)?.ToString() ?? "";
      if (SpecBaseName !="" && SpecBaseName == SpecVerName)
      {
        dgvSpec.SelectedRows[0].Cells["dgv_SVName"].Value = SpecVerName;
        PTOSpecName.Text = SpecVerName;
      }

      FormIsUpdating = false;
    }

    private void dgvSpecVer_CreateRowPathButton(int iRowIndex)
    {
      bool b = FormIsUpdating;
      FormIsUpdating = true;

      /*string SpecVerName = dgvSpecVer.Rows[iRowIndex].Cells["dgv__SVName"].Value.ToString();
      SpecVerName = "N'%" + SpecVerName.Replace("'", "''") + "'";

      string sFolder = MyGetOneValue("select EDIDir from _engDirectoryIndex where EDIDir like " + SpecVerName)?.ToString() ?? "";
      if (sFolder != "")
      {
        dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_folder"].Tag = sFolder;
        dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_folder"].Value = Properties.Resources.shared;
      }
      else
      { 
        dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_folder"].Tag = ""; 
        dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_folder"].Value = Properties.Resources.dot;
      }*/

      string sSpecVerId = dgvSpecVer.Rows[iRowIndex].Cells["dgv_id_SVId"].Value.ToString();
      if (sSpecVerId != "")
      {
        long SpecVerId = long.Parse(sSpecVerId);
        long iFromPrev = long.Parse(MyGetOneValue("exec uspSpecVer_CanBeFilledFromPrev " + SpecVerId).ToString());
        if (iFromPrev > 0)
        {
          dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_import_ver"].Tag = "from_prev";
          dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_import_ver"].Value = Properties.Resources.doc_from;
        }
      }
      if (dgvSpecVer.Rows[iRowIndex].Cells["sv_block"].Value?.ToString() == "1")
      {
        dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_import_ver"].Tag = "block";
        dgvSpecVer.Rows[iRowIndex].Cells["dgv_btn_import_ver"].Value = Properties.Resources.tick_small;
      }
      FormIsUpdating = b;
    }

    private void txtPTOSpecNameFilter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtPTOSpecNameFilter.Text = "";
        txtPTOSpecNameFilter_Enter();
        SpecTypeFilter();
      }
      if (e.KeyCode == Keys.Enter)
      {
        SpecTypeFilter();
      }
    }

    private void txtPTOSpecNameFilter_Enter(object sender=null, EventArgs e=null)
    {
      if (txtPTOSpecNameFilter.Text == txtPTOSpecNameFilter.Tag.ToString())
      {
        txtPTOSpecNameFilter.Text = "";
      }
      txtPTOSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void txtPTOSpecNameFilter_Leave(object sender, EventArgs e)
    {
      if (txtPTOSpecNameFilter.Text == "")
      {
        txtPTOSpecNameFilter.Text = txtPTOSpecNameFilter.Tag.ToString();
      }
      txtPTOSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void btnFillClear_Click(object sender, EventArgs e)
    {
      //long clearId= ((long)dgvSpecFill.Rows[e.RowIndex].Cells["dgv_id_SVId"].Value);
      int RowCount = (int)MyGetOneValue("select count(*) c from SpecFill where SFSpecVer="+ SpecVer); 
    }

    private void dgvPTOSpec_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      for (int i = 0; i < e.RowCount; i++)
      {//?.ToString() ?? ""
        int iRowIndex = e.RowIndex + i;
        //dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_btn_folder"].Value = Properties.Resources.shared;
        /*dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_img_spec"].Tag = dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_folder_spec"].Value?.ToString() ?? "";
        dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_img_budget"].Value =
            (dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_folder_budget"].Value?.ToString() ?? "") == "" ?
            Properties.Resources.dot :
            Properties.Resources.shared;
        dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_img_budget"].Tag = dgvPTOSpec.Rows[iRowIndex].Cells["dgv_S_folder_budget"].Value?.ToString() ?? "";
        */
        dgvSpec.Rows[iRowIndex].Cells["dgv_S_img_history"].Value =
          (bool)dgvSpec.Rows[iRowIndex].Cells["dgv_S_block"].Value ?
          Properties.Resources.form_history_lock :
          Properties.Resources.form_history;
      }
    }

    private void dgvPTOSpec_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      //MsgBox(this.ActiveControl.Name);
      //if (this.ActiveControl == null) return;// != dgvPTOSpec) return;
      if (e.RowIndex >= 0)
      {
        FillSpec(e.RowIndex);
      }
    }

    private void dgvPTOSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0) return;
      string s = "";
      string sCol = ((DataGridView)sender).Columns[e.ColumnIndex].Name;
      switch (sCol)
      {
        /*case "dgv_S_img_spec":
        case "dgv_S_img_budget":
          s = ((DataGridView)sender).Rows[e.RowIndex].Cells[sCol].Tag.ToString();
          if (s != "" && System.IO.Directory.Exists(s)) Process.Start(s);
          break;*/
        case "dgv_S_btn_folder":
          MyOpenSpecFolder(EntityId);
          break;
        case "dgv_S_img_history":
          SmuOk.Component.Log log = new Component.Log();
          log.uid = uid;
          log.SpecId = long.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells["dgv_SId"].Value.ToString());
          log.ShowDialog();
          break;
      }
    }

    private void dgvPTOSpec_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
      if(
          ((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewImageColumn && 
          (((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Tag?.ToString() ?? "") != ""
        ) Cursor = Cursors.Hand;
      //string ColName = ((DataGridView)sender).Columns[e.ColumnIndex].Name;
      //if (ColName == "dgv_S_img_spec" || ColName == "dgv_S_img_budget") Cursor = Cursors.Hand;
      else Cursor = Cursors.Default;
    }

    private void dgvPTOSpec_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
    {
      Cursor = Cursors.Default;
    }

    private void btnEditDone_Click(object sender, EventArgs e)
    {
      if (MsgBox("Закончить редактирование?\n\nСпецификация будет заблокорована.","Предупреждение",MessageBoxIcon.Exclamation,MessageBoxButtons.YesNo) == DialogResult.No) return;
      MyExecute("update Spec set SPTODone=" + MyES(DateTime.Now) + " where sid=" + EntityId);
      MyExecute("update SpecVer set SVPtoDone=" + MyES(DateTime.Now) + " where SVPtoDone is null and SVSpec=" + EntityId);
      MyExecute("uspUpdateSpecVerHistory " + SpecVer);
      MyLog(uid, "PTO", 13, SpecVer, EntityId);
      foreach (DataGridViewRow r in dgvSpec.Rows)
      {
        if (r.Cells["dgv_SId"].Value.ToString() == EntityId.ToString())
        {
          r.Cells["dgv_S_img_history"].Value = Properties.Resources.form_history_lock;
          break;
        }
      }
      foreach (DataGridViewRow r in dgvSpecVer.Rows)
      {
        if (r.Cells["dgv_id_SVId"].Value.ToString() == SpecVer.ToString())
        {
          r.Cells["dgv_btn_import_ver"].Value = Properties.Resources.tick_small;
          break;
        }
      }
      FillSpec();
    }

    private void PTOSpecBudgetSum_KeyPress(object sender, KeyPressEventArgs e)
    {
      char c = e.KeyChar;
      if (c==',') c='.';

      if (!char.IsControl(c) && !char.IsDigit(c) &&
          (c != '.'))
      {
        e.Handled = true;
      }

      // only allow one decimal point
      if ((c == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
      {
        e.Handled = true;
      }
    }

    private void txtFindFiles_Enter(object sender=null, EventArgs e=null)
    {
      if (txtFindFiles.Text == txtFindFiles.Tag.ToString())
      {
        txtFindFiles.Text = "";
      }
      txtFindFiles.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void txtFindFiles_Leave(object sender, EventArgs e)
    {
      if (txtFindFiles.Text == "")
      {
        txtFindFiles.Text = txtFindFiles.Tag.ToString();
      }
      txtFindFiles.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void txtFindFiles_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtFindFiles.Text = "";
        txtFindFiles_Enter();
        FillFindFiles();
      }
      if (e.KeyCode == Keys.Enter)
      {
        if (txtFindFiles.Text == "")
        {
          dgvFiles.Rows.Clear();
          return;
        }
        FillFindFiles();
      }
    }

    private void FillFindFiles()
    {
      dgvFiles.Rows.Clear();
      if (txtFindFiles.Text == txtFindFiles.Tag.ToString() || txtFindFiles.Text == "") return;
      string q = "Select EDIDir,EDIIsFile From _engDirectoryIndex where EDILastLeaf like " + MyES(txtFindFiles.Text, true);
      string s, sExt;
      string[,] vals = MyGet2DArray(q);
      if (vals == null) return;
      for (int i = 0; i < vals.GetLength(0); i++)
      {
        if (vals[i, 1] == "0")
        {
          dgvFiles.Rows.Add(Properties.Resources.dot,Properties.Resources.shared, vals[i, 0]);
        }
        else
        {
          s = vals[i, 0];
          if (s.LastIndexOf(".") < s.LastIndexOf("\\"))
          {
            dgvFiles.Rows.Add(Properties.Resources.document, Properties.Resources.shared, vals[i, 0]);
          }
          else
          {
            sExt = s.Substring(s.LastIndexOf(".")).ToLower();
            switch (sExt)
            {
              case ".pdf":
                dgvFiles.Rows.Add(Properties.Resources.document_pdf, Properties.Resources.shared, vals[i, 0]);
                break;
              case ".doc":
              case ".docx":
                dgvFiles.Rows.Add(Properties.Resources.document_word, Properties.Resources.shared, vals[i, 0]);
                break;
              case ".xls":
              case ".xlsx":
                dgvFiles.Rows.Add(Properties.Resources.document_excel, Properties.Resources.shared, vals[i, 0]);
                break;
              case ".jpg":
              case ".jpeg":
              case ".bmp":
              case ".gif":
              case ".png":
                dgvFiles.Rows.Add(Properties.Resources.image, Properties.Resources.shared, vals[i, 0]);
                break;
              default:
                dgvFiles.Rows.Add(Properties.Resources.document, Properties.Resources.shared, vals[i, 0]);
                break;
            }
          }
          
          
        }
          //dgv.Columns[vals[i, 0]].Width = int.Parse(vals[i, 1]);
      }
    }

    private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0)
      {
        try
        {
          string s = "";
          s = dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPath"].Value.ToString();
          switch (dgvFiles.Columns[e.ColumnIndex].Name){
            case "dgvPTOPathFile":
              // кликнули на файл -- проверяем, что там файл
              if (dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPathFile"].Value == Properties.Resources.dot) return;
              Process.Start(s);
              break;
            case "dgvPTOPathFolder":
              // кликнули на папку -- отрезаем файл при необходимости
              if (dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPathFile"].Value != Properties.Resources.dot)
              {
                s = s.Substring(0,s.LastIndexOf('\\'));
              }
              Process.Start(s);
              break;
            case "dgvPTOPathCopy":
              StringCollection files_to_copy = new StringCollection();
              files_to_copy.Add(s);
              Clipboard.SetFileDropList(files_to_copy);
              if (dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPathFile"].Value != Properties.Resources.dot)
                MsgBox("Файл скопирован в буфер.\n\nМожете вставить его в любую папку, нажав Ctrl+V.");
              else //не работает так((( спать хочу, когда-нибудь потом сделаю, может быть
                MsgBox("Папка скопирована в буфер.\n\nМожете вставить ее в любую папку, нажав Ctrl+V.");
              break;
          }
        }
        catch (Exception ex)
        {
          MsgBox("Что-то не работает(((");
        }
      }
    }

    private void btnEditUndone_Click(object sender, EventArgs e)
    {
      if (MsgBox("Разрешить редактирование?\n\nСпецификация будет разблокорована.", "Предупреждение", MessageBoxIcon.Exclamation, MessageBoxButtons.YesNo) == DialogResult.No) return;
      MyExecute("update Spec set SPTODone=null where sid=" + EntityId);
      MyExecute("update SpecVer set SVPtoDone=null where SVSpec=" + EntityId);
      MyExecute("delete from SpecFillHistory where SFHSpecVer=" + SpecVer);
      MyLog(uid, "PTO", 14, SpecVer, EntityId);
      foreach (DataGridViewRow r in dgvSpec.Rows)
      {
        if (r.Cells["dgv_SId"].Value.ToString() == EntityId.ToString())
        {
          r.Cells["dgv_S_img_history"].Value = Properties.Resources.form_history;
          break;
        }
      }
      //FillVer();
      FillSpec();
    }

    private void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
    }

    private void lstQuestion_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (FormIsUpdating) return;
      long t = lstQuestion.GetLstVal();
      long s = EntityId;
      string q;
      if (e.NewValue == CheckState.Checked)
        q = "uspUpdateSpecQuestion " + s + "," + t;
      else
        q = "delete from SpecQuestion Where SQSpec=" + s + " and SQType=" + t;
      MyExecute(q);
      /*if (r == 3) // Куратор для станции, от участка
      {
        MyExecute("update _engUser set EUIsCurator=" + (e.NewValue == CheckState.Checked ? 1 : 0) + " where EUId=" + u);
      }*/
    }

    private void btnSpecQuestionReport_Click(object sender, EventArgs e)
    {
      string q = @"select SId [Шифр Id], SVName [Шифр], SQTName [Вопросы]
        from vwSpec
          inner join SpecQuestion on SId = SQSpec
          inner join SpecQuestionType on SQType = SQTId";
      if (MyGetOneValue("select count(*)c from("+q+")qq").ToString() == "0")
      {
        MsgBox("Вопросов по шифрам не найдено, похоже, что все в порядке.");
        return;
      }
      q += " Order by SVName, SQTOrder";
      MyExcel(q, null, false, null, null, true);
    }

        private void dgvSpecVer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSpecVer.CurrentCell.GetType()
            == typeof(DataGridViewButtonCell))
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить версию: " + dgvSpecVer.CurrentRow.Cells["dgv__SVNo"].Value.ToString() + " ?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string verId = dgvSpecVer.CurrentRow.Cells["dgv_id_SVId"].Value.ToString();
                    string delq = "delete from SpecVer where SVId = " + verId;
                    MyExecute(delq);
                    delq = "delete from SpecFill where SFSpecVer = " + verId;
                    MyExecute(delq);
                    FillVer();
                }
            }
            return;
        }
    }
}