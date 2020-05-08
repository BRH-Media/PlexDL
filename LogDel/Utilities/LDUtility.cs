using System;
using System.Data;
using System.IO;
using System.Linq;

namespace LogDel.Utilities
{
    public static class LdUtility
    {
        public static void ToLogdel(this DataTable table, string path)
        {
            try
            {
                if (table != null)
                {
                    StreamWriter sw = new StreamWriter(path, false);
                    //headers
                    sw.Write("###");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        sw.Write(table.Columns[i]);
                        if (i < table.Columns.Count - 1)
                        {
                            sw.Write("!");
                        }
                    }
                    sw.Write(sw.NewLine);
                    foreach (DataRow dr in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                string value = dr[i].ToString();
                                if (value.Contains('!'))
                                {
                                    value = String.Format("\"{0}\"", value);
                                    sw.Write(value);
                                }
                                else
                                {
                                    sw.Write(dr[i].ToString());
                                }
                            }
                            if (i < table.Columns.Count - 1)
                            {
                                sw.Write("!");
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                    sw.Close();
                }
            }
            catch (Exception)
            {
                //ignore it
            }
        }
    }
}
