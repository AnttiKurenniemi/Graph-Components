using System.Net;
using System.Text.Json;

namespace Graphing_Component_Sample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Some data for the graphic:
            barChart1.ShowAverageLine = true;
            barChart1.ShowMovingAverageLine = true;
            barChart1.AssignValue("First", 10);
            barChart1.AssignValue("Second", 20);
            barChart1.PositiveFillColor = Color.Green;


            barChart2.ShowAverageLine = true;
            barChart2.ShowMovingAverageLine = true;
            barChart2.AssignValue("First", 10);
            barChart2.AssignValue("Second", 23);
            barChart2.AssignValue("Third", 70);
            barChart2.AssignValue("Fourth", 250);
            barChart2.AssignValue("Fifth", 90);
            barChart2.AssignValue("Sixth", 5);
            barChart2.AssignValue("Seventh", -25);
            barChart2.AssignValue("Eight", -50);
            barChart2.AssignValue("Ninth", -5);
            barChart2.AssignValue("Tenth", 0);

            barChart3.ShowAverageLine = true;
            barChart3.ShowMovingAverageLine = true;
            barChart3.AssignValue("First", -100);
            barChart3.AssignValue("Second", -150);

            barChart1.Redraw();
            barChart2.Redraw();
            barChart3.Redraw();
        }
    }
}