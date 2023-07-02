using System;
using System.Drawing;

namespace SmuOk.Common
{
  public static class MyConst
  {
    public static bool IsDebugComputer() { return Environment.MachineName == "DESKTOP-ESR8QOJ"; }
    //private const string ConnectionString_Debug = "Server=STT\\SQLE;Database=SmuOk;Trusted_Connection=True;";
    //private const string ConnectionString_Debug_Archive = "Server=STT\\SQLE;Database=SmuOk_archive;Trusted_Connection=True;";
    //private const string ConnectionString_Prod = "Server=SERVER-SMUOK\\SQLEXPRESS;Database=SmuOk;Trusted_Connection=Yes;";
    //private const string ConnectionString_Prod_Archive = "Server=SERVER-SMUOK\\SQLEXPRESS;Database=SmuOk_archive;Trusted_Connection=Yes;";
    private const string ConnectionString_Debug = "Server=DESKTOP-ESR8QOJ;Database=SmuOkk;Trusted_Connection=True;";
    private const string ConnectionString_Prod = "Server=SERVER-SMUOK\\SQLEXPRESS01;Database=SmuOk;Trusted_Connection=Yes;Connect Timeout=300";
    public static bool ConStr_IsArchive { get; set; } = false;
    public static string ConStr()
    {
      //return ConStr_IsArchive ? (IsDebugComputer() ? ConnectionString_Debug_Archive : ConnectionString_Prod_Archive) : (IsDebugComputer() ? ConnectionString_Debug : ConnectionString_Prod);
      //return IsDebugComputer() ? ConnectionString_Debug : ConnectionString_Prod;
      return IsDebugComputer() ? ConnectionString_Debug : ConnectionString_Prod;
    }
    public static Color BtnColorDesabled { get; } = Color.DarkGray;
    public static Color BtnColorNew { get; } = Color.Green;
    public static Color BtnColorSave { get; } = Color.Blue;
    public static Color BtnColorCancel { get; } = Color.Maroon;
    public static string MyToolTipText = "(требуется)";

    public static long wdToggle = 9999998;
    public static long wdAlignParagraphLeft = 0;
    public static long wdAlignParagraphCenter = 1;
    public static long wdAlignParagraphRight = 2;
    public static long wdAlignParagraphJustify = 3;
    public static int wdAlignVerticalTop = 0;
    public static long wdWord9TableBehavior = 1;
    public static long wdAutoFitFixed = 0;
    public static long wdCharacter = 1;
    public static long wdLine = 5;
    public static long wdUnderlineSingle = 1;
    public static long wdUnderlineNone = 0;
    public static long wdNumberGallery = 2;
    public static long wdTrailingTab = 0;
    public static long wdListNumberStyleArabic = 0;
    public static long wdListLevelAlignLeft = 0;
    public static long wdListApplyToWholeList = 0;
    public static long wdWord10ListBehavior = 2;
    public static long wdNumberParagraph = 1;
    public static long wdFormatDocument = 0;
    public static long wdDoNotSaveChanges = 0;
    public static long wdPrintView = 3;
    public static int wdGutterPosLeft = 0;

    public static long wdOrientPortrait = 0;
    public static long wdOrientLandscape = 1;

    public static long wdFindContinue = 1;
    public static long wdReplaceAll = 2;

    public static int wdPageBreak = 7;
    public static int wdSectionBreakNextPage = 2;
    public static int wdSectionBreakContinuous = 3;

    public static long wdCollapseStart = 1;
    public static long wdCollapseEnd = 0;

    public static long wdPreferredWidthPoints = 3;
    public static int wdAllowOnlyReading = 3;
    public static int wdExportFormatPDF = 17;
    public static int wdExportOptimizeForPrint = 0;
    public static int wdExportAllDocument = 0;
    public static int wdExportDocumentContent = 0;
    public static int wdExportCreateNoBookmarks = 0;

    public static long xlTypePDF = 0;
    public static long xlQualityStandard = 0;

    public static long xlToLeft = -4159;

    public static long xlSolid = 1;
    public static long xlAutomatic = -4105;
    public static long xlPasteValues = -4163;
    public static long xlPart = 2;
    public static long xlByRows = 1;
    public static long xlDown = -4121;
    public static long xlFormatFromLeftOrAbove = 0;
    public static long xlNone = -4142;
    public static long xlThemeColorAccent3 = 7;
    public static long xlThemeColorDark2 = 3;
    public static long xlThemeColorDark1 = 1;

    public static long xlContext = -5002;
    public static long xlDataBarColor = 0;
    public static long xlDataBarBorderSolid = 1;
    public static long xlConditionValueAutomaticMin = 6;
    public static long xlConditionValueAutomaticMax = 7;

    public static long xlTop = -4160;
    public static long xlBottom = -4107;
    public static long xlCenter = -4108;
    public static long xlLeft = -4131;
    public static long xlRight = -4152;

    //Excel.XlBorderWeight
    public static long xlHairline = 1;
    public static long xlMedium = -4138;
    public static long xlThick = 4;
    public static long xlThin = 2;

    //Excel.LineStyle
    public static long xlDash = -4115;
    public static long xlContinuous = 1;

    //Excel.XlBordersIndex
    public static long xlDiagonalDown = 5;
    public static long xlDiagonalUp = 6;
    public static long xlEdgeLeft = 7;
    public static long xlEdgeTop = 8;
    public static long xlEdgeBottom = 9;
    public static long xlEdgeRight = 10;
    public static long xlInsideVertical = 11;
    public static long xlInsideHorizontal = 12;

    public static long xlToRight = -4161;

    public static long xlNormalView = 1;
    public static long xlPageBreakPreview = 2;
    public static long xlPageLayoutView = 3;

    public static long xlDialogSaveAs = 5;
    public static long xlOpenXMLWorkbook = 51;

    //Sorting
    public static long xlSortOnValues = 0;
    public static long xlAscending = 1;
    public static long xlSortNormal = 0;
    public static long xlYes = 1;
    public static long xlTopToBottom = 1;
    public static long xlPinYin = 1;

    //Excel pivot tables
    public static long xlDatabase = 1;
    /*public static long xlPivotTableVersion2000 As UInt16 = 0 'Excel 2000
    public static long xlPivotTableVersion10 As UInt16 = 1 'Excel 2002
    public static long xlPivotTableVersion11 As UInt16 = 2 'Excel 2003
    public static long xlPivotTableVersion12 As UInt16 = 3 'Excel 2007
    public static long xlPivotTableVersion14 As UInt16 = 4 'Excel 2010*/
    public static long xlRowField = 1;
    public static long xlColumnField = 2;
    public static long xlPageField = 3;
    public static long xlCount = -4112;

    //Page sutup
    public static long xlLandscape = 2;
    public static long xlPaperA4 = 9;

    //Colors
    public static long xlBlack = 0;
    public static long xlWhite = 16777215;
    public static long xlRed = -16776961;
    public static long xlPink = 13421823;
    public static long xlDimGray = 3815994;

    //Cell comments
    public static long msoFalse = 0;
    public static long msoScaleFromTopLeft = 0;

    //PasteSpecial
    public static long xlPasteFormulas = -4123;
    public static long xlPasteAll = -4104;
  }
}
