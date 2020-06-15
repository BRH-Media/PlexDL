using System;
using System.IO;
using System.Reflection;

namespace GitHubUpdater
{
    public static class Globals
    {
        public static string UpdateRootDir { get; set; } = $@"{AssemblyDirectory}\update_files";

        //Credit: https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}