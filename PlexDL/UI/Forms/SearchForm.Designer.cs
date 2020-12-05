using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI.Forms
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnStartSearch = new System.Windows.Forms.Button();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbSearchTerm = new System.Windows.Forms.GroupBox();
            this.gbSearchRule = new System.Windows.Forms.GroupBox();
            this.cbxSearchRule = new System.Windows.Forms.ComboBox();
            this.gbSearchColumn = new System.Windows.Forms.GroupBox();
            this.cbxSearchColumn = new System.Windows.Forms.ComboBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbSearchTerm.SuspendLayout();
            this.gbSearchRule.SuspendLayout();
            this.gbSearchColumn.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStartSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartSearch.Location = new System.Drawing.Point(197, 155);
            this.btnStartSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.Size = new System.Drawing.Size(191, 26);
            this.btnStartSearch.TabIndex = 0;
            this.btnStartSearch.Text = "Search";
            this.tipMain.SetToolTip(this.btnStartSearch, "Start Content Search");
            this.btnStartSearch.Click += new System.EventHandler(this.BtnStartSearch_Click);
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchTerm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchTerm.Location = new System.Drawing.Point(3, 16);
            this.txtSearchTerm.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchTerm.MaxLength = 64;
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(378, 23);
            this.txtSearchTerm.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(2, 155);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(191, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.tipMain.SetToolTip(this.btnCancel, "Start Content Search");
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // gbSearchTerm
            // 
            this.tlpMain.SetColumnSpan(this.gbSearchTerm, 2);
            this.gbSearchTerm.Controls.Add(this.txtSearchTerm);
            this.gbSearchTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSearchTerm.Location = new System.Drawing.Point(3, 3);
            this.gbSearchTerm.Name = "gbSearchTerm";
            this.gbSearchTerm.Size = new System.Drawing.Size(384, 45);
            this.gbSearchTerm.TabIndex = 9;
            this.gbSearchTerm.TabStop = false;
            this.gbSearchTerm.Text = "Search Term";
            // 
            // gbSearchRule
            // 
            this.tlpMain.SetColumnSpan(this.gbSearchRule, 2);
            this.gbSearchRule.Controls.Add(this.cbxSearchRule);
            this.gbSearchRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSearchRule.Location = new System.Drawing.Point(3, 54);
            this.gbSearchRule.Name = "gbSearchRule";
            this.gbSearchRule.Size = new System.Drawing.Size(384, 45);
            this.gbSearchRule.TabIndex = 10;
            this.gbSearchRule.TabStop = false;
            this.gbSearchRule.Text = "Search Rule";
            // 
            // cbxSearchRule
            // 
            this.cbxSearchRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxSearchRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearchRule.FormattingEnabled = true;
            this.cbxSearchRule.Items.AddRange(new object[] {
            "Contains",
            "Equals",
            "Begins With",
            "Ends With"});
            this.cbxSearchRule.Location = new System.Drawing.Point(3, 16);
            this.cbxSearchRule.Name = "cbxSearchRule";
            this.cbxSearchRule.Size = new System.Drawing.Size(378, 21);
            this.cbxSearchRule.TabIndex = 0;
            // 
            // gbSearchColumn
            // 
            this.tlpMain.SetColumnSpan(this.gbSearchColumn, 2);
            this.gbSearchColumn.Controls.Add(this.cbxSearchColumn);
            this.gbSearchColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSearchColumn.Location = new System.Drawing.Point(3, 105);
            this.gbSearchColumn.Name = "gbSearchColumn";
            this.gbSearchColumn.Size = new System.Drawing.Size(384, 45);
            this.gbSearchColumn.TabIndex = 11;
            this.gbSearchColumn.TabStop = false;
            this.gbSearchColumn.Text = "Search Column";
            // 
            // cbxSearchColumn
            // 
            this.cbxSearchColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxSearchColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearchColumn.FormattingEnabled = true;
            this.cbxSearchColumn.Location = new System.Drawing.Point(3, 16);
            this.cbxSearchColumn.Name = "cbxSearchColumn";
            this.cbxSearchColumn.Size = new System.Drawing.Size(378, 21);
            this.cbxSearchColumn.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.btnCancel, 0, 3);
            this.tlpMain.Controls.Add(this.gbSearchColumn, 0, 2);
            this.tlpMain.Controls.Add(this.btnStartSearch, 1, 3);
            this.tlpMain.Controls.Add(this.gbSearchRule, 0, 1);
            this.tlpMain.Controls.Add(this.gbSearchTerm, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(390, 183);
            this.tlpMain.TabIndex = 13;
            // 
            // SearchForm
            // 
            this.AcceptButton = this.btnStartSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(390, 183);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SearchForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search for Content";
            this.Load += new System.EventHandler(this.FrmSearch_Load);
            this.gbSearchTerm.ResumeLayout(false);
            this.gbSearchTerm.PerformLayout();
            this.gbSearchRule.ResumeLayout(false);
            this.gbSearchColumn.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnStartSearch;
        private TextBox txtSearchTerm;
        private ToolTip tipMain;
        private GroupBox gbSearchTerm;
        private GroupBox gbSearchRule;
        private GroupBox gbSearchColumn;
        private Button btnCancel;
        private ComboBox cbxSearchRule;
        private ComboBox cbxSearchColumn;
        private TableLayoutPanel tlpMain;
    }
}