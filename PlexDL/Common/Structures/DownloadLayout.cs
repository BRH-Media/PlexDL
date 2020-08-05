using PlexDL.Common.Structures.AppOptions;
using PlexDL.Common.Structures.Plex;
using System.IO;

namespace PlexDL.Common.Structures
{
    public static class DownloadLayout
    {
        public static readonly int MF_PLEX_STANDARD_LAYOUT = 0;
        public static readonly int MF_PLEX_DL_LAYOUT = 1;
        public static readonly int MF_NO_LAYOUT = 2;

        public static TvShowDirectoryLayout CreateDownloadLayoutTvShow(PlexTvShow show, ApplicationOptions settings,
            int layout)
        {
            var dirLayout = new TvShowDirectoryLayout();
            if (layout == MF_PLEX_STANDARD_LAYOUT)
            {
                var basePath = settings.Generic.DownloadDirectory + @"\TV\";
                var season = show.Season;
                var title = show.TvShowName.ToClean();
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

        public static MusicDirectoryLayout CreateDownloadLayoutMusic(PlexMusic track, ApplicationOptions settings,
            int layout)
        {
            var dirLayout = new MusicDirectoryLayout();
            if (layout == MF_PLEX_STANDARD_LAYOUT)
            {
                var basePath = settings.Generic.DownloadDirectory + @"\Music\";
                var album = track.Album;
                var artist = track.Artist.ToClean();
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