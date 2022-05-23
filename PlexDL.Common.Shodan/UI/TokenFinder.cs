using LogDel.Utilities.Export;
using PlexDL.Common.Shodan.Enums;
using PlexDL.WaitWindow;
using Shodan.Client;
using Shodan.Models;
using Shodan.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHelpers;
using Strings = PlexDL.Common.Shodan.Globals.Strings;

// ReSharper disable InvertIf

namespace PlexDL.Common.Shodan.UI
{
    public partial class TokenFinder : Form
    {
        /// <summary>
        /// Store all Shodan results here ready for processing
        /// </summary>
        public List<Service> ShodanResults { get; set; }

        public TokenFinderResult Result { get; set; } = new TokenFinderResult();

        public TokenFinder()
        {
            InitializeComponent();
        }

        public static TokenFinderResult Launch()
        {
            try
            {
                //create a new instance
                var finder = new TokenFinder();

                //show the instance
                finder.ShowDialog();

                //return the result object
                return finder.Result;
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"TokenFinder launch error:\n\n{ex}");
            }

            //default
            return null;
        }

        private void TokenFinder_Load(object sender, EventArgs e)
        {
            //load API key
            Strings.ShodanApiKey = ApiKeyManager.StoredShodanApiKey();
        }

        private void ItmSettings_Click(object sender, EventArgs e)
        {
            //show the settings window
            new TokenFinderSettings().ShowDialog();
        }

