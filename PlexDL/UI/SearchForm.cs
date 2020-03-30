using System;
using System.Data;
using System.Windows.Forms;
using PlexDL.Common.SearchFramework;

namespace PlexDL.UI
{
    public partial class SearchForm : Form
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
                if (!string.IsNullOrEmpty(txtSearchTerm.Text) && cbxSearchColumn.SelectedItem != null && cbxSearchRule.SelectedIndex >= 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Please enter required values or exit search", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PopulateColumns()
        {
            cbxSearchColumn.Items.Clear();
            foreach (DataColumn column in SearchContext.SearchCollection.Columns)
                cbxSearchColumn.Items.Add(column.ColumnName);

            if (cbxSearchColumn.Items.Count > 0)
            {
                var first = cbxSearchColumn.Items[0].ToString();
                cbxSearchColumn.SelectedItem = first;
                cbxSearchColumn.Text = first;
            }
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            PopulateColumns();
        }

        public static SearchOptions ShowSearch(SearchOptions settings)
        {
            var frm = new SearchForm
            {
                SearchContext = settings
            };
            var result = new SearchOptions();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                result.SearchColumn = frm.cbxSearchColumn.SelectedItem.ToString();
                result.SearchTerm = frm.txtSearchTerm.Text;
                result.SearchRule = frm.cbxSearchRule.SelectedIndex;
                return result;
            }

            return result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}