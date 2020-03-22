using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Globals;
using PlexDL.Common.Renderers;
using PlexDL.Common.Structures.Plex;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Metadata : Form
    {
        public PlexObject StreamingContent { get; set; } = new PlexObject();
        public bool StationaryMode { get; set; } = false;

        public Metadata()
        {
            InitializeComponent();
        }

        private void LoadWorker(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            //fill the genre infobox
            if (!string.IsNullOrEmpty(StreamingContent.ContentGenre))
            {
                if (lblGenreValue.InvokeRequired)
                    lblGenreValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblGenreValue.Text = StreamingContent.ContentGenre;
                    });
                else
                    lblGenreValue.Text = StreamingContent.ContentGenre;
            }

            //fill the runtime duration infobox
            if (StreamingContent.StreamInformation.ContentDuration > 0)
            {
                if (lblRuntimeValue.InvokeRequired)
                    lblRuntimeValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblRuntimeValue.Text = Methods.CalculateTime(StreamingContent.StreamInformation.ContentDuration);
                    });
                else
                    lblRuntimeValue.Text = Methods.CalculateTime(StreamingContent.StreamInformation.ContentDuration);
            }

            //fill the size infobox
            if (StreamingContent.StreamInformation.ByteLength > 0)
            {
                if (lblSizeValue.InvokeRequired)
                    lblSizeValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblSizeValue.Text = Methods.FormatBytes(StreamingContent.StreamInformation.ByteLength);
                    });
                else
                    lblSizeValue.Text = Methods.FormatBytes(StreamingContent.StreamInformation.ByteLength);
            }

            //fill the resolution infobox
            if (StreamingContent.StreamResolution.Height > 0)
            {
                if (lblResolutionValue.InvokeRequired)
                    lblResolutionValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblResolutionValue.Text = StreamingContent.StreamResolution.ResolutionString();
                    });
                else
                    lblResolutionValue.Text = StreamingContent.StreamResolution.ResolutionString();
            }

            //fill the poster picturebox
            if (!string.IsNullOrEmpty(StreamingContent.StreamInformation.ContentThumbnailUri))
            {
                if (picPoster.InvokeRequired)
                    picPoster.BeginInvoke((MethodInvoker)delegate
                    {
                        picPoster.BackgroundImage = GetPoster(StreamingContent);
                    });
                else
                    picPoster.BackgroundImage = GetPoster(StreamingContent);
            }

            //fill the container infobox
            if (!string.IsNullOrEmpty(StreamingContent.StreamInformation.Container))
            {
                if (lblContainerValue.InvokeRequired)
                    lblContainerValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblContainerValue.Text = StreamingContent.StreamInformation.Container;
                    });
                else
                    lblContainerValue.Text = StreamingContent.StreamInformation.Container;
            }

            //fill the plot synopsis infobox
            if (!string.IsNullOrEmpty(StreamingContent.Synopsis))
            {
                if (txtPlotSynopsis.InvokeRequired)
                    txtPlotSynopsis.BeginInvoke((MethodInvoker)delegate
                    {
                        txtPlotSynopsis.Text = StreamingContent.Synopsis;
                    });
                else
                    txtPlotSynopsis.Text = StreamingContent.Synopsis;
            }

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
                    var p = new Panel()
                    {
                        Size = new Size(flpActors.Width, 119),
                        Location = new Point(3, 3),
                        BackColor = Color.White
                    };
                    var lblActorName = new Label()
                    {
                        Text = a.ActorName,
                        AutoSize = true,
                        Location = new Point(88, 3),
                        Font = new Font(FontFamily.GenericSansSerif, 13),
                        Visible = true
                    };

                    var lblActorRole = new Label()
                    {
                        Text = a.ActorRole,
                        AutoSize = true,
                        Location = new Point(112, 29),
                        Visible = true
                    };
                    var actorPortrait = new PictureBox()
                    {
                        Size = new Size(79, 119),
                        Location = new Point(3, 3),
                        BackgroundImageLayout = ImageLayout.Zoom,
                        BackgroundImage = Methods.GetImageFromUrl(a.ThumbnailUri),
                        Visible = true
                    };
                    p.Controls.Add(lblActorRole);
                    p.Controls.Add(lblActorName);
                    p.Controls.Add(actorPortrait);

                    if (flpActors.InvokeRequired)
                        flpActors.BeginInvoke((MethodInvoker)delegate
                        {
                            flpActors.Controls.Add(p);
                        });
                    else
                        flpActors.Controls.Add(p);
                }
            }
            else
            {
                //no actors, so tell the user with a dud panel
                if (flpActors.InvokeRequired)
                    flpActors.BeginInvoke((MethodInvoker)delegate
                    {
                        flpActors.Controls.Add(NoActorsFound());
                    });
                else
                    flpActors.Controls.Add(NoActorsFound());
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

        private Panel NoActorsFound()
        {
            var p = new Panel()
            {
                AutoSize = true,
                Location = new Point(3, 3),
                BackColor = Color.White
            };
            var lblActorName = new Label()
            {
                Text = "No Actors Found",
                AutoSize = true,
                Location = new Point(88, 3),
                Font = new Font(FontFamily.GenericSansSerif, 13),
                Visible = true
            };

            var lblActorRole = new Label()
            {
                Text = "We Couldn't Find Any Actors/Actresses For This Title",
                AutoSize = true,
                Location = new Point(112, 29),
                Visible = true
            };
            var actorPortrait = new PictureBox()
            {
                Size = new Size(79, 119),
                Location = new Point(3, 3),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = Properties.Resources.image_not_available_png_8,
                Visible = true
            };
            p.Controls.Add(lblActorRole);
            p.Controls.Add(lblActorName);
            p.Controls.Add(actorPortrait);

            return p;
        }

        private Bitmap GetPoster(PlexObject stream)
        {
            var result = Methods.GetImageFromUrl(stream.StreamInformation.ContentThumbnailUri);
            if (result != Properties.Resources.image_not_available_png_8)
            {
                if (GlobalStaticVars.Settings.Generic.AdultContentProtection)
                {
                    if (Methods.AdultKeywordCheck(stream))
                        return ImagePixelation.Pixelate(result, 64);
                    else
                        return result;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return result;
            }
        }

        private void Metadata_Load(object sender, EventArgs e)
        {
            if (!StationaryMode)
            {
                WaitWindow.WaitWindow.Show(LoadWorker, "Parsing Metadata");
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DoExport()
        {
            //check if the form has anything loaded (stationary mode), and if there is content loaded (note that the link is checked because StreamingContent is never null,
            //but the link will always be filled if there is valid content loaded)
            if (!StationaryMode && StreamingContent.StreamInformation.Link != "")
                if (sfdExport.ShowDialog() == DialogResult.OK)
                    ImportExport.MetadataToFile(sfdExport.FileName, StreamingContent);
        }

        private void DoImport()
        {
            if (ofdImport.ShowDialog() == DialogResult.OK)
            {
                var obj = ImportExport.MetadataFromFile(ofdImport.FileName);
                StreamingContent = obj;
                flpActors.Controls.Clear();
                //set this in-case the loader doesn't find anything; that way the user still gets feedback.
                txtPlotSynopsis.Text = "Plot synopsis not provided";
                WaitWindow.WaitWindow.Show(LoadWorker, "Parsing Metadata");
            }
        }

        private void itmImport_Click(object sender, EventArgs e)
        {
            DoImport();
        }

        private void itmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itmExport_Click(object sender, EventArgs e)
        {
            DoExport();
        }

        private void itmPvs_Click(object sender, EventArgs e)
        {

        }
    }
}