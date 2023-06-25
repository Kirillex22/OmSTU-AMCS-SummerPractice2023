using Xunit;
using SquareEquationLib;

namespace SquareEquationLib.UnitTests
{
    public class SquareEquation_IsEquationOK
    {
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(double.NaN, 1, 1)]
        [InlineData(double.PositiveInfinity, 1, 1)]
        [InlineData(double.NegativeInfinity, 1, 1)]
        [InlineData(1, double.NaN, 1)]
        [InlineData(1, double.PositiveInfinity, 1)]
        [InlineData(1, double.NegativeInfinity, 1)]
        [InlineData(1, 1, double.NaN)]
        [InlineData(1, 1, double.PositiveInfinity)]
        [InlineData(1, 1, double.NegativeInfinity)]
        public void IsABC_NaNorInf_ReturnTrue(double a, double b, double c)
        {
            Assert.ThrowsAny<System.ArgumentException>(() => SquareEquation.Solve(a,b,c));
        }

        [Theory]
        [InlineData(1, 2, 1)]
        public void IsCountOfRoots_1_ReturnTrue(double a, double b, double c)
        {
            double[] result = SquareEquation.Solve(a, b, c);
            Assert.Equal(result.Length, 1);
        }

        [Theory]
        [InlineData(1, 5, 10)]
        public void IsCountOfRoots_0_ReturnTrue(double a, double b, double c)
        {
            double[] result = SquareEquation.Solve(a, b, c);
            Assert.Equal(result.Length, 0);
        }

        [Theory]
        [InlineData(1, 3, 1)]
        public void IsCountOfRoots_2_ReturnTrue(double a, double b, double c)
        {
            double[] result = SquareEquation.Solve(a, b, c);
            Assert.Equal(result.Length, 2);
        }


    }
}