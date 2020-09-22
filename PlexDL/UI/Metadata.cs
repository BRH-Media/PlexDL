using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.API.Objects.AttributeTables;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Structures.Plex;
using PlexDL.Properties;
using PlexDL.WaitWindow;
using System;
using System.Drawing;
using System.Windows.Forms;
using UIHelpers;

#pragma warning disable 1591

namespace PlexDL.UI
{
    public partial class Metadata : Form
    {
        public Metadata()
        {
            InitializeComponent();
        }

        public PlexObject StreamingContent { get; set; } = new PlexObject();
        public Bitmap Poster { get; set; }
        public bool StationaryMode { get; set; }

        private void LoadWorker(object sender, WaitWindowEventArgs e)
        {
            var attributes = AttributeGatherers.AttributesFromObject(StreamingContent);

            if (attributes.Rows.Count > 0)
            {
                if (dgvAttributes.InvokeRequired)
                    dgvAttributes.BeginInvoke((MethodInvoker)delegate { dgvAttributes.DataSource = attributes; });
                else
                    dgvAttributes.DataSource = attributes;
            }

            //fill the poster picturebox
            if (!string.IsNullOrEmpty(StreamingContent.StreamInformation.ContentThumbnailUri))
            {
                if (picPoster.InvokeRequired)
                    picPoster.BeginInvoke((MethodInvoker)delegate
                   {
                       picPoster.BackgroundImage = Poster ?? ImageHandler.GetPoster(StreamingContent);
                   });
                else
                    picPoster.BackgroundImage = Poster ?? ImageHandler.GetPoster(StreamingContent);
            }

            //fill the plot synopsis infobox
            if (!string.IsNullOrEmpty(StreamingContent.Synopsis))
            {
                if (txtPlotSynopsis.InvokeRequired)
                    txtPlotSynopsis.BeginInvoke((MethodInvoker)delegate
                   {
                       txtPlotSynopsis.Text = !Methods.AdultKeywordCheck(StreamingContent)
                           ? StreamingContent.Synopsis
                           : @"Plot synopsis for this title is unavailable due to adult content protection";
                   });
                else
                    txtPlotSynopsis.Text = !Methods.AdultKeywordCheck(StreamingContent)
                        ? StreamingContent.Synopsis
                        : @"Plot synopsis for this title is unavailable due to adult content protection";
            }

            //apply content title and enable VLC streaming
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate
               {
                   itmStream.Enabled = true;
                   Text = StreamingContent.StreamInformation.ContentTitle;
                   Refresh();
               });
            }
            else
            {
                itmStream.Enabled = true;
                Text = StreamingContent.StreamInformation.ContentTitle;
                Refresh();
            }

