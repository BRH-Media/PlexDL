using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlexDL.MyPlex
{
    public class MyPlex : PlexRest
    {
        private static bool IsPrivateIp(string ipAddress)
        {
            var ipParts = ipAddress.Split(new[]
                {
                    "."
                }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
            // in private ip range
            return ipParts[0] == 10 ||
                   ipParts[0] == 192 && ipParts[1] == 168 ||
                   ipParts[0] == 172 && ipParts[1] >= 16 && ipParts[1] <= 31;

            // IP Address is probably public.
            // This doesn't catch some VPN ranges like OpenVPN and Hamachi.
        }

        public List<Server> GetServers(User user)
        {
            var request = new RestRequest("pms/servers");
            var servers = Execute<List<Server>>(request, user);

            var finalReturn = new List<Server>();

            foreach (var t in servers)
            {
                t.user = user;

                if (t.port > 0 && !IsPrivateIp(t.address)
                               && !string.IsNullOrEmpty(t.version)
                               && !string.IsNullOrEmpty(t.machineIdentifier))
                    finalReturn.Add(t);
            }

            return finalReturn;
        }
    }
}