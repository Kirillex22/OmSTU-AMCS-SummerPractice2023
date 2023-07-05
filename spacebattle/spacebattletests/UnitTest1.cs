namespace spacebattletests;
using TechTalk.SpecFlow;
using spacebattle;

[Binding]
public class UnitTest1 
{
    private SpaceShip spaceship = new SpaceShip();
    private double[] coords = new double[2];
    private double yaw;
    private double fuel;
    private Exception exception = new Exception();

    
    [Given(@"космический корабль находится в точке пространства с координатами \((.*), (.*)\)")]
    public void УстановкаКоординат(string x, string y)
    {
        spaceship.SetCoords(double.Parse(x), double.Parse(y));
        spaceship.Refuel(2);
        spaceship.SetFuelUnit(1);
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

    [Given("космический корабль имеет топливо в объеме (.*) ед")]
    public void Заправка(string fuel)
    {
        spaceship.Refuel(double.Parse(fuel));
        spaceship.SetCoords(0,0);
        spaceship.SetSpeed(1,1);

    }

    [Given("имеет скорость расхода топлива при движении (.*) ед")]
    public void РасходТоплива(string fuel_unit)
    {
        spaceship.SetFuelUnit(double.Parse(fuel_unit));
    }

    [Given("космический корабль имеет угол наклона 45 град к оси OX")]
    public void Рыскание()
    {
        spaceship.SetYaw(45);
    }

    [Given("имеет мгновенную угловую скорость 45 град")]
    public void УгловаяСкорость()
    {
        spaceship.SetYawSpeed(45);
    }

    [Given("космический корабль, угол наклона которого невозможно определить")]
    public void РысканиеНеОпределено()
    {
        spaceship.SetYaw(double.NaN);
    }

    [Given("мгновенную угловую скорость невозможно определить")]
    public void УгловаяСкоростьНеОпределена()
    {
        spaceship.SetYawSpeed(double.NaN);
    }

    [Given("невозможно изменить уголд наклона к оси OX космического корабля")]
    public void НевозможноИзменитьРыскание()
    {
        spaceship.SetSpinStatus(false);
    }

    [When("происходит прямолинейное равномерное движение без деформации")]
    public void КорабльДвижется()
    {
        try
        {
            var newcoords = spaceship.Move();
            coords[0] = newcoords[0];
            coords[1] = newcoords[1];
            
            fuel = spaceship.GetFuelCount();
        }

        catch(Exception exception)
        {
            this.exception = exception;
        }
        
    }

    [When("происходит вращение вокруг собственной оси")]
    public void КорабльРыскает()
    {
        try
        {
            this.yaw = spaceship.Spin();
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

    [Then("угол наклона космического корабля к оси OX составляет (.*) град")]
    public void НовоеРыскание(string yaw)
    {
        var expected = double.Parse(yaw);
        Assert.True(this.yaw == expected);
    }

    [Then("новый объем топлива космического корабля равен (.*) ед")]
    public void ЗапасТоплива(string fuel)
    {
        var expected = double.Parse(fuel);
        Assert.True(this.fuel == expected);
    }

}