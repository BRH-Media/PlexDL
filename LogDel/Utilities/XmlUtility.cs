using System;
using System.Data;
using System.Windows.Forms;

namespace LogDel.Utilities
{
    public static class XmlUtility
    {
        public static void ToXml(this DataTable table, string filePath)
        {
            try
            {
                DataSet ds = new DataSet("PlexDLData");
                DataTable data = table.Copy();

                ds.Tables.Add(data);
                //hierarchical XML format
                ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}