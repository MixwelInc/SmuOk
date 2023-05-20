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
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;

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
      public string DataType { get; set; } //string, date, long, decimal
      public bool Nulable { get; set; }
      public bool Subzero { get; set; }
      public string[] Vals { get; set; }
      public bool SkipOnLoad { get; set; }
      public MyXlsField(string sql_name, string title, string data_type, bool nulable = true, bool subzero = false, string[] vals = null, bool skip_on_load = false)
      {
        SqlName = sql_name;
        Title = title;
        DataType = data_type;
        Nulable = nulable; //can be empty like ""
        Subzero = subzero; //if true - decimal can be zero (only for decimal)
        Vals = vals;
        SkipOnLoad = skip_on_load; //skips the column on load :)
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
                for (int i = 17; i <= 25; i++)
                {
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
      {
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

    public static bool MyExcelImport_CheckValues(dynamic oSheet, List<MyXlsField> ReportStructure, object pb)
    {
      string s;
      long rl; decimal rd; bool b;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
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
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
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
          FillingReportStructure.Add(new MyXlsField("has_filling", "Есть спецификация", "string", true));
          FillingReportStructure.Add(new MyXlsField("is_active", "Статус", "string", true));
          FillingReportStructure.Add(new MyXlsField("case when upddt.dt is not NULL then upddt.dt when adddt.dt is not NULL then adddt.dt else " +
                                                    "'17-02-2023' end as change_dt", "Дата изменения", "date", true));
          break;
                case "InvDoc":
                    FillingReportStructure.Add(new MyXlsField("InvId", "ID счет", "long", true));
                    FillingReportStructure.Add(new MyXlsField("InvType", "Вид документа (КП, СЧЕТ)", "vals", true, vals: new string[] {"КП", "СЧЕТ"}));
                    FillingReportStructure.Add(new MyXlsField("InvINN", "ИНН юр. лица по счету", "string", true));
                    FillingReportStructure.Add(new MyXlsField("InvLegalName", "Наименование организации", "string", true));
                    FillingReportStructure.Add(new MyXlsField("InvNum", "№ счета/КП", "string", true));
                    FillingReportStructure.Add(new MyXlsField("InvDate", "Дата", "date", true));
                    FillingReportStructure.Add(new MyXlsField("InvSumWithVAT", "Сумма с НДС", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("InvSumWOVAT", "Сумма без НДС", "decimal", true));
                    FillingReportStructure.Add(new MyXlsField("InvSumFinished", "Сумма разнесена", "decimal", true, skip_on_load: true));
                    FillingReportStructure.Add(new MyXlsField("InvComment", "Комментарий", "string", true));
                    break;
                case "OrderDoc":
                    FillingReportStructure.Add(new MyXlsField("SId", "ID спец.", "long"));
                    FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
                    FillingReportStructure.Add(new MyXlsField("Initiator", "От кого", "string", true));
                    FillingReportStructure.Add(new MyXlsField("OrderNum", "Номер заявки", "string", true));
                    FillingReportStructure.Add(new MyXlsField("OrderDate", "Дата заявки", "string", true));
                    FillingReportStructure.Add(new MyXlsField("RecieveDate", "Дата получения", "string", true));
                    FillingReportStructure.Add(new MyXlsField("Note", "Примечание", "string", true));
                    FillingReportStructure.Add(new MyXlsField("OrderId", "ID заявки", "long", true));
                    FillingReportStructure.Add(new MyXlsField("RowsFinished", "Кол-во строк обработано", "long", true));
                    break;
                case "SubContract":
                    FillingReportStructure.Add(new MyXlsField("SId", "ID спец.", "long",false));
                    FillingReportStructure.Add(new MyXlsField("SSystem", "Наименование работ", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SStation", "Станция", "string", false));
                    FillingReportStructure.Add(new MyXlsField("curator", "Куратор", "string", true));
                    FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));//------------
                    FillingReportStructure.Add(new MyXlsField("SArea", "Участок строительства", "string", false));
                    FillingReportStructure.Add(new MyXlsField("SubContractId", "ID договора", "long"));
                    FillingReportStructure.Add(new MyXlsField("SubName", "Наименование субподрядчика", "string"));
                    FillingReportStructure.Add(new MyXlsField("SubINN", "ИНН субподрядчика", "string"));
                    FillingReportStructure.Add(new MyXlsField("SubContractNum", "№ договора СМУ-24-Субподрятчик", "string"));
                    FillingReportStructure.Add(new MyXlsField("SubContractDate", "Дата договора СМУ-24-Субподрятчик", "date"));
                    FillingReportStructure.Add(new MyXlsField("SubDownKoefSMR", "Понижающий к СМР суб", "decimal", subzero: true));
                    FillingReportStructure.Add(new MyXlsField("SubDownKoefPNR", "Понижающий к ПНР суб", "decimal", subzero: true));
                    FillingReportStructure.Add(new MyXlsField("SubDownKoefTMC", "Понижающий к ТМЦ суб", "decimal", subzero: true));
                    FillingReportStructure.Add(new MyXlsField("SubContractAprPriceWOVAT", "Приблизительная цена договора без НДС", "decimal", subzero: true));
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
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("SFType", "Вид по спецификации", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSupplyPID", "PID", "decimal", false, true));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQty", "Количество", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("SFSupplyPreType", "Чьи материалы", "vals", false, false, new string[] { "нет" , "заказчик" , "подрядчик", "не изменять" })); // 0=нет, 1=закупка (buy), 2=давал (gnt)
          FillingReportStructure.Add(new MyXlsField("SFSpecDateProtDel", "Дата протокола разделения", "date"));
          FillingReportStructure.Add(new MyXlsField("SFSpecNumProtDel", "№ протокола разделения", "string"));          
          FillingReportStructure.Add(new MyXlsField("SFNote", "Примечание", "string"));
          FillingReportStructure.Add(new MyXlsField("case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          break;
        case "Curator":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long"));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQty", "К-во всего", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во (исп.)", "decimal", false));
          FillingReportStructure.Add(new MyXlsField("EName", "Исполнитель", "string"));
          FillingReportStructure.Add(new MyXlsField("SFIsSub", "Самостоятельная закупка исполнителем", "string", vals: new string[] { "нет", "да" }));
          FillingReportStructure.Add(new MyXlsField("case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
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
          FillingReportStructure.Add(new MyXlsField("case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          break;
        case "InvCfm":
          FillingReportStructure.Add(new MyXlsField("sf.SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          //FillingReportStructure.Add(new MyXlsField("sfeo.SFEOId", "ID позиции заявки", "long", false));3
          FillingReportStructure.Add(new MyXlsField("sf.SFSubcode", "Шифр по спецификации", "string",true,false,null,true));
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo", "№ п/п", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo2", "№ п/п 2", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFName", "Наименование и техническая характеристика", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFMark", "Тип, марка, обозначение документа", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFUnit", "Единица измерения", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во", "decimal", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("e.ename", "Исполнитель", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("ICId", "ID строки счета", "long"));
          //FillingReportStructure.Add(new MyXlsField("so.SOResponsOS", "Ответственный ОС", "string", true, false, null, true));//13
          //FillingReportStructure.Add(new MyXlsField("SFEONum", "№ заявки от участка/субчика", "string", true, false, null, false));//14
          //FillingReportStructure.Add(new MyXlsField("so.SOOrderDate", "Дата заявки", "date", true));//15
          FillingReportStructure.Add(new MyXlsField("cnt.AmountOrdered as AmountOrdered", "К-во всего заказано", "fake"));//
          //FillingReportStructure.Add(new MyXlsField("SFEOStartDate", "Желаемая дата поставки", "fake"));//17
          //FillingReportStructure.Add(new MyXlsField("SFEOQty", "К-во заказано", "fake"));//18
          //FillingReportStructure.Add(new MyXlsField("so.SOPlan1CNum", "№ планирования 1С", "string", true, false, null, false));//19
          //FillingReportStructure.Add(new MyXlsField("so.SO1CPlanDate", "Дата планирования 1С", "date"));//20
          FillingReportStructure.Add(new MyXlsField("IC1SOrderNo", "№ заявки 1С", "string", true));
          FillingReportStructure.Add(new MyXlsField("SFSupplyDate1C", "Дата заявки 1С", "date"));//
          FillingReportStructure.Add(new MyXlsField("InvDocId", "ID счета", "long", false));
          FillingReportStructure.Add(new MyXlsField("InvINN", "ИНН юр. лица по счету", "fake",true));
          FillingReportStructure.Add(new MyXlsField("InvLegalName", "Наименование организации", "fake", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("InvType", "Вид документа (КП, счет)", "fake", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("InvNum", "№ счета", "fake", true));
          FillingReportStructure.Add(new MyXlsField("InvDate", "Дата счета", "fake", true));
          FillingReportStructure.Add(new MyXlsField("ICRowNo", "№ п/п счета", "string", true));
          FillingReportStructure.Add(new MyXlsField("ICName", "Наименование по счету", "string", true));
          FillingReportStructure.Add(new MyXlsField("ICUnit", "Ед.изм.", "string", true));
          FillingReportStructure.Add(new MyXlsField("ICQty", "К-во", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("ICPrc", "Цена за 1 ед. без НДС", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("ICK", "К перевода в ед. спец.", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("SFDaysUntilSupply", "Срок поставки в днях", "long", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("case when sf.SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          //FillingReportStructure.Add(new MyXlsField("so.SOComment", "Комментарий", "fake", true, false, null, true));//36
          //FillingReportStructure.Add(new MyXlsField("so.SOId", "SOId", "fake", nulable: true));//37
          break;
        case "SupplyOrder":
          FillingReportStructure.Add(new MyXlsField("sf.SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          //FillingReportStructure.Add(new MyXlsField("SOOrderId", "ID заказа", "long", false));
          FillingReportStructure.Add(new MyXlsField("sf.SFSubcode", "Шифр по спецификации", "string",true,false,null,true));
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo", "№ п/п", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo2", "№ п/п 2", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFName", "Наименование и техническая характеристика", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFMark", "Тип, марка, обозначение документа", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFUnit", "Единица измерения", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("SFEQty as QtyBuy", "К-во", "decimal", true, false, null, true));///////
          FillingReportStructure.Add(new MyXlsField("e.ename", "Исполнитель", "fake", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOOrderDocId", "ID заявки", "long",false));
          FillingReportStructure.Add(new MyXlsField("SOId", "ID позиции заявки", "fake",nulable: true));
          //FillingReportStructure.Add(new MyXlsField("sfeo.SFEOId", "ID з. на пост.", "long"));14
          FillingReportStructure.Add(new MyXlsField("SF.SFSupplyPID", "PID", "fake", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("CASE WHEN sf.SFQtyBuy>0 THEN 'Подрядчик' ELSE 'Заказчик' END SOSupplierType", "Чья поставка", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOResponsOS", "Ответственный ОС", "string", true, false, null, true));//
          //FillingReportStructure.Add(new MyXlsField("SFEONum", "№ заявки от участка/субчика", "fake", true, false, null, false));//18
          FillingReportStructure.Add(new MyXlsField("SORealNum", "Фактический номер заявки", "string", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("SOOrderDate", "Дата заявки", "date", true));//
          FillingReportStructure.Add(new MyXlsField("cnt.AmountOrdered as AmountOrdered", "К-во всего заказано", "fake"));//
          //FillingReportStructure.Add(new MyXlsField("SFEOStartDate", "Желаемая дата поставки", "fake"));//22
          //FillingReportStructure.Add(new MyXlsField("SFEOQty", "К-во заказано", "fake"));//23
          FillingReportStructure.Add(new MyXlsField("SOPlan1CNum", "№ планирования 1С / письма в МИП", "string", true));//
          FillingReportStructure.Add(new MyXlsField("SO1CPlanDate", "Дата планирования 1С / письма в МИП", "date", true));
          FillingReportStructure.Add(new MyXlsField("SOComment", "Комментарий", "string", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SOOrderNumPref", "Постфикс поставки", "string", true));//
          FillingReportStructure.Add(new MyXlsField("case when sf.SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          break;
          case "M15":
          FillingReportStructure.Add(new MyXlsField("sf.SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SFEId", "ID задачи", "long", false));
          FillingReportStructure.Add(new MyXlsField("vws.SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("sf.SFSubcode", "Шифр по спецификации", "string",true,false,null,true));
          FillingReportStructure.Add(new MyXlsField("sf.SFType", "Вид по спецификации", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo", "№ п/п", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFNo2", "№ п/п 2", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFName", "Наименование и техническая характеристика", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFMark", "Тип, марка, обозначение документа", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("sf.SFUnit", "Единица измерения", "string", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("SF.SFQtyGnT as QtyBuy", "К-во", "decimal", true, false, null, true));
          FillingReportStructure.Add(new MyXlsField("SF.SFSupplyPID", "PID", "fake", true, false, null, true));//
          FillingReportStructure.Add(new MyXlsField("SFEONum", "№ заявки от участка/субчика", "fake", true, false, null, false));//
          FillingReportStructure.Add(new MyXlsField("SOOrderDate", "Дата заявки", "date"));//
          FillingReportStructure.Add(new MyXlsField("cnt.AmountOrdered as AmountOrdered", "К-во всего заказано", "fake"));//
          FillingReportStructure.Add(new MyXlsField("SOPlan1CNum", "№ письма в МИП", "string"));//
          FillingReportStructure.Add(new MyXlsField("SO1CPlanDate", "Дата письма в МИП", "date"));
          FillingReportStructure.Add(new MyXlsField("BoLQtySum", "Поставлено ранее", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy-(IsNull(BoLQtySum,0)) BRestQty", "Осталось поставить", "decimal"));////
          FillingReportStructure.Add(new MyXlsField("M15Id", "ID записи M15", "string", true));
          FillingReportStructure.Add(new MyXlsField("PID2", "PID2", "string", false));
          FillingReportStructure.Add(new MyXlsField("AFNNum", "№ АФН", "string", false));
          FillingReportStructure.Add(new MyXlsField("AFNDate", "Дата АФН", "date", false));
          FillingReportStructure.Add(new MyXlsField("ABKNum", "№ АВК", "string", false));
          FillingReportStructure.Add(new MyXlsField("AFNName", "Наименование по АФН", "string", true));
          FillingReportStructure.Add(new MyXlsField("SechCab", "Сечение для кабеля", "decimal", false));
          //FillingReportStructure.Add(new MyXlsField("M15Price", "Цена по АФН", "decimal", false));//////
          FillingReportStructure.Add(new MyXlsField("BarNum", "№ барабана", "string", false));
          FillingReportStructure.Add(new MyXlsField("AFNQty", "К-во по АФН", "decimal", false));//
          FillingReportStructure.Add(new MyXlsField("Reciever", "Кто получил", "string", false));//
          FillingReportStructure.Add(new MyXlsField("LandingPlace", "Место отгрузки", "string"));//
          FillingReportStructure.Add(new MyXlsField("StoragePlace", "Место хранения", "string", false));
          FillingReportStructure.Add(new MyXlsField("UnloadDate", "Дата отгрузки Участок/Подрядчик", "string", false));
          FillingReportStructure.Add(new MyXlsField("FIO", "ФИО Участок/Подрядчик", "string"));//
          FillingReportStructure.Add(new MyXlsField("UnloadQty", "Кол-во отгруженно", "decimal"));//
          FillingReportStructure.Add(new MyXlsField("CarNum", "№ автомобиля", "string"));//
          FillingReportStructure.Add(new MyXlsField("M15Num", "№ М-15", "string", false));//
          FillingReportStructure.Add(new MyXlsField("M15Date", "Дата М-15", "date", false));
          FillingReportStructure.Add(new MyXlsField("M15Name", "Наименование по  М-15", "string",false)); 
          FillingReportStructure.Add(new MyXlsField("M15Qty", "К-во по М-15", "decimal",false));
          FillingReportStructure.Add(new MyXlsField("case when sf.SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          
          break;
        case "BoL":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", false));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFType", "Вид по спецификации", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFCode", "Код оборудования, изделия, материала", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy", "Количество", "decimal", false));
          /*FillingReportStructure.Add(new MyXlsField("ic.SFPlan1CNum", "№ планирования 1с", "string"));//12
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
          FillingReportStructure.Add(new MyXlsField("ic.ICK", "К перевода в ед. спец.", "string"));//24*/
          FillingReportStructure.Add(new MyXlsField("BoLQtySum", "Поставлено ранее", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy-(IsNull(BoLQtySum,0)) BRestQty", "Осталось поставить", "decimal"));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBId as SFBId", "ID позиции УПД", "long", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBBolNoForTSK as SFBBolNoForTSK", "№ УПД для ТСК", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBBoLDateForTSK as SFBBoLDateForTSK", "Дата УПД для ТСК", "date", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBNoForTSK as SFBNoForTSK", "№ п/п в УПД для ТСК", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBUnitForTSK as SFBUnitForTSK", "Ед. изм. по УПД для ТСК", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBQtyForTSK as SFBQtyForTSK", "К-во по УПД для ТСК", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBRecipient as SFBRecipient", "Кто получил", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBShipmentPlace as SFBShipmentPlace", "Место отгрузки", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBBoLNoFromTSK as SFBBoLNoFromTSK", "№ УПД от ТСК", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBBoLDateFromTSK as SFBBoLDateFromTSK", "Дата УПД от ТСК", "date", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBNoFromTSK as SFBNoFromTSK", "№ п/п в УПД от ТСК", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBUnitFromTSK as SFBUnitFromTSK", "Ед. изм. по УПД от ТСК", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBQtyFromTSK as SFBQtyFromTSK", "К-во по УПД от ТСК", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBName", "Название по УПД", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBPriceWONDS", "Цена без НДС", "decimal", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SFBComment", "Комментарий", "string", true));
          FillingReportStructure.Add(new MyXlsField("sfe.SFEId", "SFEId", "bigint", true));
          FillingReportStructure.Add(new MyXlsField("sfb2.SOId", "SOId", "bigint", true));
          FillingReportStructure.Add(new MyXlsField("case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          break;
        case "SupplyDate":
          FillingReportStructure.Add(new MyXlsField("SFEId", "ID работы", "long", false));
          FillingReportStructure.Add(new MyXlsField("SFEOId", "ID з. на пост.", "long"));
          FillingReportStructure.Add(new MyXlsField("SVName", "Шифр проекта", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "Шифр по спецификации", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string"));
          FillingReportStructure.Add(new MyXlsField("EName", "Исполнитель", "string"));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во всего", "string"));
          FillingReportStructure.Add(new MyXlsField("SFEQty", "К-во заказано", "string"));
          FillingReportStructure.Add(new MyXlsField("SFEOQty", "К-во требуется", "string", false));
          FillingReportStructure.Add(new MyXlsField("SFEOStartDate", "Дата начала работ", "date", false));
          FillingReportStructure.Add(new MyXlsField("SFill", "ID филки", "string"));
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
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
          FillingReportStructure.Add(new MyXlsField("QtyToOrder", "К-во требуется", "decimal", false));
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
          FillingReportStructure.Add(new MyXlsField("SFSpecList", "Лист по спецификации, код системы", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo", "№ п/п", "string"));
          FillingReportStructure.Add(new MyXlsField("SFNo2", "№ п/п 2", "string"));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string"));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string"));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Единица измерения", "string"));
          FillingReportStructure.Add(new MyXlsField("SFQty", "К-во", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyGnT", "Давальч.", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyBuy", "Закуп.", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyWarehouse", "Склад", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtyWorkshop", "Цех", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFQtySub", "Закуп. суб/п", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFSupplyPID", "PID", "long"));
          FillingReportStructure.Add(new MyXlsField("M15Num", "№ М15", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("M15Date", "Дата М15", "date", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("M15Qty", "Кол-во по М15", "decimal", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("M15Price", "Цена по М15", "decimal", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("M15Name", "Наименование по М15", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("q.Qty", "Смонтировано по ВОР", "decimal", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          break;
        case "Budget":
          FillingReportStructure.Add(new MyXlsField("SFId", "ID записи", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFId", "ID истории", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFId", "ID позиции счета", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFId", "Шифр проекта", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFId", "Шифр по спецификации", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFHId", "Исполнитель", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SVName", "Вид по спецификации", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFSubcode", "№ п/п", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("e.EName", "№ п/п 2", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFName", "Наименование и техническая характеристика", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFMark", "Тип, марка, обозначение документа", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "Ед. изм", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFUnit", "PID", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetType", "Чьи материалы", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetType", "ВОР накопительный", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetType", "Цена по счету", "string", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetType", "ID сметы", "long", skip_on_load: true));
          //FillingReportStructure.Add(new MyXlsField("SFBudgetType", "Вид по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudget", "Номер сметы", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetType", "ID позиции сметы", "long", skip_on_load: true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetType", "Вид по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetNo", "№ по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetSmrNo", "№ по СМР", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetCode", "Шифр расценки и коды ресурсов", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetName", "Наименование по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetUnit", "Ед. изм. по смете", "string"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetK", "К перевода", "decimal"));
          FillingReportStructure.Add(new MyXlsField("SFBudgetQty", "Кол-во по смете", "decimal", true, true));
          FillingReportStructure.Add(new MyXlsField("BFSum", "Сумма по смете", "decimal", true, true));
          FillingReportStructure.Add(new MyXlsField("SFBudgetPrc", "Цена сметная за ед. без НДС", "decimal", true, true));
          FillingReportStructure.Add(new MyXlsField("case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos", "Исключенная позиция", "string", vals: new string[] { "нет", "да" }));
          break;

      }
      return FillingReportStructure;
    }

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

                    }
                }

                if (TitleRowsWidthN > 1)
                {
                    oSheet.Rows(1).RowHeight = TitleRowsWidthN * 15;//M
                    oSheet.Rows("2:" + RowCount).EntireRow.AutoFit();
                }
                else oSheet.Rows("1:" + RowCount).EntireRow.AutoFit();


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
        public static bool checkExecByPID(string PID)
        {
            string checkq = "select count(*) from SpecFill inner join SpecFillExec sfe on sfe.SFEFill = SFId" +
                            " where SFSupplyPID = " + PID +
                            "  and IsNull(SFQtyBuy,0)>0 ";
            var result = MyGetOneValue(checkq);
            if(result is null || (int)result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkDoublesInM15(string PID, decimal M15Qty, string M15Num, string M15Date)
        {
            try
            {
                string checkq = " SELECT count(*)"+
                                " FROM SpecFill" +
                                " inner join M15 on FillId = SFId" +
                                " where SFSupplyPID = " + PID +
                                //" and M15Qty = " + MyES(M15Qty) +//
                                " and M15Num = " + MyES(M15Num) + 
                                " and M15Date = " + MyES(M15Date);
                var result = MyGetOneValue(checkq);
                if(result is null || (int)result == 0)
                {
                    checkq = "select count(*)" +
                        " from M15" +
                        " where PID = " + PID +
                        //" and M15Qty = " + MyES(M15Qty) +//
                        " and M15Num = " + MyES(M15Num) +
                        " and M15Date = " + MyES(M15Date);
                    result = MyGetOneValue(checkq);
                    if (result is null || (int)result == 0)
                    {
                        return true;
                    }
                    else return false;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool checkPIDExist(int PID)
        {
            try
            {
                string checkq = "SELECT count(*) FROM SpecFill where SFSupplyPID = " + PID;
                var res= MyGetOneValue(checkq);
                if (res is null || (int)res == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public static bool MyParsePID(string[,] data, int rowCount)
        {
            //int[] PIDsArr, NotFoundRows;
            string pidsubstr;
            string name;
            try
            {
                for (int i = 1; i <= rowCount; i++)/////
                {
                    int pid;
                    if (!Int32.TryParse(data[i, 3], out pid))
                    {
                        name = data[i, 2];
                        Decimal.TryParse(data[i,5], out decimal checkQty);
                        if(name is null)
                        {
                            data[i, 0] = "10"; //setting status 10 for non-data rows
                            continue;
                        }
                        else if(name.Length < 10 || checkQty == 0)
                        {
                            data[i, 0] = "10"; //setting status 10 for non-data rows
                            continue;
                        }
                        else
                        {
                            pidsubstr = name.Substring(name.Length - 8, 7);
                            if (!Int32.TryParse(pidsubstr, out pid))
                            {
                                data[i, 0] = "1"; //setting status 1 for positions without PID
                                continue;
                            }
                            else //found PID in name column
                            {
                                name = name.Substring(0, name.Length - 9);
                                data[i, 2] = name; //rewriting name without PID
                                data[i, 3] = pid.ToString();
                            }
                        }
                    }
                    else if(pid < 100)
                    {
                        data[i, 0] = "10";
                        continue;
                    }
                    else if(pid < 5000000)
                    {
                        data[i, 0] = "8"; //setting status for positions with PID2
                    }

                    if (data[i,3] is null)
                    {
                        data[i, 0] = "10";
                        continue;
                    }
                    if (!checkPIDExist(pid))
                    {
                        data[i, 0] = "2"; //setting status 2 for NotFound in DB
                    }
                    else
                    {
                        data[i, 0] = "0"; //setting 0 status for valid PIDs
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return true;
        }
        public static bool MyExcelParseVPDM(out string[,] data, object pb = null)
        {
            int exept = 0;
            string load_id = Guid.NewGuid().ToString();
            OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "MS Excel files (*.xlsx)|*.xlsx";
                ofd.RestoreDirectory = true;
                data = null;
                if (ofd.ShowDialog() != DialogResult.OK) return false;
                var f = string.Empty;
                f = ofd.FileName;
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(f);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = 10; //we do not need more data
            data = new string[rowCount+1, colCount+1];
            MyProgressUpdate(pb, 5, "Чтение данных");
            try 
            {
                for (int i = 1; i <= rowCount; i++)//
                {
                    if(i % 10 == 0)
                    {
                        MyProgressUpdate(pb, ((double)i / (double)rowCount) * 100, "Чтение данных");
                    }
                    for (int j = 1; j <= colCount; j++)
                    {

                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            if(j == 10) //parsing date via .text
                            {
                                Console.Write(xlRange.Cells[i, j].Text.ToString() + "\t");
                                data[i, j] = xlRange.Cells[i, j].Text.ToString();
                            }
                            else
                            {
                                Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                                data[i, j] = xlRange.Cells[i, j].Value2.ToString();
                            }
                        }
                        else
                        {
                            data[i, j] = null;
                        }
                    }
                    Console.WriteLine(i);
                }
                MyParsePID(data, rowCount); //parsing PID into data
                for (int i = 1; i <= rowCount; i++)
                {
                    if(data[i,0] == "0") //only for valid positions
                    {
                        Decimal.TryParse(data[i, 5], out decimal qty);
                        if(!checkDoublesInM15(data[i,3],qty,data[i,9],data[i,10])) //if true - does not exist in db (valid)
                        {
                            data[i, 0] = "3"; //setting status for positions that already exist
                        }
                        else if(!checkAlreadyUploaded(data[i, 3], data[i, 9], data[i, 10]))
                        {
                            data[i, 0] = "7"; //setting status for already uploaded rows (in fact can be only with doubles in VPDM)
                        }
                        else
                        {
                            if(!checkExecByPID(data[i,3]))
                            {
                                data[i, 0] = "4"; //setting status for position that have SFQtyBuy (подрядчик)
                            }
                        }
                    }
                    MyProgressUpdate(pb, ((double)i / (double)rowCount) * 100, "Валидация");
                }
                if(importVPDMToTMPTable(data, rowCount, load_id)) //made this to find 1-import to tmp table, 2 - select correct qty for each PID
                {
                    for (int i = 1; i <= rowCount; i++)
                    {
                        exept = i;
                        if (data[i, 0] == "0")
                        {
                            Decimal.TryParse(data[i, 5], out decimal qty);
                            decimal validQty = QtyCheckByPID(data[i, 3], qty); //add qty check here
                            if (validQty != qty)
                            {
                                data[i, 0] = "5"; //setting state for positions with not valid qty
                                data[i, 5] = validQty.ToString(); //setting valid qty for upload
                                //decimal notImportedQty = qty - validQty; //storing qty that we can't import now
                                //data[i, 6] = "" + notImportedQty;
                            }
                        }
                        else
                        {
                            data[i, 5] = "0";
                        }
                        MyProgressUpdate(pb, ((double)i / (double)rowCount) * 100, "Проверка числовых параметров");
                    }
                }
                dropOldLoad(load_id);
                importVPDMToTMPTable(data, rowCount, load_id);
                //create report for user (at least something was imported, have to create report)
                if (createUserReportVPDM(load_id, data, rowCount, colCount))
                {
                    if(MessageBox.Show("Загрузить зеленые и светло-зеленые позиции?", " Пожалуйста, ознакомьтесь с содержимым файла\n" +
                            "В случае наличия ошибок, нажите Нет и сообщите подробности администратору"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        dropOldLoad(load_id);
                        return false;
                    }
                    else
                    {
                        if (importValidPositions(load_id)) 
                            return true;
                        else 
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message + " строка: " + exept);
                return false;
            }
            finally
            {
                dropOldLoad(load_id);
                MyProgressUpdate(pb, 0);
                Console.WriteLine("in final");

                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                xlWorkbook.Save();
                xlWorkbook.Close();
                while (Marshal.ReleaseComObject(xlWorkbook) > 0) { }

                //quit and release
                xlApp.Workbooks.Close();
                xlApp.Quit();
                while (Marshal.ReleaseComObject(xlApp) > 0) { }
                Marshal.ReleaseComObject(xlApp);
                xlWorkbook = null;
                xlApp = null;

                Thread t = new Thread(new ThreadStart(() =>
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }));

                t.Start();
            }
        }

        public static bool importValidPositions(string load_id)
        {
            try
            {
                string importq = "insert into M15 (PID, M15Num, M15Date, M15Price, M15Name, load_id, M15Qty, Approver)" +
                    " select PID, M15Num, M15Date, M15Price, M15Name, load_id, M15Qty, Approver" +
                    " from M15_tmp" +
                    " where load_id = '" + load_id + "';"; // TODO: insert here and add logs with userID and load_id
                MyExecute(importq);
                dropOldLoad(load_id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool createUserReportVPDM(string load_id, string[,] data, int rowCount, int colCount) 
        {
            try
            {
                string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
                tmpl += "VPDM_report_template.xlsx";
                //создаем Excel

                Type ExcelType = MyExcelType();
                dynamic oApp;
                try { oApp = Activator.CreateInstance(ExcelType); }
                catch (Exception ex)
                {
                    MsgBox("Не удалось создать экземпляр Excel.");
                    TechLog("Activator.CreateInstance(ExcelType) :: " + ex.Message);
                    return false;
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
                }
                else oBook.Worksheets.Add();

                dynamic oSheet = oBook.Worksheets(1);

                string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
                System.IO.File.Copy(tmpl, tmp);
                dynamic oBookTmp = oApp.Workbooks.Open(tmp);

                oBookTmp.Worksheets(1).Activate();
                oBookTmp.Worksheets(1).Cells.Select();
                oApp.Selection.Copy();

                oBook.Activate();
                //oSheet.Cells.Select();
                oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

                oBookTmp.Close();
                System.IO.File.Delete(tmp);


                if (rowCount > 1)
                {
                    oSheet.Rows("8:" + (7 + rowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
                }
                oSheet.Range("A8").Resize(rowCount, colCount+1).Value = data;

                
                //oSheet.Range("X5:Y" + (RowPlusDelta).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
                //oSheet.Range.RemoveRow("AA5:AB" + (RowPlusDelta).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
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

                List<int> delRows = new List<int>();
                for(int i = 1; i <= rowCount; i++)
                {
                    if(data[i,0] == "10")
                    {
                        delRows.Add(i);
                    }
                    var res = oSheet.Cells(i + 7, 1).Value?.ToString() ?? "";
                    if (res == "0") oSheet.Range("A" + (i + 7).ToString() + ":B" + (i + 7).ToString()).Interior.color = Color.Green;
                    if (res == "5") oSheet.Range("A" + (i + 7).ToString() + ":B" + (i + 7).ToString()).Interior.color = Color.YellowGreen;
                    if (res == "1" || res == "2" || res == "3" || res == "4" 
                        || res == "7" || res == "8" ) oSheet.Range("A" + (i + 7).ToString() + ":B" + (i + 7).ToString()).Interior.color = Color.Yellow;
                    //oSheet.Range(xlsCharByNum(i) + "2:" + xlsCharByNum(i) + RowCount).Interior.color

                }
                //delRows.OrderByDescending(x => x);
                for(int i = delRows.Count - 1; i>=0; i--)
                {
                    oSheet.Rows(delRows[i] + 8).Delete(-4162); //delete with Shift Up
                }
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("0", "Валид", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("1", "PID не найден в файле", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("2", "PID не найден в базе", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("3", "Уже существует в базе", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("4", "Позиция подрядчика", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("5", "Не получается загрузить весь объем", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("7", "Позиция уже была загружена", xlPart, xlByRows, false, false, false);
                oSheet.Range("A8:A" + (rowCount + 7).ToString()).Replace("8", "Некорректный PID", xlPart, xlByRows, false, false, false);
                oSheet.Rows(8).Delete(-4162);
                oApp.Visible = true;
                oApp.ScreenUpdating = true;
                oApp.DisplayAlerts = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool dropOldLoad (string load_id)
        {
            try
            {
                string delq = "delete from M15_tmp where load_id = '" + load_id +"';";
                MyExecute(delq);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool checkAlreadyUploaded(string PID, string M15Num, string M15Date)
        {
            try
            {
                string checkq = "SELECT count(*) FROM M15_tmp where hash_id = '" + PID + M15Num + M15Date + "';";
                var res = MyGetOneValue(checkq);
                if (res is null || (int)res == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static bool importVPDMToTMPTable(string[,] data, int rowCount, string load_id)
        {
            try
            {
                int counter = 0;
                string insq = "insert into M15_tmp (PID, M15Num, M15Date, M15Qty, M15Price, M15Name, M15Unit, M15State, M15NotToImport, hash_id, load_id, Approver) values ";
                for (int i = 1; i <= rowCount; i++)
                {
                    if (data[i, 0] == "0" || (data[i,0]) == "5" && Decimal.Parse(data[i,5]) > 0) //write only valid positions or positions with greater qty but not 0 qty to be written
                    {
                        counter++;
                        decimal qty = Decimal.Parse(data[i, 5]);
                        decimal price = Decimal.Parse(data[i, 7]);
                        decimal notImportedQty = Decimal.Parse(data[i, 6]);

                        insq += " (" + data[i, 3] + ",'" + data[i, 9] + "','" + data[i, 10] + "'," +
                                MyES(qty) + "," + MyES(price) + ",'" + data[i, 2].Replace("'", "") + "','" +
                                data[i, 4] + "'," + data[i, 0] + "," + MyES(notImportedQty) + ",'" + data[i, 3] + data[i, 9] + data[i, 10] + "','" + load_id + "',"+ uid + "),";
                    }
                    if(counter >= 500) //inserting by batches
                    {
                        insq = insq.TrimEnd(',');
                        MyExecute(insq);
                        insq = "insert into M15_tmp (PID, M15Num, M15Date, M15Qty, M15Price, M15Name, M15Unit, M15State, M15NotToImport, hash_id, load_id, Approver) values ";
                        counter = 0;
                    }

                }
                insq = insq.TrimEnd(','); //крем для лица (для сухой кожи)
                if(counter != 0)
                {
                    MyExecute(insq);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
                MsgBox(ex.Message);
                return false;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static decimal QtyCheckByPID(string PID, decimal VPDMQty)
        {
            decimal totalQty;
            string selTotalQtyq = "select sum(SFQty) from vwSpecFill vwsf " +
                "outer apply (select max(svid) id  from SpecVer sv where sv.SVSpec = vwsf.SId) max_sv " +
                "where vwsf.SVId = max_sv.id and vwsf.SFSupplyPID = " + PID;
            Decimal.TryParse(MyGetOneValue(selTotalQtyq).ToString(), out totalQty);
            string selM15Qtyq =  " SELECT sum(m.M15Qty) + sum(tmp.M15Qty)" +
                                " FROM SpecFill sf" +
                                " inner join M15 m on m.FillId = sf.SFId" +
                                " left join M15_tmp tmp on tmp.PID = sf.SFSupplyPID" +
                                " where SFSupplyPID = " + PID;
            Decimal.TryParse(MyGetOneValue(selM15Qtyq).ToString() , out decimal M15Qty);
            decimal result = totalQty - M15Qty - VPDMQty;
            if (result < 0) //too much in vpdm
            {
                if (totalQty - M15Qty <= 0)
                {
                    return 0; //return 0 because qtyies are already done
                }
                else
                {
                    return totalQty - M15Qty; //return the maximum possible amount to be written in DB
                }

            }
            else
            {
                return VPDMQty;
            }
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
      string dt;

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
                dt = oSheet.Cells(r, c).Value?.ToString() ?? "";
                datarow.Add(dt);
                break;
            }
          }
        }
        ImportData.Add(datarow);
      }
      return true;
    }

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


            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select 'Шифр проекта ' + SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string sStationInfo = MyGetOneValue("select 'По системе: ' + SArea from vwSpec where SVSpec=" + sid).ToString();
           
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

        public static void MyExcelSupplyDateReport(long sid, string specVer, long executor)////
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "Шаблон_ДатаПоставки.xlsx";

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


            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            //oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string sStationInfo = MyGetOneValue("select SArea from vwSpec where SVSpec=" + sid).ToString();

            oSheet.Cells(10, 3).Value = sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(9, 3).Value = sStationInfo;

            string q = "select SFNo + '.' + SFNo2, SFSupplyPID, SFName, SFMark, SFEQty, SFUnit, SFEOQty, convert(nvarchar(10), SFEOStartDate, 104) SFEOStartDate, SFEOAddress, SFEOResponse, " +
              " cnt.AmountOrdered as AmountOrdered, cnt2.AmountDoneBoL, cnt3.AmountDoneM15, SFEId, SFEOId, sfefill, SFSpecList, case when SFRemovedPos = 'да' then 'да' else 'нет' end SFRemovedPos " +
              " from SpecVer inner join SpecFill sf on SVId=SFSpecVer inner join SpecFillExec sfe on SFId=SFEFill inner join Executor on SFEExec=EId " +
              " left join SpecFillExecOrder sfeo on SFEOSpecFillExec=SFEId " +
              " outer apply (select sum(SFEOQty) as AmountOrdered from SpecFillExecOrder sfeo left join SpecFillExec sfe2 on SFEId=SFEOSpecFillExec where sfe2.SFEFill = sfe.SFEFill ) cnt " +
              " outer apply (select sum(SFBQtyForTSK) as AmountDoneBoL from SpecFillBol where SFBFill = SFId) cnt2 " +
              " outer apply (select sum(M15Qty) as AmountDoneM15 from M15 where FillId = SFId ) cnt3" +
              " where SFSpecVer=" + specVer.ToString() +
              " and SFEExec=" + executor;
            q += " order by " +
             " case IsNumeric(SF.SFNo) when 1 then Replicate('0', 10 - Len(SF.SFNo)) +SF.SFNo else SF.SFNo end, " +
                   " case IsNumeric(SF.SFNo2) when 1 then Replicate('0', 10 - Len(SF.SFNo2)) + SF.SFNo2 else SF.SFNo2 end, sfeo.SFEOId ";
            string[,] vals = MyGet2DArray(q, false);

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("14:" + (12 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null)
            {
                oSheet.Range("A14").Resize(RowCount, ColCount).Value = vals;
            }
            else
            {
                MsgBox("Нет наполнения, нечего выгружать. \n(выгрузка возможно только при указании исполнителя)");
                return;
            }
            oSheet.PageSetup.PrintArea = "$A$1:$J$" + (RowCount + 21).ToString();
            oSheet.Range("G14:G" + (RowCount + 21).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("E14:E" + (RowCount + 21).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("J14:M" + (RowCount + 21).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Rows(14).Select();
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
                oSheet.Rows(13).AutoFilter();
                oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
            }

            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            SetForegroundWindow(new IntPtr(oApp.Hwnd));
            return;
        }

        public static void MakeMainReport(object pb)
        {
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "Главный_Отчет.xlsx";

            MyProgressUpdate(pb, 5, "Формирование шаблона");
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

            dynamic oBook = oApp.Workbooks.Add();
            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);

            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            MyProgressUpdate(pb, 10, "Создание книги");

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);
            Thread pbThread = new Thread(() => { for (int i = 0; i < 100; i++) { MyProgressUpdate(pb, i, "Чтение данных"); Thread.Sleep(5000); } });

            pbThread.Start();
            oBook.RefreshAll();
            pbThread.Join();
            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            MyProgressUpdate(pb, 0);
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
            }
            else oBook.Worksheets.Add();

            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);

            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);
            string GetKS2Query = " select d.KSId, vws.SContractNum, vws.SArea,d.KS2Num, b.BNumber, b.BMIPRegNum + ', вер. '+ cast(SVNo as nvarchar) as regNum, vws.SVName, vws.SSystem,"+
                        " e.EName,lower(SUBSTRING(datename(month,d.KS2Date),1,3)) + '.' + SUBSTRING(cast(year(d.KS2Date) as nvarchar), 3, 4), d.KS2withKeq1, d.ZP, d.EM, d.ZPm, d.TMC, d.DTMC, d.HPotZP, d.SPotZP, d.HPandSPotZPm," +
                        " round((ZP + ZPm) * 0.15,3) as colS, d.VZIS, round(d.KS2withKeq1 + ((ZP + ZPm) * 0.15) + d.VZIS,3) as new_colV, d.KS3Num, " +
                        " round((ZP + EM + HPotZP + SPotZP + HPandSPotZPm + (ZP + ZPm) * 0.15) * downKoefSMRPNR + VZIS * downKoefVZIS + TMC * downKoefTMC,3) as colW,"+
                        " round((ZP + ZPm) * downKoefSMRPNR,3) as colX,"+
                        " d.KS3VahtNum, d.KSVahtSum, "+
                        " round((ZP + EM + HPotZP + SPotZP + HPandSPotZPm + (ZP + ZPm) * 0.15) * subDownKoefSMRPNR + VZIS * downKoefVZIS + TMC * subDownKoefTMC,3) as colAA, d.subMonth, d.KS3ImportNum, vws.SSubDocNum" +
                        " FROM KS2Doc d "+
                        " left join vwSpec vws on vws.SId = d.KSSpecId "+
                        " left join Budget b on b.BId = d.KSBudgId"+
                        " left join Executor e on e.EId = d.KSExec" +
                        " where vws.SState != 1 " +
                        " order by KSId";
            string[,] vals = MyGet2DArray(GetKS2Query, false);

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("6:" + (4 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A6").Resize(RowCount, ColCount).Value = vals;

            int RowPlusDelta = RowCount + 6;
            oSheet.Range("K5:V" + (RowPlusDelta).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("X5:Y" + (RowPlusDelta).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("AA5:AB" + (RowPlusDelta).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
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
            return;
        }


        public static void MyExecNZPDocReport()
        {
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "накопительный_НЗП.xlsx";
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
            }
            else oBook.Worksheets.Add();

            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);

            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            //oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);
            string GetNZPQuery = " select d.NZPId,lower(SUBSTRING(datename(month,d.CalcNZPDate),1,3)) + '.' + SUBSTRING(cast(year(d.CalcNZPDate) as nvarchar), 3, 4)," +
                        " vws.SExecutor, d.CalcNZPNum, vws.SArea, vws.SVName, b.BNumber, vws.SSystem," +
                        " d.ZP + d.EM + d.TMC + d.HPotZP + d.SPotZP + d.HPandSPotZPm as akt_sum, d.ZP, d.EM, d.ZPm, d.TMC, d.DTMC, d.HPotZP, d.SPotZP, d.HPandSPotZPm," +
                        " d.ZTR, d.ZTR/7.2, (d.ZP + d.ZPM)/d.ZTR as col_T,((d.ZP + d.ZPM)/d.ZTR)*7.2 as col_U, d.ZP + d.EM + d.TMC + d.HPotZP + d.SPotZP + d.HPandSPotZPm + d.DTMC, " +
                        " d.MntMaster, d.Note " +
                        " FROM NZPDoc d " +
                        " left join vwSpec vws on vws.SId = d.SpecId " +
                        " left join Budget b on b.BId = d.BudgId" +
                        " where vws.SState != 1 " +
                        " order by d.NZPId";
            string[,] vals = MyGet2DArray(GetNZPQuery, false);

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("7:" + (5 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A7").Resize(RowCount, ColCount).Value = vals;

            int RowPlusDelta = RowCount + 6;
            oSheet.Range("I7:V" + (RowPlusDelta).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
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
            return;
        }

        public static void MyExcelPriceApprovementReport(long sid, long uid)
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "шаблон_согласование_цен_2.xlsx";

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



            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            //oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SVName from vwSpec where SVId=" + sid).ToString();
            string sSpecArea = MyGetOneValue("select SArea from vwSpec where SVId=" + sid).ToString();

            oSheet.Cells(13, 5).Value = "Шифр проекта: " + sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(5, 5).Value = "Объект строительства: " + sSpecArea;

            DateTime dateTime = DateTime.UtcNow.Date;
            oSheet.Cells(3, 21).Value = dateTime.ToString("dd/MM/yyyy");
            oSheet.Cells(22, 8).Value = '"' + dateTime.ToString("dd") + "\" " + dateTime.ToString("MMMM") + ' ' + dateTime.ToString("yyyy") + "г. (дата формирования заявки)";

            string userFIO = MyGetOneValue("select EUF + ' ' + EUI + ' ' + EUO from _engUser where EUId =" + uid.ToString()).ToString();
            oSheet.Cells(25, 7).Value = userFIO;

            string userEmail = MyGetOneValue("select EUEmail from _engUser where EUId = " + uid.ToString()).ToString();
            oSheet.Cells(26, 7).Value = userEmail;


            string execPAProcedure = "exec uspReport_PriceApprovement_v1 " + sid;

            string[,] vals = MyGet2DArray(execPAProcedure, true);

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("19:" + (16 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A18").Resize(RowCount, ColCount).Value = vals;

            oSheet.PageSetup.PrintArea = "$E$1:$U$" + (RowCount + 24).ToString();
            oSheet.Range("J18:Z" + (RowCount + 17).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("O19:O" + (RowCount + 17).ToString()).Formula = "=RC[-1]*RC[-5]*RC[-2]"; //count sums in excel
            oSheet.Range("R19:R" + (RowCount + 17).ToString()).Formula = "=RC[-1]*RC[-8]"; //count sums in excel
            oSheet.Range("S19:S" + (RowCount + 17).ToString()).Formula = "=RC[-2]-RC[-5]"; //count sums in excel
            oSheet.Range("T19:T" + (RowCount + 17).ToString()).Formula = "=RC[-2]-RC[-5]"; //count sums in excel
            oSheet.Rows(18).Select();
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
                oSheet.Rows(18).AutoFilter();
                oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
            }

            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            SetForegroundWindow(new IntPtr(oApp.Hwnd));
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



            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            //oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string sSpecContract = MyGetOneValue("select SContractNum from vwSpec where SVSpec=" + sid).ToString();

            oSheet.Cells(1, 11).Value = sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(3, 11).Value = sSpecContract;

            // get the numbers
            string getNumbersQuery = "select sum(KS2withKeq1),sum(ZP),sum(EM),sum(ZPm),sum(TMC),sum(DTMC),sum(HPotZP),sum(SPotZP),sum(HPandSPotZPm),sum(KZPandZPM),sum(VZIS)" +
                " from KS2Doc where KSSpecId = " + sid;
            string[,] nums = MyGet2DArray(getNumbersQuery);
            //oSheet.Cells(11, 9).Value = nums[0, 0];
            oSheet.Cells(12, 14).Value = nums[0, 1];
            oSheet.Cells(13, 14).Value = nums[0, 2];
            oSheet.Cells(14, 14).Value = nums[0, 3];
            oSheet.Cells(15, 14).Value = nums[0, 4];
            oSheet.Cells(16, 14).Value = nums[0, 5];
            oSheet.Cells(17, 14).Value = nums[0, 6];
            oSheet.Cells(18, 14).Value = nums[0, 7];
            oSheet.Cells(19, 14).Value = nums[0, 8];
            oSheet.Cells(20, 14).Value = nums[0, 9];
            oSheet.Cells(21, 14).Value = nums[0, 10];
            oSheet.Cells(11, 14).Formula = "=N12+N13+N15+N17+N18+N19+N20+N21";
            string[,] koeffs = MyGet2DArray("select ROUND(downKoefSMRPNR,3), ROUND(downKoefTMC,3), ROUND(downKoefVZIS,3) from KS2Doc where KSSpecId = " + sid);
            int RowCount = koeffs?.GetLength(0) ?? 0;
            int ColCount = koeffs?.GetLength(1) ?? 0;
            if(!(RowCount == 0 && ColCount == 0))
            {
                oSheet.Cells(6, 11).Value = koeffs[0, 0];
                oSheet.Cells(7, 11).Value = koeffs[0, 1];
                oSheet.Cells(8, 11).Value = koeffs[0, 2];
            }
            // end getting numbers
            string execKS2Procedure = "exec uspReport_KS2_v16 " + sid;

            string[,] vals = MyGet2DArray(execKS2Procedure, true);

            RowCount = vals?.GetLength(0) ?? 0;
            ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("25:" + (22 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A24").Resize(RowCount, ColCount).Value = vals;

            oSheet.PageSetup.PrintArea = "$I$1:$R$" + (RowCount + 23).ToString();
            oSheet.Range("M25:Z" + (RowCount + 23).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("P25:P" + (RowCount + 23).ToString()).Formula = "=RC[-3]-RC[-2]"; //count sums in excel
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

        public static void MyExcelM11Report_Done(long sid)
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "m-11-blank.xlsx";

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



            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            //oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SExecutor + 'по шифру ' + SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string getnum = "select max(id) from M11";
            string res = MyGetOneValue(getnum).ToString();
            oSheet.Cells(13, 6).Value = sSpecInfo;
            oSheet.Cells(5, 10).Value = res;
            string execM11Procedure = "exec uspReport_M11 " + sid;

            string[,] vals = MyGet2DArray(execM11Procedure, true);
            int RowCount, ColCount;

            RowCount = vals?.GetLength(0) ?? 0;
            ColCount = vals?.GetLength(1) ?? 0;
            
            if (RowCount > 1)
            {
                oSheet.Rows("18:" + (16 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A17").Resize(RowCount, ColCount).Value = vals;

            oSheet.PageSetup.PrintArea = "$D$1:$N$" + (RowCount + 23).ToString();
            oSheet.Range("O18:V" + (RowCount + 16).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("J18:L" + (RowCount + 16).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("M18:M" + (RowCount + 16).ToString()).Formula = "=RC[-1]*RC[-2]"; //count sums in excel
            oSheet.Rows(18).Select();
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
                oSheet.Rows(17).AutoFilter();
                oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
            }

            oApp.Visible = true;
            oApp.ScreenUpdating = true;
            oApp.DisplayAlerts = true;
            SetForegroundWindow(new IntPtr(oApp.Hwnd));
            return;
        }

        public static void MyExcelNZPReport(long sid)
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "НЗП.xlsx";

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



            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            //oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
            string sSpecContract = MyGetOneValue("select SContractNum from vwSpec where SVSpec=" + sid).ToString();

            oSheet.Cells(1, 11).Value = sSpecInfo;//[шифр проекта, изм. 1]
            oSheet.Cells(3, 11).Value = sSpecContract;

            // get the numbers
            string getNumbersQuery = "select count(*),sum(ZP),sum(EM),sum(ZPm),sum(TMC),sum(DTMC),sum(HPotZP),sum(SPotZP),sum(HPandSPotZPm),sum(ZTR)" +
                " from NZPDoc where SpecId = " + sid;
            string[,] nums = MyGet2DArray(getNumbersQuery);
            //oSheet.Cells(11, 9).Value = nums[0, 0];
            oSheet.Cells(11, 14).Value = nums[0, 1];
            oSheet.Cells(12, 14).Value = nums[0, 2];
            oSheet.Cells(13, 14).Value = nums[0, 3];
            oSheet.Cells(14, 14).Value = nums[0, 4];
            oSheet.Cells(15, 14).Value = nums[0, 5];
            oSheet.Cells(16, 14).Value = nums[0, 6];
            oSheet.Cells(17, 14).Value = nums[0, 7];
            oSheet.Cells(18, 14).Value = nums[0, 8];
            oSheet.Cells(20, 14).Value = nums[0, 9];//
            //oSheet.Cells(21, 13).Value = nums[0, 10];
            oSheet.Cells(10, 14).Formula = "=N11+N12+N14+N16+N17+N18+N20";
            //oSheet.Cells(19, 11).Formula = "=(K11 + K13)*0,15";
            string[,] koeffs = MyGet2DArray("select ROUND(downKoefSMRPNR,3), ROUND(downKoefTMC,3) from NZPDoc where SpecId = " + sid);
            int RowCount = koeffs?.GetLength(0) ?? 0;
            int ColCount = koeffs?.GetLength(1) ?? 0;
            if (!(RowCount == 0 && ColCount == 0))
            {
                oSheet.Cells(5, 11).Value = koeffs[0, 0];
                oSheet.Cells(6, 11).Value = koeffs[0, 1];
            }
            // end getting numbers
            string execKS2Procedure = "exec uspReport_NZP " + sid;

            string[,] vals = MyGet2DArray(execKS2Procedure, true);

            RowCount = vals?.GetLength(0) ?? 0;
            ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("24:" + (22 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A23").Resize(RowCount, ColCount).Value = vals;

            oSheet.PageSetup.PrintArea = "$I$1:$Q$" + (RowCount + 23).ToString();
            oSheet.Range("M24:X" + (RowCount + 23).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("P24:P" + (RowCount + 23).ToString()).Formula = "=RC[-3]-RC[-2]-RC[-1]"; //count sums in excel
            oSheet.Rows(24).Select();
            oApp.ActiveWindow.FreezePanes = true;
            //oSheet.Cells(19, 11).Formula = "=(K11 + K13)*0,15";
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
                oSheet.Rows(23).AutoFilter();
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
      }
      else oBook.Worksheets.Add();

      dynamic oSheet = oBook.Worksheets(1);

      string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
      System.IO.File.Copy(tmpl,tmp);
      dynamic oBookTmp = oApp.Workbooks.Open(tmp);


      oBookTmp.Worksheets(1).Activate();
      oBookTmp.Worksheets(1).Cells.Select();
      oApp.Selection.Copy();

      oBook.Activate();
      oSheet.Cells.Select();
      oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

      oBookTmp.Close();
      System.IO.File.Delete(tmp);

      string sSpecInfo = MyGetOneValue("select SVName + ', вер. '+ cast(SVNo as nvarchar) from vwSpec where SVSpec=" + sid).ToString();
      oSheet.Cells(5, 7).Value = sSpecInfo;//[шифр проекта, изм. 1]

      string getUspReportQuery = "exec uspReport_SpecDone " + sid;

      string[,] vals = MyGet2DArray(getUspReportQuery,true);

      int RowCount = vals?.GetLength(0) ?? 0;
      int ColCount = vals?.GetLength(1) ?? 0;

      if (RowCount > 1)
      {
        oSheet.Rows("10:"+(7+RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
      }
      if(vals!=null) oSheet.Range("A9").Resize(RowCount, ColCount).Value = vals;
      oSheet.PageSetup.PrintArea = "$E$1:$M$"+(RowCount+13).ToString();
      oSheet.Range("I10:Z"+(RowCount+8).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
      oSheet.Range("L10:L"+(RowCount+8).ToString()).Formula = "=RC[-3]-RC[-2]-RC[-1]";
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
            decimal amount, amountTotal;
            for (int i = 1; i < RowCount; i++)
            {
                if(vals[i,13] == "old")
                {
                    oSheet.Range("E" + (i + 9) + ":M"+ (i + 9)).Interior.color = Color.Red;
                    continue;
                }
                decimal.TryParse(string.Join("", vals[i, 9].Where(c => char.IsDigit(c))), out amount);
                decimal.TryParse(string.Join("", vals[i, 8].Where(c => char.IsDigit(c))), out amountTotal);
                if (amount > amountTotal)
                {
                    oSheet.Range("E" + (i + 9) + ":M" + (i + 9)).Interior.color = Color.Yellow;
                }
            }
      oApp.Visible = true;
      oApp.ScreenUpdating = true;
      oApp.DisplayAlerts = true;
      SetForegroundWindow(new IntPtr(oApp.Hwnd));
      return;
    }

        public static void MyExcelSuperCustomReport_Done(long sid)
        {
            if (sid <= 0) return;
            string tmpl = MyGetOneValue("select EOValue from _engOptions where EOName='TeplateFolder';").ToString();
            tmpl += "ВСО_шаблон.xlsx";

            //trying to get data, if null - return
            string getUspReportQuery = "exec uspReport_SpecDoneCustom " + sid;

            string[,] vals = MyGet2DArray(getUspReportQuery, true);

            if(vals is null)
            {
                MsgBox("Нет данных для отображения");
                return;
            }
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
            }
            else oBook.Worksheets.Add();

            dynamic oSheet = oBook.Worksheets(1);

            string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            System.IO.File.Copy(tmpl, tmp);
            //System.IO.File.
            dynamic oBookTmp = oApp.Workbooks.Open(tmp);


            oBookTmp.Worksheets(1).Activate();
            oBookTmp.Worksheets(1).Cells.Select();
            oApp.Selection.Copy();

            oBook.Activate();
            oSheet.Cells.Select();
            oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

            oBookTmp.Close();
            System.IO.File.Delete(tmp);

            string sSpecInfo = MyGetOneValue("select SVName from vwSpec where SVSpec=" + sid).ToString();
            oSheet.Cells(10, 5).Value = sSpecInfo;//[шифр проекта]

            string address = MyGetOneValue("select SSystem from vwSpec where SVSpec=" + sid).ToString();
            oSheet.Cells(9, 5).Value = address;//[address]

            int RowCount = vals?.GetLength(0) ?? 0;
            int ColCount = vals?.GetLength(1) ?? 0;

            if (RowCount > 1)
            {
                oSheet.Rows("19:" + (16 + RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
            }
            if (vals != null) oSheet.Range("A18").Resize(RowCount, ColCount).Value = vals;

            oSheet.PageSetup.PrintArea = "$A$1:$F$" + (RowCount + 37).ToString();
            oSheet.Range("G19:Z" + (RowCount + 17).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
            oSheet.Range("E19:E" + (RowCount + 17).ToString()).Replace(".0000", "", xlPart, xlByRows, false, false, false);
            //oSheet.Range("L10:L" + (RowCount + 8).ToString()).Formula = "=RC[-3]-RC[-2]-RC[-1]";
            oSheet.Rows(18).Select();
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
                oSheet.Rows(18).AutoFilter();
                oSheet.Columns(xlsCharByNum(ColCount + 1) + ":zz").Delete();
            }
            decimal amount, amountTotal;
            for (int i = 0; i < RowCount; i++)
            {
                if (vals[i, 8] == "old")
                {
                    oSheet.Range("A" + (i + 18) + ":F" + (i + 18)).Interior.color = Color.Red;
                    continue;
                }
                decimal.TryParse(string.Join("", vals[i, 4].Where(c => char.IsDigit(c))), out amount);
                decimal.TryParse(string.Join("", vals[i, 6].Where(c => char.IsDigit(c))), out amountTotal);
                if (amount > amountTotal)
                {
                    oSheet.Range("A" + (i + 18) + ":F" + (i + 18)).Interior.color = Color.Yellow;
                }
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
      }
      else oBook.Worksheets.Add();

      dynamic oSheet = oBook.Worksheets(1);

      string tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
      System.IO.File.Copy(tmpl, tmp);
      dynamic oBookTmp = oApp.Workbooks.Open(tmp);

      oBookTmp.Worksheets(1).Activate();
      oBookTmp.Worksheets(1).Cells.Select();
      oApp.Selection.Copy();

      oBook.Activate();
      oSheet.Cells.Select();
      oApp.Selection.PasteSpecial(xlPasteAll, xlNone, false, false);

      oBookTmp.Close();
      System.IO.File.Delete(tmp);

      string getVwReportF7Query = "select * from vw_Report_F7 where [Шифр ID] in(" + sids + ") order by [Шифры]";

      string[,] vals = MyGet2DArray(getVwReportF7Query, true);

      int RowCount = vals?.GetLength(0) ?? 0;
      int ColCount = vals?.GetLength(1) ?? 0;

      if (RowCount > 1)//maybe have to delete it 
      {
       // oSheet.Rows("4:" + (RowCount).ToString()).Insert(xlDown, xlFormatFromLeftOrAbove);
      }
      if (vals != null) oSheet.Range("A4").Resize(RowCount, ColCount).Value = vals;

      oSheet.PageSetup.PrintArea = "$D$1:$R$" + (RowCount + 4).ToString();
      oSheet.Range("I5:K" + (RowCount + 8).ToString()).Replace(".", ",", xlPart, xlByRows, false, false, false);
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


      oApp.Visible = true;
      oApp.ScreenUpdating = true;
      oApp.DisplayAlerts = true;
      SetForegroundWindow(new IntPtr(oApp.Hwnd));
      return;
    }
  }
}
