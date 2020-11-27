using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.SearchFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI.Forms
{
    public partial class SearchForm : DoubleBufferedForm
    {
        public SearchOptions SearchContext = new SearchOptions();

        public SearchForm()
        {
            InitializeComponent();
            cbxSearchRule.SelectedIndex = 0;
        }

        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchTerm.Text) && cbxSearchColumn.SelectedItem != null &&
                    cbxSearchRule.SelectedIndex >= 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    UIMessages.Error(@"Please enter all required values or exit the search",
                        @"Validation Error");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }
        }

        private void PopulateAllColumns()
        {
            foreach (DataColumn column in SearchContext.SearchCollection.Columns)
                cbxSearchColumn.Items.Add(column.ColumnName);
        }

        private void PopulateFilteredColumns()
        {
            foreach (DataColumn column in SearchContext.SearchCollection.Columns)
                if (SearchContext.ColumnCollection.Contains(column.ColumnName))
                    cbxSearchColumn.Items.Add(column.ColumnName);
        }

        private void PopulateColumns()
        {
            cbxSearchColumn.Items.Clear();

            if (SearchContext.ColumnCollection != null)
            {
                if (SearchContext.ColumnCollection.Count > 0)
                    PopulateFilteredColumns();
                else
                    PopulateAllColumns();
            }
            else
            {
                PopulateAllColumns();
            }

            ColumnPriorityMatch();
        }

        private void ColumnPriorityMatch()
        {
            if (cbxSearchColumn.Items.Count <= 0) return;
            var matched = false;

            foreach (var c in ObjectProvider.Settings.Generic.SearchColumnPriority)
                if (cbxSearchColumn.Items.Contains(c))
                {
                    cbxSearchColumn.SelectedItem = c;
                    matched = true;
                    break;
                }

            if (!matched)
                cbxSearchColumn.SelectedIndex = 0;
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            PopulateColumns();
        }

        public static SearchResult ShowSearch(SearchOptions settings, List<string> wantedColumns)
        {
            var frm = new SearchForm
            {
                SearchContext = settings
            };

            var result = new SearchResult();

            if (frm.ShowDialog() != DialogResult.OK) return result;

            result.SearchColumn = frm.cbxSearchColumn.SelectedItem.ToString();
            result.SearchTerm = frm.txtSearchTerm.Text;

            switch (frm.cbxSearchRule.SelectedIndex)
            {
                case 0:
                    result.SearchRule = SearchRule.ContainsKey;
                    break;

                case 1:
                    result.SearchRule = SearchRule.EqualsKey;
                    break;

                case 2:
                    result.SearchRule = SearchRule.BeginsWith;
                    break;

                case 3:
                    result.SearchRule = SearchRule.EndsWith;
                    break;
            }

            return result;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}