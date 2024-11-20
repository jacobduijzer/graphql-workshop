namespace ChipsFlicks.Hub.Api;

public class AllQueries
{
    public async Task<IEnumerable<Movie>> All([Service] IMoviesApi movies) => await movies.All();

    public async Task<IEnumerable<string>> Genres([Service] IMoviesApi movies) => await movies.Genres();

    public async Task<IEnumerable<Movie>> ByGenre([Service] IMoviesApi movies, string genre) =>
        await movies.ByGenre(genre);

    public async Task<IEnumerable<string>> Types([Service] IMoviesApi movies) => await movies.Types();

    public async Task<IEnumerable<Movie>> ByType([Service] IMoviesApi movies, string type) => await movies.ByType(type);

    public async Task<string> Recommendation([Service] ISnacksApi snacks, string type, string genre) =>
        await snacks.Recommendation(type, genre);
    
    public async Task<IEnumerable<Review>> Reviews([Service] IReviewsApi reviews, string title) => await reviews.ByTitle(title);
}