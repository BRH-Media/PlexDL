using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PlexDL.Common.Logging
{
    public static class LogFileParser
    {
        public static DataTable TableFromFile(string fileName, bool silent = true)
        {
            DataTable table = null;
            try
            {
                if (File.Exists(fileName))
                {
                    table = new DataTable();
                    var lineNumber = new DataColumn
                    {
                        ColumnName = "Line", Caption = "Line", DataType = typeof(string)
                    };
                    table.Columns.Add(lineNumber);
                    var intRowCount = 1;
                    var headersFound = false;
                    foreach (var line in File.ReadAllLines(fileName))
                    {
                        if (intRowCount == 2 && !headersFound)
                        {
                            var arrSplit = line.Split('!');
                            var headerCount = 0;
                            foreach (var i in arrSplit)
                            {
                                var c = new DataColumn
                                {
                                    ColumnName = "field" + headerCount, Caption = "field" + headerCount, DataType = typeof(string)
                                };
                                table.Columns.Add(c);
                                ++headerCount;
                            }
                        }

                        if (line.StartsWith("###") && intRowCount == 1 && !headersFound)
                        {
                            headersFound = true;
                            var arrSplit = line.Split('!');
                            //Remove hashtags (header indicator)
                            arrSplit[0] = arrSplit[0].Remove(0, 3);
                            //Add headers to datagridview
                            foreach (var item in arrSplit)
                            {
                                var c = new DataColumn
                                {
                                    ColumnName = item, Caption = item, DataType = typeof(string)
                                };
                                table.Columns.Add(c);
                            }
                        }
                        else if (line.Contains("!"))
                        {
                            var strSplit = line.Split('!');
                            var arrItems = new List<string>
                            {
                                (intRowCount - 1).ToString()
                            };
                            arrItems.AddRange(strSplit);
                            var items = arrItems.ToArray();
                            if (items.Length == table.Columns.Count)
                                table.Rows.Add(items);
                        }
                        else
                        {
                            var arrItems = new List<string>
                            {
                                (intRowCount - 1).ToString(), line
                            };
                            var items = arrItems.ToArray();
                            if (items.Length == table.Columns.Count)
                                table.Rows.Add(items);
                        }

                        intRowCount += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, @"LogParseError");
            }

            return table;
        }
    }
}