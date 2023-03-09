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
            var request = new RestRequest("pms/servers")
            {
                RootElement = @"MediaContainer"
            };
            var servers = Execute<GetServersResponse>(request, user);

            if (servers == null)
                return null;

            var finalReturn = new List<Server>();

            foreach (var t in servers.Servers)
            {
                t.User = user;

                if (t.Port > 0 && !IsPrivateIp(t.Address)
                               && !string.IsNullOrEmpty(t.Version)
                               && !string.IsNullOrEmpty(t.MachineIdentifier))
                    finalReturn.Add(t);
            }

            return finalReturn;
        }
    }
}