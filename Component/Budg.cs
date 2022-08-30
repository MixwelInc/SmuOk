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
  public partial class Budg : UserControl//UserControl // SmuOk_.Common.MyComponent
  {
    public Budg()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
    private string FormControlPref = "Budg";
    private string FormSqlPref = "B";
    private List<MyXlsField> FillingReportStructure;
    private long EntityId;

    private void Budg_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    private void FillFilter()
    {
      txtSpecNameFilter.Text = txtSpecNameFilter.Tag.ToString();

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
      FillingReportStructure = FillReportData("Budg");
      dgvBudg.Columns["dgv_btn_folder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      FillFilter();
      fill_dgv();
      //FillFilter();
      FormIsUpdating = false;
    }

    private void fill_dgv()
    {
      /*string q = "select distinct SId, SStation, SVName,SVStage, STName, SNo, SArea, SObject, SSystem, NewestFillingCount, SVNo, cast(SVDate as date) SVDate,SDog,SBudget,SBudgetTotal, manager, curator";
      q += " from vwSpec where 1=1";*/

      string q = "select vw.SId,vw.SSystem/*наименование работ*/,vw.SStation,vw.curator,SVName,vw.STName,vw.SExecutor,SVNo,SVStage" +
                " ,vw.SComment,BSMRorPNR,BNumber,BVer,BMIPRegNum,BStage,BCostWOVAT,BComm" +
                " from vwSpec vw" +
                " left join Budget b on b.BSId = vw.SId";

      string sName = txtSpecNameFilter.Text;
            q += " where 1=1";
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString()) 
      {
        q += " and vw.SId in (select SVSpec svs from SpecVer " +
              " where SVName like "+ MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              ") ";
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

      MyFillDgv(dgvBudg, q);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
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

      string q = "select vw.SId,vw.SSystem/*наименование работ*/,vw.SStation,vw.curator,SVName,vw.STName,vw.SExecutor,SVNo,SVStage" +
                " ,vw.SComment,BId,BSMRorPNR,BNumber,BByVer,BMIPRegNum,BVer,BStage,BCostWOVAT,BIncDate,BComm";
      //+",SDog,SBudget,SBudgetTotal ";
      q += " from vwSpec vw left join Budget b on b.BSId = vw.SId where 1=1";

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      long l = lstSpecTypeFilter.GetLstVal();
      if (l > 0) q += " and STId=" + l;

      q += " order by SVName;";

      MyExcel(q, FillingReportStructure, true, new decimal[] { 10,46,15.43M,13.14M,20,12.30M,17,9.14M,16.29M,15,10,20,24,16,16,16,16,16,16,50 }, new int[] { 1,2,3,4,5,6,7,8,9,10 }, budg: "budg", CenterColIDs: new int[] {1,3,4,6,8,9,11,12});
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
      string bSMRorPNR;
      string bBudgNumber;
      string bBudgVer;
      string bBudgMIPRegNum;
      string bId;

      string bBudgStage;
      decimal bBudgCostWOVAT;
      string bBudgComm;
            string bByVer;
            string incDate;



      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;

      //qInsert = "insert into Done (DSpecExecFill,DQty,DDate) Values\n";

      string q;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 60 + 40 * r / rows, "Формирование запросов");
                sId = oSheet.Cells(r, 1).Value?.ToString() ?? "";
                bId = oSheet.Cells(r, 11).Value?.ToString() ?? "";
                bSMRorPNR = oSheet.Cells(r, 12).Value?.ToString() ?? "";
                bBudgNumber = oSheet.Cells(r, 13).Value?.ToString() ?? "";
                bByVer = oSheet.Cells(r, 14).Value?.ToString() ?? "";//смет по изм проекта
                bBudgMIPRegNum = oSheet.Cells(r, 15).Value?.ToString() ?? "";
                bBudgVer = oSheet.Cells(r, 16).Value?.ToString() ?? "";//изм по смете
                bBudgStage = oSheet.Cells(r, 17).Value?.ToString() ?? "";
                bBudgCostWOVAT = (decimal)oSheet.Cells(r, 18).Value;
                incDate = oSheet.Cells(r, 19).Value?.ToString() ?? "";
                bBudgComm = oSheet.Cells(r, 20).Value?.ToString() ?? ""; //SV


                if (bId == "" && bBudgNumber != "")
                {//insert
                    q = "insert into Budget (BSMRorPNR,BNumber,BByVer,BVer,BMIPRegNum,BStage,BCostWOVAT,BIncDate,BComm,BSId) Values (" +
                      "'" + bSMRorPNR + "'" +
                      " ,'" + bBudgNumber + "'" +
                      " ,'" + bByVer + "'" +
                      " ,'" + bBudgVer + "'" +
                      " ,'" + bBudgMIPRegNum + "'" +
                      " ,'" + bBudgStage + "'" +
                      " ," + MyES(bBudgCostWOVAT) + "" +
                      " ,'" + incDate + "'" +
                      " ,'" + bBudgComm + "'" +
                      " ," + sId +
                      " );  select cast(scope_identity() as bigint) new_id;";
                    EntityId = (long)MyGetOneValue(q);////////////////тут остановился
                }
                else if (bId == "" && bBudgNumber == "")
                {
                    continue;
                }
                else
                {/*update*/
                    q = "update Budget set" +
                        " BSMRorPNR = '" + bSMRorPNR +
                        "' ,Bnumber = '" + bBudgNumber +
                        "' ,BByVer = '" + bByVer +
                        "' ,BVer = '" + bBudgVer +
                        "' ,BMIPRegNum = '" + bBudgMIPRegNum +
                        "' ,BStage = '" + bBudgStage +
                        "' ,BCostWOVAT = " + MyES(bBudgCostWOVAT) +
                        "  ,BIncDate = '" + incDate +
                        "' ,BComm = '" + bBudgComm +
                        "' where BId = " + bId;
                    MyExecute(q);
                }
                MyLog(uid, "Budg", 11, EntityId, EntityId);
                
            }
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
      string ColName = dgvBudg.Columns[e.ColumnIndex].Name;
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
    }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string q = "";
            q = "delete from Budget where BId in ( " + BudgId.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            BudgId.Text = "";
            return;
        }
    }
}
