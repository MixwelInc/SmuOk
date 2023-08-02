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
  public partial class M15 : UserControl
  {
    public M15()
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
            else if (Convert.ToInt32(dgvSpec.Rows[e.RowIndex].Cells["dgv_SState"].Value) == 2)
            {
                dgvSpec.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }
        }
    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = FillReportData("M15"); //здесь набиваем структуру ответа
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
                q = " select distinct vws.SId,vws.STName,vws.SVName,vws.ManagerAO, SState ";

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
         string q = "select M15Id," +
        " SF.SFId,SOOrderId, SF.SFSubcode, SF.SFType, SF.SFNo, SF.SFNo2, SF.SFName, SF.SFMark, SF.SFUnit, coalesce(SF.SFQtyBuy, SF.SFQtyGnT) as QtyBuy," +
        " e.ename as SExecutor, SF.SFSupplyPID AS PID," +
        " m.PID2, AFNNum, AFNDate, ABKNum, AFNName, M15Price, AFNQty, Reciever, M15Num, M15Date, M15Name,M15Qty " +
        " from SpecFill sf" +//
        " left join SupplyOrder so on sf.SFId = so.SOFill" +
        " left join M15 m on m.FillId = sf.SFId or m.PID = sf.SFSupplyPID " +
        " left join SpecFillExecOrder sfeo on sfeo.SFEOId = so.SOOrderId" +
        " left join SpecFillExec SFE on SFE.SFEId = SFEO.SFEOSpecFillExec" +
        " left join Executor e on e.EId = sfe.SFEExec" +
        " left join vwSpecFill vw on sf.SFId = vw.SFId" +
        " left join Spec s on s.SId = vw.SId" +
        " where sf.SFSpecVer = " + SpecVer.ToString() +
        " and s.SType != 6 and isnull(SFQtyGnT,0) > 0 ";

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

            q += "\n order by CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END, sf.sfid";

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
      string q = "select distinct ";
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        q += f.SqlName + ",";
        tt.Add(f.Title);
      }
      q = q.Substring(0, q.Length - 1);
      q += " \n " +
        " from SpecFill sf" +//
        " left join SupplyOrder so on sf.SFId = so.SOFill" +
        " left join vwSpecFill vw on sf.SFid = vw.SFId" +
        " left join vwSpec vws on vws.SId = vw.SId" +
        " left join SpecFillExecOrder sfeo on sfeo.SFEOId = so.SOOrderId" +
        " left join SpecFillExec sfe on sfe.SFEFill = sf.SFId" +//
        " left join M15 m on m.FillId = sf.SFId or m.PID = sf.SFSupplyPID" +
        " left join (select SFBFill, sum(SFBQtyForTSK) BoLQtySum from SpecFillBoL group by SFBFill)d on d.SFBFill = so.SOFill" +
        " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt " +//
        " where vws.SType != 6 and isnull(SF.SFQtyGnT, 0) > 0 and sf.SFSpecVer in (";
            if (txtSpecNameFilter.Text.ToString() == "" || txtSpecNameFilter.Text.ToString() == txtSpecNameFilter.Tag.ToString())
            {
                q += SpecVer.ToString();
                MyLog(uid, "M15", 2008, SpecVer, EntityId);//2008
            }
            else if (!check_is_lst(txtSpecNameFilter.Text.ToString()))
            {
                q += SpecVer.ToString();
                MyLog(uid, "M15", 2008, SpecVer, EntityId);//2008
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
                    MyLog(uid, "M15", 2008, long.Parse(sv), EntityId);//2008
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
        " sf.sfid";
      MyExcelIns(q, tt.ToArray(), true, new decimal[] { 7, 7, 17, 15, 17, 17, 5, 5, 60, 30, 11, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17 ,17, 17, 17, 17, 17, 17, 17, 17, 17, 25, 25, 17, 17, 17, 25, 30, 17, 20}, 
          new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 40});
        }

    private void btnImport_Click(object sender, EventArgs e)
    {
            if(dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.LightCoral || dgvSpec.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
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
      //if (bNoError && !FillingImportCheckOrderDocIds(oSheet)) bNoError = false;
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

    private void FillingImportData(dynamic oSheet, long svid) //импорт необходимо переработать, удалять по паре з на пост и заявка
    {
      object s;
      string s_id, M15Id, sfeid;
      DateTime dt;
      dynamic range = oSheet.UsedRange;
            //лучше вытащить все в структуру а не работать с экселем (начиная с проверки)
      int rows = range.Rows.Count;
            bool need_warehouse_ins = true;
            for (int r = 2; r < rows + 1; r++)
            {
                string q = "";
                MyProgressUpdate(pb, 50 + 30 * r / rows, "Формирование запросов");
                string Reciever, LandingPlace;
                s_id = oSheet.Cells(r, 1).Value?.ToString() ?? "";//FillId
                sfeid = oSheet.Cells(r, 2).Value?.ToString() ?? "";
                M15Id = oSheet.Cells(r, 21).Value?.ToString() ?? "";
                Reciever = oSheet.Cells(r, 30).Value?.ToString() ?? "";
                //soOrderId = long.Parse(oSheet.Cells(r, 3).Value.ToString());
                if (M15Id == "")
                {
                    q += "\ninsert into M15 (FillId, MSpecExecFill, PID2,AFNNum, AFNDate, ABKNum, AFNName, SechCab, BarNum, AFNQty, Reciever," +
                        "StoragePlace, UnloadDate, FIO, UnloadQty, CarNum, M15Num, M15Date, M15Name, M15Qty" +
                        ") \nValues (" + s_id + "," + sfeid;
                    for (int c = 22; c <= 39; c++)
                    {
                        if (FillingReportStructure[c - 1].DataType == "fake")
                        {
                            continue;
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
                    M15Id = MyGetOneValue(q).ToString();
                    MyLog(uid, "M15", 2009, long.Parse(M15Id), EntityId);
                }
                else if (M15Id != "")
                {
                    string PID2, AFNNum,AFNDate,ABKNum,AFNName,M15Num,M15Date,M15Name,strAFNQty,strM15Qty,M15Price, SechCab, BarNum, StoragePlace, UnloadDate, FIO, UnloadQty, CarNum;
                    PID2 = oSheet.Cells(r, 22).Value?.ToString() ?? "";
                    AFNNum = oSheet.Cells(r, 23).Value?.ToString() ?? "";
                    AFNDate = oSheet.Cells(r, 24).Value?.ToString() ?? "";
                    ABKNum = oSheet.Cells(r, 25).Value?.ToString() ?? "";
                    AFNName = oSheet.Cells(r, 26).Value?.ToString() ?? "";
                    SechCab = oSheet.Cells(r, 27).Value?.ToString() ?? "";
                    BarNum = oSheet.Cells(r, 28).Value?.ToString() ?? "";
                    strAFNQty = oSheet.Cells(r, 29).Value?.ToString() ?? "";
                    StoragePlace = oSheet.Cells(r, 31).Value?.ToString() ?? "";
                    UnloadDate = oSheet.Cells(r, 32).Value?.ToString() ?? "";
                    FIO = oSheet.Cells(r, 33).Value?.ToString() ?? "";
                    UnloadQty = oSheet.Cells(r, 34).Value?.ToString() ?? "";
                    CarNum = oSheet.Cells(r, 35).Value?.ToString() ?? "";
                    M15Num = oSheet.Cells(r, 36).Value?.ToString() ?? "";
                    M15Date = oSheet.Cells(r, 37).Value?.ToString() ?? "";
                    M15Name = oSheet.Cells(r, 38).Value?.ToString() ?? "";
                    strM15Qty = oSheet.Cells(r, 39).Value?.ToString() ?? "";

                    q = "update M15 set " +
                        " PID2 = " + PID2 +
                        " ,AFNNum = " + MyES(AFNNum) +
                        " ,AFNDate = " + MyES(AFNDate) +
                        " ,ABKNum = " + MyES(ABKNum) +
                        " ,AFNName = " + MyES(AFNName) +
                        " ,Reciever = " + MyES(Reciever) +
                        " ,M15Num = " + MyES(M15Num) +
                        " ,M15Date = " + MyES(M15Date) +
                        " ,M15Name = " + MyES(M15Name) +
                        " ,AFNQty = " + strAFNQty.Replace(",", ".") +
                        " ,M15Qty = " + strM15Qty.Replace(",", ".") +
                        " ,MSpecExecFill = " + sfeid +
                        " ,SechCab = " + SechCab.Replace(",", ".") +
                        " ,BarNum = " + MyES(BarNum) +
                        " ,StoragePlace = " + MyES(StoragePlace) +
                        " ,UnloadDate = " + MyES(UnloadDate) +
                        " ,FIO = " + MyES(FIO) +
                        " ,UnloadQty = " + UnloadQty.Replace(",", ".") +
                        " ,CarNum = " + MyES(CarNum) +
                        " where M15Id = " + M15Id;
                    MyExecute(q);
                    MyLog(uid, "M15", 2010, long.Parse(M15Id), EntityId);
                }

                if(Reciever != "" && need_warehouse_ins)
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
