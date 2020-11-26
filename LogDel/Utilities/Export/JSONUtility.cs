using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace LogDel.Utilities.Export
{
    public static class JsonUtility
    {
        public static void ToJson(this DataTable dt, string filePath)
        {
            try
            {
                var lst = new List<Dictionary<string, object>>();
                Dictionary<string, object> item;
                foreach (DataRow row in dt.Rows)
                {
                    item = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                        item.Add(col.ColumnName, Convert.IsDBNull(row[col]) ? null : row[col]);
                    lst.Add(item);
                }

                File.WriteAllText(filePath, JsonConvert.SerializeObject(lst, Formatting.Indented));
            }
            catch (Exception)
            {
                //ignore it
            }
        }
    }
}