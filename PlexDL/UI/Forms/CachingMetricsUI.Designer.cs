using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI.Forms
{
    partial class CachingMetricsUi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CachingMetricsUi));
            this.gbServerLists = new System.Windows.Forms.GroupBox();
            this.tlpServerLists = new System.Windows.Forms.TableLayoutPanel();
            this.lblSizeServerListsValue = new System.Windows.Forms.Label();
            this.lblSizeServerLists = new System.Windows.Forms.Label();
            this.lblNumberServerListsValue = new System.Windows.Forms.Label();
            this.lblNumberServerLists = new System.Windows.Forms.Label();
            this.gbThumbails = new System.Windows.Forms.GroupBox();
            this.tlpImages = new System.Windows.Forms.TableLayoutPanel();
            this.lblSizeThumbsCachedValue = new System.Windows.Forms.Label();
            this.lblSizeThumbsCached = new System.Windows.Forms.Label();
            this.lblNumberThumbsCachedValue = new System.Windows.Forms.Label();
            this.lblNumberThumbsCached = new System.Windows.Forms.Label();
            this.gbApiXml = new System.Windows.Forms.GroupBox();
            this.tlpXmlApi = new System.Windows.Forms.TableLayoutPanel();
            this.lblSizeXmlCachedValue = new System.Windows.Forms.Label();
            this.lblSizeXmlCached = new System.Windows.Forms.Label();
            this.lblNumberXmlCachedValue = new System.Windows.Forms.Label();
            this.lblNumberXmlCached = new System.Windows.Forms.Label();
            this.gbTotal = new System.Windows.Forms.GroupBox();
            this.tlpTotal = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalSizeCachedValue = new System.Windows.Forms.Label();
            this.lblTotalSizeCached = new System.Windows.Forms.Label();
            this.lblTotalAmountCachedValue = new System.Windows.Forms.Label();
            this.lblTotalAmountCached = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbCachingDir = new System.Windows.Forms.GroupBox();
            this.tlpCachingDir = new System.Windows.Forms.TableLayoutPanel();
            this.lblCachingDir = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbServerLists.SuspendLayout();
            this.tlpServerLists.SuspendLayout();
            this.gbThumbails.SuspendLayout();
            this.tlpImages.SuspendLayout();
            this.gbApiXml.SuspendLayout();
            this.tlpXmlApi.SuspendLayout();
            this.gbTotal.SuspendLayout();
            this.tlpTotal.SuspendLayout();
            this.gbCachingDir.SuspendLayout();
            this.tlpCachingDir.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbServerLists
            // 
            this.tlpMain.SetColumnSpan(this.gbServerLists, 2);
            this.gbServerLists.Controls.Add(this.tlpServerLists);
            this.gbServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbServerLists.Location = new System.Drawing.Point(13, 79);
            this.gbServerLists.Name = "gbServerLists";
            this.gbServerLists.Size = new System.Drawing.Size(366, 60);
            this.gbServerLists.TabIndex = 0;
            this.gbServerLists.TabStop = false;
            this.gbServerLists.Text = "Server Lists";
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
            this.tlpServerLists.Size = new System.Drawing.Size(360, 41);
            this.tlpServerLists.TabIndex = 1;
            // 
            // lblSizeServerListsValue
            // 
            this.lblSizeServerListsValue.AutoSize = true;
            this.lblSizeServerListsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeServerListsValue.Location = new System.Drawing.Point(183, 20);
            this.lblSizeServerListsValue.Name = "lblSizeServerListsValue";
            this.lblSizeServerListsValue.Size = new System.Drawing.Size(174, 21);
            this.lblSizeServerListsValue.TabIndex = 4;
            this.lblSizeServerListsValue.Text = "[SIZE]";
            this.lblSizeServerListsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSizeServerLists
            // 
            this.lblSizeServerLists.AutoSize = true;
            this.lblSizeServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeServerLists.Location = new System.Drawing.Point(3, 20);
            this.lblSizeServerLists.Name = "lblSizeServerLists";
            this.lblSizeServerLists.Size = new System.Drawing.Size(174, 21);
            this.lblSizeServerLists.TabIndex = 3;
            this.lblSizeServerLists.Text = "Size:";
            this.lblSizeServerLists.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberServerListsValue
            // 
            this.lblNumberServerListsValue.AutoSize = true;
            this.lblNumberServerListsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberServerListsValue.Location = new System.Drawing.Point(183, 0);
            this.lblNumberServerListsValue.Name = "lblNumberServerListsValue";
            this.lblNumberServerListsValue.Size = new System.Drawing.Size(174, 20);
            this.lblNumberServerListsValue.TabIndex = 2;
            this.lblNumberServerListsValue.Text = "[NUMBER]";
            this.lblNumberServerListsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberServerLists
            // 
            this.lblNumberServerLists.AutoSize = true;
            this.lblNumberServerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberServerLists.Location = new System.Drawing.Point(3, 0);
            this.lblNumberServerLists.Name = "lblNumberServerLists";
            this.lblNumberServerLists.Size = new System.Drawing.Size(174, 20);
            this.lblNumberServerLists.TabIndex = 1;
            this.lblNumberServerLists.Text = "Amount Cached:";
            this.lblNumberServerLists.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbThumbails
            // 
            this.tlpMain.SetColumnSpan(this.gbThumbails, 2);
            this.gbThumbails.Controls.Add(this.tlpImages);
            this.gbThumbails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThumbails.Location = new System.Drawing.Point(13, 145);
            this.gbThumbails.Name = "gbThumbails";
            this.gbThumbails.Size = new System.Drawing.Size(366, 60);
            this.gbThumbails.TabIndex = 2;
            this.gbThumbails.TabStop = false;
            this.gbThumbails.Text = "Images";
            // 
            // tlpImages
            // 
            this.tlpImages.ColumnCount = 2;
            this.tlpImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpImages.Controls.Add(this.lblSizeThumbsCachedValue, 1, 1);
            this.tlpImages.Controls.Add(this.lblSizeThumbsCached, 0, 1);
            this.tlpImages.Controls.Add(this.lblNumberThumbsCachedValue, 1, 0);
            this.tlpImages.Controls.Add(this.lblNumberThumbsCached, 0, 0);
            this.tlpImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpImages.Location = new System.Drawing.Point(3, 16);
            this.tlpImages.Name = "tlpImages";
            this.tlpImages.RowCount = 2;
            this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpImages.Size = new System.Drawing.Size(360, 41);
            this.tlpImages.TabIndex = 1;
            // 
            // lblSizeThumbsCachedValue
            // 
            this.lblSizeThumbsCachedValue.AutoSize = true;
            this.lblSizeThumbsCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeThumbsCachedValue.Location = new System.Drawing.Point(183, 20);
            this.lblSizeThumbsCachedValue.Name = "lblSizeThumbsCachedValue";
            this.lblSizeThumbsCachedValue.Size = new System.Drawing.Size(174, 21);
            this.lblSizeThumbsCachedValue.TabIndex = 4;
            this.lblSizeThumbsCachedValue.Text = "[SIZE]";
            this.lblSizeThumbsCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSizeThumbsCached
            // 
            this.lblSizeThumbsCached.AutoSize = true;
            this.lblSizeThumbsCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeThumbsCached.Location = new System.Drawing.Point(3, 20);
            this.lblSizeThumbsCached.Name = "lblSizeThumbsCached";
            this.lblSizeThumbsCached.Size = new System.Drawing.Size(174, 21);
            this.lblSizeThumbsCached.TabIndex = 3;
            this.lblSizeThumbsCached.Text = "Size:";
            this.lblSizeThumbsCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberThumbsCachedValue
            // 
            this.lblNumberThumbsCachedValue.AutoSize = true;
            this.lblNumberThumbsCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberThumbsCachedValue.Location = new System.Drawing.Point(183, 0);
            this.lblNumberThumbsCachedValue.Name = "lblNumberThumbsCachedValue";
            this.lblNumberThumbsCachedValue.Size = new System.Drawing.Size(174, 20);
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
            this.lblNumberThumbsCached.Size = new System.Drawing.Size(174, 20);
            this.lblNumberThumbsCached.TabIndex = 1;
            this.lblNumberThumbsCached.Text = "Amount Cached:";
            this.lblNumberThumbsCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbApiXml
            // 
            this.tlpMain.SetColumnSpan(this.gbApiXml, 2);
            this.gbApiXml.Controls.Add(this.tlpXmlApi);
            this.gbApiXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbApiXml.Location = new System.Drawing.Point(13, 211);
            this.gbApiXml.Name = "gbApiXml";
            this.gbApiXml.Size = new System.Drawing.Size(366, 60);
            this.gbApiXml.TabIndex = 3;
            this.gbApiXml.TabStop = false;
            this.gbApiXml.Text = "XML API Data";
            // 
            // tlpXmlApi
            // 
            this.tlpXmlApi.ColumnCount = 2;
            this.tlpXmlApi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpXmlApi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpXmlApi.Controls.Add(this.lblSizeXmlCachedValue, 1, 1);
            this.tlpXmlApi.Controls.Add(this.lblSizeXmlCached, 0, 1);
            this.tlpXmlApi.Controls.Add(this.lblNumberXmlCachedValue, 1, 0);
            this.tlpXmlApi.Controls.Add(this.lblNumberXmlCached, 0, 0);
            this.tlpXmlApi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpXmlApi.Location = new System.Drawing.Point(3, 16);
            this.tlpXmlApi.Name = "tlpXmlApi";
            this.tlpXmlApi.RowCount = 2;
            this.tlpXmlApi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpXmlApi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpXmlApi.Size = new System.Drawing.Size(360, 41);
            this.tlpXmlApi.TabIndex = 1;
            // 
            // lblSizeXmlCachedValue
            // 
            this.lblSizeXmlCachedValue.AutoSize = true;
            this.lblSizeXmlCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeXmlCachedValue.Location = new System.Drawing.Point(183, 20);
            this.lblSizeXmlCachedValue.Name = "lblSizeXmlCachedValue";
            this.lblSizeXmlCachedValue.Size = new System.Drawing.Size(174, 21);
            this.lblSizeXmlCachedValue.TabIndex = 4;
            this.lblSizeXmlCachedValue.Text = "[SIZE]";
            this.lblSizeXmlCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSizeXmlCached
            // 
            this.lblSizeXmlCached.AutoSize = true;
            this.lblSizeXmlCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSizeXmlCached.Location = new System.Drawing.Point(3, 20);
            this.lblSizeXmlCached.Name = "lblSizeXmlCached";
            this.lblSizeXmlCached.Size = new System.Drawing.Size(174, 21);
            this.lblSizeXmlCached.TabIndex = 3;
            this.lblSizeXmlCached.Text = "Size:";
            this.lblSizeXmlCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumberXmlCachedValue
            // 
            this.lblNumberXmlCachedValue.AutoSize = true;
            this.lblNumberXmlCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberXmlCachedValue.Location = new System.Drawing.Point(183, 0);
            this.lblNumberXmlCachedValue.Name = "lblNumberXmlCachedValue";
            this.lblNumberXmlCachedValue.Size = new System.Drawing.Size(174, 20);
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
            this.lblNumberXmlCached.Size = new System.Drawing.Size(174, 20);
            this.lblNumberXmlCached.TabIndex = 1;
            this.lblNumberXmlCached.Text = "Amount Cached:";
            this.lblNumberXmlCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbTotal
            // 
            this.tlpMain.SetColumnSpan(this.gbTotal, 2);
            this.gbTotal.Controls.Add(this.tlpTotal);
            this.gbTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTotal.Location = new System.Drawing.Point(13, 277);
            this.gbTotal.Name = "gbTotal";
            this.gbTotal.Size = new System.Drawing.Size(366, 60);
            this.gbTotal.TabIndex = 4;
            this.gbTotal.TabStop = false;
            this.gbTotal.Text = "Total";
            // 
            // tlpTotal
            // 
            this.tlpTotal.ColumnCount = 2;
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.Controls.Add(this.lblTotalSizeCachedValue, 1, 1);
            this.tlpTotal.Controls.Add(this.lblTotalSizeCached, 0, 1);
            this.tlpTotal.Controls.Add(this.lblTotalAmountCachedValue, 1, 0);
            this.tlpTotal.Controls.Add(this.lblTotalAmountCached, 0, 0);
            this.tlpTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTotal.Location = new System.Drawing.Point(3, 16);
            this.tlpTotal.Name = "tlpTotal";
            this.tlpTotal.RowCount = 2;
            this.tlpTotal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.Size = new System.Drawing.Size(360, 41);
            this.tlpTotal.TabIndex = 1;
            // 
            // lblTotalSizeCachedValue
            // 
            this.lblTotalSizeCachedValue.AutoSize = true;
            this.lblTotalSizeCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalSizeCachedValue.Location = new System.Drawing.Point(183, 20);
            this.lblTotalSizeCachedValue.Name = "lblTotalSizeCachedValue";
            this.lblTotalSizeCachedValue.Size = new System.Drawing.Size(174, 21);
            this.lblTotalSizeCachedValue.TabIndex = 4;
            this.lblTotalSizeCachedValue.Text = "[SIZE]";
            this.lblTotalSizeCachedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalSizeCached
            // 
            this.lblTotalSizeCached.AutoSize = true;
            this.lblTotalSizeCached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalSizeCached.Location = new System.Drawing.Point(3, 20);
            this.lblTotalSizeCached.Name = "lblTotalSizeCached";
            this.lblTotalSizeCached.Size = new System.Drawing.Size(174, 21);
            this.lblTotalSizeCached.TabIndex = 3;
            this.lblTotalSizeCached.Text = "Total Size:";
            this.lblTotalSizeCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmountCachedValue
            // 
            this.lblTotalAmountCachedValue.AutoSize = true;
            this.lblTotalAmountCachedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmountCachedValue.Location = new System.Drawing.Point(183, 0);
            this.lblTotalAmountCachedValue.Name = "lblTotalAmountCachedValue";
            this.lblTotalAmountCachedValue.Size = new System.Drawing.Size(174, 20);
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
            this.lblTotalAmountCached.Size = new System.Drawing.Size(174, 20);
            this.lblTotalAmountCached.TabIndex = 1;
            this.lblTotalAmountCached.Text = "Total Amount Cached:";
            this.lblTotalAmountCached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.tlpMain.SetColumnSpan(this.btnOK, 2);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(13, 350);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(366, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // gbCachingDir
            // 
            this.tlpMain.SetColumnSpan(this.gbCachingDir, 2);
            this.gbCachingDir.Controls.Add(this.tlpCachingDir);
            this.gbCachingDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCachingDir.Location = new System.Drawing.Point(13, 13);
            this.gbCachingDir.Name = "gbCachingDir";
            this.gbCachingDir.Size = new System.Drawing.Size(366, 60);
            this.gbCachingDir.TabIndex = 2;
            this.gbCachingDir.TabStop = false;
            this.gbCachingDir.Text = "Cached Data Location";
            // 
            // tlpCachingDir
            // 
            this.tlpCachingDir.ColumnCount = 1;
            this.tlpCachingDir.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCachingDir.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCachingDir.Controls.Add(this.lblCachingDir, 0, 0);
            this.tlpCachingDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCachingDir.Location = new System.Drawing.Point(3, 16);
            this.tlpCachingDir.Name = "tlpCachingDir";
            this.tlpCachingDir.RowCount = 1;
            this.tlpCachingDir.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCachingDir.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCachingDir.Size = new System.Drawing.Size(360, 41);
            this.tlpCachingDir.TabIndex = 1;
            // 
            // lblCachingDir
            // 
            this.lblCachingDir.AutoSize = true;
            this.lblCachingDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCachingDir.Location = new System.Drawing.Point(3, 0);
            this.lblCachingDir.Name = "lblCachingDir";
            this.lblCachingDir.Size = new System.Drawing.Size(354, 41);
            this.lblCachingDir.TabIndex = 1;
            this.lblCachingDir.Text = "[DIRECTORY]";
            this.lblCachingDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.gbCachingDir, 0, 0);
            this.tlpMain.Controls.Add(this.btnOK, 0, 5);
            this.tlpMain.Controls.Add(this.gbServerLists, 0, 1);
            this.tlpMain.Controls.Add(this.gbTotal, 0, 4);
            this.tlpMain.Controls.Add(this.gbThumbails, 0, 2);
            this.tlpMain.Controls.Add(this.gbApiXml, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(10);
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.00001F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(392, 390);
            this.tlpMain.TabIndex = 2;
            // 
            // CachingMetricsUi
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 390);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CachingMetricsUi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caching Metrics";
            this.Load += new System.EventHandler(this.CachingMetricsUI_Load);
            this.gbServerLists.ResumeLayout(false);
            this.tlpServerLists.ResumeLayout(false);
            this.tlpServerLists.PerformLayout();
            this.gbThumbails.ResumeLayout(false);
            this.tlpImages.ResumeLayout(false);
            this.tlpImages.PerformLayout();
            this.gbApiXml.ResumeLayout(false);
            this.tlpXmlApi.ResumeLayout(false);
            this.tlpXmlApi.PerformLayout();
            this.gbTotal.ResumeLayout(false);
            this.tlpTotal.ResumeLayout(false);
            this.tlpTotal.PerformLayout();
            this.gbCachingDir.ResumeLayout(false);
            this.tlpCachingDir.ResumeLayout(false);
            this.tlpCachingDir.PerformLayout();
            this.tlpMain.ResumeLayout(false);
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
        private TableLayoutPanel tlpImages;
        private Label lblSizeThumbsCachedValue;
        private Label lblSizeThumbsCached;
        private Label lblNumberThumbsCachedValue;
        private Label lblNumberThumbsCached;
        private GroupBox gbApiXml;
        private TableLayoutPanel tlpXmlApi;
        private Label lblSizeXmlCachedValue;
        private Label lblSizeXmlCached;
        private Label lblNumberXmlCachedValue;
        private Label lblNumberXmlCached;
        private GroupBox gbTotal;
        private TableLayoutPanel tlpTotal;
        private Label lblTotalSizeCachedValue;
        private Label lblTotalSizeCached;
        private Label lblTotalAmountCachedValue;
        private Label lblTotalAmountCached;
        private Button btnOK;
        private GroupBox gbCachingDir;
        private TableLayoutPanel tlpCachingDir;
        private Label lblCachingDir;
        private TableLayoutPanel tlpMain;
    }
}