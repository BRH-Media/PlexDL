namespace PlexDL.PlexAPI
{
    public class PlexItem : PlexRest
    {
        public string uri { get; set; }

        public string key { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string thumb { get; set; }
        public string banner { get; set; }
        public string art { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public int index { get; set; }

        public Server server { get; set; }
        public User user { get; set; }

        public PlexItem()
        {
        }

        public PlexItem(User user, Server server, string uri)
        {
            this.user = user;
            this.server = server;
            this.uri = uri;
        }

        public PlexItem(PlexItem item)
        {
            Utils.CopyFrom<PlexItem, PlexItem>(this, item);
        }

        protected override string GetBaseUrl()
        {
            return "http://" + server.address + ":" + server.port;
        }
    }
}