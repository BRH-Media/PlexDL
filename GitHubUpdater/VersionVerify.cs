using System;
using System.Text.RegularExpressions;

// ReSharper disable RedundantIfElseBlock

namespace GitHubUpdater
{
    public static class VersionVerify
    {
        public static Version ToValid(this Version v)
        {
            try
            {
                return new Version(v.ToString().ToValidVersionString());
            }
            catch
            {
                //ignore all errors
            }

            //default; return the original, unmodified Version object.
            return v;
        }

        public static string ToValidVersionString(this string version)
        {
            try
            {
                var versionString = version.GetValidVersionString();
                var versionSplit = versionString.Split('.');

                //valid version length is 6: <Build>.<Major>.<MajorRevision>.<Minor>.<MinorRevision>.<Revision>
                const int validLength = 6;

                if (versionSplit.Length > validLength)
                {
                    var newString = @"";

                    for (var i = 0; i < validLength; i++)
                    {
                        newString += $"{versionSplit[i]}.";
                    }

                    versionString = newString.TrimEnd('.');
                }

                return versionString;
            }
            catch
            {
                //ignore all errors
            }

            //default; return the original, unmodified Version string.
            return version;
        }

        /// <summary>
        /// Replaces all characters that aren't numbers or a dot (.) with a dot (.)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetValidVersionString(this string input)
        {
            var r = new Regex("[^0-9.]");
            return r.Replace(input, @".");
        }
    }
}