using inet;
using Newtonsoft.Json;
using PlexDL.WaitWindow;
using RestSharp;
using System;
using System.IO;
using System.Windows.Forms;
using Application = GitHubUpdater.API.Application;

namespace GitHubUpdater
{
    public class UpdateClient
    {
        public string Author { get; set; } = "";
        public string RepositoryName { get; set; } = "";
        public string ApiUrl => $"repos/{Author}/{RepositoryName}/releases/latest";
        private static string BaseUrl => "http://api.github.com/";
        public bool DebugMode { get; set; } = false;
        public Version CurrentInstalledVersion { get; set; }

        public void ShowUpdateForm(Application data)
        {
            var frm = new Update { UpdateData = data };
            frm.ShowDialog();
        }

        public void ConstructDirectory()
        {
            if (!Directory.Exists(Globals.UpdateRootDir))
                Directory.CreateDirectory(Globals.UpdateRootDir);
        }

        public void CheckIfLatest(bool silentCheck = false)
        {
            try
            {
                //make the update_files folder if it doesn't already exist
                ConstructDirectory();

                if (ConnectionChecker.CheckForInternetConnection())
                {
                    if (CurrentInstalledVersion == null)
                    {
                        if (!silentCheck)
                            MessageBox.Show(@"Couldn't determine the currently installed version because it was null.",
                                @"Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var data = GetLatestRelease(!silentCheck);
                        var vNow = CurrentInstalledVersion;
                        var vNew = new Version(data.tag_name.TrimStart('v'));
                        var vCompare = vNow.CompareTo(vNew);
                        if (vCompare < 0 || DebugMode)
                        {
                            ShowUpdateForm(data);
                        }
                        else
                        {
                            if (!silentCheck)
                                MessageBox.Show($"You're running the latest version!\n\nYour version: {vNow}" +
                                                $"\nLatest release: {vNew}", @"Message",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                    if (!silentCheck)
                    MessageBox.Show(@"No internet connection. Failed to perform update check!", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (!silentCheck)
                    MessageBox.Show($@"Error whilst checking for the latest version:

{ex}");
            }
        }

        public Application GetLatestRelease(bool waitWindow = true)
        {
            Application data = null;

            try
            {
                var api = GetUpdateInfo(waitWindow);
                data = JsonConvert.DeserializeObject<Application>(api);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update error\r\n\r\n{ex}");
            }

            return data;
        }

        protected RestClient GetRestClient()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(BaseUrl)
            };
            return client;
        }

        private string GetUpdateInfo(bool waitWindow = true)
        {
            if (waitWindow)
                return (string)WaitWindow.Show(GetUpdateInfoCallback, @"Contacting GitHub");
            return GetUpdateInfoWorker();
        }

        private void GetUpdateInfoCallback(object sender, WaitWindowEventArgs e)
        {
            e.Result = GetUpdateInfoWorker();
        }

        private string GetUpdateInfoWorker()
        {
            var client = GetRestClient();
            client.UseJson();

            var request = new RestRequest { Resource = ApiUrl };
            var response = client.Execute(request);

            return response.Content;
        }
    }
}