using GitHubUpdater.API;
using GitHubUpdater.UI;
using inet;
using Newtonsoft.Json;
using PlexDL.AltoHTTP.Common.Net;
using PlexDL.Common.Security.Hashing;
using PlexDL.WaitWindow;
using RestSharp;
using System;
using System.IO;
using System.Windows.Forms;
using Application = GitHubUpdater.API.Application;
using UpdateChannel = GitHubUpdater.Enums.UpdateChannel;

// ReSharper disable LocalizableElement
// ReSharper disable SpecifyACultureInStringConversionExplicitly
// ReSharper disable InvertIf

namespace GitHubUpdater
{
    public class UpdateClient
    {
        public string Author { get; set; } = "";
        public string RepositoryName { get; set; } = "";
        public string AllApiUrl => $"repos/{Author}/{RepositoryName}/releases";
        public string LatestApiUrl => $"{AllApiUrl}/latest";
        private static string BaseUrl => "http://api.github.com/";
        public bool DebugMode { get; set; } = false;
        public Version CurrentInstalledVersion { get; set; }

        /// <summary>
        /// Creates a new Update form and then shows it based on the UpdateResponse data provided
        /// </summary>
        /// <param name="data"></param>
        public void ShowUpdateForm(UpdateResponse data)
        {
            var frm = new Update
            {
                AppUpdate = data
            };

            frm.ShowDialog();
        }

        /// <summary>
        /// Ensures the update directory is created
        /// </summary>
        public void ConstructDirectory()
        {
            if (!Directory.Exists(Globals.UpdateRootDir))
                Directory.CreateDirectory(Globals.UpdateRootDir);
        }

        /// <summary>
        /// Downloads the update information from GitHub's servers
        /// </summary>
        /// <param name="waitWindow"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public UpdateResponse GetUpdateData(bool waitWindow = true, UpdateChannel channel = UpdateChannel.Stable)
        {
            var data = new UpdateResponse();

            try
            {
                //get the update from the right channel
                data.UpdateData = channel switch
                {
                    UpdateChannel.Stable => GetLatestStableRelease(waitWindow),
                    UpdateChannel.Development => GetLatestDevelopmentRelease(waitWindow),
                    UpdateChannel.Unknown => null,
                    _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
                };

                //fetch the data itself and apply it
                data.CurrentVersion = CurrentInstalledVersion;

                //apply the channel mode to the update data
                data.Channel = channel;
            }
            catch
            {
                //ignore
            }

            //default; return blank UpdateResponse
            return data;
        }

