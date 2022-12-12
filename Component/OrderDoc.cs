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
  public partial class OrderDoc : UserControl//UserControl // SmuOk_.Common.MyComponent
  {
    public OrderDoc()
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
      FillingReportStructure = FillReportData("OrderDoc");
      dgvBudg.Columns["dgv_btn_folder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      FillFilter();
      fill_dgv();
      //FillFilter();
      FormIsUpdating = false;
    }

    private void fill_dgv()
    {
      string q = "select vw.SId,SVName,Initiator,OrderNum,OrderDate,RecieveDate,Note,OrderId, case when od.OrderId is NULL then NULL ELSE q.c end RowsFinished" +
                " from vwSpec vw" +
                " left join OrderDoc od on od.SpecId = vw.SId" +
                " outer apply ( select count(*)c from SupplyOrder so where so.SOOrderDocId = od.OrderId)q";

      string sName = txtSpecNameFilter.Text;
            q += " where 1=1";
      if (sName != "" && sName != txtSpecNameFilter.Tag.ToString()) 
      {
        q += " and vw.SId in (select SVSpec svs from SpecVer " +
              " where SVName like "+ MyES(sName, true) +
              " or SVSpec=" + MyDigitsId(sName) +
              ") ";
      }

      if (lstSpecHasFillingFilter.Text == "без спецификации") q += " and NewestFillingCount=0 ";
      else if (lstSpecHasFillingFilter.Text == "с наполнением") q += " and NewestFillingCount>0 ";

      if (lstSpecDone.Text == "в работе") q += " and pto_block=0 ";
      else if (lstSpecDone.Text == "завершено") q += " and pto_block=1 ";

      long f = lstSpecTypeFilter.GetLstVal();
      if (f > 0) q += " and STId=" + f;

      long managerAO = lstSpecManagerAO.GetLstVal();
      if (managerAO > 0) q += " and ManagerAO=" + MyES(lstSpecManagerAO.GetLstText());

      q += " order by vw.SId, SVName";

      MyFillDgv(dgvBudg, q);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      List<string> tt = new List<string>();
      foreach (MyXlsField f in FillingReportStructure)
      {
        tt.Add(f.Title);
      }
      string q = "select vw.SId,SVName,Initiator,OrderNum,OrderDate,RecieveDate,Note,OrderId, case when od.OrderId is NULL then NULL ELSE q.c end RowsFinished";
      //+",SDog,SBudget,SBudgetTotal ";
      q += " from vwSpec vw left join OrderDoc od on od.SpecId = vw.SId outer apply(select count(*) c from SupplyOrder so where so.SOOrderDocId = od.OrderId)q where 1=1";

      int c = (int)MyGetOneValue("select count(*)c from \n(" + q + ")q");
      if (c == 0)
      {
        MsgBox("Нет наполнения, нечего выгружать.");
        return;
      }

      long l = lstSpecTypeFilter.GetLstVal();
      if (l > 0) q += " and STId=" + l;

      q += " order by vw.SId,SVName;";

      MyExcel(q, FillingReportStructure, true, new decimal[] { 10,46,15.43M,15.43M, 20, 20, 46,9.14M,16.29M}, new int[] {1,2,8,9});
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
      if (bNoError) bNoError = FillingImportCheckOrderIds(oSheet);

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
      string sId,initiator, orderNum, orderDate, recieveDate, note;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      long orderId;
      string q;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 60 + 40 * r / rows, "Формирование запросов");
                sId = oSheet.Cells(r, 1).Value?.ToString() ?? "";
                initiator = oSheet.Cells(r, 3).Value?.ToString() ?? "";
                orderNum = oSheet.Cells(r, 4).Value?.ToString() ?? "";
                orderDate = oSheet.Cells(r, 5).Value?.ToString() ?? "";
                recieveDate = oSheet.Cells(r, 6).Value?.ToString() ?? "";
                note = oSheet.Cells(r, 7).Value?.ToString() ?? "";
                if(!long.TryParse(oSheet.Cells(r, 8).Value?.ToString() ?? "",out orderId))
                {
                    orderId = 0;
                }


                if (orderId == 0 && orderNum != "")
                {//insert
                    q = "insert into OrderDoc (Initiator,OrderNum,OrderDate,RecieveDate,Note,SpecId) Values (" +
                      " " + MyES(initiator) +
                      " ," + MyES(orderNum) +
                      " ," + MyES(orderDate) +
                      " ," + MyES(recieveDate) +
                      " ," + MyES(note) +
                      " ," + MyES(sId) +
                      " );  select cast(scope_identity() as bigint) new_id;";
                    orderId = (long)MyGetOneValue(q);////////////////тут остановился
                    MyLog(uid, "OrderDoc", 2014, EntityId, orderId);
                }
                else if (orderId == 0 && orderNum == "")
                {
                    continue;
                }
                else
                {/*update*/
                    q = "update OrderDoc set" +
                        " Initiator = " + MyES(initiator) +
                        " ,OrderNum = " + MyES(orderNum) +
                        " ,OrderDate = " + MyES(orderDate) +
                        " ,RecieveDate = " + MyES(recieveDate) +
                        " ,Note = " + MyES(note) +
                        " ,SpecId = " + MyES(sId) +
                        " where OrderId = " + orderId;
                    MyExecute(q);
                    MyLog(uid, "OrderDoc", 2015, EntityId, orderId);
                }
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

        private bool FillingImportCheckOrderIds(dynamic oSheet)
        {
            string sErr = "";
            long SId;
            string s;
            long z;
            int ErrCount = 0;
            dynamic range = oSheet.UsedRange;
            int rows = range.Rows.Count;
            int c = 8; // OrderId
            if (rows == 1) return true;

            for (int r = 2; r < rows + 1; r++)
            {
                MyProgressUpdate(pb, 30 + 10 * r / rows, "Проверка идентификаторов строк.");
                s = oSheet.Cells(r, c).Value?.ToString() ?? "";
                if (s != "")
                {
                    z = long.TryParse(s, out SId) ?
                      Convert.ToInt64(MyGetOneValue("select count(*)c from OrderDoc where OrderId = " + MyES(SId) + ";"))
                      : 0;
                    if (z == 0)
                    {
                        ErrCount++;
                        oSheet.Cells(r, 8).Interior.Color = 13421823;
                        oSheet.Cells(r, 8).Font.Color = -16776961;
                    }
                    else if (z > 1) throw new Exception();
                }
            }

            if (ErrCount > 0)
            {
                sErr += "\nВ файле часть идентификаторов заказов ошибочны (" + ErrCount + ").";
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
