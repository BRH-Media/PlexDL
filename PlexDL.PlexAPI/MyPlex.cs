//using System.Threading.Tasks;

using RestSharp;
using System.Collections.Generic;

namespace PlexDL.MyPlex
{
    public class MyPlex : PlexRest
    {
        public List<Server> GetServers(User user)
        {
            var request = new RestRequest(Method.GET) { Resource = "pms/servers" };
            var servers = Execute<List<Server>>(request, user);

            foreach (var t in servers)
                t.user = user;

            return servers;
        }
    }
}