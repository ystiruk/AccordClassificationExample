namespace NSL_KDD_GUI
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prepareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.plotsTabPage = new System.Windows.Forms.TabPage();
            this.scatterplotView = new Accord.Controls.ScatterplotView();
            this.label1 = new System.Windows.Forms.Label();
            this.featuresListBox = new System.Windows.Forms.ListBox();
            this.learnTabPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.testDataRadioButton = new System.Windows.Forms.RadioButton();
            this.trainDataRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.logRegLabel = new System.Windows.Forms.Label();
            this.nbLabel = new System.Windows.Forms.Label();
            this.logRegDataGridView = new System.Windows.Forms.DataGridView();
            this.nbDataGridView = new System.Windows.Forms.DataGridView();
            this.learnAllButton = new System.Windows.Forms.Button();
            this.nbZedGraphControl = new ZedGraph.ZedGraphControl();
            this.logRegZedGraphControl = new ZedGraph.ZedGraphControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.plotsTabPage.SuspendLayout();
            this.learnTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logRegDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(936, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dataMenuItem
            // 
            this.dataMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prepareToolStripMenuItem});
            this.dataMenuItem.Name = "dataMenuItem";
            this.dataMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataMenuItem.Text = "Data";
            // 
            // prepareToolStripMenuItem
            // 
            this.prepareToolStripMenuItem.Name = "prepareToolStripMenuItem";
            this.prepareToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.prepareToolStripMenuItem.Text = "Prepare";
            this.prepareToolStripMenuItem.Click += new System.EventHandler(this.prepareToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 675);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(936, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.plotsTabPage);
            this.mainTabControl.Controls.Add(this.learnTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 27);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(912, 645);
            this.mainTabControl.TabIndex = 2;
            // 
            // plotsTabPage
            // 
            this.plotsTabPage.Controls.Add(this.scatterplotView);
            this.plotsTabPage.Controls.Add(this.label1);
            this.plotsTabPage.Controls.Add(this.featuresListBox);
            this.plotsTabPage.Location = new System.Drawing.Point(4, 22);
            this.plotsTabPage.Name = "plotsTabPage";
            this.plotsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.plotsTabPage.Size = new System.Drawing.Size(904, 619);
            this.plotsTabPage.TabIndex = 0;
            this.plotsTabPage.Text = "Plots";
            this.plotsTabPage.UseVisualStyleBackColor = true;
            // 
            // scatterplotView
            // 
            this.scatterplotView.LinesVisible = false;
            this.scatterplotView.Location = new System.Drawing.Point(254, 19);
            this.scatterplotView.Name = "scatterplotView";
            this.scatterplotView.ScaleTight = false;
            this.scatterplotView.Size = new System.Drawing.Size(644, 589);
            this.scatterplotView.SymbolSize = 7F;
            this.scatterplotView.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Features";
            // 
            // featuresListBox
            // 
            this.featuresListBox.FormattingEnabled = true;
            this.featuresListBox.HorizontalScrollbar = true;
            this.featuresListBox.Location = new System.Drawing.Point(9, 19);
            this.featuresListBox.Name = "featuresListBox";
            this.featuresListBox.Size = new System.Drawing.Size(239, 589);
            this.featuresListBox.TabIndex = 0;
            this.featuresListBox.SelectedIndexChanged += new System.EventHandler(this.featuresListBox_SelectedIndexChanged);
            // 
            // learnTabPage
            // 
            this.learnTabPage.Controls.Add(this.logRegZedGraphControl);
            this.learnTabPage.Controls.Add(this.nbZedGraphControl);
            this.learnTabPage.Controls.Add(this.panel1);
            this.learnTabPage.Controls.Add(this.label2);
            this.learnTabPage.Controls.Add(this.logRegLabel);
            this.learnTabPage.Controls.Add(this.nbLabel);
            this.learnTabPage.Controls.Add(this.logRegDataGridView);
            this.learnTabPage.Controls.Add(this.nbDataGridView);
            this.learnTabPage.Controls.Add(this.learnAllButton);
            this.learnTabPage.Location = new System.Drawing.Point(4, 22);
            this.learnTabPage.Name = "learnTabPage";
            this.learnTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.learnTabPage.Size = new System.Drawing.Size(904, 619);
            this.learnTabPage.TabIndex = 1;
            this.learnTabPage.Text = "Learn";
            this.learnTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.testDataRadioButton);
            this.panel1.Controls.Add(this.trainDataRadioButton);
            this.panel1.Location = new System.Drawing.Point(172, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 23);
            this.panel1.TabIndex = 5;
            // 
            // testDataRadioButton
            // 
            this.testDataRadioButton.AutoSize = true;
            this.testDataRadioButton.Location = new System.Drawing.Point(82, 3);
            this.testDataRadioButton.Name = "testDataRadioButton";
            this.testDataRadioButton.Size = new System.Drawing.Size(70, 17);
            this.testDataRadioButton.TabIndex = 0;
            this.testDataRadioButton.TabStop = true;
            this.testDataRadioButton.Text = "Test data";
            this.testDataRadioButton.UseVisualStyleBackColor = true;
            this.testDataRadioButton.CheckedChanged += new System.EventHandler(this.dataRadioButton_CheckedChanged);
            // 
            // trainDataRadioButton
            // 
            this.trainDataRadioButton.AutoSize = true;
            this.trainDataRadioButton.Location = new System.Drawing.Point(3, 3);
            this.trainDataRadioButton.Name = "trainDataRadioButton";
            this.trainDataRadioButton.Size = new System.Drawing.Size(73, 17);
            this.trainDataRadioButton.TabIndex = 0;
            this.trainDataRadioButton.TabStop = true;
            this.trainDataRadioButton.Text = "Train data";
            this.trainDataRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "check on: ";
            // 
            // logRegLabel
            // 
            this.logRegLabel.AutoSize = true;
            this.logRegLabel.Location = new System.Drawing.Point(449, 51);
            this.logRegLabel.Name = "logRegLabel";
            this.logRegLabel.Size = new System.Drawing.Size(147, 13);
            this.logRegLabel.TabIndex = 3;
            this.logRegLabel.Text = "Multinomial logistic resgession";
            // 
            // nbLabel
            // 
            this.nbLabel.AutoSize = true;
            this.nbLabel.Location = new System.Drawing.Point(6, 51);
            this.nbLabel.Name = "nbLabel";
            this.nbLabel.Size = new System.Drawing.Size(67, 13);
            this.nbLabel.TabIndex = 2;
            this.nbLabel.Text = "Naive Bayes";
            // 
            // logRegDataGridView
            // 
            this.logRegDataGridView.AllowUserToAddRows = false;
            this.logRegDataGridView.AllowUserToDeleteRows = false;
            this.logRegDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logRegDataGridView.Location = new System.Drawing.Point(452, 67);
            this.logRegDataGridView.Name = "logRegDataGridView";
            this.logRegDataGridView.ReadOnly = true;
            this.logRegDataGridView.Size = new System.Drawing.Size(446, 246);
            this.logRegDataGridView.TabIndex = 1;
            // 
            // nbDataGridView
            // 
            this.nbDataGridView.AllowUserToAddRows = false;
            this.nbDataGridView.AllowUserToDeleteRows = false;
            this.nbDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nbDataGridView.Location = new System.Drawing.Point(6, 67);
            this.nbDataGridView.Name = "nbDataGridView";
            this.nbDataGridView.ReadOnly = true;
            this.nbDataGridView.Size = new System.Drawing.Size(440, 246);
            this.nbDataGridView.TabIndex = 1;
            // 
            // learnAllButton
            // 
            this.learnAllButton.Location = new System.Drawing.Point(6, 6);
            this.learnAllButton.Name = "learnAllButton";
            this.learnAllButton.Size = new System.Drawing.Size(96, 23);
            this.learnAllButton.TabIndex = 0;
            this.learnAllButton.Text = "Learn models";
            this.learnAllButton.UseVisualStyleBackColor = true;
            this.learnAllButton.Click += new System.EventHandler(this.learnAllButton_Click);
            // 
            // nbZedGraphControl
            // 
            this.nbZedGraphControl.Location = new System.Drawing.Point(6, 319);
            this.nbZedGraphControl.Name = "nbZedGraphControl";
            this.nbZedGraphControl.ScrollGrace = 0D;
            this.nbZedGraphControl.ScrollMaxX = 0D;
            this.nbZedGraphControl.ScrollMaxY = 0D;
            this.nbZedGraphControl.ScrollMaxY2 = 0D;
            this.nbZedGraphControl.ScrollMinX = 0D;
            this.nbZedGraphControl.ScrollMinY = 0D;
            this.nbZedGraphControl.ScrollMinY2 = 0D;
            this.nbZedGraphControl.Size = new System.Drawing.Size(440, 294);
            this.nbZedGraphControl.TabIndex = 6;
            this.nbZedGraphControl.UseExtendedPrintDialog = true;
            // 
            // logRegZedGraphControl
            // 
            this.logRegZedGraphControl.Location = new System.Drawing.Point(452, 319);
            this.logRegZedGraphControl.Name = "logRegZedGraphControl";
            this.logRegZedGraphControl.ScrollGrace = 0D;
            this.logRegZedGraphControl.ScrollMaxX = 0D;
            this.logRegZedGraphControl.ScrollMaxY = 0D;
            this.logRegZedGraphControl.ScrollMaxY2 = 0D;
            this.logRegZedGraphControl.ScrollMinX = 0D;
            this.logRegZedGraphControl.ScrollMinY = 0D;
            this.logRegZedGraphControl.ScrollMinY2 = 0D;
            this.logRegZedGraphControl.Size = new System.Drawing.Size(446, 294);
            this.logRegZedGraphControl.TabIndex = 7;
            this.logRegZedGraphControl.UseExtendedPrintDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 697);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "NSL KDD";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.plotsTabPage.ResumeLayout(false);
            this.plotsTabPage.PerformLayout();
            this.learnTabPage.ResumeLayout(false);
            this.learnTabPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logRegDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prepareToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage plotsTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox featuresListBox;
        private System.Windows.Forms.TabPage learnTabPage;
        private System.Windows.Forms.Button learnAllButton;
        private System.Windows.Forms.DataGridView nbDataGridView;
        private System.Windows.Forms.DataGridView logRegDataGridView;
        private System.Windows.Forms.Label logRegLabel;
        private System.Windows.Forms.Label nbLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton testDataRadioButton;
        private System.Windows.Forms.RadioButton trainDataRadioButton;
        private System.Windows.Forms.Label label2;
        private Accord.Controls.ScatterplotView scatterplotView;
        private ZedGraph.ZedGraphControl nbZedGraphControl;
        private ZedGraph.ZedGraphControl logRegZedGraphControl;
    }
}

