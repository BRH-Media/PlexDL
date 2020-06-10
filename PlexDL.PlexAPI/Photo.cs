using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    public class Photo : PlexItem
    {
        public Photo()
        {
        }

        public Photo(User user, Server server, string uri) : base(user, server, uri)
        {
        }

        public int ratingKey { get; set; }
        public int parentRatingKey { get; set; }
        public string parentKey { get; set; }
        public int year { get; set; }
        public string summary { get; set; }
        public List<Media> media { get; set; }
    }
}