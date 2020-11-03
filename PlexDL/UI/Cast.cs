using PlexDL.Common;
using PlexDL.Common.CastAPI;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using SharpCaster;
using SharpCaster.Controllers;
using SharpCaster.Models;
using SharpCaster.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class Cast : DoubleBufferedForm
    {
        public Cast()
        {
            InitializeComponent();
        }

        public ChromecastService Service { get; set; } = ChromecastService.Current;
        public ChromeCastClient Client { get; set; }
        public List<Chromecast> Devices { get; set; } = new List<Chromecast>();
        public PlexObject StreamingContent { get; set; }
        public PlexController Controller { get; set; }

        //states
        public bool ConnectState { get; set; }

        public bool PlayState { get; set; }

        /// <summary>
        /// Try and launch the cast window with the specified content
        /// </summary>
        /// <param name="content">The PlexObject to cast</param>
        public static void TryCast(PlexObject content)
        {
            using (var frm = new Cast { StreamingContent = content })
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Returns the first local IPv4 address on the current computer (attempts to avoid virtualised adapters)
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIpAddress()
        {
            //current DNS Host (this PC)
            var host = Dns.GetHostEntry(Dns.GetHostName());

            //go through each address on the current machine
            foreach (var ip in host.AddressList)
                //check if it's IPv4 and if it's a private address (e.g. 10.* and 192.168.*)
                if (ip.AddressFamily == AddressFamily.InterNetwork && Methods.IsPrivateIp(ip.ToString()))
                    //first match is returned
                    return ip.ToString();

            //return blank for no matches
            return @"";
        }

        private async void BtnDiscover_Click(object sender, EventArgs e)
        {
            try
            {
                //set UI
                btnDiscover.Enabled = false;
                btnDiscover.Text = @"Finding";

                //grab local IPv4 address (for discovery)
                var ip = GetLocalIpAddress();

                //check if the address is invalid
                if (string.IsNullOrEmpty(ip))
                {
                    //it is, so alert the user, revert the UI and exit the function.
                    UIMessages.Warning(@"Couldn't find an adapter with a valid local IPv4 Address");
                    btnDiscover.Enabled = true;
                    btnDiscover.Text = @"Discover";
                    return;
                }

                //UIMessages.Info(ip);

                //devices list (filtered from below)
                var devices = new List<Chromecast>();

                //discover chromecasts on the LAN
                var chromecasts = await Service.DeviceLocator.LocateDevicesAsync(ip);

                //UIMessages.Info(chromecasts.Count.ToString());

                //clear and refill the devices list
                if (chromecasts.Count > 0)
                {
                    foreach (var c in chromecasts)
                        if (!lstDevices.Items.Contains(c.FriendlyName))
                        {
                            devices.Add(c);
                            lstDevices.Items.Add(c.FriendlyName);
                        }
                }
                else
                {
                    UIMessages.Warning(@"No devices found");
                }

                if (devices.Count > 0) Devices = devices;

                btnDiscover.Enabled = true;
                btnDiscover.Text = @"Discover"; //revert back to original text
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CastDiscoveryError");
                UIMessages.Error($"An error occurred whilst trying to find available Chromecast devices:\n\n{ex}");

                btnDiscover.Enabled = true;
                btnDiscover.Text = @"Discover"; //revert back to original text
            }
        }

        private async void BtnCast_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ConnectState)
                {
                    if (lstDevices.SelectedItem != null && Devices.Count > 0)
                    {
                        //Index check. Makes sure that the index does exist within the array before
                        //trying to access it.
                        if (Devices.Count >= lstDevices.SelectedIndex + 1)
                        {
                            //set UI
                            btnCast.Enabled = false;
                            btnCast.Text = @"Connecting";

                            //match list index to an actual stored chromecast
                            var i = lstDevices.SelectedIndex;
                            var chromecast = Devices[i];

                            //attempt the connection
                            await Service.ConnectToChromecast(chromecast);

                            //give it some time to connect (5 seconds)
                            await Task.Delay(5000);

                            //check if the client actually did connect
                            if (Service.ConnectedChromecast != null && Service.ChromeCastClient != null)
                            {
                                btnCast.Text = @"Launching";

                                //set the client to the current service client
                                Client = Service.ChromeCastClient;

                                //try and launch Plex on the Chromecast device
                                Controller = await Client.LaunchPlex();

                                //give it some time to launch Plex (4 seconds)
                                await Task.Delay(4000);

                                //if it's still null, wait it out until it isn't anymore.
                                while (Controller == null)
                                    await Task.Delay(500);

                                //set UI
                                btnCast.Text = @"Queueing";

                                //try and load the media
                                //controller and the content.
                                var content = PlexMediaData.DataFromContent(StreamingContent);
                                var data = (CustomData)content.CustomData;

                                //try and create a new playQueue
                                var queue = PlayQueueHandler.NewQueue(StreamingContent, ObjectProvider.Svr);
                                if (!queue.QueueSuccess || string.IsNullOrEmpty(queue.QueueId))
                                {
                                    UIMessages.Warning(
                                        $"Couldn't create a new playQueue with media:\n\nTitle: {StreamingContent.StreamInformation.ContentTitle}\nURI: {StreamingContent.ApiUri}");

                                    //stop the application
                                    await StopApplication();

                                    //exit function
                                    return;
                                }

                                //apply new playQueue URI to the container
                                data.containerKey = queue.QueueUri;

                                //UIMessages.Info(JsonConvert.SerializeObject(data, Formatting.Indented));

                                await Controller.LoadMedia(content.Url, content.ContentType, null, content.StreamType,
                                    0D,
                                    content.CustomData);

                                //set UI
                                btnCast.Enabled = true;
                                btnCast.Text = @"Stop";
                                btnDiscover.Enabled = false;
                                btnPlayPause.Enabled = true;
                                btnPlayPause.Text = @"Play";

                                //set global flags
                                ConnectState = true;
                                PlayState = false;
                            }
                            else
                            {
                                //alert the user; client is null which means it failed.
                                UIMessages.Warning(@"Failed to connect; null connection providers.");

                                //set UI
                                btnCast.Enabled = true;
                                btnCast.Text = @"Cast";
                            }
                        }
                        else
                            UIMessages.Error(
                                @"Indexing error: the selected index does not align with the current device list.", @"Validation Error");
                    }
                    else
                        UIMessages.Warning(
                            @"Please select a device from the list. To populate the device list, please press 'Discover'.");
                }
                else
                    await StopApplication();
            }
            catch (Exception ex)
            {
                UIMessages.Error($"An error occurred whilst initiating or terminating a connection:\n\n{ex}");
                LoggingHelpers.RecordException(ex.Message, @"CastInitError");

                //stop the application
                await StopApplication();
            }
        }

        private async Task StopApplication()
        {
            try
            {
                //set UI
                btnCast.Enabled = false;
                btnCast.Text = @"Stopping";
                btnDiscover.Enabled = true;
                btnPlayPause.Enabled = false;
                btnPlayPause.Text = @"Play";

                //kill the application
                await Controller.StopApplication();

                //disconnect
                if (Service.ConnectedChromecast != null)
                    await Client.DisconnectChromecast();

                //restore UI
                btnCast.Enabled = true;
                btnCast.Text = @"Cast";

                //set flags
                ConnectState = false;
                PlayState = false;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CastStopError");
            }
        }

        private void Cast_Load(object sender, EventArgs e)
        {
            try
            {
                lblTitle.Text = StreamingContent.StreamInformation.ContentTitle;
                picPoster.BackgroundImage = ImageHandler.GetPoster(StreamingContent);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CastUILoadError");
            }
        }

        private async void BtnPlayPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlayState)
                {
                    //send pause command
                    await Controller.Pause();

                    //set UI
                    btnPlayPause.Text = @"Play";

                    //set flag
                    PlayState = false;
                }
                else
                {
                    //send play command
                    await Controller.Play();

                    //set UI
                    btnPlayPause.Text = @"Pause";

                    //set flag
                    PlayState = true;
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"CastPlayStateError");
                UIMessages.Error($"An error occurred whilst trying to play/pause your media:\n\n{ex}");
            }
        }

        private void LstDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDevices.SelectedItem != null) btnCast.Enabled = true;
        }

        private async void BtnExit_Click(object sender, EventArgs e)
        {
            if (Devices != null)
            {
                if (Devices.Count > 0)
                    if (Service.ConnectedChromecast != null)
                    {
                        btnDiscover.Enabled = false;
                        btnCast.Enabled = false;
                        btnPlayPause.Enabled = false;
                        btnExit.Enabled = false;
                        btnExit.Text = @"Disconnecting";
                        await StopApplication();
                    }

                Devices = null;
            }

            Close();
        }
    }
}