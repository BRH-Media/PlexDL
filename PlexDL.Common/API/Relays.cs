using PlexDL.Common.Logging;
using PlexDL.PlexAPI;
using System;
using System.Collections.Generic;
using System.Xml;
using UIHelpers;

namespace PlexDL.Common.API
{
    public static class Relays
    {
        public static List<Server> GetServerRelays(string token)
        {
            try
            {
                var relays = new List<Server>();
                const string uri = "https://plex.tv/api/resources?includeHttps=1&amp;includeRelay=1&amp;X-Plex-Token=";

                //cache is disabled for relays. This is because they're always changing, and cache would need to be frequently cleared.
                var reply = XmlGet.GetXmlTransaction(uri, token, true, true);

                //an invalid reply isn't usable; so we just return the blank list declared above :)
                if (!Methods.PlexXmlValid(reply)) return relays;

                var root = reply.SelectSingleNode("MediaContainer");
                //UIMessages.Info(root.Name);

                if (root != null && !root.HasChildNodes) return relays;

                //null root node is bad news; so we can just return the blank list (no relays
                //are available anyway)
                if (root == null) return relays;

                for (var i = 0; i < root.ChildNodes.Count; i++)
                {
                    var node = root.ChildNodes[i];
                    //UIMessages.Info(node.Name);
                    var accessToken = "";
                    if (node.Attributes?["accessToken"] != null)
                        accessToken = node.Attributes["accessToken"].Value;

                    var name = node.Attributes?["name"].Value;

                    if (!node.HasChildNodes) continue;

                    foreach (XmlNode n in node.ChildNodes)
                    {
                        var u = new Uri(n.Attributes?["uri"].Value ?? string.Empty);
                        var u_parse = u.Host;
                        var local = n.Attributes?["address"].Value;

                        //UIMessages.Info(u_parse_temp);

                        if (!u_parse.Contains(".plex.direct") || Methods.IsPrivateIp(local)) continue;

                        var address = u_parse;
                        var port = Convert.ToInt32(n.Attributes?["port"].Value);
                        var svrRelay = new Server
                        {
                            address = address,
                            port = port,
                            name = name,
                            accessToken = accessToken
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