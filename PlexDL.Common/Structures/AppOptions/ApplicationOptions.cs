﻿using PlexDL.Common.Structures.AppOptions.Caching;
using PlexDL.Common.Structures.AppOptions.Display;
using PlexDL.Common.Structures.AppOptions.Metadata;
using PlexDL.Common.Structures.AppOptions.Player;
using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Category("Application Settings")]
    [DisplayName("Application Settings")]
    [Description("View and modify all available PlexDL application settings.")]
    public class ApplicationOptions
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Connection Settings")]
        [Description("View Plex connection settings. You can't directly change these values.")]
        public ConnectionInfo ConnectionInfo { get; set; } = new ConnectionInfo();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Logging Settings")]
        [Description("Values related to PlexDL logging")]
        public LoggingSettings Logging { get; set; } = new LoggingSettings();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Grid Display Settings")]
        [Description("Values related to PlexDL data rendering")]
        public GridViewDisplays DataDisplay { get; set; } = new GridViewDisplays();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Metadata Display Settings")]
        [Description("Settings related to the display of the metadata window")]
        public MetadataDisplaySettings MetadataDisplay { get; set; } = new MetadataDisplaySettings();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Streaming Settings")]
        [Description("Settings related to PVS, VLC and streaming content in general.")]
        public PlayerSettings Player { get; set; } = new PlayerSettings();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Generic Application Settings")]
        [Description("Misc. settings relating to normal functioning")]
        public GenericAppSettings Generic { get; set; } = new GenericAppSettings();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Caching Settings")]
        [Description("Settings related to how PlexDL deals with cached data")]
        public CacheSettings CacheSettings { get; set; } = new CacheSettings();
    }
}