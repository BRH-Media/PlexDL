using System;
using System.Data;
using System.Windows.Forms;

namespace LogDel.Utilities.Export
{
    public static class XmlUtility
    {
        public static void ToXml(this DataTable table, string filePath)
        {
            try
            {
                var ds = new DataSet("PlexDLData");
                var data = table.Copy();

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