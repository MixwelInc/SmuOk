using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.IO;
using static System.String;
using static SmuOk.Common.MyConst;
using System.Configuration;

namespace SmuOk.Common
{
  static class DB
  {
    public static string uname;
    public static long uid;

    public class MyItem
    {
      public string Text { get; set; }
      //public string ID { get; set; }
      public long ID { get; set; }
      public bool Checked { get; set; }
      public MyItem(long i, string s)
      {
        Text = s;
        ID = i;
        Checked = false;
      }
      public MyItem(long i, string s, bool ch)
      {
        Text = s;
        ID = i;
        Checked = ch;
      }
    }

    public static Type MyExcelType()
    {
      Exception ex = new Exception("Не найдена известная версия Excel.");
      Type xls = Type.GetTypeFromProgID("Excel.Application") ?? Type.GetTypeFromProgID("Excel.Application.14") ?? Type.GetTypeFromProgID("Excel.Application.16");
      if (xls == null)
      {
        TechLog("Не найдена известная версия Excel.");
        throw ex;
      }
      return xls;
    }

    public static void TechLog(string s)
    {
      MyExecute("insert into _engTechLog (ETLDBUser, ETLAction) values ("+uid.ToString()+"," + MyES(s) +")");
      return;
    }

    public enum DBError { OK, NoRecords, DBOrNetError }

    public static DialogResult MsgBox(string s, string caption = "", MessageBoxIcon icon = MessageBoxIcon.None,MessageBoxButtons mbb = MessageBoxButtons.OK) { return MessageBox.Show(s, caption, mbb, icon);  }

    public static int MyExecute(string sQuery)
    {
      try
      {
        using (SqlConnection con = new SqlConnection(ConStr()))
        {
          con.Open();
          using (SqlCommand Com = new SqlCommand(sQuery, con))
          {
            return Com.ExecuteNonQuery();
          }
        }
      }
      catch (System.Exception ex)
      {
        MessageBox.Show(ex.Message);
        return -2;
      }
    }

    public static Object MyGetOneValue(string sQuery/*, out DBError Err, string sType = "String"*/)
    {
      Object ret;
      try
      {
        using (SqlConnection con = new SqlConnection(ConStr()))
        {
          con.Open();
          using (SqlCommand com = new SqlCommand(sQuery, con))
          {
            ret = com.ExecuteScalar();
            return ret;
          }
        }
      }
      catch (Exception ex)
      {
        MsgBox(ex.Message + "\n\n" + sQuery);
        return null;
      }
    }

    public static List<String> MyGetOneRow(string sQuery)
    {
      List<String> ret = new List<String>();
      try
      {
        using (SqlConnection con = new SqlConnection(ConStr()))
        {
          con.Open();
          using (SqlCommand com = new SqlCommand(sQuery, con))
          {
            using (SqlDataReader r = com.ExecuteReader())
            {
              if (r.HasRows)
              {
                r.Read();
                for (int i = 0; i < r.FieldCount; i++)
                {
                  ret.Add(r.GetValue(i).ToString());
                }
              }
            }
            return ret;
          }
        }
      }
      catch (Exception ex)
      {
        MsgBox(ex.Message + "\n\n" + sQuery);
        return null;
      }
    }

    public static List<String> MyGetOneCol(string sQuery)
    {
      List<String> ret = new List<String>();
      try
      {
        using (SqlConnection con = new SqlConnection(ConStr()))
        {
          con.Open();
          using (SqlCommand com = new SqlCommand(sQuery, con))
          {
            using (SqlDataReader r = com.ExecuteReader())
            {
              while (r.Read()) ret.Add(r.GetValue(0).ToString());
            }
            return ret;
          }
        }
      }
      catch (Exception ex)
      {
        MsgBox(ex.Message + "\n\n" + sQuery);
        return null;
      }
    }

