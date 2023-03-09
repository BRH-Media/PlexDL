using PlexDL.Common.Enums;
using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Metadata
{
    [Serializable]
    public class MetadataDisplaySettings
    {
        [DisplayName("Container Display Option")]
        [Description("This setting controls what container information is displayed when viewing media attributes in the Metadata window.")]
        public MetadataContainerDisplayOption MetadataContainerDisplay { get; set; } = MetadataContainerDisplayOption.Description;

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}