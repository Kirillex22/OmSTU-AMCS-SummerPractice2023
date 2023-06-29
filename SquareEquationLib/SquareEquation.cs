namespace SquareEquationLib;

public class SquareEquation
{
    public double[] Solve(double a, double b, double c)
    {
        double Epsilon = 1e-5;
        if (Math.Abs(a)<Epsilon)
            throw new System.ArgumentException();
        else if (double.IsNaN(a)||(double.IsPositiveInfinity(a))||(double.IsNegativeInfinity(a)))
            throw new System.ArgumentException();
        else if (double.IsNaN(b)||(double.IsPositiveInfinity(b))||(double.IsNegativeInfinity(b)))
            throw new System.ArgumentException();
        else if (double.IsNaN(c)||(double.IsPositiveInfinity(c))||(double.IsNegativeInfinity(c)))
            throw new System.ArgumentException();

        double D = Math.Pow(b,2)-4*a*c;

        if (D >= Epsilon){
            if (Math.Abs(b) > Epsilon)
            {
                var roots1 = new double[2];
                double x1 = -(b + Math.Sign(b)*Math.Sqrt(D))/2*a;
                double x2 = c/x1;
                roots1[0] = x1;
                roots1[1] = x2;
                return roots1;
            }
            else
            {
                var roots1 = new double[2];
                double x1 = -(b + Math.Sqrt(D))/2*a;
                double x2 = -(b - Math.Sqrt(D))/2*a;
                roots1[0] = x1;
                roots1[1] = x2;
                return roots1;
            } 
        }
        else if (Math.Abs(D) < Epsilon){
            if (Math.Abs(b) > Epsilon)
            {
                var roots2 = new double[1];
                double x1 = -(b + Math.Sign(b)*Math.Sqrt(D))/2*a;
                roots2[0] = x1;
                return roots2;
            }
            else
            {
                var roots2 = new double[1];
                double x1 = -b/2*a;
                roots2[0] = x1;
                return roots2;
            }
            
        }
        else{
            var roots = new double[]{};
            return roots;
        }
    }
}

