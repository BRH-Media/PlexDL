using System;
using System.Xml.Serialization;

// ReSharper disable UnusedMember.Global

namespace PlexDL.MyPlex
{
    [Serializable]
    [XmlType(@"User")]
    public class User
    {
        [XmlAttribute(@"username")]
        public string Username { get; set; }

        [XmlAttribute(@"email")]
        public string Email { get; set; }

        [XmlAttribute(@"id")]
        public int Id { get; set; }

        [XmlAttribute(@"thumb")]
        public string Thumb { get; set; }

        [XmlAttribute(@"queueEmail")]
        public string QueueEmail { get; set; }

        [XmlAttribute(@"queueUid")]
        public string QueueUid { get; set; }

        [XmlAttribute(@"cloudSyncDevice")]
        public string CloudSyncDevice { get; set; }

        [XmlAttribute(@"authenticationToken")]
        public string AuthenticationToken { get; set; }

        [XmlAttribute(@"joinDate")]
        public DateTime JoinDate { get; set; }
    }
}