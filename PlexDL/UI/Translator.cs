using PlexDL.Common.API;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Translator : Form
    {
        private List<string> ValidTokens { get; set; } = new List<string>();
        private List<string> AllTokens { get; set; } = new List<string>();
        private int ValidCount { get; set; } = 0;
        private int InvalidCount { get; set; } = 0;
        private int TotalCount { get; set; } = 0;
        private int DuplicateCount { get; set; } = 0;

        public Translator()
        {
            InitializeComponent();
        }

        private void btnBrowseLogdel_Click(object sender, EventArgs e)
        {
            ShowSelector();
        }

        private void ShowSelector(bool textSet = true, bool resetStats = true)
        {
            if (ofdLogdel.ShowDialog() == DialogResult.OK)
            {
                if (textSet)
                    txtLogdel.Text = ofdLogdel.FileName;
                if (resetStats)
                    ResetStats();
                btnTranslate.Enabled = false;
            }
        }

        private void Translator_Load(object sender, EventArgs e)
        {
            ShowSelector();
        }

        private void ResetStats()
        {
            lblValidValue.Text = @"0";
            lblInvalidValue.Text = @"0";
            lblDuplicatesValue.Text = @"0";
            lblTotalValue.Text = @"0";
            DuplicateCount = 0;
            InvalidCount = 0;
            TotalCount = 0;
            ValidCount = 0;
        }

        private void LoadStats(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    DataTable logLoad = LogFileParser.TableFromFile(fileName, false);
                    if (logLoad != null)
                    {
                        if (logLoad.Columns.Contains("Token"))
                        {
                            AllTokens = new List<string>();
                            ValidTokens = new List<string>();
                            TotalCount = logLoad.Rows.Count;
                            InvalidCount = 0;
                            ValidCount = 0;
                            DuplicateCount = 0;
                            foreach (DataRow r in logLoad.Rows)
                            {
                                if (r["Token"] != null)
                                {
                                    string t = r["Token"].ToString();
                                    if (!AllTokens.Contains(t))
                                    {
                                        AllTokens.Add(t);
                                        if (t.Length == 20 && !string.IsNullOrWhiteSpace(t))
                                        {
                                            ValidCount++;
                                            ValidTokens.Add(t);
                                        }
                                        else
                                            InvalidCount++;
                                    }
                                    else
                                        DuplicateCount++;
                                }
                            }

                            //gui settings
                            lblDuplicatesValue.Text = DuplicateCount.ToString();
                            lblInvalidValue.Text = InvalidCount.ToString();
                            lblTotalValue.Text = TotalCount.ToString();
                            lblValidValue.Text = ValidCount.ToString();

                            MessageBox.Show(@"Load succeeded");
                            btnTranslate.Enabled = true;
                        }
                        else
                            MessageBox.Show(@"Invalid table layout!");
                    }
                    else
                        MessageBox.Show(@"Null data!");
                }
                else
                    MessageBox.Show(@"File doesn't exist!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnLoadDict_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogdel.Text) && !string.IsNullOrEmpty(ofdLogdel.FileName))
            {
                LoadStats(ofdLogdel.FileName);
            }
            else
                MessageBox.Show(@"Incorrect value(s)");
        }

        private ApplicationOptions ProfileFromToken(string token)
        {
            var prof = new ApplicationOptions();
            prof.ConnectionInfo.PlexAccountToken = token;
            return prof;
        }

        private bool ExportTokenProf(string token, string dir = @"translator\profs")
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                int fileCount = Directory.GetFiles(dir, "plexdl_sv*.prof").Length;
                int fileIdx = fileCount + 1;
                string fileName = "plexdl_sv" + fileIdx.ToString() + ".prof";
                string fqPath = dir + @"\" + fileName;

                var options = ProfileFromToken(token);

                ProfileImportExport.ProfileToFile(fqPath, options);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLogdel.Text) && !string.IsNullOrEmpty(ofdLogdel.FileName))
                {
                    pbMain.Maximum = TotalCount;
                    pbMain.Value = 0;
                    bwTranslate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bwTranslate_RunWorkerCompleted);
                    bwTranslate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bwTranslate_ProgressChanged);
                    bwTranslate.RunWorkerAsync();
                }
                else
                    MessageBox.Show(@"Incorrect value(s)");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void bwTranslate_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pbMain.PerformStep();
        }

        private void bwTranslate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(@"Completed!");
            Close();
        }

        private void bwTranslate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                foreach (string s in ValidTokens)
                {
                    ExportTokenProf(s);
                    bwTranslate.ReportProgress(1);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}