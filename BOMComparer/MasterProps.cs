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
            AddToComboBox(refMBOMcb, TABLEFORMAT.masterBOMcols);
            AddToComboBox(pnumMBOMcb, TABLEFORMAT.masterBOMcols);
            AddToComboBox(refNBOMcb, TABLEFORMAT.newBOMcols);
            AddToComboBox(pnumNBOMcb, TABLEFORMAT.newBOMcols);
            AddToComboBox(qtyMBOMcb, TABLEFORMAT.masterBOMcols);
            AddToComboBox(qtyNBOMcb, TABLEFORMAT.newBOMcols);

            refMBOMcb.Text = MasterProperties.Default.ReferenceMBOM;
            refNBOMcb.Text = MasterProperties.Default.ReferenceNBOM;
            pnumMBOMcb.Text = MasterProperties.Default.partNumMBOM;
            pnumNBOMcb.Text = MasterProperties.Default.partNumNBOM;
            qtyMBOMcb.Text = MasterProperties.Default.qtyMBOM;
            qtyNBOMcb.Text = MasterProperties.Default.qtyNBOM;
        }
        private void AddToComboBox(ComboBox cmb,string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                cmb.Items.Add(arr[i]);
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TABLEFORMAT.userChosenCoulomnName = new Dictionary<string, string>();
            TABLEFORMAT.userChosenIndex = new Dictionary<string, int>();

            TABLEFORMAT.userChosenIndex.Add("ReferenceMBOM", refMBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("partNumMBOM", pnumMBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("ReferenceNBOM", refNBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("partNumNBOM", pnumNBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("qtyMBOM", qtyMBOMcb.SelectedIndex);
            TABLEFORMAT.userChosenIndex.Add("qtyNBOM", qtyNBOMcb.SelectedIndex);

            TABLEFORMAT.userChosenCoulomnName.Add("ReferenceMBOM", refMBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("partNumMBOM", pnumMBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("ReferenceNBOM", refNBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("partNumNBOM", pnumNBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("qtyMBOM", qtyMBOMcb.Text);
            TABLEFORMAT.userChosenCoulomnName.Add("qtyNBOM", qtyNBOMcb.Text);
            TABLEFORMAT.UserChose = true;
            this.Hide();
        }
    }
}
