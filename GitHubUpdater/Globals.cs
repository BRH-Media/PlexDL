using System;
using System.IO;
using System.Reflection;

namespace GitHubUpdater
{
    public static class Globals
    {
        /// <summary>
        /// The folder where update files are downloaded/saved to
        /// </summary>
        public static string UpdateRootDir { get; set; } = $@"{AssemblyDirectory}\update_files";

        //Credit: https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
        /// <summary>
        /// Gets the folder of the current executable without the executable's file name
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetEntryAssembly()?.CodeBase;
                var uri = new UriBuilder(codeBase ?? string.Empty);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}