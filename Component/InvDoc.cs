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
  public partial class InvDoc : UserControl//UserControl // SmuOk_.Common.MyComponent
  {
    public InvDoc()
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

      /*MyFillList(lstSpecHasFillingFilter, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='HasFilling';", "(наполнение)");
      MyFillList(lstSpecDone, "select EFId, EFOption From _engFilter Where EFEntity='Spec' and EFFilter='PTODone';", "(обработано)");
      MyFillList(lstSpecTypeFilter, "select distinct STId,STName from SpecType", "(тип шифра)");
      MyFillList(lstSpecManagerAO, "select UId, UFIO from vwUser where ManagerAO=1 order by UFIO;", "(ответственный АО)");*/
      //if (MyGetOneValue("select count (*) from vwUser where ManagerAO=1 and UId=" + uid).ToString() == "1") lstSpecManagerAO.SelectedValue = uid;
    }

    public void LoadMe()
    {
      FormIsUpdating = true;
      FillingReportStructure = new List<MyXlsField>();
      //int i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='chkPTOMultiline' and EUIOVaue=1");
      FillingReportStructure = FillReportData("InvDoc");
      dgvBudg.Columns["dgv_btn_folder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      FillFilter();
      fill_dgv();
      //FillFilter();
      FormIsUpdating = false;
    }

    private void fill_dgv()
    {
      string q = "select InvId,InvType,InvINN,InvLegalName,InvNum,InvDate,InvSumWOVAT,case when id.InvId is NULL then NULL else q.c end InvSumFinished,InvComment" +
                " from InvDoc id" +
                " outer apply (select sum(ICQty * ICPrc)c from InvCfm ic where ic.InvDocId = id.InvId)q";

      string sName = txtSpecNameFilter.Text;
            q += " where 1=1";
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString()) 
      {
        q += " and InvId=" + MyDigitsId(sName);
      }/*

      if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstSpecDone.Text == "в работе") q += " and pto_block=0 ";
      else if (lstSpecDone.Text == "завершено") q += " and pto_block=1 ";*/

      /*long f = lstSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and InvId=" + f;*/

      /*long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      q += " order by vw.SId, SVName";*/

      MyFillDgv(dgvBudg, q);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        tt.Add(f.Title);
      }
      string q = "select InvId,InvType,InvINN,InvLegalName,InvNum,InvDate,InvSumWOVAT,case when id.InvId is NULL then NULL else q.c end InvSumFinished,InvComment";
      //+",SDog,SBudget,SBudgetTotal ";
      q += " from InvDoc id outer apply (select sum(ICQty * ICPrc)c from InvCfm ic where ic.InvDocId = id.InvId)q where 1=1";

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }
      string sName = txtSpecNameFilter.Text;
            if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
            {
                q += " and InvId=" + MyDigitsId(sName);
            }
            //long l = lstSpecTypeFilter.GetLstVal();
            //if (l > 0) q += " and InvId=" + l;

            MyExcel(q, FillingReportStructure, true, new decimal[] { 15.43M,15.43M, 20, 20, 20,9.14M,16.29M, 16.29M,30}, new int[] {8});
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
      if (bNoError) bNoError = FillingImportCheckInvIds(oSheet);

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
      string invType, invINN, invLegalName, invNum, invDate, invComment;
      decimal invSumWOVAT;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      long InvId;
      string q;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 60 + 40 * r / rows, "Формирование запросов");
                invType = oSheet.Cells(r, 2).Value?.ToString() ?? "";
                invINN = oSheet.Cells(r, 3).Value?.ToString() ?? "";
                invLegalName = oSheet.Cells(r, 4).Value?.ToString() ?? "";
                invNum = oSheet.Cells(r, 5).Value?.ToString() ?? "";
                invDate = oSheet.Cells(r, 6).Value?.ToString() ?? "";
                invComment = oSheet.Cells(r, 9).Value?.ToString() ?? "";
                if (!long.TryParse(oSheet.Cells(r, 1).Value?.ToString() ?? "",out InvId))
                {
                    InvId = 0;
                }
                if(!Decimal.TryParse(oSheet.Cells(r, 7).Value?.ToString() ?? "", out invSumWOVAT))
                {
                    invSumWOVAT = 0;
                }


                if (InvId == 0 && invNum != "")
                {//insert
                    q = "insert into InvDoc (InvType,InvINN,InvLegalName,InvNum,InvDate,InvSumWOVAT,InvComment) Values (" +
                      " " + MyES(invType) +
                      " ," + MyES(invINN) +
                      " ," + MyES(invLegalName) +
                      " ," + MyES(invNum) +
                      " ," + MyES(invDate) +
                      " ," + MyES(invSumWOVAT) +
                      " ," + MyES(invComment) +
                      " );  select cast(scope_identity() as bigint) new_id;";
                    InvId = (long)MyGetOneValue(q);////////////////тут остановился
                }
                else if (InvId == 0 && invNum == "")
                {
                    continue;
                }
                else
                {/*update*/
                    q = "update InvDoc set" +
                        " InvType = " + MyES(invType) +
                        " ,InvINN = " + MyES(invINN) +
                        " ,InvLegalName = " + MyES(invLegalName) +
                        " ,InvNum = " + MyES(invNum) +
                        " ,InvDate = " + MyES(invDate) +
                        " ,InvSumWOVAT = " + MyES(invSumWOVAT) +
                        " ,InvComment = " + MyES(invComment) +
                        " where InvId = " + InvId;
                    MyExecute(q);
                }
                MyLog(uid, "InvDoc", 11, EntityId, InvId);
                
            }
      MyProgressUpdate(pb, 95, "Импорт данных");
      return;
    }

        private bool FillingImportCheckInvIds(dynamic oSheet)
        {
            string sErr = "";
            long SId;
            string s;
            long z;
            int ErrCount = 0;
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;
            int c = 1; // InvId
            if (rows == 1) return true;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 30 + 10 * r / rows, "Проверка идентификаторов строк.");
                s = oSheet.Cells(r, c).Value?.ToString() ?? "";
                if (s != "")
                {
                    z = long.TryParse(s, out SId) ?
                      Convert.ToInt64(MyGetOneValue("select count(*)c from InvDoc where InvId = " + MyES(SId) + ";"))
                      : 0;
                    if (z == 0)
                    {
                        ErrCount++;
                        oSheet.Cells(r, c).Interior.Color = 13421823;
                        oSheet.Cells(r, c).Font.Color = -16776961;
                    }
                    else if (z > 1) throw new Exception();
                }
            }

            if (ErrCount > 0)
            {
                sErr += "\nВ файле часть идентификаторов счетов ошибочны (" + ErrCount + ").";
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

        private void deleteOrder_btn_Click(object sender, EventArgs e)
        {
            string q = "";
            q = "delete from OrderDoc where OrderId in ( " + OrderId.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            OrderId.Text = "";
            return;
        }
    }
}
