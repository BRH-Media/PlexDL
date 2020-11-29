using System.Collections.Generic;
using System.Data;

namespace PlexDL.Common.Renderers.Forms.GridView
{
    /// <summary>
    /// Data object for the GenericViewRenderer renderer(s)
    /// </summary>
    public class GenericRenderStruct
    {
        /// <summary>
        /// List (of strings) containing whitelisted columns in the Data to be rendered
        /// </summary>
        public List<string> WantedColumns { get; set; } = new List<string>();

        /// <summary>
        /// List (of strings) containing captions to apply to WantedColumns
        /// </summary>
        public List<string> WantedCaption { get; set; } = new List<string>();

        /// <summary>
        /// The data to render
        /// </summary>
        public DataTable Data { get; set; } = new DataTable();
    }
}