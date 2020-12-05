using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.SearchFramework;
using System;
using System.Data;
using System.Windows.Forms;
using UIHelpers;

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    /// <summary>
    /// GUI component of the search framework
    /// </summary>
    public partial class SearchForm : DoubleBufferedForm
    {
        /// <summary>
        ///
        /// </summary>
        public SearchOptions SearchContext = new SearchOptions();

        public SearchForm()
        {
            //Windows Forms Designer setup
            InitializeComponent();

            //first search rule is the default
            cbxSearchRule.SelectedIndex = 0;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //close the form
            Close();
        }

        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchTerm.Text)
                    && cbxSearchColumn.SelectedItem != null
                    && cbxSearchRule.SelectedIndex >= 0)
                {
                    //the result is OK, close the form
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                    //alert the user to the validation error
                    UIMessages.Error(@"Please enter all required values or exit the search",
                        @"Validation Error");
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"SearchFormStartSearchError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            //fill filter columns
            PopulateColumns();

            //select the search term TextBox
            ActiveControl = txtSearchTerm;
        }

        private void PopulateAllColumns()
        {
            //for each column in the search collection, add it to the list of potential
            //columns.
            foreach (DataColumn column in SearchContext.SearchCollection.Columns)

                //add it to the GUI
                cbxSearchColumn.Items.Add(column.ColumnName);
        }

        private void PopulateFilteredColumns()
        {
            //for each column in the search collection, add it to the list of potential
            //filtered columns.
            foreach (DataColumn column in SearchContext.SearchCollection.Columns)

                //is the current column in the list of ones allowed?
                if (SearchContext.ColumnCollection.Contains(column.ColumnName))

                    //yes, add it
                    cbxSearchColumn.Items.Add(column.ColumnName);
        }

        private void PopulateColumns()
        {
            //clear all search columns
            cbxSearchColumn.Items.Clear();

            //ensure the search column collection is valid
            if (SearchContext.ColumnCollection != null)
            {
                if (SearchContext.ColumnCollection.Count > 0)
                    //populate with the filtered columns specified
                    PopulateFilteredColumns();
                else
                    //default to using all columns
                    PopulateAllColumns();
            }
            else
                //default to using all columns
                PopulateAllColumns();

            //reorder the column priority
            ColumnPriorityMatch();
        }

        private void ColumnPriorityMatch()
        {
            //don't column priority match if there are no searchable columns
            if (cbxSearchColumn.Items.Count <= 0)
                return;

            //dictates whether ot not we found a column priority match
            var matched = false;

            //loop through each column in the priority
            foreach (var c in ObjectProvider.Settings.Generic.SearchColumnPriority)
                //does the search columns list already contain it?
                if (cbxSearchColumn.Items.Contains(c))
                {
                    //it does, set is as the first priority
                    cbxSearchColumn.SelectedItem = c;

                    //declare a found match
                    matched = true;

                    //exit the loop
                    break;
                }

            //if no match was found, default to the first column
            if (!matched)
                cbxSearchColumn.SelectedIndex = 0;
        }

        /// <summary>
        /// Shows the search form and returns the parameters entered by the user
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static SearchResult ShowSearch(SearchOptions settings)
        {
            var frm = new SearchForm
            {
                SearchContext = settings
            };

            //Build the search result object
            var result = new SearchResult();

            //The search form must return OK; not Cancel or anything else
            if (frm.ShowDialog() != DialogResult.OK)
                return result;

            //Fill with details from the form
            result.SearchColumn = frm.cbxSearchColumn.SelectedItem.ToString();
            result.SearchTerm = frm.txtSearchTerm.Text;

            //Case the search rule, because the indexing of each rule dictates the pattern
            switch (frm.cbxSearchRule.SelectedIndex)
            {
                //The text contains the term
                case 0:
                    result.SearchRule = SearchRule.ContainsKey;
                    break;

                //The text directly matches the term
                case 1:
                    result.SearchRule = SearchRule.EqualsKey;
                    break;

                //The text begins with the term
                case 2:
                    result.SearchRule = SearchRule.BeginsWith;
                    break;

                //The text ends with the term
                case 3:
                    result.SearchRule = SearchRule.EndsWith;
                    break;
            }

            //return the final result
            return result;
        }
    }
}