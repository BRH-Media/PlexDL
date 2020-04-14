using PlexDL.Common.SearchFramework.Enums;

namespace PlexDL.Common.SearchFramework
{
    internal static class SearchQuery
    {
        public static string SqlSearchFromRule(string column, string key, SearchRule rule)
        {
            var sql = "";
            switch (rule)
            {
                case SearchRule.CONTAINS_KEY:
                    sql = column + " LIKE '%" + key + "%'";
                    break;

                case SearchRule.EQUALS_KEY:
                    sql = column + " = '" + key + "'";
                    break;

                case SearchRule.BEGINS_WITH:
                    sql = column + " LIKE '" + key + "%'";
                    break;

                case SearchRule.ENDS_WITH:
                    sql = column + " LIKE '%" + key + "'";
                    break;
            }

            return sql;
        }
    }
}