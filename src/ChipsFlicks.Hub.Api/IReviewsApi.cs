using Refit;

namespace ChipsFlicks.Hub.Api;

public interface IReviewsApi
{
    [Get("/{title}")]
    Task<Review> ByTitle(string title);
}