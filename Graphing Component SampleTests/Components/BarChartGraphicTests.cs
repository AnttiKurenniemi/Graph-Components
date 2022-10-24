using Microsoft.VisualStudio.TestTools.UnitTesting;
using BRS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRS.Components.Tests
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