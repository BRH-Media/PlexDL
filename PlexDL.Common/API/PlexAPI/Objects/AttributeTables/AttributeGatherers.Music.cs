﻿using PlexDL.Common.Enums;
using PlexDL.Common.Globals.Providers;
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
        public static DataTable MusicAttributesFromObject(PlexMusic content, bool silent = false)
        {
            var table = new DataTable("MusicAttributes");
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
                var artist = new[] { "Artist", content.Artist };
                var album = new[] { "Album", content.Album };
                var genre = new[] { "Genre", content.ContentGenre };
                var duration = new[] { "Duration", Methods.CalculateTime(content.StreamInformation.ContentDuration) };
                var size = new[] { "File size", Methods.FormatBytes(content.StreamInformation.ByteLength) };
                var container = new[] { "Container", ObjectProvider.Settings.MetadataDisplay.MetadataContainerDisplay == MetadataContainerDisplayOption.Description
                    ? MediaContainerFormats.FormatToDescription(content.StreamInformation.Container)
                    : content.StreamInformation.Container
                };

                var newRows = new[]
                {
                    artist,
                    album,
                    genre,
                    duration,
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