        /// <summary>
        /// Main update routine
        /// </summary>
        /// <param name="silentCheck"></param>
        public void CheckIfLatest(bool silentCheck = false)
        {
            try
            {
                //make the update_files folder if it doesn't already exist
                ConstructDirectory();

                if (Internet.IsConnected)
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
                        //if in debug mode, the user can select the preferred update channel,
                        //otherwise it'll always be the stable channel (Production Release).
                        var channel = DebugMode
                            ? UI.UpdateChannel.ShowChannelSelector()
                            : UpdateChannel.Stable;

                        //wait window isn't silent (it's a window after all), so it needs to be accounted for here
                        //so as to not show anything
                        var data = GetUpdateData(!silentCheck, channel);

                        //did the request succeed and give us good data?
                        if (data.Valid)

                            //debug mode shows the form regardless of being up-to-date
                            if (!data.UpToDate || DebugMode)
                            {
                                ShowUpdateForm(data);
                            }
                            else
                            {
                                //show the up-to-date message
                                if (!silentCheck)
                                    MessageBox.Show(
                                        $"You're running the latest version!\n\nYour version: {data.CurrentVersion}" +
                                        $"\nLatest release: {data.UpdatedVersion} ({data.Channel} Channel)", @"Message",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        else
                            //the data we got wasn't valid and failed the checks; alert the user.
                            MessageBox.Show(DebugMode
                                    ? "Update data was invalid; cannot process update information.\nDid you select a valid channel?"
                                    : "Update data was invalid; cannot process update information.",
                                @"Update Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show($"Error whilst checking for the latest version:\n\n{ex}");
            }
        }

        /// <summary>
        /// Gets the latest 'stable' update
        /// </summary>
        /// <param name="waitWindow"></param>
        /// <returns></returns>
        public Application GetLatestStableRelease(bool waitWindow = true)
        {
            Application data = null;

            try
            {
                var api = GetUpdateInfo(LatestApiUrl, waitWindow);
                data = JsonConvert.DeserializeObject<Application>(api, Converter.Settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update error\r\n\r\n{ex}");
            }

            return data;
        }

        /// <summary>
        /// Gets the latest 'pre-release' update
        /// </summary>
        /// <param name="waitWindow"></param>
        /// <returns></returns>
        public Application GetLatestDevelopmentRelease(bool waitWindow = true)
        {
            try
            {
                var currentReleasesList = GetAllReleases(waitWindow);

                foreach (var r in currentReleasesList)
                    if (r.prerelease)
                        return r;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update error\r\n\r\n{ex}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Downloads and serialises release information for an update
        /// </summary>
        /// <param name="waitWindow"></param>
        /// <returns></returns>
        public Application[] GetAllReleases(bool waitWindow = true)
        {
            Application[] data = null;

            try
            {
                var api = GetUpdateInfo(AllApiUrl, waitWindow);
                data = JsonConvert.DeserializeObject<Application[]>(api, Converter.Settings);
            }
            catch (Exception ex)
            {
                //log the error
                ex.ExportError();

                //alert the user
                MessageBox.Show($"Update error\r\n\r\n{ex}");
            }

            return data;
        }

        /// <summary>
        /// Constructs a new rest client from the GitHub API's base URI
        /// </summary>
        /// <returns></returns>
        protected RestClient GetRestClient()
        {
            var client = new RestClient(BaseUrl);
            client.Options.UserAgent = NetGlobals.GlobalUserAgent;
            return client;
        }

        /// <summary>
        /// Multi-threaded code; this is automatic do not hook or call.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetUpdateInfo(object sender, WaitWindowEventArgs e)
        {
            if (e.Arguments.Count == 1)
            {
                var resource = (string)e.Arguments[0];
                e.Result = GetUpdateInfo(resource, false);
            }
        }

        /// <summary>
        /// Retrieve a GitHub API update resource
        /// </summary>
        /// <param name="resource">The relative path; not a complete URI</param>
        /// <param name="waitWindow"></param>
        /// <returns></returns>
        private string GetUpdateInfo(string resource, bool waitWindow = true)
        {
            if (waitWindow)
                return (string)WaitWindow.Show(GetUpdateInfo, @"Contacting GitHub", resource);

            var client = GetRestClient();
            client.UseJson();

            var request = new RestRequest { Resource = resource };
            var response = client.Execute(request);

            var apiJson = response.Content;

            //log json (pretty-printed)
            LogApiResponse(apiJson, true);

            return apiJson;
        }

        /// <summary>
        /// Writes API JSON to a file in &lt;AssemblyDirectory&gt;\update_files\debug
        /// </summary>
        /// <param name="apiResponse">The raw JSON itself</param>
        /// <param name="prettyPrint">Whether or not the JSON is formatted when the file is written (false for better performance, true for readability)</param>
        private void LogApiResponse(string apiResponse, bool prettyPrint = false)
        {
            try
            {
                //check if the data is valid before trying to write it to a file,
                //and only allow this function to run if in debug mode
                if (!string.IsNullOrWhiteSpace(apiResponse) && DebugMode)
                {
                    //hash a new GUID for uniqueness
                    var uniqueString = MD5Helper.CalculateMd5Hash(Guid.NewGuid().ToString());

                    //GitHub API responses are always in JSON format; the extension must reflect this.
                    var fileName = @$"apiResponse_{uniqueString}.json";
                    var fileDir = $@"{Globals.UpdateRootDir}\debug";
                    var filePath = $@"{fileDir}\{fileName}";

                    //ensure this directory exists
                    if (!Directory.Exists(fileDir))
                        Directory.CreateDirectory(fileDir);

                    //finally, write the API contents to the file
                    File.WriteAllText(filePath,
                        prettyPrint
                        ? apiResponse.FormatJson()
                        : apiResponse);
                }
            }
            catch
            {
                //ignore all errors
            }
        }
    }
}