using PlexDL.Common.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Display
{
    [Serializable]
    public class ColumnAdapter
    {
        [TypeConverter(typeof(StringListTypeConverter))]
        [DisplayName("Columns")]
        [Description("Columns to render. Must have a matching caption at the same index.")]
        public List<string> DisplayColumns { get; set; } = null;

        [TypeConverter(typeof(StringListTypeConverter))]
        [DisplayName("Captions")]
        [Description("Captions to render. Must have a matching column at the same index.")]
        public List<string> DisplayCaptions { get; set; } = null;

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}