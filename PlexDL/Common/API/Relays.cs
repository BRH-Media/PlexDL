using PlexAPI;
using PlexDL.Common.Logging;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.Common.API
{
    public class Relays
    {
        public static List<Server> GetServerRelays(string token, bool matchCurrentList = true)
        {
            try
            {
                List<Server> relays = new List<Server>();
                string uri = "https://plex.tv/api/resources?includeHttps=1&amp;includeRelay=1&amp;X-Plex-Token=";

                //cache is disabled for relays. This is because they're always changing, and cache would need to be frequently cleared.
                XmlDocument reply = XmlGet.GetXMLTransaction(uri, token, true, true);
                if (Methods.PlexXmlValid(reply))
                {
                    XmlNode root = reply.SelectSingleNode("MediaContainer");
                    //MessageBox.Show(root.Name);
                    if (root.HasChildNodes)
                    {
                        for (int i = 0; i < root.ChildNodes.Count; i++)
                        {
                            XmlNode node = root.ChildNodes[i];
                            //MessageBox.Show(node.Name);
                            string accessToken = "";
                            if (node.Attributes["accessToken"] != null)
                            {
                                accessToken = node.Attributes["accessToken"].Value.ToString();
                            }
                            string name = node.Attributes["name"].Value.ToString();
                            string address = "";
                            string local = "";
                            int port = 0;
                            if (node.HasChildNodes)
                            {
                                foreach (XmlNode n in node.ChildNodes)
                                {
                                    Uri u = new Uri(n.Attributes["uri"].Value.ToString());
                                    string u_parse = u.Host;
                                    string[] u_parse_split = u_parse.Split('.');
                                    local = n.Attributes["address"].Value.ToString();

                                    //MessageBox.Show(u_parse_temp);
                                    if (u_parse.Contains(".plex.direct") && (!Methods.IsPrivateIP(local)))
                                    {
                                        address = u_parse;
                                        port = Convert.ToInt32(n.Attributes["port"].Value);
                                        Server svrRelay = new Server()
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
                    }
                    return relays;
                }
                else
                    return new List<Server>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Relay Retrieval Error\n\n" + ex.ToString(), "Relay Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.recordException(ex.Message, "GetRelaysError");
                return new List<Server>();
            }
        }
    }
}