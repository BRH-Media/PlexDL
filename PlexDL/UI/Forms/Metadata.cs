using PlexDL.Common;
using PlexDL.Common.API.PlexAPI.IO;
using PlexDL.Common.API.PlexAPI.Objects.AttributeTables;
using PlexDL.Common.Components.Controls;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Structures;
using PlexDL.Common.Structures.Plex;
using PlexDL.ResourceProvider.Properties;
using PlexDL.WaitWindow;
using System;
using System.Drawing;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class Metadata : DoubleBufferedForm
    {
        public Metadata()
            => InitializeComponent();

        public PlexObject StreamingContent { get; set; } = new();
        public bool StationaryMode { get; set; }
        public bool ContainerChange { get; set; } = false;

        private void DgvAttributes_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (ContainerChange)
                    return;
                if (e.ColumnIndex >= 1)
                {
                    var cellToChange = dgvAttributes.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var cellHeading = dgvAttributes.Rows[e.RowIndex].Cells[0];
                    if (cellToChange != null && cellHeading != null)
                    {
                        if (cellHeading.Value.ToString() == @"Container")
                        {
                            var format = ObjectProvider.Settings.MetadataDisplay.MetadataContainerDisplay == MetadataContainerDisplayOption.Description
                                ? MediaContainerFormats.DescriptionToFormat(cellToChange.Value.ToString())
                                : MediaContainerFormats.FormatToDescription(cellToChange.Value.ToString());
                            if (!string.IsNullOrWhiteSpace(format))
                            {
                                cellToChange.Value = format;
                                ContainerChange = true;
                            }
                        }
                    }
                }
            }
            catch
            {
                //nothing
            }
        }

        private void DgvAttributes_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!ContainerChange)
                    return;
                if (e.ColumnIndex >= 1)
                {
                    var cellToChange = dgvAttributes.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var cellHeading = dgvAttributes.Rows[e.RowIndex].Cells[0];
                    if (cellToChange != null && cellHeading != null)
                    {
                        if (cellHeading.Value.ToString() == @"Container")
                        {
                            var description = ObjectProvider.Settings.MetadataDisplay.MetadataContainerDisplay == MetadataContainerDisplayOption.Description
                                ? MediaContainerFormats.FormatToDescription(cellToChange.Value.ToString())
                                : MediaContainerFormats.DescriptionToFormat(cellToChange.Value.ToString());
                            if (!string.IsNullOrWhiteSpace(description))
                            {
                                cellToChange.Value = description;
                                ContainerChange = false;
                            }
                        }
                    }
                }
            }
            catch
            {
                //nothing
            }
        }

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
                       picPoster.BackgroundImage = StreamingContent.StreamInformation.ContentThumbnail ?? ImageHandler.GetPoster(StreamingContent);
                   });
                else
                    picPoster.BackgroundImage = StreamingContent.StreamInformation.ContentThumbnail ?? ImageHandler.GetPoster(StreamingContent);
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
                BackgroundImage = Resources.unavailable,
                Visible = true
            };
            p.Controls.Add(lblActorRole);
            p.Controls.Add(lblActorName);
            p.Controls.Add(actorPortrait);

            return p;
        }

        private Panel ActorPanel(PlexActor a)
        {
            //the containing panel to be returned
            var p = new Panel
            {
                Size = new Size(flpActors.Width, 119),
                Location = new Point(3, 3),
                BackColor = Color.White
            };

            //their name
            var lblActorName = new Label
            {
                Text = a.ActorName,
                AutoSize = true,
                Location = new Point(88, 3),
                Font = new Font(FontFamily.GenericSansSerif, 13),
                Visible = true
            };

            //the role they play in the content
            var lblActorRole = new Label
            {
                Text = a.ActorRole,
                AutoSize = true,
                Location = new Point(112, 29),
                Visible = true
            };

            //actor's photo
            var actorPortrait = new PreviewPictureBox()
            {
                Size = new Size(79, 119),
                Location = new Point(3, 3),
                BackgroundImageLayout = ImageLayout.Zoom,
                BackgroundImage = a.Thumbnail ?? ImageHandler.GetImageFromUrl(a.ThumbnailUri),
                Visible = true
            };

            //add the sub-controls
            p.Controls.Add(lblActorRole);
            p.Controls.Add(lblActorName);
            p.Controls.Add(actorPortrait);

            //return the result
            return p;
        }

        private void ActorsWorker(object sender, WaitWindowEventArgs e)
        {
            //Clear all previous actors (maybe there's a dud panel in place?)
            if (flpActors.Controls.Count > 0)
            {
                if (flpActors.InvokeRequired)
                    flpActors.BeginInvoke((MethodInvoker)delegate
                   {
                       flpActors.Controls.Clear();
                   });
                else
                    flpActors.Controls.Clear();
            }

            if (StreamingContent.Actors.Count > 0)
            {
                //start filling the actors panel from the real data
                foreach (var a in StreamingContent.Actors)
                {
                    //actor panel
                    var p = ActorPanel(a);

                    //add it to the form
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

        private void DoDataLoad()
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

        private void DoImport()
        {
            try
            {
                if (ofdImport.ShowDialog() != DialogResult.OK)
                    return;

                var pxz = MetadataIO.LoadMetadataArchive(ofdImport.FileName);

                if (pxz != null)
                {
                    //assign globals
                    StreamingContent = MetadataIO.MetadataFromFile(pxz);
                    StationaryMode = false;

                    //clear all actors
                    flpActors.Controls.Clear();

                    //set this in-case the loader doesn't find anything; that way the user still gets feedback.
                    txtPlotSynopsis.Text = @"Plot synopsis not provided";

                    //reload all data
                    DoDataLoad();
                }
                else
                    UIMessages.Error(@"Import error; PXZ parse returned null.");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"MetadataPxzImportError");
                UIMessages.Error($"Import error:\n\n{ex}");
            }
        }

        private void DoExport()
        {
            //check if the form has anything loaded (stationary mode), and if there is content loaded (note that the link is checked because StreamingContent is never null,
            //but the link will always be filled if there is valid content loaded)
            if (StationaryMode || StreamingContent.StreamInformation.Links.Download == null ||
                StreamingContent.StreamInformation.Links.View == null) return;
            if (sfdExport.ShowDialog() == DialogResult.OK)
            {
                WaitWindow.WaitWindow.Show(DoExport, @"Exporting");
            }
        }

        private void DoExport(object sender, WaitWindowEventArgs e)
            => MetadataIO.MetadataToFile(sfdExport.FileName, StreamingContent);

        private void Metadata_Load(object sender, EventArgs e)
            => DoDataLoad();

        private void ItmImport_Click(object sender, EventArgs e)
            => DoImport();

        private void ItmExit_Click(object sender, EventArgs e)
            => Close();

        private void ItmExport_Click(object sender, EventArgs e)
            => DoExport();

        private void ItmPvs_Click(object sender, EventArgs e)
            => PvsLauncher.LaunchPvs(StreamingContent);

        private void ItmBrowser_Click(object sender, EventArgs e)
            => BrowserLauncher.LaunchBrowser(StreamingContent);

        private void ItmVlc_Click(object sender, EventArgs e)
            => VlcLauncher.LaunchVlc(StreamingContent);

        private void ItmSourceLinkView_Click(object sender, EventArgs e)
            => LinkViewer.ShowLinkViewer(StreamingContent);

        private void ItmSourceLinkDownload_Click(object sender, EventArgs e)
            => LinkViewer.ShowLinkViewer(StreamingContent, false);

        private void TxtPlotSynopsis_SelectionChanged(object sender, EventArgs e)
            => txtPlotSynopsis.SelectionLength = 0;

        private void ItmDataExplorer_Click(object sender, EventArgs e)
        {
            if (StationaryMode || StreamingContent.StreamInformation.Links.Download == null ||
                StreamingContent.StreamInformation.Links.View == null) return;

            DataExplorer.ShowExplorer(StreamingContent);
        }
    }
}