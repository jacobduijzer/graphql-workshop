using Refit;

namespace ChipsFlicks.Hub.Api;

public interface IMoviesApi
{
    [Get("/all")]
    Task<IEnumerable<Movie>> All();

    [Get("/genres")]
    Task<IEnumerable<string>> Genres();
    
    [Get("/genre/{genre}")]
    Task<IEnumerable<Movie>> ByGenre(string genre);
    
    [Get("/types")]
    Task<IEnumerable<string>> Types();
    
    [Get("/type/{type}")]
    Task<IEnumerable<Movie>> ByType(string type);
}