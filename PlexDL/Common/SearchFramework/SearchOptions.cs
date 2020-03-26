using System.Data;

namespace PlexDL.Common.SearchFramework
{
    public class SearchOptions
    {
        public string SearchColumn { get; set; } = "";
        public string SearchTerm { get; set; } = "";
        public int SearchRule { get; set; } = 0;
        public DataTable SearchCollection { get; set; } = new DataTable();
    }
}