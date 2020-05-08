namespace OfficeSearcher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsBtnCloseAll = new System.Windows.Forms.ToolStripButton();
            this.TsClose = new System.Windows.Forms.ToolStripButton();
            this.TsBtnAbout = new System.Windows.Forms.ToolStripButton();
            this.TsBtnIndex = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new OfficeSearcher.TabFind();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsBtnCloseAll,
            this.TsClose,
            this.TsBtnAbout,
            this.TsBtnIndex});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsBtnCloseAll
            // 
            this.TsBtnCloseAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsBtnCloseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnCloseAll.Image = ((System.Drawing.Image)(resources.GetObject("TsBtnCloseAll.Image")));
            this.TsBtnCloseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnCloseAll.Name = "TsBtnCloseAll";
            this.TsBtnCloseAll.Size = new System.Drawing.Size(23, 22);
            this.TsBtnCloseAll.Text = "Close All";
            this.TsBtnCloseAll.Click += new System.EventHandler(this.TsBtnCloseAll_Click);
            // 
            // TsClose
            // 
            this.TsClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsClose.Image = ((System.Drawing.Image)(resources.GetObject("TsClose.Image")));
            this.TsClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsClose.Name = "TsClose";
            this.TsClose.Size = new System.Drawing.Size(23, 22);
            this.TsClose.Text = "Close";
            this.TsClose.Click += new System.EventHandler(this.TsClose_Click);
            // 
            // TsBtnAbout
            // 
            this.TsBtnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsBtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnAbout.Image = ((System.Drawing.Image)(resources.GetObject("TsBtnAbout.Image")));
            this.TsBtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnAbout.Name = "TsBtnAbout";
            this.TsBtnAbout.Size = new System.Drawing.Size(23, 22);
            this.TsBtnAbout.Text = "About";
            this.TsBtnAbout.Click += new System.EventHandler(this.TsBtnAbout_Click);
            // 
            // TsBtnIndex
            // 
            this.TsBtnIndex.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsBtnIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsBtnIndex.Image = ((System.Drawing.Image)(resources.GetObject("TsBtnIndex.Image")));
            this.TsBtnIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsBtnIndex.Name = "TsBtnIndex";
            this.TsBtnIndex.Size = new System.Drawing.Size(23, 22);
            this.TsBtnIndex.Text = "Index";
            this.TsBtnIndex.Click += new System.EventHandler(this.TsBtnIndex_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 436);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 410);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Find";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsBtnCloseAll;
        private System.Windows.Forms.ToolStripButton TsClose;
        private System.Windows.Forms.TabControl tabControl1;
        // private System.Windows.Forms.TabPage tabPage1;
        private TabFind tabPage1;
        private System.Windows.Forms.ToolStripButton TsBtnAbout;
        private System.Windows.Forms.ToolStripButton TsBtnIndex;
    }
}

