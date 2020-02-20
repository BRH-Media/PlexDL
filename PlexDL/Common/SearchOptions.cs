namespace PlexDL.Common.Structures
{
    public class SearchOptions
    {
        public string SearchColumn { get; set; } = "";
        public string SearchTerm { get; set; } = "";
        public bool SearchDirect { get; set; } = false;
        public System.Data.DataTable SearchCollection { get; set; } = new System.Data.DataTable();
    }
}