        public static DataTable loadExternalDataSet(string sQuery)
        {
            DataTable dt = new DataTable();
            //string con = ConStr();
            try
            {
                //SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
                //adapter.Fill(dt);
                /*SqlConnection con = new SqlConnection(ConStr());
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand();
                command.CommandText = sQuery;
                command.CommandType = CommandType.Text;
                command.Connection = con;
                adapter.SelectCommand = command;
                //con.Open();                       
                adapter.SelectCommand.ExecuteNonQuery();
                adapter.Fill(dt);
                con.Close();*/
                using (SqlConnection con = new SqlConnection(ConStr()))
                {
                    con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
                    //DataTable dt = new DataTable();
                    //adapter.SelectCommand = new SqlCommand(sQuery, con);
                    //adapter.SelectCommand.ExecuteNonQuery();
                            adapter.Fill(dt);
                            con.Close();
                            adapter.Dispose();
                        
                   
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.Message + "\n\n" + sQuery);
                return null;
            }
            return dt;
        }

        public static string [,] MyGet2DArray(string sQuery, bool ShowColTitles = false)
    {
      //string ret[][]= { };
      //List<string[]> ret = new List<string[]>();
      //List<string> ss = new List<string>();
      // = { };
      List<string> s = new List<string>();
      int cols = 0;
      try
      {
        using (SqlConnection con = new SqlConnection(ConStr()))
        {
          con.Open();
          using (SqlCommand com = new SqlCommand(sQuery, con))
          {
            using (SqlDataReader r = com.ExecuteReader())
            {
              if (!r.HasRows) return null;
              cols = r.FieldCount;
              if (ShowColTitles){for (int i = 0; i < cols; i++) s.Add(r.GetName(i).Replace("\\n","\n"));}
              while (r.Read()){
                for (int i = 0; i < cols; i++)
                {
                  if(r.GetFieldType(i).Name=="Decimal") s.Add(r.GetValue(i).GetType().Name == "DBNull"?"": r.GetDecimal(i).ToString().Replace(',','.'));
                  else s.Add(r.GetValue(i).ToString());
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        MsgBox(ex.Message + "\n\n" + sQuery);
        return null;
      }
      //string[,] rez = new string[s.Count / cols, cols];
      return toRectangular<string>(s.ToArray(), cols);
    }

    private static T[,] toRectangular<T>(T[] flatArray, int width)
    {
      int height = (int)Math.Ceiling(flatArray.Length / (double)width);
      T[,] result = new T[height, width];
      int rowIndex, colIndex;

      for (int index = 0; index < flatArray.Length; index++)
      {
        rowIndex = index / width;
        colIndex = index % width;
        result[rowIndex, colIndex] = flatArray[index];
      }
      return result;
    }

    public static void MyFillDictionary(Dictionary<long, string> dct, string sQuery)
    {
      dct.Clear();
      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlCommand com = new SqlCommand(sQuery, con))
        {
          using (SqlDataReader r = com.ExecuteReader())
          {
            while (r.Read())
            {
              dct.Add((long)r[0], r[1].ToString());
            }
          }
        }
      }
    }

    public static bool MyFillList(CheckedListBox lst, string sQuery)
    {
      ArrayList MyLstData = new ArrayList();
      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlCommand com = new SqlCommand(sQuery, con))
        {
          using (SqlDataReader r = com.ExecuteReader())
          {
            while (r.Read())
            {
              MyLstData.Add(new MyItem((long)r[0], r[1].ToString(), r.GetInt32(2)==1));
            }
            lst.DataSource = MyLstData;
            lst.DisplayMember = "Text";
            lst.ValueMember = "ID";
            for(int i=0;i< lst.Items.Count; i++)
            {
              lst.SetItemChecked(i, ((MyItem)MyLstData[i]).Checked);
            }
          }
        }
      }
      if (lst.GetType().ToString() == "System.Windows.Forms.ComboBox")
      {
        lst.SelectedIndex = 0;
      }
      else lst.SelectedIndex = -1;
      return true;
    }

    public static bool MyFillList(DataGridViewComboBoxColumn col, string sQuery, string sAllValue = "")
    {
      DataGridView dgv = col.DataGridView;
      int ind = col.Index;
      string n = col.Name;

      DataGridViewComboBoxColumn c = (DataGridViewComboBoxColumn)col.Clone();
      c.DataSource = null;

      List<MyItem> MyLstData = new List<MyItem>();

      if (sAllValue != "") MyLstData.Add(new MyItem(0, sAllValue));

      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlCommand com = new SqlCommand(sQuery, con))
        {
          using (SqlDataReader r = com.ExecuteReader())
          {
            while (r.Read())
            {
              MyLstData.Add(new MyItem((long)r[0], r[1].ToString()));
            }
            c.DataSource = MyLstData;
            c.DisplayMember = "Text";
            c.ValueMember = "ID"; //SFType
            //lst.DataPropertyName = "ID"; //
          }
        }
      }

