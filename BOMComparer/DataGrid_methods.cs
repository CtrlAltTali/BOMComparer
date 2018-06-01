using System;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data;

namespace BOMComparer
{
    class DataGrid_methods
    {

        int Son_PN_Items;
        int quantity;
        int Description;
        int QTYcount;
        int Location;
        public string[] firstErrorIndex = new string[2] { "-1", "-1" };
        System.Data.DataSet[] DtSet = new System.Data.DataSet[2];

        Node<string> tocolor;

        /// <summary>
        /// Opens File Manager so the user could choose an excel file
        /// Input: No input
        /// Output: No output
        /// Author: amazingtali
        /// </summary>
        public string OpenFileManager()
        {
            string filepath = "";
            // Displays an OpenFileDialog so the user can select a Cursor.  
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls;";
            openFileDialog.Title = "Select an Excel File";

            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .CUR file was selected, open it.  
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.  
                filepath = openFileDialog.FileName;
                MessageBox.Show(filepath);

            }
            return filepath;
        }


        /// <summary>
        /// Imports the chosen excel file to the form
        /// Input: A dataGridView to host the excel file in the form
        /// Output: No output
        /// Author: amazingtali
        /// </summary>
        public void Import(DataGridView dataGridView, int sheetindex, string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.Data.OleDb.OleDbConnection MyConnection;

                System.Data.OleDb.OleDbDataAdapter MyCommand;
                //Build the connection string
                string connectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                string sheetname = GetSheetName(connectionstring, 0);
                //connect to excel file
                MyConnection = new System.Data.OleDb.OleDbConnection(connectionstring);
                //select the whole table from excel file               
                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [" + sheetname + "]", MyConnection);
                MyCommand.TableMappings.Add("Table", "Net-informations.com");
                DtSet[sheetindex] = new System.Data.DataSet();
                try
                {
                    int stat;
                    //put the excel table in the data grid object
                   stat =  MyCommand.Fill(DtSet[sheetindex]);
                    dataGridView.DataSource = DtSet[sheetindex].Tables[0];
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                MyConnection.Close();
            }
        }

        public string Export(int sheetindex, string filename, bool toinform, string destinationpath)
        {
            return SQLhelper.ExportFile(DtSet[sheetindex], filename, toinform, destinationpath);
        }

        /// <summary>
        /// Gets the name of the sheet that it's index is given
        /// Input: an OLEDB connection string, a sheet index
        /// Output: the sheet's name (string)
        /// Author: amazingtali
        /// </summary>
        static public string GetSheetName(string connectionstring, int index)
        {
            string sheetName = "";
            using (System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
                    cmd.Connection = conn;

                    // Get 1st sheet name in Excel File
                    System.Data.DataTable dtSheet = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);

                    sheetName = dtSheet.Rows[index]["TABLE_NAME"].ToString();


                    cmd = null;
                    conn.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return sheetName;
        }

        /// <summary>
        /// Every table has a different format. this method 
        /// adjusts the variables for the table's format
        /// Input: the index of the table (0 for master 1 for new)
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        private void updatetablevars(int sheetindex)
        {
            TABLEFORMAT.ModifyEnum(sheetindex, DtSet[sheetindex].Tables[0]);
            Son_PN_Items = TABLEFORMAT.Son_PN_Items;
            Location = TABLEFORMAT.Location;
            quantity = TABLEFORMAT.QTY;
        }

        /// <summary>
        /// checks if a location is legal
        /// example:
        /// legal: A1032
        /// illegal: H463B
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsLegal(string str)
        {
            bool illegal = str[str.Length - 1] >= 65 && str[str.Length - 1] <= 90;
            if (illegal) return false;
            else return true;
            string[] parts = new string[2] { "", "" };

        }

