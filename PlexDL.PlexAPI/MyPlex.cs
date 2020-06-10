//using System.Threading.Tasks;

using RestSharp;
using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    public class MyPlex : PlexRest
    {
        public User Authenticate(string username, string password)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "users/sign_in.xml";

            return Execute<User>(request, username, password);
        }

        public List<Server> GetServers(User user)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "pms/servers";

            var servers = Execute<List<Server>>(request, user);
            for (var i = 0; i < servers.Count; i++)
                servers[i].user = user;

            return servers;
        }
    }
}