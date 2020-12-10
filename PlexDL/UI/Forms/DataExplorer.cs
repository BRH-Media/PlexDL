using LogDel.Utilities.Export;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using UIHelpers;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable InconsistentNaming

#pragma warning disable 1591

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

        private void SetupGUI(int rowCount, string tableName)
        {
            lblTableValue.Text = tableName;
            lblTableValue.ForeColor = Color.Black;
            lblViewingValue.Text = $@"{rowCount}/{rowCount}";

            //enable exporting
            itmExport.Enabled = true;
        }

        private void ResetGUI()
        {
            lblTableValue.Text = @"Not Loaded";
            lblTableValue.ForeColor = Color.DarkRed;
            lblViewingValue.Text = @"0/0";

            //disable exporting
            itmExport.Enabled = false;
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

        private void Startup()
        {
            if (PlexData != null)
            {
                var data = FromPlexRaw(PlexData);

                RawData = data;

                PopulateList(data);
            }
        }

        private void DataUpdate()
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

        private void DataExport(ExportFormat format)
        {
            try
            {
                //validation
                if (dgvMain.Rows.Count > 0)
                {
                    //flag for success message
                    var exportSuccess = false;

                    //the data to export
                    var data = (DataTable)dgvMain.DataSource;

                    //use export framework
                    switch (format)
                    {
                        //XML exporter
                        case ExportFormat.Xml:

                            //setup the dialog
                            sfdExport.Filter = @"XML File|*.xml";
                            sfdExport.DefaultExt = @"xml";

                            //show the dialog
                            if (sfdExport.ShowDialog() == DialogResult.OK)
                            {
                                //do the export
                                data.ToXml(sfdExport.FileName);

                                //set success
                                exportSuccess = true;
                            }

                            break;

                        //CSV exporter
                        case ExportFormat.Csv:

                            //setup the dialog
                            sfdExport.Filter = @"CSV File|*.csv";
                            sfdExport.DefaultExt = @"csv";

                            //show the dialog
                            if (sfdExport.ShowDialog() == DialogResult.OK)
                            {
                                //do the export
                                data.ToCsv(sfdExport.FileName);

                                //set success
                                exportSuccess = true;
                            }

                            break;

                        //JSON exporter
                        case ExportFormat.Json:

                            //setup the dialog
                            sfdExport.Filter = @"JSON File|*.json";
                            sfdExport.DefaultExt = @"json";

                            //show the dialog
                            if (sfdExport.ShowDialog() == DialogResult.OK)
                            {
                                //do the export
                                data.ToJson(sfdExport.FileName);

                                //set success
                                exportSuccess = true;
                            }

                            break;

                        //LOGDEL exporter
                        case ExportFormat.Logdel:

                            //setup the dialog
                            sfdExport.Filter = @"LOGDEL File|*.logdel";
                            sfdExport.DefaultExt = @"logdel";

                            //show the dialog
                            if (sfdExport.ShowDialog() == DialogResult.OK)
                            {
                                //do the export
                                data.ToLogdel(sfdExport.FileName);

                                //set success
                                exportSuccess = true;
                            }

                            break;
                    }

                    //show only on success
                    if (exportSuccess)
                        UIMessages.Info(@"Export was successful");
                }
                else
                    UIMessages.Error(@"Export failed; no data available.");
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"ApiExplorerExportError");

                //alert the user
                UIMessages.Error($"Export error:\n\n{ex}");
            }
        }

        private void ApiExplorer_Load(object sender, EventArgs e)
            => Startup();

        private void LstTables_SelectedIndexChanged(object sender, EventArgs e)
            => DataUpdate();

        private void ItmExportXml_Click(object sender, EventArgs e)
            => DataExport(ExportFormat.Xml);

        private void ItmExportJson_Click(object sender, EventArgs e)
            => DataExport(ExportFormat.Json);

        private void ItmExportCsv_Click(object sender, EventArgs e)
            => DataExport(ExportFormat.Csv);

        private void ItmExportLogdel_Click(object sender, EventArgs e)
            => DataExport(ExportFormat.Logdel);

        public static void ShowExplorer(PlexObject content)
            => new DataExplorer { PlexData = content }.ShowDialog();

        private void ItmRawXml_Click(object sender, EventArgs e)
            => XmlExplorer.ShowXmlExplorer(PlexData.RawMetadata);
    }
}