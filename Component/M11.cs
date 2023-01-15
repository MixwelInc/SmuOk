using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;
using SmuOk.Common;

namespace SmuOk.Component
{
  public partial class M11 : UserControl
  {
    public M11()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private long EntityId = -1;
    private long SpecVer = -1;
    int shit = 0;
    bool flag = false;
    private List<MyXlsField> FillingReportStructure;

    private void SupplyOrder_Load(object sender, EventArgs e)
    {
      LoadMe();
      fill_dgv(); //контент приходит отсюда
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
      FillingReportStructure = FillReportData("M11"); //здесь набиваем структуру ответа
      FillFilter(); //наполнение значений для фильтров

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
      txtFilter1.Text = txtFilter1.Tag.ToString();
      txtFilter2.Text = txtFilter2.Tag.ToString();
      filter1.Text = "(фильтр 1)";
      filter2.Text = "(фильтр 2)";
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecUserFilter, "select -1 uid,'<не выбран>' ufio union select UId, UFIO from vwUser order by UFIO;", "(ответственный)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    private void fill_dgv()
    {
            string filterText1 = txtFilter1.Text;
            string filterText2 = txtFilter2.Text;
            string q, sName;
            long f, managerAO;
            if ((filterText1 == "" || filterText1 == txtFilter1.Tag.ToString()) && (filterText2 == "" || filterText2 == txtFilter2.Tag.ToString()))
            {
                q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO,SState ";

                if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += " from SupplyOrder so" +
                         " inner join vwSpecFill vw on so.SOFill = vw.SFId " +
                         " inner join vwSpec vws on vws.SId = vw.SId " +
                         " inner join SpecFill sf on sf.SFId = so.SOFill ";
                }
                else //default search, all rows
                {
                    q += " from vwSpec vws left join vwSpecFill vw on vw.SId = vws.SId";
                }

                sName = txtSpecNameFilter.Text;
                if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
                {
                    q += " inner join (select SVSpec svs from SpecVer " +
                          " where SVName like " + MyES(sName, true) +
                          " or SVSpec in (" + sName +
                          "))q on svs=vws.SId";
                }

                q += " where vws.pto_block=1 and vws.SType != 6 and vw.[Чьи материалы] = 'заказчик' ";

                f = lstSpecTypeFilter.GetLstVal();
                if (f > 0) q += " and vws.STId=" + f;

                if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
                else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";
                else if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += " and isnull(SFQtyGnT,0) > 0 ";
                }

                if (lstSpecUserFilter.GetLstVal() > 0) q += "and vws.SUser=" + lstSpecUserFilter.GetLstVal();
                else if (lstSpecUserFilter.GetLstVal() == -1) q += "and vws.SUser=0";

                managerAO = lstSpecManagerAO.GetLstVal();
                if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());
            }
            else
            {

                q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO,SState " +
                          "from vwSpec vws inner join vwSpecFill vwsf on vwsf.SId = vws.SId inner join SupplyOrder so on so.SOFill = vwsf.SFId";

                sName = txtSpecNameFilter.Text;
                if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
                {
                    q += " inner join (select SVSpec svs from SpecVer " +
                          " where SVName like " + MyES(sName, true) +
                          " or SVSpec in (" + sName +
                          "))q on svs=SId";
                }

                q += " where pto_block=1 ";

                f = lstSpecTypeFilter.GetLstVal();
                if (f > 0) q += " and STId=" + f;

                if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
                else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";
                else if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += " and SOId is not null and isnull(SFQtyGnT,0) > 0 ";
                }

                if (lstSpecUserFilter.GetLstVal() > 0) q += "and SUser=" + lstSpecUserFilter.GetLstVal();
                else if (lstSpecUserFilter.GetLstVal() == -1) q += "and SUser=0";

                managerAO = lstSpecManagerAO.GetLstVal();
                if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

