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
    public partial class BoLWarehouse : UserControl//UserControl // SmuOk_.Common.MyComponent
    {
        public BoLWarehouse()
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
            string q = "select InvId,InvType,InvINN,InvLegalName,InvNum,InvDate,InvComment" +
                      " from InvDoc id" +
                      " outer apply (select sum(ICQty * ICPrc)c from InvCfm ic where ic.InvDocId = id.InvId)q";

            string sName = txtSpecNameFilter.Text;
            q += " where 1=1 ";
            if (sName != "" && sName != txtSpecNameFilter.Tag.ToString())
            {
                q += " and InvNum = '" + sName + "'";
            }

            MyFillDgv(dgvBudg, q);
        }

        private void FillInvDocFilling()
        {
            string q = "select b.BoLDoCFillingId,b.No1,b.No2,b.Amount as BoLAmount,i.InvDocPosId,Name,Unit " +
                       "from InvDocFilling_new i " +
                       "left join BoLDocFilling b on b.InvDocPosId = i.InvDocPosId " +
                       "where InvDocId = " + EntityId.ToString();

            MyFillDgv(dgvInvFilling, q);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MyExcelInvDocReport(EntityId);
            return;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            dynamic oExcel;
            dynamic oSheet;
            bool bNoError = MyExcelImportOpenDialog(out oExcel, out oSheet, "");

            if (!bNoError) return;

            /*if (bNoError) bNoError = MyExcelImport_CheckTitle(oSheet, FillingReportStructure, pb);
            if (bNoError) MyExcelUnmerge(oSheet);

            if (bNoError) bNoError = MyExcelImport_CheckValues(oSheet, FillingReportStructure, pb);
            if (bNoError) bNoError = FillingImportCheckInvIds(oSheet);*/

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

        private bool check_is_lst(string str)
        {
            int n;
            foreach (char c in str)
            {
                if (c == ',')
                {
                    return true;
                }
            }
            return false;
        }

            private void FillingImportData(dynamic oSheet)
        {
            string invType, invINN, invLegalName, invNum, invDate, invComment, specLstStr;
            decimal invSumWOVAT, invSumWithVAT;
            long InvId;
            int r = 24;
            invType = oSheet.Cells(2, 6).Value?.ToString() ?? "";
            invNum = oSheet.Cells(3, 6).Value?.ToString() ?? "";
            invINN = oSheet.Cells(4, 6).Value?.ToString() ?? "";
            invLegalName = oSheet.Cells(5, 6).Value?.ToString() ?? "";
            invDate = oSheet.Cells(6, 6).Value?.ToString() ?? "";
            invComment = oSheet.Cells(11, 6).Value?.ToString() ?? "";
            long.TryParse(oSheet.Cells(13, 6).Value?.ToString() ?? "", out InvId);
            Decimal.TryParse(oSheet.Cells(7, 6).Value?.ToString() ?? "", out invSumWOVAT);
            Decimal.TryParse(oSheet.Cells(8, 6).Value?.ToString() ?? "", out invSumWithVAT);
            specLstStr = oSheet.Cells(9, 6).Value?.ToString() ?? "";

            string upd_q = "update InvDoc set " +
                           "InvType = " + MyES(invType) +
                           ",InvNum = " + MyES(invNum) +
                           ",InvINN = " + MyES(invINN) +
                           ",InvLegalName = " + MyES(invLegalName) +
                           ",InvDate = " + MyES(invDate) +
                           ",InvComment = " + MyES(invComment) +
                           ",InvSumWOVAT = " + MyES(invSumWOVAT) +
                           ",InvSumWithVAT = " + MyES(invSumWithVAT) +
                           " where InvId = " + InvId;
            MyExecute(upd_q); //добавить удаление старых списков спецификаций или запретить изменение после первой загрузки или придумать новый id


            string del_q = "delete from SpecLstForInvDoc where InvDocId = " + InvId;
            MyExecute(del_q);
            string insSpecLst_q = "insert into SpecLstForInvDoc (InvDocId, SpecId) values ";
            if (check_is_lst(specLstStr))
            {
                string[] specLst = specLstStr.Split(',');
                foreach (string id in specLst)
                {
                    insSpecLst_q += "(" + MyES(InvId) + "," + id + ") ";
                }
            }
            else
            {
                insSpecLst_q += "(" + MyES(InvId) + "," + specLstStr + ") ";
            }
            insSpecLst_q += "; select SCOPE_IDENTITY();";
            string newLstId = MyGetOneValue(insSpecLst_q).ToString();

            // ниже импорт табличных данных

            while ((oSheet.Cells(r, 4).Value?.ToString() ?? "") != "") //до пустой строки
            {
                string No1, No2, Name, Unit, Amount_str, PriceWOVAT_str, Price_str, TotalSum_str, InvDocPosId;
                decimal Amount, PriceWOVAT, Price, TotalSum;
                No1 = oSheet.Cells(r, 4).Value?.ToString() ?? "";
                No2 = oSheet.Cells(r, 5).Value?.ToString() ?? "";
                Name = oSheet.Cells(r, 6).Value?.ToString() ?? "";
                Unit = oSheet.Cells(r, 7).Value?.ToString() ?? "";
                Amount_str = oSheet.Cells(r, 8).Value?.ToString() ?? "0";
                if (!decimal.TryParse(Amount_str, out Amount)) Amount = 0;
                PriceWOVAT_str = oSheet.Cells(r, 9).Value?.ToString() ?? "0";
                if (!decimal.TryParse(PriceWOVAT_str, out PriceWOVAT)) PriceWOVAT = 0;
                Price_str = oSheet.Cells(r, 10).Value?.ToString() ?? "0";
                if (!decimal.TryParse(Price_str, out Price)) Price = 0;
                TotalSum_str = oSheet.Cells(r, 11).Value?.ToString() ?? "0";
                if (!decimal.TryParse(TotalSum_str, out TotalSum)) TotalSum = 0;
                InvDocPosId = oSheet.Cells(r, 1).Value?.ToString() ?? "";
                if(InvDocPosId != "")
                {
                    upd_q = " update InvDocFilling_new set " +
                            " No1 = " + MyES(No1) +
                            ",No2 = " + MyES(No2) +
                            ",Name = " + MyES(Name) +
                            ",Unit = " + MyES(Unit) +
                            ",Amount = " + MyES(Amount) +
                            ",PriceWOVAT = " + MyES(PriceWOVAT) +
                            ",TotalSum = " + MyES(TotalSum) +
                            ",Price = " + MyES(Price) +
                            " where InvDocPosId = " + InvDocPosId;
                    MyExecute(upd_q);
                }   
                else
                {
                    string ins_q = "insert into InvDocFilling_new (No1, No2, Name, Unit, Amount, PriceWOVAT, Price, TotalSum, InvDocId) values " +
                                   "(" + MyES(No1) + "," + MyES(No2) + "," + MyES(Name) + "," + MyES(Unit) + "," + MyES(Amount) + "," + MyES(PriceWOVAT) + "," + MyES(Price) + "," + MyES(TotalSum) + "," + InvId + ")";
                    MyExecute(ins_q);
                }
                r++;
            }

            MyProgressUpdate(pb, 95, "Импорт данных");
            return;
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
                foreach (string sss in ss)
                {
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
            q = "delete from InvDoc where InvId in ( " + OrderId.Text + " );";
            MyExecute(q);
            fill_dgv();
            MsgBox("OK");
            OrderId.Text = "";
            return;
        }

        private void addDoc_btn_Click(object sender, EventArgs e)
        {
            string ins_q = "insert into InvDoc (InvType) values(null)";

            MyExecute(ins_q);
            MsgBox("Ok");
            fill_dgv();
            return;
        }

        private void dgvBudg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FormIsUpdating) return;
            if (e.RowIndex >= 0)
            {
                EntityId = (long)dgvBudg.Rows[e.RowIndex].Cells["dgv_InvId"].Value;
                FillInvDocFilling();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            return; //тут добавить перенос остатков в стоки склада
        }
    }
}
