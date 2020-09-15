using PlexDL.Common.Structures.AppOptions.Display.Grids.Library;
using PlexDL.Common.Structures.AppOptions.Display.Grids.Movies;
using PlexDL.Common.Structures.AppOptions.Display.Grids.Music;
using PlexDL.Common.Structures.AppOptions.Display.Grids.TV;
using System;
using System.ComponentModel;

namespace PlexDL.Common.Structures.AppOptions.Display
{
    [Serializable]
    public class GridViewDisplays
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Movies")]
        [Description("Rendering information. It is advised not to change these values.")]
        public MoviesDisplay MoviesView { get; set; } = new MoviesDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TV Shows")]
        [Description("Rendering information. It is advised not to change these values.")]
        public TvDisplay TvView { get; set; } = new TvDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TV Seasons")]
        [Description("Rendering information. It is advised not to change these values.")]
        public SeriesDisplay SeriesView { get; set; } = new SeriesDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TV Episodes")]
        [Description("Rendering information. It is advised not to change these values.")]
        public EpisodesDisplay EpisodesView { get; set; } = new EpisodesDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Music Artists")]
        [Description("Rendering information. It is advised not to change these values.")]
        public ArtistDisplay ArtistsView { get; set; } = new ArtistDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Music Albums")]
        [Description("Rendering information. It is advised not to change these values.")]
        public AlbumDisplay AlbumsView { get; set; } = new AlbumDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Music Tracks")]
        [Description("Rendering information. It is advised not to change these values.")]
        public TrackDisplay TracksView { get; set; } = new TrackDisplay();

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Library Sections")]
        [Description("Rendering information. It is advised not to change these values.")]
        public LibraryDisplay LibraryView { get; set; } = new LibraryDisplay();

        // to make sure the PropertyGrid doesn't keep showing the name of this class, just return a blank string.
        public override string ToString()
        {
            return "";
        }
    }
}