            //there's content now; so the window isn't stationary anymore.
            StationaryMode = false;
        }

        private void ActorsWorker(object sender, WaitWindowEventArgs e)
        {
            //Clear all previous actors (maybe there's a dud panel in place?)
            if (flpActors.Controls.Count > 0)
            {
                if (flpActors.InvokeRequired)
                    flpActors.BeginInvoke((MethodInvoker)delegate { flpActors.Controls.Clear(); });
                else
                    flpActors.Controls.Clear();
            }

            if (StreamingContent.Actors.Count > 0)
            {
                //start filling the actors panel from the real data
                foreach (var a in StreamingContent.Actors)
                {
                    var p = new Panel
                    {
                        Size = new Size(flpActors.Width, 119),
                        Location = new Point(3, 3),
                        BackColor = Color.White
                    };
                    var lblActorName = new Label
                    {
                        Text = a.ActorName,
                        AutoSize = true,
                        Location = new Point(88, 3),
                        Font = new Font(FontFamily.GenericSansSerif, 13),
                        Visible = true
                    };

                    var lblActorRole = new Label
                    {
                        Text = a.ActorRole,
                        AutoSize = true,
                        Location = new Point(112, 29),
                        Visible = true
                    };
                    var actorPortrait = new PictureBox
                    {
                        Size = new Size(79, 119),
                        Location = new Point(3, 3),
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BackgroundImage = ImageHandler.GetImageFromUrl(a.ThumbnailUri),
                        Visible = true
                    };
                    p.Controls.Add(lblActorRole);
                    p.Controls.Add(lblActorName);
                    p.Controls.Add(actorPortrait);

                    if (flpActors.InvokeRequired)
                        flpActors.BeginInvoke((MethodInvoker)delegate { flpActors.Controls.Add(p); });
                    else
                        flpActors.Controls.Add(p);
                }
            }
            else
            {
                //no actors, so tell the user with a dud panel
                if (flpActors.InvokeRequired)
                    flpActors.BeginInvoke((MethodInvoker)delegate { flpActors.Controls.Add(NoActorsFound()); });
                else
                    flpActors.Controls.Add(NoActorsFound());
            }
        }

        private static Panel NoActorsFound()
        {
            var p = new Panel
            {
                AutoSize = true,
                Location = new Point(3, 3),
                BackColor = Color.White
            };
            var lblActorName = new Label
            {
                Text = @"No Actors Found",
                AutoSize = true,
                Location = new Point(88, 3),
                Font = new Font(FontFamily.GenericSansSerif, 13),
                Visible = true
            };

            var lblActorRole = new Label
            {
                Text = @"We Couldn't Find Any Actors/Actresses For This Title",
                AutoSize = true,
                Location = new Point(112, 29),
                Visible = true
            };
            var actorPortrait = new PictureBox
            {
                Size = new Size(79, 119),
                Location = new Point(3, 3),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = Resources.image_not_available_png_8,
                Visible = true
            };
            p.Controls.Add(lblActorRole);
            p.Controls.Add(lblActorName);
            p.Controls.Add(actorPortrait);

            return p;
        }

        private void Metadata_Load(object sender, EventArgs e)
        {
            if (!StationaryMode)
            {
                WaitWindow.WaitWindow.Show(LoadWorker, "Parsing Metadata");
                WaitWindow.WaitWindow.Show(ActorsWorker, @"Downloading cast info");
            }
            else
            {
                flpActors.Controls.Clear();
                flpActors.Controls.Add(NoActorsFound());
            }
        }

        private void Metadata_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void DoExport()
        {
            //check if the form has anything loaded (stationary mode), and if there is content loaded (note that the link is checked because StreamingContent is never null,
            //but the link will always be filled if there is valid content loaded)
            if (StationaryMode || StreamingContent.StreamInformation.Links.Download == null ||
                StreamingContent.StreamInformation.Links.View == null) return;
            if (sfdExport.ShowDialog() == DialogResult.OK)
            {
                WaitWindow.WaitWindow.Show(DoExport_Callback, @"Exporting");
            }
        }

        private void DoExport_Callback(object sender, WaitWindowEventArgs e)
        {
            MetadataImportExport.MetadataToFile(sfdExport.FileName, StreamingContent);
        }

        private void DoImport()
        {
            try
            {
                if (ofdImport.ShowDialog() != DialogResult.OK) return;

                var pxz = MetadataImportExport.LoadMetadataArchive(ofdImport.FileName);

                if (pxz != null)
                {
                    StreamingContent = MetadataImportExport.MetadataFromFile(pxz);

                    var profilePoster = (Bitmap)pxz.LoadRecord(@"poster").Content.ToImage();

                    Poster = profilePoster;

                    flpActors.Controls.Clear();

                    //set this in-case the loader doesn't find anything; that way the user still gets feedback.
                    txtPlotSynopsis.Text = @"Plot synopsis not provided";

                    WaitWindow.WaitWindow.Show(LoadWorker, "Parsing Metadata");
                }
                else
                {
                    UIMessages.Error(@"Import error; PXZ parse returned null.");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"MetadataPxzImportError");
                UIMessages.Error($"Import error:\n\n{ex}");
            }
        }

        private void ItmImport_Click(object sender, EventArgs e)
        {
            DoImport();
        }

        private void ItmExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ItmExport_Click(object sender, EventArgs e)
        {
            DoExport();
        }

        private void ItmPvs_Click(object sender, EventArgs e)
        {
            PvsLauncher.LaunchPvs(StreamingContent);
        }

        private void ItmBrowser_Click(object sender, EventArgs e)
        {
            BrowserLauncher.LaunchBrowser(StreamingContent);
        }

        private void ItmVlc_Click(object sender, EventArgs e)
        {
            VlcLauncher.LaunchVlc(StreamingContent);
        }

        private void TxtPlotSynopsis_SelectionChanged(object sender, EventArgs e)
        {
            // cancel any possible selection
            txtPlotSynopsis.SelectionLength = 0;
        }

        private void ItmViewLink_Click(object sender, EventArgs e)
        {
            var viewer = new LinkViewer
            {
                Link = StreamingContent.StreamInformation.Links.Download //download link (octet-stream)
            };

            viewer.ShowDialog();
        }

        private void ItmDataExplorer_Click(object sender, EventArgs e)
        {
            if (StationaryMode || StreamingContent.StreamInformation.Links.Download == null ||
                StreamingContent.StreamInformation.Links.View == null) return;

            DataExplorer.ShowExplorer(StreamingContent);
        }
    }
}