        /// <summary>
        /// this method removes the dashes for a string
        /// Input: a string with dashes
        /// Output: an array of the splitted string
        /// Author: amazingtali
        /// </summary>
        private string[] removedash(string locs)
        {
            string[] rangelocs = locs.Split('-');
            string[] prefix = new string[2] { "", "" };
            string[] suffix = new string[2] { "", "" };
            for (int k = 0; k < rangelocs.Length; k++)
            {
                string str = rangelocs[k];
                for (int i = 0; i < str.Length; i++)
                {
                    int ascii = (int)(str[i]);
                    bool number = ascii >= 48 && ascii <= 57; //char is a number
                    bool letter = ascii >= 65 && ascii <= 90; //char is a letter
                    if (letter)
                        prefix[k] += str[i];
                    if (number)
                        suffix[k] += str[i];
                }
            }


            string[] nums = new string[0];
            if (prefix[0] == prefix[1])
            {
                int num2 = int.Parse(suffix[1]);
                int num1 = int.Parse(suffix[0]);
                int count = num2 - num1;
                nums = new string[count + 1];
                for (int k = 0; k < count + 1; k++)
                {
                    nums[k] = prefix[0] + num1.ToString();
                    num1++;
                }
            }
            return nums;
        }

        /// <summary>
        /// Adds a new row to the linked list of rows
        /// Input: a linked list of rows, new row
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        private void AddToList<T>(Node<T> rows, T val)
        {
            if (rows.GetValue() == null)
            {
                rows.SetValue(val);
            }
            else
            {
                while (rows.GetNext() != null)
                {
                    rows = rows.GetNext();
                }
                rows.SetNext(new Node<T>(val));
            }

        }
        /// <summary>
        /// replaces spaces with '?'
        /// </summary>
        /// <param name="str"></param>
        /// <returns> a string with '?' instead of spaces</returns>
        public string SearchForSpaces(string str)
        {
            if (str.Contains(" "))
                return str.Replace(" ", "?");
            return str;

        }

