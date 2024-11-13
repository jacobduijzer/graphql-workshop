using System.Text.Json;

namespace ChipsFlicks.Movies.Api;

public class MoviesRepository
{
    private IEnumerable<Movie> _movies;

    public MoviesRepository()
    {
        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var jsonData = File.ReadAllText(Path.Combine(path, "movies.json"));
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _movies = JsonSerializer.Deserialize<IEnumerable<Movie>>(jsonData, options)!;
    } 
    
    public IEnumerable<Movie> All() => _movies;

    public IEnumerable<string> Genres() => _movies
        .Select(x => x.Genre)
        .Distinct();

    public IEnumerable<Movie> ByGenre(string genre) =>
        _movies.Where(p => p.Genre == genre);

    public IEnumerable<string> Types() => _movies
        .Select(x => x.Type)
        .Distinct();

    public IEnumerable<Movie> ByType(string type) =>
        _movies.Where(p => p.Type == type);
}