        private void ItmFileExport_Click(object sender, EventArgs e)
        {
            try
            {
                //validation
                if (dgvTokens.Rows.Count > 0)
                {
                    //grab the data table that is being used
                    var data = (DataTable)dgvTokens.DataSource;

                    //show the window
                    if (sfdExport.ShowDialog() == DialogResult.OK)
                    {
                        //file chosen; extract extension
                        var ext = Path.GetExtension(sfdExport.FileName);

                        //what should we do?
                        switch (ext)
                        {
                            //CSV
                            case @".csv":

                                //start the export
                                data.ToCsv(sfdExport.FileName);

                                //exit
                                break;

                            //JSON
                            case @".json":

                                //start the export
                                data.ToJson(sfdExport.FileName);

                                //exit
                                break;

                            //LOGDEL
                            case @".logdel":

                                //start the export
                                data.ToLogdel(sfdExport.FileName);

                                //exit
                                break;

                            //XML
                            case @".xml":

                                //start the export
                                data.ToXml(sfdExport.FileName);

                                //exit
                                break;

                            //invalid type!
                            default:

                                //alert user
                                UIMessages.Error(@"Export failed; invalid file type specified");

                                //exit
                                return;
                        }

                        //alert user
                        UIMessages.Info($@"Exported data to: {sfdExport.FileName}");
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"No data available");
                }
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"Export error:\n\n{ex}");
            }
        }

        private void SetViewingLabel()
        {
            //loaded results
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    lblViewingValue.Text = $@"{dgvTokens.Rows.Count}/{dgvTokens.Rows.Count}";
                });
            }
            else
            {
                lblViewingValue.Text = $@"{dgvTokens.Rows.Count}/{dgvTokens.Rows.Count}";
            }
        }

        private void ExecuteTokenScraping(object sender, WaitWindowEventArgs e)
            => ExecuteTokenScraping(false);

        private void ExecuteTokenScraping(bool multiThreaded = true)
        {
            try
            {
                //ensure Shodan results are filled
                if (ShodanResults?.Count > 0)
                {
                    //do we need to apply a wait  window?
                    if (multiThreaded)
                    {
                        //setup wait window
                        WaitWindow.WaitWindow.Show(ExecuteTokenScraping, $@"Scraping {ShodanResults.Count} servers");
                    }
                    else
                    {
                        //this is where data will be stored
                        var data = new DataTable(@"TautulliHosts");
                        data.Columns.Add(@"First Seen");
                        data.Columns.Add(@"IP Address");
                        data.Columns.Add(@"Port");
                        data.Columns.Add(@"Token");

                        //go through each service
                        foreach (var s in ShodanResults)
                        {
                            //form a URL based on the service
                            var settingsUrl = $"http://{s.IPStr}:{s.Port}/settings";

                            //store HTML content here
                            var page = @"";

                            try
                            {
                                //download the page
                                var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
                                page = httpClient.GetStringAsync(settingsUrl).GetAwaiter().GetResult();
                            }
                            catch (HttpRequestException)
                            {
                                //do nothing
                            }
                            catch (TaskCanceledException)
                            {
                                //do nothing
                            }

                            //validation
                            if (!string.IsNullOrWhiteSpace(page))
                            {
                                //load a HTML document for parsing purposes
                                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                                htmlDoc.LoadHtml(page);

                                //try and find the pms_token <input> tag in the html file (the textbox in Tautulli for Plex.tv Authentication)
                                var pmsToken = htmlDoc.DocumentNode.Descendants("input")
                                    .Where(node => node.GetAttributeValue("id", "")
                                        .Equals("pms_token")).ToList();

                                //validation
                                if (pmsToken.Count > 0)
                                {
                                    //get the token from <input id="pms_token" value="[token_here]">
                                    var token = pmsToken[0].Attributes["value"].Value;

                                    //check if the token's valid (Plex tokens are always exactly 20 characters in length)
                                    if (!string.IsNullOrWhiteSpace(token))
                                    {
                                        //these checks must be nested, because if we check for token.Length before a null check is passed, an exception could be thrown.
                                        if (token.Length == 20)
                                        {
                                            //calculate date first seen
                                            var firstSeen =
                                                (DateTime.Now - TimeSpan.FromMinutes(s.Uptime)).ToString(@"dd/MM/yyyy");

                                            //create information array
                                            object[] dataRow = { firstSeen, s.IPStr, s.Port.ToString(), token };

                                            //add it to the table
                                            data.Rows.Add(dataRow);
                                        }
                                    }
                                }
                            }
                        }

                        //do we have results?
                        if (data.Rows.Count > 0)
                        {
                            //setup table
                            if (dgvTokens.InvokeRequired)
                            {
                                dgvTokens.BeginInvoke((MethodInvoker)delegate
                               {
                                   dgvTokens.DataSource = data;
                                   cxtGridStartSession.Enabled = true;
                                   cxtGridCopyToken.Enabled = true;
                               });
                            }
                            else
                            {
                                dgvTokens.DataSource = data;
                                cxtGridStartSession.Enabled = true;
                                cxtGridCopyToken.Enabled = true;
                            }

                            //setup viewing label
                            SetViewingLabel();
                        }
                        else
                        {
                            //setup table
                            if (dgvTokens.InvokeRequired)
                            {
                                dgvTokens.BeginInvoke((MethodInvoker)delegate
                                {
                                    dgvTokens.DataSource = null;
                                    cxtGridStartSession.Enabled = false;
                                    cxtGridCopyToken.Enabled = false;
                                });
                            }
                            else
                            {
                                dgvTokens.DataSource = null;
                                cxtGridStartSession.Enabled = false;
                                cxtGridCopyToken.Enabled = false;
                            }

                            //setup viewing label
                            SetViewingLabel();

                            //alert user
                            UIMessages.Error(@"No results found");
                        }
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"Failed to scrape tokens; no Shodan results are currently defined");
                }
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"Token scrape error:\n\n{ex}");
            }
        }

        private void ExecuteSearch(object sender, WaitWindowEventArgs e)
            => ExecuteSearch(false);

        private void ExecuteSearch(bool multiThreaded = true)
        {
            try
            {
                //do we need to apply a wait window?
                if (multiThreaded)
                {
                    //setup wait window
                    WaitWindow.WaitWindow.Show(ExecuteSearch, @"Searching Shodan");
                }
                else
                {
                    //setup Shodan client
                    var client = new ClientFactory(Strings.ShodanApiKey).GetFullClient();

                    //setup search
                    var query = new ShodanSearchQuery
                    {
                        //this will try and find all Tautulli instances
                        Text = "CherryPy/5.1.0 /home"
                    };

                    //execute the query!
                    var result = client.Search(query).GetAwaiter().GetResult();

                    //validation
                    if (result?.Services != null)
                    {
                        //set Shodan results list
                        ShodanResults = result.Services;
                    }
                    else
                    {
                        //alert user
                        UIMessages.Error(@"Couldn't execute search; result was null");
                    }
                }
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"Shodan search error:\n\n{ex}");
            }
        }

        private void BeginSearch()
        {
            try
            {
                //data loaded?
                if (dgvTokens.Rows.Count > 0)
                {
                    //ask user first
                    if (!UIMessages.Question(
                            @"You've already got a set of tokens loaded; do you want to clear these and search again?"))
                    {
                        //user doesn't want to proceed
                        return;
                    }
                }

                //token available?
                if (!string.IsNullOrWhiteSpace(Strings.ShodanApiKey))
                {
                    //execute the Shodan search
                    ExecuteSearch();

                    //results filled?
                    if (ShodanResults?.Count > 0)
                    {
                        //execute scraping
                        ExecuteTokenScraping();
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"Please setup a Shodan API key in Settings before performing a search");
                }
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"Search error:\n\n{ex}");
            }
        }

        private void StartSession()
        {
            try
            {
                //data selected?
                if (dgvTokens.SelectedRows.Count == 1)
                {
                    //locate the token
                    var selectedToken = (string)dgvTokens.SelectedRows[0].Cells[dgvTokens.SelectedRows[0].Cells.Count - 1]
                        .Value;

                    //validation
                    if (!string.IsNullOrWhiteSpace(selectedToken))
                    {
                        //token length validation
                        if (selectedToken.Length == 20)
                        {
                            //apply results
                            Result = new TokenFinderResult
                            {
                                Status = TokenFinderStatus.TOKEN_FOUND_START_SESSION,
                                Token = selectedToken
                            };

                            //close the form
                            Close();
                        }
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"You cannot start a session as you have not selected a token from the grid");
                }
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"Start session error:\n\n{ex}");
            }
        }

        private void ItmActionsBeginSearch_Click(object sender, EventArgs e)
            => BeginSearch();

        private void CxtGridBeginSearch_Click(object sender, EventArgs e)
            => BeginSearch();

        private void CxtGridStartSession_Click(object sender, EventArgs e)
            => StartSession();

        private void CxtGridCopyToken_Click(object sender, EventArgs e)
        {
            try
            {
                //data selected?
                if (dgvTokens.SelectedRows.Count == 1)
                {
                    //locate the token
                    var selectedToken = (string)dgvTokens.SelectedRows[0].Cells[dgvTokens.SelectedRows[0].Cells.Count - 1]
                        .Value;

                    //validation
                    if (!string.IsNullOrWhiteSpace(selectedToken))
                    {
                        //token length validation
                        if (selectedToken.Length == 20)
                        {
                            //set it to the clipboard
                            Clipboard.SetText(selectedToken);
                        }
                    }
                }
                else
                {
                    //alert user
                    UIMessages.Error(@"Please select a token to copy");
                }
            }
            catch (Exception ex)
            {
                //alert user
                UIMessages.Error($"Copy token error:\n\n{ex}");
            }
        }
    }
}