using LogDel;
using PlexDL.Common.API.IO;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class Translator : DoubleBufferedForm
    {
        public Translator()
        {
            InitializeComponent();
        }

        private List<string> ValidTokens { get; set; } = new List<string>();
        private List<string> AllTokens { get; set; } = new List<string>();
        private int ValidCount { get; set; }
        private int InvalidCount { get; set; }
        private int TotalCount { get; set; }
        private int DuplicateCount { get; set; }

        private void BtnBrowseLogdel_Click(object sender, EventArgs e)
        {
            ShowSelector();
        }

        private void ShowSelector(bool textSet = true, bool resetStats = true)
        {
            if (ofdLogdel.ShowDialog() != DialogResult.OK) return;

            if (textSet)
                txtLogdel.Text = ofdLogdel.FileName;
            if (resetStats)
                ResetStats();
            btnTranslate.Enabled = false;
        }

        private void Translator_Load(object sender, EventArgs e)
        {
            //ShowSelector();
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
                    var logLoad = LogReader.TableFromFile(fileName, false);
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
                                if (r["Token"] == null) continue;

                                var t = r["Token"].ToString();

                                if (!AllTokens.Contains(t))
                                {
                                    AllTokens.Add(t);
                                    if (t.Length == 20 && !string.IsNullOrWhiteSpace(t))
                                    {
                                        ValidCount++;
                                        ValidTokens.Add(t);
                                    }
                                    else
                                    {
                                        InvalidCount++;
                                    }
                                }
                                else
                                {
                                    DuplicateCount++;
                                }
                            }

                            //gui settings
                            lblDuplicatesValue.Text = DuplicateCount.ToString();
                            lblInvalidValue.Text = InvalidCount.ToString();
                            lblTotalValue.Text = TotalCount.ToString();
                            lblValidValue.Text = ValidCount.ToString();

                            UIMessages.Info(@"Load succeeded");
                            btnTranslate.Enabled = true;
                        }
                        else
                        {
                            UIMessages.Error(@"Invalid table layout");
                        }
                    }
                    else
                    {
                        UIMessages.Error(@"Null table data");
                    }
                }
                else
                {
                    UIMessages.Error(@"File doesn't exist");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }
        }

        private void btnLoadDict_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogdel.Text) && !string.IsNullOrEmpty(ofdLogdel.FileName))
                LoadStats(ofdLogdel.FileName);
            else
                UIMessages.Error(@"Incorrect value(s)");
        }

        private static ApplicationOptions ProfileFromToken(string token)
        {
            var prof = new ApplicationOptions { ConnectionInfo = { PlexAccountToken = token } };
            return prof;
        }

        private static void ExportTokenProf(string token, string dir = @"translator\profs")
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var fileCount = Directory.GetFiles(dir, "plexdl_sv*.prof").Length;
                var fileIdx = fileCount + 1;
                var fileName = "plexdl_sv" + fileIdx + ".prof";
                var fqPath = dir + @"\" + fileName;

                var options = ProfileFromToken(token);

                ProfileIO.ProfileToFile(fqPath, options);
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }
        }

        private void BtnTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLogdel.Text) && !string.IsNullOrEmpty(ofdLogdel.FileName))
                {
                    pbMain.Maximum = TotalCount;
                    pbMain.Value = 0;
                    bwTranslate.RunWorkerCompleted += BwTranslate_RunWorkerCompleted;
                    bwTranslate.ProgressChanged += BwTranslate_ProgressChanged;
                    bwTranslate.RunWorkerAsync();
                }
                else
                {
                    UIMessages.Error(@"Incorrect value(s)");
                }
            }
            catch (Exception ex)
            {
                UIMessages.Error(ex.ToString());
            }
        }

        private void BwTranslate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbMain.PerformStep();
        }

        private void BwTranslate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UIMessages.Info(@"Completed", @"Success");
            Close();
        }

        private void BwTranslate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (var s in ValidTokens)
                {
                    ExportTokenProf(s);
                    bwTranslate.ReportProgress(1);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}