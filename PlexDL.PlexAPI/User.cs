using System;
using System.Xml.Serialization;

// ReSharper disable UnusedMember.Global

namespace PlexDL.MyPlex
{
    [Serializable]
    public class User
    {
        [XmlElement(@"username")]
        public string Username { get; set; }

        [XmlElement(@"email")]
        public string Email { get; set; }

        [XmlElement(@"id")]
        public int Id { get; set; }

        [XmlElement(@"thumb")]
        public string Thumb { get; set; }

        [XmlElement(@"queueEmail")]
        public string QueueEmail { get; set; }

        [XmlElement(@"queueUid")]
        public string QueueUid { get; set; }

        [XmlElement(@"cloudSyncDevice")]
        public string CloudSyncDevice { get; set; }

        [XmlElement(@"authenticationToken")]
        public string AuthenticationToken { get; set; }

        [XmlElement(@"joinDate")]
        public DateTime JoinDate { get; set; }
    }
}