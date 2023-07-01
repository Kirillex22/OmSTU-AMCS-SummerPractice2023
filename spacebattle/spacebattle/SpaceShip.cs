namespace spacebattle;

public class SpaceShip
{
    private double x; 
    private double y; 
    private double Vx; 
    private double Vy; 
    private bool status = true;

    public void SetCoords(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetSpeed(double Vx, double Vy)
    {
        this.Vx = Vx;
        this.Vy = Vy;
    }

    public void SetMoveStatus(bool status)
    {
        this.status = status;
    }

    public double[] Move(double t = 1)
    {
        if ((x == double.NaN)||(y == double.NaN))
            throw new Exception();

        else if ((Vx == double.NaN)||(Vy == double.NaN))
            throw new Exception();

        else if(!status)
            throw new Exception();

        x += Vx*t;
        y += Vy*t;

        var CurrCoords = new double[] {x, y};

        return CurrCoords;
        
    }

}
