using System.ComponentModel;
using PlexDL.Common.Structures.AppOptions.Display.Grids;

namespace PlexDL.Common.Structures.AppOptions.Display
{
    public class GridViewDisplays
    {
        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Movies")]
        [Description("Rendering information. It is advised not to change these values.")]
        public ContentDisplay ContentView { get; set; } = new ContentDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TV Shows")]
        [Description("Rendering information. It is advised not to change these values.")]
        public TvDisplay TVView { get; set; } = new TvDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TV Seasons")]
        [Description("Rendering information. It is advised not to change these values.")]
        public SeriesDisplay SeriesView { get; set; } = new SeriesDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TV Episodes")]
        [Description("Rendering information. It is advised not to change these values.")]
        public EpisodesDisplay EpisodesView { get; set; } = new EpisodesDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Library Sections")]
        [Description("Rendering information. It is advised not to change these values.")]
        public LibraryDisplay LibraryView { get; set; } = new LibraryDisplay();
    }
}