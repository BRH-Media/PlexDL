using System;

namespace PlexAPI.DirectoryTypes
{
    public class Show : Directory
    {
        public Show()
        {
        }

        public Show(PlexItem item) : base(item)
        {
        }

        public Show(User user, Server server, String uri) : base(user, server, uri)
        {
        }
    }
}