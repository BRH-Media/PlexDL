using System.Collections.Generic;
using System.Data;

namespace PlexDL.Common.SearchFramework
{
    public class SearchOptions
    {
        /// <summary>
        /// The column to search in. This will be a column that exists in SearchCollection.
        /// </summary>
        public string SearchColumn { get; set; } = "";

        /// <summary>
        /// The term of the search. E.g. "kittens" to search for the latter.
        /// </summary>
        public string SearchTerm { get; set; } = "";

        /// <summary>
        /// The RuleID of the search method. Please check SearchRuleIDs for more information.
        /// </summary>
        public Enums.SearchRule SearchRule { get; set; } = Enums.SearchRule.CONTAINS_KEY;

        /// <summary>
        /// Columns to include in the search. We don't want a jumbled mess of heaps of columns being shown to the user!
        /// </summary>
        public List<string> ColumnCollection { get; set; } = new List<string>();

        /// <summary>
        /// The data to search through. This should be a direct replica/reference of the table you would like to search.
        /// </summary>
        public DataTable SearchCollection { get; set; } = new DataTable();
    }
}