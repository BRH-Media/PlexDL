using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using UIHelpers;

namespace PlexDL.Common.API.PlexAPI.Objects.AttributeTables
{
    public static partial class AttributeGatherers
    {
        public static DataTable AttributesFromObject(object content, bool silent = false)
        {
            var table = new DataTable();

            try
            {
                var contentType = content.GetType();
                var moviesType = typeof(PlexMovie);
                var musicType = typeof(PlexMusic);
                var tvShowType = typeof(PlexTvShow);

                if (contentType == moviesType)
                    table = MovieAttributesFromObject((PlexMovie)content, silent);
                else if (contentType == musicType)
                    table = MusicAttributesFromObject((PlexMusic)content, silent);
                else if (contentType == tvShowType)
                    table = TvAttributesFromObject((PlexTvShow)content, silent);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "AttributeTableError");
                if (!silent)
                    UIMessages.Error("Error occurred whilst building content attribute table:\n\n" + ex, @"Data Error");
            }

            return table;
        }
    }
}