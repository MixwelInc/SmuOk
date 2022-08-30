using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;
using static SmuOk.Common.MyExportExcel;
using System.Runtime.InteropServices;

namespace SmuOk.Common
{
  public static class MyReport
  {
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    public struct MyXlsField
    {
      public string SqlName { get; set; }
      public string Title { get; set; }
      public string DataType { get; set; } //string, date, long
      public bool Nulable { get; set; }
      public bool Subzero { get; set; }
      public string[] Vals { get; set; }
      public bool SkipOnLoad { get; set; }
      public MyXlsField(string sql_name, string title, string data_type, bool nulable = true, bool subzero = false, string[] vals = null, bool skip_on_load = false)
      {
        SqlName = sql_name;
        Title = title;
        DataType = data_type;
        Nulable = nulable;
        Subzero = subzero;
        Vals = vals;
        SkipOnLoad = skip_on_load;
      }
    }

    public static void MyProgressUpdate(object pb, double iPercent = -1, string sOperation = "")
    {
      if (iPercent > 100)
        iPercent = 100;
      if (pb == null)
        return;
      switch (pb.GetType().ToString())
      {
        case "System.Windows.Forms.ProgressBar":
          {
            Control c_pb = (Control)pb;
            if (iPercent < 1)
              c_pb.Visible = false;
            else
            {
              c_pb.Visible = true;
              ((ProgressBar)c_pb).Value = (int)iPercent;
            }
            if (c_pb.Tag.ToString() != "")
            {
              c_pb.Parent.Controls[c_pb.Tag.ToString()].Visible = iPercent >= 1;
              c_pb.Parent.Controls[c_pb.Tag.ToString()].Text = sOperation;
            }
            break;
          }
      }
    }

