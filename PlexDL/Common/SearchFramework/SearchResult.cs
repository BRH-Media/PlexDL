using PlexDL.Common.Enums;

namespace PlexDL.Common.SearchFramework
{
    public class SearchResult
    {
        /// <summary>
        ///     The column to search in. This will be a column that exists in SearchCollection.
        /// </summary>
        public string SearchColumn { get; set; } = "";

        /// <summary>
        ///     The term of the search. E.g. "kittens" to search for the latter.
        /// </summary>
        public string SearchTerm { get; set; } = "";

        /// <summary>
        ///     The RuleID of the search method. Please check SearchRuleIDs for more information.
        /// </summary>
        public SearchRule SearchRule { get; set; } = SearchRule.ContainsKey;
    }
}