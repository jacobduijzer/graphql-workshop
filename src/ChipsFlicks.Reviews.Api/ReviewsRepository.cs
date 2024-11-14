using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ChipsFlicks.Reviews.Api;

public class ReviewsRepository(IDistributedCache cache, JsonSerializerOptions jsonSerializerOptions)
{
    public async Task<IEnumerable<Review>> ByTitle(string title)
    {
        var cachedReview = await cache.GetAsync(title);
        if (cachedReview == null)
            return Enumerable.Empty<Review>();

        var cachedReviews = JsonSerializer.Deserialize<CachedReview>(Encoding.UTF8.GetString(cachedReview), jsonSerializerOptions);
        return cachedReviews!.Reviews;
    }

    public async Task AddReview(Review review)
    {
        CachedReview updatedCachedReview;
        
        var cachedReview = await cache.GetAsync(review.Title);
        if (cachedReview == null)
            updatedCachedReview = new CachedReview(review.Title,[review]);
        else
        {
            var cachedReviews = JsonSerializer.Deserialize<CachedReview>(Encoding.UTF8.GetString(cachedReview), jsonSerializerOptions);
            updatedCachedReview = new CachedReview(review.Title, cachedReviews!.Reviews.Append(review));
        }

        await cache.SetAsync(review.Title,
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(updatedCachedReview, jsonSerializerOptions)), new()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(5)
            });
    }
}