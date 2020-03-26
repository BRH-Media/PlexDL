using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI
{
    partial class CachingMetricsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CachingMetricsUI));
            this.gbServerLists = new System.Windows.Forms.GroupBox();
            this.lblNumberServerLists = new System.Windows.Forms.Label();
            this.lblNumberServerListsValue = new System.Windows.Forms.Label();
            this.tlpServerLists = new System.Windows.Forms.TableLayoutPanel();
            this.lblSizeServerLists = new System.Windows.Forms.Label();
            this.lblSizeServerListsValue = new System.Windows.Forms.Label();
            this.gbThumbails = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSizeThumbsCachedValue = new System.Windows.Forms.Label();
            this.lblSizeThumbsCached = new System.Windows.Forms.Label();
            this.lblNumberThumbsCachedValue = new System.Windows.Forms.Label();
            this.lblNumberThumbsCached = new System.Windows.Forms.Label();
            this.gbApiXml = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSizeXmlCachedValue = new System.Windows.Forms.Label();
            this.lblSizeXmlCached = new System.Windows.Forms.Label();
            this.lblNumberXmlCachedValue = new System.Windows.Forms.Label();
            this.lblNumberXmlCached = new System.Windows.Forms.Label();
            this.gbTotal = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalSizeCachedValue = new System.Windows.Forms.Label();
            this.lblTotalSizeCached = new System.Windows.Forms.Label();
            this.lblTotalAmountCachedValue = new System.Windows.Forms.Label();
            this.lblTotalAmountCached = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbServerLists.SuspendLayout();
            this.tlpServerLists.SuspendLayout();
            this.gbThumbails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbApiXml.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbTotal.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbServerLists
            // 
            this.gbServerLists.Controls.Add(this.tlpServerLists);
            this.gbServerLists.Location = new System.Drawing.Point(13, 13);
            this.gbServerLists.Name = "gbServerLists";
            this.gbServerLists.Size = new System.Drawing.Size(364, 62);
            this.gbServerLists.TabIndex = 0;
            this.gbServerLists.TabStop = false;
            this.gbServerLists.Text = "Server Lists";
            // 
            // lblNumberServerLists
            // 
            this.lblNumberServerLists.AutoSize = true;
            this.lblNumberServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberServerLists.Location = new System.Drawing.Point(3, 0);
            this.lblNumberServerLists.Name = "lblNumberServerLists";
            this.lblNumberServerLists.Size = new System.Drawing.Size(173, 21);
            this.lblNumberServerLists.TabIndex = 1;
            this.lblNumberServerLists.Text = "Amount Cached:";
            this.lblNumberServerLists.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberServerListsValue
            // 
            this.lblNumberServerListsValue.AutoSize = true;
            this.lblNumberServerListsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberServerListsValue.Location = new System.Drawing.Point(182, 0);
            this.lblNumberServerListsValue.Name = "lblNumberServerListsValue";
            this.lblNumberServerListsValue.Size = new System.Drawing.Size(173, 21);
            this.lblNumberServerListsValue.TabIndex = 2;
            this.lblNumberServerListsValue.Text = "[NUMBER]";
            this.lblNumberServerListsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpServerLists
            // 
            this.tlpServerLists.ColumnCount = 2;
            this.tlpServerLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpServerLists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpServerLists.Controls.Add(this.lblSizeServerListsValue, 1, 1);
            this.tlpServerLists.Controls.Add(this.lblSizeServerLists, 0, 1);
            this.tlpServerLists.Controls.Add(this.lblNumberServerListsValue, 1, 0);
            this.tlpServerLists.Controls.Add(this.lblNumberServerLists, 0, 0);
            this.tlpServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpServerLists.Location = new System.Drawing.Point(3, 16);
            this.tlpServerLists.Name = "tlpServerLists";
            this.tlpServerLists.RowCount = 2;
            this.tlpServerLists.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpServerLists.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpServerLists.Size = new System.Drawing.Size(358, 43);
            this.tlpServerLists.TabIndex = 1;
            // 
            // lblSizeServerLists
            // 
            this.lblSizeServerLists.AutoSize = true;
            this.lblSizeServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeServerLists.Location = new System.Drawing.Point(3, 21);
            this.lblSizeServerLists.Name = "lblSizeServerLists";
            this.lblSizeServerLists.Size = new System.Drawing.Size(173, 22);
            this.lblSizeServerLists.TabIndex = 3;
            this.lblSizeServerLists.Text = "Size:";
            this.lblSizeServerLists.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSizeServerListsValue
            // 
            this.lblSizeServerListsValue.AutoSize = true;
            this.lblSizeServerListsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeServerListsValue.Location = new System.Drawing.Point(182, 21);
            this.lblSizeServerListsValue.Name = "lblSizeServerListsValue";
            this.lblSizeServerListsValue.Size = new System.Drawing.Size(173, 22);
            this.lblSizeServerListsValue.TabIndex = 4;
            this.lblSizeServerListsValue.Text = "[SIZE]";
            this.lblSizeServerListsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbThumbails
            // 
            this.gbThumbails.Controls.Add(this.tableLayoutPanel1);
            this.gbThumbails.Location = new System.Drawing.Point(13, 81);
            this.gbThumbails.Name = "gbThumbails";
            this.gbThumbails.Size = new System.Drawing.Size(364, 62);
            this.gbThumbails.TabIndex = 2;
            this.gbThumbails.TabStop = false;
            this.gbThumbails.Text = "Images";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblSizeThumbsCachedValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSizeThumbsCached, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblNumberThumbsCachedValue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNumberThumbsCached, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(358, 43);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblSizeThumbsCachedValue
            // 
            this.lblSizeThumbsCachedValue.AutoSize = true;
            this.lblSizeThumbsCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeThumbsCachedValue.Location = new System.Drawing.Point(182, 21);
            this.lblSizeThumbsCachedValue.Name = "lblSizeThumbsCachedValue";
            this.lblSizeThumbsCachedValue.Size = new System.Drawing.Size(173, 22);
            this.lblSizeThumbsCachedValue.TabIndex = 4;
            this.lblSizeThumbsCachedValue.Text = "[SIZE]";
            this.lblSizeThumbsCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSizeThumbsCached
            // 
            this.lblSizeThumbsCached.AutoSize = true;
            this.lblSizeThumbsCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeThumbsCached.Location = new System.Drawing.Point(3, 21);
            this.lblSizeThumbsCached.Name = "lblSizeThumbsCached";
            this.lblSizeThumbsCached.Size = new System.Drawing.Size(173, 22);
            this.lblSizeThumbsCached.TabIndex = 3;
            this.lblSizeThumbsCached.Text = "Size:";
            this.lblSizeThumbsCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberThumbsCachedValue
            // 
            this.lblNumberThumbsCachedValue.AutoSize = true;
            this.lblNumberThumbsCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberThumbsCachedValue.Location = new System.Drawing.Point(182, 0);
            this.lblNumberThumbsCachedValue.Name = "lblNumberThumbsCachedValue";
            this.lblNumberThumbsCachedValue.Size = new System.Drawing.Size(173, 21);
            this.lblNumberThumbsCachedValue.TabIndex = 2;
            this.lblNumberThumbsCachedValue.Text = "[NUMBER]";
            this.lblNumberThumbsCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberThumbsCached
            // 
            this.lblNumberThumbsCached.AutoSize = true;
            this.lblNumberThumbsCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberThumbsCached.Location = new System.Drawing.Point(3, 0);
            this.lblNumberThumbsCached.Name = "lblNumberThumbsCached";
            this.lblNumberThumbsCached.Size = new System.Drawing.Size(173, 21);
            this.lblNumberThumbsCached.TabIndex = 1;
            this.lblNumberThumbsCached.Text = "Amount Cached:";
            this.lblNumberThumbsCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbApiXml
            // 
            this.gbApiXml.Controls.Add(this.tableLayoutPanel2);
            this.gbApiXml.Location = new System.Drawing.Point(13, 149);
            this.gbApiXml.Name = "gbApiXml";
            this.gbApiXml.Size = new System.Drawing.Size(364, 62);
            this.gbApiXml.TabIndex = 3;
            this.gbApiXml.TabStop = false;
            this.gbApiXml.Text = "XML API Data";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblSizeXmlCachedValue, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblSizeXmlCached, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblNumberXmlCachedValue, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNumberXmlCached, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(358, 43);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblSizeXmlCachedValue
            // 
            this.lblSizeXmlCachedValue.AutoSize = true;
            this.lblSizeXmlCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeXmlCachedValue.Location = new System.Drawing.Point(182, 21);
            this.lblSizeXmlCachedValue.Name = "lblSizeXmlCachedValue";
            this.lblSizeXmlCachedValue.Size = new System.Drawing.Size(173, 22);
            this.lblSizeXmlCachedValue.TabIndex = 4;
            this.lblSizeXmlCachedValue.Text = "[SIZE]";
            this.lblSizeXmlCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSizeXmlCached
            // 
            this.lblSizeXmlCached.AutoSize = true;
            this.lblSizeXmlCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeXmlCached.Location = new System.Drawing.Point(3, 21);
            this.lblSizeXmlCached.Name = "lblSizeXmlCached";
            this.lblSizeXmlCached.Size = new System.Drawing.Size(173, 22);
            this.lblSizeXmlCached.TabIndex = 3;
            this.lblSizeXmlCached.Text = "Size:";
            this.lblSizeXmlCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberXmlCachedValue
            // 
            this.lblNumberXmlCachedValue.AutoSize = true;
            this.lblNumberXmlCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberXmlCachedValue.Location = new System.Drawing.Point(182, 0);
            this.lblNumberXmlCachedValue.Name = "lblNumberXmlCachedValue";
            this.lblNumberXmlCachedValue.Size = new System.Drawing.Size(173, 21);
            this.lblNumberXmlCachedValue.TabIndex = 2;
            this.lblNumberXmlCachedValue.Text = "[NUMBER]";
            this.lblNumberXmlCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberXmlCached
            // 
            this.lblNumberXmlCached.AutoSize = true;
            this.lblNumberXmlCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberXmlCached.Location = new System.Drawing.Point(3, 0);
            this.lblNumberXmlCached.Name = "lblNumberXmlCached";
            this.lblNumberXmlCached.Size = new System.Drawing.Size(173, 21);
            this.lblNumberXmlCached.TabIndex = 1;
            this.lblNumberXmlCached.Text = "Amount Cached:";
            this.lblNumberXmlCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbTotal
            // 
            this.gbTotal.Controls.Add(this.tableLayoutPanel3);
            this.gbTotal.Location = new System.Drawing.Point(13, 217);
            this.gbTotal.Name = "gbTotal";
            this.gbTotal.Size = new System.Drawing.Size(364, 62);
            this.gbTotal.TabIndex = 4;
            this.gbTotal.TabStop = false;
            this.gbTotal.Text = "Total";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblTotalSizeCachedValue, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblTotalSizeCached, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblTotalAmountCachedValue, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblTotalAmountCached, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(358, 43);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblTotalSizeCachedValue
            // 
            this.lblTotalSizeCachedValue.AutoSize = true;
            this.lblTotalSizeCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalSizeCachedValue.Location = new System.Drawing.Point(182, 21);
            this.lblTotalSizeCachedValue.Name = "lblTotalSizeCachedValue";
            this.lblTotalSizeCachedValue.Size = new System.Drawing.Size(173, 22);
            this.lblTotalSizeCachedValue.TabIndex = 4;
            this.lblTotalSizeCachedValue.Text = "[SIZE]";
            this.lblTotalSizeCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalSizeCached
            // 
            this.lblTotalSizeCached.AutoSize = true;
            this.lblTotalSizeCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalSizeCached.Location = new System.Drawing.Point(3, 21);
            this.lblTotalSizeCached.Name = "lblTotalSizeCached";
            this.lblTotalSizeCached.Size = new System.Drawing.Size(173, 22);
            this.lblTotalSizeCached.TabIndex = 3;
            this.lblTotalSizeCached.Text = "Total Size:";
            this.lblTotalSizeCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmountCachedValue
            // 
            this.lblTotalAmountCachedValue.AutoSize = true;
            this.lblTotalAmountCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmountCachedValue.Location = new System.Drawing.Point(182, 0);
            this.lblTotalAmountCachedValue.Name = "lblTotalAmountCachedValue";
            this.lblTotalAmountCachedValue.Size = new System.Drawing.Size(173, 21);
            this.lblTotalAmountCachedValue.TabIndex = 2;
            this.lblTotalAmountCachedValue.Text = "[NUMBER]";
            this.lblTotalAmountCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmountCached
            // 
            this.lblTotalAmountCached.AutoSize = true;
            this.lblTotalAmountCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmountCached.Location = new System.Drawing.Point(3, 0);
            this.lblTotalAmountCached.Name = "lblTotalAmountCached";
            this.lblTotalAmountCached.Size = new System.Drawing.Size(173, 21);
            this.lblTotalAmountCached.TabIndex = 1;
            this.lblTotalAmountCached.Text = "Total Amount Cached:";
            this.lblTotalAmountCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(13, 285);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(364, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // CachingMetricsUI
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 321);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbTotal);
            this.Controls.Add(this.gbApiXml);
            this.Controls.Add(this.gbThumbails);
            this.Controls.Add(this.gbServerLists);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CachingMetricsUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caching Metrics";
            this.Load += new System.EventHandler(this.CachingMetricsUI_Load);
            this.gbServerLists.ResumeLayout(false);
            this.tlpServerLists.ResumeLayout(false);
            this.tlpServerLists.PerformLayout();
            this.gbThumbails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbApiXml.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbTotal.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbServerLists;
        private Label lblNumberServerLists;
        private Label lblNumberServerListsValue;
        private TableLayoutPanel tlpServerLists;
        private Label lblSizeServerListsValue;
        private Label lblSizeServerLists;
        private GroupBox gbThumbails;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblSizeThumbsCachedValue;
        private Label lblSizeThumbsCached;
        private Label lblNumberThumbsCachedValue;
        private Label lblNumberThumbsCached;
        private GroupBox gbApiXml;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblSizeXmlCachedValue;
        private Label lblSizeXmlCached;
        private Label lblNumberXmlCachedValue;
        private Label lblNumberXmlCached;
        private GroupBox gbTotal;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblTotalSizeCachedValue;
        private Label lblTotalSizeCached;
        private Label lblTotalAmountCachedValue;
        private Label lblTotalAmountCached;
        private Button btnOK;
    }
}