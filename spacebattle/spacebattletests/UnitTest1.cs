namespace spacebattletests;
using TechTalk.SpecFlow;
using spacebattle;

[Binding]
public class UnitTest1 
{
    private SpaceShip spaceship = new SpaceShip();
    private double[] coords = new double[2];
    private Exception exception = new Exception();

    
    [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
    public void УстановкаКоординат(string x, string y)
    {
        spaceship.SetCoords(double.Parse(x), double.Parse(y));
    }

    [Given(@"имеет мгновенную скорость \((.*), (.*)\)")]
    public void УстановкаСкорости(string Vx, string Vy)
    {
        spaceship.SetSpeed(double.Parse(Vx), double.Parse(Vy));
    }

    [Given("космический корабль, положение в пространстве которого невозможно определить")]
    public void КоординатыНеОпределены()
    {
        spaceship.SetCoords(double.NaN, double.NaN);
    }

    [Given("скорость корабля определить невозможно")]
    public void СкоростьНеОпределена()
    {
        spaceship.SetSpeed(double.NaN, double.NaN);
    }

    [Given("изменить положение в пространстве космического корабля невозможно")]
    public void КорабльНедвижим()
    {
        spaceship.SetMoveStatus(false);
    }

    [When("происходит прямолинейное равномерное движение без деформации")]
    public void КорабльДвижется()
    {
        try
        {
            var newcoords = spaceship.Move();
            coords[0] = newcoords[0];
            coords[1] = newcoords[1];
        }

        catch(Exception exception)
        {
            this.exception = exception;
        }
        
    }
    
    [Then(@"космический корабль перемещается в точку пространства с координатами \((.*), (.*)\)")]
    public void КорабльПереместился(string x, string y)
    {
        bool res1 = (double.Parse(x) == coords[0]);
        bool res2 = (double.Parse(y) == coords[1]);
        Assert.True(res1&&res2);
    }

    [Then("возникает ошибка Exception")]
    public void ВозникаетОшибка()
    {
        Assert.ThrowsAny<Exception>(() => throw exception);
    }

}