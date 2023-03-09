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
        [XmlAttribute(@"accessToken")]
        public string AccessToken { get; set; }

        [XmlAttribute(@"name")]
        public string Name { get; set; }

        [XmlAttribute(@"address")]
        public string Address { get; set; }

        [XmlAttribute(@"port")]
        public int Port { get; set; }

        [XmlAttribute(@"version")]
        public string Version { get; set; }

        [XmlAttribute(@"host")]
        public string Host { get; set; }

        [XmlAttribute(@"localAddresses")]
        public string LocalAddresses { get; set; }

        [XmlAttribute(@"machineIdentifier")]
        public string MachineIdentifier { get; set; }

        [XmlAttribute(@"owned")]
        public int Owned { get; set; }

        [XmlAttribute(@"createdAt")]
        public string CreatedAt
        {
            get => _createDate.ToString(CultureInfo.CurrentCulture);
            set => _createDate = Utils.GetDateTimeFromTimestamp(value);
        }

        [XmlAttribute(@"updatedAt")]
        public string UpdatedAt
        {
            get => _lastUpdated.ToString(CultureInfo.CurrentCulture);
            set => _lastUpdated = Utils.GetDateTimeFromTimestamp(value);
        }

        [XmlElement(@"user")]
        public User User { get; set; }

        private DateTime _createDate;
        private DateTime _lastUpdated;

        protected override string GetBaseUrl()
        {
            return "http://" + Address + ":" + Port;
        }
    }
}