using Newtonsoft.Json;
using PlexDL.WaitWindow;
using RestSharp;
using System;
using System.Windows.Forms;
using Application = GitHubUpdater.API.Application;

namespace GitHubUpdater
{
    public class UpdateClient
    {
        public string Author { get; set; } = "";
        public string RepositoryName { get; set; } = "";
        public string ApiUrl => $"repos/{Author}/{RepositoryName}/releases/latest";
        private string BaseUrl => "http://api.github.com/";
        public bool DebugMode { get; set; } = false;
        public Version CurrentInstalledVersion { get; set; }

        public void ShowUpdateForm(Application data)
        {
            var frm = new Update { UpdateData = data };
            frm.ShowDialog();
        }

        public void CheckIfLatest()
        {
            if (CurrentInstalledVersion == null)
            {
                MessageBox.Show(@"Couldn't determine the currently installed version because it was null.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var data = GetLatestRelease();
                var vNow = CurrentInstalledVersion;
                var vNew = new Version(data.tag_name.TrimStart('v'));
                var vCompare = vNow.CompareTo(vNew);
                if (vCompare < 0 || DebugMode)
                {
                    ShowUpdateForm(data);
                }
                else
                {
                    MessageBox.Show($"You're running the latest version!\n\nYour version: {vNow}" +
                                    $"\nLatest release: {vNew}", @"Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ShowUpdateForm()
        {
            var data = GetLatestRelease();
            ShowUpdateForm(data);
        }

        public Application GetLatestRelease()
        {
            Application data = null;

            try
            {
                var api = GetUpdateInfo();
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

        private string GetUpdateInfo()
        {
            return (string)WaitWindow.Show(GetUpdateInfoWorker, @"Contacting GitHub");
        }

        protected virtual string GetBaseUrl()
        {
            return BaseUrl;
        }

        private void GetUpdateInfoWorker(object sender, WaitWindowEventArgs e)
        {
            var client = GetRestClient();
            client.UseJson();

            var request = new RestRequest { Resource = ApiUrl };
            var response = client.Execute(request);

            e.Result = response.Content;
        }
    }
}