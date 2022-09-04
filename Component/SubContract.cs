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
  public partial class SubContract : UserControl//UserControl // SmuOk_.Common.MyComponent
  {
    public SubContract()
    {
      InitializeComponent();
    }

    private bool FormIsUpdating = true;
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
      FillingReportStructure = FillReportData("SubContract");
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

      string q = "select vw.SId,vw.SSystem/*наименование работ*/,vw.SStation,vw.curator,SVName,vw.SArea" +
                " ,sc.SubContractId, sc.SubName, sc.SubINN, sc.SubContractNum, sc.SubContractDate, sc.SubDownKoefSMR, sc.SubDownKoefPNR, sc.SubDownKoefTMC, sc.SubContractAprPriceWOVAT " +
                " from vwSpec vw" +
                " left join SubContract sc on vw.SId = sc.SubSpecId ";

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

      string q = "select vw.SId,vw.SSystem/*наименование работ*/,vw.SStation,vw.curator,SVName,vw.SArea" +
                " ,sc.SubContractId, sc.SubName, sc.SubINN, sc.SubContractNum, sc.SubContractDate, sc.SubDownKoefSMR, sc.SubDownKoefPNR, sc.SubDownKoefTMC, sc.SubContractAprPriceWOVAT ";

      q += " from vwSpec vw left join SubContract sc on vw.SId = sc.SubSpecId where 1=1";

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      long l = lstSpecTypeFilter.GetLstVal();
      if (l > 0) q += " and STId=" + l;

      q += " order by SVName;";

      MyExcel(q, FillingReportStructure, true, new decimal[] { 10,46,15.43M,13.14M,20,12.30M,17,9.14M,16.29M,15,10,20,24,16,16,16,16,16,16,50 }, new int[] { 1,2,3,4,5,6,7 });
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
      if (bNoError) bNoError = FillingImportCheckRules(oSheet);
      //if (bNoError) bNoError = FillingImportCheckCurator(oSheet);
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


    private void FillingImportData(dynamic oSheet)/*here*/
    {
      long subSpecId, subContractId;
      string subName, subINN, subContractNum, subContractDate;
      decimal subDownKoefSMR, subDownKoefPNR, subDownKoefTMC, subContractAprPriceWOVAT;


      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;

      

      string q;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 60 + 40 * r / rows, "Формирование запросов");
                subSpecId = long.Parse(oSheet.Cells(r, 1).Value?.ToString() ?? "0");
                subContractId = long.Parse(oSheet.Cells(r, 7).Value?.ToString() ?? "0");
                subName = oSheet.Cells(r, 8).Value?.ToString() ?? "";
                subINN = oSheet.Cells(r, 9).Value?.ToString() ?? "";
                subContractNum = oSheet.Cells(r, 10).Value?.ToString() ?? "";
                subContractDate = oSheet.Cells(r, 11).Value?.ToString() ?? "";
                subDownKoefSMR = Decimal.Parse(oSheet.Cells(r, 12).Value?.ToString() ?? "0");
                subDownKoefPNR = Decimal.Parse(oSheet.Cells(r, 13).Value?.ToString() ?? "0");
                subDownKoefTMC = Decimal.Parse(oSheet.Cells(r, 14).Value?.ToString() ?? "0");
                subContractAprPriceWOVAT = Decimal.Parse(oSheet.Cells(r, 15).Value?.ToString() ?? "0");

                
                if (subContractId == 0 && subName != "")
                {//insert
                    if((subName == "конкурс" || subName == "Конкурс") && subContractAprPriceWOVAT == 0)
                    {
                        q = "insert into SubContract (subName, subSpecId) Values(" +
                        "" + MyES(subName) +
                        " ," + subSpecId + "); select cast(scope_identity() as bigint) new_id;";
                        subContractId = (long)MyGetOneValue(q);
                    }
                    else
                    {
                        q = "insert into SubContract (subName,subINN,subContractNum,subContractDate,subDownKoefSMR,subDownKoefPNR,subDownKoefTMC,subContractAprPriceWOVAT,subSpecId) Values (" +
                      "" + MyES(subName) + "" +
                      " ," + MyES(subINN) + "" +
                      " ," + MyES(subContractNum) + "" +
                      " ," + MyES(subContractDate) + "" +
                      " ," + MyES(subDownKoefSMR) + "" +
                      " ," + MyES(subDownKoefPNR) + "" +
                      " ," + MyES(subDownKoefTMC) + "" +
                      " ," + MyES(subContractAprPriceWOVAT) + "" +
                      " ," + subSpecId + "" +
                      " );  select cast(scope_identity() as bigint) new_id;";
                        subContractId = (long)MyGetOneValue(q);
                    }
                }
                else if (subContractId == 0 && subName == "")
                {
                    continue;
                }
                else
                {//update
                    if ((subName == "конкурс" || subName == "Конкурс") && subContractAprPriceWOVAT == 0)
                    {
                        continue;
                    }
                    else 
                    {
                        q = "update SubContract set" +
                        "  subName = " + MyES(subName) +
                        " ,subINN = " + MyES(subINN) +
                        " ,subContractNum = " + MyES(subContractNum) +
                        " ,subContractDate = " + MyES(subContractDate) +
                        " ,subDownKoefSMR = " + MyES(subDownKoefSMR) +
                        " ,subDownKoefPNR = " + MyES(subDownKoefPNR) +
                        " ,subDownKoefTMC = " + MyES(subDownKoefTMC) +
                        " ,subContractAprPriceWOVAT = " + MyES(subContractAprPriceWOVAT) +
                        "  where subContractId = " + subContractId;
                        MyExecute(q);
                    }
                }
                MyLog(uid, "SubContract", 11, subContractId, EntityId);
            }
                MyProgressUpdate(pb, 95, "Импорт данных");
      return;
    }

        private bool FillingImportCheckRules(dynamic oSheet)
        {
            string sErr = "";
            long SId;
            string s;
            long z;
            int ErrCount = 0;
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;
            int c = 1; // 1-based SFEOId
            long subSpecId, subContractId;
            string subName, subINN, subContractNum, subContractDate;
            string subDownKoefSMR, subDownKoefPNR, subDownKoefTMC, subContractAprPriceWOVAT;
            if (rows == 1) return true;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 60 + 40 * r / rows, "Формирование запросов");
                subSpecId = long.Parse(oSheet.Cells(r, 1).Value?.ToString() ?? "0");
                subContractId = long.Parse(oSheet.Cells(r, 7).Value?.ToString() ?? "0");
                subName = oSheet.Cells(r, 8).Value?.ToString() ?? "";
                subINN = oSheet.Cells(r, 9).Value?.ToString() ?? "";
                subContractNum = oSheet.Cells(r, 10).Value?.ToString() ?? "";
                subContractDate = oSheet.Cells(r, 11).Value?.ToString() ?? "";
                subDownKoefSMR = (oSheet.Cells(r, 12).Value?.ToString() ?? "");
                subDownKoefPNR = (oSheet.Cells(r, 13).Value?.ToString() ?? "");
                subDownKoefTMC = (oSheet.Cells(r, 14).Value?.ToString() ?? "");
                subContractAprPriceWOVAT = (oSheet.Cells(r, 15).Value?.ToString() ?? "");

                if (subName == "конкурс" || subName == "Конкурс") continue;
                else if (subContractId == 0 && subName != "")
                {
                    if (subINN == "" || subContractNum == "" || subContractDate == "" || (subDownKoefSMR + subDownKoefPNR + subDownKoefTMC) == "" || subContractAprPriceWOVAT == "")
                    {
                        ErrCount++;
                        oSheet.Cells(r, 1).Interior.Color = 13421823;
                        oSheet.Cells(r, 1).Font.Color = -16776961;
                    }
                    else continue;
                }
                else if (subContractId == 0 && subName == "")
                {
                    ErrCount++;
                    oSheet.Cells(r, 1).Interior.Color = 13421823;
                    oSheet.Cells(r, 1).Font.Color = -16776961;
                }
            }
            if (ErrCount > 0)
            {
                sErr += "\nВ файле часть строк введено некорректно (" + ErrCount + ").";
                MsgBox(sErr, "Ошибка", MessageBoxIcon.Warning);
            }
            return ErrCount == 0;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string q = "delete from SubContract where SubContractId in ( " + SubContractId.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            SubContractId.Text = "";
            return;
        }
    }
}
