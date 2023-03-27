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
using SmuOk.Common;
using Microsoft.Win32;

namespace SmuOk.Component
{
  public partial class InvCfm : UserControl
  {
    public InvCfm()
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
    

    private void InvCfm_Load(object sender, EventArgs e)
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
            else if (Convert.ToInt32(dgvSpec.Rows[e.RowIndex].Cells["dgv_SState"].Value) == 2)
            {
                dgvSpec.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }
        }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("InvCfm"); //здесь набиваем структуру ответа
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
                q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO,vws.SState " +
                "from vwSpec vws ";

                if (lstSpecHasFillingFilter.Text == "есть записи")
                {
                    q += " left join SpecFill sf on sf.SFSpecVer = vws.SVId " +
                          " left join SpecFillExec sfe on sfe.SFEFill = sf.SFId ";
                          //" left join SpecFillExecOrder sfeo on sfeo.SFEOSpecFillExec = sfe.SFEId " +
                          //" left join SupplyOrder so on SOOrderId = sfeo.SFEOId" +
                          //" left join InvCfm ic on ic.SOId = so.SOId " + 
                          //" left join InvDoc id on id.Invid = ic.InvDocId ";
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
                    q += /*" and sfeo.sfeoid is not null and IC.ICId is not null*/" and isnull(sf.SFQtyBuy,0)>0 ";
                }


                    if (lstSpecUserFilter.GetLstVal() > 0) q += "and vws.SUser=" + lstSpecUserFilter.GetLstVal();
                else if (lstSpecUserFilter.GetLstVal() == -1) q += "and vws.SUser=0";

