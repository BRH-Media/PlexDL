using System;
using System.Globalization;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace PlexDL.MyPlex
{
    public class Server : PlexRest
    {
        public string accessToken { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int port { get; set; }
        public string version { get; set; }
        public string host { get; set; }
        public string localAddresses { get; set; }
        public string machineIdentifier { get; set; }
        public bool owned { get; set; }
        public User user { get; set; }

        public string createdAt
        {
            get => createDate.ToString(CultureInfo.CurrentCulture);
            set => createDate = Utils.GetDateTimeFromTimestamp(value);
        }

        public string updatedAt
        {
            get => lastUpdated.ToString(CultureInfo.CurrentCulture);
            set => lastUpdated = Utils.GetDateTimeFromTimestamp(value);
        }

        public DateTime createDate { get; set; }
        public DateTime lastUpdated { get; set; }

        protected override string GetBaseUrl()
        {
            return "http://" + address + ":" + port;
        }
    }
}