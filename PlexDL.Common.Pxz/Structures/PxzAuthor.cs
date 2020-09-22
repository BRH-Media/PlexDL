using System;
using System.DirectoryServices.AccountManagement;

namespace PlexDL.Common.Pxz.Structures
{
    public class PxzAuthor
    {
        public string MachineName { get; set; }
        public string UserAccount { get; set; }
        public string DisplayName { get; set; }

        public static PxzAuthor FromCurrent()
        {
            try
            {
                return new PxzAuthor
                {
                    MachineName = Environment.MachineName,
                    UserAccount = Environment.UserName,
                    DisplayName = CurrentDisplayName()
                };
            }
            catch
            {
                //blank author if there's an error
                return new PxzAuthor();
            }
        }

        private static string CurrentDisplayName()
        {
            var p = UserPrincipal.Current;
            return p.DisplayName;
        }
    }
}