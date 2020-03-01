using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexDL.Common.Structures
{
    class SearchRuleIDs
    {
        public const int ContainsKey = 0;
        public const int EqualsKey = 1;
        public const int BeginsWith = 2;
        public const int EndsWith = 3;

        public static string SQLSearchFromRule(string column, string key, int rule)
        {
            string sql = "";
            if (rule <= 3  && rule >= 0)
                switch (rule)
                {
                    case ContainsKey:
                        sql = column + " LIKE '%" + key + "%'";
                        break;
                    case EqualsKey:
                        sql = column + " = '" + key + "'";
                        break;
                    case BeginsWith:
                        sql = column + " LIKE '" + key + "%'";
                        break;
                    case EndsWith:
                        sql = column + " LIKE '%" + key + "'";
                        break;
                }
            return sql;
        }
    }
}
