using LogDel.Utilities.Extensions;
using System;
using System.Data;
using System.IO;

namespace LogDel.Utilities
{
    public static class LdUtility
    {
        public static void ToLogdel(this DataTable table, string path)
        {
            try
            {
                //null values cannot be processed
                if (table == null) return;

                var sw = new StreamWriter(path, false);
                //headers
                sw.Write("###");
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    sw.Write(table.Columns[i]);
                    if (i < table.Columns.Count - 1) sw.Write("!");
                }

                sw.Write(sw.NewLine);
                foreach (DataRow dr in table.Rows)
                {
                    for (var i = 0; i < table.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            var value = dr[i].ToString().CleanLogDel();
                            sw.Write(value);
                        }

                        if (i < table.Columns.Count - 1) sw.Write("!");
                    }

                    sw.Write(sw.NewLine);
                }

                sw.Close();
            }
            catch (Exception) //catch all exceptions
            {
                //ignore it
            }
        }
    }
}