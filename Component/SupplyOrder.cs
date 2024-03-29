﻿using System;
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
using SmuOk.Common;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;
//Excel = Microsoft.Office.Interop.Excel;

namespace SmuOk.Component
{
  public partial class SupplyOrder : UserControl
  {
    public SupplyOrder()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    //private string FormControlPref = "InvCfm";
    //private string FormSqlPref = "IC";
    private long EntityId = -1;
    private long SpecVer = -1;
    int shit = 0;
    bool flag = false;
    private List<MyXlsField> FillingReportStructure;

    private void SupplyOrder_Load(object sender, EventArgs e)
    {
      LoadMe();
      fill_dgv(); //контент приходит отсюда
      fill_dgv_stock();
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
      FillingReportStructure = FillReportData("SupplyOrder"); //здесь набиваем структуру ответа
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
            string filterText1 = txtFilter1.Text;
            string filterText2 = txtFilter2.Text;
            string q, sName;
            long f, managerAO;
            if ((filterText1 == "" || filterText1 == txtFilter1.Tag.ToString()) && (filterText2 == "" || filterText2 == txtFilter2.Tag.ToString()))
            {
                q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO, SState " +
                "from vwSpec vws ";

                if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += " left join vwSpecFill vwsf on vws.SId = vwsf.SId " +
                         " left join SpecFillExec sfe on sfe.SFEFill = vwsf.SFId ";
                         /*" left join SpecFillExecOrder sfeo on sfeo.SFEOSpecFillExec = sfe.SFEId " +
                         " left join SupplyOrder SO on so.SOFill = sfe.SFEFill "; //remove (or change for another table for join)*/
                }

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

                q += " where vws.pto_block=1 and vws.SType != 6 ";

                f = lstSpecTypeFilter.GetLstVal();
                if (f > 0) q += " and vws.STId=" + f;

                if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
                else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";
                else if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += "";// and sfeo.sfeoid is not null and SOId is not null ";  //remove soid and sfeoid
                }

                if (lstSpecUserFilter.GetLstVal() > 0) q += "and vws.SUser=" + lstSpecUserFilter.GetLstVal();
                else if (lstSpecUserFilter.GetLstVal() == -1) q += "and vws.SUser=0";

