using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NRS.Components.Tests
{
    [TestClass()]
    public class BarChartGraphicTests
    {


        [TestMethod()]
        public void GetMinMaxValues_returns_correct_values()
        {
            BarChartGraphic graphic = new BarChartGraphic();
            graphic.AssignValue("first", 7);
            graphic.AssignValue("second", 100);
            graphic.AssignValue("first", 3);
            graphic.AssignValue("second", 500);
            graphic.AssignValue("first", 8);
            graphic.AssignValue("third", 2);

            //graphic.GetMinMaxValues();

            //Assert.AreEqual(2, graphic.MinimumValue);
            //Assert.AreEqual(500, graphic.MaximumValue);
        }

    }
}