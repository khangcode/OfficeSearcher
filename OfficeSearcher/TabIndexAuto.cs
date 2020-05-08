namespace OfficeSearcher
{
    partial class TabIndex
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
            this.label1 = new System.Windows.Forms.Label();
            this.BtnIndexFrom = new System.Windows.Forms.Button();
            this.BtnIndexAll = new System.Windows.Forms.Button();
            this.TxtStatus = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtIndexFolder = new System.Windows.Forms.TextBox();
            this.BtnSelectFolder = new System.Windows.Forms.Button();
            this.DPickFrom = new System.Windows.Forms.DateTimePicker();
            this.BgWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Index from";
            // 
            // BtnIndexFrom
            // 
            this.BtnIndexFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnIndexFrom.Location = new System.Drawing.Point(588, 9);
            this.BtnIndexFrom.Name = "BtnIndexFrom";
            this.BtnIndexFrom.Size = new System.Drawing.Size(75, 23);
            this.BtnIndexFrom.TabIndex = 2;
            this.BtnIndexFrom.Text = "Index From";
            this.BtnIndexFrom.UseVisualStyleBackColor = true;
            this.BtnIndexFrom.Click += new System.EventHandler(this.BtnIndexFrom_Click);
            // 
            // BtnIndexAll
            // 
            this.BtnIndexAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnIndexAll.Location = new System.Drawing.Point(669, 9);
            this.BtnIndexAll.Name = "BtnIndexAll";
            this.BtnIndexAll.Size = new System.Drawing.Size(75, 23);
            this.BtnIndexAll.TabIndex = 3;
            this.BtnIndexAll.Text = "Index All";
            this.BtnIndexAll.UseVisualStyleBackColor = true;
            this.BtnIndexAll.Click += new System.EventHandler(this.BtnIndexAll_Click);
            // 
            // TxtStatus
            // 
            this.TxtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtStatus.Location = new System.Drawing.Point(16, 46);
            this.TxtStatus.Multiline = true;
            this.TxtStatus.Name = "TxtStatus";
            this.TxtStatus.ReadOnly = true;
            this.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtStatus.Size = new System.Drawing.Size(728, 290);
            this.TxtStatus.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder";
            // 
            // TxtIndexFolder
            // 
            this.TxtIndexFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtIndexFolder.Location = new System.Drawing.Point(66, 357);
            this.TxtIndexFolder.Name = "TxtIndexFolder";
            this.TxtIndexFolder.ReadOnly = true;
            this.TxtIndexFolder.Size = new System.Drawing.Size(597, 20);
            this.TxtIndexFolder.TabIndex = 6;
            // 
            // BtnSelectFolder
            // 
            this.BtnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectFolder.Location = new System.Drawing.Point(679, 354);
            this.BtnSelectFolder.Name = "BtnSelectFolder";
            this.BtnSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectFolder.TabIndex = 7;
            this.BtnSelectFolder.Text = "Select Folder";
            this.BtnSelectFolder.UseVisualStyleBackColor = true;
            this.BtnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // DPickFrom
            // 
            this.DPickFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DPickFrom.Location = new System.Drawing.Point(382, 12);
            this.DPickFrom.Name = "DPickFrom";
            this.DPickFrom.Size = new System.Drawing.Size(200, 20);
            this.DPickFrom.TabIndex = 8;
            // 
            // BgWorker1
            // 
            this.BgWorker1.WorkerReportsProgress = true;
            this.BgWorker1.WorkerSupportsCancellation = true;
            this.BgWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker1_DoWork);
            this.BgWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker1_ProgressChanged);
            this.BgWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker1_RunWorkerCompleted);
            // 
            // FrmIndex
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 395);
            this.Controls.Add(this.DPickFrom);
            this.Controls.Add(this.BtnSelectFolder);
            this.Controls.Add(this.TxtIndexFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtStatus);
            this.Controls.Add(this.BtnIndexAll);
            this.Controls.Add(this.BtnIndexFrom);
            this.Controls.Add(this.label1);
            this.Name = "FrmIndex";
            this.Text = "FrmIndex";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnIndexFrom;
        private System.Windows.Forms.Button BtnIndexAll;
        private System.Windows.Forms.TextBox TxtStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtIndexFolder;
        private System.Windows.Forms.Button BtnSelectFolder;
        private System.Windows.Forms.DateTimePicker DPickFrom;
        private System.ComponentModel.BackgroundWorker BgWorker1;
    }
}