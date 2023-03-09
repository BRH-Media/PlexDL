using PlexDL.MyPlex;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable

namespace PlexDL.Common.API.CastAPI
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
            var user = svr.User;
            var newServer = new Server
            {
                machineIdentifier = svr.MachineIdentifier,
                transcoderAudio = transcode,
                transcoderVideo = transcode,
                transcoderVideoRemuxOnly = transcode,
                version = svr.Version,
                myPlexSubscription = false, //it's possible that this is true, but always default to false just in case.
                isVerifiedHostname = true, //bad things will happen if this isn't true
                protocol = "http", //https needs a certificate and Windows will get cranky if it doesn't have one; http just to be on the safe side.
                address = svr.Address,
                port = svr.Port,
                accessToken = svr.AccessToken
            };

            data.user = svr.User;
            data.server = newServer;

            return data;
        }
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