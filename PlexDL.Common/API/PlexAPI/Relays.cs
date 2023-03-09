using PlexDL.Common.Logging;
using PlexDL.Common.Net;
using PlexDL.MyPlex;
using System;
using System.Collections.Generic;
using System.Xml;
using UIHelpers;

namespace PlexDL.Common.API.PlexAPI
{
    public static class Relays
    {
        public static List<Server> GetServerRelays()
        {
            try
            {
                var relays = new List<Server>();
                const string uri = "https://plex.tv/api/resources?includeHttps=1&includeRelay=1&X-Plex-Token=";

                //cache is disabled for relays. This is because they're always changing, and cache would need to be frequently cleared.
                var reply = XmlGet.GetXmlTransaction(uri, true, true, false);

                //an invalid reply isn't usable; so we just return the blank list declared above :)
                if (!Methods.PlexXmlValid(reply))
                    return relays;

                var root = reply.SelectSingleNode("MediaContainer");
                //UIMessages.Info(root.Name);

                if (root != null && !root.HasChildNodes) return relays;

                //null root node is bad news; so we can just return the blank list (no relays
                //are available anyway)
                if (root == null) return relays;

                for (var i = 0; i < root.ChildNodes.Count; i++)
                {
                    var node = root.ChildNodes[i];
                    var accessToken = "";

                    if (node.Attributes?["accessToken"] != null)
                        accessToken = node.Attributes["accessToken"].Value;

                    var name = node.Attributes?["name"].Value;

                    if (!node.HasChildNodes)
                        continue;

                    foreach (XmlNode n in node.ChildNodes)
                    {
                        var u = new Uri(n.Attributes?["uri"].Value ?? string.Empty);
                        var uParse = u.Host;
                        var local = n.Attributes?["address"].Value;

                        if (!uParse.Contains(".plex.direct") || Methods.IsPrivateIp(local))
                            continue;

                        var address = uParse;
                        var port = Convert.ToInt32(n.Attributes?["port"].Value);
                        var svrRelay = new Server
                        {
                            Address = address,
                            Port = port,
                            Name = name,
                            AccessToken = accessToken
                        };
                        relays.Add(svrRelay);
                        break;
                    }
                }

                return relays;
            }
            catch (Exception ex)
            {
                UIMessages.Error("Relay Retrieval Error\n\n" + ex, "Relay Data Error");
                LoggingHelpers.RecordException(ex.Message, "GetRelaysError");
                return new List<Server>();
            }
        }
    }
}