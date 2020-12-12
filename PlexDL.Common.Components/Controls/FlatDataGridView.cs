using PlexDL.Common.Components.Styling;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable UnusedVariable
// ReSharper disable UnusedMember.Local
// ReSharper disable InvertIf

namespace PlexDL.Common.Components.Controls
{
    public sealed class FlatDataGridView : DataGridView
    {
        /// <summary>
        /// Default 8.25pt, regular font
        /// </summary>
        private Font DefaultCellFont { get; } = new Font(FontFamily.GenericSansSerif, (float)8.25, FontStyle.Regular);

        public FlatDataGridView()
        {
            //user modification permissions
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            AllowUserToAddRows = false;
            AllowUserToResizeColumns = true;
            AllowUserToOrderColumns = true;

            //display flags
            MultiSelect = false;
            RowHeadersVisible = false;

            //visual settings
            BackgroundColor = Color.FromArgb(224, 224, 224);
            Font = DefaultCellFont;
            DefaultCellStyle.Font = DefaultCellFont;

            //base event handlers
            Paint += DgvPaint;
            DataError += DgvDataError;
            ColumnHeaderMouseClick += DgvColumnHeaderClicked;

            //border styling
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;

            //generic grid settings
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        [Category("PlexDL")]
        [Description("The text to display if the RowCount is 0")]
        public string RowsEmptyText { get; set; } = "No Data Found";

        [Category("PlexDL")]
        [Description("The ForeColor of RowsEmptyText")]
        public Color RowsEmptyTextForeColor { get; set; } = Color.FromArgb(134, 134, 134);

        [Category("PlexDL")]
        [Description("Tracks, Episodes, Movies, etc.")]
        public bool IsContentTable { get; set; } = false;

        [Category("PlexDL")]
        [Description("Configures bool colouring")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BoolColour BoolColouringScheme { get; set; } = new BoolColour();

        public new object DataSource
        {
            get => base.DataSource;
            set
            {
                //new value is applied
                base.DataSource = value;

                //grid is repainted with boolean colouring
                DgvRepaintBoolColouring();
            }
        }

        /// <summary>
        /// This is the function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvPaint(object sender, PaintEventArgs e)
        {
            //middle of the control
            const TextFormatFlags middleCenter = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            try
            {
                //does the grid have any data yet?
                if (DataSource == null || Rows.Count == 0)
                {
                    //the font size is increased to enhance readability
                    Font = new Font(FontFamily.GenericSansSerif, 13);

                    //the border must be removed for the no data message
                    BorderStyle = BorderStyle.None;

                    //renders RowsEmptyText in the middle of the control
                    TextRenderer.DrawText(e.Graphics,
                        RowsEmptyText,
                        Font,
                        ClientRectangle,
                        RowsEmptyTextForeColor,
                        BackgroundColor,
                        middleCenter);
                }
                else
                    //default font for when data is displayed;
                    //this must be set to avoid the default large font size
                    Font = DefaultCellFont;
            }
            catch
            {
                //force an immediate repaint
                Invalidate();
            }
        }

        /// <summary>
        /// Handles repainting bool colouring on reorder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvColumnHeaderClicked(object sender, EventArgs e)
            => DgvRepaintBoolColouring();

        /// <summary>
        /// Cancels any data errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DgvDataError(object sender, DataGridViewDataErrorEventArgs e)
            => e.Cancel = true;

        /// <summary>
        /// Attempts to find the column name of a provided cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string ColumnNameFromCell(DataGridViewCell cell)
        {
            try
            {
                //null validation
                if (cell != null)
                {
                    //index of the column
                    var columnIndex = cell.ColumnIndex;

                    //name of the column
                    if (ColumnCount >= columnIndex + 1)
                        return Columns[columnIndex].Name;
                }
            }
            catch
            {
                //nothing
            }

            //default
            return @"";
        }

        /// <summary>
        /// Initiates a cell boolean repaint based on the current settings
        /// </summary>
        /// <param name="c"></param>
        private void DgvRepaintCellBoolColouring(DataGridViewCell c)
        {
            //null validation
            if (c != null)
            {
                //convert cell value
                var cellString = c.Value.ToString().ToLower();

                //decide correct colour
                var colour = Color.Black;

                //switch for the correct colour
                switch (cellString)
                {
                    case @"true":
                        colour = BoolColouringScheme.TrueColour;
                        break;

                    case @"false":
                        colour = BoolColouringScheme.FalseColour;
                        break;
                }

                //switch for correct operation
                switch (BoolColouringScheme.ColouringMode)
                {
                    case BoolColourMode.BackColour:

                        //apply new font for bold
                        c.Style.Font = new Font(FontFamily.GenericSansSerif, (float)8.25, FontStyle.Bold);

                        //apply bool colour
                        c.Style.ForeColor = Color.White;
                        c.Style.SelectionForeColor = Color.White;

                        //apply bool colour
                        c.Style.BackColor = colour;
                        c.Style.SelectionBackColor = colour;

                        break;

                    case BoolColourMode.ForeColour:

                        //apply new font for bold
                        c.Style.Font = new Font(FontFamily.GenericSansSerif, (float)8.25, FontStyle.Bold);

                        //apply bool colour
                        c.Style.ForeColor = colour;
                        c.Style.SelectionForeColor = Color.White;

                        //apply back colour
                        c.Style.BackColor = SystemColors.Window;
                        c.Style.SelectionBackColor = SystemColors.Highlight;

                        break;
                }
            }
        }

        /// <summary>
        /// Initiates a full boolean repaint
        /// </summary>
        private void DgvRepaintBoolColouring()
        {
            try
            {
                //check if colouring is even enabled
                if (BoolColouringScheme.BoolColouringEnabled)

                    //validation
                    if (Rows.Count > 0 && ColumnCount > 0)
                    {
                        //loop through each row
                        foreach (DataGridViewRow r in Rows)
                        {
                            //loop through each cell in this row
                            foreach (DataGridViewCell c in r.Cells)
                            {
                                //column name of the current cell
                                var columnName = ColumnNameFromCell(c);

                                //null validation
                                if (!string.IsNullOrWhiteSpace(columnName))

                                    //check if the current column should be coloured
                                    if (BoolColouringScheme.RelevantColumns.Contains(columnName))

                                        //check cell value against bool colouring values
                                        DgvRepaintCellBoolColouring(c);
                            }
                        }
                    }
            }
            catch
            {
                //force an immediate repaint
                Invalidate();
            }
        }
    }
}