    public static bool MyExcelImport_CheckTitle(dynamic oSheet, List<MyXlsField> ReportStructure, object pb, bool budg = false)
    {
      MyProgressUpdate(pb, 10, "Проверка заголовков");
      string s;
      bool e = false;
            if(budg)
            {
                for (int i = 15; i <= 24; i++)
                {// MyXlsField f in FillingReportStructure)
                    MyProgressUpdate(pb, 10 + i * .5, "Проверка заголовков");
                    s = oSheet.Cells(1, i).Value?.ToString() ?? "";
                    if (s != ReportStructure[i-1].Title)
                    {
                        e = true;
                        oSheet.Cells(1, 1).Interior.Color = 13421823;
                        oSheet.Cells(1, 1).Font.Color = -16776961;
                        oSheet.Cells(1, i).Interior.Color = 0;
                        oSheet.Cells(1, i).Font.Color = -16776961;
                    }
                }
                if (e) MsgBox("Заголовки столбцов не соответствуют ожидаемым.", "Ошибка", MessageBoxIcon.Warning);
                return !e;
            }
      for (int i = 1; i <= ReportStructure.Count(); i++)
      {// MyXlsField f in FillingReportStructure)
        MyProgressUpdate(pb, 10 + i * .5, "Проверка заголовков");
        s = oSheet.Cells(1, i).Value?.ToString() ?? "";
        if (s != ReportStructure[i - 1].Title)
        {
          e = true;
          oSheet.Cells(1, 1).Interior.Color = 13421823;
          oSheet.Cells(1, 1).Font.Color = -16776961;
          oSheet.Cells(1, i).Interior.Color = 0;
          oSheet.Cells(1, i).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Заголовки столбцов не соответствуют ожидаемым.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    /*public static bool MyExcelImport_CheckValues(List<List<object>> ReportData, List<MyXlsField> ReportStructure, dynamic oSheet, object pb, int pbFromPercent=0)
    {
      string s;
      long rl; decimal rd; bool b;
      bool e = false;

      for (int r = 0; r < ReportData.Count; r++)
      {
        if(pb!=null) MyProgressUpdate(pb, pbFromPercent + 10 * r / ReportData.Count, "Проверка данных");
        for (int c = 1; c < ReportStructure.Count() + 1; c++)
        {
          // если можно пустое, то пустое, если непустое -- проверяем корректность типа
          b = true;
          s = ReportData[r][c].ToString();//  oSheet.Cells(r, c).Value?.ToString() ?? "";
          if (s == "" && !ReportStructure[c - 1].Nulable) b = false;
          else if (s != "")
          {
            // Можно добавить дату. Упрощать не надо!
            if (ReportStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            if (ReportStructure[c - 1].DataType == "decimal") b = decimal.TryParse(s, out rd) && rd > 0;
            if (ReportStructure[c - 1].DataType == "date") b = oSheet.Cells(r, c).Value.GetType().Name == "DateTime";
          }
          if (!b)
          {
            e = true;
            oSheet.Cells(r + 2, 1).Interior.Color = xlPink;
            oSheet.Cells(r + 2, 1).Font.Color = xlRed;
            oSheet.Cells(r + 2, c).Interior.Color = xlDimGray;
            oSheet.Cells(r + 2, c).Font.Color = xlRed;
          }
        }
      }
      if (e) MsgBox("Не заданы корректные значения для обязательных столбцов.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }*/

    public static bool MyExcelImport_CheckValues(dynamic oSheet, List<MyXlsField> ReportStructure, object pb)
    {
      string s;
      long rl; decimal rd; bool b;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      //int c = range.Columns.Count;
      MyProgressUpdate(pb, 20, "Проверка данных");
      if (rows > 1000)
      {
        if (MessageBox.Show("Файл содержит " + rows.ToString() + " строк. Продолжить?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        {
          return false;
        }
      }

      if (rows == 1)
      {
        MsgBox("Файл нужно заполнить.");
        return false;
      }

      //oSheet.Cells.Interior.Color = xlWhite;
      //oSheet.Cells.Font.Color = xlBlack;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 20 + 10 * r / rows, "Проверка данных");
        for (int c = 1; c < ReportStructure.Count() + 1; c++)
        {
          if (ReportStructure[c - 1].SkipOnLoad) continue;
          // если можно пустое, то пустое, если непустое -- проверяем корректность типа
          b = true;
          s = oSheet.Cells(r, c).Value?.ToString() ?? ""; //считываем значение из клетки, если ничего нет то 
          if (s == "" && !ReportStructure[c - 1].Nulable) b = false;
          else if (s != "")
          {
            // Можно добавить дату. Упрощать не надо!
            if (ReportStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            else if (ReportStructure[c - 1].DataType == "decimal")// b = decimal.TryParse(s, out rd) && rd > 0;
            {
              if (ReportStructure[c - 1].Subzero) b = decimal.TryParse(s, out rd);
              else b = decimal.TryParse(s, out rd) && rd > 0;
            }
            else if (ReportStructure[c - 1].DataType == "InvCfmType")
            {
                if(((oSheet.Cells(r, c).Value.ToString()) == "КП") || ((oSheet.Cells(r, c).Value.ToString()) == "СЧЕТ"))
                {
                    b = true;
                }
                else b = false;
            }
            else if (ReportStructure[c - 1].DataType == "date")
            {
              if (oSheet.Cells(r, c).Value.GetType().Name == "DateTime") b = true;
              else if(oSheet.Cells(r, c).Value.GetType().Name == "String") {
                DateTime dt = DateTime.MinValue;
                b = DateTime.TryParse(oSheet.Cells(r, c).Value.ToString(), out dt);
              }
              else b = false;
            }
            else if (ReportStructure[c - 1].DataType == "vals") b = ReportStructure[c - 1].Vals.Contains(s);
          }
          if (!b)
          {
            e = true;
            oSheet.Cells(r, 1).Interior.Color = xlPink;
            oSheet.Cells(r, 1).Font.Color = xlRed;
            oSheet.Cells(r, c).Interior.Color = xlDimGray;
            oSheet.Cells(r, c).Font.Color = xlRed;
          }
        }
      }
      if (e) MsgBox("Не заданы корректные значения для обязательных столбцов.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    public static bool MyExcel_ValueIsDate(dynamic oCell)
    {
      if (oCell.Value == null) return false;
      if (oCell.Value.GetType().Name == "DateTime") return true;
      else if (oCell.Value.GetType().Name == "String")
      {
        DateTime dt = DateTime.MinValue;
        bool b = DateTime.TryParse(oCell.Value.ToString(), out dt);
        return b;
      }
      else return false;
    }

    public static List<MyXlsField> FillReportData(string sReportName) 
    {
      List<MyXlsField> FillingReportStructure = new List<MyXlsField>();
      switch (sReportName)
      {
        case "Spec":
          FillingReportStructure.Add(new MyXlsField("SId", "ID спец.", "long"));
          FillingReportStructure.Add(new MyXlsField("SSystem", "Наименование работ", "string", false));
          FillingReportStructure.Add(new MyXlsField("SStation", "Станция", "string", false));
          FillingReportStructure.Add(new MyXlsField("curator", "Куратор", "string", true));//-
          FillingReportStructure.Add(new MyXlsField("SContractNum", "№ договора", "string", true));//yjdj
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false)); // исправить "_" на "/", в папках наоборот
          FillingReportStructure.Add(new MyXlsField("STName", "Тип шифра", "vals", false, false, new string[] { "Электрика", "Сантехника", "Слаботочка", "Участок № 5", "Затворы", "Демонтаж", "ПНР" }));
          FillingReportStructure.Add(new MyXlsField("SExecutor", "Исполнитель", "string", true));
          FillingReportStructure.Add(new MyXlsField("SArea", "Участок строительства", "string", false));
          FillingReportStructure.Add(new MyXlsField("SNo", "Этап строительства", "string", false));
          FillingReportStructure.Add(new MyXlsField("SVNo", "Версия", "string", false));

          FillingReportStructure.Add(new MyXlsField("SVStage", "Стадия проектной документации", "string", false)); // add
          FillingReportStructure.Add(new MyXlsField("SVProjectSignDate", "Дата подписания версии проектным институтом", "date", true));//-
          FillingReportStructure.Add(new MyXlsField("SVProjectBy", "Проектный институт", "string", true));//-
          FillingReportStructure.Add(new MyXlsField("SSubDocNum", "№ договора суб", "string",true));
          FillingReportStructure.Add(new MyXlsField("SVDate", "Дата поступления версии", "date", true));//-

          FillingReportStructure.Add(new MyXlsField("SComment", "Комментарий", "string", true));
          break;
                case "SubContract":
                    FillingReportStructure.Add(new MyXlsField("SId", "ID спец.", "long"));
                    FillingReportStructure.Add(new MyXlsField("SSystem", "Наименование работ", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SStation", "Станция", "string", false));
                    FillingReportStructure.Add(new MyXlsField("curator", "Куратор", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));//------------
                    FillingReportStructure.Add(new MyXlsField("SArea", "Участок строительства", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SubContractId", "ID договора", "long"));
                    FillingReportStructure.Add(new MyXlsField("SubName", "Наименование субподрядчика", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SubINN", "ИНН субподрядчика", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SubContractNum", "№ договора СМУ-24-Субподрятчик", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SubContractDate", "Дата договора СМУ-24-Субподрятчик", "date", true));
                    FillingReportStructure.Add(new MyXlsField("SubDownKoefSMR", "Понижающий к СМР суб", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("SubDownKoefPNR", "Понижающий к ПНР суб", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("SubDownKoefTMC", "Понижающий к ТМЦ суб", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("SubContractAprPriceWOVAT", "Приблизительная цена договора без НДС", "decimal", true));
                    break;
                case "Budg":
                    FillingReportStructure.Add(new MyXlsField("SId", "ID спец.", "long"));
                    FillingReportStructure.Add(new MyXlsField("SSystem", "Наименование работ", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SStation", "Станция", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("curator", "Куратор", "string", skip_on_load: true));//-
                    FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", skip_on_load: true)); // исправить "_" на "/", в папках наоборот
                    FillingReportStructure.Add(new MyXlsField("STName", "Тип шифра", "vals", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SExecutor", "Исполнитель", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SVNo", "Версия", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SVStage", "Стадия проектной документации", "string", skip_on_load: true)); // add
                    FillingReportStructure.Add(new MyXlsField("SComment", "Комментарий", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("BId", "ID сметы", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SBudgetSMRorPNR", "СМР/ПНР", "vals", true, vals: new string[] {"СМР", "ПНР"}));
                    FillingReportStructure.Add(new MyXlsField("SBudgetNumber", "Номер сметы", "string", true));
                    FillingReportStructure.Add(new MyXlsField("BByVer", "Смета по изм. проекта", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SBudgetMIPRegNum", "Рег. Номер сметы от МИП", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SBudgetVer", "Изм по смете", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SBudgetStage", "Стадия сметы", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SBudgetCostWOVAT", "Сметная стоимость без НДС", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SVDate", "Дата выдачи субподрядчику / на участок", "date", true));
                    FillingReportStructure.Add(new MyXlsField("SBudgetComm", "Комментарий по смете", "string", true));
                    break;
                case "Invoice":
          FillingReportStructure.Add(new MyXlsField("OId", "Строка заявки", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("O1sId", "№ планирования", "long", false));
          FillingReportStructure.Add(new MyXlsField("ONo", "№ п/п по заявке 1С", "long", false));
          FillingReportStructure.Add(new MyXlsField("OArt", "Артикул 1С", "string", false));
          FillingReportStructure.Add(new MyXlsField("OName", "Наименование 1С", "string", false));
          FillingReportStructure.Add(new MyXlsField("OUnit", "Ед. изм. 1С", "string", false));
          FillingReportStructure.Add(new MyXlsField("OQty", "Кол-во 1С", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("OInvINN", "ИНН", "string", false));
          FillingReportStructure.Add(new MyXlsField("OInvNo", "Номер счета", "string", false));
          FillingReportStructure.Add(new MyXlsField("OInvDate", "Дата счета", "date", false));
          FillingReportStructure.Add(new MyXlsField("OInvNo2", "№ п/п в счете", "string", false));
          FillingReportStructure.Add(new MyXlsField("OInvName", "Наименование", "string", false));
          FillingReportStructure.Add(new MyXlsField("OInvUnit", "Ед. изм.", "string", false));
          FillingReportStructure.Add(new MyXlsField("OInvK", "К перевода", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("OInvQty", "Кол-во", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("OInvPrc", "Цена за ед.", "decimal", false));
          break;
        case "SpecFilling_add":
          //FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long"));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFType", "Вид по спецификации", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFCode", "Код оборудования, изделия, материала", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMaker", "Завод-изготовитель", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQty", "Количество", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("SFUnitWeight", "Масса единицы, кг", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFNote", "Примечание", "string"));
          FillingReportStructure.Add(new MyXlsField("SFDocs", "Вид документа для ИД", "string"));
          break;
        case "SpecFilling":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long"));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFType", "Вид по спецификации", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFCode", "Код оборудования, изделия, материала", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMaker", "Завод-изготовитель", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQty", "Количество", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("SFUnitWeight", "Масса единицы, кг", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFNote", "Примечание", "string"));
          FillingReportStructure.Add(new MyXlsField("SFDocs", "Вид документа для ИД", "string"));
          FillingReportStructure.Add(new MyXlsField("SFSupplyPID", "PID", "decimal", false, true));
          FillingReportStructure.Add(new MyXlsField("SFSupplyPreType", "Чьи материалы", "vals", false, false, new string[] { "нет" , "заказчик" , "подрядчик", "не изменять" })); // 0=нет, 1=закупка (buy), 2=давал (gnt)
          break;
        case "Curator":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long"));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQty", "К-во всего", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во (исп.)", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("EName", "Исполнитель", "string"));
          break;
        case "Done":
          FillingReportStructure.Add(new MyXlsField("SFEId", "ID задачи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string"));
          FillingReportStructure.Add(new MyXlsField("EName", "Исполнитель", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во", "decimal"));
          FillingReportStructure.Add(new MyXlsField("DSumQty", "в т.ч. ранее", "decimal"));
          FillingReportStructure.Add(new MyXlsField("DRestQty", "к выполнению", "decimal"));
          FillingReportStructure.Add(new MyXlsField("DQty", "К-во выполнено", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("DDate", "Дата выполнения", "date", false));
          break;
        case "InvCfm":
          FillingReportStructure.Add(new MyXlsField("sf.SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("sf.SFSubcode", "Шифр по спецификации", "string",true,false,null,true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo", "№ п/п", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo2", "№ п/п 2", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFName", "Наименование и техническая характеристика", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFMark", "Тип, марка, обозначение документа", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFUnit", "Единица измерения", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFQtyBuy", "К-во", "decimal", true, false, null, true));
          //блок новых
          FillingReportStructure.Add(new MyXlsField("e.ename", "Исполнитель", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("ICId", "ID строки счета", "long"));
          FillingReportStructure.Add(new MyXlsField("so.SOResponsOS", "Ответственный ОС", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("so.SOOrderNum", "№ заявки от участка/субчика", "string", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("so.SOOrderDate", "Дата заявки", "date", true));//
          FillingReportStructure.Add(new MyXlsField("WishDates", "Желаемая дата поставки", "fake"));//
          FillingReportStructure.Add(new MyXlsField("Qties", "К-во заказано", "fake"));//
          FillingReportStructure.Add(new MyXlsField("so.SOPlan1CNum", "№ планирования 1С", "string", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("so.SO1CPlanDate", "Дата планирования 1С", "date"));//
          FillingReportStructure.Add(new MyXlsField("IC1SOrderNo", "№ заявки 1С", "string", true));
          FillingReportStructure.Add(new MyXlsField("SFSupplyDate1C", "Дата заявки 1С", "date"));//
          FillingReportStructure.Add(new MyXlsField("ICINN", "ИНН юр. лица по счету", "long",true));
          FillingReportStructure.Add(new MyXlsField("SFLegalName", "Наименование организации", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SFDocType", "Вид документа (КП, счет)", "InvCfmType", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("ICNo", "№ счета", "string",true));
          FillingReportStructure.Add(new MyXlsField("ICDate", "Дата счета", "date", true));
          FillingReportStructure.Add(new MyXlsField("ICRowNo", "№ п/п счета", "string", true));
          FillingReportStructure.Add(new MyXlsField("ICName", "Наименование по счету", "string", true));
          FillingReportStructure.Add(new MyXlsField("ICUnit", "Ед.изм.", "string", true));
          FillingReportStructure.Add(new MyXlsField("ICQty", "К-во", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("ICPrc", "Цена за 1 ед. без НДС", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("ICK", "К перевода в ед. спец.", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("SFDaysUntilSupply", "Срок поставки в днях", "long", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("so.SOComment", "Комментарий", "string", true, false, null, true));//
          
          break;
        case "SupplyOrder":
          FillingReportStructure.Add(new MyXlsField("sf.SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("sf.SFSubcode", "Шифр по спецификации", "string",true,false,null,true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo", "№ п/п", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo2", "№ п/п 2", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFName", "Наименование и техническая характеристика", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFMark", "Тип, марка, обозначение документа", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFUnit", "Единица измерения", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("coalesce(SF.SFQtyBuy, SF.SFQtyGnT) as QtyBuy", "К-во", "decimal", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("e.ename", "Исполнитель", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOId", "ID заявки", "long"));
          FillingReportStructure.Add(new MyXlsField("SF.SFSupplyPID", "PID", "long", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END SOSupplierType", "Чья поставка", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOResponsOS", "Ответственный ОС", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOOrderNum", "№ заявки от участка/субчика", "string", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("SOOrderDate", "Дата заявки", "date", true));//
          FillingReportStructure.Add(new MyXlsField("SOWishDates", "Желаемая дата поставки", "fake"));//
          FillingReportStructure.Add(new MyXlsField("SOQties", "К-во заказано", "fake"));//
          FillingReportStructure.Add(new MyXlsField("SOPlan1CNum", "№ планирования 1С / письма в ТСК", "string", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("SO1CPlanDate", "Дата планирования 1С / письма в ТСК", "date", true));
          FillingReportStructure.Add(new MyXlsField("SOComment", "Комментарий", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOOrderNumPref", "Постфикс поставки", "string", true, false, null, true));//
          break;
        case "BoL":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFType", "Вид по спецификации", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFCode", "Код оборудования, изделия, материала", "string"));
          //FillingReportStructure.Add(new MyXlsField("SFMaker", "Завод-изготовитель", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy", "Количество", "decimal", false));
                    //FillingReportStructure.Add(new MyXlsField("SFUnitWeight", "Масса единицы, кг", "decimal"));
                    //FillingReportStructure.Add(new MyXlsField("SFNote", "Примечание", "string"));
                    //FillingReportStructure.Add(new MyXlsField("SFDocs", "Вид документа для ИД", "string"));
                    //FillingReportStructure.Add(new MyXlsField("SFSupplyPID", "PID", "decimal", false, true));
                    //FillingReportStructure.Add(new MyXlsField("'подрядчик' f1", "Чьи материалы", "vals", false, false, new string[] { "подрядчик" })); // 0=нет, 1=закупка (buy), 2=давал (gnt)
                    FillingReportStructure.Add(new MyXlsField("ic.SFPlan1CNum", "№ планирования 1с", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.IC1SOrderNo", "№ заявки 1С", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICINN", "ИНН юр. лица по счету", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.SFLegalName", "Наименование организации", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.SFDocType", "Вид документа (КП, счет)", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICNo", "№ счета/КП", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICDate", "Дата счета/КП", "date"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICRowNo", "№ п/п счета/КП", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICName", "Наименование по счету/КП", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICUnit", "Ед.изм.", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICQty", "К-во", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICPrc", "Цена за 1 ед. без НДС", "string"));
                    FillingReportStructure.Add(new MyXlsField("ic.ICK", "К перевода в ед. спец.", "string"));
          FillingReportStructure.Add(new MyXlsField("BoLQtySum", "Поставлено ранее", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy-(IsNull(BoLQtySum,0)) BRestQty", "Осталось поставить", "decimal"));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBBolNoForTSK", "№ УПД для ТСК", "string", true));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBBoLDateForTSK", "Дата УПД для ТСК", "date", true));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBNoForTSK", "№ п/п в УПД для ТСК", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBUnitForTSK", "Ед. изм. по УПД для ТСК", "string", true));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBQtyForTSK", "К-во по УПД для ТСК", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBRecipient", "Кто получил", "string", true));
                    FillingReportStructure.Add(new MyXlsField("sfb.SFBShipmentPlace", "Место отгрузки", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb.SFBBoLNoFromTSK", "№ УПД от ТСК", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb.SFBBoLDateFromTSK", "Дата УПД от ТСК", "date", true));
          FillingReportStructure.Add(new MyXlsField("sfb.SFBNoFromTSK", "№ п/п в УПД от ТСК", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("sfb.SFBUnitFromTSK", "Ед. изм. по УПД от ТСК", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb.SFBQtyFromTSK", "К-во по УПД от ТСК", "decimal", true));
          break;
        case "SupplyDate":
          FillingReportStructure.Add(new MyXlsField("SFEId", "ID работы", "long", false));
          FillingReportStructure.Add(new MyXlsField("SFEOId", "ID з. на пост.", "long"));
          //FillingReportStructure.Add(new MyXlsField("SFEOId", "ID з. на пост.", "long"));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string"));
          FillingReportStructure.Add(new MyXlsField("EName", "Исполнитель", "string"));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во всего", "string"));
          FillingReportStructure.Add(new MyXlsField("SFEOQty", "К-во требуется", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFEOStartDate", "Дата начала работ", "date", false));
          break;
        case "SupplyBuy":
          FillingReportStructure.Add(new MyXlsField("SFEOId", "ID з. на пост.", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string"));
          FillingReportStructure.Add(new MyXlsField("EName", "Исполнитель", "string"));
          FillingReportStructure.Add(new MyXlsField("QtyToOrder", "К-во требуется", "decimal", false)); //тут надо будет поправить vwOrder на usp
          FillingReportStructure.Add(new MyXlsField("SFEOStartDate", "Дата начала работ", "date", false));
          FillingReportStructure.Add(new MyXlsField("O1sId", "№ планирования", "long", false));
          FillingReportStructure.Add(new MyXlsField("ONo", "№ п/п по заявке 1С", "long", false));
          FillingReportStructure.Add(new MyXlsField("OArt", "Артикул 1С", "string", false));
          FillingReportStructure.Add(new MyXlsField("OName", "Наименование 1С", "string", false));
          FillingReportStructure.Add(new MyXlsField("OUnit", "Ед.изм. 1С", "string", false));
          FillingReportStructure.Add(new MyXlsField("OK", "К перевода", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("OQty", "Кол-во 1С", "decimal", false));          
          break;
        case "SupplyRMTypes":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string"));
          FillingReportStructure.Add(new MyXlsField("SFQty", "К-во", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyGnT", "Давальч.", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy", "Закуп.", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyWarehouse", "Склад", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyWorkshop", "Закуп. суб/п", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtySub", "Закуп. суб/п", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFSupplyPID", "PID", "long"));
          break;
        case "Budget":
                    FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", skip_on_load: true));
                     FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", skip_on_load: true));
                     FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", skip_on_load: true));
                     FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", skip_on_load: true));
                              FillingReportStructure.Add(new MyXlsField("SFHId", "ID записи", "long", skip_on_load: true));
                              FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string", skip_on_load: true));
                              FillingReportStructure.Add(new MyXlsField("e.EName", "Исполнитель", "long", skip_on_load: true));
                              FillingReportStructure.Add(new MyXlsField("SFBudgetType", "Вид по спецификации", "long", skip_on_load: true));
                              FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("SFBudgetType", "Вид по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudget", "Номер сметы", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetNo", "№ по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetSmrNo", "№ по СМР", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetCode", "Шифр расценки и коды ресурсов", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetName", "Наименование по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetUnit", "Ед. изм. по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetK", "К перевода", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetQty", "Кол-во по смете", "decimal", true, true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetPrc", "Цена сметная за ед. без НДС", "decimal", true, true));
          break;

      }
      return FillingReportStructure;
    }

    ///// <summary>
    ///// Returns Excel
    ///// </summary>
    //public static object MyExcelInput(string sFile="")
    //  ///

    //  ///
    //   * 
    //   */
          //{
          //  if (sFile == "") return null;
          //  if (sFile == "") return null;

          //  return null;
          //}

    private static Size GetTrueScreenSize(Screen screen)
    {
      //get system DPI
      var systemDPI = (int)Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LogPixels", 96);

      using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
      {
        //get graphics DPI
        var dpiX = graphics.DpiX;
        var dpiY = graphics.DpiY;

        //get true screen size
        var w = (int)Math.Round(screen.Bounds.Width * systemDPI / dpiX);
        var h = (int)Math.Round(screen.Bounds.Height * systemDPI / dpiY);

        return new Size(w, h);
      }
    }

    public static bool MyMakeScreenshot(string FullPath="")
    {
      //get true screen size
      var size = GetTrueScreenSize(Screen.PrimaryScreen);

      //create bitmap and make screenshot
      var bmp = new Bitmap(size.Width, size.Height);
      using (var graph = Graphics.FromImage(bmp))
        graph.CopyFromScreen(0, 0, 0, 0, size);

      //save
      bmp.Save(FullPath, ImageFormat.Png);
      return true;
    }

    public static string xlsCharByNum(long ColNum)
    {
      string s1 = "";
      string s2 = "";
      long i1;
      long i2;
      if (ColNum > 26)
      {
        i2 = ColNum % 26;
        i1 = (ColNum - i2) / 26;
        if (i2 > 0)
        {
          s2 = ((char)(64 + i2)).ToString();
          s1 = ((char)(64 + i1)).ToString();
        }
        else
        {
          s2 = ((char)(64 + 26)).ToString();
          s1 = ((char)(64 + i1-1)).ToString();
        }
      }
      else s2 = ((char)(64 + ColNum)).ToString();
      return s1 + s2;
    }

    public static void MyExcelUnmerge(dynamic oSheet)
    {
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int cols = range.Columns.Count;
      object Address;

      for (int r=1; r<=rows; r++)
      {
        for (int c=1; c<=cols; c++)
        {
          if(oSheet.Cells(r, c).MergeCells)
          {
            Address = oSheet.Cells(r, c).MergeArea.Address;
            oSheet.Cells(r, c).UnMerge();
            oSheet.Range(Address).Value = oSheet.Cells(r, c).Value;
          }
        }
      }
    }

    public static void MyFormatExcelTable(dynamic oSheet, long RowCount, long ColCount, int TitleRowsWidthN=1, decimal[] colsWidth = null, int[] GrayColIDs = null, bool nowrap=false, List<int> DateValueColIDs=null, string budg = "", int[] CenterColIDs = null)
    {
      if(oSheet == null || RowCount < 1 || ColCount < 1) return;

      string LastCol = xlsCharByNum(ColCount);
      string s;
      DateTime dt;
      if(budg == "budg")
            {
                oSheet.Range("A1:" + LastCol + RowCount).Formula = oSheet.Range("A1:" + LastCol + RowCount).Value;

                oSheet.Rows(1).Font.Bold = true;
                oSheet.Rows(1).HorizontalAlignment = xlCenter;
                //oSheet.Rows(1).XlSortOn.xlSortOnValues;
                // Заголовок
                oSheet.Range("A1:" + LastCol + 1).Interior.color = 13237908;
                oSheet.Range("A1:" + LastCol + 1).Font.color = 0;
                oSheet.Range("A1:" + LastCol + 1).AutoFilter();

                xlsBorders(oSheet.Range("A1:" + LastCol + RowCount), true);

                //1 строка, нижняя граница
                oSheet.Range("A1:" + LastCol + 1).Borders(xlEdgeBottom).Weight = xlThin;

                //Перенос по словам
                if (!nowrap) oSheet.Cells.WrapText = true;

                //Возвращаем даты из строк, если есть
                if (DateValueColIDs != null && DateValueColIDs.Count > 0 && RowCount > 1)
                {
                    for (int col = 0; col < DateValueColIDs.Count; col++)
                    {
                        int c = DateValueColIDs[col] + 1; // excel starts naming from 1
                        for (int r = 2; r < RowCount + 1; r++)
                        {
                            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
                            if (s != "")
                            {
                                dt = DateTime.Parse(s);
                                oSheet.Cells(r, c).Value = dt;
                            }
                        }
                    }
                }

                string state;
                for(int i = 2; i < RowCount; i++)
                {
                    state = oSheet.Cells(i, 17).Value?.ToString() ?? "";
                    if (state == "АННУЛИРОВАНА")
                        oSheet.Rows(i).Interior.color = 14277081;
                }
                oSheet.Columns(18).HorizontalAlignment = xlRight;
                oSheet.Rows(1).HorizontalAlignment = xlCenter;

                oSheet.Range("R2:R" + RowCount).NumberFormat = "0,00";
                //Автоподбор ширины и высоты, автофильтр
                if (colsWidth != null)
                {
                    for (int c = 1; c <= colsWidth.Length; c++)
                    {
                        oSheet.Columns(xlsCharByNum(c)).ColumnWidth = colsWidth[c - 1];
                    }
                }
                else oSheet.Columns("A:" + LastCol).EntireColumn.AutoFit();

                if (GrayColIDs != null)
                {
                    foreach (int i in GrayColIDs)
                    {
                        if (RowCount > 1) oSheet.Range(xlsCharByNum(i) + "2:" + xlsCharByNum(i) + RowCount).Interior.color = 14277081;
                        oSheet.Range(xlsCharByNum(i) + "1:" + xlsCharByNum(i) + "1").Font.color = 8421504;
                    }
                }

                if (CenterColIDs != null)
                {
                    foreach (int i in CenterColIDs)
                    {
                        if (RowCount > 1) oSheet.Range(xlsCharByNum(i) + "2:" + xlsCharByNum(i) + RowCount).HorizontalAlignment = xlCenter;
                        //oSheet.Range(xlsCharByNum(i) + "1:" + xlsCharByNum(i) + "1").Font.color = 8421504;

                    }
                    //oSheet.Range("")
                }

                if (TitleRowsWidthN > 1)
                {
                    oSheet.Rows(1).RowHeight = TitleRowsWidthN * 15;//M
                    oSheet.Rows("2:" + RowCount).EntireRow.AutoFit();
                }
                else oSheet.Rows("1:" + RowCount).EntireRow.AutoFit();

                //oSheet.Rows(1).AutoFilter();

                //фиксируем 1 строку
                oSheet.Parent.Parent.ActiveWindow.SplitRow = 1;
                oSheet.Parent.Parent.ActiveWindow.FreezePanes = true;
                return;
            }
      // форматы по умолчанию
      oSheet.Range("A1:" + LastCol + RowCount).Formula = oSheet.Range("A1:" + LastCol + RowCount).Value;

      oSheet.Rows(1).Font.Bold = true;
      oSheet.Rows(1).HorizontalAlignment = xlCenter;
      // Заголовок
      oSheet.Range("A1:" + LastCol + 1).Interior.color = 4210752;
      oSheet.Range("A1:" + LastCol + 1).Font.color = 16777215;

      xlsBorders(oSheet.Range("A1:" + LastCol + RowCount),true);

      //1 строка, нижняя граница
      oSheet.Range("A1:" + LastCol + 1).Borders(xlEdgeBottom).Weight = xlThin;

      //Перенос по словам
      if (!nowrap) oSheet.Cells.WrapText = true;

      //Возвращаем даты из строк, если есть
      if (DateValueColIDs != null && DateValueColIDs.Count > 0 && RowCount > 1)
      {
        for (int col = 0; col < DateValueColIDs.Count; col++)
        {
          int c = DateValueColIDs[col]+1; // excel starts naming from 1
          for (int r = 2; r < RowCount+1; r++)
          {
            s = oSheet.Cells(r, c).Value?.ToString() ?? "";
            if (s != "")
            {
              dt = DateTime.Parse(s);
              oSheet.Cells(r, c).Value = dt;
            }
          }
        }
      }

      //Автоподбор ширины и высоты, автофильтр
      if (colsWidth != null)
      {
        for(int c = 1; c <= colsWidth.Length; c++)
        {
          oSheet.Columns(xlsCharByNum(c)).ColumnWidth = colsWidth[c-1];
        }
      }
      else oSheet.Columns("A:" + LastCol).EntireColumn.AutoFit();

      if (GrayColIDs != null)
      {
        foreach (int i in GrayColIDs)
        {
          if(RowCount>1) oSheet.Range(xlsCharByNum(i) + "2:" + xlsCharByNum(i) + RowCount).Interior.color = 14277081;
          oSheet.Range(xlsCharByNum(i) + "1:"+xlsCharByNum(i)+ "1").Font.color = 8421504;
        }
      }

      if (TitleRowsWidthN > 1)
      {
        oSheet.Rows(1).RowHeight = TitleRowsWidthN * 15;//M
        oSheet.Rows("2:" + RowCount).EntireRow.AutoFit();
      }
      else oSheet.Rows("1:" + RowCount).EntireRow.AutoFit();

      //oSheet.Rows(1).AutoFilter();

      //фиксируем 1 строку
      oSheet.Parent.Parent.ActiveWindow.SplitRow = 1;
      oSheet.Parent.Parent.ActiveWindow.FreezePanes = true;
    }
    

    //public static string xlsCharByNum(long ColNum)
    //{
    //  char s1;
    //  char s2;
    //  long i1;
    //  long i2;
    //  if (ColNum > 26)
    //  {
    //    i2 = ColNum % 26;
    //    i1 = ColNum / 26;
    //    if (i2 > 0)
    //    {
    //      s2 = (char)(64 + i2);
    //      s1 = (char)(64 + i1);
    //    }
    //    else
    //    {
    //      s2 = (char)(64 + 26);
    //      s1 = (char)(64 + i1 - 1);
    //    }
    //    string ret = s1.ToString() + s2.ToString();
    //    return ret;
    //  }
    //  else
    //  {
    //    s2 = (char)(64 + ColNum);
    //    return s2.ToString();
    //  }
    //}

    public static void  xlsBorders(dynamic oRange , bool bInsideDotted = false)
    {
      if (oRange == null) return;
      oRange.Borders(xlEdgeTop).Weight = xlThin;
      oRange.Borders(xlEdgeBottom).Weight = xlThin;
      oRange.Borders(xlEdgeLeft).Weight = xlThin;
      oRange.Borders(xlEdgeRight).Weight = xlThin;
      oRange.Borders(xlInsideVertical).Weight = bInsideDotted? xlHairline : xlThin;
      oRange.Borders(xlInsideHorizontal).Weight = bInsideDotted? xlHairline: xlThin;
      oRange.VerticalAlignment = xlCenter;
      return;
    }

    public static void MyExcel(List<MyExportExcel> reports_data)
    {
      MyProgress pw = new MyProgress();
      pw.MyExportExcelList = reports_data;
      pw.ShowInTaskbar = true;
      pw.ShowDialog();
    }

    public static void MyExcel(string sQuery, List<MyXlsField> ReportFields, bool Title2Rows = false, decimal[] colsWidth = null, int[] GrayColIDs = null, bool nowrap = false, string budg = "", int[] CenterColIDs = null)
    {
      if (ReportFields == null)
      {
        MyExcelIns(sQuery, null, Title2Rows, colsWidth, GrayColIDs, nowrap, null);
        return;
      }

      List<string> tt = new List<string>();
      List<int> dd = new List<int>();
      for (int i=0;i< ReportFields.Count();i++)
      {
        tt.Add(ReportFields[i].Title);
        if (ReportFields[i].DataType == "date") dd.Add(i);
      }

      MyExcelIns(sQuery, tt.ToArray(), Title2Rows, colsWidth, GrayColIDs, nowrap,dd, budg:budg, CenterColIDs: CenterColIDs);
      return;
    }

    public static void MyExcelIns(string sQuery, string[] ssTitle = null, bool Title2Rows = false, decimal[] colsWidth=null, int[]GrayColIDs=null, bool nowrap=false, List<int> DateValueColIDs = null, string budg = "", int[] CenterColIDs = null)
    {
      MyExportExcel mee = new MyExportExcel();
      mee.sQuery = sQuery;
      mee.ssTitle = ssTitle;
      mee.Title2Rows = Title2Rows;
      mee.colsWidth = colsWidth;
      mee.GrayColIDs = GrayColIDs;
      mee.nowrap = nowrap;
      mee.DateValueColIDs = DateValueColIDs;
            mee.AfterFormat = budg;
            mee.CenterColIDs = CenterColIDs;

      MyProgress pw = new MyProgress();
      pw.MyExportExcelList.Add(mee);
      pw.ShowInTaskbar = true;
      pw.ShowDialog();

      

    //  Public Function MyReportExcelSimple(ByVal sQuery As String, ByVal ssTitles As String(), _

    //                           Optional ByVal sName As String = "", Optional ByVal bShowProcess As Boolean = False, _

    //                           Optional ByVal sSheetName As String = "", Optional ByVal SaveToFolder As String = "", Optional ByVal AddPivot As String = "", _

    //                           Optional ByVal pb As Object = Nothing) As String

    //If SaveToFolder<> "" And Not My.Computer.FileSystem.DirectoryExists(SaveToFolder) Then
    // Return "Папка " & SaveToFolder & " не существует или недоступна."
    //End If

    //MyProgressUpdate(pb, 10, "Выгрузка...")

    //Cursor.Current = Cursors.WaitCursor
    //Application.DoEvents()

    //MyReportExcelSimple = ""

    //'  Dim sQuery As String = " select vwReportLotStatusLast.* From vwReportLotStatusLast Inner join " & _
    //'" (Select LId l_id, Max(s_order) max_order from vwReportLotStatusLast Group By LId)sss On LId=l_id And s_order=max_order " & _
    //'" where LYear=" & iYear & " Order By id"

    //Using con As New Data.SqlClient.SqlConnection(constr)
    //  Try
    //    con.Open() ' соединились с сервером
    //  Catch ex As Exception
    //    MyReportExcelSimple = "ОШИБКА" & vbNewLine & ex.Message ' нифига
    //    Exit Function
    //  End Try

    //  'Using com As New Data.SqlClient.SqlCommand(sQuery, con)
    //  'Try
    //  'Using r = com.ExecuteReader()
    //  'If Not r.HasRows Then
    //  ' MySimpleReportExcel = "Записей не найдено."
    //  'Else
    //  Dim oApp As Object
    //  Dim oBook As Object
    //  Dim oSheet As Object
    //  oApp = CreateObject("Excel.Application")
    //  oApp.Visible = False
    //  oApp.ScreenUpdating = False
    //  oBook = oApp.Workbooks.Add
    //  oApp.DisplayAlerts = False

    //  MyProgressUpdate(pb, 20, "Выгрузка...")

    //  Do While oBook.worksheets.count > 1
    //    oBook.worksheets(2).Delete()
    //  Loop

    //  oSheet = oBook.worksheets(1)

    //  Dim iRow As Long = 0, iColNum As Long = 0, i As Integer = 0
    //  Dim iRowStartData As Long = 0
    //  Dim iRows As Long

    //  MySendArrayToExcel(oSheet, 1, 1, ssTitles)
    //  MyProgressUpdate(pb, 40, "Выгрузка...")
    //  MySendQueryDataToExcel(sQuery, oSheet, 2, 1, iRows, , pb, 75, "Выгрузка...")
    //  iRows += 1

    //  MyFormatExcelTable(oSheet, iRows, iColNum)

    //  If sSheetName = "" Then oSheet.name = Format(Now, "yyyy-MM-dd") Else oSheet.name = Strings.Left(sSheetName, 31)

      // 2
    //  ' Pivot
    //  If AddPivot<> "" Then MySimpleReportExcel_AddPivot(oBook, AddPivot, 1, 1, iRows, iColNum)

    //  With oApp
    //    .ActiveWindow.SplitRow = 1
    //    .ActiveWindow.FreezePanes = True
    //    .ScreenUpdating = True
    //    If SaveToFolder = "" Then 'Or Not IsNothing(oSheetOut)
    //      .DisplayAlerts = True
    //      .Visible = True
    //      'If IsNothing(oSheetOut) Then
    //      .Dialogs(xlDialogSaveAs).Show(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" _
    //                                        & IIf(sName <> "", sName & " ", "") & Format(Now, "yyyy-MM-dd") & ".xlsx")
    //      'Else
    //      '  oSheetOut = oSheet
    //      'End If
    //    Else
    //      oBook.SaveAs(SaveToFolder & sName)
    //      oBook.Close()
    //      .DisplayAlerts = True
    //      oApp.Quit()
    //      oSheet = Nothing
    //      oBook = Nothing
    //      oApp = Nothing
    //      GC.Collect()
    //    End If

    //  End With
    //End Using
    //'Catch ex As System.Exception
    //'  MsgBox(ex.Message)
    //'End Try

    //MyProgressUpdate(pb, 90, "Выгрузка...")

    //Application.DoEvents()
    //Cursor.Current = Cursors.Default
    }

    public static bool MyExcelImportOpenDialog(out dynamic oExcel, out dynamic oWorksheet, string sFullPath = "")
    {

      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Filter = "MS Excel files (*.xlsx)|*.xlsx";
      ofd.RestoreDirectory = true;

      if (ofd.ShowDialog() != DialogResult.OK) { oExcel = null; oWorksheet = null; return false; }
      var f = string.Empty;
      f = ofd.FileName;

      Application.UseWaitCursor = true;
      Application.DoEvents();
      Type ExcelType = Type.GetTypeFromProgID("Excel.Application");
      oExcel = Activator.CreateInstance(ExcelType);
      oExcel.Visible = false;
      oExcel.ScreenUpdating = false;
      oExcel.DisplayAlerts = false;

      /*try
      {
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Office\\14.0\\Excel\\Security", true);
        rk.SetValue("AccessVBOM", 1, RegistryValueKind.DWord);
        rk.SetValue("Level", 1, RegistryValueKind.DWord);
        rk.SetValue("VBAWarnings", 1, RegistryValueKind.DWord);
      }
      catch
      {
        oExcel.ScreenUpdating = true;
        oExcel.DisplayAlerts = true;
        oExcel.Quit();
        oWorksheet = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        throw;
      }*/

      dynamic oBook = oExcel.Workbooks.Add();

      // макросом обходим проблему с именованным диапазоним при наличии в файле автофильтра
      var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
      var codeModule = oModule.CodeModule;
      var lineNum = codeModule.CountOfLines + 1;
      string sCode = "Public Sub myop1()\r\n";
      sCode += "  'MsgBox \"Hi from Excel\"" + "\r\n";
      sCode += "  Workbooks.Open \"" + f + "\"\r\n";
      sCode += "End Sub";

      codeModule.InsertLines(lineNum, sCode);
      oExcel.Run(oBook.Worksheets[1].Name + ".myop1");

      oExcel.Workbooks[1].Close();
      if (oExcel.Workbooks[1].Worksheets.Count > 1)
      {
        MsgBox("В книге более 1 листа.", "Ошибка", MessageBoxIcon.Warning);
        oWorksheet = null;
        oExcel.Quit();
        return false;
      }

      oWorksheet = oExcel.Workbooks[1].Worksheets(1);
      return true;
    }

    public static bool MyExcelImport_GetData(dynamic oSheet, List<MyXlsField> ReportStructure, out List<List<object>> ImportData, object pb)
    {
      ImportData = new List<List<object>>();
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      MyProgressUpdate(pb, 10, "Проверка данных");
      if (rows > 1000)
      {
        if (MessageBox.Show("Файл содержит " + rows.ToString() + " строк. Продолжить?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        {
          return false;
        }
      }

      if (rows <= 1)
      {
        MsgBox("Файл нужно заполнить.");
        return false;
      }

      string s;
      long l;
      decimal d;
      Nullable<DateTime> dt;

      for (int r = 2; r < rows + 1; r++)
      {
        List<object> datarow = new List<object>();
        MyProgressUpdate(pb, 20 + 10 * r / rows, "Считываем файл");
        for (int c = 1; c <= ReportStructure.Count; c++)
        {
          if (ReportStructure[c - 1].SkipOnLoad) continue;
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          if (s == "") datarow.Add("");
          else
          {
            switch (ReportStructure[c - 1].DataType)
            {
              case "long":
                l = long.Parse(s);
                datarow.Add(l);
                break;
              case "decimal":
                d = decimal.Parse(s);
                datarow.Add(d);
                break;
              case "string":
              case "vals":
                datarow.Add(s);
                break;
              case "date":
                dt = (DateTime)oSheet.Cells(r, c).Value;
                datarow.Add(dt);
                break;
            }
          }
        }
        ImportData.Add(datarow);
      }
      return true;
    }

        /*public static void MyExcelCustomReport(string sRoport, long sid)
        {
          MyProgress pw = new MyProgress();
          pw.MyExportExcelList = reports_data;
          pw.ShowInTaskbar = true;
          pw.ShowDialog();
          "done"
        }*/

        public static void MyExcelCuratorReport(long sid)
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "Шаблон_Куратор.xlsx";

            //создаем Excel

            Type ExcelType = MyExcelType();
            dynamic oApp;
            try { oApp = Activator.CreateInstance(ExcelType); }
            catch (Exception ex)
            {
                MsgBox("Не удалось создать экземпляр Excel.");
                TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
                return;
            }

            oApp.Visible = false;
            oApp.ScreenUpdating = false;
            oApp.DisplayAlerts = false;

            //добавляем книгу

            bool first_sheet = true;
            dynamic oBook = oApp.Workbooks.Add();
            if (first_sheet)
            {
                while (oBook.Worksheets.Count > 1) oBook.Worksheets(2).Delete();
                first_sheet = false;
            }
            else oBook.Worksheets.Add();

            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);


            /*
             ActiveSheet.PageSetup.PrintArea = "$C$1:$K$16" :: 15 + к-во строк
             ActiveSheet.VPageBreaks(1).Delete
            */

            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            /*dynamic oSheetTpl = oBookTmp.Worksheets(1);
            oBook.Worksheets.AddCopy(oSheetTpl);*/
            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select 'Шифр проекта ' + SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string sStationInfo = MyGetOneValue("select 'По системе: ' + SArea from vwSpec where SVSpec=" + sid).ToString();
            /*string qq = "select sum(total) from(select distinct header, d_ks.KSTotal as total" +
        " from SpecVer inner join (select max(svid) sv_spec_ver_max, SVSpec from SpecVer where SVSpec = " + sid + " group by SVSpec" +
            ") max_ver on sv_spec_ver_max = SVId" +
            " inner join SpecFill on SFSpecVer = sv_spec_ver_max" +
           " left join(select SFEId, SFEFill from Done inner join SpecFillExec on SFEId = DSpecExecFill group by SFEId, SFEFill" +
           ")d_done on SFId = SFEFill" +
            " left join(select dd.KSSpecFillId, concat('KC2 №', dd.KSNum, ' на сумму ', d.KSTotal) as header, dd.KSSum, d.KSTotal" +
            " from(select sum(KSSum) sumks, KSNum, KSTotal from KS2 group by KSNum, KSTotal)d left join(select KSSpecFillId, KSNum, KSSum from KS2" +
                    ")dd on dd.KSNum = d.KSNum)d_ks on d_ks.KSSpecFillId = SFId )pizda;";
            string total = MyGetOneValue(qq).ToString();*/
            
            //oSheet.Cells(5, 3).Value = "Работы выполнены по проекту: " + sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(5, 1).Value = sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(6, 1).Value = sStationInfo;

            string q = "exec CuratorReport_v5 " + sid;

            string[,] vals = MyGet2DArray(q, true);

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("9:" + (7 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A8").Resize(RowCount, ColCount).Value = vals;

            //oSheet.PageSetup.PrintArea = "$A$1:$F$" + (RowCount + 10).ToString();
            oSheet.Range("F9:H" + (RowCount + 8).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Rows(9).Select();
            oApp.ActiveWindow.FreezePanes = true;
            oSheet.Range("A1").Select();

            var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
            var codeModule = oModule.CodeModule;
            var lineNum = codeModule.CountOfLines + 1;
            string sCode = "Public Sub mypagesetup()\r\n";
            sCode += " ActiveWindow.View = xlPageBreakPreview\r\n";
            sCode += " While ActiveSheet.VPageBreaks.Count > 0\r\n";
            sCode += "  ActiveSheet.VPageBreaks(1).DragOff xlToRight, 1\r\n";
            sCode += " Wend\r\n";
            sCode += "End Sub";
            codeModule.InsertLines(lineNum, sCode);
            oApp.Run(oBook.Worksheets[1].Name + ".mypagesetup");
            codeModule.DeleteLines(1, codeModule.CountOfLines); //start, count

            if (vals != null)
            {
                oSheet.Rows(7).AutoFilter();
                oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
            }

            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            SetForegroundWindow(new IntPtr(oApp.Hwnd));
            return;
        }
        public static void MyExecKS2DocReport()
        {
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "Шаблон_накопительная_KC2.xlsx";
            //создаем Excel

            Type ExcelType = MyExcelType();
            dynamic oApp;
            try { oApp = Activator.CreateInstance(ExcelType); }
            catch (Exception ex)
            {
                MsgBox("Не удалось создать экземпляр Excel.");
                TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
                return;
            }

            oApp.Visible = false;
            oApp.ScreenUpdating = false;
            oApp.DisplayAlerts = false;

            //добавляем книгу

            bool first_sheet = true;
            dynamic oBook = oApp.Workbooks.Add();
            if (first_sheet)
            {
                while (oBook.Worksheets.Count > 1) oBook.Worksheets(2).Delete();
                first_sheet = false;
            }
            else oBook.Worksheets.Add();

            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);


            /*
             ActiveSheet.PageSetup.PrintArea = "$C$1:$K$16" :: 15 + к-во строк
             ActiveSheet.VPageBreaks(1).Delete
            */

            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            /*dynamic oSheetTpl = oBookTmp.Worksheets(1);
            oBook.Worksheets.AddCopy(oSheetTpl);*/
            oBookTmp.Close();
            System.IO.File.Delete(tmp);
            string q = " select d.KSId, vws.SContractNum, vws.SArea,d.KS2Num, b.BNumber, b.BMIPRegNum + ', вер. '+ cast(SVNo as nvarchar) as regNum, vws.SVName, vws.SSystem,"+
                        " e.EName,datename(month,d.KS2Date) + ' ' + cast(year(d.KS2Date) as nvarchar), d.KS2withKeq1, d.ZP, d.EM, d.ZPm, d.TMC, d.DTMC, d.HPotZP, d.SPotZP, d.HPandSPotZPm," +
                        " (ZP + ZPm) * 0.15 as colS, d.VZIS, d.KS2withKeq1 + ((ZP + ZPm) * 0.15) + d.VZIS as new_colV, d.KS3Num, " +
                        " (ZP + EM + HPotZP + SPotZP + HPandSPotZPm + (ZP + ZPm) * 0.15) * downKoefSMRPNR + VZIS * downKoefVZIS + TMC * downKoefTMC as colW,"+
                        " (ZP + ZPm) * downKoefSMRPNR as colX,"+
                        " d.KS3VahtNum, d.KSVahtSum, "+
                        " (ZP + EM + HPotZP + SPotZP + HPandSPotZPm + (ZP + ZPm) * 0.15) * subDownKoefSMRPNR + VZIS * downKoefVZIS + TMC * subDownKoefTMC as colAA, d.subMonth, d.KS3ImportNum, vws.SSubDocNum" +
                        " FROM KS2Doc d "+
                        " left join vwSpec vws on vws.SId = d.KSSpecId "+
                        " left join Budget b on b.BId = d.KSBudgId"+
                        " left join Executor e on e.EId = d.KSExec"+
                        " order by KSId";
            string[,] vals = MyGet2DArray(q, false);
            

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            for(int i = 0; i<RowCount; i++)
            {
                for(int j = 0; j<ColCount;j++)
                {
                    //convert to decimal
                }
            }

            if (RowCount > 1)
            {
                oSheet.Rows("6:" + (4 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A6").Resize(RowCount, ColCount).Value = vals;

            string[,] koeffs = MyGet2DArray("SELECT downKoefSMRPNR,downKoefTMC,downKoefVZIS,subDownKoefSMRPNR,subDownKoefTMC" +
                                " FROM KS2Doc order by KSId");
            int rows = RowCount + 5;
            oSheet.Range("K5:V" + (RowCount + 6).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("W5:Y" + (RowCount + 6).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("AA5:AB" + (RowCount + 6).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
            var codeModule = oModule.CodeModule;
            var lineNum = codeModule.CountOfLines + 1;
            string sCode = "Public Sub mypagesetup()\r\n";
            sCode += " ActiveWindow.View = xlPageBreakPreview\r\n";
            sCode += " While ActiveSheet.VPageBreaks.Count > 0\r\n";
            sCode += "  ActiveSheet.VPageBreaks(1).DragOff xlToRight, 1\r\n";
            sCode += " Wend\r\n";
            sCode += "End Sub";
            codeModule.InsertLines(lineNum, sCode);
            oApp.Run(oBook.Worksheets[1].Name + ".mypagesetup");
            codeModule.DeleteLines(1, codeModule.CountOfLines); //start, count

           
            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            //SetForegroundWindow(new IntPtr(oApp.Hwnd));
            return;
        }
    
        public static void MyExcelKS2Report_Done(long sid)
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "новейший_шаблон_КС-2.xlsx";

            //создаем Excel

            Type ExcelType = MyExcelType();
            dynamic oApp;
            try { oApp = Activator.CreateInstance(ExcelType); }
            catch (Exception ex)
            {
                MsgBox("Не удалось создать экземпляр Excel.");
                TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
                return;
            }

            oApp.Visible = false;
            oApp.ScreenUpdating = false;
            oApp.DisplayAlerts = false;

            //добавляем книгу

            bool first_sheet = true;
            dynamic oBook = oApp.Workbooks.Add();
            if (first_sheet)
            {
                while (oBook.Worksheets.Count > 1) oBook.Worksheets(2).Delete();
                first_sheet = false;
            }
            else oBook.Worksheets.Add();

            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);


            /*
             ActiveSheet.PageSetup.PrintArea = "$C$1:$K$16" :: 15 + к-во строк
             ActiveSheet.VPageBreaks(1).Delete
            */

            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            /*dynamic oSheetTpl = oBookTmp.Worksheets(1);
            oBook.Worksheets.AddCopy(oSheetTpl);*/
            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string sSpecContract = MyGetOneValue("select SContractNum from vwSpec where SVSpec=" + sid).ToString();
            /*string qq = "select sum(total) from(select distinct header, d_ks.KSTotal as total"+
        " from SpecVer inner join (select max(svid) sv_spec_ver_max, SVSpec from SpecVer where SVSpec = "+ sid+" group by SVSpec"+
            ") max_ver on sv_spec_ver_max = SVId"+
            " inner join SpecFill on SFSpecVer = sv_spec_ver_max"+
           " left join(select SFEId, SFEFill from Done inner join SpecFillExec on SFEId = DSpecExecFill group by SFEId, SFEFill"+
           ")d_done on SFId = SFEFill"+
            " left join(select dd.KSSpecFillId, concat('KC2 №', dd.KSNum, ' на сумму ', d.KSTotal) as header, dd.KSSum, d.KSTotal"+
            " from(select sum(KSSum) sumks, KSNum, KSTotal from KS2 group by KSNum, KSTotal)d left join(select KSSpecFillId, KSNum, KSSum from KS2"+
                    ")dd on dd.KSNum = d.KSNum)d_ks on d_ks.KSSpecFillId = SFId )pizda;";
            string total = MyGetOneValue(qq).ToString();*/
            //oSheet.Cells(5, 3).Value = "Работы выполнены по проекту: " + sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(1, 6).Value = sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(3, 6).Value = sSpecContract;
            //oSheet.Cells(4, 5).Value = total;
            // get the numbers
            string numSelq = "select sum(KS2withKeq1),sum(ZP),sum(EM),sum(ZPm),sum(TMC),sum(DTMC),sum(HPotZP),sum(SPotZP),sum(HPandSPotZPm),sum(KZPandZPM),sum(VZIS)" +
                " from KS2Doc where KSSpecId = " + sid;
            string[,] nums = MyGet2DArray(numSelq);
            //oSheet.Cells(11, 9).Value = nums[0, 0];
            oSheet.Cells(12, 9).Value = nums[0, 1];
            oSheet.Cells(13, 9).Value = nums[0, 2];
            oSheet.Cells(14, 9).Value = nums[0, 3];
            oSheet.Cells(15, 9).Value = nums[0, 4];
            oSheet.Cells(16, 9).Value = nums[0, 5];
            oSheet.Cells(17, 9).Value = nums[0, 6];
            oSheet.Cells(18, 9).Value = nums[0, 7];
            oSheet.Cells(19, 9).Value = nums[0, 8];
            oSheet.Cells(20, 9).Value = nums[0, 9];
            oSheet.Cells(21, 9).Value = nums[0, 10];
            oSheet.Cells(11, 9).Formula = "=I12+I13+I15+I17+I18+I19+I20+I21";
            string[,] koeffs = MyGet2DArray("select downKoefSMRPNR, downKoefTMC, downKoefVZIS from KS2Doc where KSSpecId = " + sid);
            int RowCount = koeffs?.GetLength(0) ?? 0;
            int ColCount = koeffs?.GetLength(1) ?? 0;
            if(!(RowCount == 0 && ColCount == 0))
            {
                oSheet.Cells(6, 6).Value = koeffs[0, 0];
                oSheet.Cells(7, 6).Value = koeffs[0, 1];
                oSheet.Cells(8, 6).Value = koeffs[0, 2];
            }
            // end getting numbers
            string q = "exec uspReport_KS2_v16 " + sid;

            string[,] vals = MyGet2DArray(q, true);

            RowCount = vals?.GetLength(0) ?? 0;
            ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("25:" + (22 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A24").Resize(RowCount, ColCount).Value = vals;

            oSheet.PageSetup.PrintArea = "$D$1:$M$" + (RowCount + 23).ToString();
            oSheet.Range("H25:R" + (RowCount + 23).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("K25:K" + (RowCount + 23).ToString()).Formula = "=RC[-3]-RC[2]";
            oSheet.Rows(25).Select();
            oApp.ActiveWindow.FreezePanes = true;
            oSheet.Range("A1").Select();

            var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
            var codeModule = oModule.CodeModule;
            var lineNum = codeModule.CountOfLines + 1;
            string sCode = "Public Sub mypagesetup()\r\n";
            sCode += " ActiveWindow.View = xlPageBreakPreview\r\n";
            sCode += " While ActiveSheet.VPageBreaks.Count > 0\r\n";
            sCode += "  ActiveSheet.VPageBreaks(1).DragOff xlToRight, 1\r\n";
            sCode += " Wend\r\n";
            sCode += "End Sub";
            codeModule.InsertLines(lineNum, sCode);
            oApp.Run(oBook.Worksheets[1].Name + ".mypagesetup");
            codeModule.DeleteLines(1, codeModule.CountOfLines); //start, count

            if (vals != null)
            {
                oSheet.Rows(24).AutoFilter();
                oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
            }

            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            SetForegroundWindow(new IntPtr(oApp.Hwnd));
            return;
        }

        public static void MyExcelCustomReport_Done(long sid)
    {
      if (sid <= 0) return;
      string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
      tmpl += "Акт мершейдерского замера.xlsx";

      //создаем Excel

      Type ExcelType = MyExcelType();
      dynamic oApp;
      try { oApp = Activator.CreateInstance(ExcelType);}
      catch (Exception ex)
      {
        MsgBox("Не удалось создать экземпляр Excel.");
        TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
        return;
      }

      oApp.Visible = false;
      oApp.ScreenUpdating = false;
      oApp.DisplayAlerts = false;

      //добавляем книгу

      bool first_sheet = true;
      dynamic oBook = oApp.Workbooks.Add();
      if (first_sheet)
      {
        while (oBook.Worksheets.Count > 1) oBook.Worksheets(2).Delete();
        first_sheet = false;
      }
      else oBook.Worksheets.Add();

      dynamic oSheet = oBook.Worksheets(1);

      string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
      System.IO.File.Copy(tmpl,tmp);
      dynamic oBookTmp = oApp.Workbooks.Open(tmp);

      
      /*
       ActiveSheet.PageSetup.PrintArea = "$C$1:$K$16" :: 15 + к-во строк
       ActiveSheet.VPageBreaks(1).Delete
      */

      oBookTmp.Worksheets(1).Activate();
      oBookTmp.Worksheets(1).Cells.Select();
      oApp.Selection.Copy();

      oBook.Activate();
      oSheet.Cells.Select();
      oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

      /*dynamic oSheetTpl = oBookTmp.Worksheets(1);
      oBook.Worksheets.AddCopy(oSheetTpl);*/
      oBookTmp.Close();
      System.IO.File.Delete(tmp);

      string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
      //oSheet.Cells(5, 3).Value = "Работы выполнены по проекту: " + sSpecInfo;//[шифр проекта, изм. 1]
      oSheet.Cells(5, 5).Value = sSpecInfo;//[шифр проекта, изм. 1]

      string q = "exec uspReport_SpecDone " + sid;

      string[,] vals = MyGet2DArray(q,true);

      int RowCount = vals?.GetLength(0) ?? 0;
      int ColCount = vals?.GetLength(1) ?? 0;

      if (RowCount > 1)
      {
        oSheet.Rows("10:"+(7+RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
      }
      if(vals!=null) oSheet.Range("A9").Resize(RowCount, ColCount).Value = vals;

      oSheet.PageSetup.PrintArea = "$C$1:$K$"+(RowCount+13).ToString();
      oSheet.Range("G10:R"+(RowCount+8).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
      oSheet.Range("J10:J"+(RowCount+8).ToString()).Formula = "=RC[-3]-RC[-2]-RC[-1]";
      oSheet.Rows(10).Select();
      oApp.ActiveWindow.FreezePanes = true;
      oSheet.Range("A1").Select();

      var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
      var codeModule = oModule.CodeModule;
      var lineNum = codeModule.CountOfLines + 1;
      string sCode = "Public Sub mypagesetup()\r\n";
      sCode += " ActiveWindow.View = xlPageBreakPreview\r\n";
      sCode += " While ActiveSheet.VPageBreaks.Count > 0\r\n";
      sCode += "  ActiveSheet.VPageBreaks(1).DragOff xlToRight, 1\r\n";
      sCode += " Wend\r\n";
      sCode += "End Sub";
      codeModule.InsertLines(lineNum, sCode);
      oApp.Run(oBook.Worksheets[1].Name + ".mypagesetup");
      codeModule.DeleteLines(1, codeModule.CountOfLines); //start, count

      if (vals != null)
      {
        oSheet.Rows(9).AutoFilter();
        oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
      }

      oApp.Visible = true;
      oApp.ScreenUpdating = true;
      oApp.DisplayAlerts = true;
      SetForegroundWindow(new IntPtr(oApp.Hwnd));
      return;
    }

    public static void MyExcelCustomReport_F7(string sids)
    {
      if (sids.Length == 0)
      {
        MsgBox("Список шифров не задан.");
        return;
      }
      string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
      tmpl += "Форма 7.xlsx";

      //создаем Excel

      Type ExcelType = MyExcelType();
      dynamic oApp;
      try { oApp = Activator.CreateInstance(ExcelType); }
      catch (Exception ex)
      {
        MsgBox("Не удалось создать экземпляр Excel.");
        TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
        return;
      }

      oApp.Visible = false;
      oApp.ScreenUpdating = false;
      oApp.DisplayAlerts = false;

      //добавляем книгу

      bool first_sheet = true;
      dynamic oBook = oApp.Workbooks.Add();
      if (first_sheet)
      {
        while (oBook.Worksheets.Count > 1) oBook.Worksheets(2).Delete();
        first_sheet = false;
      }
      else oBook.Worksheets.Add();

      dynamic oSheet = oBook.Worksheets(1);

      string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
      System.IO.File.Copy(tmpl, tmp);
      dynamic oBookTmp = oApp.Workbooks.Open(tmp);


      /*
       ActiveSheet.PageSetup.PrintArea = "$C$1:$K$16" :: 15 + к-во строк
       ActiveSheet.VPageBreaks(1).Delete
      */

      oBookTmp.Worksheets(1).Activate();
      oBookTmp.Worksheets(1).Cells.Select();
      oApp.Selection.Copy();

      oBook.Activate();
      oSheet.Cells.Select();
      oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

      /*dynamic oSheetTpl = oBookTmp.Worksheets(1);
      oBook.Worksheets.AddCopy(oSheetTpl);*/
      oBookTmp.Close();
      System.IO.File.Delete(tmp);

      //string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
      //oSheet.Cells(5, 3).Value = "Работы выполнены по проекту: " + sSpecInfo;//[шифр проекта, изм. 1]
      //oSheet.Cells(5, 5).Value = sSpecInfo;//[шифр проекта, изм. 1]

      string q = "select * from vw_Report_F7 where [Шифр ID] in(" + sids + ") order by [Шифры]";

      string[,] vals = MyGet2DArray(q, true);

      int RowCount = vals?.GetLength(0) ?? 0;
      int ColCount = vals?.GetLength(1) ?? 0;

      if (RowCount > 1)
      {
       // oSheet.Rows("4:" + (RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
      }
      if (vals != null) oSheet.Range("A4").Resize(RowCount, ColCount).Value = vals;

      oSheet.PageSetup.PrintArea = "$D$1:$R$" + (RowCount + 4).ToString();
      oSheet.Range("I5:K" + (RowCount + 8).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
      //oSheet.Range("J10:J" + (RowCount + 8).ToString()).Formula = "=RC[-3]-RC[-2]-RC[-1]";
      //oSheet.Rows(10).Select();
      //oApp.ActiveWindow.FreezePanes = true;
      oSheet.Range("A1").Select();

      var oModule = oBook.VBProject.VBComponents.Item(oBook.Worksheets[1].Name);
      var codeModule = oModule.CodeModule;
      var lineNum = codeModule.CountOfLines + 1;
      string sCode = "Public Sub mypagesetup()\r\n";
      sCode += " ActiveWindow.View = xlPageBreakPreview\r\n";
      sCode += " While ActiveSheet.VPageBreaks.Count > 0\r\n";
      sCode += "  ActiveSheet.VPageBreaks(1).DragOff xlToRight, 1\r\n";
      sCode += " Wend\r\n";
      sCode += "End Sub";
      codeModule.InsertLines(lineNum, sCode);
      oApp.Run(oBook.Worksheets[1].Name + ".mypagesetup");
      codeModule.DeleteLines(1, codeModule.CountOfLines); //start, count

      /*if (vals != null)
      {
        oSheet.Rows(9).AutoFilter();
        oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
      }*/

      oApp.Visible = true;
      oApp.ScreenUpdating = true;
      oApp.DisplayAlerts = true;
      SetForegroundWindow(new IntPtr(oApp.Hwnd));
      return;
    }

    /*
    private static bool FillingImportCheckValues(dynamic oSheet, List<MyXlsField> FillingReportStructure, object pb)
    {
      string s;
      long rl; decimal rd; DateTime dt; bool b;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      //int c = range.Columns.Count;
      MyProgressUpdate(pb, 20, "Проверка данных");
      if (rows > 1000)
      {
        if (MessageBox.Show("Файл содержит " + rows.ToString() + " строк. Продолжить?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        {
          return false;
        }
      }

      if (rows == 1)
      {
        MsgBox("Файл нужно заполнить.");
        return false;
      }

      //oSheet.Cells.Interior.Color = xlWhite;
      //oSheet.Cells.Font.Color = xlBlack;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 20 + 10 * r / rows, "Проверка данных");
        for (int c = 1; c < FillingReportStructure.Count() + 1; c++)
        {
          if (c >= 3 && c <= 19) continue;
          b = true;
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          if (!FillingReportStructure[c - 1].Nulable && s == "") b = false;
          if (b && s != "")
          {
            if (FillingReportStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            if (FillingReportStructure[c - 1].DataType == "decimal") b = decimal.TryParse(s, out rd) && rd > 0;
            if (FillingReportStructure[c - 1].DataType == "date") b = DateTime.TryParse(s, out dt);
          }
          if (!b)
          {
            e = true;
            oSheet.Cells(r, 1).Interior.Color = xlPink;
            oSheet.Cells(r, 1).Font.Color = xlRed;
            oSheet.Cells(r, c).Interior.Color = xlDimGray;
            oSheet.Cells(r, c).Font.Color = xlRed;
          }
        }
      }
      if (e) MsgBox("Не заданы корректные значения для обязательных столбцов.", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }
    */

  }
}
