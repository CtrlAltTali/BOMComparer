using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace BOMComparer
{
    public partial class Form1 : Form
    {
        DataGrid_methods datagrid = new DataGrid_methods();
        Color activebtn = Color.LightSeaGreen;
        Color inactivebtn = Color.White;
        string[] filepaths = new string[4];
        string[] newFilepaths = new string[2];
        int sheetindex;
        bool built1 = false;
        bool built2 = false;
        bool tb1 = false;
        bool tb2 = false;
        bool compared = false;
        string newDirpath = "";
        string basedir = "";
        string[] tablenames = new string[2] { "Master BOM", "New BOM" };
        public Form1()
        {
            InitializeComponent();
            compareBTN.Enabled = false;
            buildBTN.Enabled = false;
            basedir = Application.StartupPath + "\\Reports";
            if (!Directory.Exists(basedir))
                Directory.CreateDirectory(basedir);
            //string[] diffQuery = System.IO.File.ReadAllLines("sqlite_diff.txt");
            //dataGridView1.Hide();

        }
        public void enableCompareBTN (bool enable){
            compareBTN.Enabled = enable;
        }
        /// <summary>
        /// imports Master BOM excel file to this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importmbBTN_Click(object sender, EventArgs e)
        {
            sheetindex = 0;
            compared = false;
            filepaths[0] = datagrid.OpenFileManager();
            datagrid.Import(dataGridView1, 0, filepaths[0]);
            datagrid.StoreColumns(sheetindex, dataGridView1);
            tb1 = true;
            buildBTN.Enabled = tb1 && tb2;
        }

        /// <summary>
        /// imports New BOM excel file to this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importnbBTN_Click(object sender, EventArgs e)
        {
            sheetindex = 1;
            compared = false;
            filepaths[1] = datagrid.OpenFileManager();
            datagrid.Import(dataGridView2, 1, filepaths[1]);
            datagrid.StoreColumns(sheetindex, dataGridView2);
            tb2 = true;
            buildBTN.Enabled = tb1 && tb2;
        }


        /// <summary>
        /// fixes the two tables
        /// seperates lines and tells if the program can proceed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buildBTN_Click(object sender, EventArgs e)
        {

            //make new dir to the new files
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            string currenntTime = DateTime.Now.ToString("hh:mm:ss").Replace(':', '-');
            newDirpath = Path.Combine(basedir, currentDate + "_" + currenntTime);
            Directory.CreateDirectory(newDirpath);

            //open the field mapper window
            MasterProps f = new MasterProps();
            f.ShowDialog();

            //if user mapped all fields
            if (TABLEFORMAT.UserChose)
            {
                //build the new tables
                this.built1 = datagrid.BuildTable(dataGridView1, 0);
                built2 = datagrid.BuildTable(dataGridView2, 1);
                string improvedpath = newDirpath;
                //if at least one table is illegal
                if (datagrid.errors.Tables[0].Rows.Count != 0 || datagrid.errors.Tables[1].Rows.Count != 0)
                {

                    //disable the comparison
                    compareBTN.Enabled = false;
                    //export the error table to a new excel file

                    string resultpath = SQLhelper.ExportFile(datagrid.errors, "Errors.xlsx", 2, newDirpath);
                    improvedpath = newDirpath + "-Error";
                    System.IO.Directory.Move(newDirpath, improvedpath);

                }
                //if both legal, enable the comparison
                else
                {
                    improvedpath = newDirpath + "-Comparison";
                    System.IO.Directory.Move(newDirpath, improvedpath);
                    compareBTN.Enabled = true;
                }
                newDirpath = improvedpath;
                //export the new tables into the new dir
                newFilepaths[0] = datagrid.Export(0, filepaths[0].Substring(filepaths[0].LastIndexOf('\\') + 1), 0, newDirpath);
                newFilepaths[1] = datagrid.Export(1, filepaths[1].Substring(filepaths[1].LastIndexOf('\\') + 1), 0, newDirpath);






                //draw row index in row headers in the datagrid
                this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
                this.dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView2_RowPostPaint);
                dataGridView1.Refresh();
                dataGridView2.Refresh();


            }


        }

        /// <summary>
        /// draws row index in row headers in datagridview1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        /// <summary>
        /// draws row index in row headers in datagridview2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView2.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        /// <summary>
        /// compares between the two tables when "compare" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void compareBTN_Click(object sender, EventArgs e)
        {
            //create database and tables
            SQLhelper.CreateDBandInsertTables("BOMCompareDB", newFilepaths);

            //execute queries
            SQLhelper.ExecuteScript("BOMCompareDB", "sqlite_diff.txt");
            SQLhelper.ExecuteScript("BOMCompareDB", "sqlite_material.txt");

            //get the comparison report tables
            DataTable diff = SQLhelper.FetchTable("BOMCompareDB", "diff");
            DataTable material = SQLhelper.FetchTable("BOMCompareDB", "material");

            //put these tables in a dataset
            DataSet results = new DataSet();
            results.Tables.Add(diff);
            results.Tables[0].TableName = "Change in References";
            results.Tables.Add(material);
            results.Tables[1].TableName = "Material Change";

            //export the dataset to a new excel file
            string resultpath = SQLhelper.ExportFile(results, "Comparison_Report.xlsx", 1, newDirpath);
        }

        private void saveBTN_Click(object sender, EventArgs e)
        {
            DataTable table1 = datagrid.DtSet[0].Tables[0];
            DataTable table2 = datagrid.DtSet[1].Tables[0];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    table1.Rows[i][j] = dataGridView1[j, i].Value;
                }

            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    table2.Rows[i][j] = dataGridView2[j, i].Value;
                }

            }
        }
    }
}
