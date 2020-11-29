using System;

namespace PlexDL.Common.Caching
{
    /// <summary>
    /// Specifies caching file directories; not to be hardcoded elsewhere.
    /// </summary>
    public static class CachingFileDir
    {
        /// <summary>
        /// The current user's %APPDATA% folder
        /// </summary>
        public static string UserAppData { get; } =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// The root folder for PlexDL's caching; e.g. '.\caching'
        /// </summary>
        public static string RootCacheDirectory { get; } = UserAppData + @"\.plexdl\caching";

        /// <summary>
        /// The relative directory for XML storage; requires the root directory and extra directories in addition.
        /// </summary>
        public static string XmlRelativeDirectory { get; } = @"\xml";

        /// <summary>
        /// The relative directory for image storage; requires the root directory and extra directories in addition.
        /// </summary>
        public static string ThumbRelativeDirectory { get; } = @"\thumb";
    }
}