namespace GitHubUpdater.UI
{
    partial class UpdateChannel
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
            this.gbSelectChannel = new System.Windows.Forms.GroupBox();
            this.radDeveloperChannel = new System.Windows.Forms.RadioButton();
            this.radStableChannel = new System.Windows.Forms.RadioButton();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbSelectChannel.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSelectChannel
            // 
            this.tlpMain.SetColumnSpan(this.gbSelectChannel, 2);
            this.gbSelectChannel.Controls.Add(this.radDeveloperChannel);
            this.gbSelectChannel.Controls.Add(this.radStableChannel);
            this.gbSelectChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSelectChannel.Location = new System.Drawing.Point(3, 3);
            this.gbSelectChannel.Name = "gbSelectChannel";
            this.gbSelectChannel.Size = new System.Drawing.Size(329, 66);
            this.gbSelectChannel.TabIndex = 0;
            this.gbSelectChannel.TabStop = false;
            this.gbSelectChannel.Text = "Select Update Channel";
            // 
            // radDeveloperChannel
            // 
            this.radDeveloperChannel.AutoSize = true;
            this.radDeveloperChannel.Location = new System.Drawing.Point(6, 42);
            this.radDeveloperChannel.Name = "radDeveloperChannel";
            this.radDeveloperChannel.Size = new System.Drawing.Size(116, 17);
            this.radDeveloperChannel.TabIndex = 1;
            this.radDeveloperChannel.TabStop = true;
            this.radDeveloperChannel.Text = "Developer Channel";
            this.radDeveloperChannel.UseVisualStyleBackColor = true;
            this.radDeveloperChannel.CheckedChanged += new System.EventHandler(this.RadDeveloperChannel_CheckedChanged);
            // 
            // radStableChannel
            // 
            this.radStableChannel.AutoSize = true;
            this.radStableChannel.Checked = true;
            this.radStableChannel.Location = new System.Drawing.Point(6, 19);
            this.radStableChannel.Name = "radStableChannel";
            this.radStableChannel.Size = new System.Drawing.Size(97, 17);
            this.radStableChannel.TabIndex = 0;
            this.radStableChannel.TabStop = true;
            this.radStableChannel.Text = "Stable Channel";
            this.radStableChannel.UseVisualStyleBackColor = true;
            this.radStableChannel.CheckedChanged += new System.EventHandler(this.RadStableChannel_CheckedChanged);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.btnCancel, 0, 1);
            this.tlpMain.Controls.Add(this.gbSelectChannel, 0, 0);
            this.tlpMain.Controls.Add(this.btnOK, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(10, 10);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(335, 101);
            this.tlpMain.TabIndex = 1;
            this.tlpMain.Paint += new System.Windows.Forms.PaintEventHandler(this.TlpMain_Paint);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(3, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(170, 75);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(162, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // UpdateChannel
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(355, 121);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateChannel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.gbSelectChannel.ResumeLayout(false);
            this.gbSelectChannel.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSelectChannel;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton radStableChannel;
        private System.Windows.Forms.RadioButton radDeveloperChannel;
    }
}