      dgv.Columns.Insert(ind - 1, c);
      dgv.Columns.Remove(col);

      return true;
    }

    public static bool MyFillListSimple(ComboBox lst, string sQuery, string sAllValue = "")
    {
      lst.Items.Clear();
      List<string> MyLstData = new List<string>();
      if (sAllValue != "") MyLstData.Add(sAllValue);
      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlCommand com = new SqlCommand(sQuery, con))
        {
          using (SqlDataReader r = com.ExecuteReader())
          {
            while (r.Read())
            {
              MyLstData.Add(r[0].ToString());
            }
            lst.Items.AddRange(MyLstData.ToArray());
          }
        }
      }
      if (lst.Items.Count>0) lst.SelectedIndex = 0;
      return true;
    }

    public static bool MyFillList(ListControl lst, string sQuery, string sAllValue = "")
    {
      List<MyItem> MyLstData = new List<MyItem>();

      if (sAllValue != "") MyLstData.Add(new MyItem(0, sAllValue));

      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlCommand com = new SqlCommand(sQuery, con))
        {
          using (SqlDataReader r = com.ExecuteReader())
          {
            while (r.Read())
            {
              MyLstData.Add(new MyItem((long)r[0], r[1].ToString()));
            }
            lst.DataSource = MyLstData;
            lst.DisplayMember = "Text";
            lst.ValueMember = "ID";
          }
        }
      }
      if (lst.GetType().ToString() == "System.Windows.Forms.ComboBox")
      {
        if (((System.Windows.Forms.ComboBox)lst).Items.Count>0) lst.SelectedIndex = 0;
      }
      else lst.SelectedIndex = -1;
      return true;
    }

    public static void SelectItemByValue(this ListBox lst, object val)
    {
      for (int i = 0; i < lst.Items.Count; i++)
      {
        var prop = lst.Items[i].GetType().GetProperty(lst.ValueMember);
        if (prop != null && prop.GetValue(lst.Items[i], null).ToString() == val.ToString())
        {
          lst.SelectedIndex = i;
          break;
        }
      }
    }

    public static void SelectItemByValue(this ComboBox lst, object val)
    {
      for (int i=0; i<lst.Items.Count; i++)
      {
        var prop = lst.Items[i].GetType().GetProperty(lst.ValueMember);
        if(prop!=null && prop.GetValue(lst.Items[i], null).ToString()==val.ToString())
        {
          lst.SelectedIndex = i;
          break;
        }
      }
    }

    public static void MyMoveSelectedItemFromListToList(ListControl lstFrom, ListControl lstTo)
    {
      MyItem itm;
      switch (lstFrom.GetType().ToString())
      {
        case "System.Windows.Forms.ListBox":
          itm = new MyItem(((MyItem)((ListBox)lstFrom).SelectedItem).ID, ((MyItem)((ListBox)lstFrom).SelectedItem).Text);
          ((ListBox)lstFrom).SelectedIndex = 0;
          break;
        case "System.Windows.Forms.ComboBox":
          itm = new MyItem(((MyItem)((ComboBox)lstFrom).SelectedItem).ID, ((MyItem)((ComboBox)lstFrom).SelectedItem).Text);
          ((ComboBox)lstFrom).SelectedIndex = 0;
        break;
        default:
          return;
      }

      List<MyItem> liFrom = new List<MyItem>((List<MyItem>)lstFrom.DataSource);
      List<MyItem> liTo = new List<MyItem>((List<MyItem>)lstTo.DataSource);

      liTo.Add(itm);
      for(int i=0;i<liFrom.Count;i++)
      {
        if (liFrom[i].ID == itm.ID)
        {
          liFrom.RemoveAt(i);
          break;
        }
      }
      //liFrom.Remove(itm);


      lstTo.DataSource = null;
      lstTo.DataSource = liTo;
      lstTo.DisplayMember = "Text";
      lstTo.ValueMember = "ID";

      lstFrom.DataSource = null;
      lstFrom.DataSource = liFrom;
      lstFrom.DisplayMember = "Text";
      lstFrom.ValueMember = "ID";
      return;
    }

    public static string GetDataColumnForDB(this ListBox lst)
    {
      string s="";
      if (lst.Items.Count > 0)
      {
        for (int i = 0; i < lst.Items.Count; i++)
        {
          s += ((MyItem)lst.Items[i]).ID + ",";
        }
      }
      return s;
    }

    public static long MyFillDgv(DataGridView dgv, string sQuery /*,sWhere*/ )
    {
      long rez = 0;
      //int s_col = -1;
      //System.Windows.Forms.SortOrder so=dgv.SortOrder;
      //if (dgv.SortedColumn != null) {
      //  s_col = dgv.SortedColumn.Index;
      //}
      //try
      //{
        using (SqlConnection con = new SqlConnection(ConStr()))
        {
          con.Open();
          using (SqlDataAdapter da = new SqlDataAdapter(sQuery, con))
          {
            DataTable tbl = new DataTable();
            rez = da.Fill(tbl);
            dgv.DataSource = tbl;
          }
        }
      //}
      //catch (Exception ex) { MsgBox(ex.Message); }
      return rez;
    }

    public static long MyFillDgvPivot(DataGridView dgv, string sQuery /*,sWhere*/ )
    {
      /*
      Заполнение:
      1. Получаем выборку
      2. Создаем столбцы (col_i)
      3. Заполняем строки, только чекбоксы в этот раз
       */
      long rez = 0;
      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlDataAdapter da = new SqlDataAdapter(sQuery, con))
        {
          DataTable tbl = new DataTable();
          rez = da.Fill(tbl);
          dgv.DataSource = tbl;
        }
      }
      return rez;
    }

    private static void MyRowAdd(DataGridView dgv)
    {
      List<object> cols = new List<object>();
      for (int i = 0; i < dgv.Columns.Count - 1; i++)
      {
        if (!dgv.Columns[i].Name.StartsWith("dgv_btn_save_")) cols.Add(null);
      }
      ((DataTable)dgv.DataSource).Rows.Add(cols.ToArray());
      MyDgvMarkRow(dgv, dgv.Rows.Count - 1);
      dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;
      return;
    }

    public static void MyRowAdd(this object dgv)
    {
      if (dgv.GetType().ToString() == "System.Windows.Forms.DataGridView")
        MyRowAdd((DataGridView)dgv);
      return;
    }

    public static void MyDataError(this object dgv, DataGridViewDataErrorEventArgs e)
    {
      string s = ((DataGridView)dgv).Columns[e.ColumnIndex].DefaultCellStyle.Format;
      if (s == "d") MsgBox("Введите дату в формате день-месяц-год, например:\n31-12-20.", "", MessageBoxIcon.Exclamation);
      else MsgBox("Введите данные.", "", MessageBoxIcon.Exclamation);
      return;
    }

    public static long MySaveDgv(DataGridView dgv, string sTableTo)
    {
      return 0;
    }

    public static void MyCellValueChanged(object sender, DataGridViewCellEventArgs e, ref bool FormIsUpdating)
    {
      if (FormIsUpdating) return;
      DataGridView dgv = (DataGridView)sender;
      FormIsUpdating = true;
      foreach (DataGridViewCell c in dgv.Rows[e.RowIndex].Cells)
      {
        if (dgv.Columns[c.ColumnIndex].Name.StartsWith("dgv_btn_save"))
        {
          c.Value = Properties.Resources.save;
          c.Tag = "update";
          break;
        }
      }
      MyDgvMarkRow(dgv, e.RowIndex);
      FormIsUpdating = false;
    }

    public static void MyOpenSpecFolder(long lSId)
    {
      //long lSId = (long)dgv.Rows[e.RowIndex].Cells["dgv_id_SId"].Value;

      string sFolder = MyGetOneValue("select EOValue from _engOptions where EOName='DataFolder'").ToString();
      if (!sFolder.EndsWith("\\")) sFolder += "\\";
      sFolder += lSId.ToString();
      string sTxtName = MyGetOneValue("select SVName from vwSpec where SId=" + lSId).ToString();
      sTxtName = sFolder + "\\" + MakeValidFileName(sTxtName) + ".txt";

      string sVerNo = "\\Версия " + MyGetOneValue("select SVNo from vwSpec where SId=" + lSId).ToString();// dgv.Rows[e.RowIndex].Cells["dgv_SVNo"].Value.ToString();

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
      System.Diagnostics.Process.Start(sFolder);
    }

    public static bool MyCellContentClick(object sender, DataGridViewCellEventArgs e, bool SayOK = false)
    {
      /// <summary>
      /// клик по кнопке save в столбце dgv_btn_save_[table]
      /// </summary>
      if (e.RowIndex < 0) return false;
      DataGridView dgv = ((DataGridView)sender);
      if ((dgv.Tag?.ToString() ?? "") == "block") return false;

      if ((dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag?.ToString() ?? "") == "update")
      {
        //catch(Exception ex);
        long rez = MySaveDgvRow(dgv, e.RowIndex, dgv.Name.Substring(3));
        if (rez < 0) return false;
        if (SayOK) MsgBox("Ok");
        DataGridViewImageCell ic = new DataGridViewImageCell();
        ic.Value = Properties.Resources.dot;
        ic.Tag = null;
        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] = ic;// 
      }
      return true;
    }

    public static void MyClearRows(this DataGridView dgv)
    {
      if (dgv.DataSource != null) ((DataTable)dgv.DataSource).Rows.Clear();
      //else dgv.Rows.Clear();
      return;
    }

    public static void MySaveColWidthForUser(this DataGridView dgv, long uid, DataGridViewColumnEventArgs e)
    {
      if (e.Column.AutoSizeMode != DataGridViewAutoSizeColumnMode.None) return;
      string dgvName = dgv.Name;
      string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + dgvName + "' and EUIOOption = '" + e.Column.Name + "';\n";
      q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values ("+uid+",'"+ dgvName + "','"+ e.Column.Name + "',"+e.Column.Width.ToString()+")";
      MyExecute(q);
    }

    public static void MyRestoreColWidthForUser(this DataGridView dgv, long uid)
    {
      string q = "Select EUIOOption,EUIOVaue from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + dgv.Name+"';";
      string[,] vals = MyGet2DArray(q);
      if (vals == null) return;
      for (int i = 0; i < vals.GetLength(0); i++)
      {
        if (dgv.Columns[vals[i, 0]] != null) {
          try
          {
            dgv.Columns[vals[i, 0]].Width = int.Parse(vals[i, 1]);
          }
          catch (Exception ex) { }
        }
      }
    }

    public static long MySaveDgvRow(DataGridView dgv, int iRowIndex, string sTableTo="")
    {
      string sQuery;
      List<string> sf = new List<string>();
      List<string> sv = new List<string>();
      long id = 0;
      int id_col = -1;
      string sIdFieldName = "";
      if (sTableTo == "") sTableTo = dgv.Name.Substring(4);
      for (int c = 0; c < dgv.Columns.Count; c++)
      {
        //DataGridCell f = dgv.Rows[iRowIndex].Cells(z);
        if (dgv.Columns[c].ToolTipText == MyToolTipText && dgv.Rows[iRowIndex].Cells[c].Value.ToString() == "")
        {
          MsgBox("Ведите обязательные значения.", "Требуются данные", MessageBoxIcon.Exclamation);
          return -1;
        }
        if (dgv.Columns[c].Name.StartsWith("dgv__"))
        {
          sf.Add(dgv.Columns[c].DataPropertyName);
          if (dgv.Rows[iRowIndex].Cells[c].ValueType.ToString() == "System.Decimal") sv.Add(dgv.Rows[iRowIndex].Cells[c].Value.ToString().Replace(',', '.'));
          else sv.Add(dgv.Rows[iRowIndex].Cells[c].Value==null?"": dgv.Rows[iRowIndex].Cells[c].Value.ToString());
        }
        else if (dgv.Columns[c].Name.StartsWith("dgv_id_"))
        {
          if (dgv.Rows[iRowIndex].Cells[c].Value.ToString() == "") id = -1; // add new
          else id = (long)dgv.Rows[iRowIndex].Cells[c].Value;
          id_col = c;
          sIdFieldName = dgv.Columns[c].Name.Substring(7);
        }
        //else if (dgv.Columns[c].Name== "dgv_btn_save")
        //{
        //  sSqlCmd = dgv.Rows[iRowIndex].Cells[c].Tag.ToString();
        //}
      }

      if (id == 0) return -1;

      if (id > 0) // update
      {
        sQuery = "Update " + sTableTo + " Set";
        for (int i = 0; i < sf.Count; i++)
        {
          if (sv[i] == "") sQuery += "\n" + sf[i] + "=null,";
          else sQuery += "\n" + sf[i] + "=" + MyES(sv[i]) + ",";
        }
        sQuery = sQuery.Remove(sQuery.Length - 1);
        sQuery += "\nWhere " + sIdFieldName + "=" + id.ToString();
        MyExecute(sQuery);
        MyLogStandart(dgv, "update", sTableTo, id);
        return id;
      }

      if (id == -1) // insert
      {
        long iNewId;
        sQuery = "insert into " + sTableTo + "(\n";
        for (int i = 0; i < sf.Count; i++) sQuery += sf[i] + ",";
        sQuery = sQuery.Remove(sQuery.Length - 1);
        sQuery += "\n) values (\n";
        for (int i = 0; i < sv.Count; i++)
        {
          if (sv[i] == "") sQuery += "null,";
          else sQuery += MyES(sv[i]) + ",";
        }
        sQuery = sQuery.Remove(sQuery.Length - 1);
        sQuery += "); select cast(scope_identity() as bigint) new_id;";
        iNewId = (long)MyGetOneValue(sQuery);
        dgv.Rows[iRowIndex].Cells[id_col].Value = iNewId;
        MyLogStandart(dgv, "new", sTableTo, iNewId);
        return iNewId;
      }
      return -2;
    }

    public static void MyDgvMarkRow(DataGridView dgv, int iRowIndex)
    {
      for (int c = 0; c < dgv.Columns.Count; c++)
      {
        if (dgv.Columns[c].ToolTipText == MyToolTipText && (dgv.Rows[iRowIndex].Cells[c].Value.ToString()=="" /*|| dgv.Rows[iRowIndex].Cells[c].Value == null*/))
        {
          dgv.Rows[iRowIndex].Cells[c].Style.BackColor = System.Drawing.Color.Pink;
        }
        else
        {
          dgv.Rows[iRowIndex].Cells[c].Style.BackColor = System.Drawing.Color.White;
        }
      }
    }

    public static string GetLstText(this ComboBox lst)
    {
      return ((MyItem)lst.SelectedItem).Text.ToString();
    }

    public static long GetLstVal(this ComboBox lst)
    {
      return ((MyItem)lst.SelectedItem).ID;
    }

    public static long GetLstVal(this CheckedListBox lst)
    {
      return ((MyItem)lst.SelectedItem).ID;
    }

    public static void MyEnableForm(Control f, string sPref, bool bEnable)
    {
      foreach (Control c in f.Controls)
      {
        if (!c.Name.StartsWith(sPref)) continue;
        c.Enabled = bEnable;
      }
    }

    public static void MyClearForm(Control f, string sPref)
    {
      foreach (Control c in f.Controls)
      {
        if (!c.Name.StartsWith(sPref)) continue;
        switch (c.GetType().ToString())
        {
          case "System.Windows.Forms.TextBox":
            if(c.Tag == null) { c.Text = ""; }
            else if(c.Tag.ToString() == "c") { c.Text = MyGetStrCurrency("0"); }
            else if(c.Tag.ToString() == "q") { c.Text = Format("0", "#,##0"); }
            else { c.Text = ""; }
            break;
          case "System.Windows.Forms.ComboBox":
            if (((ComboBox)c).Items.Count > 0)
            { 
              if(((ComboBox)c).ValueMember=="") ((ComboBox)c).Text = "";
              else ((ComboBox)c).SelectedIndex = 0;
            }
            break;
          case "System.Windows.Forms.DateTimePicker":
            ((DateTimePicker)c).Text = "";
            break;
          case "System.Windows.Forms.CheckBox":
            ((CheckBox)c).Checked = false;
            break;
        }
      }
      return;
    }

    public static void MyFillForm(Control f, string FormControlPref,string FormSqlPref, string sQuery)
    {
      Control c;
      string s;
      using (SqlConnection con = new SqlConnection(ConStr()))
      {
        con.Open();
        using (SqlCommand com = new SqlCommand(sQuery, con))
        {
          using (SqlDataReader r = com.ExecuteReader())
          {
            while (r.Read())
            {
              for (int i = 0; i<r.FieldCount; i++)
              {
                s = r.GetName(i);
                s= FormControlPref + s.Remove(0, FormSqlPref.Length);
                c = f.Controls[s];
                switch (c.GetType().ToString())
                {
                  case "System.Windows.Forms.TextBox":
                    if (c.Tag==null) c.Text = r.GetString(i);
                    else if (c.Tag.ToString() == "c") { c.Text = MyGetStrCurrency(r.GetDecimal(i).ToString()); }
                    else if (c.Tag.ToString() == "q") { c.Text = Format(r.GetDecimal(i).ToString(), "#,##0"); }
                    else c.Text = r.GetString(i);
                    break;
                  case "System.Windows.Forms.ComboBox":
                    if (((ComboBox)c).DataSource == null) ((ComboBox)c).SelectedItem = r.GetString(i);
                    else SelectItemByValue((ComboBox)c, r.GetInt64(i));
                    break;
                  case "System.Windows.Forms.DateTimePicker":
                    ((DateTimePicker)c).Value = r.IsDBNull(i)? DateTimePicker.MinimumDateTime:r.GetDateTime(i);
                    break;
                  case "System.Windows.Forms.CheckBox":
                    ((CheckBox)c).Checked = r.GetBoolean(i);
                    break;
                }
              }
            }
          }
        }
      }
      return;
    }

    public static string MyGetStrCurrency(String s)
    {
      return Format((Decimal.Parse(MyGetStrDecimal(s)), "C").ToString()).Replace( "р.", " р.");
    }

    public static string MyGetStrDecimal(string s, bool bUseDBDelim = false)
    {
      Char[] cc = s.ToCharArray();
      char c;
      bool b = false;
      string sDone="";
      foreach(Char c2 in cc)
      {
        c = c2;
        if (c == '.') c = ',';
        if (c == ',' && !b) b = true;
        else c = 'z';
        if (("0123456789,").IndexOf(c) > -1) sDone +=c;
      }
      if (bUseDBDelim) return sDone.Replace(',', '.');
      return sDone;
    }

    public static string MyES(ComboBox lst, bool bForLike = false)
    {
      if (lst.DataSource == null)
        return MyES(lst.Text, bForLike);
      else
        return MyES(lst.SelectedValue.ToString(), bForLike);
    }

    public static string MyES(TextBox txt, bool bForLike = false, bool nullable = false)
    {
      if (nullable && txt.Text == "") return "null";
      return MyES(txt.Text, bForLike);
    }

    public static string MyES(object val, bool bForLike = false, bool clear_as_null = false, bool mak = false, bool make_neg = false)
    {
      string s = val.ToString();
      if (val.GetType().ToString() == "System.Decimal") s = s.Replace(',','.');
      //заменяем пробел на % для LIKE
      s = s.Replace("\r", " ");//.Replace("\n", " ");
      while (s.Contains(" \n") || s.Contains("\n ") || s.Contains("\n\n") || s.Contains("  ")) s = s.Replace(" \n", "\n").Replace("\n ", "\n").Replace("\n\n", "\n").Replace("  ", " ");
      s = s.Replace(" .", ".").Replace(" ,", ",");
      s = s.Trim(new char[] {' ', '\n'});
      if (clear_as_null && s=="" & bForLike == false)
        return "null";
            if (s == "" && mak)
            {
                string ret = "null";
                return ret;
            }
            /*if (s != "" && mak)
            {

            }*/
            if (s == "null") return "null";
      if (bForLike)
        return "N'%" + s.Replace("'", "''").Replace(" ", "%") + "%'";
      else
        return "N'" + s.Replace("'", "''") + "'";
    }

    public static long MyLog(long lCurrentUser, string sModule, long EventType, long lEntityId, long lSpecId, string sAd="", string sCaption="")
    {
      string q;
      string strHostName = System.Environment.MachineName;
      //strHostName = System.Net.Dns.GetHostName()

      string strIPAddress = "";
      string strmachine = System.Net.Dns.GetHostName();
      System.Net.IPHostEntry iphe = System.Net.Dns.GetHostEntry(strmachine);

      foreach (System.Net.IPAddress ipheal in iphe.AddressList)
      {
        if (ipheal.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) strIPAddress = ipheal.ToString();
      }

      q = "Insert Into [_engLog] (ELIP,ELNetbios,ELDBUser,ELModule,ELEvent,ELEntityId,ELAd,ELCaption,ELSpec)" +
              " Select " + MyES(strIPAddress) + ", " + MyES(strHostName) + "," + lCurrentUser + ",'" + sModule + "'," +
               EventType + "," + lEntityId + "," +  MyES(sAd) + "," + MyES(sCaption) + "," +lSpecId +
              "; Select SCOPE_IDENTITY() as new_id;";
      long rez = long.Parse(MyGetOneValue(q).ToString());
      return rez;
    }

    public static void MyLogStandart(DataGridView dgv, string operation, string table, long id)
    {
      long SpecId=0;
      switch (table.ToLower())
      {
        case "spec":
          SpecId = id;
          break;
        case "specver":
          SpecId = (long)MyGetOneValue("Select SVSpec from SpecVer where SVId=" + id);
          break;
        case "_engdept":
          break;
        default:
          throw new NotImplementedException();
      }

      string q = "select EEOId from _engEntityOperation "+
        " where lower(EEOOperation)=" + MyES(operation.ToLower()) +
        " and lower(EEOEntity)=" + MyES(table.ToLower());
      long OperationType = (long)MyGetOneValue(q);
      string module = dgv.Name;
      MyLog(uid, module, OperationType, id, SpecId);
    }

    public static string MakeValidFileName(string name)
    {
      string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
      string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

      return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
    }

    public static string MyDigitsId(string s)
    {
      string rez = new string(s.Where(t => char.IsDigit(t)).ToArray());
      if (rez == "") rez = "0";
      return rez;
    }

    public static void SpecList_CheckedChanged(object sender, bool FormIsUpdating)
    {
      bool c = ((CheckBox)sender).Checked;
      string sColName = ((CheckBox)sender).Tag.ToString();
      Control p = ((Control)sender).Parent;
      ((DataGridView) p.Controls["dgvSpec"]).Columns[sColName].Visible = c;
      if (!FormIsUpdating)
      {
        string cName = ((Control)sender).Name;//  "chkDoneMultiline";
        string q = "delete from _engUserInterfaceOptions where EUIOUser=" + uid.ToString() + " and EUIOElement='" + cName + "';\n";// + "' and EUIOOption = '" + e.Column.Name + "';\n";
        q += "insert into _engUserInterfaceOptions (EUIOUser,EUIOElement,EUIOOption,EUIOVaue) values (" + uid + ",'" + cName + "','Checked','" + (c ? "1" : "0") + "')";
        MyExecute(q);
      }
    }

    public static void SpecList_RestoreColunns(DataGridView dgvSpec)
    {
      
      dgvSpec.Columns["dgv_S_btn_folder"].HeaderCell.Style.Font = new System.Drawing.Font("Wingdings", 10);
      int i;
      Control ctrl = dgvSpec.Parent;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='SpecList_ShowID' and EUIOVaue=1");
      ((CheckBox)ctrl.Controls["SpecList_ShowID"]).Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='SpecList_ShowType' and EUIOVaue=1");
      ((CheckBox)ctrl.Controls["SpecList_ShowType"]).Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='SpecList_ShowManagerAO' and EUIOVaue=1");
      ((CheckBox)ctrl.Controls["SpecList_ShowManagerAO"]).Checked = i == 1;
      i = (int)MyGetOneValue("select count(*) s from _engUserInterfaceOptions where EUIOUser=" + uid + " and EUIOElement='SpecList_ShowFolder' and EUIOVaue=1");
      ((CheckBox)ctrl.Controls["SpecList_ShowFolder"]).Checked = i == 1;
    }

  }
}
