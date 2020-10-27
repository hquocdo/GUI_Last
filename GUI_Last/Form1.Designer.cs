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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.scottPlotUC1 = new ScottPlot.FormsPlot();
            this.scottPlotUC2 = new ScottPlot.FormsPlot();
            this.scottPlotUC3 = new ScottPlot.FormsPlot();
            this.lblBPM = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.37304F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.62696F));
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.scottPlotUC3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblBPM, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.17189F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.49477F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1083, 574);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC1.Location = new System.Drawing.Point(137, 3);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(943, 178);
            this.scottPlotUC1.TabIndex = 0;
            // 
            // scottPlotUC2
            // 
            this.scottPlotUC2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC2.Location = new System.Drawing.Point(137, 187);
            this.scottPlotUC2.Name = "scottPlotUC2";
            this.scottPlotUC2.Size = new System.Drawing.Size(943, 192);
            this.scottPlotUC2.TabIndex = 1;
            // 
            // scottPlotUC3
            // 
            this.scottPlotUC3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scottPlotUC3.Location = new System.Drawing.Point(137, 385);
            this.scottPlotUC3.Name = "scottPlotUC3";
            this.scottPlotUC3.Size = new System.Drawing.Size(943, 186);
            this.scottPlotUC3.TabIndex = 2;
            // 
            // lblBPM
            // 
            this.lblBPM.AutoSize = true;
            this.lblBPM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBPM.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblBPM.Location = new System.Drawing.Point(3, 147);
            this.lblBPM.Name = "lblBPM";
            this.lblBPM.Size = new System.Drawing.Size(128, 37);
            this.lblBPM.TabIndex = 3;
            this.lblBPM.Text = "500 BPM";
            this.lblBPM.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Lattepanda_Ehealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 574);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Lattepanda_Ehealth";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.FormsPlot scottPlotUC1;
        private ScottPlot.FormsPlot scottPlotUC2;
        private ScottPlot.FormsPlot scottPlotUC3;
        private System.Windows.Forms.Label lblBPM;
    }
}