        /// <summary>
        /// checks that the quantity written in table is
        /// the same as the number of locations
        /// </summary>
        /// <param name="expectedQuantity"></param>
        /// <param name="locations"></param>
        /// <returns>a boolean value indicates if the quantity is valid</returns>
        private bool ValidQuantity(double expectedQuantity, string locations)
        {
            int counted = 0;
            //remove spaces from string
            locations = locations.Replace(" ", "");

            //check if it containd dashes or colons
            if (locations.Contains("-") || locations.Contains(","))
            {
                //if contains colons
                if (locations.Contains(","))
                {                    
                    //spit the string
                    string[] locs = locations.Split(',');
                    for (int i = 0; i < locs.Length; i++)
                    {
                        //now check if every location in array has dashes
                        if (locs[i].Contains("-"))
                        {
                            //the length of "dashed" is the difference between the two locations
                            string[] dashed = removedash(locs[i]);

                            //add the difference
                            counted += dashed.Length;
                        }
                        //if not, then add 1
                        else
                            counted++;
                    }
                }
                //if not, it must conatin dashes
                else
                {
                    //the length of "dashed" is the difference between the two locations
                    string[] dashed = removedash(locations);

                    //add the difference
                    counted += dashed.Length;
                }
            }
            //if not, then add 1
            else
                counted++;

            if (expectedQuantity == counted)
                return true;
            return false;
        }
        /// <summary>
        /// this method fixes the DataGridView table:
        /// it removes dashes, commas, makes new rows and deletes unnessecery ones
        /// Input: the index of the table (0 for master 1 for new), datagridview
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        public bool BuildTable(DataGridView datagrid, int sheetindex)
        {

            Node<DataRow> toadd = new Node<DataRow>();
            Node<System.Data.DataRow> toremove = new Node<System.Data.DataRow>();
            tocolor = new Node<string>();
            string[] rowobjects = new string[DtSet[sheetindex].Tables[0].Columns.Count];
            updatetablevars(sheetindex);
            double qtyInRow = 0;
            //go over every row in the table
            for (int i = 0; i < datagrid.RowCount - 1; i++)
            {

                string items = datagrid.Rows[i].Cells[Son_PN_Items].Value.ToString();
                string qtyVal = datagrid.Rows[i].Cells[quantity].Value.ToString();
                string location = datagrid.Rows[i].Cells[Location].Value.ToString(); ;
                //if items/stx_num is not empty...
                if (qtyVal != "" && items != "" && location!=null && location!="")
                {
                    //store the row objects in an array
                    for (int k = 0; k < rowobjects.Length; k++)
                    {
                        rowobjects[k] = datagrid.Rows[i].Cells[k].Value.ToString();
                    }
                    qtyInRow = double.Parse(rowobjects[quantity]);

                    rowobjects[Location] = location;

                    bool validQuantity = ValidQuantity(qtyInRow, rowobjects[Location]);
                    //string fixesitems = SearchForSpaces(items);
                    //items = fixesitems;
                    //rowobjects[Son_PN_Items] = fixesitems;
                    bool containsComma = rowobjects[Location].Contains(',');
                    bool containsDash = rowobjects[Location].Contains('-');
                    bool legalLocation = IsLegal(rowobjects[Location]);
                    

                    if ((containsDash || containsComma) && legalLocation && validQuantity)
                    {
                        string[] newrowobjects = rowobjects;
                        //spaces in the location string are corrupting the string
                        rowobjects[Location] = rowobjects[Location].Replace(" ", "");

                        System.Data.DataRow row = DtSet[sheetindex].Tables[0].NewRow();
                        row[Son_PN_Items] = rowobjects[Son_PN_Items];

                        //if Location contains commas
                        if (rowobjects[Location].Contains(','))
                        {

                            // split it
                            string[] locs = rowobjects[Location].Split(',');

                            //go over the split-string and find dashes
                            for (int j = 0; j < locs.Length; j++)
                            {
                                //if dashes exist
                                if (locs[j].Contains('-'))
                                {
                                    //remove them...
                                    string[] nums = removedash(locs[j]);

                                    //go over the split string
                                    for (int m = 0; m < nums.Length; m++)
                                    {
                                        //add this row to the list of new rows that should be added
                                        newrowobjects[Location] = nums[m];
                                        newrowobjects[quantity] = "1";
                                        AddToList(toadd, GenerateRow(newrowobjects, sheetindex));
                                    }
                                }
                                //if dashes do not exist
                                else
                                {
                                    //add this row to the list of new rows that should be added
                                    newrowobjects[Location] = locs[j];
                                    newrowobjects[quantity] = "1";
                                    AddToList(toadd, GenerateRow(newrowobjects, sheetindex));
                                }
                            }
                        }
                        //if Location does not contain commas
                        else
                        {
                            //remove dashes
                            string[] nums = removedash(rowobjects[Location]);

                            //go over the split string
                            for (int m = 0; m < nums.Length; m++)
                            {
                                //add this row to the list of new rows that should be added
                                newrowobjects[Location] = nums[m];
                                newrowobjects[quantity] = "1";
                                AddToList(toadd, GenerateRow(newrowobjects, sheetindex));
                            }
                        }

                        //this row is corrupted and not needed anymore
                        //add this row to the list of rows that should be deleted
                        AddToList(toremove, ((System.Data.DataRowView)datagrid.Rows[i].DataBoundItem).Row);

                    }

                }
                //if items/stx_num is empty...
                else
                {
                    //add this row to the list of rows that should be deleted
                    AddToList(toremove, ((System.Data.DataRowView)datagrid.Rows[i].DataBoundItem).Row);
                }


            }

            //if there are rows that should be added
            if (toadd.GetValue() != null)
                AddRows(toadd, sheetindex);

            //if there are rows that should be removed
            if (toremove.GetValue() != null)
                DeleteRows(toremove, sheetindex);



            DtSet[sheetindex].Tables[0].AcceptChanges();

            BindingSource source = new BindingSource();
            source.DataSource = DtSet[sheetindex].Tables[0];
            datagrid.DataSource = source;

            //check for errors in the updated table
            CheckForErrors(datagrid, sheetindex, qtyInRow);
           
            //color the errors in red if they exist
            if (tocolor.GetValue() != null)
                ColorCells(datagrid, tocolor);
            
            return true;
        }
        
