using Newtonsoft.Json;

namespace SpaceCadets
{

public class Mark
{
    [JsonProperty("name")]
    public string name;

    [JsonProperty("group")]
    public string group;

    [JsonProperty("discipline")]
    public string discipline;

    [JsonProperty("mark")]
    public int mark;

}

public class Program
{
    public IList<Mark> Parser(string json_input)
    {
        JArray json = JArray.Parse(json_input);
        IList<Mark> marks = json.Select(p => new Mark
        {
            name = (string)p["name"],
            group = (string)p["group"],
            discipline = (string)p["discipline"],
            mark = (int)p["mark"],
        }).ToList();

        return marks;
    }
    public void GetStudentsWithHighestGPA(string json_input)
    {
        IList<Mark> marks = Parser(json_input);
        IEnumerable<string[]> HighestGPA;
            

    }

    public void CalculateGPAByDiscipline(string json_input)
    {
         IList<Mark> marks = Parser(json_input);

    }

    public void GetBestGroupsByDiscipline(string json_input)
    {
        IList<Mark> marks = Parser(json_input);

    }
}

}

