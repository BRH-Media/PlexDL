using System.Collections.Generic;

namespace PlexDL.Common.Structures.AppOptions.Display.Grids
{
    public class LibraryDisplay
    {
        public List<string> LibraryDisplayColumns { get; set; } = new List<string>() { "title", "type" };
        public List<string> LibraryDisplayCaption { get; set; } = new List<string>() { "Title", "Type" };
    }
}