using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using UIHelpers;

namespace PlexDL.Common.API.PlexAPI.Objects.AttributeTables
{
    public static partial class AttributeGatherers
    {
        public static DataTable MovieAttributesFromObject(PlexMovie content, bool silent = false)
        {
            var table = new DataTable("MovieAttributes");
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
                var genre = new[] { "Genre", content.ContentGenre };
                var runtime = new[] { "Runtime", Methods.CalculateTime(content.StreamInformation.ContentDuration) };
                var resolution = new[] { "Resolution", content.StreamResolution.ResolutionString() };
                var frameRate = new[] { "Frame-rate", FormatFramerate(content) };
                var size = new[] { "File size", Methods.FormatBytes(content.StreamInformation.ByteLength) };
                var container = new[] { "Container", content.StreamInformation.Container };

                var newRows = new[]
                {
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