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
            this.barChart2 = new BRS.Components.BarChartGraphic();
            this.barChart1 = new BRS.Components.BarChartGraphic();
            this.barChart3 = new BRS.Components.BarChartGraphic();
            this.SuspendLayout();
            // 
            // barChart2
            // 
            this.barChart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barChart2.HighlightedValue = "";
            this.barChart2.HighlightNegativeValues = true;
            this.barChart2.Location = new System.Drawing.Point(485, 12);
            this.barChart2.Name = "barChart2";
            this.barChart2.Size = new System.Drawing.Size(659, 720);
            this.barChart2.TabIndex = 0;
            // 
            // barChart1
            // 
            this.barChart1.HighlightedValue = "";
            this.barChart1.HighlightNegativeValues = true;
            this.barChart1.Location = new System.Drawing.Point(12, 12);
            this.barChart1.Name = "barChart1";
            this.barChart1.Size = new System.Drawing.Size(467, 720);
            this.barChart1.TabIndex = 1;
            // 
            // barChart3
            // 
            this.barChart3.HighlightedValue = "";
            this.barChart3.HighlightNegativeValues = true;
            this.barChart3.Location = new System.Drawing.Point(1150, 12);
            this.barChart3.Name = "barChart3";
            this.barChart3.Size = new System.Drawing.Size(633, 720);
            this.barChart3.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1795, 734);
            this.Controls.Add(this.barChart3);
            this.Controls.Add(this.barChart1);
            this.Controls.Add(this.barChart2);
            this.Name = "MainForm";
            this.Text = "Bar Graph component - sample app";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BRS.Components.BarChartGraphic barChart2;
        private BRS.Components.BarChartGraphic barChart1;
        private BRS.Components.BarChartGraphic barChart3;
    }
}