namespace PlexDL.Common.Shodan.UI
{
    partial class TokenFinderSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenFinderSettings));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbShodanApi = new System.Windows.Forms.GroupBox();
            this.tlpShodanApi = new System.Windows.Forms.TableLayoutPanel();
            this.gbApiTest = new System.Windows.Forms.GroupBox();
            this.btnExecuteTest = new System.Windows.Forms.Button();
            this.gbApiKey = new System.Windows.Forms.GroupBox();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            this.gbShodanApi.SuspendLayout();
            this.tlpShodanApi.SuspendLayout();
            this.gbApiTest.SuspendLayout();
            this.gbApiKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(3, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(185, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(194, 125);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(186, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.gbShodanApi, 0, 0);
            this.tlpMain.Controls.Add(this.btnOK, 1, 1);
            this.tlpMain.Controls.Add(this.btnCancel, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(383, 151);
            this.tlpMain.TabIndex = 3;
            // 
            // gbShodanApi
            // 
            this.tlpMain.SetColumnSpan(this.gbShodanApi, 2);
            this.gbShodanApi.Controls.Add(this.tlpShodanApi);
            this.gbShodanApi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbShodanApi.Location = new System.Drawing.Point(3, 3);
            this.gbShodanApi.Name = "gbShodanApi";
            this.gbShodanApi.Size = new System.Drawing.Size(377, 116);
            this.gbShodanApi.TabIndex = 4;
            this.gbShodanApi.TabStop = false;
            this.gbShodanApi.Text = "Shodan API";
            // 
            // tlpShodanApi
            // 
            this.tlpShodanApi.ColumnCount = 1;
            this.tlpShodanApi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShodanApi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShodanApi.Controls.Add(this.gbApiTest, 0, 1);
            this.tlpShodanApi.Controls.Add(this.gbApiKey, 0, 0);
            this.tlpShodanApi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpShodanApi.Location = new System.Drawing.Point(3, 16);
            this.tlpShodanApi.Name = "tlpShodanApi";
            this.tlpShodanApi.RowCount = 2;
            this.tlpShodanApi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShodanApi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShodanApi.Size = new System.Drawing.Size(371, 97);
            this.tlpShodanApi.TabIndex = 0;
            // 
            // gbApiTest
            // 
            this.tlpShodanApi.SetColumnSpan(this.gbApiTest, 2);
            this.gbApiTest.Controls.Add(this.btnExecuteTest);
            this.gbApiTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbApiTest.Location = new System.Drawing.Point(3, 51);
            this.gbApiTest.Name = "gbApiTest";
            this.gbApiTest.Size = new System.Drawing.Size(365, 43);
            this.gbApiTest.TabIndex = 4;
            this.gbApiTest.TabStop = false;
            this.gbApiTest.Text = "API Test";
            // 
            // btnExecuteTest
            // 
            this.btnExecuteTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecuteTest.Location = new System.Drawing.Point(3, 16);
            this.btnExecuteTest.Name = "btnExecuteTest";
            this.btnExecuteTest.Size = new System.Drawing.Size(359, 24);
            this.btnExecuteTest.TabIndex = 0;
            this.btnExecuteTest.Text = "Execute Test";
            this.btnExecuteTest.UseVisualStyleBackColor = true;
            this.btnExecuteTest.Click += new System.EventHandler(this.BtnApiTest_Click);
            // 
            // gbApiKey
            // 
            this.tlpShodanApi.SetColumnSpan(this.gbApiKey, 2);
            this.gbApiKey.Controls.Add(this.txtApiKey);
            this.gbApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbApiKey.Location = new System.Drawing.Point(3, 3);
            this.gbApiKey.Name = "gbApiKey";
            this.gbApiKey.Size = new System.Drawing.Size(365, 42);
            this.gbApiKey.TabIndex = 3;
            this.gbApiKey.TabStop = false;
            this.gbApiKey.Text = "API Key";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtApiKey.Location = new System.Drawing.Point(3, 16);
            this.txtApiKey.MaxLength = 32;
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(359, 20);
            this.txtApiKey.TabIndex = 0;
            // 
            // TokenFinderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 151);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TokenFinderSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.TokenFinderSettings_Load);
            this.tlpMain.ResumeLayout(false);
            this.gbShodanApi.ResumeLayout(false);
            this.tlpShodanApi.ResumeLayout(false);
            this.gbApiTest.ResumeLayout(false);
            this.gbApiKey.ResumeLayout(false);
            this.gbApiKey.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox gbShodanApi;
        private System.Windows.Forms.TableLayoutPanel tlpShodanApi;
        private System.Windows.Forms.GroupBox gbApiTest;
        private System.Windows.Forms.Button btnExecuteTest;
        private System.Windows.Forms.GroupBox gbApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
    }
}