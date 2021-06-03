using PlexDL.Common.Logging;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using UIHelpers;

namespace PlexDL.Common.API.PlexAPI.Objects.AttributeTables
{
    public static partial class AttributeGatherers
    {
        public static DataTable TvAttributesFromObject(PlexTvShow content, bool silent = false)
        {
            var table = new DataTable("TvAttributes");
            var columnAttributeName = new DataColumn("Name", typeof(string));
            var columnAttributeValue = new DataColumn("Value");
            table.Columns.AddRange(
                new[]
                {
                    columnAttributeName,
                    columnAttributeValue
                });
            try
            {
                var season = new[] { "Season Name", content.Season };
                var episodeCount = new[] { "Episode Count", content.EpisodesInSeason.ToString() };
                var seasonNumber = new[] { "Season #", content.SeasonNumber.ToString() };
                var episodeNumber = new[] { "Episode #", content.EpisodeNumber.ToString() };
                var genre = new[] { "Genre", content.ContentGenre };
                var runtime = new[]
                    {"Runtime", Methods.CalculateTime(content.StreamInformation.ContentDuration)};
                var resolution = new[] { "Resolution", content.StreamResolution.ResolutionString() };
                var frameRate = new[] { "Frame-rate", FormatFramerate(content) };
                var size = new[] { "File size", Methods.FormatBytes(content.StreamInformation.ByteLength) };
                var container = new[] { "Container", MediaContainerFormats.FormatToDescription(content.StreamInformation.Container) };

                var newRows = new[]
                {
                    season,
                    episodeCount,
                    seasonNumber,
                    episodeNumber,
                    genre,
                    runtime,
                    resolution,
                    frameRate,
                    size,
                    container
                };

                foreach (object[] row in newRows)
                    table.Rows.Add(row);
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