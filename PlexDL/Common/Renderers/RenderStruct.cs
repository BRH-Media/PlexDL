using System.Collections.Generic;
using System.Data;

namespace PlexDL.Common.Renderers
{
    public class RenderStruct
    {
        public List<string> WantedColumns { get; set; } = new List<string>();
        public List<string> WantedCaption { get; set; } = new List<string>();
        public DataTable Data { get; set; } = new DataTable();
    }
}