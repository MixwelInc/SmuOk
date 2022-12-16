using System;
using SmuOk.Common;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;
using System.Windows.Forms;
using System.Drawing;

namespace SmuOk.Component
{
  public partial class DocumentAppoint : UserControl
  {
    public DocumentAppoint()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "App";
    private string FormSqlPref = "S";
    private long EntityId = -1;
    private long SpecVer = -1;

    private void DocmentAppoint_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillFilter();
      SpecList_RestoreColunns(dgvSpec);
      FillControlsSize();
      FormIsUpdating = false;
    }

    private void FillControlsSize()
    {
      scAppoint.IsSplitterFixed = false;
      int i = (int)(MyGetOneValue("select EUIOVaue val from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='scAppoint' and EUIOOption='SplitterDistance';") ?? -1);
      //if (i>=0) scAppoint.SplitterDistance = i;
    }

    private void FillFilter()
    {
      // проверяем, что зашел начальник отдела
      long dept = Convert.ToInt64(MyGetOneValue("select count (*)c from _engDept where EDHead=" + uid));
      if (dept == 0)
      {
        MsgBox("В базе отсутствует информация о том, что Вы являетесь руководителем отдела.");
        this.Enabled = false;
        return;
      }
      
      // получаем кол-во сотрудников отдела
      long emp_count = Convert.ToInt64(MyGetOneValue("select count(*) from _engUser where EUDept=" + dept));
      if (emp_count == 0)
      {
        MsgBox("В базе отсутствует информация о сотрудниках отдела, которым можно назначить задание. Обратитесь к администратору для заполнения списка сотрудников вашего отдела.");
        this.Enabled = false;
        return;
      }

      /*int n = (int) MyGetOneValue("select count (*) from _engUser where EUHead=" + uid);
      if (n == 0)
      {
        MsgBox("В базе отсутствует информация о сотрудниках, которым можно назначить задание. Обратитесь к администратору для заполнения списка сотрудников вашего отдела.");
        this.Enabled = false;
      }
      else */
      if (!this.Enabled) this.Enabled = true;


      MyFillList(lstAppointTo, "select UId, UFIO from vwUser where EDId=" + dept + " order by UFIO", "(исполнитель)");

      txtNameFilter.Text = txtNameFilter.Tag.ToString();
      MyFillList(lstTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void fill_dgv()
    {
      string q = " select distinct SId,STName,SVName,ManagerAO from vwSpec ";

      string sName = txtNameFilter.Text;
      if (sName != "" && sName != txtNameFilter.Tag.ToString())
      {
        q += " inner join (select SVSpec svs from SpecVer " +
              " where SVName like " + MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              ")q on svs=SId";
      }

      q += " where pto_block=1 and SState != 1 ";

      long f = lstTypeFilter.GetLstVal();
      if (f > 0) q += " and STId=" + f;

      if (lstHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstUserFilter.GetLstVal() > 0) q += "and SUser=" + lstUserFilter.GetLstVal();
      else if (lstUserFilter.GetLstVal() == -1) q += "and SUser=0";

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      MyFillDgv(dgvSpec, q);
      if (dgvSpec.Rows.Count == 0) NewEntity();
      else dgvSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));
      return;
    }

    private void dgvSpec_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (FormIsUpdating) return;
      if (e.RowIndex >= 0)
      {
        EntityId = (long)dgvSpec.Rows[e.RowIndex].Cells["dgv_SId"].Value;
        FillSpec();
      }
      else
      {
        EntityId = 0;
      }
    }

    private void FillSpec()
    {
      Cursor = Cursors.WaitCursor;
      FillAdtInfo();
      FillDocType();
      FillInWork();
      Cursor = Cursors.Default;
      return;
    }

    private void NewEntity()
    {
      EntityId = -1;
      MyClearForm(this, this.FormControlPref);
      dgvDoc.Enabled = false;
      dgvInWork.Enabled = false;
    }

    private void FillDocType()
    {
      long EntityType = (long)MyGetOneValue("select SType from Spec where SId=" + EntityId);
      string q = "Select DTId, DTName from DocType Inner Join SpecTypeDocType on DTId=STDTDocType where STDTSpecType=" + EntityType;
      MyFillDgv(dgvDoc, q);
    }

    private void FillAdtInfo()
    {
      // Вер.: , Получено: , строк
      string q = "select SVName + ' ('+ STName +') :: версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: '" +
          " +case when NewestFillingCount = 0 then 'нет' else convert(nvarchar, NewestFillingCount) end f " +
          " from vwSpec Where SId=" + EntityId.ToString();
      string s = (string)MyGetOneValue(q);
      AppSpecInfo.Text = s;
    }

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void txtNameFilter_Enter(object sender = null, EventArgs e = null)
    {
      if (txtNameFilter.Text == txtNameFilter.Tag.ToString())
      {
        txtNameFilter.Text = "";
      }
      txtNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void txtNameFilter_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtNameFilter.Text = "";
        txtNameFilter_Enter();
        SpecTypeFilter();
      }
      if (e.KeyCode == Keys.Enter)
      {
        SpecTypeFilter();
      }
    }

    private void txtNameFilter_Leave(object sender, EventArgs e)
    {
      if (txtNameFilter.Text == "")
      {
        txtNameFilter.Text = txtNameFilter.Tag.ToString();
      }
      txtNameFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void lstAppointTo_SelectedIndexChanged(object sender=null, EventArgs e=null)
    {
      FillAppoints();
    }

    private void dgvDoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void FillAppoints()
    {
      bool b = lstAppointTo.Text == "(исполнитель)";
      dgvDoc.Enabled = !b;
      btnSaveDoc.Enabled = !b;
      dgvInWork.Enabled = true;
    }

    private void btnSaveDoc_Click(object sender, EventArgs e)
    {
      if (dgvDoc.Rows.Count == 0 || EntityId < 0) return;
      string q;
      long t, new_id;
      long to_user = lstAppointTo.GetLstVal();
      string apt_note;
      bool b=false;
      for (int i = 0; i < dgvDoc.Rows.Count; i++)
      {
        if (dgvDoc.Rows[i].Cells["chkAdd"].Value != null) // == true
        {
          t = (long)dgvDoc.Rows[i].Cells["dgv_DTId"].Value;
          apt_note = dgvDoc.Rows[i].Cells["Note"].Value?.ToString() ?? "";
          q = "insert into Doc (DType,DAppointFrom,DAppointTo, DNote) values(";
          q += t.ToString() + "," + uid.ToString() + "," + to_user + "," + MyES(apt_note);
          q += "); select cast(scope_identity() as bigint) new_id;";

          new_id = (long)MyGetOneValue(q);
          dgvDoc.Rows[i].Cells["Note"].Value = null;
          dgvDoc.Rows[i].Cells["chkAdd"].Value = null;

          q = "insert into DocSpec (DSDoc,DSSpec) values (" + new_id + "," + EntityId + ");";
          MyExecute(q);
          b = true;
        }
      }
      if (b) FillInWork();
    }

    private void FillInWork()
    {
      string q;
      q = "select Spec, Doc, AppointFrom, AppointTo, AppointDT, ReadyDT, Note from vwAppoint ";
      q += " where SId =" + EntityId;
      q += " order by Doc";
      MyFillDgv(dgvInWork, q);
    }

    private void scAppoint_SplitterMoved(object sender, SplitterEventArgs e)
    {
      if (scAppoint.IsSplitterFixed) return;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','SplitterDistance','" + scAppoint.SplitterDistance.ToString() + "')";
        MyExecute(q);
      }
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
