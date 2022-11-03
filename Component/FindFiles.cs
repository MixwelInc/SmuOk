using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static SmuOk.Common.DB;

namespace SmuOk.Component
{
  public partial class FindFiles : UserControl
  {
    public FindFiles()
    {
      InitializeComponent();
    }

    private void FindFiles_Load(object sender, EventArgs e)
    {
      LoadMe();
    }

    public void LoadMe()
    {
      dgvFiles.Columns["dgvPTOPathFile"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 11);
      dgvFiles.Columns["dgvPTOPathFolder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      dgvFiles.Columns["dgvPTOPathCopy"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings 2", 10);
    }

      private void txtFindFiles_Enter(object sender=null, EventArgs e=null)
    {
      if (txtFindFiles.Text == txtFindFiles.Tag.ToString())
      {
        txtFindFiles.Text = "";
      }
      txtFindFiles.ForeColor = Color.FromKnownColor(KnownColor.Black);
    }

    private void txtFindFiles_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        txtFindFiles.Text = "";
        txtFindFiles_Enter();
        FillFindFiles();
      }
      if (e.KeyCode == Keys.Enter)
      {
        if (txtFindFiles.Text == "")
        {
          dgvFiles.Rows.Clear();
          return;
        }
        FillFindFiles();
      }
    }

    private void txtFindFiles_Leave(object sender, EventArgs e)
    {
      if (txtFindFiles.Text == "")
      {
        txtFindFiles.Text = txtFindFiles.Tag.ToString();
      }
      txtFindFiles.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
    }

    private void FillFindFiles()
    {
      dgvFiles.Rows.Clear();
      if (txtFindFiles.Text == txtFindFiles.Tag.ToString() || txtFindFiles.Text == "") return;
      string q = "Select EDIDir,EDIIsFile From _engDirectoryIndex where EDILastLeaf like " + MyES(txtFindFiles.Text, true);
      string s, sExt;
      string[,] vals = MyGet2DArray(q);
      if (vals == null) return;
      for (int i = 0; i < vals.GetLength(0); i++)
      {
        if (vals[i, 1] == "0")
        {
          dgvFiles.Rows.Add(Properties.Resources.dot, Properties.Resources.shared, vals[i, 0]);
        }
        else
        {
          s = vals[i, 0];
          if (s.LastIndexOf(".") < s.LastIndexOf("\\"))
          {
            dgvFiles.Rows.Add(Properties.Resources.document, Properties.Resources.shared, vals[i, 0]);
          }
          else
          {
            sExt = s.Substring(s.LastIndexOf(".")).ToLower();
            switch (sExt)
            {
              case ".pdf":
                dgvFiles.Rows.Add(Properties.Resources.document_pdf, Properties.Resources.shared, vals[i, 0]);
                break;
              case ".doc":
              case ".docx":
                dgvFiles.Rows.Add(Properties.Resources.document_word, Properties.Resources.shared, vals[i, 0]);
                break;
              case ".xls":
              case ".xlsx":
                dgvFiles.Rows.Add(Properties.Resources.document_excel, Properties.Resources.shared, vals[i, 0]);
                break;
              case ".jpg":
              case ".jpeg":
              case ".bmp":
              case ".gif":
              case ".png":
                dgvFiles.Rows.Add(Properties.Resources.image, Properties.Resources.shared, vals[i, 0]);
                break;
              default:
                dgvFiles.Rows.Add(Properties.Resources.document, Properties.Resources.shared, vals[i, 0]);
                break;
            }
          }


        }
        //dgv.Columns[vals[i, 0]].Width = int.Parse(vals[i, 1]);
      }
    }

    private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0)
      {
        try
        {
          string s = "";
          s = dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPath"].Value.ToString();
          switch (dgvFiles.Columns[e.ColumnIndex].Name)
          {
            case "dgvPTOPathFile":
              // кликнули на файл -- проверяем, что там файл
              if (dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPathFile"].Value == Properties.Resources.dot) return;
              Process.Start(s);
              break;
            case "dgvPTOPathFolder":
              // кликнули на папку -- отрезаем файл при необходимости
              if (dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPathFile"].Value != Properties.Resources.dot)
              {
                s = s.Substring(0, s.LastIndexOf('\\'));
              }
              Process.Start(s);
              break;
            case "dgvPTOPathCopy":
              StringCollection files_to_copy = new StringCollection();
              files_to_copy.Add(s);
              Clipboard.SetFileDropList(files_to_copy);
              if (dgvFiles.Rows[e.RowIndex].Cells["dgvPTOPathFile"].Value != Properties.Resources.dot)
                MsgBox("Файл скопирован в буфер.\n\nМожете вставить его в любую папку, нажав Ctrl+V.");
              else //не работает так((( спать хочу, когда-нибудь потом сделаю, может быть
                MsgBox("Папка скопирована в буфер.\n\nМожете вставить ее в любую папку, нажав Ctrl+V.");
              break;
          }
        }
        catch (Exception ex)
        {
          MsgBox("Что-то не работает(((");
        }
      }
    }

    private void dgvFiles_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0) return;
      dgvFiles.Rows[e.RowIndex].Selected = true;
      /*dgvFiles.ReadOnly = false;
      dgvFiles.CurrentCell = dgvFiles.Rows[e.RowIndex].Cells[e.ColumnIndex];
      dgvFiles.ReadOnly = true;*/
      //Cursor = Cursors.Hand;
    }

        private void button4_Click(object sender = null, EventArgs e = null)
        {
            if(txtPIDFilter.Text.ToString() == "")
            {
                MsgBox("Введите PID");
                return;
            }
            else
            {
                string q = "SELECT SFSupplyPID,SId,SVName,SFName,SFMark,SFUnit,M15Num,M15Date,M15Price " +
                       "FROM vwSpec vws " +
                       "left join SpecFill sf on sf.SFSpecVer = vws.SVId " +
                       "left join M15 on FillId = SFId " +
                       "where SFSupplyPID in (" + txtPIDFilter.Text.ToString() + ")";
                MyFillDgv(dgvPIDSearch, q);
            }
        }

        private void txtPIDFilter_Enter(object sender = null, EventArgs e = null)
        {
            if (txtPIDFilter.Text == txtPIDFilter.Tag.ToString())
            {
                txtPIDFilter.Text = "";
            }
            txtPIDFilter.ForeColor = Color.FromKnownColor(KnownColor.Black);
        }

        private void txtPIDFilter_Leave(object sender = null, EventArgs e = null)
        {
            if (txtPIDFilter.Text == "")
            {
                txtPIDFilter.Text = txtPIDFilter.Tag.ToString();
            }
            txtPIDFilter.ForeColor = Color.FromKnownColor(KnownColor.DimGray);
        }

        private void txtPIDFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtPIDFilter.Text = "";
            }
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click();
            }
        }
    }
}
