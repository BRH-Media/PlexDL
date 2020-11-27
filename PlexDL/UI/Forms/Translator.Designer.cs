namespace PlexDL.UI.Forms
{
    partial class Translator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Translator));
            this.gbLogdel = new System.Windows.Forms.GroupBox();
            this.btnLoadDict = new System.Windows.Forms.Button();
            this.btnBrowseLogdel = new System.Windows.Forms.Button();
            this.txtLogdel = new System.Windows.Forms.TextBox();
            this.ofdLogdel = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblValidValue = new System.Windows.Forms.Label();
            this.lblInvalidValue = new System.Windows.Forms.Label();
            this.lblDuplicatesValue = new System.Windows.Forms.Label();
            this.lblDuplicates = new System.Windows.Forms.Label();
            this.lblInvalid = new System.Windows.Forms.Label();
            this.lblValid = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.bwTranslate = new libbrhscgui.Components.AbortableBackgroundWorker();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.gbLogdel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLogdel
            // 
            this.gbLogdel.Controls.Add(this.btnLoadDict);
            this.gbLogdel.Controls.Add(this.btnBrowseLogdel);
            this.gbLogdel.Controls.Add(this.txtLogdel);
            this.gbLogdel.Location = new System.Drawing.Point(12, 13);
            this.gbLogdel.Name = "gbLogdel";
            this.gbLogdel.Size = new System.Drawing.Size(222, 78);
            this.gbLogdel.TabIndex = 0;
            this.gbLogdel.TabStop = false;
            this.gbLogdel.Text = "Input LOGDEL Token Dictionary";
            // 
            // btnLoadDict
            // 
            this.btnLoadDict.Location = new System.Drawing.Point(7, 46);
            this.btnLoadDict.Name = "btnLoadDict";
            this.btnLoadDict.Size = new System.Drawing.Size(208, 23);
            this.btnLoadDict.TabIndex = 2;
            this.btnLoadDict.Text = "Load";
            this.btnLoadDict.UseVisualStyleBackColor = true;
            this.btnLoadDict.Click += new System.EventHandler(this.btnLoadDict_Click);
            // 
            // btnBrowseLogdel
            // 
            this.btnBrowseLogdel.Location = new System.Drawing.Point(183, 20);
            this.btnBrowseLogdel.Name = "btnBrowseLogdel";
            this.btnBrowseLogdel.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseLogdel.TabIndex = 1;
            this.btnBrowseLogdel.Text = "...";
            this.btnBrowseLogdel.UseVisualStyleBackColor = true;
            this.btnBrowseLogdel.Click += new System.EventHandler(this.BtnBrowseLogdel_Click);
            // 
            // txtLogdel
            // 
            this.txtLogdel.Location = new System.Drawing.Point(7, 20);
            this.txtLogdel.Name = "txtLogdel";
            this.txtLogdel.ReadOnly = true;
            this.txtLogdel.Size = new System.Drawing.Size(170, 20);
            this.txtLogdel.TabIndex = 0;
            // 
            // ofdLogdel
            // 
            this.ofdLogdel.Filter = "Token Dictionary|tokens.logdel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalValue);
            this.groupBox1.Controls.Add(this.lblValidValue);
            this.groupBox1.Controls.Add(this.lblInvalidValue);
            this.groupBox1.Controls.Add(this.lblDuplicatesValue);
            this.groupBox1.Controls.Add(this.lblDuplicates);
            this.groupBox1.Controls.Add(this.lblInvalid);
            this.groupBox1.Controls.Add(this.lblValid);
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Location = new System.Drawing.Point(12, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 75);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Statistics";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Location = new System.Drawing.Point(107, 16);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(13, 13);
            this.lblTotalValue.TabIndex = 7;
            this.lblTotalValue.Text = "0";
            // 
            // lblValidValue
            // 
            this.lblValidValue.AutoSize = true;
            this.lblValidValue.Location = new System.Drawing.Point(107, 29);
            this.lblValidValue.Name = "lblValidValue";
            this.lblValidValue.Size = new System.Drawing.Size(13, 13);
            this.lblValidValue.TabIndex = 6;
            this.lblValidValue.Text = "0";
            // 
            // lblInvalidValue
            // 
            this.lblInvalidValue.AutoSize = true;
            this.lblInvalidValue.Location = new System.Drawing.Point(107, 42);
            this.lblInvalidValue.Name = "lblInvalidValue";
            this.lblInvalidValue.Size = new System.Drawing.Size(13, 13);
            this.lblInvalidValue.TabIndex = 5;
            this.lblInvalidValue.Text = "0";
            // 
            // lblDuplicatesValue
            // 
            this.lblDuplicatesValue.AutoSize = true;
            this.lblDuplicatesValue.Location = new System.Drawing.Point(107, 55);
            this.lblDuplicatesValue.Name = "lblDuplicatesValue";
            this.lblDuplicatesValue.Size = new System.Drawing.Size(13, 13);
            this.lblDuplicatesValue.TabIndex = 4;
            this.lblDuplicatesValue.Text = "0";
            // 
            // lblDuplicates
            // 
            this.lblDuplicates.AutoSize = true;
            this.lblDuplicates.Location = new System.Drawing.Point(7, 55);
            this.lblDuplicates.Name = "lblDuplicates";
            this.lblDuplicates.Size = new System.Drawing.Size(94, 13);
            this.lblDuplicates.TabIndex = 3;
            this.lblDuplicates.Text = "Duplicate Tokens:";
            // 
            // lblInvalid
            // 
            this.lblInvalid.AutoSize = true;
            this.lblInvalid.Location = new System.Drawing.Point(7, 42);
            this.lblInvalid.Name = "lblInvalid";
            this.lblInvalid.Size = new System.Drawing.Size(80, 13);
            this.lblInvalid.TabIndex = 2;
            this.lblInvalid.Text = "Invalid Tokens:";
            // 
            // lblValid
            // 
            this.lblValid.AutoSize = true;
            this.lblValid.Location = new System.Drawing.Point(7, 29);
            this.lblValid.Name = "lblValid";
            this.lblValid.Size = new System.Drawing.Size(72, 13);
            this.lblValid.TabIndex = 1;
            this.lblValid.Text = "Valid Tokens:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(7, 16);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(73, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total Tokens:";
            // 
            // btnTranslate
            // 
            this.btnTranslate.Enabled = false;
            this.btnTranslate.Location = new System.Drawing.Point(12, 178);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(222, 23);
            this.btnTranslate.TabIndex = 2;
            this.btnTranslate.Text = "Translate to *.prof";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.BtnTranslate_Click);
            // 
            // bwTranslate
            // 
            this.bwTranslate.WorkerReportsProgress = true;
            this.bwTranslate.WorkerSupportsCancellation = true;
            this.bwTranslate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwTranslate_DoWork);
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(12, 207);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(222, 13);
            this.pbMain.Step = 1;
            this.pbMain.TabIndex = 8;
            // 
            // Translator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 229);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbLogdel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Translator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Translator";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Translator_Load);
            this.gbLogdel.ResumeLayout(false);
            this.gbLogdel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogdel;
        private System.Windows.Forms.TextBox txtLogdel;
        private System.Windows.Forms.Button btnBrowseLogdel;
        private System.Windows.Forms.Button btnLoadDict;
        private System.Windows.Forms.OpenFileDialog ofdLogdel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblValid;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblInvalid;
        private System.Windows.Forms.Label lblDuplicates;
        private System.Windows.Forms.Label lblDuplicatesValue;
        private System.Windows.Forms.Label lblInvalidValue;
        private System.Windows.Forms.Label lblValidValue;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Button btnTranslate;
        private libbrhscgui.Components.AbortableBackgroundWorker bwTranslate;
        private System.Windows.Forms.ProgressBar pbMain;
    }
}