using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PlexDL.Common.Structures
{
    public class DownloadLayouts
    {
        public static readonly int PlexStandardLayout = 0;
        public static readonly int PlexDLLayout = 1;
        public static readonly int NoLayout = 2;

        public static TVShowDirectoryLayout CreateDownloadLayoutTVShow(PlexTVShow show, AppOptions settings, int layout)
        {
            TVShowDirectoryLayout dirLayout = new TVShowDirectoryLayout();
            if (layout == DownloadLayouts.PlexStandardLayout)
            {
                string basePath = settings.Generic.DownloadDirectory + @"\TV\";
                string season = show.Season;
                string title = Methods.removeIllegalCharacters(show.TVShowName);
                string seasonPath = basePath + title + @"\" + season;
                string titlePath = basePath + title;

                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
                if (!Directory.Exists(titlePath))
                    Directory.CreateDirectory(titlePath);
                if (!Directory.Exists(seasonPath))
                    Directory.CreateDirectory(seasonPath);
                dirLayout.TitlePath = titlePath;
                dirLayout.SeasonPath = seasonPath;
                dirLayout.BasePath = basePath;
            }
            return dirLayout;
        }
    }

    public class TVShowDirectoryLayout
    {
        public string TitlePath { get; set; } = "";
        public string SeasonPath { get; set; } = "";
        public string BasePath { get; set; } = "";
    }

    public class TVShowMetadata
    {
    }

    [XmlInclude(typeof(PlexMovie))]
    [XmlInclude(typeof(PlexTVShow))]
    [Serializable]
    public class PlexObject
    {
        public DownloadInfo StreamInformation { get; set; } = new DownloadInfo();
        public int StreamIndex { get; set; } = 0;
        public Resolution StreamResolution { get; set; } = new Resolution();
        public string StreamPosterUri { get; set; } = "";
        public string ContentGenre { get; set; } = "";
        public long ContentDuration { get; set; } = 0;
        public List<PlexActor> Actors { get; set; } = new List<PlexActor>();
    }

    [Serializable]
    public class PlexMovie : PlexObject
    {
        //used as an easier-to-read class. It doesn't serve a real purpose.
    }

    [Serializable]
    public class PlexTVShow : PlexObject
    {
        public string TVShowName { get; set; } = "";
        public string Season { get; set; } = "";

        public int EpisodesInSeason { get; set; } = 0;
    }

    public class Resolution
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public string ResolutionString()
        {
            return Width + "x" + Height;
        }

        public string ResolutionShorthand()
        {
            return Height + "p";
        }
    }

    public class ConnectionInformation
    {
        public int PlexPort { get; set; } = 32400;
        public string PlexAddress { get; set; } = "";
        public string PlexAccountToken { get; set; } = "";
        [NonSerialized]
        public bool RelaysOnly = false;
    }

    public class DownloadInfo
    {
        public string Link { get; set; } = "";
        public string Container { get; set; } = "";
        public long ByteLength { get; set; } = 0;
        public long ContentDuration { get; set; } = 0;
        public string DownloadPath { get; set; } = "";
        public string FileName { get; set; } = "";
        public string ContentTitle { get; set; } = "";
        public string ContentThumbnailUri { get; set; } = "";
    }

    public class PlexActor
    {
        public string ThumbnailUri { get; set; } = "";
        public string ActorName { get; set; } = "";
        public string ActorRole { get; set; } = "";
    }
}