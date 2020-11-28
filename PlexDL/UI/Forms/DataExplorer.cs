using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using System.Drawing;
using System.Xml;

#pragma warning disable 1591

// ReSharper disable InconsistentNaming

namespace PlexDL.UI.Forms
{
    public partial class DataExplorer : DoubleBufferedForm
    {
        public PlexObject PlexData { get; set; }
        public DataSet RawData { get; set; }

        public DataExplorer()
        {
            InitializeComponent();
        }

        public DataSet FromPlexRaw(PlexObject data)
        {
            try
            {
                var doc = data.RawMetadata;

                var sections = new DataSet();
                sections.ReadXml(new XmlNodeReader(doc));

                return sections;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ApiExplorerPxzError");
                return null;
            }
        }

        private void PopulateList(DataSet data)
        {
            lstTables.Items.Clear();

            foreach (DataTable t in data.Tables)
                lstTables.Items.Add(t.TableName);

            //if the count's above 0, select the first index,
            //thereby loading the first table.
            if (lstTables.Items.Count > 0)
                lstTables.SelectedIndex = 0;
        }

        private void ApiExplorer_Load(object sender, EventArgs e)
        {
            if (PlexData != null)
            {
                var data = FromPlexRaw(PlexData);

                RawData = data;

                PopulateList(data);
            }
        }

        private void LstTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTables.SelectedIndex > -1 && RawData != null)
            {
                var tableName = lstTables.SelectedItem.ToString();

                if (RawData.Tables.Contains(tableName))
                {
                    var t = RawData.Tables[tableName];
                    var c = t.Rows.Count;

                    dgvMain.DataSource = t;

                    SetupGUI(c, tableName);
                }
            }
            else
                ResetGUI();
        }

        private void SetupGUI(int rowCount, string tableName)
        {
            lblTableValue.Text = tableName;
            lblTableValue.ForeColor = Color.Black;
            lblViewingValue.Text = $@"{rowCount}/{rowCount}";
        }

        private void ResetGUI()
        {
            lblTableValue.Text = @"Not Loaded";
            lblTableValue.ForeColor = Color.DarkRed;
            lblViewingValue.Text = @"0/0";
        }

        public static void ShowExplorer(PlexObject content)
        {
            var frm = new DataExplorer
            {
                PlexData = content
            };

            frm.ShowDialog();
        }

        private void ItmRawXml_Click(object sender, EventArgs e)
        {
            XmlExplorer.ShowXmlExplorer(PlexData.RawMetadata);
        }
    }
}