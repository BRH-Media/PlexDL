using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids
{
    public class EpisodesDisplay
    {
        public List<string> EpisodesDisplayColumns { get; set; } = new List<string>() { "title", "year", "contentRating" };
        public List<string> EpisodesDisplayCaption { get; set; } = new List<string>() { "Title", "Year", "Rating" };
    }
}