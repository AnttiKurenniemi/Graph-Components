namespace Graphing_Component_Sample
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.barChart2 = new NRS.Components.BarChartGraphic();
            this.barChart1 = new NRS.Components.BarChartGraphic();
            this.barChart3 = new NRS.Components.BarChartGraphic();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // barChart2
            // 
            this.barChart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barChart2.HighlightedValue = "";
            this.barChart2.HighlightNegativeValues = true;
            this.barChart2.Location = new System.Drawing.Point(553, 46);
            this.barChart2.Name = "barChart2";
            this.barChart2.Size = new System.Drawing.Size(659, 686);
            this.barChart2.TabIndex = 0;
            // 
            // barChart1
            // 
            this.barChart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.barChart1.HighlightedValue = "";
            this.barChart1.HighlightNegativeValues = true;
            this.barChart1.Location = new System.Drawing.Point(12, 46);
            this.barChart1.Name = "barChart1";
            this.barChart1.Size = new System.Drawing.Size(416, 686);
            this.barChart1.TabIndex = 1;
            // 
            // barChart3
            // 
            this.barChart3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barChart3.HighlightedValue = "";
            this.barChart3.HighlightNegativeValues = true;
            this.barChart3.Location = new System.Drawing.Point(1348, 46);
            this.barChart3.Name = "barChart3";
            this.barChart3.Size = new System.Drawing.Size(435, 686);
            this.barChart3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "A chart with only positive values";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(553, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Positive and negative values";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1348, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Only negative values";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1795, 734);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barChart3);
            this.Controls.Add(this.barChart1);
            this.Controls.Add(this.barChart2);
            this.Name = "MainForm";
            this.Text = "Bar Graph component - sample app";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NRS.Components.BarChartGraphic barChart2;
        private NRS.Components.BarChartGraphic barChart1;
        private NRS.Components.BarChartGraphic barChart3;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}