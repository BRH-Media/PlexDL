using System;
using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    public class Video : PlexItem
    {
        public int ratingKey { get; set; }
        public int parentRatingKey { get; set; }
        public string parentKey { get; set; }
        public string studio { get; set; }
        public string contentRating { get; set; }
        public string summary { get; set; }
        public float rating { get; set; }
        public int viewCount { get; set; }
        public int year { get; set; }
        public string tagline { get; set; }
        public long duration { get; set; }
        public string originallyAvailableAt { get; set; }
        public string addedAt { get; set; }
        public List<Media> media { get; set; }
        public List<Genre> genres { get; set; }
        public List<Writer> writers { get; set; }
        public List<Media> directors { get; set; }
        public List<Country> countries { get; set; }
        public List<Role> roles { get; set; }

        public bool secondary { get; set; }
        public bool search { get; set; }
        public string prompt { get; set; }

        public Video()
        {
        }

        public Video(User user, Server server, String uri) : base(user, server, uri)
        {
        }
    }
}