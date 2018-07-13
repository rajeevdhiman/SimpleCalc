using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCalc.Models;

namespace SimpleCalcTests
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator _calculator;
        public CalculatorTests()
        {
            _calculator = new Calculator();
        }
        [TestMethod]
        public void Calculate_Expression_Returns1()
        {
            var expression = "1+2/3";

            var result = _calculator.Calculate(expression);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Calculate_Expression_Returns3_5()
        {
            var expression = "1/2+3";
            
            var result = _calculator.Calculate(expression);
            Assert.AreEqual(3.5, result);
        }

        [TestMethod]
        public void Calculate_Expression_Returns19()
        {
            var expression = "12+23+3/2";

            var result = _calculator.Calculate(expression);
            Assert.AreEqual(19, result);
        }

        [TestMethod]
        public void Calculate_ExpressionIsNull_ThrowsException()
        {
            string expression = "";
            Assert.ThrowsException<ArgumentNullException>(() => _calculator.Calculate(expression));
        }
    }
}
