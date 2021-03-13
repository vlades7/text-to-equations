using Laba_WPF_4.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestMathODE
{
    [TestClass]
    public class UnitTestMathODE
    {
        [TestMethod]
        public void TestMathODEvar1()
        {
            var calculator = new Calculator();
            var result = calculator.Solve(new[]
            {
                "t = 0",
                "tmax = 0.11",
                "n = 11",
                "y1_0 = 1",
                "y2_0 = 1",
                "y1' = y1 * (1 - sqrt(y1 * y1 + y2 * y2)) - y2",
                "y2' = y2 * (1 - sqrt(y1 * y1 + y2 * y2)) + y1"
            });

            var lastResult = result[result.Count - 1];

            Assert.AreEqual(Math.Round(0.84760621, 5), Math.Round(lastResult[0], 5));
            Assert.AreEqual(Math.Round(1.05808145, 5), Math.Round(lastResult[1], 5));
        }

        [TestMethod]
        public void TestMathODEvar2()
        {
            var calculator = new Calculator();
            var result = calculator.Solve(new[]
            {
                "t = 0",
                "tmax = 0.11",
                "n = 11",
                "y1_0 = 1",
                "y2_0 = 3",
                "y1' = 2 * (y1 - y1 * y2)",
                "y2' = -(y2 - y1 * y2)"
            });

            var lastResult = result[result.Count - 1];

            Assert.AreEqual(Math.Round(0.64709818, 5), Math.Round(lastResult[0], 5));
            Assert.AreEqual(Math.Round(2.93790763, 5), Math.Round(lastResult[1], 5));
        }

        [TestMethod]
        public void TestMathODEvar3()
        {
            var calculator = new Calculator();
            var result = calculator.Solve(new[]
            {
                "t = 0",
                "tmax = 0.11",
                "n = 11",
                "y1_0 = 3",
                "y2_0 = 0",
                "y3_0 = 0",
                "y1' = -y2 + (y1 * y3) / sqrt(y1 * y1 + y2 * y2)",
                "y2' = y1 - (y2 * y3) / sqrt(y1 * y1 + y2 * y2)",
                "y3' = y1 / sqrt(y1 * y1 + y2 * y2)"
            });

            var lastResult = result[result.Count - 1];

            Assert.AreEqual(Math.Round(2.98790008, 5), Math.Round(lastResult[0], 5));
            Assert.AreEqual(Math.Round(0.32911445, 5), Math.Round(lastResult[1], 5));
            Assert.AreEqual(Math.Round(0.10977901, 5), Math.Round(lastResult[2], 5));
        }
    }
}
