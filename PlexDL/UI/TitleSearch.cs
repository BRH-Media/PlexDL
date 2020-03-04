using MetroSet_UI.Forms;
using PlexDL.Common;
using PlexDL.Common.Structures;
using System;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class SearchForm : MetroSetForm
    {
        public SearchOptions SearchContext = new SearchOptions();
        public bool CanFadeOut = true;
        private Timer t1 = new Timer();

        public SearchForm()
        {
            InitializeComponent();
            this.styleMain = GlobalStaticVars.GlobalStyle;
            this.styleMain.MetroForm = this;
            this.cbxSearchRule.SelectedIndex = 0;
        }

        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchTerm.Text) && cbxSearchColumn.SelectedItem != null && cbxSearchRule.SelectedIndex >= 0)
                {
                    this.DialogResult = DialogResult.OK;
                    CanFadeOut = false;
                    this.Close();
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

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        private void FadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void PopulateColumns()
        {
            cbxSearchColumn.Items.Clear();
            foreach (DataColumn column in SearchContext.SearchCollection.Columns)
            {
                cbxSearchColumn.Items.Add(column.ColumnName);
            }

            if (cbxSearchColumn.Items.Count > 0)
            {
                string first = cbxSearchColumn.Items[0].ToString();
                cbxSearchColumn.SelectedItem = first;
                cbxSearchColumn.Text = first;
            }
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            if (Home.settings.Generic.AnimationSpeed > 0)
            {
                Opacity = 0;      //first the opacity is 0
                t1 = new Timer
                {
                    Interval = Home.settings.Generic.AnimationSpeed  //we'll increase the opacity every 10ms
                };
                t1.Tick += new EventHandler(FadeIn);  //this calls the function that changes opacity
                t1.Start();
            }
            PopulateColumns();
        }

        private void FrmSearch_Closing(object sender, FormClosingEventArgs e)
        {
            if ((Home.settings.Generic.AnimationSpeed > 0) && (CanFadeOut))
            {
                e.Cancel = true;
                t1 = new Timer
                {
                    Interval = Home.settings.Generic.AnimationSpeed
                };
                t1.Tick += new EventHandler(FadeOut);  //this calls the fade out function
                t1.Start();

                if (Opacity == 0)
                {
                    //resume the event - the program can be closed
                    e.Cancel = false;
                }
            }
        }

        public static SearchOptions ShowSearch(SearchOptions settings)
        {
            SearchForm frm = new SearchForm
            {
                SearchContext = settings
            };
            SearchOptions result = new SearchOptions();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                result.SearchColumn = frm.cbxSearchColumn.SelectedItem.ToString();
                result.SearchTerm = frm.txtSearchTerm.Text;
                result.SearchRule = frm.cbxSearchRule.SelectedIndex;
                return result;
            }
            return result;
        }

        private void BtnCancelSearch_Click(object sender, EventArgs e)
        {
            CanFadeOut = true;
            this.Close();
        }
    }
}