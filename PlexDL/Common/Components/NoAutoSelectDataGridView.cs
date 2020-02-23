using System;
using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class NoAutoSelectDataGridView : DataGridView
    {
        public bool SuppressAutoSelection { get; set; } = false;

        public NoAutoSelectDataGridView() : base()
        {
        }

        public new /*shadowing*/ object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                SuppressAutoSelection = true;
                Form parent = this.FindForm();

                // Either the selection gets cleared on form load....
                parent.Load -= parent_Load;
                parent.Load += parent_Load;

                base.DataSource = value;

                // ...or it gets cleared straight after the DataSource is set
                ClearSelectionAndResetSuppression();
            }
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            if (SuppressAutoSelection)
                return;

            base.OnSelectionChanged(e);
        }

        private void ClearSelectionAndResetSuppression()
        {
            if (this.SelectedRows.Count > 0 || this.SelectedCells.Count > 0)
            {
                this.ClearSelection();
                SuppressAutoSelection = false;
            }
        }

        private void parent_Load(object sender, EventArgs e)
        {
            ClearSelectionAndResetSuppression();
        }
    }
}