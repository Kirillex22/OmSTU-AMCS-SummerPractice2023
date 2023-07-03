
namespace SpaceCadets;
public class Student
{
    private string name;
    private string group;
    private string discipline;
    private int mark;

}

public class Program
{
    public void GetStudentsWithHighestGPA(string json_input)
    {
        JArray json = JArray.Parse(json_input);
        IList<Student> students = json.Select(p => new Student
        {
            name = (string)p["name"],
            group = (string)p["group"],
            discipline = (string)p["discipline"],
            mark = (int)p["mark"],
        }).ToList();
        
        IEnumerable U

    }

    public void CalculateGPAByDiscipline(string json_input)
    {
        
    }

    public void GetBestGroupsByDiscipline(string json_input)
    {
        
    }
}
