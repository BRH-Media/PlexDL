using LogDel.IO;
using PlexDL.Common.API.PlexAPI.IO;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using UIHelpers;

#pragma warning disable 1591

namespace PlexDL.UI.Forms
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
            => ShowSelector();

        private void BtnLoadDict_Click(object sender, EventArgs e)
        {
            //ensure LogDel file TextBox is filled and the OpenFileDialog has a LogDel file selected
            if (!string.IsNullOrWhiteSpace(txtLogdel.Text) && !string.IsNullOrWhiteSpace(ofdLogdel.FileName))

                //both are valid, load the statistics and process tokens
                LoadStats(ofdLogdel.FileName);
            else

                //alert the user
                UIMessages.Error(@"Incorrect value(s)");
        }

        private void ShowSelector(bool textSet = true, bool resetStats = true)
        {
            //show the OpenFileDialog for a LogDel token file
            if (ofdLogdel.ShowDialog() != DialogResult.OK)

                //user didn't click OK; exit
                return;

            //do we fill the TextBox with the FileName selected?
            if (textSet)

                //yes, fill it
                txtLogdel.Text = ofdLogdel.FileName;

            //do we clear the previous statistics and reset them to defaults?
            if (resetStats)

                //yes, reset them
                ResetStats();

            //disable translate button; new file was selected
            btnTranslate.Enabled = false;
        }

        private void ResetStats()
        {
            //GUI
            lblValidValue.Text = @"0";
            lblInvalidValue.Text = @"0";
            lblDuplicatesValue.Text = @"0";
            lblTotalValue.Text = @"0";

            //globals
            DuplicateCount = 0;
            InvalidCount = 0;
            TotalCount = 0;
            ValidCount = 0;
        }

        private void LoadStats(string fileName)
        {
            try
            {
                //ensure logdel file exists
                if (File.Exists(fileName))
                {
                    //load it to a DataTable
                    var logLoad = LogReader.TableFromFile(fileName, false);

                    //null validation
                    if (logLoad != null)
                    {
                        //ensure it contains a token column
                        if (logLoad.Columns.Contains("Token"))
                        {
                            //reset token lists
                            AllTokens = new List<string>();
                            ValidTokens = new List<string>();

                            //reset token counters
                            TotalCount = logLoad.Rows.Count;
                            InvalidCount = 0;
                            ValidCount = 0;
                            DuplicateCount = 0;

                            //each row in the log file
                            foreach (DataRow r in logLoad.Rows)
                            {
                                //skip over a blank token
                                if (r["Token"] == null)
                                    continue;

                                //the token is converted to a string
                                var t = r["Token"].ToString();

                                //if we haven't already processed this token
                                if (!AllTokens.Contains(t))
                                {
                                    //add it to the global list of current tokens
                                    AllTokens.Add(t);

                                    //is the token of the correct Plex length and is it a valid string?
                                    if (t.Length == 20 && !string.IsNullOrWhiteSpace(t))
                                    {
                                        //increment valid tokens
                                        ValidCount++;

                                        //add it to the list of valid tokens
                                        ValidTokens.Add(t);
                                    }
                                    else

                                        //token was invalid, increment invalid tokens
                                        InvalidCount++;
                                }
                                else

                                    //token was already processed, increment duplicate tokens
                                    DuplicateCount++;
                            }

                            //setup GUI
                            lblDuplicatesValue.Text = DuplicateCount.ToString();
                            lblInvalidValue.Text = InvalidCount.ToString();
                            lblTotalValue.Text = TotalCount.ToString();
                            lblValidValue.Text = ValidCount.ToString();

                            //inform user
                            UIMessages.Info(@"Load succeeded");

                            //enable the translator button
                            btnTranslate.Enabled = true;
                        }
                        else

                            //alert the user of the error
                            UIMessages.Error(@"Invalid table layout");
                    }
                    else

                        //alert the user of the error
                        UIMessages.Error(@"Null table data");
                }
                else

                    //alert the user of the error
                    UIMessages.Error(@"File doesn't exist");
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"TokenTranslatorError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private static ApplicationOptions ProfileFromToken(string token)

            //create a new configuration object from the specified token
            => new ApplicationOptions
            {
                ConnectionInfo =
                {
                    PlexAccountToken = token
                }
            };

        private static void ExportTokenProf(string token, string dir = @"translator\profs")
        {
            try
            {
                //does the directory for stored profiles exist?
                if (!Directory.Exists(dir))

                    //no, create it
                    Directory.CreateDirectory(dir);

                //how many tokens have we already exported to profiles?
                var fileCount = Directory.GetFiles(dir, "plexdl_sv*.prof").Length;

                //the index of the new profile
                var fileIdx = fileCount + 1;

                //file name for the new profile
                var fileName = $"plexdl_sv{fileIdx}.prof";

                //full path for the profile to be exported
                var fqPath = $@"{dir}\{fileName}";

                //create a new configuration object from the token provided
                var options = ProfileFromToken(token);

                //export the configuration object to a file
                ProfileIO.ProfileToFile(fqPath, options);
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"TokenTranslatorExportError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void BtnTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                //are both LogDel file name storage locations valid?
                if (!string.IsNullOrWhiteSpace(txtLogdel.Text) && !string.IsNullOrWhiteSpace(ofdLogdel.FileName))
                {
                    //reset progress bar
                    pbMain.Maximum = TotalCount;
                    pbMain.Value = 0;

                    //setup background worker event handlers
                    bwTranslate.RunWorkerCompleted += BwTranslate_RunWorkerCompleted;
                    bwTranslate.ProgressChanged += BwTranslate_ProgressChanged;

                    //start processing in the background
                    bwTranslate.RunWorkerAsync();
                }
                else

                    //alert the user
                    UIMessages.Error(@"Incorrect value(s)");
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"TokenTranslatorProcessingError");

                //alert the user
                UIMessages.Error(ex.ToString());
            }
        }

        private void BwTranslate_ProgressChanged(object sender, ProgressChangedEventArgs e)
            => pbMain.PerformStep();

        private void BwTranslate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //alert the user to the success
            UIMessages.Info(@"Completed", @"Success");

            //close the form and hence exit the application
            Close();
        }

        private void BwTranslate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //go through every single valid token
                foreach (var s in ValidTokens)
                {
                    //export the valid token to a profile XML file
                    ExportTokenProf(s);

                    //report completed 1 token
                    bwTranslate.ReportProgress(1);
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"TokenTranslatorWorkerError");
            }
        }
    }
}