using PlexDL.Common;
using PlexDL.Common.API.CastAPI;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.Plex;
using SharpCaster.Controllers;
using SharpCaster.Models;
using SharpCaster.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Local

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class Cast : DoubleBufferedForm
    {
        public Cast()
        {
            InitializeComponent();
        }

        public ChromecastService Service { get; set; } = ChromecastService.Current;
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
            => new Cast() { StreamingContent = content }.ShowDialog();

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
                //setup GUI
                btnDiscover.Enabled = false;
                btnDiscover.Text = @"Finding";

                //grab local IPv4 address (for discovery)
                var ip = GetLocalIpAddress();

                //check if the address is invalid
                if (string.IsNullOrEmpty(ip))
                {
                    //alert user
                    UIMessages.Warning(@"Couldn't find an adapter with a valid local IPv4 Address");

                    //setup GUI
                    btnDiscover.Enabled = true;
                    btnDiscover.Text = @"Discover";

                    //exit
                    return;
                }

                //devices list (filtered from below)
                var devices = new List<Chromecast>();

                //discover chromecasts on the LAN
                var chromecasts = await Service.DeviceLocator.LocateDevicesAsync(ip);

                //UIMessages.Info(chromecasts.Count.ToString());

                //clear and refill the devices list
                if (chromecasts.Count > 0)
                {
                    //foreach discovered Chromecast
                    foreach (var c in chromecasts)

                        //if the list doesn't already contain the current Chromecast
                        if (!lstDevices.Items.Contains(c.FriendlyName))
                        {
                            //add it to the devices list
                            devices.Add(c);

                            //add it to the GUI list
                            lstDevices.Items.Add(c.FriendlyName);
                        }
                }
                else

                    //alert the user
                    UIMessages.Warning(@"No devices found");

                //did we find any devices?
                if (devices.Count > 0)

                    //yes, reset the global list
                    Devices = devices;

                //setup the GUI
                btnDiscover.Enabled = true;
                btnDiscover.Text = @"Discover"; //revert back to original text
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastDiscoveryError");

                //alert the user
                UIMessages.Error($"An error occurred whilst trying to find available Chromecast devices:\n\n{ex}");

                //setup the GUI
                btnDiscover.Enabled = true;
                btnDiscover.Text = @"Discover"; //revert back to original text
            }
        }

        private async void Client_Connected(object sender, EventArgs e)
        {
            try
            {
                //multi-threaded check
                if (InvokeRequired)

                    //invoke form
                    BeginInvoke((MethodInvoker)delegate
                   {
                       Client_Connected(sender, e);
                   });
                else
                {
                    //check if the client actually did connect
                    if (Service.ConnectedChromecast != null)
                    {
                        //setup GUI
                        btnCast.Text = @"Launching";

                        //try and launch Plex on the Chromecast device
                        Controller = await Service.ChromeCastClient.LaunchPlex();

                        //give it some time to launch Plex (4 seconds)
                        await Task.Delay(4000);

                        //if it's still null, wait it out until it isn't anymore.
                        while (Controller == null)
                            await Task.Delay(500);

                        //setup GUI
                        btnCast.Text = @"Queueing";

                        //try and load the media
                        //controller and the content.
                        var content = PlexMediaData.DataFromContent(StreamingContent);

                        //recast
                        var data = (CustomData)content.CustomData;

                        //debugging
                        //UIMessages.Info(JsonConvert.SerializeObject(data, Formatting.Indented));

                        //try and create a new playQueue
                        var queue = PlayQueueHandler.NewQueue(StreamingContent, ObjectProvider.Svr);

                        //did the connection request not succeed or was the queue ID empty
                        if (!queue.QueueSuccess || string.IsNullOrWhiteSpace(queue.QueueId))
                        {
                            //alert the user to the problem
                            UIMessages.Warning(
                                $"Couldn't create a new playQueue with media:\n\n" +
                                $"Title: {StreamingContent.StreamInformation.ContentTitle}\n" +
                                $"URI: {StreamingContent.ApiUri}");

                            //stop the application
                            await StopApplication();

                            //exit function
                            return;
                        }

                        //apply new playQueue URI to the container
                        data.containerKey = queue.QueueUri;

                        //UIMessages.Info(data.containerKey);

                        //load the Plex play queue for playback operation
                        await Controller.LoadMedia(content.Url, content.ContentType, null,
                            content.StreamType,
                            0D,
                            data);

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
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastConnectedError");

                //alert the user
                UIMessages.Error($"Error occurred whilst handling post-connection event:\n\n{ex}");

                //stop application
                await StopApplication();
            }
        }

        private async void BtnCast_Click(object sender, EventArgs e)
        {
            try
            {
                //is the application not already connected?
                if (!ConnectState)
                {
                    //has the user selected a device and do we have devices loaded?
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

                            //validation
                            var chromecast = Devices[i];

                            //null validation
                            if (chromecast != null)
                            {
                                //attempt the connection
                                if (Service != null)
                                    await Service.ConnectToChromecast(chromecast);
                            }
                            else
                            {
                                //alert the user; chromecast device provider was null
                                UIMessages.Warning(@"Failed to connect; null cast device provider.");

                                //set UI
                                btnCast.Enabled = true;
                                btnCast.Text = @"Cast";
                            }
                        }
                        else

                            //alert the user
                            UIMessages.Error(
                                @"Indexing error: the selected index does not align with the current device list.", @"Validation Error");
                    }
                    else

                        //alert the user
                        UIMessages.Warning(
                            @"Please select a device from the list. To populate the device list, please press 'Discover'.");
                }
                else

                    //stop the application
                    await StopApplication();
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastInitError");

                //alert the user
                UIMessages.Error($"An error occurred whilst initiating or terminating a connection:\n\n{ex}");

                //stop the application
                await StopApplication();
            }
        }

        private async Task StopApplication()
        {
            try
            {
                //multi-threaded
                if (InvokeRequired)

                    BeginInvoke((MethodInvoker)delegate
                   {
                       StopApplication().GetAwaiter().GetResult();
                   });
                else
                {
                    //set UI
                    btnCast.Enabled = false;
                    btnDiscover.Enabled = true;
                    btnPlayPause.Enabled = false;
                    btnCast.Text = @"Stopping";
                    btnPlayPause.Text = @"Play";

                    //kill the application
                    if (Controller != null)
                        await Controller.StopApplication();

                    //disconnect
                    if (Service?.ConnectedChromecast != null)
                        if (Service.ChromeCastClient != null)
                            await Service.ChromeCastClient.DisconnectChromecast();

                    //restore UI
                    btnCast.Enabled = true;
                    btnCast.Text = @"Cast";

                    //set flags
                    ConnectState = false;
                    PlayState = false;
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastStopError");

                //alert the user
                UIMessages.Error($"Error occurred whilst stopping the application:\n\n{ex}");
            }
        }

        private void Cast_Load(object sender, EventArgs e)
        {
            try
            {
                //warn user
                UIMessages.Warning("Please be aware that the Cast feature is highly " +
                                   "experimental and is unstable; work on this feature " +
                                   "has been limited due to technological issues associated " +
                                   "with WinForms.");

                //setup service events
                if (Service != null)
                    Service.ChromeCastClient.ConnectedChanged += Client_Connected;

                //setup form title
                lblTitle.Text = StreamingContent.StreamInformation.ContentTitle;

                //setup the poster
                picPoster.BackgroundImage = ImageHandler.GetPoster(StreamingContent);
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastUILoadError");

                //alert the user
                UIMessages.Error($"Error occurred during cast load:\n\n{ex}");
            }
        }

        private async void BtnPlayPause_Click(object sender, EventArgs e)
        {
            try
            {
                //is the content already playing?
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
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastPlayStateError");

                //alert the user
                UIMessages.Error($"An error occurred whilst trying to play/pause your media:\n\n{ex}");
            }
        }

        private void LstDevices_SelectedIndexChanged(object sender, EventArgs e)
            =>  //is the current selected item valid?
                btnCast.Enabled = lstDevices.SelectedItem != null;

        private async void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                //is the current devices list valid?
                if (Devices != null)
                {
                    //does the current devices list contain entries?
                    if (Devices.Count > 0)

                        //are we currently connected to a Chromecast?
                        if (Service.ConnectedChromecast != null)
                        {
                            //disable all UI buttons
                            btnDiscover.Enabled = false;
                            btnCast.Enabled = false;
                            btnPlayPause.Enabled = false;
                            btnExit.Enabled = false;

                            //disable the devices list to avoid firing the reenable Cast button event
                            lstDevices.Enabled = false;

                            //set disconnecting text
                            btnExit.Text = @"Disconnecting";

                            //stop the application
                            await StopApplication();
                        }

                    //clear the current devices list
                    Devices = null;
                }
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"CastExitError");
            }

            //close the form
            Close();
        }
    }
}