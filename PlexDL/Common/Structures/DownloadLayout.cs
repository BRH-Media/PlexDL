using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.Plex;
using System.IO;

namespace PlexDL.Common.Structures
{
    public static class DownloadLayout
    {
        public static readonly int PlexStandardLayout = 0;
        public static readonly int PlexDlLayout = 1;
        public static readonly int NoLayout = 2;

        public static TvShowDirectoryLayout CreateDownloadLayoutTvShow(PlexTvShow show, ApplicationOptions settings, int layout)
        {
            var dirLayout = new TvShowDirectoryLayout();
            if (layout == PlexStandardLayout)
            {
                var basePath = settings.Generic.DownloadDirectory + @"\TV\";
                var season = show.Season;
                var title = Methods.RemoveIllegalCharacters(show.TvShowName);
                var seasonPath = basePath + title + @"\" + season;
                var titlePath = basePath + title;

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

        public static MusicDirectoryLayout CreateDownloadLayoutMusic(PlexMusic track, ApplicationOptions settings, int layout)
        {
            var dirLayout = new MusicDirectoryLayout();
            if (layout == PlexStandardLayout)
            {
                var basePath = settings.Generic.DownloadDirectory + @"\Music\";
                var album = track.Album;
                var artist = Methods.RemoveIllegalCharacters(track.Artist);
                var albumPath = basePath + artist + @"\" + album;
                var artistPath = basePath + artist;

                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
                if (!Directory.Exists(artistPath))
                    Directory.CreateDirectory(artistPath);
                if (!Directory.Exists(albumPath))
                    Directory.CreateDirectory(albumPath);
                dirLayout.ArtistPath = artistPath;
                dirLayout.AlbumPath = albumPath;
                dirLayout.BasePath = basePath;
            }

            return dirLayout;
        }
    }
}