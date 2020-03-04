using MetroSet_UI.Forms;
using PlexDL.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class LogViewer : MetroSetForm
    {
        public libbrhscgui.Components.UserInteraction objUI = new libbrhscgui.Components.UserInteraction();
        public string dir = AppDomain.CurrentDomain.BaseDirectory + @"\Logs";

        public LogViewer()
        {
            InitializeComponent();
            this.styleMain = GlobalStaticVars.GlobalStyle;
            this.styleMain.MetroForm = this;
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

        public void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.Directory.Exists(dir))
                {
                    if (sfdBackup.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfdBackup.FileName))
                            File.Delete(sfdBackup.FileName);
                        ZipFile.CreateFromDirectory(dir, sfdBackup.FileName, CompressionLevel.Optimal, false);
                        MessageBox.Show("Successfully backed up logs to " + sfdBackup.FileName, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Could not backup logs due to no folder existing. This is a clear sign that no logs have been created, however you can check by clicking the Refresh button on the bottom left of this form.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst backing up log files. Details:\n\n" + ex.ToString(), "IO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void LogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Home.settings.Generic.AnimationSpeed > 0)
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

        public void RefreshLogItems()
        {
            try
            {
                int already = -1;
                if (lstLogFiles.SelectedItem != null)
                    already = lstLogFiles.SelectedIndex;
                lstLogFiles.Items.Clear();
                if (Directory.Exists("Logs"))
                {
                    foreach (string file in Directory.GetFiles("Logs"))
                    {
                        if ((Path.GetExtension(file).ToLower() ?? "") == ".log" | (Path.GetExtension(file).ToLower() ?? "") == ".logdel")
                        {
                            lstLogFiles.Items.Add(Path.GetFileName(file));
                        }
                    }
                }
                if (already > -1)
                {
                    try
                    {
                        lstLogFiles.SelectedIndex = already;
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                {
                    if (lstLogFiles.Count > 0)
                    {
                        lstLogFiles.SelectedIndex = 0;
                        DoLoadFromSelected();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst refreshing log files. Details:\n\n" + ex.ToString(), "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lstLogFiles.Items.Clear();
                return;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLogItems();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            RefreshLogItems();
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
        }

        private void LstLogFiles_SelectedIndexChanged(object sender)
        {
            DoLoadFromSelected();
        }

        private void DoLoadFromSelected()
        {
            try
            {
                if (lstLogFiles.SelectedIndex > -1)
                {
                    dgvMain.Columns.Clear();
                    dgvMain.Rows.Clear();
                    dgvMain.Columns.Add("LINE", "LINE");
                    int intRowCount = 1;
                    bool headersFound = false;
                    foreach (string line in System.IO.File.ReadAllLines(@"Logs\" + lstLogFiles.Items[lstLogFiles.SelectedIndex].ToString()))
                    {
                        if ((intRowCount == 2) && (!headersFound))
                        {
                            string[] arrSplit = line.Split('!');
                            int headerCount = 0;
                            foreach (string i in arrSplit)
                            {
                                dgvMain.Columns.Add("field" + headerCount.ToString(), "field" + headerCount.ToString());
                                ++headerCount;
                            }
                        }
                        if (line.StartsWith("###") && (intRowCount == 1) && (!headersFound))
                        {
                            headersFound = true;
                            var arrSplit = line.Split('!');
                            //Remove hashtags (header indicator)
                            arrSplit[0] = arrSplit[0].Remove(0, 3);
                            //Add headers to datagridview
                            foreach (string item in arrSplit)
                                dgvMain.Columns.Add(item, item);
                        }
                        else if (line.Contains("!"))
                        {
                            var strSplit = line.Split('!');
                            var arrItems = new List<string>
                            {
                                (intRowCount - 1).ToString()
                            };
                            arrItems.AddRange(strSplit);
                            dgvMain.Rows.Add(arrItems.ToArray());
                        }
                        else
                        {
                            List<string> arrValues = new List<string>
                            {
                                (intRowCount - 1).ToString(),
                                line
                            };
                            dgvMain.Rows.Add(arrValues.ToArray());
                        }
                        intRowCount += 1;
                    }
                }
                try
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\" + lstLogFiles.SelectedItem + ".tmp");
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred whilst loading the log file. Details:\n\n" + ex.ToString(), "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvMain.Columns.Clear();
                dgvMain.Rows.Clear();
                lstLogFiles.Items.Clear();
                return;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGoToLine_Click(object sender, EventArgs e)
        {
            libbrhscgui.Components.InputResult ipt = objUI.showInputForm("Enter Row Number", "Go To Row");
            if (ipt.txt == "!cancel=user")
                return;
            else if (string.IsNullOrEmpty(ipt.txt))
            {
                MessageBox.Show("Nothing was entered", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!int.TryParse(ipt.txt, out _))
            {
                MessageBox.Show("Specified Value is not numeric", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    if (dgvMain.Rows.IndexOf(row) == (Convert.ToInt32(ipt.txt) - 1))
                    {
                        dgvMain.CurrentCell = row.Cells[0];
                        return;
                    }
                }
                MessageBox.Show("Could not find row '" + ipt.txt + "'", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            libbrhscgui.Components.InputResult ipt = objUI.showInputForm("Enter Search Term", "Search");
            if (ipt.txt == "!cancel=user")
                return;
            else if (string.IsNullOrEmpty(ipt.txt))
            {
                MessageBox.Show("Nothing was entered", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                foreach (DataGridViewRow row in dgvMain.Rows)
                {
                    foreach (DataGridViewCell c in row.Cells)
                    {
                        if (c.Value.ToString().ToLower().Contains(ipt.txt.ToLower()))
                        {
                            dgvMain.CurrentCell = c;
                            return;
                        }
                    }
                }
                MessageBox.Show("Could not find '" + ipt.txt + "'", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.Rows.Count > 0)
            {
                string value = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                MessageBox.Show(value, "Cell Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}