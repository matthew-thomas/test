using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoolCalc.Core.Test.Unit
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public 
        void
        Calc_EmptyString_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var calculator        = new Calculator();
            var input             = string.Empty;
            var exceptionReceived = false;

            // Act
            try
            {
                calculator.Calc(input);
            }
            catch (ArgumentOutOfRangeException)
            {
                exceptionReceived = true;
            }

            // Assert
            Assert.IsTrue(exceptionReceived);
        }

        [TestMethod]
        public
        void
        Calc_EmptyString_ThrowsExceptionWithParameterNameInMessage()
        {
            // Arrange
            var calculator        = new Calculator();
            var input             = string.Empty;
            var exceptionMessage  = string.Empty;

            // Act
            try
            {
                calculator.Calc(input);
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
            }

            // Assert
            Assert.IsTrue(exceptionMessage.Contains("input"));
        }

        [TestMethod]
        public void Calc_AddTwoNumbers_ReturnsResult()
        {
            // Arrange
            var calculator = new Calculator();
            var input      = "5+5";

            // Act
            var result = calculator.Calc(input);

            // Assert
            Assert.AreEqual(10, result);
        }
    }
}
