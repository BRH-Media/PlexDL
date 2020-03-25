using PlexDL.Common.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class FlatDataGridView : DataGridView
    {
        public string RowsEmptyText { get; set; } = "No Data Found";
        public Color RowsEmptyTextForeColor { get; set; } = Color.FromArgb(134, 134, 134);
        public string ErrorText { get; set; } = "Render Error";

        public FlatDataGridView()
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = Color.FromArgb((int)(byte)224, (int)(byte)224, (int)(byte)224);
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            Font = new Font(FontFamily.GenericSansSerif, (float)8.25);
            MultiSelect = false;
            Name = "dgvMain";
            Paint += new PaintEventHandler(this.DGVPaint);
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            DataError += new DataGridViewDataErrorEventHandler(DGVDataError);
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void DGVPaint(object sender, PaintEventArgs e)
        {
            try
            {
                if (Rows.Count == 0)
                {
                    TextRenderer.DrawText(e.Graphics, RowsEmptyText,
                        Font, ClientRectangle,
                        RowsEmptyTextForeColor, BackgroundColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    Font = new Font(FontFamily.GenericSansSerif, (float)13);
                    BorderStyle = BorderStyle.None;
                }
                else
                {
                    Font = new Font(FontFamily.GenericSansSerif, (float)8.25);
                }
            }
            catch (Exception ex)
            {
                this.Invalidate();
                LoggingHelpers.RecordException(ex.Message, "DGVPaintError");
            }
        }

        private void DGVDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                LoggingHelpers.RecordException(e.Exception.Message, "DGVDataError");
            }
            catch
            {
                //nothing
            }
            e.Cancel = true;
        }
    }
}