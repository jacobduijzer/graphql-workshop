using Refit;

namespace ChipsFlicks.Hub.Api;

public interface IReviewsApi
{
    [Get("/{title}")]
    Task<IEnumerable<Review>> ByTitle(string title);

    [Post("/")]
    Task<Review> Add(Review review);

    [Post("/batch")]
    Task<IEnumerable<Review>> Batch([Body]List<string> titles);
}