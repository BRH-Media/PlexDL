namespace PlexDL.UI
{
    partial class SearchForm
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
            this.btnStartSearch = new MetroSet_UI.Controls.MetroSetEllipse();
            this.btnCancelSearch = new MetroSet_UI.Controls.MetroSetEllipse();
            this.cbxSearchColumn = new MetroSet_UI.Controls.MetroSetComboBox();
            this.lblSearchField = new MetroSet_UI.Controls.MetroSetLabel();
            this.chkDirectMatch = new MetroSet_UI.Controls.MetroSetCheckBox();
            this.txtSearchTerm = new MetroSet_UI.Controls.MetroSetTextBox();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            styleMain = new MetroSet_UI.StyleManager();
            this.ctrlMain = new MetroSet_UI.Controls.MetroSetControlBox();
            this.SuspendLayout();
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.BorderThickness = 0;
            this.btnStartSearch.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStartSearch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnStartSearch.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnStartSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnStartSearch.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnStartSearch.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnStartSearch.HoverTextColor = System.Drawing.Color.White;
            this.btnStartSearch.Image = global::PlexDL.Properties.Resources.baseline_search_black_18dp;
            imageSet1.Focus = global::PlexDL.Properties.Resources.baseline_search_black_18dp_white;
            imageSet1.Idle = global::PlexDL.Properties.Resources.baseline_search_black_18dp;
            this.btnStartSearch.ImageSet = imageSet1;
            this.btnStartSearch.ImageSize = new System.Drawing.Size(28, 28);
            this.btnStartSearch.Location = new System.Drawing.Point(311, 215);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStartSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnStartSearch.NormalTextColor = System.Drawing.Color.Black;
            this.btnStartSearch.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStartSearch.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStartSearch.PressTextColor = System.Drawing.Color.White;
            this.btnStartSearch.Size = new System.Drawing.Size(44, 36);
            this.btnStartSearch.Style = MetroSet_UI.Design.Style.Light;
            this.btnStartSearch.StyleManager = styleMain;
            this.btnStartSearch.TabIndex = 4;
            this.btnStartSearch.ThemeAuthor = null;
            this.btnStartSearch.ThemeName = null;
            this.tipMain.SetToolTip(this.btnStartSearch, "Start Content Search");
            this.btnStartSearch.Click += new System.EventHandler(this.btnStartSearch_Click);
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.BorderThickness = 0;
            this.btnCancelSearch.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancelSearch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.btnCancelSearch.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.btnCancelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancelSearch.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnCancelSearch.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnCancelSearch.HoverTextColor = System.Drawing.Color.White;
            this.btnCancelSearch.Image = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            imageSet2.Focus = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp_white;
            imageSet2.Idle = global::PlexDL.Properties.Resources.baseline_cancel_black_18dp;
            this.btnCancelSearch.ImageSet = imageSet2;
            this.btnCancelSearch.ImageSize = new System.Drawing.Size(28, 28);
            this.btnCancelSearch.Location = new System.Drawing.Point(261, 215);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancelSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnCancelSearch.NormalTextColor = System.Drawing.Color.Black;
            this.btnCancelSearch.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancelSearch.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancelSearch.PressTextColor = System.Drawing.Color.White;
            this.btnCancelSearch.Size = new System.Drawing.Size(44, 36);
            this.btnCancelSearch.Style = MetroSet_UI.Design.Style.Light;
            this.btnCancelSearch.StyleManager = styleMain;
            this.btnCancelSearch.TabIndex = 3;
            this.btnCancelSearch.ThemeAuthor = null;
            this.btnCancelSearch.ThemeName = null;
            this.tipMain.SetToolTip(this.btnCancelSearch, "Cancel Content Search");
            this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
            // 
            // cbxSearchColumn
            // 
            this.cbxSearchColumn.AllowDrop = true;
            this.cbxSearchColumn.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cbxSearchColumn.BackColor = System.Drawing.Color.Transparent;
            this.cbxSearchColumn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.cbxSearchColumn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cbxSearchColumn.CausesValidation = false;
            this.cbxSearchColumn.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cbxSearchColumn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.cbxSearchColumn.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.cbxSearchColumn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxSearchColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearchColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cbxSearchColumn.FormattingEnabled = true;
            this.cbxSearchColumn.ItemHeight = 20;
            this.cbxSearchColumn.Location = new System.Drawing.Point(24, 177);
            this.cbxSearchColumn.Name = "cbxSearchColumn";
            this.cbxSearchColumn.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cbxSearchColumn.SelectedItemForeColor = System.Drawing.Color.White;
            this.cbxSearchColumn.Size = new System.Drawing.Size(331, 26);
            this.cbxSearchColumn.Style = MetroSet_UI.Design.Style.Light;
            this.cbxSearchColumn.StyleManager = styleMain;
            this.cbxSearchColumn.TabIndex = 5;
            this.cbxSearchColumn.ThemeAuthor = null;
            this.cbxSearchColumn.ThemeName = null;
            // 
            // lblSearchField
            // 
            this.lblSearchField.AutoSize = true;
            this.lblSearchField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSearchField.Location = new System.Drawing.Point(24, 157);
            this.lblSearchField.Name = "lblSearchField";
            this.lblSearchField.Size = new System.Drawing.Size(87, 17);
            this.lblSearchField.Style = MetroSet_UI.Design.Style.Light;
            this.lblSearchField.StyleManager = styleMain;
            this.lblSearchField.TabIndex = 6;
            this.lblSearchField.Text = "Search Field";
            this.lblSearchField.ThemeAuthor = null;
            this.lblSearchField.ThemeName = null;
            // 
            // chkDirectMatch
            // 
            this.chkDirectMatch.BackColor = System.Drawing.Color.Transparent;
            this.chkDirectMatch.BackgroundColor = System.Drawing.Color.White;
            this.chkDirectMatch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.chkDirectMatch.Checked = false;
            this.chkDirectMatch.CheckSignColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.chkDirectMatch.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            this.chkDirectMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDirectMatch.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.chkDirectMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chkDirectMatch.Location = new System.Drawing.Point(24, 126);
            this.chkDirectMatch.Name = "chkDirectMatch";
            this.chkDirectMatch.SignStyle = MetroSet_UI.Enums.SignStyle.Sign;
            this.chkDirectMatch.Size = new System.Drawing.Size(331, 16);
            this.chkDirectMatch.Style = MetroSet_UI.Design.Style.Light;
            this.chkDirectMatch.StyleManager = styleMain;
            this.chkDirectMatch.TabIndex = 7;
            this.chkDirectMatch.Text = "Direct Match";
            this.chkDirectMatch.ThemeAuthor = null;
            this.chkDirectMatch.ThemeName = null;
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.AutoCompleteCustomSource = null;
            this.txtSearchTerm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSearchTerm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSearchTerm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtSearchTerm.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtSearchTerm.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.txtSearchTerm.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.txtSearchTerm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchTerm.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtSearchTerm.Image = null;
            this.txtSearchTerm.Lines = null;
            this.txtSearchTerm.Location = new System.Drawing.Point(24, 90);
            this.txtSearchTerm.MaxLength = 64;
            this.txtSearchTerm.Multiline = false;
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.ReadOnly = false;
            this.txtSearchTerm.Size = new System.Drawing.Size(331, 26);
            this.txtSearchTerm.Style = MetroSet_UI.Design.Style.Light;
            this.txtSearchTerm.StyleManager = styleMain;
            this.txtSearchTerm.TabIndex = 8;
            this.txtSearchTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearchTerm.ThemeAuthor = null;
            this.txtSearchTerm.ThemeName = null;
            this.txtSearchTerm.UseSystemPasswordChar = false;
            this.txtSearchTerm.WatermarkText = "Search Term (Limited to 64 Characters)";
            // 
            // styleMain
            // 
            styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            styleMain.MetroForm = this;
            styleMain.Style = MetroSet_UI.Design.Style.Light;
            styleMain.ThemeAuthor = null;
            styleMain.ThemeName = null;
            // 
            // ctrlMain
            // 
            this.ctrlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlMain.CloseHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ctrlMain.CloseHoverForeColor = System.Drawing.Color.White;
            this.ctrlMain.CloseNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.DisabledForeColor = System.Drawing.Color.DimGray;
            this.ctrlMain.Location = new System.Drawing.Point(277, 2);
            this.ctrlMain.MaximizeBox = true;
            this.ctrlMain.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ctrlMain.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MinimizeBox = true;
            this.ctrlMain.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ctrlMain.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            this.ctrlMain.Name = "ctrlMain";
            this.ctrlMain.Size = new System.Drawing.Size(100, 25);
            this.ctrlMain.Style = MetroSet_UI.Design.Style.Light;
            this.ctrlMain.StyleManager = styleMain;
            this.ctrlMain.TabIndex = 12;
            this.ctrlMain.Text = "Title Search";
            this.ctrlMain.ThemeAuthor = null;
            this.ctrlMain.ThemeName = null;
            // 
            // SearchForm
            // 
            this.AllowResize = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(379, 271);
            this.ControlBox = false;
            this.Controls.Add(this.ctrlMain);
            this.Controls.Add(this.txtSearchTerm);
            this.Controls.Add(this.chkDirectMatch);
            this.Controls.Add(this.lblSearchField);
            this.Controls.Add(this.cbxSearchColumn);
            this.Controls.Add(this.btnCancelSearch);
            this.Controls.Add(this.btnStartSearch);
            this.Name = "SearchForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = styleMain;
            this.Text = "Search for Content";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.Load += new System.EventHandler(this.frmSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetEllipse btnStartSearch;
        private MetroSet_UI.Controls.MetroSetEllipse btnCancelSearch;
        private MetroSet_UI.Controls.MetroSetComboBox cbxSearchColumn;
        private MetroSet_UI.Controls.MetroSetLabel lblSearchField;
        private MetroSet_UI.Controls.MetroSetCheckBox chkDirectMatch;
        private MetroSet_UI.Controls.MetroSetTextBox txtSearchTerm;
        private System.Windows.Forms.ToolTip tipMain;
        private MetroSet_UI.StyleManager styleMain;
        private MetroSet_UI.Controls.MetroSetControlBox ctrlMain;
    }
}