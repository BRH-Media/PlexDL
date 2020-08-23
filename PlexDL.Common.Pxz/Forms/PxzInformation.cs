using PlexDL.Common.Pxz.Structures;
using System;
using System.Data;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.Common.Pxz.Forms
{
    public partial class PxzInformation : Form
    {
        public PxzFile Pxz { get; set; }

        public PxzInformation()
        {
            InitializeComponent();
        }

        public DataTable Records()
        {
            var table = new DataTable();
            table.Columns.Add(@"Record Name", typeof(string));
            table.Columns.Add(@"Stored Name", typeof(string));
            table.Columns.Add(@"Type", typeof(string));
            table.Columns.Add(@"Protected", typeof(string));
            table.Columns.Add(@"Sizing", typeof(string));
            table.Columns.Add(@"C. Ratio", typeof(string));
            table.Columns.Add(@"CS Valid", typeof(string));

            try
            {
                foreach (var r in Pxz.Records)
                {
                    var recordName = r.Header.Naming.RecordName;
                    var storedName = r.Header.Naming.StoredName;
                    var recordType = r.Header.DataType.ToString();
                    var recordProt = r.ProtectedRecord.ToString();
                    var recordSize = $"{Utilities.FormatBytes((long)r.Header.Size.RawSize)}/{Utilities.FormatBytes((long)r.Header.Size.DecSize)}";
                    var comprRatio = $"{r.Header.Size.Ratio}%";
                    var checkValid = r.ChecksumValid.ToString();

                    object[] row =
                    {
                        recordName,
                        storedName,
                        recordType,
                        recordProt,
                        recordSize,
                        comprRatio,
                        checkValid
                    };

                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }

            return table;
        }

        public DataTable Attributes()
        {
            var table = new DataTable();
            table.Columns.Add(@"Attribute", typeof(string));
            table.Columns.Add(@"Value", typeof(string));

            try
            {
                object[] authorName = { @"User", Pxz.FileIndex.Author.UserAccount };
                object[] authorDisplay = { @"Display Name", Pxz.FileIndex.Author.DisplayName };
                object[] authorMachine = { @"PC Name", Pxz.FileIndex.Author.MachineName };
                object[] formatVersion = { @"Version", Pxz.FileIndex.FormatVersion.ToString() };
                object[] buildState = { @"Release State", Pxz.FileIndex.BuildState.ToString() };
                object[] recordCount = { @"# Records", Pxz.FileIndex.RecordReference.Count.ToString() };

                table.Rows.Add(authorName);
                table.Rows.Add(authorDisplay);
                table.Rows.Add(authorMachine);
                table.Rows.Add(formatVersion);
                table.Rows.Add(buildState);
                table.Rows.Add(recordCount);
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }

            return table;
        }

        private void PxzInformation_Load(object sender, EventArgs e)
        {
            try
            {
                if (Pxz != null)
                {
                    dgvAttributes.DataSource = Attributes();
                    dgvRecords.DataSource = Records();
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString(), @"Load Error");
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void ShowPxzInformation(PxzFile file)
        {
            var frm = new PxzInformation { Pxz = file };
            frm.ShowDialog();
        }
    }
}