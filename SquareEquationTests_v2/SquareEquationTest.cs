namespace XUnit.Coverlet.MSBuild;
using SquareEquationLib;
using TechTalk.SpecFlow;

[Binding]
public class UnitTest1
{

    private double eps = 1e-5;
    private double[] solution = new double[0];
    private double[] abc = new double[3];
    private Exception exception = new Exception();
    private SquareEquation squareq = new SquareEquation();

    [Given(@"Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
    public void ДаноКвадратноеУравнение(string a, string b, string c)
    {
        var s_abc = new string[] {a,b,c};

        for(int i = 0; i < 3; i++){
            if (s_abc[i] == "NaN")
                abc[i] = double.NaN;
            else if (s_abc[i] == "Double.PositiveInfinity")
                abc[i] = double.PositiveInfinity;
            else if (s_abc[i] == "Double.NegativeInfinity")
                abc[i] = double.NegativeInfinity;
            else
                abc[i] = double.Parse(s_abc[i]);
        }
    }

    [When("вычисляются корни квадратного уравнения")]
    public void ВычисляютсяКорниКвадратногоУравнения()
    {
        try
        {
            double[] solution = squareq.Solve(abc[0], abc[1], abc[2]);
            var len = solution.Length;
            if (len == 1)
                solution = new double[] {solution[0]};
            else if (len == 2)
                solution = new double[] {solution[0], solution[1]};
        }
        catch(Exception exception)
        {
            this.exception = exception;
        }
    }
    
    [Then("выбрасывается исключение ArgumentException")]
    public void ВыбрасываетсяИсключениеАргумента()
    {
        Assert.ThrowsAny<ArgumentException>(() => throw exception);
    }

    [Then(@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
    public void СравниваютсяДваКорняКратностиОдин(string x1, string x2)
    {
        var expected = new double[]{double.Parse(x2), double.Parse(x1)};
        Assert.True((Math.Abs(solution[0] - expected[0]) < eps)&&(Math.Abs(solution[1] - expected[1]) < eps));
    }

    [Then(@"квадратное уравнение имеет один корень 1 кратности два")]
    public void СравниваютсяОдинКореньКратностиДва()
    {
        var expected = 1;
        Assert.True(Math.Abs(solution[0] - expected) < eps);
    }

    [Then(@"множество корней квадратного уравнения пустое")]
    public void СравниваетсяСПустымМножествомКорней()
    {
        double expected = 0;
        double actual = solution.Length;
        Assert.True(expected == actual);
    }
}