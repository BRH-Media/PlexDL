using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids
{
    public class SeriesDisplay
    {
        public List<string> SeriesDisplayColumns { get; set; } = new List<string>() { "title", "studio", "year", "contentRating" };
        public List<string> SeriesDisplayCaption { get; set; } = new List<string>() { "Title", "Studio", "Year", "Rating" };
    }
}