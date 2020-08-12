using LogDel.Utilities.Extensions;
using PlexDL.Common.Enums;
using PlexDL.Common.Security;
using System;
using System.Data;
using System.IO;
using LogDel.Enums;

namespace LogDel.Utilities
{
    public static class LdUtility
    {
        public static void ToLogdel(this DataTable table, string path, LogSecurity security = LogSecurity.Unprotected)
        {
            try
            {
                //null values cannot be processed
                if (table == null) return;

                //store all characters here
                var sw = new StringWriter();

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

                var contentToWrite = sw.ToString();

                //check if the content needs to be protected with DPAPI
                if (security == LogSecurity.Protected)
                {
                    //encrypt the log
                    var provider = new ProtectedString(contentToWrite, StringProtectionMode.Encrypt);

                    //replace the plainText contents with the new encrypted contents
                    contentToWrite = provider.ProcessedValue;
                }

                //finalise the log file
                File.WriteAllText(path, contentToWrite);
            }
            catch (Exception) //catch all exceptions
            {
                //ignore it
            }
        }
    }
}