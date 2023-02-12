using System;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using SmuOk.Common;
using System.Drawing;

namespace SmuOk.Component
{
  public partial class ExecDoc : UserControl
  {
    public ExecDoc()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    //private string FormControlPref = "CuratorSpec";
    //private string FormSqlPref = "S";
    private long EntityId = -1;
    private long SpecVer = -1;
    //private List<MyXlsField> FillingReportStructure;

    private enum ExecType { all, CIW, Acc }

    private void ExecDoc_Load(object sender, EventArgs e)
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
      //FillingReportStructure = FillReportData("Curator");
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
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO,SState from vwSpec ";

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
      MyClearForm(this, "CuratorSpec");
      lstAcc.Enabled = false;
      lstCIW.Enabled = false;
      lstExecAcc.Enabled = false;
      lstExecCIW.Enabled = false;
      btnAddExecAcc.Enabled = false;
      btnAddExecCIW.Enabled = false;
      dgvPTODoc.MyClearRows();
      btnSpecSave.Enabled = false;
      CuratorSpecName.Enabled = true;
      CuratorSpecName.Focus();
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

      FillExecutors();

      lstAcc.Enabled = true;
      lstCIW.Enabled = true;
      lstExecAcc.Enabled = true;
      lstExecCIW.Enabled = true;
      //btnScanOpenFolder.Enabled = true;
      //btnScanSelectFolder.Enabled = true;
      CuratorSpecName.Enabled = false;
      btnSpecSave.Enabled = true;
      CuratorSpecName.Focus();
      Cursor = Cursors.Default;
      return;
    }

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
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
        sQ += ssExecs[i, 0].ToString() + ","; ;
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

    private void btnSpecSave_Click(object sender, EventArgs e)
    {
      string q = "";
            if (dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral || dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
            {
                MsgBox("Запрещено вносить изменения по заблокированным шифрам!");
                return;
            }
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
      FillDoc();
      FormIsUpdating = false;
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

    private void dgvSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0) return;
      if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "dgv_S_btn_folder") MyOpenSpecFolder(EntityId);
    }

    public void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
    }

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void txtSpecNameFilter_Enter(object sender=null, EventArgs e=null)
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
  }
}
