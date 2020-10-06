using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.UI
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.lblNewFunctionality = new System.Windows.Forms.Label();
            this.lblNothingInteresting = new System.Windows.Forms.Label();
            this.pnlTestingArea = new System.Windows.Forms.Panel();
            this.btnOpenPxzExplorer = new System.Windows.Forms.Button();
            this.pnlNothingInteresting = new System.Windows.Forms.Panel();
            this.pnlTestingArea.SuspendLayout();
            this.pnlNothingInteresting.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNewFunctionality
            // 
            this.lblNewFunctionality.AutoSize = true;
            this.lblNewFunctionality.Location = new System.Drawing.Point(3, 1);
            this.lblNewFunctionality.Name = "lblNewFunctionality";
            this.lblNewFunctionality.Size = new System.Drawing.Size(168, 13);
            this.lblNewFunctionality.TabIndex = 0;
            this.lblNewFunctionality.Text = "This is for testing new functionality";
            // 
            // lblNothingInteresting
            // 
            this.lblNothingInteresting.AutoSize = true;
            this.lblNothingInteresting.Location = new System.Drawing.Point(3, 14);
            this.lblNothingInteresting.Name = "lblNothingInteresting";
            this.lblNothingInteresting.Size = new System.Drawing.Size(260, 13);
            this.lblNothingInteresting.TabIndex = 1;
            this.lblNothingInteresting.Text = "You won\'t find anything of interest here, unfortunately.";
            // 
            // pnlTestingArea
            // 
            this.pnlTestingArea.Controls.Add(this.btnOpenPxzExplorer);
            this.pnlTestingArea.Controls.Add(this.pnlNothingInteresting);
            this.pnlTestingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTestingArea.Location = new System.Drawing.Point(0, 0);
            this.pnlTestingArea.Name = "pnlTestingArea";
            this.pnlTestingArea.Size = new System.Drawing.Size(800, 450);
            this.pnlTestingArea.TabIndex = 2;
            // 
            // btnOpenPxzExplorer
            // 
            this.btnOpenPxzExplorer.Location = new System.Drawing.Point(638, 415);
            this.btnOpenPxzExplorer.Name = "btnOpenPxzExplorer";
            this.btnOpenPxzExplorer.Size = new System.Drawing.Size(150, 23);
            this.btnOpenPxzExplorer.TabIndex = 3;
            this.btnOpenPxzExplorer.Text = "Open PXZ Explorer";
            this.btnOpenPxzExplorer.UseVisualStyleBackColor = true;
            this.btnOpenPxzExplorer.Click += new System.EventHandler(this.BtnOpenPxzExplorer_Click);
            // 
            // pnlNothingInteresting
            // 
            this.pnlNothingInteresting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNothingInteresting.Controls.Add(this.lblNewFunctionality);
            this.pnlNothingInteresting.Controls.Add(this.lblNothingInteresting);
            this.pnlNothingInteresting.Location = new System.Drawing.Point(0, 0);
            this.pnlNothingInteresting.Name = "pnlNothingInteresting";
            this.pnlNothingInteresting.Size = new System.Drawing.Size(264, 30);
            this.pnlNothingInteresting.TabIndex = 2;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlTestingArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Testing Area";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.pnlTestingArea.ResumeLayout(false);
            this.pnlNothingInteresting.ResumeLayout(false);
            this.pnlNothingInteresting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblNewFunctionality;
        private Label lblNothingInteresting;
        private Panel pnlTestingArea;
        private Panel pnlNothingInteresting;
        private Button btnOpenPxzExplorer;
    }
}