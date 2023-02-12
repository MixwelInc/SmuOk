using System;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;
using SmuOk.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SmuOk.Component
{
  public partial class Curator : UserControl //MyComponent
  {
    public Curator()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "CuratorSpec";
    private string FormSqlPref = "S";
    private long EntityId = -1;
    private long SpecVer = -1;
    private List<MyXlsField> FillingReportStructure;

    private enum ExecType { all, CIW, Acc }

    private void Curator_Load(object sender, EventArgs e)
    {
      LoadMe();
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

        public /*override*/ void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("Curator");
      FillFilter();

      SpecList_RestoreColunns(dgvSpec);

      dgvSpecFillExec.MyRestoreColWidthForUser(uid);

      int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkCuratorMultiline' and EUIOVaue=1");
      chkCutatorMultiline.Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkCuratorSubcode' and EUIOVaue=1");
      chkCuratorSubcode.Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkCuratorType' and EUIOVaue=1");
      chkCuratorType.Checked = i == 1;

      dgvSpecFillExec.Columns[0].Tag = "";

      FormIsUpdating = false;
      fill_dgv();
    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO, SState from vwSpec ";

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
      MyClearForm(this, "CuratorSpec");
      lstAcc.Enabled = false;
      lstCIW.Enabled = false;
      lstExecAcc.Enabled = false;
      lstExecCIW.Enabled = false;
      btnAddExecAcc.Enabled = false;
      btnAddExecCIW.Enabled = false;
      dgvPTODoc.MyClearRows();
      dgvSpecPerf.MyClearRows();
      btnSpecSave.Enabled=false;
      CuratorSpecName.Enabled = true;
      CuratorSpecName.Focus();
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    /*private void FillLists()
    {
      //FillExecutors(); все равно заполняется вместе с шифром
    }*/

    private void FillExecutors(ExecType et = ExecType.all)
    {
      if (EntityId < 0)
      {
        lstAcc.Items.Clear();
        lstCIW.Items.Clear();
        lstExecAcc.Items.Clear();
        lstExecCIW.Items.Clear();
      }
      else
      {
        if (et == ExecType.all || et == ExecType.CIW)
        {
          FillExecutorsType(1);
        }
        if (et == ExecType.all || et == ExecType.Acc)
        {
          FillExecutorsType(0);
        }
      }
    }

    private void FillExecutorsType(int iIsCiw)
    {
      ListControl lstAll = iIsCiw == 1 ? lstCIW : lstAcc;
      ListControl lstExec = iIsCiw == 1 ? lstExecCIW : lstExecAcc;
      MyFillList(lstAll, "select EId,EName from Executor" +
            " left join(select SEId, EId e from SpecExec inner join Executor on SEExec = EId " +
            "           where SEIsCIW = " + iIsCiw + " and SESpec = " + EntityId + ")q on EId = e" +
            " where SEId is null" +
            " order by case when ESmuDept=0 then 900000 else ESmuDept end, EName ", "(исполнитель)");
      MyFillList(lstExec, "select SEExec,EName " +
        " from SpecExec inner join Executor on SEExec = EId " +
        " where SEIsCIW = " + iIsCiw + " and SESpec = " + EntityId +
        " order by case when ESmuDept=0 then 900000 else ESmuDept end, EName ");
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
      CuratorSpecName.Text = MyGetOneValue("select SVName from vwSpec where SId=" + EntityId).ToString();
      SpecVer = (long)(MyGetOneValue("select SVId from vwSpec where SId=" + EntityId) ?? -1);
      FillDoc();
      FillPerf();
      FillAdtInfo();

      FillExecutors();
      FillFilling();

      lstAcc.Enabled = true;
      lstCIW.Enabled = true;
      lstExecAcc.Enabled = true;
      lstExecCIW.Enabled = true;
      //btnScanOpenFolder.Enabled = true;
      //btnScanSelectFolder.Enabled = true;
      CuratorSpecName.Enabled = false;
      btnSpecSave.Enabled=true;
      CuratorSpecName.Focus();
      Cursor = Cursors.Default;
      return;
    }

    private void FillFilling()
    {
      bool b = FormIsUpdating;
      FormIsUpdating = true;

      string q = "select SFEId,SFId SFEFill, SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQty,SFEQty,SFEExec " +
        " from SpecFill left join SpecFillExec on SFId=SFEFill " +
        " where SFSpecVer=-1";
      MyFillDgv(dgvSpecFillExec, q);

      q = "select EId,EName from SpecExec inner join Executor on SEExec=EId	where SEIsCIW=1 and SESpec=" + EntityId;
      MyFillList((DataGridViewComboBoxColumn)dgvSpecFillExec.Columns["dgv__SFEExec"], q);
      
      MyFillList(lstCuratorCIWForAll, q, "(исполнитель)");

      FormIsUpdating = b;

      q = "select SFEId,SFId SFEFill, SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFUnit,SFQty,SFEQty,SFEExec " +
        " from SpecFill left join SpecFillExec on SFId=SFEFill " +
        " where SFSpecVer=" + SpecVer.ToString() +
        " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end ";
      MyFillDgv(dgvSpecFillExec, q);
      return;
    }

    private void FillAdtInfo()
    {
      // Вер.: , Получено: , строк
      string q = "select 'Версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: '" +
          " +case when NewestFillingCount = 0 then 'нет' else convert(nvarchar, NewestFillingCount) end f " +
          " from vwSpec Where SId=" + EntityId.ToString();
      string s = (string)MyGetOneValue(q);
      CuratorSpecInfo.Text = s;
    }

    private void FillDoc()
    {
      //MyFillDgvPivot();
      dgvPTODoc.ScrollBars = ScrollBars.None;

      dgvPTODoc.DataSource = null;
      dgvPTODoc.Columns.Clear();
      string sQuery = "select distinct EId,EName,o " +
        " from SpecExec inner join (" +
        "   select EId,EName,case when ESmuDept = 0 then 900000 else ESmuDept end o from Executor" +
        " )e on SEExec = EId " +
        " where SESpec = " + EntityId +
        " order by o, EName";
      string[,] ssExecs = MyGet2DArray(sQuery);
      if (ssExecs == null) return;
      sQuery = "";
      string sQ = "";
      string sTitles = "";

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Name = "dgv_doc_dtname";
      col.DataPropertyName = "dtname";
      col.HeaderText = "Документы";
      //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
      col.Resizable = DataGridViewTriState.True;
      //col.Frozen = true;
      //col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
      dgvPTODoc.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "dgv_doc_dt";
      col.DataPropertyName = "dt";
      col.HeaderText = "dgv_doc_dt";
      col.Visible = false;
      dgvPTODoc.Columns.Add(col);

      for (int i = 0; i < ssExecs.GetLength(0); i++)
      {
        // 1. добавить столбцы
        //ssExecs[i, 0];
        /*DataGridViewColumn*/
        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn(false);
        chk.FalseValue = 0;
        chk.TrueValue = 1;
        chk.Name = "col_" + ssExecs[i, 0];
        chk.HeaderText = ssExecs[i, 1];
        chk.DataPropertyName = "col_" + ssExecs[i, 0];
        chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        dgvPTODoc.Columns.Add(chk);
        // 2. Собрать сводный запрос
        sQ += ssExecs[i, 0].ToString() + ",";
        sQuery += "[" + ssExecs[i, 0].ToString() + "],";
        sTitles += "[" + ssExecs[i, 0].ToString() + "] col_" + ssExecs[i, 0].ToString() + ","; //[17] col_17,
      }
      sQ = sQ.TrimEnd(',');
      sQuery = sQuery.TrimEnd(',');
      sTitles = sTitles.TrimEnd(',');
      sQuery = "select dt, dtname," + sTitles + "\n" +
        " from (" + "\n" +
        " select dt, DTName, format(e, '0') c, case when isnull(SEDId, 0)=0 then 0 else 1 end sed from( " + "\n" +
        " select DTId dt, DTName, EID e from DocType full join Executor on 1 = 1 where EId in (" + sQ + ")" + "\n" +
        " )q left join " + "\n" +
        " (select * from SpecExecDoc where SEDSpec = " + EntityId + ")q2 " + "\n" +
        " on SEDDocType = dt and SEDExec = e " + "\n" +
        " )pt " + "\n" +
        " pivot(sum(sed) for c in (" + sQuery + "\n" +
        " )) pvt order by DTName";

      MyFillDgv(dgvPTODoc, sQuery);
      dgvPTODoc.PerformLayout();
      dgvPTODoc.ScrollBars = ScrollBars.Both;
      return;
    }

    private void FillPerf()
    {
      string q = "exec usp_FillSpecPerf " + EntityId.ToString();
      MyExecute(q);
      q = "select SPId,SPSpec,SPTName,SPPlan,SPFact " +
      " from SpecPerf inner join SpecPerfType on SPPerfType = SPTId " +
      " where SPSpec=" + EntityId.ToString();
      MyFillDgv(dgvSpecPerf, q);
      return;
    }

    private void btnDocSave_Click(object sender, EventArgs e)
    {
            if (dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral || dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
            {
                MsgBox("Запрещено вносить изменения по заблокированным шифрам!");
                return;
            }
      if (dgvPTODoc.Rows.Count == 0) return;
      int c = dgvPTODoc.Columns["dgv_doc_dt"].Index;
      string sQuery = "";
      string iDoc;
      string iExec;
      string iVal;
      for (int i = 0; i < dgvPTODoc.Rows.Count; i++)
      {
        for (int j = 2; j < dgvPTODoc.Columns.Count; j++)
        {
          iExec = dgvPTODoc.Columns[j].Name.Substring(4);
          iDoc = dgvPTODoc.Rows[i].Cells[c].Value.ToString();
          iVal = dgvPTODoc.Rows[i].Cells[j].Value.ToString();
          sQuery += "exec usp_UpdateSpecDoc " + EntityId + ", " + iExec + ", " + iDoc + ", " + iVal + ";\n";
        }
      }
      MyExecute(sQuery);
      FillDoc();
      dgvPTODoc.Focus();
    }

    private void lstCIW_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnAddExecCIW.Enabled = !(lstCIW.SelectedIndex == 0);
    }

    private void lstAcc_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnAddExecAcc.Enabled = !(lstAcc.SelectedIndex == 0);
    }

    private void btnAddExecCIW_Click(object sender, EventArgs e)
    {
      if (lstCIW.SelectedIndex > 0)
      {
        MyMoveSelectedItemFromListToList(lstCIW, lstExecCIW);
      }
    }

    private void btnAddExecAcc_Click(object sender, EventArgs e)
    {
      if (lstAcc.SelectedIndex > 0)
      {
        MyMoveSelectedItemFromListToList(lstAcc, lstExecAcc);
      }
    }

    private void lstExecCIW_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
      {
        MyMoveSelectedItemFromListToList(lstExecCIW, lstCIW);
      }
    }

    private void lstExecAcc_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
      {
        MyMoveSelectedItemFromListToList(lstExecAcc, lstAcc);
      }
    }

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void btnSpecSave_Click(object sender, EventArgs e)
    {
            if (dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral || dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
            {
                MsgBox("Запрещено вносить изменения по заблокированным шифрам!");
                return;
            }
            string q = "";
      switch (btnSpecSave.Text)
      {
        case "Сохранить":
          q = "exec UpdateSpecExecutors " + EntityId + ",'";
          q += lstExecCIW.GetDataColumnForDB();
          q += "',1"; // @iCIW_or_Acc int /*1 for ciw, 0 for acc*/
          MyExecute(q);

          q = "exec UpdateSpecExecutors " + EntityId + ",'";
          q += lstExecAcc.GetDataColumnForDB();
          q += "',0"; // @iCIW_or_Acc int /*1 for ciw, 0 for acc*/
          MyExecute(q);
          break;
      }
      FormIsUpdating = true;
      FillExecutors();
      FillFilling();
      FormIsUpdating = false;
    }

    private void txtPTOSpecNameFilter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtSpecNameFilter.Text = "";
        txtPTOSpecNameFilter_Enter();
        SpecTypeFilter();
      }
      if (e.KeyCode == Keys.Enter)
      {
        SpecTypeFilter();
      }
    }

    private void txtPTOSpecNameFilter_Enter(object sender = null, EventArgs e = null)
    {
      if (txtSpecNameFilter.Text == txtSpecNameFilter.Tag.ToString())
      {
        txtSpecNameFilter.Text = "";
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void txtPTOSpecNameFilter_Leave(object sender, EventArgs e)
    {
      if (txtSpecNameFilter.Text == "")
      {
        txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void chkCuratorSubcode_CheckedChanged(object sender, EventArgs e)
    {
      dgvSpecFillExec.Columns["dgv_SFSubcode"].Visible = chkCuratorSubcode.Checked;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkCuratorSubcode.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void chkCuratorType_CheckedChanged(object sender, EventArgs e)
    {
      dgvSpecFillExec.Columns["dgv_SFType"].Visible = chkCuratorType.Checked;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkCuratorType.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void dgvSpecCuratorFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      ((DataGridView)sender).MySaveColWidthForUser(uid, e);
    }

    private void chkCutatorMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkCutatorMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecFillExec.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
      dgvSpecFillExec.Columns["dgv_SFMark"].DefaultCellStyle.WrapMode = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkCutatorMultiline.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    private void dgvSpecCuratorFill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      MyCellValueChanged(sender, e, ref FormIsUpdating);
    }

    private void dgvSpecCuratorFill_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      MyCellContentClick(sender, e, true);
    }

    private void lstCuratorCIWForAll_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FormIsUpdating) return;
      if (lstCuratorCIWForAll.GetLstVal() == 0) return;
      if (MsgBox("Да?", "", MessageBoxIcon.None, MessageBoxButtons.YesNo) != DialogResult.Yes)
      {
        FormIsUpdating = true;
        lstCuratorCIWForAll.SelectedIndex = 0;
        FormIsUpdating = false;
        return;
      }
      string q= " insert into SpecFillExec(SFEFill,SFEQty,SFEExec) "+
                " select SFId, SFQty, " + lstCuratorCIWForAll.GetLstVal() +
                " from SpecFill left join SpecFillExec on SFId = SFEFill where SFEId is null and SFSpecVer = "+SpecVer;
      MyExecute(q);
      FillFilling();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
            //MyExcelCuratorReport(EntityId);/////////////вынести в отдельную функцию добавить кнопку загрузку оставить по старой форме

      string q = "select ";
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        q += f.SqlName + ",";
        tt.Add(f.Title);
      }
      q = q.Substring(0, q.Length - 1);
      q += " \nfrom SpecFill inner join SpecVer on SFSpecVer=SVId left join SpecFillExec on SFId = SFEFill left join Executor on SFEExec=EId Where SVId=" + SpecVer.ToString();

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      q += " order by case IsNumeric(SFNo) when 1 then Replicate('0', 10 - Len(SFNo)) + SFNo else SFNo end, case IsNumeric(SFNo2) when 1 then Replicate('0', 10 - Len(SFNo2)) + SFNo2 else SFNo2 end";
      MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 17, 17, 5, 5, 110, 11, 6, 6, 15}, new int[] { 3, 4, 5, 6, 7});
      MyLog(uid, "Curator", 1080, SpecVer, EntityId);
    }

    private void btnImport_Click(object sender, EventArgs e)
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

     /* try
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
      if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure,pb)) bNoError = false;   //FillingImportCheckTitle(oSheet)) bNoError = false;
      MyExcelUnmerge(oSheet);
      if (bNoError && !FillingImportCheckValues(oSheet)) bNoError = false;
      if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
      if (bNoError && !FillingImportCheckSVIds(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckSums(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckSumElements(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckExecs(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckExecsUniq(oSheet, SpecVer)) bNoError = false;

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet, SpecVer);
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

    private void FillingImportData(dynamic oSheet, long svid)
    {
      string q = "";
      string s = "";
      long i_id = 0;
      string s_exec = "";
      decimal d_qty = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      // !!! ЗАМЕНИТЬ СОХРАНЕНКОЙ, чтобы удалять, обновлять и добавлять, а не просто удалять
      // пока весь остальной учет не посыпался
      Dictionary<long, string> execs = new Dictionary<long, string>();

      q= "select SEExec, EName " +
        " from SpecExec inner join Executor on SEExec = EId " +
        " where SEIsCIW = 1 and SESpec = " + EntityId;
      MyFillDictionary(execs, q);

      q = "delete dd \n"+
        " from SpecFillExec dd \n" +
        " inner join SpecFill on SFEFill = SFId \n"+
        " where SFSpecVer = " + svid + ";\n";
      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 30 * r / rows, "Формирование запросов");
        i_id = (long)oSheet.Cells(r, 1).Value;
        s_exec = oSheet.Cells(r, 10).Value.ToString();
        d_qty = (decimal)oSheet.Cells(r, 9).Value;

        q += "insert into SpecFillExec (SFEFill,SFEExec,SFEQty) Values (" ;
        q += i_id.ToString() + ",";
        q += execs.FirstOrDefault(x => x.Value == s_exec).Key + ",";
        q += MyES(d_qty);
        q += ");\n";
      }
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      MyLog(uid, "Curator", 80, SpecVer, EntityId, s_exec, (rows-1).ToString());
      return;
    }

    private bool FillingImportCheckTitle(dynamic oSheet)
    {
      MyProgressUpdate(pb, 10, "Проверка заголовков");
      string s;
      bool e = false;
      for (int i = 1; i <= FillingReportStructure.Count(); i++) 
      {// MyXlsField f in FillingReportStructure)
        MyProgressUpdate(pb, 10 + i * .5, "Проверка заголовков");
        s = oSheet.Cells(1, i).Value?.ToString() ?? "";
        if (s != FillingReportStructure[i - 1].Title)
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
    }

    private bool FillingImportCheckValues(dynamic oSheet)
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
        for (int c = 1; c < FillingReportStructure.Count()+1; c++)
        {
          b = true;
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          if (!FillingReportStructure[c - 1].Nulable && s == "") b = false;
          if (b && s != "")
          {
            // Можно добавить дату. Упрощать не надо!
            if (FillingReportStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            if (FillingReportStructure[c - 1].DataType == "decimal") b = decimal.TryParse(s, out rd) && rd > 0;
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

      //z = Convert.ToInt64(MyGetOneValue("select count (SFId) c from SpecFill where SFSpecVer=" + svid.ToString()));
      z = Convert.ToInt64(MyGetOneValue("select count (SFId) c from SpecFill where SFSpecVer=" + svid.ToString() + " and SFId not in ("+ string.Join(",",ss.Distinct()) +")"));

      if (e) sErr = "Идентификатор(ы) строк в файле (см. столбец <A>) не найдены в базе для этого шифра.\n";
      if (z > 0) { sErr += "\nВ файле для загрузки не найдена часть идентификаторов строк (" + z + ")."; e = true; }

      if (sErr!="") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckSums(dynamic oSheet, long svid)
    {
      string sId;
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      string sSum;
      decimal d;
      int cs = 8; // 1-based SFQty
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 10 * r / rows, "Проверка идентификаторов строк.");
        sId = oSheet.Cells(r, c).Value.ToString();
        sSum = oSheet.Cells(r, cs).Value.ToString();
        d = decimal.Parse(sSum);
        z = Convert.ToInt64(MyGetOneValue("select count (*) c, sum(SFQty) s from SpecFill "+
          " where SFSpecVer=" + svid.ToString() + " and SFId=" + MyES(sId) +" and SFQty=" + MyES(d))); //нашлось?
        if (z == 0)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, cs).Interior.Color = 0;
          oSheet.Cells(r, cs).Font.Color = -16776961;
        }
        //s_to_del += o_s + ",";
      }
      if (e) MsgBox("Сумма к распределению в файле (см. столбец <H>) указана неверно.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }
    
    private bool FillingImportCheckSumElements(dynamic oSheet, long svid)
    {
      int iId;
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      string sSum;
      decimal d;
      int cs = 8; // 1-based SFQty
      if (rows == 1) return true;

      Dictionary<int, decimal> sums = new Dictionary<int, decimal>();
      List<int> sum_errors = new List<int>();

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 60 + 5 * r / rows, "Проверка суммы по нескольким исполнителям");
        iId = int.Parse(oSheet.Cells(r, c).Value.ToString());
        sSum = oSheet.Cells(r, cs+1).Value.ToString();
        d = decimal.Parse(sSum);
        try
        {
          sums[iId] += d;
        } 
        catch
        {
          sums[iId] = d;
        }
      }
      foreach (KeyValuePair<int, decimal> ss in sums)
      {
        //Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
        d = Convert.ToDecimal(MyGetOneValue("select sum(SFQty) s from SpecFill "+
        " where SFSpecVer=" + svid.ToString() + " and SFId=" + MyES(ss.Key))); //нашлось?
        if (d != ss.Value) sum_errors.Add(ss.Key);
      }

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 65 + 5 * r / rows, "Проверка суммы по нескольким исполнителям");
        iId = int.Parse(oSheet.Cells(r, c).Value.ToString());
        if(sum_errors.FindIndex(x => x == iId) != -1)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, cs+1).Interior.Color = 0;
          oSheet.Cells(r, cs+1).Font.Color = -16776961;
        }
      }

      if (e) MsgBox("Количество к исполнению распределено по исполнителям не полностью либо излишне (см. столбцы <H>, <I>).", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckExecs(dynamic oSheet, long svid)
    {
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      string sExec;
      int cs = 10; // 1-based SFExecutor
      if (rows == 1) return true;

      List<string> execs = new List<string>();
      List<string> execs_unic = new List<string>();
      List<string> execs_errors = new List<string>();

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 70 + 5 * r / rows, "Проверка наличия исполнителя для шифра");
        sExec = oSheet.Cells(r, cs).Value.ToString();
        execs.Add(sExec);
      }
      execs_unic = (List<string>)execs.Distinct().ToList();
      foreach(string s in execs_unic)
      {
        if ((int)MyGetOneValue("select count(*) c from SpecExec inner join Executor on SEExec=EId Where SEIsCIW=1 and SESpec=" + EntityId.ToString() + " and EName="+MyES(s)) == 0)
        {
          execs_errors.Add(s);
        }
      }

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 75 + 5 * r / rows, "Проверка наличия исполнителя для шифра");
        sExec = oSheet.Cells(r, cs).Value.ToString();
        if (execs_errors.Contains(sExec))
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, cs).Interior.Color = 0;
          oSheet.Cells(r, cs).Font.Color = -16776961;
        }
      }

      if (e) MsgBox("Исполнитель указан неверно (см. столбец <J>).", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private bool FillingImportCheckExecsUniq(dynamic oSheet, long svid)
    {
      int iId;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      string sExec;
      int cs = 10; // 1-based SFExecutor
      if (rows == 1) return true;

      // Key=iId.ToString()+":"+ sExec
      // Value = iId
      Dictionary<string, int> execs = new Dictionary<string, int>(); 
      List<int> id_errors = new List<int>();

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 80 + 10 * r / rows, "Проверка уникальности исполнителя для позиции");
        iId = int.Parse(oSheet.Cells(r, c).Value.ToString());
        sExec = oSheet.Cells(r, cs).Value.ToString();
        //d = decimal.Parse(sSum);
        try
        {
          execs[iId.ToString()+":"+ sExec] += 1;
          id_errors.Add(r);
        } 
        catch
        {
          execs[iId.ToString() + ":" + sExec] = 1;
        }
      }

      foreach (int r in id_errors)
      {
        e = true;
        oSheet.Cells(r, 1).Interior.Color = 13421823;
        oSheet.Cells(r, 1).Font.Color = -16776961;
        oSheet.Cells(r, cs).Interior.Color = 0;
        oSheet.Cells(r, cs).Font.Color = -16776961;
        if (oSheet.Cells(r, 1).Value == oSheet.Cells(r-1, 1).Value && oSheet.Cells(r, cs).Value == oSheet.Cells(r - 1, cs).Value)
        {
          oSheet.Cells(r-1, 1).Interior.Color = 13421823;
          oSheet.Cells(r-1, 1).Font.Color = -16776961;
          oSheet.Cells(r-1, cs).Interior.Color = 0;
          oSheet.Cells(r-1, cs).Font.Color = -16776961;
        }
      }

      if (e) MsgBox("Найдено задвоение исполнения по одной записи с одинаковым исполнителем (см. столбец <J>).", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void dgvSpecFillExec_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      //MsgBox(e.RowIndex.ToString()+" : " + e.ColumnIndex.ToString());
    }

    public void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
    }

    private void dgvSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0) return;
      if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "dgv_S_btn_folder") MyOpenSpecFolder(EntityId);
    }

        private void button1_Click(object sender, EventArgs e)
        {
            MyExcelCuratorReport(EntityId);
            MyLog(uid, "Curator", 1080, SpecVer, EntityId);
        }
    }
}