                filterText1 = txtFilter1.Text;
                if (filterText1 != "" && filterText1 != txtFilter1.Tag.ToString())
                {
                    if (filter1.Text == "Ответственный ОС")
                    {
                        q += " and so.SOResponsOS = '" + filterText1 + "' ";
                    }
                    if (filter1.Text == "№ планирования 1С / письма в ТСК")
                    {
                        q += " and so.SOPlan1CNum = '" + filterText1 + "' ";
                    }
                }
                filterText2 = txtFilter2.Text;
                if (filterText2 != "" && filterText2 != txtFilter2.Tag.ToString())
                {
                    if (filter2.Text == "Ответственный ОС")
                    {
                        q += " and so.SOResponsOS = '" + filterText2 + "' ";
                    }
                    if (filter2.Text == "№ планирования 1С / письма в ТСК")
                    {
                        q += " and so.SOPlan1CNum = '" + filterText2 + "' ";
                    }
                }
            }

            MyFillDgv(dgvSpec, q);
            if (dgvSpec.Rows.Count == 0) NewEntity();
      else dgvSpec_CellClick(dgvSpec, new DataGridViewCellEventArgs(0, 0));
            return;
    }

    private void NewEntity()
    {
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

    private void txtSpecNameFilter_Enter(object sender = null, EventArgs e = null)
    {
      if (txtSpecNameFilter.Text == txtSpecNameFilter.Tag.ToString())
      {
        txtSpecNameFilter.Text = "";
      }
      txtSpecNameFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
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
        EntityId = (long)dgvSpec.Rows[e.RowIndex].Cells["dgv_SId"].Value; //считываем идентификатор спецификации на которую кликнули
        FillSpec();
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
         string q = "select m.Id," +
        " SF.SFId, SF.SFNo, SF.SFNo2, sf.SFName, SF.SFId, SF.SFUnit, m.Requested, m.Released " +
        " from vwSpecFill sf" +//
        " outer apply(select top(1) SFBId ,sum(SFBQtyForTSK)ssum from SpecFillBol where SFBFill = sf.SFId group by SFBId)sfb" +
        " left join M15 mm on mm.FillId = sf.SFId  or mm.PID = sf.SFSupplyPID " +
        " left join M11 m on sf.SFId = m.FillId " +
        " where sf.SVId = " + SpecVer.ToString() +
        " and sf.SType != 6 and (sfb.SFBId is not null or mm.M15Id is not null) ";

            q += "\n order by case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) + SF.SFNo else SF.SFNo end, " +
                    " case IsNumeric(SF.SFNo2) when 1 then Replicate('0', 10 - Len(SF.SFNo2)) + SF.SFNo2 else SF.SFNo2 end";

            MyFillDgv(dgvSpecFill, q);
    }

    private void FillAdtInfo() //получаем доп инфу вверху страницы (версию, дата обновы и т.д.)
    {
      // Вер.: , Получено: , строк
      string q = "select SVName + ' :: версия: ' + cast(SVNo as nvarchar) + ', получена: ' + case when SVDate is null then 'УКАЖИТЕ ДАТУ!' else convert(nvarchar, SVDate, 104) end + ', строк: ' " +
          "  +isnull(cc,0) " +
          "	+' ('+case when NewestFillingCount = 0 then 'нет' else convert(nvarchar, NewestFillingCount) end +')' f " +
          "  from vwSpec " +
          "	left join ( " +
          "	select SFSpecVer, cast(count(SFEId) as nvarchar) cc from SpecFill left join SpecFillExec on SFId=SFEFill " +
          "	where 1=1" +
          "	group by SFSpecVer " +
          ")ff " +
          "	on SFSpecVer=SVId " +
          "	Where SVId= " + SpecVer.ToString();
      string s = (string)MyGetOneValue(q);
      SpecInfo.Text = s;
    }

    private void chkDoneSubcode_CheckedChanged(object sender, EventArgs e)
    {
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
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (chkDoneType.Checked ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    public void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
    }

    private void chkDoneMultiline_CheckedChanged(object sender, EventArgs e)
    {
      DataGridViewTriState c = chkDoneMultiline.Checked ? DataGridViewTriState.True : DataGridViewTriState.False;
      dgvSpecFill.Columns["dgv_SFName"].DefaultCellStyle.WrapMode = c;
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
      if (FormIsUpdating) return;
      dgvSpecFill.MySaveColWidthForUser(uid, e);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
            MyExcelM11Report_Done(EntityId);
            return;
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
            if(dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral)
            {
                MsgBox("Запрещено вносить изменения по заблокированным шифрам!");
                return;
            }
      string sSpecName = MyGetOneValue("select SVName from vwSpec where SId=" + EntityId).ToString();
      long svid = long.Parse(MyGetOneValue("select svid from vwSpec where SId=" + EntityId).ToString());
      if (sSpecName == "")
      {
        MsgBox("Шифр " + EntityId.ToString() + " не найден.");
        return;
      }
      dynamic oExcel;
      dynamic oSheet;
      bool bNoError = MyExcelImportOpenDialog(out oExcel, out oSheet, "");

            string num = oSheet.Cells(5, 9).Value?.ToString() ?? "";
            if (num == "")
            {
                bNoError = false;
                MsgBox("Необходимо ввести номер накладной");
            }

      //if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb)) bNoError = false;   //FillingImportCheckTitle(oSheet)) bNoError = false;
      

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet, SpecVer);
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

    private void FillingImportData(dynamic oSheet, long svid) //импорт необходимо переработать, удалять по паре з на пост и заявка
    {
      object s;
      string s_id, M15Id, sfeid;
      DateTime dt;
      dynamic range = oSheet.UsedRange;
            //лучше вытащить все в структуру а не работать с экселем (начиная с проверки)
      int rows = range.Rows.Count;
      string num = oSheet.Cells(5, 9).Value?.ToString() ?? "";
            int r = 18;
            string insq = "insert into M11 (Num, FillId, Requested, Released) values ";
            while ((oSheet.Cells(r, 1).Value?.ToString() ?? "") != "") //до пустой строки
            {
                string id = oSheet.Cells(r, 1).Value?.ToString() ?? "";
                string strReq = oSheet.Cells(r, 9).Value?.ToString() ?? "0";
                string strRel = oSheet.Cells(r, 10).Value?.ToString() ?? "0";
                Decimal.TryParse(strReq, out decimal Req);
                Decimal.TryParse(strRel, out decimal Rel);
                insq += "(" + MyES(num) + "," + id + "," + MyES(Req) + "," + MyES(Rel) + "),";
                r++;
            }
            insq = insq.Substring(0, insq.Length - 1);
            MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(insq);
      return;
    }


        private void txtFilter1_Leave(object sender, EventArgs e)
        {
            if (txtFilter1.Text == "")
            {
                txtFilter1.Text = txtFilter1.Tag.ToString();
            }
            txtFilter1.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
        }

        private void txtFilter2_Leave(object sender, EventArgs e)
        {
            if (txtFilter2.Text == "")
            {
                txtFilter2.Text = txtFilter2.Tag.ToString();
            }
            txtFilter2.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
        }

        private void txtFilter1_Enter(object sender, EventArgs e)
        {
            txtFilter1.ForeColor = Color.FromKnownColor(KnownColor.Black);
            if (txtFilter1.Text == txtFilter1.Tag.ToString())
            {
                txtFilter1.Text = "";
            }
        }

        private void txtFilter2_Enter(object sender, EventArgs e)
        {
            txtFilter2.ForeColor = Color.FromKnownColor(KnownColor.Black);
            if (txtFilter2.Text == txtFilter2.Tag.ToString())
            {
                txtFilter2.Text = "";
            }
        }

        private void txtFilter1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtFilter1.Text = "";
                filter1.SelectedItem = filter1.Items[0];
            }
        }

        private void txtFilter2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtFilter2.Text = "";
                filter2.SelectedItem = filter2.Items[0];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtFilter1.Text != txtFilter1.Tag.ToString() && filter1.Text == "(фильтр 1)")
            {
                MsgBox("Задайте корректные значения фильтров!");
                return;
            }
            if (txtFilter2.Text != txtFilter2.Tag.ToString() && filter2.Text == "(фильтр 2)")
            {
                MsgBox("Задайте корректные значения фильтров!");
                return;
            }
            if (FormIsUpdating) return;
            fill_dgv();
            FillFilling();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string q;
            q = " delete from M15 " +
                " where M15Id in ( " + M15Id.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            M15Id.Text = "";
            return;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (!MyExcelParseVPDM(out string[,] data, pb))
            {
                MsgBox("Данные не загружены");
                return;
            }
            else
            {
                MsgBox("Данные успешно загружены");
                return;
            }
        }
    }
}
