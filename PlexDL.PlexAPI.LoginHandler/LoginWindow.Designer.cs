namespace PlexDL.PlexAPI.LoginHandler
{
    partial class LoginWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            this.pbMain = new PlexDL.Common.Components.CircularProgressBar();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.tmrDotChange = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.AnimationFunction = PlexDL.Animation.WinFormAnimation.KnownAnimationFunctions.Liner;
            this.pbMain.AnimationSpeed = 500;
            this.pbMain.BackColor = System.Drawing.Color.Transparent;
            this.pbMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.pbMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pbMain.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbMain.InnerMargin = 2;
            this.pbMain.InnerWidth = -1;
            this.pbMain.Location = new System.Drawing.Point(12, 12);
            this.pbMain.MarqueeAnimationSpeed = 2000;
            this.pbMain.Name = "pbMain";
            this.pbMain.OuterColor = System.Drawing.Color.Gray;
            this.pbMain.OuterMargin = -25;
            this.pbMain.OuterWidth = 26;
            this.pbMain.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pbMain.ProgressWidth = 25;
            this.pbMain.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.pbMain.Size = new System.Drawing.Size(200, 200);
            this.pbMain.StartAngle = 270;
            this.pbMain.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.pbMain.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.pbMain.SubscriptText = "";
            this.pbMain.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.pbMain.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.pbMain.SuperscriptText = "";
            this.pbMain.TabIndex = 0;
            this.pbMain.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.pbMain.Value = 68;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.ForeColor = System.Drawing.Color.Gray;
            this.lblInstructions.Location = new System.Drawing.Point(235, 12);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(315, 99);
            this.lblInstructions.TabIndex = 1;
            this.lblInstructions.Text = "Press \'OK\' once you\'ve\r\nlogged in with your\r\nbrowser";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Gray;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(241, 141);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(315, 48);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // tmrDotChange
            // 
            this.tmrDotChange.Interval = 200;
            this.tmrDotChange.Tick += new System.EventHandler(this.TmrDotChange_Tick);
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 225);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.pbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plex.tv Authentication";
            this.Load += new System.EventHandler(this.LoginWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.Components.CircularProgressBar pbMain;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer tmrDotChange;
    }
}