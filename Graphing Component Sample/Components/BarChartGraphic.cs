using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NRS.Components
{
    /// <summary>
    /// Bar chart graphic control. Can draw one or more bar graphs displaying values below the chart area.
    /// </summary>
    public partial class BarChartGraphic : UserControl
    {
        private double MinimumValue = 0;
        private double MaximumValue  = 0;

        private double HeightMultiplier = 1;
        private int YAxisTextHeight = 50;

        /// <summary>Zero-line Y coordinate. At the bottom if only positive values; at top if only negatives and somewhere
        /// in the middle if both values exist</summary>
        int zeroLineY = 0;



        // Colors:
        readonly Color backgroundColor = Color.White;
        readonly Pen borderPen_Dark = new Pen(Color.DarkGray, 1);


        private Brush positiveFillBrush;
        private Brush negativeFillBrush;

        // Main bar drawing color:
        private Color _positiveFillColor = Color.Blue;
        [Description("Fill color for a postive value bar. Default is Blue."), Category("Appearance")]
        public Color PositiveFillColor 
        {
            get { return _positiveFillColor; }
            set { _positiveFillColor = value; positiveFillBrush = new SolidBrush(value); } 
        }

        private Color _negativeFillColor = Color.DarkRed;
        [Description("Fill color for a negative value bar. Default is DarkRed."), Category("Appearance")]
        public Color NegativeFillColor 
        {
            get { return _negativeFillColor; }
            set { _negativeFillColor = value; negativeFillBrush = new SolidBrush(value); } 
        }

        readonly Brush highlightedBarFillBrush = new SolidBrush(Color.SeaGreen);
        readonly Brush backgroundFillBrush = new SolidBrush(Color.LightGray);

        // Legend:
        readonly Brush legendBackground = new SolidBrush(Color.LightYellow);
        readonly Pen legendBorder = new Pen(Color.Black, 1);
        readonly Brush legendTextBrush = new SolidBrush(Color.Black);
        readonly Font legendFont = new Font("Tahoma", 8);


        /// <summary>Flag to set if we want a selected key to be drawn with different color</summary>
        public bool HighlightNegativeValues { get; set; } = true;
        
        /// <summary>Highlighted value; if set and the value is found from the valueDictionary, fill with different color</summary>
        public string HighlightedValue { get; set; } = "";

        /// <summary>Internal list of key-value pairs to draw</summary>
        private Dictionary<string, double> valueDictionary = new Dictionary<string, double>();

        /// <summary>This value is used to determine if the legend (key) string can fit in the area it is drawn in</summary>
        private double LastDrawnLegendX;

        // Following variables are used by the double-buffered drawing:
        private bool initializationComplete;
        private bool isDisposing;
        private BufferedGraphicsContext backbufferContext = null!;
        private BufferedGraphics backbufferGraphics;
        private Graphics drawingGraphics;




        #region Double-buffered drawing system methods

        // For the constructor, don't want the CS8618 (backbufferContext must be nullable) warning:
#pragma warning disable CS8618
        public BarChartGraphic()
        {
            InitializeComponent();

            // Set the control style to double buffer.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Assign our buffer context.
            backbufferContext = BufferedGraphicsManager.Current;
            initializationComplete = true;

            RecreateBuffers();

            // Colors to Brushes:
            positiveFillBrush = new SolidBrush(_positiveFillColor);
            negativeFillBrush = new SolidBrush(_negativeFillColor);


            Redraw();
        }
#pragma warning restore CS8618



        private void RecreateBuffers()
        {
            // Check initialization has completed so we know backbufferContext has been assigned.
            // Also check that we aren't disposing or this could be invalid.
            if (!initializationComplete || isDisposing)
                return;

            // We recreate the buffer with a width and height of the control. The "+ 1" 
            // guarantees we never have a buffer with a width or height of 0. 
            backbufferContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            // Dispose of old backbufferGraphics (if one has been created already)
            if (backbufferGraphics != null)
                backbufferGraphics.Dispose();

            // Create new backbufferGrpahics that matches the current size of buffer.
            backbufferGraphics = backbufferContext.Allocate(this.CreateGraphics(),
                new Rectangle(0, 0, Math.Max(this.Width, 1), Math.Max(this.Height, 1)));

            // Assign the Graphics object on backbufferGraphics to "drawingGraphics" for easy reference elsewhere.
            drawingGraphics = backbufferGraphics.Graphics;

            // ...this is a good place to assign drawingGraphics.SmoothingMode if you want a better anti-aliasing technique.
            drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Invalidate the control so a repaint gets called somewhere down the line.
            this.Invalidate();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecreateBuffers();
            Redraw();
        }


        protected override void Dispose(bool disposing)
        {
            isDisposing = true;
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                // Must dispose of backbufferGraphics before we dispose of backbufferContext or there will be an exception.
                if (backbufferGraphics != null)
                    backbufferGraphics.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // If we've initialized the backbuffer properly, render it on the control. 
            // Otherwise, do just the standard control paint.
            if (!isDisposing && backbufferGraphics != null)
                backbufferGraphics.Render(e.Graphics);
        }
        #endregion



        /// <summary>
        /// Set a key-value pair for drawing. Create the key if it does not exist already, and assign value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AssignValue(string key, double value)
        {
            if (valueDictionary.ContainsKey(key))
            {
                valueDictionary[key] = value;
            }
            else
            {
                valueDictionary.Add(key, value);
            }
        }



        /// <summary>
        /// Clear the key-value pairs list completely.
        /// </summary>
        public void Clear()
        {
            valueDictionary.Clear();
        }



        /// <summary>
        /// Check the values and assign min and max values. Not that the MIN value is not yet used, it will be needed
        /// for negative values
        /// </summary>
        private void CalculateMinMaxValues()
        {
            if ((valueDictionary != null) && (valueDictionary.Count > 0))
            {
                MaximumValue = valueDictionary.Values.Max();
                if (MaximumValue < 0)
                    MaximumValue = 0;
                MinimumValue = valueDictionary.Values.Min();
                if (MinimumValue > 0)
                    MinimumValue = 0;
            }
            else
            {
                // Some abstract defaults
                MaximumValue = 100;
                MinimumValue = 0;
            }
        }


        /// <summary>
        /// Main drawing method.
        /// </summary>
        public void Redraw()
        {
            // Check that we are not disposing and otherwise everything checks out ok
            if (drawingGraphics == null)
                return;

            if ((Disposing) || (isDisposing))
                return;

            // Get the MAX and MIN values
            CalculateMinMaxValues();

            // Clear the background
            drawingGraphics.Clear(backgroundColor);

            // Calculate the text height to draw the legend below the graphic
            SizeF legendSize = drawingGraphics.MeasureString("XXX", legendFont);
            YAxisTextHeight = Convert.ToInt32(legendSize.Height) + 2;

            // Calculate the height multiplier:
            HeightMultiplier = (this.Height - (YAxisTextHeight + 1)) / (MaximumValue - MinimumValue);

            LastDrawnLegendX = -1;

            DrawGraph();


            // Borders:
            drawingGraphics.DrawLine(borderPen_Dark, 0, 0, this.Width - 1, 0);                               // Top left - top right
            drawingGraphics.DrawLine(borderPen_Dark, this.Width - 1, 0, this.Width - 1, this.Height - 1);    // top right - bottom right
            drawingGraphics.DrawLine(borderPen_Dark, 0, this.Height - 1, 0, 0);                             // bottom left - top left
            drawingGraphics.DrawLine(borderPen_Dark, 0, this.Height - 1, this.Width - 1, this.Height - 1);  // bottom left - bottom right

            this.Refresh();
        }



        /// <summary>
        /// Draw the bars.
        /// </summary>
        private void DrawGraph()
        {
            // Have anything to draw?
            if (valueDictionary == null || valueDictionary.Count < 1)
                return;

            // Horizontal line between legend area (bottom) and actual bars
            drawingGraphics.DrawLine(borderPen_Dark, 0, this.Height - (YAxisTextHeight + 1), this.Width - 1, this.Height - (YAxisTextHeight + 1));  


            // Width of a single bar (the actual bar + space on both sizes=
            int barAreaWidth = Convert.ToInt32((this.Width - 3) / valueDictionary.Count);

            // if MinimumValue < 0, draw "zero line"
            if (MinimumValue < 0)
                zeroLineY = Convert.ToInt32((this.Height - (YAxisTextHeight + 2)) + (MinimumValue * HeightMultiplier));
            if (zeroLineY < 0)
                zeroLineY = 0;

            // Loop over all values and draw a graph for each
            int i = 0;
            foreach (KeyValuePair<string, double> bar in valueDictionary)
            {
                int left = Convert.ToInt32((((this.Width - 3) / valueDictionary.Count) * i)) + 1 + i;
                Rectangle rct = new Rectangle(left, 1, barAreaWidth, this.Height - 2);
                DrawSingleBar(rct, bar.Key, bar.Value);
                i++;
            }

            // This is the "Zero line" between positive and negative values
            if (MinimumValue < 0)
                drawingGraphics.DrawLine(borderPen_Dark, 0, zeroLineY, this.Width, zeroLineY);
        }



        /// <summary>Draw a "bar" on the chart.</summary>
        /// <param name="rct">Rectangle of the area within which the current bar should be drawn INCLUDING the legend at the bottom</param>
        /// <param name="key">Identifier of the bar to be drawn</param>
        /// <param name="value">Value of the bar to be drawn</param>
        private void DrawSingleBar(Rectangle rct, string key, double value)
        {
            // Separate the bar area rectangle and the bottom legend area rectangle:
            Rectangle barRectangle = new Rectangle(rct.Left, rct.Top, rct.Width, rct.Height - YAxisTextHeight);
            Rectangle legendRectangle = new Rectangle(rct.Left, rct.Top + YAxisTextHeight + 2, rct.Width, rct.Height - barRectangle.Height);

            // Background of the drawn area:
            drawingGraphics.FillRectangle(backgroundFillBrush, barRectangle);


            // Calculate where the actual bar is to be drawn:
            double barLeft = barRectangle.Left + (barRectangle.Width / 4);    // Left is 0.25 from left edge
            double barWidth = barRectangle.Width / 2;                         // Width is half the width of the reserved area

            double barTop;
            double barHeight = (Math.Abs(value) * HeightMultiplier);

            if (value < 0)
            {
                // Negative bar, below the "zero line"
                barTop = zeroLineY + 1;
                barHeight--;
            }
            else
            {
                // Positive bar
                if (MinimumValue < 0)
                    barTop = zeroLineY - (value * HeightMultiplier);
                else
                    barTop = barRectangle.Height - (value * HeightMultiplier) + 1;
            }

            Rectangle filledRect = new Rectangle(Convert.ToInt32(barLeft),
                Convert.ToInt32(barTop),
                Convert.ToInt32(barWidth),
                Convert.ToInt32(barHeight));

            // This is the actual bar:
            if (key == HighlightedValue)
                drawingGraphics.FillRectangle(highlightedBarFillBrush, filledRect);
            else if (value < 0 && HighlightNegativeValues)
                drawingGraphics.FillRectangle(negativeFillBrush, filledRect);
            else
                drawingGraphics.FillRectangle(positiveFillBrush, filledRect);


            // Draw the legend (keys) below the grapg
            DrawLegend(key, value, barRectangle, legendRectangle, barTop);
        }


        private bool LegendFits(double leftEdge)
        {
            // If desired left edge is more to the right than last drawn value, it should fit:
            if (leftEdge > LastDrawnLegendX)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Draw legend below the grapg
        /// </summary>
        private void DrawLegend(string key, double value, Rectangle barRectangle, Rectangle legendRectangle, double barTop)
        {
            string valueString = value.ToString("0.00");
            SizeF valueStringSize = new SizeF(drawingGraphics.MeasureString(valueString, legendFont));

            // Top should be the top of the bar:
            double legendLeft = (barRectangle.Width / 2) - (valueStringSize.Width / 2) + barRectangle.Left;
            double legendTop = barTop + 5;   // Slight padding to show the bar behind the legend

            if (MinimumValue >= 0)
            {
                if ((legendTop + valueStringSize.Height) > barRectangle.Height)
                    legendTop = barRectangle.Height - valueStringSize.Height;
            }

            // For positive small values, the legend should stay on top of the "zero line"
            if (value >= 0 && MinimumValue < 0)
            {
                if (legendTop + valueStringSize.Height + 2 > zeroLineY)
                {
                    // Adjust accordingly
                    legendTop = zeroLineY - (valueStringSize.Height + 2);
                }
            }

            // Value rectangle:
            Rectangle txtRect = new Rectangle(Convert.ToInt32(legendLeft),
                Convert.ToInt32(legendTop),
                Convert.ToInt32(valueStringSize.Width),
                Convert.ToInt32(valueStringSize.Height));
            drawingGraphics.FillRectangle(legendBackground, txtRect);
            drawingGraphics.DrawRectangle(legendBorder, txtRect);

            // Value string:
            drawingGraphics.DrawString(valueString, legendFont, legendTextBrush, txtRect.Left, txtRect.Top);


            // Legend text at the bottom, below the bars:
            SizeF keyStringSize = new SizeF(drawingGraphics.MeasureString(key, legendFont));
            double keyStringLeft = (barRectangle.Width / 2) - (keyStringSize.Width / 2) + barRectangle.Left;
            double keyStringTop = (legendRectangle.Height / 2) - (keyStringSize.Height / 2) + barRectangle.Height + 5;

            // Only draw the "Key" if it fits
            if (LegendFits(keyStringLeft))
            {
                LastDrawnLegendX = keyStringLeft + keyStringSize.Width;

                // Rectangle (border) inside which the Key (name) is drawn:
                Rectangle keyRect = new Rectangle(Convert.ToInt32(keyStringLeft),
                    Convert.ToInt32(keyStringTop),
                    Convert.ToInt32(keyStringSize.Width),
                    Convert.ToInt32(keyStringSize.Height));

                // Key (name of the bar) string:
                drawingGraphics.DrawString(key, legendFont, legendTextBrush, keyRect.Left, keyRect.Top);
            }
        }
    }
}
