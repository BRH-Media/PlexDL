using System.Runtime.InteropServices;

namespace inet
{
    public static class ConnectionChecker
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        /// <summary>
        /// Uses the Win32 API to check whether Windows has a valid internet connection
        /// </summary>
        /// <returns></returns>
        public static bool CheckForInternetConnection()
        {
            return InternetGetConnectedState(out _, 0);
        }
    }
}