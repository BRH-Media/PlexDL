using PlexDL.Common.Logging;
using PlexDL.PlexAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.Common.API
{
    public static class Relays
    {
        public static List<Server> GetServerRelays(string token)
        {
            try
            {
                var relays = new List<Server>();
                var uri = "https://plex.tv/api/resources?includeHttps=1&amp;includeRelay=1&amp;X-Plex-Token=";

                //cache is disabled for relays. This is because they're always changing, and cache would need to be frequently cleared.
                var reply = XmlGet.GetXmlTransaction(uri, token, true, true);
                if (Methods.PlexXmlValid(reply))
                {
                    var root = reply.SelectSingleNode("MediaContainer");
                    //MessageBox.Show(root.Name);
                    if (root.HasChildNodes)
                        for (var i = 0; i < root.ChildNodes.Count; i++)
                        {
                            var node = root.ChildNodes[i];
                            //MessageBox.Show(node.Name);
                            var accessToken = "";
                            if (node.Attributes["accessToken"] != null)
                                accessToken = node.Attributes["accessToken"].Value;

                            var name = node.Attributes["name"].Value;
                            string address;
                            string local;
                            int port;
                            if (node.HasChildNodes)
                                foreach (XmlNode n in node.ChildNodes)
                                {
                                    var u = new Uri(n.Attributes["uri"].Value);
                                    var u_parse = u.Host;
                                    var u_parse_split = u_parse.Split('.');
                                    local = n.Attributes["address"].Value;

                                    //MessageBox.Show(u_parse_temp);
                                    if (u_parse.Contains(".plex.direct") && !Methods.IsPrivateIp(local))
                                    {
                                        address = u_parse;
                                        port = Convert.ToInt32(n.Attributes["port"].Value);
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
                        }
                }

                return relays;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Relay Retrieval Error\n\n" + ex, "Relay Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "GetRelaysError");
                return new List<Server>();
            }
        }
    }
}