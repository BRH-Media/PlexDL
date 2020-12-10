using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace LogDel.Utilities.Export
{
    public static class JsonUtility
    {
        public static void ToJson(this DataTable dt, string filePath)
        {
            try
            {
                var lst = (from DataRow row in dt.Rows
                           select dt.Columns.Cast<DataColumn>()
                               .ToDictionary(col => col.ColumnName, col => Convert.IsDBNull(row[col])
                                   ? null
                                   : row[col])).ToList();

                File.WriteAllText(filePath, JsonConvert.SerializeObject(lst, Formatting.Indented));
            }
            catch (Exception)
            {
                //ignore it
            }
        }
    }
}