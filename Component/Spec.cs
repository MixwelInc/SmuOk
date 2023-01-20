using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmuOk.Common;
using static SmuOk.Common.MyReport;
using static SmuOk.Common.DB;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using Microsoft.VisualBasic;

namespace SmuOk.Component
{
  public partial class Spec : UserControl//UserControl // SmuOk_.Common.MyComponent
  {
    public Spec()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "Spec";
    private string FormSqlPref = "S";
    private List<MyXlsField> FillingReportStructure;
    private long EntityId;

    private void Spec_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();
      txtFilter1.Text = txtFilter1.Tag.ToString();
      txtFilter2.Text = txtFilter2.Tag.ToString();
      filter1.Text = "(фильтр 1)";
      filter2.Text = "(фильтр 2)";
      MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecDone, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='PTODone';", "(обработано)");
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = new List<MyXlsField>();
      //int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkPTOMultiline' and EUIOVaue=1");
      FillingReportStructure = FillReportData("Spec");
      dgvSpec.Columns["dgv_btn_folder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      FillFilter();
      fill_dgv();
      //FillFilter();
      FormIsUpdating = false;
    }

        private void dgvSpec_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.ToInt32(dgvSpec.Rows[e.RowIndex].Cells["dgv_SState"].Value) == 1)
            {
                dgvSpec.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
            }
        }

        private void fill_dgv()
    {
      /*string q = "select distinct SId, SStation, SVName,SVStage, STName, SNo, SArea, SObject, SSystem, NewestFillingCount, SVNo, cast(SVDate as date) SVDate,SDog,SBudget,SBudgetTotal, manager, curator";
      q += " from vwSpec where 1=1";*/
      string filterText1 = txtFilter1.Text;
      string filterText2 = txtFilter2.Text;
      string q = "select distinct SId,SSystem,SStation,curator,SContractNum,SVName,STName,SExecutor,SArea,SNo,SVNo,SVStage," +
                "cast(SVProjectSignDate as date)SVProjectSignDate,SVProjectBy,cast(SVDate as date)SVDate,SComment,SState" +
        ",SDog,SBudget,SBudgetTotal, case when NewestFillingCount > 0 then 'да' else 'нет' end as has_filling, case when SState = 1 then 'заблокирован' else 'активен' end as is_active " +
        " from vwSpec ";

      string sName = txtSpecNameFilter.Text;
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString()) 
      {
        q += " inner join (select SVSpec svs from SpecVer " +
              " where SVName like "+ MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              ")q on svs=SId";
      }

            q += " where 1=1 ";
            if ((filterText1 == "" || filterText1 == txtFilter1.Tag.ToString()) && (filterText2 == "" || filterText2 == txtFilter2.Tag.ToString()))
            {
                q += "";    
            }
            else
            {
                if (filterText1 != "" && filterText1 != txtFilter1.Tag.ToString())
                {
                    if (filter1.Text == "Объект")
                    {
                        q += " and SStation like '%" + filterText1 + "%' ";
                    }
                    if (filter1.Text == "Исполнитель")
                    {
                        q += " and SExecutor like '%" + filterText1 + "%' ";
                    }
                }
                if (filterText2 != "" && filterText2 != txtFilter2.Tag.ToString())
                {
                    if (filter2.Text == "Объект")
                    {
                        q += " and SStation like '%" + filterText2 + "%' ";
                    }
                    if (filter2.Text == "Исполнитель")
                    {
                        q += " and SExecutor like '%" + filterText2 + "%' ";
                    }
                }
            }

      //q += " where 1=1";

      if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstSpecDone.Text == "в работе") q += " and pto_block=0 ";
      else if (lstSpecDone.Text == "завершено") q += " and pto_block=1 ";

      long f = lstSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and STId=" + f;

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      q += " order by SVName";

      MyFillDgv(dgvSpec, q);
      /*foreach (DataGridViewRow row in dgvSpec.Rows)
      if (Convert.ToInt32(row.Cells["dgv_SState"].Value) == 1)
      {
          row.DefaultCellStyle.BackColor = Color.LightCoral;
      }*/
      return;
    }


    private void btnExport_Click(object sender, EventArgs e)
    {
      string filterText1 = txtFilter1.Text;
      string filterText2 = txtFilter2.Text;
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        tt.Add(f.Title);
      }

      /*
       
      */

      /*string q = "select distinct SId,SArea,SStation,SNo,STName,SVName,SVStage,SVNo,convert(nvarchar(10),SVDate,104) SVDate,SOBject,SSystem,";
      q += "\nNewestFillingCount,manager,curator,SDog,SBudget,SBudgetTotal ";
      q += "\nfrom vwSpec where 1=1 ";*/

      string q = "select distinct SId,SSystem,SStation,curator,SContractNum,SVName,STName,SExecutor,SArea,SNo,SVNo,SVStage,cast(SVProjectSignDate as date)SVProjectSignDate,SVProjectBy,SSubDocNum, cast(SVDate as date)SVDate,SComment" +
                ", case when NewestFillingCount > 0 then 'да' else 'нет' end as has_filling, case when SState = 1 then 'заблокирован' else 'активен' end as is_active ";
      //+",SDog,SBudget,SBudgetTotal ";
      q += " from vwSpec where 1=1 ";

            if ((filterText1 == "" || filterText1 == txtFilter1.Tag.ToString()) && (filterText2 == "" || filterText2 == txtFilter2.Tag.ToString()))
            {
                q += "";
            }
            else
            {
                if (filterText1 != "" && filterText1 != txtFilter1.Tag.ToString())
                {
                    if (filter1.Text == "Объект")
                    {
                        q += " and SStation like '%" + filterText1 + "%' ";
                    }
                    if (filter1.Text == "Исполнитель")
                    {
                        q += " and SExecutor like '%" + filterText1 + "%' ";
                    }
                }
                if (filterText2 != "" && filterText2 != txtFilter2.Tag.ToString())
                {
                    if (filter2.Text == "Объект")
                    {
                        q += " and SStation like '%" + filterText2 + "%' ";
                    }
                    if (filter2.Text == "Исполнитель")
                    {
                        q += " and SExecutor like '%" + filterText2 + "%' ";
                    }
                }
            }

            if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
            else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

            if (lstSpecDone.Text == "в работе") q += " and pto_block=0 ";
            else if (lstSpecDone.Text == "завершено") q += " and pto_block=1 ";

            long ff = lstSpecTypeFilter.GetLstVal();
            if (ff > 0) q += " and STId=" + ff;
            
            long managerAO = lstSpecManagerAO.GetLstVal();
            if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

            int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }


      q += " order by SVName;";

      MyExcel(q, FillingReportStructure, true, new decimal[] { 10,46,22,20,20,32,17,26,27,15,10,20,24,16,16,16,50,10 }, new int[] { 1,4,8,18,19 });
    }

    private void lstSpecTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void btnExportManager_Click(object sender, EventArgs e)
    {
      MsgBox("Если в списке не выгрузился нужный сотрудник АО, позвоните системному администратору и попросите проверить наличие и роль этого пользователя в базе.");

      List<string> tt = new List<string>();
      tt.Add("Ответственный АО");

      
      string q = "select UFIO from vwUser where ManagerAO = 1 and ufio is not null and EUIsFired = 0 ";

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      q += " order by UFIO;";

      MyExcelIns(q, tt.ToArray(), false, new decimal[] { 20 });
    }

    private void btnExportCurator_Click(object sender, EventArgs e)
    {
      MsgBox("Если в списке не выгрузился нужный куратор, позвоните системному администратору и попросите проверить наличие и роль этого пользователя в базе.");

      List<string> tt = new List<string>();
      tt.Add("Куратор");

      string q = "select UFIO from vwUser where EUIsCurator=1 and ufio is not null and EUIsFired=0 ";

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      q += " order by UFIO;";

      MyExcelIns(q, tt.ToArray(), false, new decimal[] { 20 });
    }

    private void btnImport_Click(object sender, EventArgs e)
    {

      dynamic oExcel;
      dynamic oSheet;
      bool bNoError = MyExcelImportOpenDialog(out oExcel, out oSheet, "");

      if (!bNoError) return;

      if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
      if (bNoError) MyExcelUnmerge(oSheet);

      if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);
      if (bNoError) bNoError = FillingImportCheckSIds(oSheet);
      if (bNoError) bNoError = FillingImportCheckCurator(oSheet);
      //if (bNoError) bNoError = FillingImportCheckManager(oSheet);
      //if (bNoError) bNoError = FillingImportCheckIdsUniq(oSheet);

      //if (bNoError && !FillingImportCheckSums(oSheet)) bNoError = false; // qty?

      oExcel.ScreenUpdating = true;
      oExcel.DisplayAlerts = true;

      if (bNoError)
      {
        if (MessageBox.Show("Ошибок не обнаружено. Продолжить?"
            , "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet);
          fill_dgv();
          MsgBox("Ok");
        }
        oExcel.Quit();
      }
      else
      {
        oExcel.Visible = true;
        oExcel.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private void FillingImportData(dynamic oSheet)
    {
      string sId;
      string sSystem;
      string sStation;
      string sCurator;
      string sContractNum;

      string sName;
      string sType; long lType;
      string sExecutor;

      string sArea;
      string sNo;
      string sSVNo; //SpecVer.SVNo

      string sStage;

      DateTime dtProjectSignDate;
      string sProjectSignDate;
      string sProjectBy;
      DateTime dt;
      string sDT;
      string sComment;


      //string sObject;
      //string sSVNo;
      //string sManager;
      //long lManager;

      //List<string> qInsert;
      //string qUpdate = "";
      //string qLog = "";

      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;

      //qInsert = "insert into Done (DSpecExecFill,DQty,DDate) Values\n";

      string q;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 60 + 40 * r / rows, "Формирование запросов");
        sId = oSheet.Cells(r, 1).Value?.ToString() ?? "";
        sSystem = oSheet.Cells(r, 2).Value.ToString();
        sStation = oSheet.Cells(r, 3).Value.ToString();
        sCurator = oSheet.Cells(r, 4).Value?.ToString() ?? "";
        sCurator = sCurator == "" ? "0" : MyGetOneValue("select UId from vwUser where EUIsCurator=1 and UFIO = " + MyES(sCurator)).ToString();

        sContractNum = oSheet.Cells(r, 5).Value?.ToString() ?? "";
        //sManagerAO = sManagerAO == "" ? "0" : MyGetOneValue("select UId from vwUser where ManagerAO=1 and UFIO = " + MyES(sManagerAO)).ToString();

        sName = oSheet.Cells(r, 6).Value.ToString(); //SV
        sType = oSheet.Cells(r, 7).Value.ToString();
        lType = Convert.ToInt64(MyGetOneValue("select STId from SpecType where STName = " + MyES(sType)));
        sExecutor= oSheet.Cells(r, 8).Value?.ToString() ?? "";
        sArea = oSheet.Cells(r, 9).Value.ToString();
        sNo = oSheet.Cells(r, 10).Value.ToString();
        sSVNo = oSheet.Cells(r, 11).Value.ToString(); //SV
        sStage = oSheet.Cells(r, 12).Value.ToString(); //SV

        if ((oSheet.Cells(r, 13).Value?.ToString() ?? "") == "") sProjectSignDate = "null";
        else
        {
          dtProjectSignDate = oSheet.Cells(r, 13).Value.GetType().Name == "DateTime" ? oSheet.Cells(r, 13).Value : DateTime.Parse(oSheet.Cells(r, 13).Value); //SV
          sProjectSignDate = MyES(dtProjectSignDate);
        }
        sProjectBy = oSheet.Cells(r, 14).Value?.ToString() ?? ""; //SV

        if ((oSheet.Cells(r, 16).Value?.ToString() ?? "") == "") sDT = "null";
        else
        {
          dt = oSheet.Cells(r, 16).Value.GetType().Name == "DateTime" ? oSheet.Cells(r, 16).Value : DateTime.Parse(oSheet.Cells(r, 16).Value); //SV
          sDT = MyES(dt);
        }
        //  dt = oSheet.Cells(r, 15).Value.GetType().Name == "DateTime" ? oSheet.Cells(r, 15).Value : DateTime.Parse(oSheet.Cells(r, 15).Value); //SV
        sComment = oSheet.Cells(r, 17).Value?.ToString() ?? "";
                string SSubDocNum = oSheet.Cells(r, 15).Value?.ToString() ?? "";
        if (sId == "")
        {//insert
          q = "insert into Spec (SSystem,SStation,SType,SExecutor,SArea,SNo,SCurator,SContractNum,SSubDocNum,SComment) Values (" +
            MyES(sSystem) + 
            " ," + MyES(sStation) +
            " ," + lType +
            " ," + MyES(sExecutor) +
            " ," + MyES(sArea) +
            " ," + MyES(sNo) +
            " ," + MyES(sCurator) +
            " ," + MyES(sContractNum) +
            " ," + MyES(SSubDocNum) +
            " ," + MyES(sComment) +
            " );  select cast(scope_identity() as bigint) new_id;";
          EntityId = (long)MyGetOneValue(q);
          q = "insert into SpecVer (SVSpec,SVName,SVNo,SVStage,SVProjectSignDate,SVProjectBy,SVDate) values (" + 
            EntityId + 
            "," + MyES(sName) + 
            "," + MyES(sSVNo) + 
            "," + MyES(sStage) +
            "," + sProjectSignDate +
            "," + MyES(sProjectBy) +
            "," + sDT +
          ");";
          MyExecute(q);
          MyLog(uid, "Spec", 11, EntityId, EntityId);
        }
        else
        {//update
          q = "update Spec set " +
            " SSystem=" + MyES(sSystem) +
            " ,SStation=" + MyES(sStation) +
            " ,SType=" + MyES(lType) +
            " ,SExecutor=" + MyES(sExecutor) +
            " ,SArea=" + MyES(sArea) +
            " ,SNo=" + MyES(sNo) +
            " ,SCurator=" + sCurator +
            " ,SContractNum=" + MyES(sContractNum) +
            " ,SSubDocNum=" + MyES(SSubDocNum) +
            " ,SComment=" + MyES(sComment) +
            " where SId=" + MyES(sId) + "\n";
          MyExecute(q);
          MyLog(uid, "Spec", 12, long.Parse(sId), long.Parse(sId));
          //long lSvidCount = long.Parse(MyGetOneValue("select count (*) from SpecVer where SVSpec=" + sId + " and SVNo='"+sSVNo.Replace(',','.')+"'").ToString());
          string sSvid = MyGetOneValue("select SVId from SpecVer where SVSpec=" + sId + " and SVNo='" + sSVNo.Replace(',', '.') + "'")?.ToString() ?? "";
          if (sSvid == "")
          {
            q = "insert into SpecVer (SVSpec,SVName,SVNo,SVStage,SVProjectSignDate,SVProjectBy,SVDate) values (" +
              sId +
              "," + MyES(sName) +
              "," + "'" + sSVNo.Replace(',', '.') + "'" +
              "," + MyES(sStage) +
              "," + sProjectSignDate +
              "," + MyES(sProjectBy) +
              "," + sDT +
            ");  select cast(scope_identity() as bigint) new_id;";
            long newSVId = (long)MyGetOneValue(q);
            MyLog(uid, "Spec", 21, newSVId, long.Parse(sId));
          }
          else
          {//
            q = "Update SpecVer set "+
              "SVName=" + MyES(sName) +
              ",SVNo = '" + sSVNo.Replace(',', '.') + "'" +
              ",SVStage=" + MyES(sStage) +
              ",SVProjectSignDate=" + sProjectSignDate +
              ",SVProjectBy=" + MyES(sProjectBy) +
              ",SVDate=" + sDT +
            " where SVId="+MyES(sSvid) +";";
            MyExecute(q);
            //добавить заись в лог для изменения, для чего создать сохраненку
          }
          /*else {MsgBox("Найден дубль номера версии для шифра "+ sName + "!\n\nОбратитесь к руководителю.","Ошибка!",MessageBoxIcon.Exclamation);}*/
        }
      }
      MyExecute("update spec set SObject='' where SObject is null");
      MyProgressUpdate(pb, 95, "Импорт данных");
      return;
    }

    private bool FillingImportCheckSIds(dynamic oSheet)
    {
      string sErr = "";
      long SId;
      string s;
      long z;
      int ErrCount = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SFEOId
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 30 + 10 * r / rows, "Проверка идентификаторов строк.");
        s = oSheet.Cells(r, c).Value?.ToString() ?? "";
        if (s != "")
        {
          z = long.TryParse(s, out SId) ?
            Convert.ToInt64(MyGetOneValue("select count(*)c from Spec where SId = " + MyES(SId) + ";"))
            : 0;
          if (z == 0)
          {
            ErrCount++;
            oSheet.Cells(r, 1).Interior.Color = 13421823;
            oSheet.Cells(r, 1).Font.Color = -16776961;
          }
          else if (z > 1) throw new Exception();
        }
      }

      if (ErrCount > 0)
      {
        sErr += "\nВ файле часть идентификаторов шифров ошибочны (" + ErrCount + ").";
        MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      }
      return ErrCount == 0;
    }

    private bool FillingImportCheckManager(dynamic oSheet)
    {
      string sErr = "";
      string s;
      long z;
      int ErrCount = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 5; // 1-based UFIO
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка ответственных АО.");
        s = oSheet.Cells(r, c).Value?.ToString() ?? "";
        z = s == "" ? 1 : Convert.ToInt64(MyGetOneValue("select count(*) from vwUser where ManagerAO = 1 and UFIO = " + MyES(s)));
        if (z == 0)
        {
          ErrCount++;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }

      if (ErrCount > 0)
      {
        sErr += "\nВ файле ответственный АО указан неверно (" + ErrCount + ").";
        MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      }
      return ErrCount == 0;
    }

    private bool FillingImportCheckCurator(dynamic oSheet)
    {
      string sErr = "";
      string s;
      long z;
      int ErrCount = 0;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 4; //14 // 1-based UFIO
      if (rows == 1) return true;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 40 + 10 * r / rows, "Проверка кураторов.");
        s = oSheet.Cells(r, c).Value?.ToString() ?? "";
        z = s=="" ? 1 : Convert.ToInt64(MyGetOneValue("select count(*) from vwUser where EUIsCurator = 1 and UFIO = "+ MyES(s)));
        if (z == 0)
        {
          ErrCount++;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }

      if (ErrCount > 0)
      {
        sErr += "\nВ файле ответственный ПТО указан неверно (" + ErrCount + ").";
        MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
      }
      return ErrCount == 0;
    }

    private void dgvSpec_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      DataGridView dgv = ((DataGridView)sender);
      if (dgv.Columns[e.ColumnIndex].Name == "dgv_btn_folder" && e.RowIndex >= 0)
      {
        long lSId = (long)dgv.Rows[e.RowIndex].Cells["dgv_id_SId"].Value;

        string sFolder = MyGetOneValue("select EOValue from _engOptions where EOName='DataFolder'").ToString();
        sFolder = sFolder + lSId.ToString();
        string sTxtName = dgv.Rows[e.RowIndex].Cells["dgv_SVName"].Value.ToString();
        sTxtName = sFolder + "\\" + MakeValidFileName(sTxtName) + ".txt";

        string sVerNo = "\\Версия " + dgv.Rows[e.RowIndex].Cells["dgv_SVNo"].Value.ToString();

        List<string> ss = new List<string>();
        //ss.Add(sFolder);
        ss.Add(sFolder + "\\Проект" + sVerNo);
        ss.Add(sFolder + "\\Протоколы разделения поставки" + sVerNo);
        ss.Add(sFolder + "\\Протоколы разделения работ" + sVerNo);
        ss.Add(sFolder + "\\Сканы УПД и М15");
        ss.Add(sFolder + "\\Тех. документация для ИД");
        ss.Add(sFolder + "\\Сканы ВОР");
        ss.Add(sFolder + "\\Смета" + sVerNo);
        ss.Add(sFolder + "\\КС");
        foreach (string sss in ss) { 
          if (!System.IO.Directory.Exists(sss)) System.IO.Directory.CreateDirectory(sss); 
        }

        try
        {
          string[] ff = Directory.GetFiles(sFolder, "*.txt");
          bool b = false;
          foreach (string f in ff)
          {
            if (f != sTxtName) File.Delete(f);
            else b = true;
          }
          if (!b)
          {
            FileStream fs = File.Create(sTxtName);
            fs.Close();
          }
        }
        catch (Exception ex) { }
        Process.Start(sFolder);
      }
    }

    private void dgvSpec_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
      Cursor = Cursors.Default;
    }

    private void dgvSpec_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
      string ColName = dgvSpec.Columns[e.ColumnIndex].Name;
      if (ColName == "dgv_btn_folder" && Cursor != Cursors.Hand) Cursor = Cursors.Hand;
      else Cursor = Cursors.Default;
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

    private void SpecTypeFilter(object sender = null, EventArgs e = null)
    {
      if (FormIsUpdating) return;
      fill_dgv();
    }

    private void btnReportF7_Click(object sender, EventArgs e)
    {
      Form mli = new MultilineInput();
      mli.ShowDialog();
      /*string s;
      s = Interaction.InputBox("Prompt", "Title", "Default");//, x_coordinate, y_coordinate);
      MsgBox(s);*/

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
        }

        /**/

        /*private bool FillingImportCheckIdsUniq(dynamic oSheet)
        {
          string sErr = "";
          string s;
          HashSet<string> ssuniq = new HashSet<string>();
          List<string> errs = new List<string>();
          long z;
          dynamic range = oSheet.UsedRange;
          int rows = range.Rows.Count;
          int c = 1; // 1-based SFEId
          if (rows == 1) return true;

          for (int r = 2; r < rows + 1; r++)
          {
            MyProgressUpdate(pb, 60 + 10 * r / rows, "Проверка единичности идентификаторов строк.");
            s = oSheet.Cells(r, c).Value.ToString();
            if (!ssuniq.Add(s)) errs.Add(s);
          }

          if (errs.Count() > 0)
          {
            oSheet.Columns(1).FormatConditions.AddUniqueValues();
            oSheet.Columns(1).FormatConditions(1).DupeUnique = 1;// xlDuplicate;
            oSheet.Columns(1).FormatConditions(1).Font.Color = -16776961;
            oSheet.Columns(1).FormatConditions(1).Interior.Color = 0;
            oSheet.Columns(1).FormatConditions(1).StopIfTrue = 0;
            sErr += "\nВ файле для загрузки идентификаторы строк не уникальны.";
          }

          if (sErr != "") MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
          return sErr == "";
        }*/
    }
}
