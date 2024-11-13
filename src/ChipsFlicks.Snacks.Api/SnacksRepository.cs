using System.Text.Json;

namespace ChipsFlicks.Snacks.Api;

public class SnacksRepository
{
    private readonly IEnumerable<Recommendation> _snacks;

    public SnacksRepository()
    {
        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var jsonData = File.ReadAllText(Path.Combine(path, "snacks.json"));
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _snacks = JsonSerializer.Deserialize<IEnumerable<Recommendation>>(jsonData, options)!;
    }

    public string Recommendation(string type, string genre) =>
        _snacks.FirstOrDefault(x => 
            x.Type.Equals(type) && x.Genre.Equals(genre))!.Snack;
}