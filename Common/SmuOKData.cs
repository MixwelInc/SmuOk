using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SmuOk.Common.DB;
using static SmuOk.Common.MyConst;
using static SmuOk.Common.MyReport;

namespace SmuOk.Common
{
  static class SmuOKData
  {
    public static void AddRowsToSpecFill(long uid, long lSpecVer, string source, object pb = null)
    {
      //1. Open excel file
      //2. Check data
      //3. Load data

      string sSpecName = MyGetOneValue("select SVName from SpecVer where SVId="+lSpecVer.ToString()).ToString();
      if (sSpecName == "")
      {
        MsgBox("Название шифра не должно быть пустым.");
        return;
      }

      OpenFileDialog ofd = new OpenFileDialog();
      int z_add = 0;//, z_upd = 0, z_del = 0;

      Application.UseWaitCursor = true;
      dynamic oExcel;
      dynamic oSheet;
      if (!MyExcelImportOpenDialog(out oExcel, out oSheet, "")) return;

      //FillingStructure = new List<MyXlsField>();
      List<MyXlsField> FillingStructure = FillReportData("SpecFilling_add");

      bool bNoError = true;

      bNoError = FillingImportCheckTitle(oSheet, FillingStructure, pb);
      if (bNoError && !FillingImportCheckValues(oSheet, FillingStructure, pb)) bNoError = false;
      if (bNoError && !FillingImportCheckSpecName(oSheet, sSpecName, out z_add, FillingStructure, pb)) bNoError = false;
      // не надо, это для существующих строк if (bNoError && !FillingImportCheckSVIds(oSheet, lSpecVer, out z_add, out z_upd, out z_del)) bNoError = false;

      if (bNoError)
      {
        if (MessageBox.Show("Всего будет добавлено строк: " + z_add + ".\n\n" +
            "Продолжить?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FillingImportData(oSheet, lSpecVer, source, FillingStructure, pb);
          //*FillFilling();
          MsgBox("Ok");
        }
        oExcel.ScreenUpdating = true;
        oExcel.DisplayAlerts = true;
        oExcel.Quit();
      }
      else
      {
        oExcel.ScreenUpdating = true;
        oExcel.Visible = true;
        oExcel.ActiveWindow.Activate();
      }
      GC.Collect();
      GC.WaitForPendingFinalizers();
      Application.UseWaitCursor = false;
      MyProgressUpdate(pb, 0);
      return;
    }

    private static bool FillingImportCheckTitle(dynamic oSheet, List<MyXlsField> FillingStructure, object pb=null)
    {
      MyProgressUpdate(pb, 10, "Проверка заголовков");
      string s;
      bool e = false;
      for (int i = 1; i <= FillingStructure.Count; i++)
      {// MyXlsField f in FillingReportStructure)
        MyProgressUpdate(pb, 10 + i * .5, "Проверка заголовков");
        s = oSheet.Cells(1, i).Value?.ToString() ?? "";
        if (s != FillingStructure[i - 1].Title)
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

    private static bool FillingImportCheckValues(dynamic oSheet, List<MyXlsField> FillingStructure, object pb = null)
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

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 20 + 10 * r / rows, "Проверка данных");
        for (int c = 1; c <= FillingStructure.Count; c++)
        {
          b = true;
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          if (!FillingStructure[c - 1].Nulable && s == "") b = false;
          if (b && s != "")
          {
            // Можно добавить дату. Упрощать не надо!
            if (FillingStructure[c - 1].DataType == "long") b = long.TryParse(s, out rl) && rl > 0;
            if (FillingStructure[c - 1].DataType == "decimal") b = decimal.TryParse(s, out rd) && rd > 0;
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

    private static bool FillingImportCheckSpecName(dynamic oSheet, string SpecCode, out int z_add, List<MyXlsField> FillingStructure, object pb = null)
    {
      z_add = 0;
      object o_s;
      string s;
      bool e = false;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      int c = 1; // 1-based SpecCodeCol
      if (rows == 1) return true;

      z_add = rows - 1;

      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 30 + 10 * r / rows, "Проверка шифра проекта");
        o_s = oSheet.Cells(r, c).Value;
        s = o_s == null ? "" : o_s.ToString();
        if (FillingStructure[c - 1].Nulable == false && s != SpecCode)
        {
          e = true;
          oSheet.Cells(r, 1).Interior.Color = 13421823;
          oSheet.Cells(r, 1).Font.Color = -16776961;
          oSheet.Cells(r, c).Interior.Color = 0;
          oSheet.Cells(r, c).Font.Color = -16776961;
        }
      }
      if (e) MsgBox("Шифр проекта в файле (см. столбец <B>) не совпадает с шифром проекта в изменяемой версии (изменении), «" + SpecCode + "».", "Ошибка", MessageBoxIcon.Warning);
      return !e;
    }

    private static void FillingImportData(dynamic oSheet, long svid, string source, List<MyXlsField> FillingStructure, object pb = null)
    {
      string q = "";
      string s;
      dynamic range = oSheet.UsedRange;
      int rows = range.Rows.Count;
      for (int r = 2; r < rows + 1; r++)
      {
        MyProgressUpdate(pb, 50 + 30 * r / rows, "Формирование запросов");
        q += "insert into SpecFill (SFSpecVer,SFSubcode,SFType,SFNo,SFNo2,SFName,SFMark,SFCode,SFMaker,SFUnit,SFQty,SFUnitWeight,SFNote,SFDocs,SFSource) \nValues (" + svid;
        for (int c = 2; c <= FillingStructure.Count; c++)
        {
          s = oSheet.Cells(r, c).Value?.ToString() ?? "";
          if (FillingStructure[c - 1].DataType == "decimal") s = s.Replace(",", ".");
          q += "," + MyES(s, false, FillingStructure[c - 1].Nulable);
          //;
        }
        q += "," + MyES(source);
        q += ");\n\n";
      }
      MyProgressUpdate(pb, 95, "Импорт данных");
      MyExecute(q);
      return;
    }

  }
}
