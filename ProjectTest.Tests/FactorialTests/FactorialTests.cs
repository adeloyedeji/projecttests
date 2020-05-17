using Xunit;
using ProjectTest.Libs;
using System;
using ProjectTest.Libs.Services;
using ProjectTest.Libs.Contracts;
using Moq;

namespace ProjectTest.Tests.FactorialTests
{
    public class FactorialTests
    {
        private readonly FactorialCalculator _factorialCalculator;
        private readonly Calculator _calculator;

        public FactorialTests()
        {
            _factorialCalculator = new FactorialCalculator();
            _calculator          = new Calculator();
        }

        [Fact]
        public void GetFactorial_ValidInput_ShouldSucceed()
        {
            //Arrange
            int num = 0;

            //Act
            var result = _factorialCalculator.GetFactorial(num);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        //[Fact]
        //public void GetFactorial_ValidInput_shouldReturnCorrectAnswer()
        //{
        //    //Arrange
        //    int num  = 5;

        //    //Act
        //    var result = _factorialCalculator.GetFactorial(num);

        //    //Assert
        //    Assert.Throws<Exception>(() => _factorialCalculator.GetFactorial(num));
        //    Assert.Equal(120, result);
        //}

        [Fact]
        public void Factorial_ValidInput_ShouldSucceed()
        {
            //Arrange
            var calculatorMock = new Mock<ICalculator>();
            calculatorMock.Setup(c => c.Factorial(It.IsAny<int>())).Returns(It.IsAny<int>());

            //Act
            var num = 0;
            var result = calculatorMock.Object.Factorial(num);

            //Assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public void Factorial_ValidInputNoMock_ShouldSucceed()
        {
            //Arrange 
            var num = 0;

            //Act
            var result = _calculator.Factorial(num);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }
    }
}
