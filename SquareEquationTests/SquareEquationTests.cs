using Xunit;
using SquareEquationLib;

namespace SquareEquationLib.UnitTests
{
    public class SquareEquation_IsEquationOK
    {
        public double eps = 1e-5;

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
        public void IsABC_NaNorInf_CatchArgExc(double a, double b, double c)
        {
            var squareq = new SquareEquation();
            Assert.ThrowsAny<System.ArgumentException>(() => squareq.Solve(a,b,c));
        }

        [Theory]
        [InlineData(1, 2, 1)]
        public void IsCountOfRoots_1_ReturnTrue(double a, double b, double c)
        {
            double expected = -1;
            var squareq = new SquareEquation();
            double[] result = squareq.Solve(a, b, c);
            bool compare = Math.Abs(result[0] - expected) < eps;
            bool compare_size = (result.Length == 1);
            Assert.True(compare&&compare_size);
        }

        [Theory]
        [InlineData(1, 5, 10)]
        public void IsCountOfRoots_0_ReturnTrue(double a, double b, double c)
        {

            var squareq = new SquareEquation();
            double result = squareq.Solve(a, b, c).Length;
            double expected = 0;
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 3, 2)]
        public void IsCountOfRoots_2_ReturnTrue(double a, double b, double c)
        {
            double[] expected = new double[2]{-2, -1};
            var squareq = new SquareEquation();
            double[] result = squareq.Solve(a, b, c);
            bool compare_r1 = Math.Abs(result[0] - expected[0]) < eps;
            bool compare_r2 = Math.Abs(result[1] - expected[1]) < eps;
            bool compare_size = (result.Length == 2);
            Assert.True(compare_r1&&compare_r2&&compare_size);
        }

    }
}