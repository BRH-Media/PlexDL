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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbGlobalFlags = new System.Windows.Forms.GroupBox();
            this.tmrUpdateRef = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTimer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvGlobalFlags = new PlexDL.Common.Components.FlatDataGridView();
            this.gbGlobalFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).BeginInit();
            this.SuspendLayout();
            // 
            // gbGlobalFlags
            // 
            this.gbGlobalFlags.Controls.Add(this.dgvGlobalFlags);
            this.gbGlobalFlags.Location = new System.Drawing.Point(13, 13);
            this.gbGlobalFlags.Name = "gbGlobalFlags";
            this.gbGlobalFlags.Size = new System.Drawing.Size(225, 425);
            this.gbGlobalFlags.TabIndex = 0;
            this.gbGlobalFlags.TabStop = false;
            this.gbGlobalFlags.Text = "Global Flags";
            // 
            // tmrUpdateRef
            // 
            this.tmrUpdateRef.Tick += new System.EventHandler(this.tmrUpdateRef_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(244, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 425);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Values";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(13, 444);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.Location = new System.Drawing.Point(94, 444);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(75, 23);
            this.btnTimer.TabIndex = 2;
            this.btnTimer.Text = "Timer On";
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(366, 444);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel Debugging";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGlobalFlags.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGlobalFlags.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGlobalFlags.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvGlobalFlags.Location = new System.Drawing.Point(6, 19);
            this.dgvGlobalFlags.MultiSelect = false;
            this.dgvGlobalFlags.Name = "dgvGlobalFlags";
            this.dgvGlobalFlags.RowHeadersVisible = false;
            this.dgvGlobalFlags.RowsEmptyText = "No Flags Found";
            this.dgvGlobalFlags.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvGlobalFlags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGlobalFlags.Size = new System.Drawing.Size(213, 400);
            this.dgvGlobalFlags.TabIndex = 0;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 509);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTimer);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbGlobalFlags);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.gbGlobalFlags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalFlags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGlobalFlags;
        private System.Windows.Forms.Timer tmrUpdateRef;
        private System.Windows.Forms.GroupBox groupBox1;
        private Common.Components.FlatDataGridView dgvGlobalFlags;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnCancel;
    }
}