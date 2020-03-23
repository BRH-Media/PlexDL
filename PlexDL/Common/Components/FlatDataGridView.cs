using PlexDL.Common.Logging;
using System;
using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class FlatDataGridView : DataGridView
    {
        public string RowsEmptyText { get; set; } = "No Data Found";
        public string ErrorText { get; set; } = "Render Error";

        public FlatDataGridView()
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = System.Drawing.Color.FromArgb((int)(byte)224, (int)(byte)224, (int)(byte)224);
            BorderStyle = BorderStyle.None;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)8.25);
            MultiSelect = false;
            Name = "dgvMain";
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            DataError += new DataGridViewDataErrorEventHandler(DGVDataError);
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (Rows != null)
                {
                    if (Rows.Count == 0)
                    {
                        TextRenderer.DrawText(e.Graphics, RowsEmptyText,
                            Font, ClientRectangle,
                            ForeColor, BackgroundColor,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)13);
                        BorderStyle = BorderStyle.None;
                    }
                    else
                    {
                        Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)8.25);
                    }
                }
                else
                {
                    TextRenderer.DrawText(e.Graphics, RowsEmptyText,
                            Font, ClientRectangle,
                            ForeColor, BackgroundColor,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)13);
                    BorderStyle = BorderStyle.None;
                }
                
                base.OnPaint(e);
            }
            catch (Exception ex)
            {
                this.Invalidate();
                LoggingHelpers.RecordException(ex.Message, "DGVPaintError");
            }
        }

        private void DGVDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}