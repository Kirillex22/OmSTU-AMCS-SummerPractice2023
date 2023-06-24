namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        double Epsilon = 1e-5;
        if (Math.Abs(a)<Epsilon)
            throw new System.ArgumentException();
        else if (double.IsNaN(a)||(double.IsPositiveInfinity(a))||(double.IsNegativeInfinity(a)))
            throw new ArgumentException();
        else if (double.IsNaN(b)||(double.IsPositiveInfinity(b))||(double.IsNegativeInfinity(b)))
            throw new ArgumentException();
        else if (double.IsNaN(c)||(double.IsPositiveInfinity(c))||(double.IsNegativeInfinity(c)))
            throw new ArgumentException();

        double D = Math.Pow(b,2)-4*a*c;

        if (D >= Epsilon){
            var roots1 = new double[2];
            double x1 = -(b + Math.Sign(b)*Math.Sqrt(D))/2;
            double x2 = c/x1;
            roots1[0] = x1;
            roots1[1] = x2;
            return roots1;
        }
        else if (Math.Abs(D) < Epsilon){
            var roots2 = new double[1];
            double x1 = -(b + Math.Sign(b)*Math.Sqrt(D))/2;
            roots2[0] = x1;
            return roots2;
        }
        else{
            var roots = new double[]{};
            return roots;
        }
    }
}
