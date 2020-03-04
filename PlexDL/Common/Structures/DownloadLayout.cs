using System.IO;

namespace PlexDL.Common.Structures
{
    public static class DownloadLayout
    {
        public static readonly int PlexStandardLayout = 0;
        public static readonly int PlexDLLayout = 1;
        public static readonly int NoLayout = 2;

        public static TVShowDirectoryLayout CreateDownloadLayoutTVShow(Plex.PlexTVShow show, AppOptions.ApplicationOptions settings, int layout)
        {
            TVShowDirectoryLayout dirLayout = new TVShowDirectoryLayout();
            if (layout == DownloadLayout.PlexStandardLayout)
            {
                string basePath = settings.Generic.DownloadDirectory + @"\TV\";
                string season = show.Season;
                string title = Methods.RemoveIllegalCharacters(show.TVShowName);
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
}