using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using PlexDL.Common;

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

        private void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        private void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        public void btnBackup_Click(object sender, EventArgs e)
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
                t1 = new Timer();
                t1.Interval = Home.settings.Generic.AnimationSpeed;
                t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
                t1.Start();

                if (Opacity == 0)
                {
                    //resume the event - the program can be closed
                    e.Cancel = false;
                }
            }
        }

        public void refreshLogItems()
        {
            try
            {
                string strAlready = "";
                if (lstLogFiles.SelectedItem != null)
                    strAlready = lstLogFiles.SelectedItem.ToString();
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
                if (!string.IsNullOrEmpty(strAlready))
                {
                    try
                    {
                        lstLogFiles.SelectedItem = strAlready;
                    }
                    catch
                    {
                        return;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshLogItems();
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            refreshLogItems();
            if (Home.settings.Generic.AnimationSpeed > 0)
            {
                Opacity = 0;      //first the opacity is 0
                t1 = new Timer();
                t1.Interval = Home.settings.Generic.AnimationSpeed;  //we'll increase the opacity every 10ms
                t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity
                t1.Start();
            }
        }

        private void lstLogFiles_SelectedIndexChanged(object sender)
        {
            {
                try
                {
                    if (lstLogFiles.SelectedIndex != -1)
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
                                var arrItems = new List<string>();
                                arrItems.Add((intRowCount - 1).ToString());
                                arrItems.AddRange(strSplit);
                                dgvMain.Rows.Add(arrItems.ToArray());
                            }
                            else
                            {
                                var arrValues = new List<string>();
                                arrValues.Add((intRowCount - 1).ToString());
                                arrValues.Add(line);
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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGoToLine_Click(object sender, EventArgs e)
        {
            libbrhscgui.Components.InputResult ipt = objUI.showInputForm("Enter Row Number", "Go To Row");
            if (ipt.txt == "!cancel=user")
                return;
            else if (string.IsNullOrEmpty(ipt.txt))
            {
                MessageBox.Show("Nothing was entered", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!int.TryParse(ipt.txt, out int r))
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

        private void btnSearch_Click(object sender, EventArgs e)
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

        private void dgvMain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.Rows.Count > 0)
            {
                string value = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                MessageBox.Show(value, "Cell Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}