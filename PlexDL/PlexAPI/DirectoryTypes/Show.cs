using System;

namespace PlexDL.PlexAPI.DirectoryTypes
{
    public class Show : Directory
    {
        public Show()
        {
        }

        public Show(PlexItem item) : base(item)
        {
        }

        public Show(User user, Server server, string uri) : base(user, server, uri)
        {
        }
    }
}