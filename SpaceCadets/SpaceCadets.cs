using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpaceCadets;

public class Task
{
    [JsonProperty("taskName")]
    public string taskName = "";
    [JsonProperty("data")]
    public Mark[] data = new Mark[]{};
}

public class Mark
{
    [JsonProperty("name")]
    public string name = "";
    [JsonProperty("group")]
    public string group = "";
    [JsonProperty("discipline")]
    public string discipline = "";
    [JsonProperty("mark")]
    public double mark;

}

public class SpaceCadetsMarks
{
    public static IEnumerable<JObject> GetStudentsWithHighestGPA(Task json_input)
    {
        var max =  json_input.data
            .GroupBy(c => c.name)
            .Max(c => c.Average(x=> x.mark));

        var studentsWithHighestGPA = json_input.data
            .GroupBy(c => c.name)
            .Where(c=> c.Average(x => x.mark) == max)
            .Select(c=> new JObject(new JProperty("Cadet", c.Key), new JProperty("GPA", c.Average(x=> x.mark), 3)));

        return studentsWithHighestGPA;
    }

    public static IEnumerable<JObject> GetGPAByDiscipline(Task json_input)
    {
        var GPAByDiscipline = json_input.data
            .GroupBy(c => c.discipline)
            .Select(d => new JObject(new JProperty(d.Key, d.Average(c => c.mark))));

            return GPAByDiscipline;   
    }

    public static IEnumerable<JObject> GetBestGroupsByDiscipline(Task json_input)
    {
        var bestGroupsByDiscipline = json_input.data
            .GroupBy(c => new {c.discipline, c.group})
            .Select(d => new {Discipline = d.Key.discipline, Group = d.Key.group, GPA = d.Average(c => c.mark)})
            .GroupBy(d => d.Discipline)
            .Select(s => new JObject(new JProperty("Discipline", s.Key), 
                    new JProperty("Group", s.OrderByDescending(c => c.GPA).First().Group),
                    new JProperty("GPA", s.Max(c => c.GPA))));

            return bestGroupsByDiscipline;
    }

    static void Main(string[] args)
    {
        string inputPath = args[0];
        string outputPath = args[1];

        var json_input = new Task();

        json_input = JsonConvert.DeserializeObject<Task>(File.ReadAllText(inputPath));

        if (json_input.taskName == "GetStudentsWithHighestGPA")
        {
            IEnumerable<JObject> studentsWithHighestGPA = GetStudentsWithHighestGPA(json_input);
            var res = new JObject(new JProperty("Response", studentsWithHighestGPA));
            File.WriteAllText(outputPath, JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        else if (json_input.taskName == "GetGPAByDiscipline")
        {
            IEnumerable<JObject> GPAByDiscipline = GetGPAByDiscipline(json_input);
            var res = new JObject(new JProperty("Response", GPAByDiscipline));
            File.WriteAllText(outputPath, JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        else if (json_input.taskName == "GetBestGroupsByDiscipline")
        {
            IEnumerable<JObject> bestGroupsByDiscipline = GetBestGroupsByDiscipline(json_input);
            var res = new JObject(new JProperty("Response", bestGroupsByDiscipline));
            File.WriteAllText(outputPath, JsonConvert.SerializeObject(res, Formatting.Indented));
        }

    }
}