                managerAO = lstSpecManagerAO.GetLstVal();
                if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());
            }
            else
            {


                q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO,SState " +
                          "from vwSpec vws inner join vwSpecFill vwsf on vwsf.SId = vws.SId inner join SupplyOrder so on so.SOFill = vwsf.SFId"; //remove

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

                q += " where pto_block=1 ";

                f = lstSpecTypeFilter.GetLstVal();
                if (f > 0) q += " and STId=" + f;

                if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
                else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";
                else if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += "";// and sfeo.sfeoid is not null and SOId is not null "; //remove soid and sfeoid
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

        private void fill_dgv_stock()
        {
            string name = NameBox.Text;
            string q, sName;
            long f, managerAO;
            if (name == "" || name == txtFilter1.Tag.ToString())
            {
                q = "select distinct Num, Code, Name, Unit, " +
                    "	case when Amount - q.AmountFromStock is NULL then Amount  " +
                    "	else Amount - q.AmountFromStock  " +
                    "	end as Amount, Price, Surname " +
                    "from wh_stock ws " +
                    "outer apply (select so.StockCode, sum(so.AmountFromStock) AmountFromStock  " +
                    "			from SupplyOrder so  " +
                    "			where so.StockCode = ws.Code  " +
                    "			group by so.StockCode)q " +
                    "where Amount - q.AmountFromStock > 0 or Amount - q.AmountFromStock is NULL ";
            }
            else
            {
                q = "select distinct Num, Code, Name, Unit, " +
                    "	case when Amount - q.AmountFromStock is NULL then Amount  " +
                    "	else Amount - q.AmountFromStock  " +
                    "	end as Amount, Price, Surname " +
                    "from wh_stock ws " +
                    "outer apply (select so.StockCode, sum(so.AmountFromStock) AmountFromStock  " +
                    "			from SupplyOrder so  " +
                    "			where so.StockCode = ws.Code  " +
                    "			group by so.StockCode)q " +
                    "where Amount - q.AmountFromStock > 0 or Amount - q.AmountFromStock is NULL and (Name like '%" + NameBox.Text + "%' or ws.Code like '%" + NameBox.Text + "%')";
            }

            MyFillDgv(dgv_stock, q);
            if (dgv_stock.Rows.Count == 0) NewEntity();
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
            string q = "select SOID," +
           " SF.SFId, SF.SFSubcode, SF.SFType, SF.SFNo, SF.SFNo2, SF.SFName, SF.SFMark, SF.SFUnit, SFEQty as QtyBuy," +
           " e.ename as SExecutor, SF.SFSupplyPID AS PID," +
           " CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END SOSupplierType," +
           " SOOrderDocId, " +
           " SOResponsOS, SORealNum, StockCode, AmountFromStock, SOOrderDate,cnt.AmountOrdered as TotalOrdered, SOPlan1CNum, SO1CPlanDate, SOComment" +
           " from" +
           " SpecFill sf" +
           " left join SupplyOrder so on sf.SFId = SOFill" +
           " left join vwSpecFill vw on sf.SFId = vw.SFId" +
           " left join Spec s on s.SId = vw.SId" +
           " left join SpecFillExec sfe on sf.SFId=SFEFill" +//
           " left join Executor e on e.eid = sfe.sfeexec" +
           //" left join SpecFillExecOrder sfeo on so.SOOrderId = sfeo.SFEOId" + //remove sfeo from here ang query
           " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt" +//
           " where sf.SFSpecVer = " + SpecVer.ToString() +
           " and s.SType != 6 ";// and sfeo.SFEOId is not null and so.soid is not null "; //remove sfeoid is not null and soid is not null
            
                string filterText1 = txtFilter1.Text;
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
                string filterText2 = txtFilter2.Text;
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

            q += " order by " +
              "CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END, case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) + SF.SFNo else SF.SFNo end, " +
                    " case IsNumeric(SF.SFNo2) when 1 then Replicate('0', 10 - Len(SF.SFNo2)) + SF.SFNo2 else SF.SFNo2 end ";//, sfeo.SFEOId ";

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

    public void SpecList_CheckedChanged(object sender, EventArgs e)
    {
      DB.SpecList_CheckedChanged(sender, FormIsUpdating);
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

    private void dgvSpecFill_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      if (FormIsUpdating) return;
      dgvSpecFill.MySaveColWidthForUser(uid, e);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      string q = "select ";
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        q += f.SqlName + ",";
        tt.Add(f.Title);
      }
      q = q.Substring(0, q.Length - 1);
      q = q.Replace("SO1CPlanDate", "convert(nvarchar,SO1CPlanDate,104)SO1CPlanDate");
      q += " \n " +
        " from" +
        " SpecFill sf" +
        " left join SupplyOrder so on sf.SFId = SOFill" +
        " left join vwSpecFill vw on sf.SFId = vw.SFId" +
        " left join Spec s on s.SId = vw.SId" +
        " left join SpecFillExec sfe on sf.SFId=SFEFill" +//
        " left join Executor e on e.eid = sfe.sfeexec" +
        //" left join SpecFillExecOrder sfeo on so.SOOrderId = sfeo.SFEOId" + //remove
        " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt" +//
        " where s.SType != 6 and sf.SFSpecVer in (";
            if (txtSpecNameFilter.Text.ToString() == "" || txtSpecNameFilter.Text.ToString() == txtSpecNameFilter.Tag.ToString())
            {
                q += SpecVer.ToString();
                MyLog(uid, "SupplyOrder", 1081, SpecVer, EntityId);
            }
            else
            {
                string selq = "select SVId from vwSpec where SId in (";
                List<string> specver = txtSpecNameFilter.Text.ToString().Split(',').ToList<string>();
                foreach (string sv in specver)
                {
                    selq += sv + ",";
                }
                selq = selq.TrimEnd(',');
                selq += ")";
                specver = MyGetOneCol(selq);
                foreach (string sv in specver)
                {
                    q += sv + ",";
                    MyLog(uid, "SupplyOrder", 1081, long.Parse(sv), EntityId);
                }
                q = q.TrimEnd(',');
            }

            q += ") ";
            string filterText1 = txtFilter1.Text;
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
            string filterText2 = txtFilter2.Text;
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

            int c = (int)MyGetOneValue("select count(*) from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

            q += " order by " +
              "CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END, case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) +SF.SFNo else SF.SFNo end, " +
                    " case IsNumeric(SF.SFNo2) when 1 then Replicate('0', 10 - Len(SF.SFNo2)) + SF.SFNo2 else SF.SFNo2 end ";//, sfeo.SFEOId ";
            MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 17, /*15,*/ 17, 17, 5, 5, 60, 30, 11, 17, 17, 17, 17, 17, 17, 17, 17, /*17,*/ 17, 17 ,17, /*17, 17,*/ 17, 30, 17, 17, 20}, new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 20, 21, 25, 26});//поправить тут ширину колонок в екселе
      MyLog(uid, "SupplyOrder", 1081, SpecVer, EntityId);
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
            if (dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral || dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
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

      if (bNoError && !MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb)) bNoError = false;   //FillingImportCheckTitle(oSheet)) bNoError = false;
      if (bNoError) MyExcelUnmerge(oSheet);
      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb); //проверка значений в столбцах
      //if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName)) bNoError = false;
      //if (bNoError && !FillingImportCheckSVIds(oSheet, svid)) bNoError = false;
      if (bNoError && !FillingImportCheckOrderDocIds(oSheet)) bNoError = false;
      if (bNoError && !FillingImportCheckStockAmount(oSheet)) bNoError = false;
      /*if (bNoError && !FillingImportCheckSums(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckSumElements(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckExecs(oSheet, SpecVer)) bNoError = false;
      if (bNoError && !FillingImportCheckExecsUniq(oSheet, SpecVer)) bNoError = false;*/

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

        /* private bool FillingImportCheckSpecName(dynamic oSheet, string SpecCode)
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
     }*/

        private bool FillingImportCheckStockAmount(dynamic oSheet)
        {
            string StockCode, Amount_str, sel_q;
            bool e = false;
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;
            int numc = 18;
            int amountc = 19;
            int z = 1;
            decimal amount, res_amount;
            if (rows == 1) return true;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка наличия объемов на складе");
                StockCode = oSheet.Cells(r, numc).Value?.ToString() ?? "";
                if (StockCode != "")
                {
                    Amount_str = oSheet.Cells(r, amountc).Value?.ToString() ?? "0";
                    if (!decimal.TryParse(Amount_str, out amount))
                    {
                        e = true;
                        z = 0;
                    }

                    sel_q = "select " +
                    "	case when Amount - q.AmountFromStock is NULL then Amount  " +
                    "	else Amount - q.AmountFromStock  " +
                    "	end as Amount " +
                    "from wh_stock ws " +
                    "outer apply (select so.StockCode, sum(so.AmountFromStock) AmountFromStock  " +
                    "			from SupplyOrder so  " +
                    "			where so.StockCode = ws.Code  " +
                    "			group by so.StockCode)q " +
                    "where (Amount - q.AmountFromStock > 0 or Amount - q.AmountFromStock is NULL) and ws.Code = '" + StockCode + "';";

                    try
                    {
                        if (!decimal.TryParse(MyGetOneValue(sel_q).ToString() ?? "", out res_amount))
                        {
                            e = true;
                            z = 0;
                        }
                        else if (res_amount - amount < 0)
                        {
                            e = true;
                            z = 0;
                        }
                    }

                    catch
                    {
                        MsgBox("Введен несуществующий складской номер. \n\n Код: " + StockCode);
                        e = true;
                        oSheet.Cells(r, 1).Interior.Color = 13421823;
                        oSheet.Cells(r, 1).Font.Color = -16776961;
                        oSheet.Cells(r, numc).Interior.Color = 0;
                        oSheet.Cells(r, numc).Font.Color = -16776961;
                        continue;
                    }


                    if (z == 0)
                    {
                        e = true;
                        oSheet.Cells(r, 1).Interior.Color = 13421823;
                        oSheet.Cells(r, 1).Font.Color = -16776961;
                        oSheet.Cells(r, numc).Interior.Color = 0;
                        oSheet.Cells(r, numc).Font.Color = -16776961;
                    }
                    else if (z != 1) MsgBox("Так не должно быть! Обязательно пошлите скриншот этого окна разработчику.\n\nДля отладки: Код: " + StockCode);//нашлось >1, странное дело
                }
                else
                {
                    continue;
                }
            }
            if (e) MsgBox("Некорректное распределение товаров со стока", "Ошибка", MessageBoxIcon.Warning);
            return !e;
        }

        private bool FillingImportCheckOrderDocIds(dynamic oSheet)
        {
            object o_s;
            string s;
            bool e = false;
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;
            int c = 12; // 11-based SOOrderDocId
            if (rows == 1) return true;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка существования заявок");
                o_s = oSheet.Cells(r, c).Value;
                s = o_s == null ? "" : o_s.ToString();
                if (s != "")
                {
                    if (!long.TryParse(s, out long z)) z = 0; //не число
                    else if (z.ToString() != s || z < 0) z = 0; //не положительное целое
                    else
                    {
                        z = Convert.ToInt64(MyGetOneValue("select count (*) c from OrderDoc where OrderId=" + s.ToString())); //нашлось?
                    }
                    if (z == 0)
                    {
                        e = true;
                        oSheet.Cells(r, 1).Interior.Color = 13421823;
                        oSheet.Cells(r, 1).Font.Color = -16776961;
                        oSheet.Cells(r, c).Interior.Color = 0;
                        oSheet.Cells(r, c).Font.Color = -16776961;
                    }
                    else if (z != 1) MsgBox("Так не должно быть! Обязательно пошлите скриншот этого окна разработчику.\n\nДля отладки: OrderDocId: " + s);//нашлось >1, странное дело
                }
                else
                {
                    e = true;
                }
            }
            if (e) MsgBox("Заявки с указанным(и) идентификатором(ами) не найдены в реестре заявок", "Ошибка", MessageBoxIcon.Warning);
            return !e;
        }

        /*private bool FillingImportCheckSVIds(dynamic oSheet, long svid)
    {
      object o_s;
      string s;
      string s_to_del = "";
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка принадлежности строк шифру");
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
          s_to_del += o_s + ",";
        }
        else
        {
          e = true;
        }
      }
      if (e) MsgBox("Идентификатор(ы) строк в файле (см. столбец <A>) не найдены в базе для этого шифра.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }*/

    private void FillingImportData(dynamic oSheet, long svid) //импорт необходимо переработать, удалять по паре з на пост и заявка
    {
      //string q = "";
      object s;
      string s_id, soId;
      DateTime dt;
      dynamic range = oSheet.UsedRange;
            //лучше вытащить все в структуру а не работать с экселем (начиная с проверки)
      int rows = range.Rows.Count;
            bool need_warehouse_ins = true;
                
            for (int r = 2; r < rows + 1; r++)
            {
                string q = "";
                string SOOrderDocId, SOResponsOS, SORealNum, StockCode, AmountFromStock, SOPlan1CNum, SO1CPlanDate, SOComment;
                decimal AFStock;
                MyProgressUpdate(pb, 50 + 30 * r / rows, "Формирование запросов");
                
            s_id = oSheet.Cells(r, 1).Value?.ToString() ?? "";
                soId = oSheet.Cells(r, 13).Value?.ToString() ?? "";
                StockCode = oSheet.Cells(r, 18).Value?.ToString() ?? "";
                AmountFromStock = oSheet.Cells(r, 19).Value?.ToString() ?? "0";
                AFStock = Decimal.Parse(AmountFromStock);
                if (soId == "")
                {
                    q += "\ninsert into SupplyOrder (SOFill, SOOrderDocId, SOSupplierType, SOResponsOS, SORealNum, StockCode, AmountFromStock, SOOrderDate, " +
                        "SOPlan1CNum, SO1CPlanDate, SOComment, SOOrderNumPref" +
                        ") \nValues (" + s_id;
                    for (int c = 12; c <= 25; c++) 
                    {
                        if (FillingReportStructure[c - 1].DataType == "fake")
                        {
                            continue;
                        }
                        if (FillingReportStructure[c - 1].DataType == "InvCfmType")
                        {
                            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
                        }
                        else if (FillingReportStructure[c - 1].DataType == "date")
                        {
                            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
                            if (s != "")
                            {
                                dt = DateTime.Parse(oSheet.Cells(r, c).Value.ToString());
                                s = dt.ToString();
                            }
                            s = s.ToString();
                        }
                        else
                        {
                            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
                            if (FillingReportStructure[c - 1].DataType == "decimal") s = s.ToString().Replace(",", ".");
                        }
                        q += "," + MyES(s, false, FillingReportStructure[c - 1].Nulable);
                    }
                    q += "); select SCOPE_IDENTITY();";
                    soId = MyGetOneValue(q).ToString();
                    if(AFStock > 0) MyLog(uid, "SupplyOrder", 2023, long.Parse(soId), EntityId, StockCode, AFStock.ToString());
                    /*string insq = "insert into InvCfm(SOId,ICOrderId,ICFill) values(" + soId + ","+ soOrderId + "," + s_id + "); select SCOPE_IDENTITY()";
                    string newICId = MyGetOneValue(insq).ToString();
                    string insq2 = "insert into BudgetFill(ICId, SpecFillId) values(" + newICId + "," + s_id + ")"; РАЗОБРАТЬСЯ КАК ВЛИЯЕТ
                    MyExecute(insq2);*/
                    MyLog(uid, "SupplyOrder", 2002, long.Parse(soId), EntityId);
                }
                else if (soId != "")
                {
                    SOOrderDocId = oSheet.Cells(r, 12).Value?.ToString() ?? "";
                    SOResponsOS = oSheet.Cells(r, 16).Value?.ToString() ?? "";
                    SORealNum = oSheet.Cells(r, 17).Value?.ToString() ?? "";
                    StockCode = oSheet.Cells(r, 18).Value?.ToString() ?? "";
                    AmountFromStock = oSheet.Cells(r, 19).Value?.ToString() ?? "0";
                    AFStock = Decimal.Parse(AmountFromStock);
                    SOPlan1CNum = oSheet.Cells(r, 22).Value?.ToString() ?? "";
                    SO1CPlanDate = oSheet.Cells(r, 23).Value?.ToString() ?? "";
                    SOComment = oSheet.Cells(r, 24).Value?.ToString() ?? "";
                    q = "update SupplyOrder set " +
                        " SOOrderDocId = " + SOOrderDocId +
                        " ,SOResponsOS = " + MyES(SOResponsOS) +
                        " ,SORealNum = " + MyES(SORealNum) +
                        " ,StockCode = " + MyES(StockCode) +
                        " ,AmountFromStock = " + MyES(AFStock) +
                        " ,SOPlan1CNum = " + MyES(SOPlan1CNum) +
                        " ,SO1CPlanDate = " + MyES(SO1CPlanDate) +
                        " ,SOComment = " + MyES(SOComment) + 
                        " where SOId = " + soId;
                    MyExecute(q);
                    MyLog(uid, "SupplyOrder", 2003, long.Parse(soId), EntityId);
                    if (AFStock > 0) MyLog(uid, "SupplyOrder", 2023, long.Parse(soId), EntityId, StockCode, AFStock.ToString());
                }

                if(StockCode != "" && AFStock != 0 && need_warehouse_ins)
                {
                    string ins_q = "insert into SpecWarehouse (SpecId) values (" + EntityId.ToString() + ")";
                    MyExecute(ins_q);
                    need_warehouse_ins = false;
                }
            }
      MyProgressUpdate(pb, 95, "Импорт данных");
      //MyExecute(q);
      return;
    }

        private void dgvSpec_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Export1SBtn_Click(object sender, EventArgs e)
        {
            // Create a new ExcelPackage
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo("test")))
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "VDWWD";
                excelPackage.Workbook.Properties.Title = "Title of Document";
                excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
                excelPackage.Workbook.Properties.Created = DateTime.Now;
                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                //Add some text to cell A1
                /*Весь лист*/
                worksheet.Column(1).Width = 1.83;
                worksheet.Column(7).Width = 13.17;
                worksheet.Column(9).Width = 34.5;
                worksheet.Column(10).Width = 13.17;
                worksheet.Column(11).Width = 12.17;
                worksheet.Column(12).Width = 16.17;
                worksheet.Column(13).Width = 16.17;
                worksheet.Column(14).Width = 14.83;
                worksheet.Column(15).Width = 19.67;
                worksheet.Column(16).Width = 17.33;
                worksheet.Row(1).Height = 8.25;
                /*Весь лист*/
                /*Шапка*/
                worksheet.Cells["A1:Z99"].Style.Font.Name = "Arial"; //везде шрифт ариал
                worksheet.Cells["B2"].Style.Font.Size = 11;
                worksheet.Cells["B2"].Style.Font.Bold = true;
                worksheet.Cells["B3:B20"].Style.Font.Size = 10;
                worksheet.Cells["B12:P12"].Style.Font.Size = 12;
                worksheet.Cells["B12:P12"].Style.Font.Bold = true;
                worksheet.Cells["B2:P2"].Merge = true;
                worksheet.Cells["B3:F3"].Merge = true;
                worksheet.Cells["B4:F4"].Merge = true;
                worksheet.Cells["B6:P6"].Merge = true;
                worksheet.Cells["B7:P7"].Merge = true;
                worksheet.Cells["B8:P8"].Merge = true;
                worksheet.Cells["B9:P9"].Merge = true;
                worksheet.Cells["B10:P10"].Merge = true;
                worksheet.Cells["B12:P12"].Merge = true;
                worksheet.Cells["B6:P10"].Style.Font.Bold = true;
                worksheet.Cells["B6:P10"].Style.Font.Size = 11;
                worksheet.Cells["B6:P6"].Value = "Генеральному директору";
                worksheet.Cells["B7:P7"].Value = "АО \"ТСК\"";
                worksheet.Cells["B8:P8"].Value = "И.В. Головачевой";
                worksheet.Cells["B9:P9"].Value = "127051, г. Москва, ул. Цветной бульвар, д. 17, комната 20";
                worksheet.Cells["B10:P10"].Value = "ИНН 770201001 КПП 770701001";
                worksheet.Cells["B6:P10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells["B12:P12"].Value = "Заявка по ";
                worksheet.Cells["B2"].Value = "ООО \"СМУ-24 Метростоя\"";
                worksheet.Cells["B3"].Value = "ИНН 7707846672, КПП 770701001";
                worksheet.Cells["B4"].Value = "127051, Москва г, Цветной б-р, дом № 17, оф.24";
                worksheet.Cells["B14:C14"].Merge = true;
                worksheet.Cells["B15:C15"].Merge = true;
                worksheet.Cells["B16:C16"].Merge = true;
                worksheet.Cells["B17:C17"].Merge = true;
                worksheet.Cells["B18:C18"].Merge = true;
                worksheet.Cells["B14:C18"].Style.Font.Size = 11;
                worksheet.Cells["B14:C14"].Value = "Объект:";
                worksheet.Cells["D14:P14"].Merge = true;
                worksheet.Cells["D18:P18"].Merge = true;
                worksheet.Cells["D15:P15"].Merge = true;
                worksheet.Cells["D16:F16"].Merge = true;
                worksheet.Cells["D16:F16"].Merge = true;
                worksheet.Cells["H16:I16"].Merge = true;
                worksheet.Cells["H17:I17"].Merge = true;
                worksheet.Cells["H16:I16"].Value = "Ответственный:";
                worksheet.Cells["H17:I17"].Value = "Телефон:";
                worksheet.Cells["B15:C15"].Value = "Шифр проекта:";
                worksheet.Cells["B16:C16"].Value = "Статус:";
                worksheet.Cells["B17:C17"].Value = "Менеджер:";
                worksheet.Cells["B18:C18"].Value = "Адрес поставки:";
                worksheet.Cells["J16:P16"].Merge = true;
                worksheet.Cells["J17:P17"].Merge = true;
                /*Шапка*/
                /*Заголовки таблицы*/
                worksheet.Row(20).Height = 24.75;
                worksheet.Cells["B20:F20"].Merge = true;
                worksheet.Cells["B20:P20"].Style.Font.Bold = true;
                worksheet.Cells["B20:P20"].Style.Font.Size = 10;
                worksheet.Cells["B20:P20"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Cells["B20:P20"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["B20:F20"].Value = "Номенклатура";
                worksheet.Cells["G20"].Value = "Артикул";
                worksheet.Cells["H20"].Value = "Ед.изм.";
                worksheet.Cells["I20"].Value = "Комментарий";
                worksheet.Cells["J20"].Value = "Количество";
                worksheet.Cells["K20"].Value = "Цена";
                worksheet.Cells["L20"].Value = "Желаемая дата поставки";
                worksheet.Cells["M20"].Value = "Количество поставленное";
                worksheet.Cells["N20"].Value = "Дата поставки";
                worksheet.Cells["O20"].Value = "Менеджер";
                worksheet.Cells["P20"].Value = "Остаток";
                worksheet.Cells["B20:F20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["B20:F20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["B20:F20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["B20:F20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["G20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["G20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["G20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["G20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["H20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["H20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["H20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["H20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["I20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["I20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["I20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["I20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["J20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["J20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["J20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["J20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["K20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["K20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["K20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["K20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["L20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["L20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["L20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["L20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["M20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["M20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["M20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["M20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["N20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["N20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["N20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["N20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["O20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["O20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["O20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["O20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells["P20"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["P20"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["P20"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["P20"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Row(20).Style.WrapText = true;
                string selObj = "select SObject from vwSpec where SVId = " + SpecVer.ToString();
                string Obj = MyGetOneValue(selObj).ToString();
                string selSpec = "select SVName from vwSpec where SVId = " + SpecVer.ToString();
                string spec = MyGetOneValue(selSpec).ToString();
                worksheet.Cells["D14:P14"].Value = Obj;
                worksheet.Cells["D15:P15"].Value = spec;
                string exec = "SELECT ic.ICUnit, CONCAT(sf.SFName, ' ', sf.SFMark), SFEOQty, ' ', convert(nvarchar,SFEOStartDate,104)" +
                  " FROM SpecFill sf" +
                  " left join SpecFillExec sfe on sfe.SFEFill = sf.SFId" +
                  " left join SpecFillExecOrder sfeo on sfeo.SFEOSpecFillExec = sfe.SFEId" +
                  " left join InvCfm ic on ic.ICOrderId = sfeo.SFEOId" +
                  " where sf.SFSpecVer = " + SpecVer.ToString() +
                  " and isnull(sf.SFQtyBuy, 0) > 0" +
                  " and ic.IC1SOrderNo is NULL and not sfeo.SFEOStartDate is NULL";
                string[,] dataArrays = MyGet2DArray(exec);
                if(dataArrays is null)
                {
                    MsgBox("Нечего выгружать по " + spec);
                    return;
                }
                DataTable dataTable = new DataTable();
                for (int i = 0; i < 5; i++)
                {
                    dataTable.Columns.Add();
                }
                for (int i = 0; i < dataArrays.Length / 5; i++)
                {
                    DataRow row = dataTable.NewRow();
                    for (int j = 0; j < dataArrays.Length / (dataArrays.Length / 5); j++)
                    {
                        row[j] = dataArrays[i, j];
                    }
                    dataTable.Rows.Add(row);
                }
                string count = "select count(*)" +
                    " FROM SpecFill sf" +
                  " left join SpecFillExec sfe on sfe.SFEFill = sf.SFId" +
                  " left join SpecFillExecOrder sfeo on sfeo.SFEOSpecFillExec = sfe.SFEId" +
                  " left join InvCfm ic on ic.ICOrderId = sfeo.SFEOId" +
                  " where sf.SFSpecVer = " + SpecVer.ToString() +
                  " and isnull(sf.SFQtyBuy, 0) > 0" +
                  " and ic.IC1SOrderNo is NULL and not sfeo.SFEOStartDate is NULL";
                int cnt = (int)MyGetOneValue(count);
                int reserve = cnt;
                int currRow = 21;
                while (cnt > 0)
                {
                    worksheet.Cells["B" + currRow + ":F" + currRow].Merge = true;
                    worksheet.Cells["B" + currRow + ":F" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["B" + currRow + ":F" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["B" + currRow + ":F" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["B" + currRow + ":F" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["G" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["G" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["G" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["G" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["H" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["H" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["H" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["H" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["I" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["I" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["I" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["I" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["J" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["J" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["J" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["J" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["K" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["K" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["K" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["K" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["L" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["L" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["L" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["L" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["M" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["M" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["M" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["M" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["N" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["N" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["N" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["N" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["O" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["O" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["O" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["O" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells["P" + currRow].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["P" + currRow].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["P" + currRow].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells["P" + currRow].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    currRow++;
                    cnt--;
                }

                worksheet.Cells["B" + (currRow + 1) + ":C" + (currRow + 1)].Merge = true;
                worksheet.Cells["B" + (currRow + 1) + ":C" + (currRow + 1)].Value = "Комментарий";
                worksheet.Cells["B" + (currRow + 3)].Value = "Автор:";
                worksheet.Cells["B" + (currRow + 3)].Style.Font.Bold = true;
                //DataTable dataTable = loadExternalDataSet(exec);
                worksheet.Cells["H21"].LoadFromDataTable(dataTable, false);

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Title = "Save Excel sheet";
                    saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                    saveFileDialog1.FileName = "Заявка1С_" + spec + ".xlsx";
                    //check if user clicked the save button
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //Get the FileInfo
                        FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                        //write the file to the disk
                        excelPackage.SaveAs(fi);
                    }
                MsgBox("Ok");


            }
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
            q = "update SupplyOrder set " +
                " SOOrderDocId = NULL," +
                " SOResponsOS = NULL," +
                " SORealNum = NULL," +
                " SOPlan1CNum = NULL," +
                " SO1CPlanDate = NULL," +
                " SOComment = NULL" +
                " where SOId in ( " + BudgId.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            BudgId.Text = "";
            return;
        }

        private void SearchStock_btn_Click(object sender, EventArgs e)
        {
            fill_dgv_stock();
            return;
        }

        private void NameBox_Enter(object sender, EventArgs e)
        {
            if (NameBox.Text == NameBox.Tag.ToString())
            {
                NameBox.Text = "";
            }
            NameBox.ForeColor = Color.FromKnownColor(KnownColor.Black);
        }

        private void NameBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                NameBox.Text = "";
                fill_dgv_stock();
            }
            if (e.KeyCode == Keys.Enter)
            {
                fill_dgv_stock();
            }
        }

        private void NameBox_Leave(object sender, EventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameBox.Text = NameBox.Tag.ToString();
            }
            NameBox.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
        }
    }
}
