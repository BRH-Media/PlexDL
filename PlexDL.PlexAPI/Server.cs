using System;
using System.Globalization;
using System.Xml.Serialization;

// ReSharper disable UnusedMember.Global

namespace PlexDL.MyPlex
{
    [Serializable]
    [XmlType(@"Server")]
    public class Server : PlexRest
    {
        [XmlElement(@"accessToken")]
        public string AccessToken { get; set; }

        [XmlElement(@"name")]
        public string Name { get; set; }

        [XmlElement(@"address")]
        public string Address { get; set; }

        [XmlElement(@"port")]
        public int Port { get; set; }

        [XmlElement(@"version")]
        public string Version { get; set; }

        [XmlElement(@"host")]
        public string Host { get; set; }

        [XmlElement(@"localAddresses")]
        public string LocalAddresses { get; set; }

        [XmlElement(@"machineIdentifier")]
        public string MachineIdentifier { get; set; }

        [XmlElement(@"owned")]
        public bool Owned { get; set; }

        [XmlElement(@"user")]
        public User User { get; set; }

        [XmlElement(@"createdAt")]
        public string CreatedAt
        {
            get => _createDate.ToString(CultureInfo.CurrentCulture);
            set => _createDate = Utils.GetDateTimeFromTimestamp(value);
        }

        [XmlElement(@"updatedAt")]
        public string UpdatedAt
        {
            get => _lastUpdated.ToString(CultureInfo.CurrentCulture);
            set => _lastUpdated = Utils.GetDateTimeFromTimestamp(value);
        }

        private DateTime _createDate;
        private DateTime _lastUpdated;

        protected override string GetBaseUrl()
        {
            return "http://" + Address + ":" + Port;
        }
    }
}