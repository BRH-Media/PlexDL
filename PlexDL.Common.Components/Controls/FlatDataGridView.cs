using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Common.Components.Controls
{
    public sealed class FlatDataGridView : DataGridView
    {
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
            Font = new Font(FontFamily.GenericSansSerif, (float)8.25);

            //base event handlers
            Paint += DgvPaint;
            DataError += DgvDataError;

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
                    Font = new Font(FontFamily.GenericSansSerif, (float)8.25);
            }
            catch
            {
                //force an immediate repaint
                Invalidate();
            }
        }

        private static void DgvDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //cancel any data error events; the user does not need to be bothered by them
            e.Cancel = true;
        }
    }
}