        /// <summary>
        /// checks for erros in the datagrid:
        /// valid quantity, if it has spaces or if locations are legal
        /// </summary>
        /// <param name="datagrid"></param>
        /// <param name="sheetindex"></param>
        /// <param name="qtyInRow"></param>
        private void CheckForErrors(DataGridView datagrid, int sheetindex, double qtyInRow)
        {
            //for each row in the grid
            for (int i = 0; i < datagrid.RowCount - 1; i++)
            {
                //search for spaces in "items"
                string items = datagrid.Rows[i].Cells[Son_PN_Items].Value.ToString();
                items = SearchForSpaces(items);

                //update the new "items" value in the dataset
                DtSet[sheetindex].Tables[0].Rows[i][Son_PN_Items] = items;
                
                //value of quantity cell
                string qtyVal = datagrid.Rows[i].Cells[quantity].Value.ToString();
                qtyInRow = double.Parse(qtyVal);

                //value of location cell
                string location = datagrid.Rows[i].Cells[Location].Value.ToString();

                if (qtyVal != "" && items != "")
                {
                    //quantity is valid
                    bool validQuantity = ValidQuantity(qtyInRow, location);

                    //"items" has spaces
                    bool hasspaces = items.Contains("?");

                    //location is legal
                    bool legalLocation = IsLegal(location);

                    //if one of them is false
                    if (!validQuantity || hasspaces || !legalLocation)
                    {
                        //we need this to tell the user where the first error at
                        string err = firstErrorIndex[sheetindex];
                        if (firstErrorIndex[sheetindex] == "-1")
                        {
                            if(hasspaces)
                                firstErrorIndex[sheetindex] = i.ToString()+"S";
                            else if(!validQuantity)
                                firstErrorIndex[sheetindex] = i.ToString() + "Q";
                            else if(!legalLocation)
                                firstErrorIndex[sheetindex] = i.ToString() + "L";
                        }
                            

                        //now the table is not legal and we can proceed to comparison
                        TABLEFORMAT.legalTable[sheetindex] = false;

                        //add this row to the list of rows that should be colourd
                        AddToList(tocolor, location);
                    }
                }
            }
            DtSet[sheetindex].Tables[0].AcceptChanges();
        }

        /// <summary>
        /// creates a new datarow contains the values given in the array
        /// </summary>
        /// <param name="rowobjects"></param>
        /// <param name="sheetindex"></param>
        /// <returns></returns>
        private DataRow GenerateRow(string[] rowobjects, int sheetindex)
        {
            DataRow dtr = DtSet[sheetindex].Tables[0].NewRow();
            for (int i = 0; i < rowobjects.Length; i++)
            {
                dtr[i] = rowobjects[i];
            }
            return dtr;
        }

        /// <summary>
        /// this colors the problematique items in the table
        /// Input: a linked list of rows and the datagrid
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        private void ColorCells(DataGridView datagrid, Node<string> tocolor)
        {
            while (tocolor != null)
            {
                foreach (DataGridViewRow row in datagrid.Rows)
                {

                    if (tocolor != null)
                    {
                        if (row.Cells[Location].Value.ToString() == tocolor.GetValue())
                        {
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                            tocolor = tocolor.GetNext();

                        }
                    }

                }
            }

        }
        /// <summary>
        /// this method adds rows to the table from a linked list
        /// Input: a linked list of rows
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        private void AddRows(Node<DataRow> rows, int sheetindex)
        {
            while (rows != null)
            {
                
                DtSet[sheetindex].Tables[0].Rows.Add(rows.GetValue());
                rows = rows.GetNext();
            }
            DtSet[sheetindex].Tables[0].AcceptChanges();
        }

        /// <summary>
        /// this method deletes rows from the table
        /// Input: a linked list of rows
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        private void DeleteRows(Node<System.Data.DataRow> rows, int sheetindex)
        {
            while (rows != null)
            {
                DtSet[sheetindex].Tables[0].Rows.Remove(rows.GetValue());
                rows = rows.GetNext();
            }
            DtSet[sheetindex].Tables[0].AcceptChanges();
        }

        /// <summary>
        /// this method stores the indexes of the columns in an external array
        /// Input: the table index and the datagridview
        /// Output: no output
        /// Author: amazingtali
        /// </summary>
        public void StoreColumns(int tableindex, DataGridView datagrid)
        {
            string[] columns = new string[datagrid.ColumnCount];

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = datagrid.Columns[i].HeaderText;
            }
            switch (tableindex)
            {
                case 0:
                    TABLEFORMAT.masterBOMcols = columns;
                    break;
                case 1:
                    TABLEFORMAT.newBOMcols = columns;
                    break;
            }
        }
    }
}
