using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyReport;
using static SmuOk.Common.MyConst;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SmuOk.Common
{
  public partial class MyProgress : Form
  {
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    public List<MyExportExcel> MyExportExcelList = new List<MyExportExcel>();

    public MyProgress()
    {
      InitializeComponent();
    }

    public void MakeExcelReport()
    {
      if (MyExportExcelList.Count == 0)
      {
        MsgBox("Нет данных.");
        return;
      }

      int ColCount;
      int RowCount; // title + data

      lblCaption.Text = "Создаем книгу Excel";
      pb.Value = 5;
      Application.UseWaitCursor = true;
      Type ExcelType = MyExcelType();

      dynamic oApp;

      try
      {
        oApp = Activator.CreateInstance(ExcelType);
      }
      catch (Exception ex)
      {
        TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
        return;
      }

      oApp.Visible = false;
      oApp.ScreenUpdating = false;
      oApp.DisplayAlerts = false;

      bool first_sheet = true;
      dynamic oBook = oApp.Workbooks.Add();

      int i = 0;
      int j = 5;
      int z = MyExportExcelList.Count();
      int report_sheet_count = 0;
      string s_prev="";

      for (i=MyExportExcelList.Count-1; i>=0; i--)
      {
        MyExportExcel mee = MyExportExcelList[i];
        lblCaption.Text = s_prev + "Получаем данные";
        pb.Value = j + 10 / z;
        string[,] vals = MyGet2DArray(mee.sQuery, mee.ssTitle == null);

        RowCount = vals?.GetLength(0) ?? 0;
        ColCount = vals?.GetLength(1) ?? 0;

        if (ColCount == 0) continue;
        report_sheet_count++;

        if (z > 1) {
          s_prev = "Отчет " + (z - i).ToString() + "/" + z + ": ";
          j = 5 + ((z-i-1) * 100 / z);
        }
        //  MyProgressUpdate(pb, 20, "Выгрузка...")
        if (first_sheet)
        {
          while (oBook.Worksheets.Count > 1) oBook.Worksheets(2).Delete();
          first_sheet = false;
        }
        else oBook.Worksheets.Add();

        dynamic oSheet = oBook.Worksheets(1); //oBook.Worksheets.Count

        lblCaption.Text = s_prev + "Заполняем заголовки"; // на самом деле не совсем в таком порядке, но ток логичнее читать юзерам
        pb.Value = j + 60 / z;

        if (mee.ssTitle != null)
        {
          oSheet.Range("A1").Resize(1, mee.ssTitle.Count()).Value = mee.ssTitle;
          if (vals != null)
          {
            oSheet.Range("A2").Resize(RowCount, ColCount).Value = vals;//r,c
            RowCount++;
          }
        }
        else
        {
          if (vals != null)
          {
            oSheet.Range("A1").Resize(RowCount, ColCount).Value = vals;//r,c////////////////////////////////////////////
          }
          else
          {
            //throw new Exception("Пустой заголовок и дата! СОобщте администратору.");
          }
        }
        if (mee.ReportTitle != "") oSheet.Name = mee.ReportTitle;
        //else if (vals != null) oSheet.Range("A1").Resize(RowCount, ColCount).Value = vals;
        lblCaption.Text = s_prev + "Фoрматируем лист";
        pb.Value = j + 80 / z;
                
        MyFormatExcelTable(oSheet, RowCount, ColCount, 3, mee.colsWidth, mee.GrayColIDs, mee.nowrap, mee.DateValueColIDs, budg:mee.AfterFormat, CenterColIDs: mee.CenterColIDs);

        switch (mee.AfterFormat)
        {
          case "SpecFillHistory":
            int r = 2;
            int cFrom = 7;
            int cName = 6;
            decimal dPrev, dCurr;
            /*
             vals?.GetLength(0) ?? 0 
            */
            try
            {
              while ((oSheet.Cells(r, 6).Value?.ToString() ?? "") != "")
              {
                for (int c = cFrom + 1; c <= ColCount; c++)
                {
                  dPrev = (decimal)oSheet.Cells(r, c - 1).Value;
                  dCurr = (decimal)oSheet.Cells(r, c).Value;
                  //dCurr = decimal.Parse(oSheet.Cells(r, c - 1).Value.ToString());
                  if (dPrev != dCurr)
                  {
                    if (dCurr > dPrev && dPrev == 0) // новая строка, бг желтым
                    {
                      oSheet.Cells(r, c).Interior.Color = 13434879;
                      if (c == ColCount) oSheet.Cells(r, cName).Interior.Color = 13434879;
                    }
                    else if (dCurr > dPrev) // количество увеличилось, бг зеленым
                    {
                      oSheet.Cells(r, c).Interior.Color = 13434828;
                      if (c == ColCount) oSheet.Cells(r, cName).Interior.Color = 13434828;
                    }
                    else if (dCurr < dPrev) // количество уменьшилось, бг красным
                    {
                      oSheet.Cells(r, c).Interior.Color = 13421823;
                      if (c == ColCount) oSheet.Cells(r, cName).Interior.Color = 13421823;
                      if (dCurr == 0) // строка удалена (не обязательно в актуальной версии) - красный шрифт
                      {
                        oSheet.Rows(r).Font.Color = -16776961;
                      }
                    }
                  }

                }
                r++;
              }
            }
            catch (Exception ex)
            {
              MsgBox(ex.Message.ToString());
            }
             

            break;
                    case "SpecFillBudgetHistory":
                        int rCounter = 2;
                        int From = 26;
                        int Name = 9;
                        decimal Prev, Curr;
                        int end = 11;

                        /*for(int counter = 10; counter < ColCount; counter++)
                        {
                            if ((oSheet.Cells(1, counter).Value?.ToString() ?? "") == "PID")
                                end = counter;
                            else continue;
                        }
                        /*
                         vals?.GetLength(0) ?? 0 
                        */
                        try
                        {
                            while ((oSheet.Cells(rCounter, Name).Value?.ToString() ?? "") != "")
                            {
                                for (int c = From + 1; c <= ColCount ; c++)
                                {
                                    Prev = (decimal)oSheet.Cells(rCounter, c - 1).Value;
                                    Curr = (decimal)oSheet.Cells(rCounter, c).Value;
                                    //dCurr = decimal.Parse(oSheet.Cells(r, c - 1).Value.ToString());
                                    if (Prev != Curr)
                                    {
                                        if (Curr > Prev && Prev == 0) // новая строка, бг желтым
                                        {
                                            oSheet.Cells(rCounter, c).Interior.Color = 13434879;
                                            if (c == ColCount) oSheet.Cells(rCounter, Name).Interior.Color = 13434879;
                                        }
                                        else if (Curr > Prev) // количество увеличилось, бг зеленым
                                        {
                                            oSheet.Cells(rCounter, c).Interior.Color = 13434828;
                                            if (c == ColCount) oSheet.Cells(rCounter, Name).Interior.Color = 13434828;
                                        }
                                        else if (Curr < Prev) // количество уменьшилось, бг красным
                                        {
                                            oSheet.Cells(rCounter, c).Interior.Color = 13421823;
                                            if (c == ColCount) oSheet.Cells(rCounter, Name).Interior.Color = 13421823;
                                            if (Curr == 0) // строка удалена (не обязательно в актуальной версии) - красный шрифт
                                            {
                                                oSheet.Rows(rCounter).Font.Color = -16776961;
                                                //oSheet.Cells(1, 2).Merge = true;
                                            }
                                        }
                                    }

                                }
                                rCounter++;
                            }
                        }
                        catch (Exception ex)
                        {
                            MsgBox(ex.Message.ToString());
                        }
                        break;

                }
        /*try
        {
          ((IDisposable)oSheet).Dispose();
          ((IDisposable)oBook).Dispose();
        }
        catch (System.Exception ex) {
          MsgBox(ex.Message);
        }*/

      }

      //xlsBorders(oSheet.Range("A1"))
      //  MyProgressUpdate(pb, 40, "Выгрузка...")

      pb.Value = 105;
      lblCaption.Text = s_prev + "Отчет готов";

      oApp.ScreenUpdating = true;

      if (report_sheet_count == 0)
      {
        MsgBox("Нет данных для отображения.");
        oApp.Workbooks(1).Close();
        oApp.DisplayAlerts = true;
        oApp.Quit();
      }
      else
      {
        oApp.Visible = true;
        oApp.DisplayAlerts = true;
        SetForegroundWindow(new IntPtr(oApp.Hwnd));
      }

      Application.UseWaitCursor = false;
      this.Close();
      return;
    }

    private void MyProgress_Load(object sender, EventArgs e)
    {
      this.Visible = true;
      MakeExcelReport();
    }

  }
}
