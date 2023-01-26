using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexDL.Common.Shodan.Globals
{
    public static class ObjectProvider
    {
        public static DataTable PreviousResults { get; set; } = null;
    }
}
