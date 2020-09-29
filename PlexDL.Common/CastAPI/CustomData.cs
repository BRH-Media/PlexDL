namespace PlexDL.Common.CastAPI
{
    public class CustomData
    {
        public int offset { get; set; } = 0;
        public bool directPlay { get; set; } = true;
        public bool directStream { get; set; } = true;
        public int subtitleSize { get; set; } = 100;
        public int audioBoost { get; set; } = 100;
        public Server server { get; set; }
        public User user { get; set; }
        public string containerKey { get; set; }

        public static CustomData FillFromApiServer(MyPlex.Server svr, bool transcode = false)
        {
            var data = new CustomData();
            var user = svr.user;
            var newServer = new Server
            {
                machineIdentifier = svr.machineIdentifier,
                transcoderAudio = transcode,
                transcoderVideo = transcode,
                transcoderVideoRemuxOnly = transcode,
                version = svr.version,
                myPlexSubscription = false, //it's possible that this is true, but always default to false just in case.
                isVerifiedHostname = true, //bad things will happen if this isn't true
                protocol = "http", //https needs a certificate and Windows will get cranky if it doesn't have one; http just to be on the safe side.
                address = svr.address,
                port = svr.port,
                accessToken = svr.accessToken
            };

            data.user = new User { username = svr.user.username };
            data.server = newServer;

            return data;
        }
    }

    public class User
    {
        public string username { get; set; }
    }

    public class Server
    {
        public string machineIdentifier { get; set; }
        public bool transcoderVideo { get; set; }
        public bool transcoderVideoRemuxOnly { get; set; }
        public bool transcoderAudio { get; set; }
        public string version { get; set; }
        public bool myPlexSubscription { get; set; }
        public bool isVerifiedHostname { get; set; }
        public string protocol { get; set; }
        public string address { get; set; }
        public int port { get; set; }
        public string accessToken { get; set; }
    }
}