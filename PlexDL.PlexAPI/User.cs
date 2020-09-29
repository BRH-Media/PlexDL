using System;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace PlexDL.MyPlex
{
    public class User
    {
        public string username { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public string thumb { get; set; }
        public string queueEmail { get; set; }
        public string queueUid { get; set; }
        public string cloudSyncDevice { get; set; }
        public string authenticationToken { get; set; }
        public DateTime joinDate { get; set; }
    }
}