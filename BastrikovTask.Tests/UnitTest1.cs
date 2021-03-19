using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BastrikovTask.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void runreturned()
        {
            // arrange
            double[,] matrix = new double[3, 3] { { 0d, 29d, 3d }, { 2d, 0d, 10d }, { 15d, 12d, 0d } };
            double expected = 12;

            // act
            BastrikovTask.Matrix target = new Matrix();
            target.setTargetMatrix(matrix);
            double actual = target.run(0);
            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
