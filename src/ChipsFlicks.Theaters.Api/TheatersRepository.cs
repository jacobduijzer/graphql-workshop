using System.Text.Json;
using Path = System.IO.Path;

namespace ChipsFlicks.Theaters.Api;

public class TheatersRepository
{
    private readonly IEnumerable<Theater> _theaters;

    public TheatersRepository()
    {
        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var jsonData = File.ReadAllText(Path.Combine(path, "theaters.json"));
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _theaters = JsonSerializer.Deserialize<IEnumerable<Theater>>(jsonData, options)!;
    }

    public IEnumerable<Theater> All() => _theaters;
}