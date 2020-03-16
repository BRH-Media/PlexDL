using MetroSet_UI.Controls;
using MetroSet_UI.Forms;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.PlayerLaunchers;
using PlexDL.Common.Renderers;
using PlexDL.Common.Structures.Plex;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class Metadata : MetroSetForm
    {
        public PlexObject StreamingContent { get; set; } = new PlexObject();
        public bool StationaryMode { get; set; } = false;

        public Metadata()
        {
            InitializeComponent();
            this.styleMain = GlobalStaticVars.GlobalStyle;
            this.styleMain.MetroForm = this;
        }

        private void LoadWorker(object sender, PlexDL.WaitWindow.WaitWindowEventArgs e)
        {
            //fill the genre infobox
            if (!string.IsNullOrEmpty(StreamingContent.ContentGenre))
            {
                if (lblGenreValue.InvokeRequired)
                {
                    lblGenreValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblGenreValue.Text = StreamingContent.ContentGenre;
                    });
                }
                else
                {
                    lblGenreValue.Text = StreamingContent.ContentGenre;
                }
            }

            //fill the runtime duration infobox
            if (StreamingContent.StreamInformation.ContentDuration > 0)
            {
                if (lblRuntimeValue.InvokeRequired)
                {
                    lblRuntimeValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblRuntimeValue.Text = Common.Methods.CalculateTime(StreamingContent.StreamInformation.ContentDuration);
                    });
                }
                else
                {
                    lblRuntimeValue.Text = Common.Methods.CalculateTime(StreamingContent.StreamInformation.ContentDuration);
                }
            }

            //fill the size infobox
            if (StreamingContent.StreamInformation.ByteLength > 0)
            {
                if (lblSizeValue.InvokeRequired)
                {
                    lblSizeValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblSizeValue.Text = Common.Methods.FormatBytes(StreamingContent.StreamInformation.ByteLength);
                    });
                }
                else
                {
                    lblSizeValue.Text = Common.Methods.FormatBytes(StreamingContent.StreamInformation.ByteLength);
                }
            }

            //fill the resolution infobox
            if (StreamingContent.StreamResolution.Height > 0)
            {
                if (lblResolutionValue.InvokeRequired)
                {
                    lblResolutionValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblResolutionValue.Text = StreamingContent.StreamResolution.ResolutionString();
                    });
                }
                else
                {
                    lblResolutionValue.Text = StreamingContent.StreamResolution.ResolutionString();
                }
            }

            //fill the poster picturebox
            if (!string.IsNullOrEmpty(StreamingContent.StreamInformation.ContentThumbnailUri))
            {
                if (picPoster.InvokeRequired)
                {
                    picPoster.BeginInvoke((MethodInvoker)delegate
                    {
                        picPoster.BackgroundImage = GetPoster(StreamingContent);
                    });
                }
                else
                {
                    picPoster.BackgroundImage = GetPoster(StreamingContent);
                }
            }

            //fill the container infobox
            if (!string.IsNullOrEmpty(StreamingContent.StreamInformation.Container))
            {
                if (lblContainerValue.InvokeRequired)
                {
                    lblContainerValue.BeginInvoke((MethodInvoker)delegate
                    {
                        lblContainerValue.Text = StreamingContent.StreamInformation.Container;
                    });
                }
                else
                {
                    lblContainerValue.Text = StreamingContent.StreamInformation.Container;
                }
            }

            //fill the plot synopsis infobox
            if (!string.IsNullOrEmpty(StreamingContent.Synopsis))
            {
                if (txtPlotSynopsis.InvokeRequired)
                {
                    txtPlotSynopsis.BeginInvoke((MethodInvoker)delegate
                    {
                        txtPlotSynopsis.Text = StreamingContent.Synopsis;
                    });
                }
                else
                {
                    txtPlotSynopsis.Text = StreamingContent.Synopsis;
                }
            }

            //Clear all previous actors (maybe there's a dud panel in place?)
            if (flpActors.Controls.Count > 0)
            {
                if (flpActors.InvokeRequired)
                {
                    flpActors.BeginInvoke((MethodInvoker)delegate
                    {
                        flpActors.Controls.Clear();
                    });
                }
                else
                {
                    flpActors.Controls.Clear();
                }
            }

            if (StreamingContent.Actors.Count > 0)
            {
                //start filling the actors panel from the real data
                foreach (PlexActor a in StreamingContent.Actors)
                {
                    Panel p = new Panel()
                    {
                        AutoSize = true,
                        Location = new Point(3, 3),
                        BackColor = Color.White
                    };
                    Label lblActorName = new Label()
                    {
                        Text = a.ActorName,
                        AutoSize = true,
                        Location = new Point(88, 3),
                        Font = new Font(FontFamily.GenericSansSerif, 13),
                        Visible = true
                    };

                    MetroSetLabel lblActorRole = new MetroSetLabel()
                    {
                        Text = a.ActorRole,
                        AutoSize = true,
                        Location = new Point(112, 29),
                        Visible = true
                    };
                    PictureBox actorPortrait = new PictureBox()
                    {
                        Size = new Size(79, 119),
                        Location = new Point(3, 3),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        BackgroundImage = Common.Methods.GetImageFromUrl(a.ThumbnailUri),
                        Visible = true
                    };
                    p.Controls.Add(lblActorRole);
                    p.Controls.Add(lblActorName);
                    p.Controls.Add(actorPortrait);

                    if (flpActors.InvokeRequired)
                    {
                        flpActors.BeginInvoke((MethodInvoker)delegate
                        {
                            flpActors.Controls.Add(p);
                        });
                    }
                    else
                    {
                        flpActors.Controls.Add(p);
                    }
                }
            }
            else
            {
                //no actors, so tell the user with a dud panel
                if (flpActors.InvokeRequired)
                {
                    flpActors.BeginInvoke((MethodInvoker)delegate
                    {
                        flpActors.Controls.Add(NoActorsFound());
                    });
                }
                else
                {
                    flpActors.Controls.Add(NoActorsFound());
                }
            }
            //apply content title and enable VLC streaming
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    btnStreamInVLC.Visible = true;
                    this.Text = StreamingContent.StreamInformation.ContentTitle;
                    this.Refresh();
                });
            }
            else
            {
                btnStreamInVLC.Visible = true;
                this.Text = StreamingContent.StreamInformation.ContentTitle;
                this.Refresh();
            }
        }

        private Panel NoActorsFound()
        {
            Panel p = new Panel()
            {
                AutoSize = true,
                Location = new Point(3, 3),
                BackColor = Color.White
            };
            Label lblActorName = new Label()
            {
                Text = "No Actors Found",
                AutoSize = true,
                Location = new Point(88, 3),
                Font = new Font(FontFamily.GenericSansSerif, 13),
                Visible = true
            };

            MetroSetLabel lblActorRole = new MetroSetLabel()
            {
                Text = "We Couldn't Find Any Actors/Actresses For This Title",
                AutoSize = true,
                Location = new Point(112, 29),
                Visible = true
            };
            PictureBox actorPortrait = new PictureBox()
            {
                Size = new Size(79, 119),
                Location = new Point(3, 3),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = PlexDL.Properties.Resources.image_not_available_png_8,
                Visible = true
            };
            p.Controls.Add(lblActorRole);
            p.Controls.Add(lblActorName);
            p.Controls.Add(actorPortrait);

            return p;
        }

        private Bitmap GetPoster(PlexObject stream)
        {
            Bitmap result = Common.Methods.GetImageFromUrl(stream.StreamInformation.ContentThumbnailUri);
            if (result != PlexDL.Properties.Resources.image_not_available_png_8)
            {
                if (Home.settings.Generic.AdultContentProtection)
                {
                    if (Methods.AdultKeywordCheck(stream))
                    {
                        return ImagePixelation.Pixelate(result, 64);
                    }
                    else
                        return result;
                }
                else
                    return result;
            }
            else
                return result;
        }

        private void Metadata_Load(object sender, EventArgs e)
        {
            if (!StationaryMode)
                PlexDL.WaitWindow.WaitWindow.Show(LoadWorker, "Parsing Metadata");
            else
            {
                flpActors.Controls.Clear();
                flpActors.Controls.Add(NoActorsFound());
                if (Home.settings.Generic.AnimationSpeed > 0)
                {
                    Opacity = 0;      //first the opacity is 0
                    t1 = new Timer
                    {
                        Interval = Home.settings.Generic.AnimationSpeed  //we'll increase the opacity every 10ms
                    };
                    t1.Tick += new EventHandler(FadeIn);  //this calls the function that changes opacity
                    t1.Start();
                }
            }
        }

        private void Metadata_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Home.settings.Generic.AnimationSpeed > 0)
            {
                e.Cancel = true;
                t1 = new Timer
                {
                    Interval = Home.settings.Generic.AnimationSpeed
                };
                t1.Tick += new EventHandler(FadeOut);  //this calls the fade out function
                t1.Start();

                if (Opacity == 0)
                {
                    //resume the event - the program can be closed
                    e.Cancel = false;
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }

        private void FadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void BtnExportMetadata_Click(object sender, EventArgs e)
        {
            if (sfdExport.ShowDialog() == DialogResult.OK)
            {
                ImportExport.MetadataToFile(sfdExport.FileName, StreamingContent);
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (ofdMetadata.ShowDialog() == DialogResult.OK)
            {
                PlexObject obj = ImportExport.MetadataFromFile(ofdMetadata.FileName);
                StreamingContent = obj;
                flpActors.Controls.Clear();
                txtPlotSynopsis.Text = "Plot synopsis not provided";
                PlexDL.WaitWindow.WaitWindow.Show(LoadWorker, "Parsing Metadata");
            }
        }

        private void BtnStreamInVLC_Click(object sender, EventArgs e)
        {
            VLCLauncher.LaunchVLC(StreamingContent);
        }
    }
}