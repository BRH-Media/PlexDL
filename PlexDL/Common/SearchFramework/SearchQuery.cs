using PlexDL.Common.Enums;

namespace PlexDL.Common.SearchFramework
{
    internal static class SearchQuery
    {
        public static string SqlSearchFromRule(string column, string key, SearchRule rule)
        {
            var sql = "";
            switch (rule)
            {
                case SearchRule.ContainsKey:
                    sql = column + " LIKE '%" + key + "%'";
                    break;

                case SearchRule.EqualsKey:
                    sql = column + " = '" + key + "'";
                    break;

                case SearchRule.BeginsWith:
                    sql = column + " LIKE '" + key + "%'";
                    break;

                case SearchRule.EndsWith:
                    sql = column + " LIKE '%" + key + "'";
                    break;
            }

            return sql;
        }
    }
}