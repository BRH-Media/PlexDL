using System.Runtime.InteropServices;

namespace wininet
{
    public static class ConnectionChecker
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckForInternetConnection()
        {
            return InternetGetConnectedState(out _, 0);
        }
    }
}