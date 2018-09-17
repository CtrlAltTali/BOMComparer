using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BOMComparer
{
    static class TABLEFORMAT
    {
        static public string[] masterBOMcols;
        static public string[] newBOMcols;
        static public Dictionary<string, int> userChosenIndex;
        static public Dictionary<string, string> userChosenCoulomnName;
        static public bool UserChose = false;
        static public bool[] legalTable = new bool[2] { true, true };

        static public int Son_PN_Items { get; set; }
        //static public int Description { get; set; }
        static public int QTY { get; set; }
        static public int Location { get; set; }
        static public bool ignore { get; set; }
        static public string[] suffixes = new string[2]
        {
            "_master","_new"
        };
        static public void ModifyEnum(int tableindex, DataTable dt )
        {
            string[] colnames = new string[4];
            switch (tableindex)
            {
                
                case 0:
                    Son_PN_Items = userChosenIndex["partNumMBOM"];
                    Location = userChosenIndex["ReferenceMBOM"];
                    QTY = userChosenIndex["qtyMBOM"];
                    break;
                case 1:
                    Son_PN_Items = userChosenIndex["partNumNBOM"];
                    Location = userChosenIndex["ReferenceNBOM"];
                    QTY = userChosenIndex["qtyNBOM"];
                    break;
            }
        }
    }
}
