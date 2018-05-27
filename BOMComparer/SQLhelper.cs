using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Reflection;

namespace BOMComparer
{
    static class SQLhelper
    {
        public static bool created = false;
        public static bool compared = false;
        public static string ExportFile(System.Data.DataSet dtset, string filename, bool toinform, string destinationpath)
        {
            //Random r = new Random();
            string prefix = filename.Substring(0, filename.LastIndexOf('.'));
            string suffix = filename.Substring(filename.LastIndexOf('.'));
            string newpath = destinationpath + "\\" + prefix + "_new" + suffix;
            CreateExcelFile.CreateExcelDocument(dtset, newpath);
            if (toinform)
                MessageBox.Show("You can find the new file in: " + newpath);
            return newpath;
        }
        static private DataSet[] Fill(string[] filepaths)
        {
            DataSet[] dt = new DataSet[filepaths.Length];
            for (int i = 0; i < filepaths.Length; i++)
            {
                string connectString =
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepaths[i] + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
                OleDbConnection conn = new OleDbConnection(connectString);
                string sheetname = DataGrid_methods.GetSheetName(connectString, 0);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetname + "]", conn);
                dt[i] = new DataSet();
                da.Fill(dt[i]);
            }
            return dt;
        }
        static private void CreateTable(string tablename, DataSet ds, SQLiteConnection sqlite_conn)
        {
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            DataTable dt = ds.Tables[0];
            string commandstr = "CREATE TABLE " + tablename + " (";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colname = dt.Columns[i].ColumnName.ToString();
                if (colname.Contains("'"))
                    colname = colname.Replace("'", " ");
                commandstr += "[" + colname + "]" + " varchar(100)";
                if (i < dt.Columns.Count - 1)
                    commandstr += ", ";
            }
            commandstr += ");";
            // Let the SQLiteCommand object know our SQL-Query:
            sqlite_cmd.CommandText = commandstr;

            // Now lets execute the SQL ;-)
            sqlite_cmd.ExecuteNonQuery();
        }
        static private void InsertToTable(string tablename, DataSet ds, SQLiteConnection sqlite_conn)
        {
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            DataTable dt = ds.Tables[0];
            string basestrcommand = "INSERT INTO " + tablename + " (";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colname = dt.Columns[i].ColumnName.ToString();
                if (colname.Contains("'"))
                    colname = colname.Replace("'", " ");
                basestrcommand += "[" + colname + "]";
                if (i < dt.Columns.Count - 1)
                    basestrcommand += ", ";
            }
            basestrcommand += ") VALUES (";
            string strcommand = basestrcommand;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string colname = dt.Rows[i][j].ToString();
                    if (colname.Contains("'"))
                        colname = colname.Replace("'", " ");
                    strcommand += "'" + colname + "'";
                    if (j < dt.Columns.Count - 1)
                        strcommand += ", ";
                }
                strcommand += ");";
                sqlite_cmd.CommandText = strcommand;

                sqlite_cmd.ExecuteNonQuery();
                strcommand = basestrcommand;
            }

        }
        public static void CreateDBandInsertTables(string dbname, string[] filepaths)
        {
            try
            {
                // create a new database connection:
                SQLiteConnection sqlite_conn =
                  new SQLiteConnection("Data Source=" + dbname + ".sqlite;Version=3;");

                // open the connection:
                sqlite_conn.Open();

                DataSet[] DtSet = Fill(filepaths);

                string[] tablenames = new string[4] { "Master_BOM", "New_BOM", "diff", "material" };


                SQLiteCommand sqlComm;
                sqlComm = new SQLiteCommand("begin", sqlite_conn);
                sqlComm.ExecuteNonQuery();
                for (int i = 0; i < tablenames.Length; i++)
                {
                    sqlComm = new SQLiteCommand("drop table if exists " + tablenames[i], sqlite_conn);
                    sqlComm.ExecuteNonQuery();
                }

                for (int i = 0; i < tablenames.Length - 2; i++)
                {
                    CreateTable(tablenames[i], DtSet[i], sqlite_conn);
                    InsertToTable(tablenames[i], DtSet[i], sqlite_conn);
                }

                sqlComm = new SQLiteCommand("end", sqlite_conn);
                sqlComm.ExecuteNonQuery();
                sqlite_conn.Close();
                created = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public static void ExecuteScript(string dbname, string resname)
        {
            try
            {
                // create a new database connection:
                SQLiteConnection sqlite_conn =
                  new SQLiteConnection("Data Source=" + dbname + ".sqlite;Version=3;");

                // open the connection:
                sqlite_conn.Open();

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BOMComparer." + resname;

                string Query;
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Query = reader.ReadToEnd();
                }

                SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = Query;
                sqlite_cmd.ExecuteNonQuery();
                compared = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static DataTable FetchTable(string dbname,string name)
        {
            DataTable dataTable = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbname + ".sqlite;Version=3;"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    dataTable = sh.Select("select * from "+name+";");

                    conn.Close();
                }
            }
            return dataTable;
        }


    }
}
