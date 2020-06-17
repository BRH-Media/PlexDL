using System.Runtime.InteropServices;

namespace inet
{
    public static class ConnectionChecker
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool CheckForInternetConnection()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
    }
}