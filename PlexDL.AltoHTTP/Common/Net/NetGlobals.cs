using System;
using System.Reflection;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace PlexDL.AltoHTTP.Common.Net
{
    public static class NetGlobals
    {
        public static string GlobalUserAgent { get; set; } = $@"PlexDL/{CurrentClientVersion} {OSVersionString}";

        public static string CurrentClientVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string OSVersionString => $@"{Environment.OSVersion.Platform}_{Environment.OSVersion.Version.Major}_{Environment.OSVersion.Version.Minor}";
        public static int Timeout { get; set; } = 0;
    }
}