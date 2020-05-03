using PlexDL.Common.Logging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class FlatDataGridView : DataGridView
    {
        public FlatDataGridView()
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = Color.FromArgb(224, 224, 224);
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            Font = new Font(FontFamily.GenericSansSerif, (float)8.25);
            MultiSelect = false;
            Name = "dgvMain";
            Paint += DgvPaint;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            DataError += DGVDataError;
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        [Category("PlexDL")]
        [Description("The text to display if the RowCount is 0")]
        public string RowsEmptyText { get; set; } = "No Data Found";

        [Category("PlexDL")]
        [Description("The ForeColor of RowsEmptyText")]
        public Color RowsEmptyTextForeColor { get; set; } = Color.FromArgb(134, 134, 134);

        public sealed override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        private void DgvPaint(object sender, PaintEventArgs e)
        {
            try
            {
                if (Rows.Count == 0)
                {
                    TextRenderer.DrawText(e.Graphics, RowsEmptyText,
                        Font, ClientRectangle,
                        RowsEmptyTextForeColor, BackgroundColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    Font = new Font(FontFamily.GenericSansSerif, 13);
                    BorderStyle = BorderStyle.None;
                }
                else
                {
                    Font = new Font(FontFamily.GenericSansSerif, (float)8.25);
                }
            }
            catch (Exception ex)
            {
                Invalidate();
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