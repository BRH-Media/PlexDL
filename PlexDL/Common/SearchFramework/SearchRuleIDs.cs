namespace PlexDL.Common.SearchFramework
{
    internal class SearchRuleIDs
    {
        public const int CONTAINS_KEY = 0;
        public const int EQUALS_KEY = 1;
        public const int BEGINS_WITH = 2;
        public const int ENDS_WITH = 3;

        public static string SqlSearchFromRule(string column, string key, int rule)
        {
            var sql = "";
            if (rule <= 3 && rule >= 0)
                switch (rule)
                {
                    case CONTAINS_KEY:
                        sql = column + " LIKE '%" + key + "%'";
                        break;

                    case EQUALS_KEY:
                        sql = column + " = '" + key + "'";
                        break;

                    case BEGINS_WITH:
                        sql = column + " LIKE '" + key + "%'";
                        break;

                    case ENDS_WITH:
                        sql = column + " LIKE '%" + key + "'";
                        break;
                }

            return sql;
        }
    }
}