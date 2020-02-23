using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class FlatDataGridView : DataGridView
    {
        public string RowsEmptyText { get; set; } = "No Data Found";

        public FlatDataGridView()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)8.25);
            this.MultiSelect = false;
            this.Name = "dgvMain";
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RowHeadersVisible = false;
            this.Paint += new PaintEventHandler(DGVPaint);
            this.DataError += new DataGridViewDataErrorEventHandler(this.DGVDataError);
        }

        private void DGVPaint(object sender, PaintEventArgs e)
        {
            if (this.Rows.Count == 0)
            {
                TextRenderer.DrawText(e.Graphics, RowsEmptyText,
                    this.Font, this.ClientRectangle,
                    this.ForeColor, this.BackgroundColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                this.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)13);
                this.BorderStyle = BorderStyle.None;
            }
            else
            {
                this.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, (float)8.25);
            }
        }

        private void DGVDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}