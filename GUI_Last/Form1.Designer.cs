namespace GUI_Last
{
    partial class Lattepanda_Ehealth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lattepanda_Ehealth));
            this.timerRenderGraph = new System.Windows.Forms.Timer(this.components);
            this.timerMqttPublish = new System.Windows.Forms.Timer(this.components);
            this.AutoConnect = new System.Windows.Forms.Timer(this.components);
            this.scottPlotUC3 = new ScottPlot.FormsPlot();
            this.scottPlotUC2 = new ScottPlot.FormsPlot();
            this.scottPlotUC1 = new ScottPlot.FormsPlot();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heartbeatDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblSPO2 = new System.Windows.Forms.Label();
            this.lblBPM = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerRenderGraph
            // 
            this.timerRenderGraph.Interval = 20;
            this.timerRenderGraph.Tick += new System.EventHandler(this.timerRenderGraph_Tick);
            // 
            // timerMqttPublish
            // 
            this.timerMqttPublish.Interval = 1000;
            this.timerMqttPublish.Tick += new System.EventHandler(this.timerMqttPublish_Tick);
            // 
            // AutoConnect
            // 
            this.AutoConnect.Enabled = true;
            this.AutoConnect.Interval = 10000;
            this.AutoConnect.Tick += new System.EventHandler(this.AutoConnect_Tick);
            // 
            // scottPlotUC3
            // 
            this.scottPlotUC3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC3.BackColor = System.Drawing.Color.White;
            this.scottPlotUC3.Location = new System.Drawing.Point(128, 387);
            this.scottPlotUC3.Name = "scottPlotUC3";
            this.scottPlotUC3.Size = new System.Drawing.Size(881, 175);
            this.scottPlotUC3.TabIndex = 2;
            // 
            // scottPlotUC2
            // 
            this.scottPlotUC2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC2.BackColor = System.Drawing.Color.White;
            this.scottPlotUC2.Location = new System.Drawing.Point(128, 201);
            this.scottPlotUC2.Name = "scottPlotUC2";
            this.scottPlotUC2.Size = new System.Drawing.Size(881, 180);
            this.scottPlotUC2.TabIndex = 1;
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC1.BackColor = System.Drawing.Color.White;
            this.scottPlotUC1.Location = new System.Drawing.Point(128, 27);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(881, 168);
            this.scottPlotUC1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.37304F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.62696F));
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.17189F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.49477F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1012, 565);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.menuStrip1, 2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.heartbeatDetectionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // heartbeatDetectionToolStripMenuItem
            // 
            this.heartbeatDetectionToolStripMenuItem.Name = "heartbeatDetectionToolStripMenuItem";
            this.heartbeatDetectionToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.heartbeatDetectionToolStripMenuItem.Text = "Heartbeat Detection";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTemp);
            this.panel2.Controls.Add(this.lblSPO2);
            this.panel2.Controls.Add(this.lblBPM);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 201);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 180);
            this.panel2.TabIndex = 6;
            // 
            // lblTemp
            // 
            this.lblTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(22, 88);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(46, 30);
            this.lblTemp.TabIndex = 5;
            this.lblTemp.Text = "0°C";
            // 
            // lblSPO2
            // 
            this.lblSPO2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSPO2.AutoSize = true;
            this.lblSPO2.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSPO2.ForeColor = System.Drawing.Color.Black;
            this.lblSPO2.Location = new System.Drawing.Point(33, 58);
            this.lblSPO2.Name = "lblSPO2";
            this.lblSPO2.Size = new System.Drawing.Size(49, 30);
            this.lblSPO2.TabIndex = 4;
            this.lblSPO2.Text = "0 %";
            // 
            // lblBPM
            // 
            this.lblBPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBPM.AutoSize = true;
            this.lblBPM.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblBPM.ForeColor = System.Drawing.Color.Black;
            this.lblBPM.Location = new System.Drawing.Point(9, 28);
            this.lblBPM.Name = "lblBPM";
            this.lblBPM.Size = new System.Drawing.Size(99, 30);
            this.lblBPM.TabIndex = 3;
            this.lblBPM.Text = "500 BPM";
            this.lblBPM.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 168);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 168);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Lattepanda_Ehealth
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1012, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lattepanda_Ehealth";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerRenderGraph;
        private System.Windows.Forms.Timer timerMqttPublish;
        private System.Windows.Forms.Timer AutoConnect;
        private ScottPlot.FormsPlot scottPlotUC3;
        private ScottPlot.FormsPlot scottPlotUC2;
        private ScottPlot.FormsPlot scottPlotUC1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heartbeatDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBPM;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSPO2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
    }
}

