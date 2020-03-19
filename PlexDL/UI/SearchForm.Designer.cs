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
            this.styleMain = new MetroSet_UI.StyleManager();
            this.btnCancelSearch = new MetroSet_UI.Controls.MetroSetEllipse();
            this.cbxSearchColumn = new MetroSet_UI.Controls.MetroSetComboBox();
            this.lblSearchField = new MetroSet_UI.Controls.MetroSetLabel();
            this.txtSearchTerm = new MetroSet_UI.Controls.MetroSetTextBox();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.ctrlMain = new MetroSet_UI.Controls.MetroSetControlBox();
            this.lblSearchRule = new MetroSet_UI.Controls.MetroSetLabel();
            this.cbxSearchRule = new MetroSet_UI.Controls.MetroSetComboBox();
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
            this.btnStartSearch.Location = new System.Drawing.Point(311, 230);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnStartSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnStartSearch.NormalTextColor = System.Drawing.Color.Black;
            this.btnStartSearch.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStartSearch.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnStartSearch.PressTextColor = System.Drawing.Color.White;
            this.btnStartSearch.Size = new System.Drawing.Size(44, 36);
            this.btnStartSearch.Style = MetroSet_UI.Design.Style.Light;
            this.btnStartSearch.StyleManager = this.styleMain;
            this.btnStartSearch.TabIndex = 5;
            this.btnStartSearch.ThemeAuthor = null;
            this.btnStartSearch.ThemeName = null;
            this.tipMain.SetToolTip(this.btnStartSearch, "Start Content Search");
            this.btnStartSearch.Click += new System.EventHandler(this.BtnStartSearch_Click);
            // 
            // styleMain
            // 
            this.styleMain.CustomTheme = "C:\\Users\\baele\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.styleMain.MetroForm = this;
            this.styleMain.Style = MetroSet_UI.Design.Style.Light;
            this.styleMain.ThemeAuthor = null;
            this.styleMain.ThemeName = null;
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
            this.btnCancelSearch.Location = new System.Drawing.Point(261, 230);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnCancelSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btnCancelSearch.NormalTextColor = System.Drawing.Color.Black;
            this.btnCancelSearch.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancelSearch.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancelSearch.PressTextColor = System.Drawing.Color.White;
            this.btnCancelSearch.Size = new System.Drawing.Size(44, 36);
            this.btnCancelSearch.Style = MetroSet_UI.Design.Style.Light;
            this.btnCancelSearch.StyleManager = this.styleMain;
            this.btnCancelSearch.TabIndex = 4;
            this.btnCancelSearch.ThemeAuthor = null;
            this.btnCancelSearch.ThemeName = null;
            this.tipMain.SetToolTip(this.btnCancelSearch, "Cancel Content Search");
            this.btnCancelSearch.Click += new System.EventHandler(this.BtnCancelSearch_Click);
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
            this.cbxSearchColumn.Location = new System.Drawing.Point(24, 192);
            this.cbxSearchColumn.Name = "cbxSearchColumn";
            this.cbxSearchColumn.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cbxSearchColumn.SelectedItemForeColor = System.Drawing.Color.White;
            this.cbxSearchColumn.Size = new System.Drawing.Size(331, 26);
            this.cbxSearchColumn.Style = MetroSet_UI.Design.Style.Light;
            this.cbxSearchColumn.StyleManager = this.styleMain;
            this.cbxSearchColumn.TabIndex = 3;
            this.cbxSearchColumn.ThemeAuthor = null;
            this.cbxSearchColumn.ThemeName = null;
            // 
            // lblSearchField
            // 
            this.lblSearchField.AutoSize = true;
            this.lblSearchField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSearchField.Location = new System.Drawing.Point(24, 172);
            this.lblSearchField.Name = "lblSearchField";
            this.lblSearchField.Size = new System.Drawing.Size(87, 17);
            this.lblSearchField.Style = MetroSet_UI.Design.Style.Light;
            this.lblSearchField.StyleManager = this.styleMain;
            this.lblSearchField.TabIndex = 2;
            this.lblSearchField.Text = "Search Field";
            this.lblSearchField.ThemeAuthor = null;
            this.lblSearchField.ThemeName = null;
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
            this.txtSearchTerm.StyleManager = this.styleMain;
            this.txtSearchTerm.TabIndex = 0;
            this.txtSearchTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearchTerm.ThemeAuthor = null;
            this.txtSearchTerm.ThemeName = null;
            this.txtSearchTerm.UseSystemPasswordChar = false;
            this.txtSearchTerm.WatermarkText = "Search Term (Limited to 64 Characters)";
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
            this.ctrlMain.StyleManager = this.styleMain;
            this.ctrlMain.TabIndex = 6;
            this.ctrlMain.Text = "Title Search";
            this.ctrlMain.ThemeAuthor = null;
            this.ctrlMain.ThemeName = null;
            // 
            // lblSearchRule
            // 
            this.lblSearchRule.AutoSize = true;
            this.lblSearchRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSearchRule.Location = new System.Drawing.Point(24, 119);
            this.lblSearchRule.Name = "lblSearchRule";
            this.lblSearchRule.Size = new System.Drawing.Size(86, 17);
            this.lblSearchRule.Style = MetroSet_UI.Design.Style.Light;
            this.lblSearchRule.StyleManager = this.styleMain;
            this.lblSearchRule.TabIndex = 7;
            this.lblSearchRule.Text = "Search Rule";
            this.lblSearchRule.ThemeAuthor = null;
            this.lblSearchRule.ThemeName = null;
            // 
            // cbxSearchRule
            // 
            this.cbxSearchRule.AllowDrop = true;
            this.cbxSearchRule.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cbxSearchRule.BackColor = System.Drawing.Color.Transparent;
            this.cbxSearchRule.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.cbxSearchRule.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.cbxSearchRule.CausesValidation = false;
            this.cbxSearchRule.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cbxSearchRule.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.cbxSearchRule.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.cbxSearchRule.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxSearchRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearchRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cbxSearchRule.FormattingEnabled = true;
            this.cbxSearchRule.ItemHeight = 20;
            this.cbxSearchRule.Items.AddRange(new object[] {
            "Contains",
            "Direct Match",
            "Begins With",
            "Ends With"});
            this.cbxSearchRule.Location = new System.Drawing.Point(24, 139);
            this.cbxSearchRule.Name = "cbxSearchRule";
            this.cbxSearchRule.SelectedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.cbxSearchRule.SelectedItemForeColor = System.Drawing.Color.White;
            this.cbxSearchRule.Size = new System.Drawing.Size(331, 26);
            this.cbxSearchRule.Style = MetroSet_UI.Design.Style.Light;
            this.cbxSearchRule.StyleManager = this.styleMain;
            this.cbxSearchRule.TabIndex = 8;
            this.cbxSearchRule.ThemeAuthor = null;
            this.cbxSearchRule.ThemeName = null;
            // 
            // SearchForm
            // 
            this.AllowResize = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(379, 284);
            this.ControlBox = false;
            this.Controls.Add(this.cbxSearchRule);
            this.Controls.Add(this.lblSearchRule);
            this.Controls.Add(this.ctrlMain);
            this.Controls.Add(this.txtSearchTerm);
            this.Controls.Add(this.lblSearchField);
            this.Controls.Add(this.cbxSearchColumn);
            this.Controls.Add(this.btnCancelSearch);
            this.Controls.Add(this.btnStartSearch);
            this.Name = "SearchForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StyleManager = this.styleMain;
            this.Text = "Search for Content";
            this.ThemeAuthor = null;
            this.ThemeName = null;
            this.Load += new System.EventHandler(this.FrmSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetEllipse btnStartSearch;
        private MetroSet_UI.Controls.MetroSetEllipse btnCancelSearch;
        private MetroSet_UI.Controls.MetroSetComboBox cbxSearchColumn;
        private MetroSet_UI.Controls.MetroSetLabel lblSearchField;
        private MetroSet_UI.Controls.MetroSetTextBox txtSearchTerm;
        private System.Windows.Forms.ToolTip tipMain;
        private MetroSet_UI.StyleManager styleMain;
        private MetroSet_UI.Controls.MetroSetControlBox ctrlMain;
        private MetroSet_UI.Controls.MetroSetLabel lblSearchRule;
        private MetroSet_UI.Controls.MetroSetComboBox cbxSearchRule;
    }
}