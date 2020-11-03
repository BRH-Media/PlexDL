using System;
using System.Windows.Forms;

namespace PlexDL.Common.Components.Controls
{
    public class NoAutoSelectDataGridView : DataGridView
    {
        public bool SuppressAutoSelection { get; set; }

        public new /*shadowing*/ object DataSource
        {
            get => base.DataSource;
            set
            {
                SuppressAutoSelection = true;
                var parent = FindForm();

                // Either the selection gets cleared on form load....
                parent.Load -= Parent_Load;
                parent.Load += Parent_Load;

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
            if (SelectedRows.Count > 0 || SelectedCells.Count > 0)
            {
                ClearSelection();
                SuppressAutoSelection = false;
            }
        }

        private void Parent_Load(object sender, EventArgs e)
        {
            ClearSelectionAndResetSuppression();
        }
    }
}