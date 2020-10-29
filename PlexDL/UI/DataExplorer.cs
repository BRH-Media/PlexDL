using PlexDL.Common.Components;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using System.Xml;

namespace PlexDL.UI
{
    public partial class DataExplorer : DoubleBufferedForm
    {
        public PlexObject PlexData { get; set; } = null;
        public DataSet RawData { get; set; } = null;

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

                    lblTableValue.Text = tableName;
                    lblViewingValue.Text = $@"{c}/{c}";
                }
            }
            else
            {
                lblTableValue.Text = @"Not Loaded";
                lblViewingValue.Text = @"0/0";
            }
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