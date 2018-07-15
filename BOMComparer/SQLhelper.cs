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
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace BOMComparer
{
    static class SQLhelper
    {
        public static bool created = false;
        public static bool compared = false;
        private static string[][] formats = new string[2][];
        static private void InitFormats()
        {
            formats[0] = new string[7];
            formats[1] = new string[7];

            //formats[0][0] = TABLEFORMAT.userChosenCoulomnName["ReferenceMBOM"];
            //formats[0][1] = TABLEFORMAT.userChosenCoulomnName["partNumMBOM"];
            //formats[0][2] = TABLEFORMAT.userChosenCoulomnName["descMBOM"];
            //formats[0][3] = TABLEFORMAT.userChosenCoulomnName["ReferenceNBOM"];
            //formats[0][4] = TABLEFORMAT.userChosenCoulomnName["partNumNBOM"];
            //formats[0][5] = TABLEFORMAT.userChosenCoulomnName["descNBOM"];
            //formats[0][6] = "status";

            formats[1][0] = TABLEFORMAT.userChosenCoulomnName["qtyMBOM"];
            formats[1][1] = TABLEFORMAT.userChosenCoulomnName["partNumMBOM"];
            formats[1][2] = TABLEFORMAT.userChosenCoulomnName["descMBOM"];
            formats[1][3] = TABLEFORMAT.userChosenCoulomnName["qtyNBOM"];
            formats[1][4] = TABLEFORMAT.userChosenCoulomnName["partNumNBOM"];
            formats[1][5] = TABLEFORMAT.userChosenCoulomnName["descMBOM"];
            formats[1][6] = "delta";


        }
        static private void SetOrdinalByFormat(DataSet dt, int index)
        {
            try
            {
                if (index == 1)
                {
                    DataColumnCollection cols = dt.Tables[index].Columns;
                    cols[0].ColumnName = TABLEFORMAT.userChosenCoulomnName["qtyMBOM"];
                    cols[1].ColumnName = TABLEFORMAT.userChosenCoulomnName["partNumMBOM"];
                    cols[2].ColumnName = TABLEFORMAT.userChosenCoulomnName["qtyNBOM"];
                    cols[3].ColumnName = TABLEFORMAT.userChosenCoulomnName["partNumNBOM"];
                    cols[4].ColumnName = "delta";
                    cols[5].ColumnName = TABLEFORMAT.userChosenCoulomnName["descMBOM"];
                    cols[6].ColumnName = TABLEFORMAT.userChosenCoulomnName["descNBOM"];
                }

                for (int j = 0; j < formats[index].Length; j++)
                {
                    dt.Tables[index].Columns[formats[index][j]].SetOrdinal(j);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        /// <summary>
        /// Exports a dataset into an excel file. parameter "filecode" is used to 
        /// know what kind of file is exported
        /// 0 for built original file
        /// 1 for comparison results
        /// 2 for error file
        /// </summary>
        /// <param name="dtset"></param>
        /// <param name="filename"></param>
        /// <param name="toinform"></param>
        /// <param name="destinationpath"></param>
        /// <returns>
        /// the path of the new file
        /// </returns>
        public static string ExportFile(DataSet dtset, string filename, int filecode, string destinationpath)
        {
            if (filecode == 1)
            {
                InitFormats();
                SetOrdinalByFormat(dtset, 1);
            }

            //creates a new name for the file
            string prefix = filename.Substring(0, filename.LastIndexOf('.'));
            string suffix = filename.Substring(filename.LastIndexOf('.'));
            string newpath = destinationpath + "\\" + prefix + "_new" + suffix;

            //creates an excel document
            CreateExcelFile.CreateExcelDocument(dtset, newpath);

            //if the user needs to know...
            if (filecode != 0)
            {
                if (filecode == 1)
                    ColorFile(newpath);
                MessageBox.Show("You can find the new file in: " + newpath);
            }

            return newpath;
        }

        private static void ColorFile(string path)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Open(path);
            string sheet1 = "Change in References";
            string sheet2 = "Material Change";

            wb.Sheets[sheet1].Columns.AutoFit();
            wb.Sheets[sheet1].Cells(1, 1).interior.color = Color.LightBlue;
            wb.Sheets[sheet1].Cells(1, 2).interior.color = Color.LightBlue;
            wb.Sheets[sheet1].Cells(1, 3).interior.color = Color.LightBlue;
            wb.Sheets[sheet1].Cells(1, 4).interior.color = Color.LightGreen;
            wb.Sheets[sheet1].Cells(1, 5).interior.color = Color.LightGreen;
            wb.Sheets[sheet1].Cells(1, 6).interior.color = Color.LightGreen;
            wb.Sheets[sheet1].Cells(1, 7).interior.color = Color.Orange;

            wb.Sheets[sheet2].Columns.AutoFit();
            wb.Sheets[sheet2].Cells(1, 1).interior.color = Color.LightBlue;
            wb.Sheets[sheet2].Cells(1, 2).interior.color = Color.LightBlue;
            wb.Sheets[sheet2].Cells(1, 3).interior.color = Color.LightBlue;
            wb.Sheets[sheet2].Cells(1, 4).interior.color = Color.LightGreen;
            wb.Sheets[sheet2].Cells(1, 5).interior.color = Color.LightGreen;
            wb.Sheets[sheet2].Cells(1, 6).interior.color = Color.LightGreen;
            wb.Sheets[sheet2].Cells(1, 7).interior.color = Color.Orange;

            wb.Save();

        }
        /// <summary>
        /// fills datasets with the content of excel files
        /// </summary>
        /// <param name="filepaths"></param>
        /// <returns> an array of datasets</returns>
        static private DataSet[] Fill(string[] filepaths)
        {
            //create an array of datasets
            DataSet[] dt = new DataSet[filepaths.Length];

            //for every file...
            for (int i = 0; i < filepaths.Length; i++)
            {
                //connect to excel
                string connectString =
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepaths[i] + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
                OleDbConnection conn = new OleDbConnection(connectString);

                //get the sheet name (excel table name)
                string sheetname = DataGrid_methods.GetSheetName(connectString, 0);

                //excecute it
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetname + "]", conn);

                //fill the dataset with the content of the excel file
                dt[i] = new DataSet();
                da.Fill(dt[i]);
            }
            return dt;
        }


        /// <summary>
        /// creates a sqlite table
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="ds"></param>
        /// <param name="sqlite_conn"></param>
        static private void CreateTable(string tablename, DataSet ds, SQLiteConnection sqlite_conn)
        {
            //create the sqlite command
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            System.Data.DataTable dt = ds.Tables[0];

            //create the beginning of the commnand string
            string commandstr = "CREATE TABLE " + tablename + " (";

            //for every column in table...
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //column name
                string colname = dt.Columns[i].ColumnName.ToString();

                //clean any "junk" that can harm the command string
                if (colname.Contains("'"))
                    colname = colname.Replace("'", " ");

                //create a new column in the command string
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

        /// <summary>
        /// inserts a dataset to a sqlite table
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="ds"></param>
        /// <param name="sqlite_conn"></param>
        static private void InsertToTable(string tablename, DataSet ds, SQLiteConnection sqlite_conn)
        {
            //create the sqlite command
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            System.Data.DataTable dt = ds.Tables[0];

            //create the beginning of the commnand string
            string basestrcommand = "INSERT INTO " + tablename + " (";

            //for every column in table
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //column name
                string colname = dt.Columns[i].ColumnName.ToString();

                //clean any "junk" that can harm the command string
                if (colname.Contains("'"))
                    colname = colname.Replace("'", " ");

                //add column name to the insert query
                basestrcommand += "[" + colname + "]";
                if (i < dt.Columns.Count - 1)
                    basestrcommand += ", ";
            }

            basestrcommand += ") VALUES (";
            string strcommand = basestrcommand;

            //for every row in table
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //for every cell in row
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //cell name
                    string colname = dt.Rows[i][j].ToString();

                    //remove any "junk" that can harm the commnad string
                    if (colname.Contains("'"))
                        colname = colname.Replace("'", " ");
                    strcommand += "'" + colname + "'";
                    if (j < dt.Columns.Count - 1)
                        strcommand += ", ";
                }
                strcommand += ");";
                sqlite_cmd.CommandText = strcommand;

                //execute the sqlite command
                sqlite_cmd.ExecuteNonQuery();
                strcommand = basestrcommand;
            }

        }

        /// <summary>
        /// creates a sqlite database and inserts excel tables to it
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="filepaths"></param>
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

                //create new sqlite command 
                SQLiteCommand sqlComm;
                //we call "begin" and "end" commands so sqlite won't take long to execute commands 
                sqlComm = new SQLiteCommand("begin", sqlite_conn);
                sqlComm.ExecuteNonQuery();

                //delete existing tables
                for (int i = 0; i < tablenames.Length; i++)
                {
                    sqlComm = new SQLiteCommand("drop table if exists " + tablenames[i], sqlite_conn);
                    sqlComm.ExecuteNonQuery();
                }

                //create new tables
                for (int i = 0; i < tablenames.Length - 2; i++)
                {
                    CreateTable(tablenames[i], DtSet[i], sqlite_conn);
                    InsertToTable(tablenames[i], DtSet[i], sqlite_conn);
                }

                //end the process
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


        /// <summary>
        /// execute diff or material scripts which are resources in this project
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="resname"></param>
        public static void ExecuteScript(string dbname, string resname)
        {
            try
            {
                // create a new database connection:
                SQLiteConnection sqlite_conn =
                  new SQLiteConnection("Data Source=" + dbname + ".sqlite;Version=3;");

                // open the connection:
                sqlite_conn.Open();

                //get the query
                string Query = GetUpdatedQuery(resname);

                //create new sqlite commnad
                SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = Query;

                //execute the query
                sqlite_cmd.ExecuteNonQuery();
                compared = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// selects a given table from a given sqlite database
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static System.Data.DataTable FetchTable(string dbname, string name)
        {
            //new data table
            System.Data.DataTable dataTable = new System.Data.DataTable();

            //connect to database
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbname + ".sqlite;Version=3;"))
            {
                //create new command
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper sh = new SQLiteHelper(cmd);
                    //execute the select query
                    dataTable = sh.Select("select * from " + name + ";");

                    conn.Close();
                }
            }
            return dataTable;
        }

        /// <summary>
        /// gets the script from resources and edits it according to the user's will 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetUpdatedQuery(string filename)
        {
            //get resource names
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BOMComparer." + filename;
            string query;
            //use stream reader to read the embedded resource
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    query = reader.ReadToEnd();
                }
            }
            //edit the query according the column names the user chose when "build" was pressed
            query = query.Replace("Son P/N Items", TABLEFORMAT.userChosenCoulomnName["partNumMBOM"]);
            query = query.Replace("stx_num", TABLEFORMAT.userChosenCoulomnName["partNumNBOM"]);
            query = query.Replace("Description", TABLEFORMAT.userChosenCoulomnName["descMBOM"]);
            query = query.Replace("Value", TABLEFORMAT.userChosenCoulomnName["descNBOM"]);
            query = query.Replace("Location", TABLEFORMAT.userChosenCoulomnName["ReferenceMBOM"]);
            query = query.Replace("Part Reference", TABLEFORMAT.userChosenCoulomnName["ReferenceNBOM"]);
            query = query.Replace("QTY", TABLEFORMAT.userChosenCoulomnName["qtyMBOM"]);
            query = query.Replace("Quantity", TABLEFORMAT.userChosenCoulomnName["qtyNBOM"]);

            return query;
        }

    }
}
