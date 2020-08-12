using LogDel.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

// ReSharper disable CoVariantArrayConversion

namespace LogDel
{
    public static class LogReader
    {
        public static DataTable TableFromFile(string fileName, bool silent = true, bool lineNumbers = true)
        {
            //the table that we're creating from the .logdel file
            DataTable table = null;
            try
            {
                //check if specified file exists
                if (File.Exists(fileName))
                {
                    //initialise the table
                    table = new DataTable();
                    //check if we're meant to be including line numbers
                    //--this will insert an incremented number for every row
                    //in a new "Line" column with ColIndex 0--
                    if (lineNumbers)
                    {
                        //define our new "Line" column as a string
                        //column
                        var lineNumber = new DataColumn
                        {
                            ColumnName = "Line",
                            Caption = "Line",
                            DataType = typeof(string)
                        };
                        //add the column to the return table
                        table.Columns.Add(lineNumber);
                    }

                    //define the row counter; this begins at 0 and is incremented at the START of the loop
                    var intRowCount = 0;

                    //headers in .logdel files are ALWAYS the first line in the file.
                    //they begin with ### (three hash tags), and are separated by ! (exclamation mark)
                    //we need a way of knowing if the loop has already parsed headers, so if it has,
                    //this will be set to true.
                    var headersFound = false;

                    //store all raw ASCII log lines here
                    var logContents = File.ReadAllText(fileName);

                    //store processed log lines here
                    var log = SecurityUtils.ProtectionEnabled()
                        ? SecurityUtils.DecryptLog(logContents)
                        : FileHelper.SplitLogLines(logContents);

                    //loop through every line in the .logdel file
                    foreach (var line in log)
                    {
                        //increment the row counter
                        intRowCount++;

                        //the header is always on the first line, so if the row count is 2, and no headers have been found
                        //(headersFound == false), then there were probably no headers defined in the file.
                        if (intRowCount == 2 && !headersFound)
                        {
                            //parsing improvised headers won't work without delimiters.
                            //If there aren't any delimiters, we can add a temporary "Entry" column,
                            //and treat all rows as single-celled.
                            if (line.Contains("!"))
                            {
                                /*
                                    Because no headers were defined, we need to improvise.
                                    Splitting at ! (exclamation mark) will give the amount of cells in this row,
                                    since ! is the delimiter, but we still need to provide columns for those cells.
                                    Hence, we will include a numerical field notation for each column. Like so:

                                    +------+------+------+
                                    |field0|field1|field2|
                                    +------+------+------+
                                    |value0|value1|value2|
                                    +------+------+------+
                                */
                                var arrSplit = line.Split('!');

                                //loop the amount of times equal to the cell-count (arrSplit.Length)
                                for (var i = 0; i < arrSplit.Length; i++)
                                {
                                    //define new column for the table, and set the field values to the current 'i'
                                    //index. This will create our numerical field notation.
                                    var c = new DataColumn
                                    {
                                        ColumnName = "field" + i,
                                        Caption = "field" + i,
                                        DataType = typeof(string)
                                    };

                                    //add this column to the table
                                    table.Columns.Add(c);
                                }
                            }
                            //the current line isn't LogDel compliant, but we can still try and parse it
                            //by making it a single-celled row.
                            else
                            {
                                //we do this by adding a new column called "Entry", then later adding the entire line to it
                                //if it isn't LogDel.
                                var c = new DataColumn
                                {
                                    ColumnName = "Entry",
                                    Caption = "Entry",
                                    DataType = typeof(string)
                                };

                                //add the column
                                table.Columns.Add(c);
                            }
                        }

                        //check if the line starts with ### (the line is a header if it begins with this), and if the rowCount is 1
                        //(headers can only be on the first line, and also if we haven't found any headers yet (this is a one-time check on row 1).
                        if (line.StartsWith("###") && intRowCount == 1 && !headersFound)
                        {
                            //headers were found, specify this.
                            headersFound = true;

                            //split row 1 (where we are currently at, and the header row) at the '!' delimiter.
                            var arrSplit = line.Split('!');

                            //Remove hash tags from the first cell (header indicator)
                            arrSplit[0] = arrSplit[0].Remove(0, 3);

                            //Add headers to table
                            foreach (var item in arrSplit)
                            {
                                var c = new DataColumn
                                {
                                    ColumnName = item,
                                    Caption = item,
                                    DataType = typeof(string)
                                };
                                table.Columns.Add(c);
                            }
                        }
                        //all of the above checks failed, so check if the line contains the '!' delimiter; if it does, then it should be a
                        //value row and we can process it as LogDel.
                        else if (line.Contains("!"))
                        {
                            //split the line into cells via the '!' delimiter.
                            var strSplit = line.Split('!');

                            //define a new List in order to add line numbers (if enabled).
                            //This is needed due to string[] arrays being fixed.
                            var arrItems = new List<string>();

                            //check if we're meant to be including line numbers
                            if (lineNumbers)
                                //we are, so add the current row count - 1.
                                //we subtract 1, because the row count includes the header row.
                                //If we didn't do this, it would show up in a grid as the first row being
                                //line 2, instead of line 1. This is added first, so that it is the first item
                                //in the array (first column value).
                                arrItems.Add((intRowCount - 1).ToString());

                            //add the rest of the cells.
                            arrItems.AddRange(strSplit);

                            //convert the dynamic array (List) back into a static string[] array
                            var items = arrItems.ToArray();

                            //check if the cell-count (amount of values in the items[] array) is equal
                            //to the header-count (amount of Columns in our table).
                            //If this isn't equal, it will error out, and complain that it can't add the row.
                            //So to fix that, we only add it if they're both equal.
                            if (items.Length == table.Columns.Count)
                                table.Rows.Add(items);
                        }
                        //none of the above were satisfied as true, this means that the current line
                        //is not recognisable as LogDel-compliant, and hence, is probably a regular text-file.
                        else
                        {
                            //to verify that it isn't LogDel, we can check to see if no headers have been found.
                            if (headersFound) continue; //headers were found, don't worry about the next section of code :)

                            //define a new List in order to add line numbers (if enabled).
                            //This is needed due to string[] arrays being fixed.
                            var arrItems = new List<string>();

                            //check if we're meant to be including line numbers
                            if (lineNumbers)
                                //we are, so add the current row count - 1.
                                //we subtract 1, because the row count includes the header row.
                                //If we didn't do this, it would show up in a grid as the first row being
                                //line 2, instead of line 1. This is added first, so that it is the first item
                                //in the array (first column value).
                                arrItems.Add((intRowCount - 1).ToString());

                            //add the whole line as a single cell
                            arrItems.Add(line);

                            //convert the dynamic array (List) back into a static string[] array
                            var items = arrItems.ToArray();

                            //check if the cell-count (amount of values in the items[] array) is equal
                            //to the header-count (amount of Columns in our table).
                            //If this isn't equal, it will error out, and complain that it can't add the row.
                            //So to fix that, we only add it if they're both equal.
                            if (items.Length == table.Columns.Count)
                                table.Rows.Add(items);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }
    }
}