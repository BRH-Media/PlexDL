namespace PlexDL.UI
{
    partial class Debug
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmrUpdateRef = new System.Windows.Forms.Timer(this.components);
            this.tlpDebug = new System.Windows.Forms.TableLayoutPanel();
            this.gbGlobalFlags = new System.Windows.Forms.GroupBox();
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRefreshCount = new System.Windows.Forms.Label();
            this.lblRefreshCountValue = new System.Windows.Forms.Label();
            this.dgvGlobalFlags = new PlexDL.Common.Components.FlatDataGridView();
            this.tlpRefreshCount = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPollRate = new System.Windows.Forms.TableLayoutPanel();
            this.lblPollRateValue = new System.Windows.Forms.Label();
            this.lblPollRate = new System.Windows.Forms.Label();
            this.tlpDebug.SuspendLayout();
            this.gbGlobalFlags.SuspendLayout();
            this.tlpControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).BeginInit();
            this.tlpRefreshCount.SuspendLayout();
            this.tlpPollRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrUpdateRef
            // 
            this.tmrUpdateRef.Tick += new System.EventHandler(this.TmrUpdateRef_Tick);
            // 
            // tlpDebug
            // 
            this.tlpDebug.AutoSize = true;
            this.tlpDebug.ColumnCount = 2;
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDebug.Controls.Add(this.groupBox1, 1, 0);
            this.tlpDebug.Controls.Add(this.gbGlobalFlags, 0, 0);
            this.tlpDebug.Controls.Add(this.tlpControls, 0, 9);
            this.tlpDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDebug.Location = new System.Drawing.Point(0, 0);
            this.tlpDebug.Name = "tlpDebug";
            this.tlpDebug.RowCount = 10;
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.48566F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.97323F));
            this.tlpDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDebug.Size = new System.Drawing.Size(450, 523);
            this.tlpDebug.TabIndex = 0;
            this.tlpDebug.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpDebug_Paint);
            // 
            // gbGlobalFlags
            // 
            this.gbGlobalFlags.Controls.Add(this.dgvGlobalFlags);
            this.gbGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGlobalFlags.Location = new System.Drawing.Point(3, 3);
            this.gbGlobalFlags.Name = "gbGlobalFlags";
            this.tlpDebug.SetRowSpan(this.gbGlobalFlags, 9);
            this.gbGlobalFlags.Size = new System.Drawing.Size(219, 422);
            this.gbGlobalFlags.TabIndex = 14;
            this.gbGlobalFlags.TabStop = false;
            this.gbGlobalFlags.Text = "Global Flags";
            // 
            // tlpControls
            // 
            this.tlpControls.ColumnCount = 4;
            this.tlpDebug.SetColumnSpan(this.tlpControls, 2);
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpControls.Controls.Add(this.tlpPollRate, 2, 0);
            this.tlpControls.Controls.Add(this.tlpRefreshCount, 0, 0);
            this.tlpControls.Controls.Add(this.btnTimer, 0, 1);
            this.tlpControls.Controls.Add(this.btnRefresh, 0, 1);
            this.tlpControls.Controls.Add(this.btnCancel, 3, 1);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(3, 431);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 2;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpControls.Size = new System.Drawing.Size(444, 89);
            this.tlpControls.TabIndex = 15;
            // 
            // btnTimer
            // 
            this.tlpControls.SetColumnSpan(this.btnTimer, 2);
            this.btnTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTimer.Location = new System.Drawing.Point(114, 47);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(216, 39);
            this.btnTimer.TabIndex = 17;
            this.btnTimer.Text = "Auto-refresh On";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.Location = new System.Drawing.Point(3, 47);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 39);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(336, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 39);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(228, 3);
            this.groupBox1.Name = "groupBox1";
            this.tlpDebug.SetRowSpan(this.groupBox1, 9);
            this.groupBox1.Size = new System.Drawing.Size(219, 422);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Values";
            // 
            // lblRefreshCount
            // 
            this.lblRefreshCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefreshCount.Location = new System.Drawing.Point(3, 0);
            this.lblRefreshCount.Name = "lblRefreshCount";
            this.lblRefreshCount.Size = new System.Drawing.Size(102, 38);
            this.lblRefreshCount.TabIndex = 4;
            this.lblRefreshCount.Text = "Refresh Count:";
            this.lblRefreshCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefreshCountValue
            // 
            this.lblRefreshCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRefreshCountValue.Location = new System.Drawing.Point(111, 0);
            this.lblRefreshCountValue.Name = "lblRefreshCountValue";
            this.lblRefreshCountValue.Size = new System.Drawing.Size(102, 38);
            this.lblRefreshCountValue.TabIndex = 5;
            this.lblRefreshCountValue.Text = "0";
            this.lblRefreshCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvGlobalFlags
            // 
            this.dgvGlobalFlags.AllowUserToAddRows = false;
            this.dgvGlobalFlags.AllowUserToDeleteRows = false;
            this.dgvGlobalFlags.AllowUserToOrderColumns = true;
            this.dgvGlobalFlags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGlobalFlags.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvGlobalFlags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGlobalFlags.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvGlobalFlags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGlobalFlags.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGlobalFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGlobalFlags.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGlobalFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvGlobalFlags.Location = new System.Drawing.Point(3, 16);
            this.dgvGlobalFlags.MultiSelect = false;
            this.dgvGlobalFlags.Name = "dgvGlobalFlags";
            this.dgvGlobalFlags.RowHeadersVisible = false;
            this.dgvGlobalFlags.RowsEmptyText = "No Flags Found";
            this.dgvGlobalFlags.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvGlobalFlags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGlobalFlags.Size = new System.Drawing.Size(213, 403);
            this.dgvGlobalFlags.TabIndex = 2;
            // 
            // tlpRefreshCount
            // 
            this.tlpRefreshCount.ColumnCount = 2;
            this.tlpControls.SetColumnSpan(this.tlpRefreshCount, 2);
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRefreshCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRefreshCount.Controls.Add(this.lblRefreshCountValue, 1, 0);
            this.tlpRefreshCount.Controls.Add(this.lblRefreshCount, 0, 0);
            this.tlpRefreshCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRefreshCount.Location = new System.Drawing.Point(3, 3);
            this.tlpRefreshCount.Name = "tlpRefreshCount";
            this.tlpRefreshCount.RowCount = 1;
            this.tlpRefreshCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRefreshCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRefreshCount.Size = new System.Drawing.Size(216, 38);
            this.tlpRefreshCount.TabIndex = 6;
            // 
            // tlpPollRate
            // 
            this.tlpPollRate.ColumnCount = 2;
            this.tlpControls.SetColumnSpan(this.tlpPollRate, 2);
            this.tlpPollRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPollRate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPollRate.Controls.Add(this.lblPollRateValue, 1, 0);
            this.tlpPollRate.Controls.Add(this.lblPollRate, 0, 0);
            this.tlpPollRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPollRate.Location = new System.Drawing.Point(225, 3);
            this.tlpPollRate.Name = "tlpPollRate";
            this.tlpPollRate.RowCount = 1;
            this.tlpPollRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPollRate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPollRate.Size = new System.Drawing.Size(216, 38);
            this.tlpPollRate.TabIndex = 19;
            // 
            // lblPollRateValue
            // 
            this.lblPollRateValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPollRateValue.Location = new System.Drawing.Point(111, 0);
            this.lblPollRateValue.Name = "lblPollRateValue";
            this.lblPollRateValue.Size = new System.Drawing.Size(102, 38);
            this.lblPollRateValue.TabIndex = 5;
            this.lblPollRateValue.Text = "0";
            this.lblPollRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPollRate
            // 
            this.lblPollRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPollRate.Location = new System.Drawing.Point(3, 0);
            this.lblPollRate.Name = "lblPollRate";
            this.lblPollRate.Size = new System.Drawing.Size(102, 38);
            this.lblPollRate.TabIndex = 4;
            this.lblPollRate.Text = "Poll Rate:";
            this.lblPollRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 523);
            this.ControlBox = false;
            this.Controls.Add(this.tlpDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.tlpDebug.ResumeLayout(false);
            this.gbGlobalFlags.ResumeLayout(false);
            this.tlpControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).EndInit();
            this.tlpRefreshCount.ResumeLayout(false);
            this.tlpPollRate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrUpdateRef;
        private System.Windows.Forms.TableLayoutPanel tlpDebug;
        private System.Windows.Forms.GroupBox gbGlobalFlags;
        private Common.Components.FlatDataGridView dgvGlobalFlags;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRefreshCount;
        private System.Windows.Forms.Label lblRefreshCountValue;
        private System.Windows.Forms.TableLayoutPanel tlpRefreshCount;
        private System.Windows.Forms.TableLayoutPanel tlpPollRate;
        private System.Windows.Forms.Label lblPollRateValue;
        private System.Windows.Forms.Label lblPollRate;
    }
}