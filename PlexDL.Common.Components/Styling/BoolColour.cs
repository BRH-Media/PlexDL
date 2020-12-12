using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PlexDL.Common.Components.Styling
{
    /// <summary>
    /// Represents a boolean cell colouring scheme
    /// </summary>
    public class BoolColour
    {
        /// <summary>
        /// Cell colour on 'False' value
        /// </summary>
        [Description("Cell colour on 'False' value")]
        public Color FalseColour { get; set; } = Color.DarkRed;

        /// <summary>
        /// Cell colour on 'True' value
        /// </summary>
        [Description("Cell colour on 'True' value")]
        public Color TrueColour { get; set; } = Color.DarkGreen;

        /// <summary>
        /// List of column names to apply bool colouring to
        /// </summary>
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, " +
                "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
            typeof(System.Drawing.Design.UITypeEditor))]
        [Description("List of column names to apply bool colouring to")]
        public List<string> RelevantColumns { get; set; } = new List<string>();

        /// <summary>
        /// Enables/disables bool colouring
        /// </summary>
        [Description("Enables/disables bool colouring")]
        public bool BoolColouringEnabled { get; set; } = false;

        /// <summary>
        /// Specifies how the cell will be coloured
        /// </summary>
        [Description("Specifies how the cell will be coloured")]
        public BoolColourMode ColouringMode { get; set; } = BoolColourMode.BackColour;

        /// <summary>
        /// Purely a null override; always reports an empty string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return @"";
        }
    }
}