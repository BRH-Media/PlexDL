using PlexDL.PlexAPI.LoginHandler;
using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Caching
{
    [Serializable]
    public class CacheMode
    {
        private bool _authCaching = true;

        [DisplayName("Server Caching")]
        [Description(
            "Instead of grabbing servers from Plex.tv every time, PlexDL can cache server lists for you (this does not apply to relays). You can disable this if your servers frequently change locations.")]
        public bool EnableServerCaching { get; set; } = true;

        [DisplayName("Image Caching")]
        [Description(
            "Instead of grabbing images from various servers every time (which can be very performance intensive), PlexDL can cache these images for you.")]
        public bool EnableThumbCaching { get; set; } = true;

        [DisplayName("API Caching")]
        [Description(
            "API Caching enables PlexDL to cache all API requests to a PMS server. This means content lists, metadata, library sections and other important information doesn't have to be downloaded again.")]
        public bool EnableXmlCaching { get; set; } = true;

        [DisplayName("Auth Caching")]
        [Description(
            "Auth Caching enables PlexDL to cache your Plex.tv token. This means that your token does not have to be requested every time you login, as it's stored locally.")]
        public bool EnableAuthCaching
        {
            get => _authCaching;
            set
            {
                TokenManager.TokenCachingEnabled = value; //apply token caching flag to the LoginManager module
                _authCaching = value;
            }
        }

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}