                managerAO = lstSpecManagerAO.GetLstVal();
                if (managerAO > 0) q += " vws.and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());
            }
            else
            {

                          q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO,vws.SState " +
                          " from vwSpec vws " +
                          " left join SpecFill sf on sf.SFSpecVer = vws.SVId " +
                          " left join SpecFillExec sfe on sfe.SFEFill = sf.SFId " +
                          /*" left join SpecFillExecOrder sfeo on sfeo.SFEOSpecFillExec = sfe.SFEId " +
                          " left join SupplyOrder so on SOOrderId = sfeo.SFEOId" +*/
                          " left join InvCfm ic on ic.SOId = so.SOId " +
                          " left join InvDoc id on id.Invid = ic.InvDocId ";

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
                    q += /*" and sfeo.sfeoid is not null and IC.ICId is not null*/" and isnull(sf.SFQtyBuy,0)>0 ";
                }

                if (lstSpecUserFilter.GetLstVal() > 0) q += "and SUser=" + lstSpecUserFilter.GetLstVal();
                else if (lstSpecUserFilter.GetLstVal() == -1) q += "and SUser=0";

                managerAO = lstSpecManagerAO.GetLstVal();
                if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

                filterText1 = txtFilter1.Text;
                if (filterText1 != "" && filterText1 != txtFilter1.Tag.ToString())
                {
                    /*if (filter1.Text == "Ответственный ОС")
                    {
                        q += " and so.SOResponsOS = '" + filterText1 + "' ";
                    }
                    if (filter1.Text == "№ планирования 1С / письма в ТСК")
                    {
                        q += " and so.SOPlan1CNum = '" + filterText1 + "' ";
                    }*/
                    if (filter1.Text == "Номер заявки 1С")////////////////////////////
                    {
                        q += " and IC1SOrderNo = '" + filterText1 + "' ";
                    }
                    if (filter1.Text == "Номер счета")
                    {
                        q += " and id.InvId is not NULL and id.InvNum = '" + filterText1 + "' ";
                    }
                    if (filter1.Text == "ИНН")
                    {
                        q += " and ICINN = " + filterText1 + " ";
                    }
                    if (filter1.Text == "Наименование")
                    {
                        q += " and sf.SFName like '%" + filterText1 + "%' ";
                    }
                    if (filter1.Text == "Наименование по счету")
                    {
                        q += " and ICName like '%" + filterText1 + "%' ";
                    }
                }
                filterText2 = txtFilter2.Text;
                if (filterText2 != "" && filterText2 != txtFilter2.Tag.ToString())
                {
                    /*if (filter2.Text == "Ответственный ОС")
                    {
                        q += " and so.SOResponsOS = '" + filterText2 + "' ";
                    }
                    if (filter2.Text == "№ планирования 1С / письма в ТСК")
                    {
                        q += " and so.SOPlan1CNum = '" + filterText2 + "' ";
                    }*/
                    if (filter2.Text == "Номер заявки 1С")////////////////////////////
                    {
                        q += " and IC1SOrderNo = '" + filterText2 + "' ";
                    }
                    if (filter2.Text == "Номер счета")
                    {
                        q += "  and id.InvId is not NULL and id.InvNum = '" + filterText2 + "' ";
                    }
                    if (filter2.Text == "ИНН")
                    {
                        q += " and ICINN = " + filterText2 + " ";
                    }
                    if (filter2.Text == "Наименование")
                    {
                        q += " and sf.SFName like '%" + filterText2 + "%' ";
                    }
                    if (filter2.Text == "Наименование по счету")
                    {
                        q += " and ICName like '%" + filterText2 + "%' ";
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
            
           string q = " select  ICId, SF.SFId/*, sfeo.SFEOId*/, SF.SFSubcode, SF.SFType, SF.SFNo, SF.SFNo2, SF.SFName, SF.SFMark " +
           " , SF.SFUnit, SFEQty, e.ename as SExecutor /*, so.SOResponsOS as SFResponsOS, SFEONum as SFOrderNum, so.SOOrderDate as SFOrderDate*/ " +
           " , cnt.AmountOrdered as TotalOrdered, /*SFEOStartDate, SFEOQty, so.SOPlan1CNum as SFPlan1CNum, so.SO1CPlanDate,*/ SFSupplyDate1C, InvLegalName " +
           " , ic.InvDocId, InvType, SFDaysUntilSupply,/* so.SOComment as SFComment,*/ IC1SOrderNo,convert(bigint, InvINN) as INN,InvNum,InvDate,ICRowNo " +
           " , ICName, ICUnit, ICQty, ICPrc, ICK " +
           " from SpecFill sf" +
           //" inner join SupplyOrder so on sf.SFId = SOFill " +
           " left join InvCfm ic on ic.ICFill = sf.SFId " +
           " left join vwSpecFill vw on sf.SFId = vw.SFId" +
           " left join Spec s on s.SId = vw.SId" +
           " left join SpecFillExec sfe on sf.SFId=SFEFill" +//
           " left join Executor e on e.eid = sfe.sfeexec" +
           //" left join SpecFillExecOrder sfeo on so.SOOrderId = sfeo.SFEOId" +
           " left join InvDoc id on id.InvId = ic.InvDocId" +
           " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt" +//
           " where isnull(SFQtyBuy,0)>0 and /*sfeo.SFEOId is not null and ic.ICId is not null and*/ sf.SFSpecVer = " + SpecVer.ToString();
            string filterText1 = txtFilter1.Text;
            if (filterText1 != "" && filterText1 != txtFilter1.Tag.ToString())
            {
                /*if (filter1.Text == "Ответственный ОС")
                {
                    q += " and so.SOResponsOS = '" + filterText1 + "' ";
                }
                if (filter1.Text == "№ планирования 1С / письма в ТСК")
                {
                    q += " and so.SOPlan1CNum = '" + filterText1 + "' ";
                }*/
                if (filter1.Text == "Номер заявки 1С")////////////////////////////
                {
                    q += " and IC1SOrderNo = '" + filterText1 + "' ";
                }
                if (filter1.Text == "Номер счета")
                {
                    q += " and id.InvNum = '" + filterText1 + "' ";
                }
                if (filter1.Text == "ИНН")
                {
                    q += " and ICINN = " + filterText1 + " ";
                }
                if (filter1.Text == "Наименование")
                {
                    q += " and sf.SFName like '%" + filterText1 + "%' ";
                }
                if (filter1.Text == "Наименование по счету")
                {
                    q += " and ICName like '%" + filterText1 + "%' ";
                }
            }
            string filterText2 = txtFilter2.Text;
            if (filterText2 != "" && filterText2 != txtFilter2.Tag.ToString())
            {
                /*if (filter2.Text == "Ответственный ОС")
                {
                    q += " and so.SOResponsOS = '" + filterText2 + "' ";
                }
                if (filter2.Text == "№ планирования 1С / письма в ТСК")
                {
                    q += " and so.SOPlan1CNum = '" + filterText2 + "' ";
                }*/
                if (filter2.Text == "Номер заявки 1С")////////////////////////////
                {
                    q += " and IC1SOrderNo = '" + filterText2 + "' ";
                }
                if (filter2.Text == "Номер счета")
                {
                    q += " and id.InvNum = '" + filterText2 + "' ";
                }
                if (filter2.Text == "ИНН")
                {
                    q += " and ICINN = " + filterText2 + " ";
                }
                if (filter2.Text == "Наименование")
                {
                    q += " and sf.SFName like '%" + filterText2 + "%' ";
                }
                if (filter2.Text == "Наименование по счету")
                {
                    q += " and ICName like '%" + filterText2 + "%' ";
                }
            }

            q += " order by " +
              "CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END, case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) +SF.SFNo else SF.SFNo end, " +
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
      q = q.Replace("ICDate", "convert(nvarchar,ICDate,104)ICDate");
      q = q.Replace("SFEOStartDate", "convert(nvarchar,SFEOStartDate,104)SFEOStartDate");
            q += " \n from" +
           " SpecFill sf" +
           //" left join SupplyOrder so on sf.SFId = SOFill" +
           " left join InvCfm ic on ic.ICFill = sf.SFId " +
           " left join vwSpecFill vw on sf.SFId = vw.SFId" +
           " left join Spec s on s.SId = vw.SId" +
           " left join SpecFillExec sfe on sf.SFId=SFEFill" +//
           " left join Executor e on e.eid = sfe.sfeexec" +
           //" left join SpecFillExecOrder sfeo on so.SOOrderId = sfeo.SFEOId" +
           " left join InvDoc id on id.InvId = ic.InvDocId" +
           " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt" +//
           " where isnull(SFQtyBuy,0)>0 ";// and sfeo.SFEOId is not null and ic.ICId is not null ";// and sf.SFSpecVer in (";
            
            if (txtSpecNameFilter.Text.ToString() == "" || txtSpecNameFilter.Text.ToString() == txtSpecNameFilter.Tag.ToString())
            {
                if(checkBox1.Checked)
                {
                    q += "";
                }
                else
                {
                    q += " and sf.SFSpecVer in (";
                    q += SpecVer.ToString();
                    q += ") ";
                    MyLog(uid, "InvCfm", 1082, SpecVer, EntityId);
                }
            }
            else
            {
                q += "and sf.SFSpecVer in (";
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
                    MyLog(uid, "InvCfm", 1082, long.Parse(sv), EntityId);
                }
                q = q.TrimEnd(',');
                q += ") ";
            }
            string filterText1 = txtFilter1.Text;
            if (filterText1 != "" && filterText1 != txtFilter1.Tag.ToString())
            {
                /*if (filter1.Text == "Ответственный ОС")
                {
                    q += " and so.SOResponsOS = '" + filterText1 + "' ";
                }
                if (filter1.Text == "№ планирования 1С / письма в ТСК")
                {
                    q += " and so.SOPlan1CNum = '" + filterText1 + "' ";
                }*/
                if (filter1.Text == "Номер заявки 1С")////////////////////////////
                {
                    q += " and IC1SOrderNo = '" + filterText1 + "' ";
                }
                if (filter1.Text == "Номер счета")
                {
                    q += " and id.InvNum = '" + filterText1 + "' ";
                }
                if (filter1.Text == "ИНН")
                {
                    q += " and ICINN = " + filterText1 + " ";
                }
                if (filter1.Text == "Наименование")
                {
                    q += " and sf.SFName like '%" + filterText1 + "%' ";
                }
                if (filter1.Text == "Наименование по счету")
                {
                    q += " and ICName like '%" + filterText1 + "%' ";
                }
            }
            string filterText2 = txtFilter2.Text;
            if (filterText2 != "" && filterText2 != txtFilter2.Tag.ToString())
            {
                /*if (filter2.Text == "Ответственный ОС")
                {
                    q += " and so.SOResponsOS = '" + filterText2 + "' ";
                }
                if (filter2.Text == "№ планирования 1С / письма в ТСК")
                {
                    q += " and so.SOPlan1CNum = '" + filterText2 + "' ";
                }*/
                if (filter2.Text == "Номер заявки 1С")////////////////////////////
                {
                    q += " and IC1SOrderNo = '" + filterText2 + "' ";
                }
                if (filter2.Text == "Номер счета")
                {
                    q += " and id.InvNum = '" + filterText2 + "' ";
                }
                if (filter2.Text == "ИНН")
                {
                    q += " and ICINN = " + filterText2 + " ";
                }
                if (filter2.Text == "Наименование")
                {
                    q += " and sf.SFName like '%" + filterText2 + "%' ";
                }
                if (filter2.Text == "Наименование по счету")
                {
                    q += " and ICName like '%" + filterText2 + "%' ";
                }
            }

                int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }
            if(c > 1000)
            {
                if (MsgBox("Выгрузка содержит большое количество записей! (" + c + " cтр.)\nХотите продолжить?", mbb: MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    q += " order by " +
              "CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END, case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) +SF.SFNo else SF.SFNo end, " +
                    " case IsNumeric(SF.SFNo2) when 1 then Replicate('0', 10 - Len(SF.SFNo2)) + SF.SFNo2 else SF.SFNo2 end";//, sfeo.SFEOId ";
                    MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 17, /*12,*/ 17, 5, 5, 60, 30, 11, 17, 17, 17, /*17, 17, 17,*/ 17, /*11, 17, 17, 17,*/ 17, 17, 17, 17, 30, 17, 17, 20, 17, 25, 11, 11, 17, 17/*, 11, 30 */}, new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 12, 16, 17, 18, 19, 20, /*, 36, 37 */});//поправить тут ширину колонок в екселе
                    MyLog(uid, "InvCfm", 1082, SpecVer, EntityId);
                }
                else return;
                
            }
            else
            {
                q += " order by " +
              "CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END, case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) +SF.SFNo else SF.SFNo end, " +
                    " case IsNumeric(SF.SFNo2) when 1 then Replicate('0', 10 - Len(SF.SFNo2)) + SF.SFNo2 else SF.SFNo2 end";//, sfeo.SFEOId ";
                MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 17, /*12,*/ 17, 5, 5, 60, 30, 11, 17, 17, 17, /*17, 17, 17,*/ 17, /*11, 17, 17, 17,*/ 17, 17, 17, 17, 30, 17, 17, 20, 17, 25, 11, 11, 17, 17/*, 11, 30 */}, new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 12, 16, 17, 18, 19, 20, /*, 36, 37 */});//поправить тут ширину колонок в екселе
                MyLog(uid, "InvCfm", 1082, SpecVer, EntityId);
            }
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
      if (bNoError && !FillingImportCheckInvIds(oSheet)) bNoError = false;
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

        private bool FillingImportCheckInvIds(dynamic oSheet)
    {
      object o_s;
      string s;
      string s_to_del = "";
      long z;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 15; // 15-based InvDocId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка существования счетов");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (s != "")
        {
          if (!long.TryParse(s, out z)) z = 0; //не число
          else if (z.ToString() != s || z < 0) z = 0; //не положительное целое
          else
          {
            z = Convert.ToInt64(MyGetOneValue("select count (*) c from InvDoc where InvId=" + s)); //нашлось?
          }
          if (z == 0)
          {
            e = true;
            oSheet.Cells(r, 1).Interior.Color = 13421823;
            oSheet.Cells(r, 1).Font.Color = -16776961;
            oSheet.Cells(r, c).Interior.Color = 0;
            oSheet.Cells(r, c).Font.Color = -16776961;
          }
          else if (z != 1) MsgBox("Так не должно быть! Обязательно пошлите скриншот этого окна разработчику.\n\nДля отладки: InvId: " + s);//нашлось >1, странное дело
          s_to_del += o_s + ",";
        }
        else
        {
          e = true;
        }
      }
      if (e) MsgBox("Идентификатор(ы) счетов в файле (см. столбец <W>) не найдены в базе.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private void FillingImportData(dynamic oSheet, long svid)
    {
      object s;
      string s_id, icOrderId, icId;
      DateTime dt;
      dynamic range = oSheet.UsedRange;
            //лучше вытащить все в структуру а не работать с экселем (начиная с проверки)
      int rows = range.Rows.Count;

            //upd = "update Spec set SExecutor="
            for (int r = 2; r < rows + 1; r++)
              {
                string q = "";
                MyProgressUpdate(pb, 50 + 30 * r / rows, "Формирование запросов");
                s_id = oSheet.Cells(r, 1).Value?.ToString() ?? "";
                icOrderId = oSheet.Cells(r, 3).Value?.ToString() ?? "";
                icId = oSheet.Cells(r, 12).Value?.ToString() ?? "";
                //q += "delete from InvCfm where ICOrderId = " + icOrderId; //12
                if(icId == "")
                {
                    q += "\ninsert into InvCfm (ICFill," +
                            " IC1SOrderNo, SFSupplyDate1C, InvDocId," +
                            " ICRowNo,ICName,ICUnit,ICQty,ICPrc,ICK,SFDaysUntilSupply" +
                            ") \nValues (" + s_id;
                    for (int c = 13; c <= 27; c++)
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
                    string soId = oSheet.Cells(r, 37).Value?.ToString() ?? "";
                    q += "); select SCOPE_IDENTITY();";
                    icId = MyGetOneValue(q).ToString();
                    string insq = "insert into BudgetFill (SpecFillId,ICId) values(" + s_id + "," + icId + ");"; //TODO budget
                    MyExecute(insq);
                    MyLog(uid, "InvCfm", 2004, long.Parse(icId), EntityId);
                }
                else if(icId != "") //ниже доделать в соответствии с текущими колонками
                {
                    string IC1SOrderNo, SFSupplyDate1C, InvDocId, ICRowNo, ICName, ICUnit, SFDaysUntilSupply, ICQtystr, ICPrcstr, ICKstr;
                    decimal ICQty, ICPrc, ICK;
                    IC1SOrderNo = oSheet.Cells(r, 13).Value?.ToString() ?? "";
                    SFSupplyDate1C = oSheet.Cells(r, 14).Value?.ToString() ?? "";
                    InvDocId = oSheet.Cells(r, 15).Value?.ToString() ?? "";
                    ICRowNo = oSheet.Cells(r, 21).Value?.ToString() ?? "";
                    ICName = oSheet.Cells(r, 22).Value?.ToString() ?? "";
                    ICUnit = oSheet.Cells(r, 23).Value?.ToString() ?? "";
                    ICQtystr = oSheet.Cells(r, 24).Value?.ToString() ?? "";
                    ICPrcstr = oSheet.Cells(r, 25).Value?.ToString() ?? "";
                    ICKstr = oSheet.Cells(r, 26).Value?.ToString() ?? "";
                    if (!decimal.TryParse(ICQtystr, out ICQty)) ICQty = 0;
                    if (!decimal.TryParse(ICPrcstr, out ICPrc)) ICPrc = 0;
                    if (!decimal.TryParse(ICKstr, out ICK)) ICK = 0;
                    SFDaysUntilSupply = oSheet.Cells(r, 27).Value?.ToString() ?? "";
                    q += " update InvCfm set " +
                        " IC1SOrderNo = " + MyES(IC1SOrderNo) +
                        " ,SFSupplyDate1C = " + MyES(SFSupplyDate1C) +
                        " ,InvDocId = " + MyES(InvDocId) +
                        " ,ICRowNo = " + MyES(ICRowNo) +
                        " ,ICName = " + MyES(ICName) + 
                        " ,ICUnit = " + MyES(ICUnit) +
                        " ,ICQty = " + MyES(ICQty) +
                        " ,ICPrc = " + MyES(ICPrc) +
                        " ,ICK = " + MyES(ICK) +
                        " ,SFDaysUntilSupply = " + MyES(SFDaysUntilSupply) +
                        " where ICId = " + icId;
                    MyExecute(q);
                    MyLog(uid, "InvCfm", 2005, long.Parse(icId), EntityId);
                }
                //MyExecute(q);
            }
      MyProgressUpdate(pb, 95, "Импорт данных");
      return;
    }

        private void dgvSpec_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
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

        private void button3_Click(object sender, EventArgs e)
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
            q = "update InvCfm set " +
                " InvDocId = NULL," + 
                " IC1SOrderNo = NULL," + 
                " SFSupplyDate1C = NULL," + 
                " ICRowNo = NULL," + 
                " ICName = NULL," + 
                " ICUnit = NULL," + 
                " ICPrc = NULL," + 
                " ICQty = NULL," +
                " ICK = NULL," +
                " SFDaysUntilSupply = NULL" + 
                " where ICId in ( " + BudgId.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            BudgId.Text = "";
            return;
        }
    }
}
