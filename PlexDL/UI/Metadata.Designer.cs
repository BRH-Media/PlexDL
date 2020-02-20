namespace PlexDL.UI
{
    partial class Metadata
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            MetroSet_UI.Extensions.ImageSet imageSet1 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet2 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet3 = new MetroSet_UI.Extensions.ImageSet();
            MetroSet_UI.Extensions.ImageSet imageSet4 = new MetroSet_UI.Extensions.ImageSet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Metadata));
            this.flpActors = new System.Windows.Forms.FlowLayoutPanel();
            this.lblActors = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblMetadata = new System.Windows.Forms.Label();
            this.lblSize = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblSizeValue = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblRuntime = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblRuntimeValue = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblGenre = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblGenreValue = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblResolutionValue = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblResolution = new MetroSet_UI.Controls.MetroSetLabel();
            this.btnExit = new MetroSet_UI.Controls.MetroSetEllipse();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.btnExportMetadata = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnImport = new MetroSet_UI.Controls.MetroSetEllipse();
            this.ofdMetadata = new System.Windows.Forms.OpenFileDialog();
            this.t1 = new System.Windows.Forms.Timer(this.components);
            this.lblContainerValue = new MetroSet_UI.Controls.MetroSetLabel();
            this.lblContainer = new MetroSet_UI.Controls.MetroSetLabel();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.btnStreamInVLC = new MetroSet_UI.Controls.MetroSetEllipse();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // flpActors
            // 
            this.flpActors.AutoScroll = true;
            this.flpActors.BackColor = System.Drawing.Color.White;
            this.flpActors.Location = new System.Drawing.Point(232, 100);
            this.flpActors.Name = "flpActors";
            this.flpActors.Size = new System.Drawing.Size(652, 341);
            this.flpActors.TabIndex = 1;
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblActors.Location = new System.Drawing.Point(528, 98);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(59, 18);
            this.lblActors.Style = MetroSet_UI.Design.Style.Light;
            this.lblActors.StyleManager = null;
            this.lblActors.TabIndex = 2;
            this.lblActors.Text = "Starring";
            this.lblActors.ThemeAuthor = "Narwin";
            this.lblActors.ThemeName = "MetroLite";
            // 
            // lblMetadata
            // 
            this.lblMetadata.AutoSize = true;
            this.lblMetadata.BackColor = System.Drawing.Color.White;
            this.lblMetadata.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetadata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMetadata.Location = new System.Drawing.Point(12, 348);
            this.lblMetadata.Name = "lblMetadata";
            this.lblMetadata.Size = new System.Drawing.Size(77, 18);
            this.lblMetadata.TabIndex = 3;
            this.lblMetadata.Text = "Metadata";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSize.Location = new System.Drawing.Point(12, 367);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(41, 18);
            this.lblSize.Style = MetroSet_UI.Design.Style.Light;
            this.lblSize.StyleManager = null;
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Size:";
            this.lblSize.ThemeAuthor = "Narwin";
            this.lblSize.ThemeName = "MetroLite";
            // 
            // lblSizeValue
            // 
            this.lblSizeValue.AutoSize = true;
            this.lblSizeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSizeValue.Location = new System.Drawing.Point(104, 367);
            this.lblSizeValue.Name = "lblSizeValue";
            this.lblSizeValue.Size = new System.Drawing.Size(71, 18);
            this.lblSizeValue.Style = MetroSet_UI.Design.Style.Light;
            this.lblSizeValue.StyleManager = null;
            this.lblSizeValue.TabIndex = 5;
            this.lblSizeValue.Text = "Unknown";
            this.lblSizeValue.ThemeAuthor = "Narwin";
            this.lblSizeValue.ThemeName = "MetroLite";
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblRuntime.Location = new System.Drawing.Point(12, 386);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(67, 18);
            this.lblRuntime.Style = MetroSet_UI.Design.Style.Light;
            this.lblRuntime.StyleManager = null;
            this.lblRuntime.TabIndex = 6;
            this.lblRuntime.Text = "Runtime:";
            this.lblRuntime.ThemeAuthor = "Narwin";
            this.lblRuntime.ThemeName = "MetroLite";
            // 
            // lblRuntimeValue
            // 
            this.lblRuntimeValue.AutoSize = true;
            this.lblRuntimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblRuntimeValue.Location = new System.Drawing.Point(104, 386);
            this.lblRuntimeValue.Name = "lblRuntimeValue";
            this.lblRuntimeValue.Size = new System.Drawing.Size(71, 18);
            this.lblRuntimeValue.Style = MetroSet_UI.Design.Style.Light;
            this.lblRuntimeValue.StyleManager = null;
            this.lblRuntimeValue.TabIndex = 7;
            this.lblRuntimeValue.Text = "Unknown";
            this.lblRuntimeValue.ThemeAuthor = "Narwin";
            this.lblRuntimeValue.ThemeName = "MetroLite";
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblGenre.Location = new System.Drawing.Point(12, 443);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(53, 18);
            this.lblGenre.Style = MetroSet_UI.Design.Style.Light;
            this.lblGenre.StyleManager = null;
            this.lblGenre.TabIndex = 8;
            this.lblGenre.Text = "Genre:";
            this.lblGenre.ThemeAuthor = "Narwin";
            this.lblGenre.ThemeName = "MetroLite";
            // 
            // lblGenreValue
            // 
            this.lblGenreValue.AutoSize = true;
            this.lblGenreValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblGenreValue.Location = new System.Drawing.Point(104, 443);
            this.lblGenreValue.Name = "lblGenreValue";
            this.lblGenreValue.Size = new System.Drawing.Size(71, 18);
            this.lblGenreValue.Style = MetroSet_UI.Design.Style.Light;
            this.lblGenreValue.StyleManager = null;
            this.lblGenreValue.TabIndex = 9;
            this.lblGenreValue.Text = "Unknown";
            this.lblGenreValue.ThemeAuthor = "Narwin";
            this.lblGenreValue.ThemeName = "MetroLite";
            // 
            // lblResolutionValue
            // 
            this.lblResolutionValue.AutoSize = true;
            this.lblResolutionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblResolutionValue.Location = new System.Drawing.Point(104, 405);
            this.lblResolutionValue.Name = "lblResolutionValue";
            this.lblResolutionValue.Size = new System.Drawing.Size(71, 18);
            this.lblResolutionValue.Style = MetroSet_UI.Design.Style.Light;
            this.lblResolutionValue.StyleManager = null;
            this.lblResolutionValue.TabIndex = 11;
            this.lblResolutionValue.Text = "Unknown";
            this.lblResolutionValue.ThemeAuthor = "Narwin";
            this.lblResolutionValue.ThemeName = "MetroLite";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblResolution.Location = new System.Drawing.Point(12, 405);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(83, 18);
            this.lblResolution.Style = MetroSet_UI.Design.Style.Light;
            this.lblResolution.StyleManager = null;
            this.lblResolution.TabIndex = 10;
            this.lblResolution.Text = "Resolution:";
            this.lblResolution.ThemeAuthor = "Narwin";
            this.lblResolution.ThemeName = "MetroLite";
            // 
            // btnExit
            // 
            this.btnExit.BorderThickness = 0;
            this.btnExit.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExit.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnExit.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExit.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExit.HoverTextColor = System.Drawing.Color.White;
            this.btnExit.Image = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            imageSet1.Focus = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp_white;
            imageSet1.Idle = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnExit.ImageSet = imageSet1;
            this.btnExit.ImageSize = new System.Drawing.Size(28, 28);
            this.btnExit.Location = new System.Drawing.Point(836, 470);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExit.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnExit.NormalTextColor = System.Drawing.Color.Black;
            this.btnExit.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExit.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExit.PressTextColor = System.Drawing.Color.White;
            this.btnExit.Size = new System.Drawing.Size(44, 36);
            this.btnExit.Style = MetroSet_UI.Design.Style.Light;
            this.btnExit.StyleManager = null;
            this.btnExit.TabIndex = 1;
            this.btnExit.ThemeAuthor = "Narwin";
            this.btnExit.ThemeName = "MetroLite";
            this.tipMain.SetToolTip(this.btnExit, "Close Metadata");
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "pmxml";
            this.sfdExport.Filter = "PlexMovie XML|*.pmxml";
            this.sfdExport.Title = "Export PlexMovie Metadata";
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.picPoster.BackgroundImage = global::PlexDL.Properties.Resources.image_not_available_png_8;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPoster.Location = new System.Drawing.Point(12, 75);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(167, 250);
            this.picPoster.TabIndex = 12;
            this.picPoster.TabStop = false;
            // 
            // btnExportMetadata
            // 
            this.btnExportMetadata.BorderThickness = 0;
            this.btnExportMetadata.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExportMetadata.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnExportMetadata.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnExportMetadata.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExportMetadata.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExportMetadata.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnExportMetadata.HoverTextColor = System.Drawing.Color.White;
            this.btnExportMetadata.Image = global::PlexDL.Properties.Resources.baseline_save_black_18dp;
            imageSet2.Focus = global::PlexDL.Properties.Resources.baseline_save_black_18dp_white;
            imageSet2.Idle = global::PlexDL.Properties.Resources.baseline_save_black_18dp;
            this.btnExportMetadata.ImageSet = imageSet2;
            this.btnExportMetadata.ImageSize = new System.Drawing.Size(28, 28);
            this.btnExportMetadata.Location = new System.Drawing.Point(15, 470);
            this.btnExportMetadata.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExportMetadata.Name = "btnExportMetadata";
            this.btnExportMetadata.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnExportMetadata.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnExportMetadata.NormalTextColor = System.Drawing.Color.Black;
            this.btnExportMetadata.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExportMetadata.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnExportMetadata.PressTextColor = System.Drawing.Color.White;
            this.btnExportMetadata.Size = new System.Drawing.Size(44, 36);
            this.btnExportMetadata.Style = MetroSet_UI.Design.Style.Light;
            this.btnExportMetadata.StyleManager = null;
            this.btnExportMetadata.TabIndex = 13;
            this.btnExportMetadata.ThemeAuthor = "Narwin";
            this.btnExportMetadata.ThemeName = "MetroLite";
            this.tipMain.SetToolTip(this.btnExportMetadata, "Export Metadata");
            this.btnExportMetadata.Click += new System.EventHandler(this.btnExportMetadata_Click);
            // 
            // btnImport
            // 
            this.btnImport.BorderThickness = 0;
            this.btnImport.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnImport.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnImport.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnImport.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnImport.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnImport.HoverTextColor = System.Drawing.Color.White;
            this.btnImport.Image = global::PlexDL.Properties.Resources.baseline_folder_open_black_18dp;
            imageSet3.Focus = global::PlexDL.Properties.Resources.baseline_folder_open_black_18dp_white;
            imageSet3.Idle = global::PlexDL.Properties.Resources.baseline_folder_open_black_18dp;
            this.btnImport.ImageSet = imageSet3;
            this.btnImport.ImageSize = new System.Drawing.Size(28, 28);
            this.btnImport.Location = new System.Drawing.Point(67, 470);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnImport.Name = "btnImport";
            this.btnImport.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnImport.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnImport.NormalTextColor = System.Drawing.Color.Black;
            this.btnImport.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnImport.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnImport.PressTextColor = System.Drawing.Color.White;
            this.btnImport.Size = new System.Drawing.Size(44, 36);
            this.btnImport.Style = MetroSet_UI.Design.Style.Light;
            this.btnImport.StyleManager = null;
            this.btnImport.TabIndex = 14;
            this.btnImport.ThemeAuthor = "Narwin";
            this.btnImport.ThemeName = "MetroLite";
            this.tipMain.SetToolTip(this.btnImport, "Import Metadata");
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // ofdMetadata
            // 
            this.ofdMetadata.Filter = "PlexMovie XML|*.pmxml";
            this.ofdMetadata.Title = "Import PlexMovie Metadata";
            // 
            // t1
            // 
            this.t1.Interval = 10;
            // 
            // lblContainerValue
            // 
            this.lblContainerValue.AutoSize = true;
            this.lblContainerValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblContainerValue.Location = new System.Drawing.Point(104, 424);
            this.lblContainerValue.Name = "lblContainerValue";
            this.lblContainerValue.Size = new System.Drawing.Size(71, 18);
            this.lblContainerValue.Style = MetroSet_UI.Design.Style.Light;
            this.lblContainerValue.StyleManager = null;
            this.lblContainerValue.TabIndex = 16;
            this.lblContainerValue.Text = "Unknown";
            this.lblContainerValue.ThemeAuthor = "Narwin";
            this.lblContainerValue.ThemeName = "MetroLite";
            // 
            // lblContainer
            // 
            this.lblContainer.AutoSize = true;
            this.lblContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblContainer.Location = new System.Drawing.Point(12, 424);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(76, 18);
            this.lblContainer.Style = MetroSet_UI.Design.Style.Light;
            this.lblContainer.StyleManager = null;
            this.lblContainer.TabIndex = 15;
            this.lblContainer.Text = "Container:";
            this.lblContainer.ThemeAuthor = "Narwin";
            this.lblContainer.ThemeName = "MetroLite";
            // 
            // btnStreamInVLC
            // 
            this.btnStreamInVLC.BorderThickness = 0;
            this.btnStreamInVLC.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStreamInVLC.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnStreamInVLC.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnStreamInVLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnStreamInVLC.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnStreamInVLC.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnStreamInVLC.HoverTextColor = System.Drawing.Color.White;
            this.btnStreamInVLC.Image = global::PlexDL.Properties.Resources.baseline_rss_feed_black_18dp;
            imageSet4.Focus = global::PlexDL.Properties.Resources.baseline_rss_feed_black_18dp_white;
            imageSet4.Idle = global::PlexDL.Properties.Resources.baseline_rss_feed_black_18dp;
            this.btnStreamInVLC.ImageSet = imageSet4;
            this.btnStreamInVLC.ImageSize = new System.Drawing.Size(24, 24);
            this.btnStreamInVLC.Location = new System.Drawing.Point(119, 470);
            this.btnStreamInVLC.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStreamInVLC.Name = "btnStreamInVLC";
            this.btnStreamInVLC.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStreamInVLC.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnStreamInVLC.NormalTextColor = System.Drawing.Color.Black;
            this.btnStreamInVLC.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStreamInVLC.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStreamInVLC.PressTextColor = System.Drawing.Color.White;
            this.btnStreamInVLC.Size = new System.Drawing.Size(44, 36);
            this.btnStreamInVLC.Style = MetroSet_UI.Design.Style.Light;
            this.btnStreamInVLC.StyleManager = null;
            this.btnStreamInVLC.TabIndex = 17;
            this.btnStreamInVLC.ThemeAuthor = "Narwin";
            this.btnStreamInVLC.ThemeName = "MetroLite";
            this.tipMain.SetToolTip(this.btnStreamInVLC, "Stream in VLC");
            this.btnStreamInVLC.Visible = false;
            this.btnStreamInVLC.Click += new System.EventHandler(this.btnStreamInVLC_Click);
            // 
            // Metadata
            // 
            this.AllowResize = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 515);
            this.Controls.Add(this.btnStreamInVLC);
            this.Controls.Add(this.lblContainerValue);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExportMetadata);
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblResolutionValue);
            this.Controls.Add(this.lblResolution);
            this.Controls.Add(this.lblGenreValue);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblRuntimeValue);
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.lblSizeValue);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblMetadata);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.flpActors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Metadata";
            this.Padding = new System.Windows.Forms.Padding(12, 90, 12, 12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metadata";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Metadata_FormClosing);
            this.Load += new System.EventHandler(this.frmTitleInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpActors;
        private MetroSet_UI.Controls.MetroSetLabel lblActors;
        private System.Windows.Forms.Label lblMetadata;
        private MetroSet_UI.Controls.MetroSetLabel lblSize;
        private MetroSet_UI.Controls.MetroSetLabel lblSizeValue;
        private MetroSet_UI.Controls.MetroSetLabel lblRuntime;
        private MetroSet_UI.Controls.MetroSetLabel lblRuntimeValue;
        private MetroSet_UI.Controls.MetroSetLabel lblGenre;
        private MetroSet_UI.Controls.MetroSetLabel lblGenreValue;
        private MetroSet_UI.Controls.MetroSetLabel lblResolutionValue;
        private MetroSet_UI.Controls.MetroSetLabel lblResolution;
        private System.Windows.Forms.PictureBox picPoster;
        private MetroSet_UI.Controls.MetroSetEllipse btnExit;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private MetroSet_UI.Controls.MetroSetEllipse btnExportMetadata;
        private MetroSet_UI.Controls.MetroSetEllipse btnImport;
        private System.Windows.Forms.OpenFileDialog ofdMetadata;
        private System.Windows.Forms.Timer t1;
        private MetroSet_UI.Controls.MetroSetLabel lblContainerValue;
        private MetroSet_UI.Controls.MetroSetLabel lblContainer;
        private System.Windows.Forms.ToolTip tipMain;
        private MetroSet_UI.Controls.MetroSetEllipse btnStreamInVLC;
    }
}