using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOMComparer
{
    public partial class MasterProps : Form
    {

        public MasterProps()
        {
            InitializeComponent();
            //add to combo boxes all the columns in the table so the user could choose
            AddToComboBox(refMBOMcb, TABLEFORMAT.masterBOMcols);
            AddToComboBox(pnumMBOMcb, TABLEFORMAT.masterBOMcols);
            AddToComboBox(refNBOMcb, TABLEFORMAT.newBOMcols);
            AddToComboBox(pnumNBOMcb, TABLEFORMAT.newBOMcols);
            AddToComboBox(qtyMBOMcb, TABLEFORMAT.masterBOMcols);
            AddToComboBox(qtyNBOMcb, TABLEFORMAT.newBOMcols);

            //put a default value in the combobox so the user won't click too much
            refMBOMcb.Text = MasterProperties.Default.ReferenceMBOM;
            refNBOMcb.Text = MasterProperties.Default.ReferenceNBOM;
            pnumMBOMcb.Text = MasterProperties.Default.partNumMBOM;
            pnumNBOMcb.Text = MasterProperties.Default.partNumNBOM;
            qtyMBOMcb.Text = MasterProperties.Default.qtyMBOM;
            qtyNBOMcb.Text = MasterProperties.Default.qtyNBOM;

        }

        /// <summary>
        /// adds an array of items to the combo box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="arr"></param>
        private void AddToComboBox(ComboBox cmb,string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                cmb.Items.Add(arr[i]);
                
            }
        }

        /// <summary>
        /// when user clicks "ok" the parameters he chose are put in a static class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //initiallize the two dictionaries
            TABLEFORMAT.userChosenCoulomnName = new Dictionary<string, string>();
            TABLEFORMAT.userChosenIndex = new Dictionary<string, int>();

            //add to "userChosenIndex" dictionary
            TABLEFORMAT.userChosenIndex.Add("ReferenceMBOM", refMBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("partNumMBOM", pnumMBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("ReferenceNBOM", refNBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("partNumNBOM", pnumNBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("qtyMBOM", qtyMBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("qtyNBOM", qtyNBOMcb.SelectedIndex);
            //TABLEFORMAT.userChosenIndex.Add("descMBOM", descMBOMcb.SelectedIndex);
            //TABLEFORMAT.userChosenIndex.Add("descNBOM", descNBOMcb.SelectedIndex);

            //add to "userChosenCoulomnName" dictionary
            TABLEFORMAT.userChosenCoulomnName.Add("ReferenceMBOM", refMBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("partNumMBOM", pnumMBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("ReferenceNBOM", refNBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("partNumNBOM", pnumNBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("qtyMBOM", qtyMBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("qtyNBOM", qtyNBOMcb.Text);
            //TABLEFORMAT.userChosenCoulomnName.Add("descMBOM", descMBOMcb.Text);
            //TABLEFORMAT.userChosenCoulomnName.Add("descNBOM", descNBOMcb.Text);

            //no combo mox is empty
            bool a = refMBOMcb.Text != "";
            bool b = pnumMBOMcb.Text != "";
            bool c = refNBOMcb.Text != "";
            bool d = pnumNBOMcb.Text != "";
            bool j = qtyMBOMcb.Text != "";
            bool f = qtyNBOMcb.Text != "";
            //bool g = descMBOMcb.Text != "";
            //bool h = descNBOMcb.Text != "";

            if (a && b && c && d && j && f /*&& g && h*/&&ToLegal())
            {
                TABLEFORMAT.UserChose = true;
                this.Hide();
            }

        }
        private bool ToLegal()
        {
            string[] texts = new string[6] { refMBOMcb.Text , pnumMBOMcb.Text , refNBOMcb.Text ,
            pnumNBOMcb.Text, qtyMBOMcb.Text,qtyNBOMcb.Text/*,descMBOMcb.Text, descNBOMcb.Text*/};
            bool legal = true;
            for (int i = 0; i < texts.Length; i++)
            {
                for (int j = 0; j < texts.Length; j++)
                {
                    if (i != j)
                    {
                        if (texts[i] == texts[j])
                            legal = false;
                    }
                }
            }

            if (!legal)
                MessageBox.Show("Please choose different names for columns");
            return legal;
        }
    }
}
