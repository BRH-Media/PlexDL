using System;
using System.Text;

// ReSharper disable RedundantIfElseBlock

namespace LogDel.Utilities.Extensions
{
    public static class LineParseHelper
    {
        public static string[] LogEntryParse(this string[] lineEntry)
        {
            try
            {
                //validation
                if (lineEntry?.Length > 0)
                {
                    //stores all properly decoded entries
                    var decodedEntries = new string[lineEntry.Length];

                    //go through each entry
                    for (var i = 0; i < lineEntry.Length; i++)
                    {
                        //check for base64-encoded entry
                        if (lineEntry[i].StartsWith(LogDelGlobals.LogBase64Prefix))
                        {
                            //remove prefix
                            var encodedEntry = lineEntry[i].Remove(0, LogDelGlobals.LogBase64Prefix.Length);

                            //decode entry
                            var entry = Encoding.Default.GetString(Convert.FromBase64String(encodedEntry));

                            //apply decoded entry
                            decodedEntries[i] = entry;
                        }
                        else

                            //apply the entry as-is
                            decodedEntries[i] = lineEntry[i];
                    }

                    //return final result
                    return decodedEntries;
                }
            }
            catch (Exception)
            {
                //ignore it
            }

            //default return
            return new string[] { };
        }
    }
}