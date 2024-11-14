using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ChipsFlicks.Reviews.Api;

public class CacheFeeder(IDistributedCache cache, JsonSerializerOptions jsonSerializerOptions)
{
    public async Task Feed()
    {
        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var jsonData = File.ReadAllText(Path.Combine(path, "reviews.json"));
        var reviews = JsonSerializer.Deserialize<IEnumerable<Review>>(jsonData, jsonSerializerOptions)!;
        var cachedReviews = reviews
            .GroupBy(r => r.Title)
            .Select(g => new CachedReview(g.Key, g.ToList()));

        foreach (var cachedReview in cachedReviews)
        {
            await cache.SetAsync(cachedReview.Title, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cachedReview, jsonSerializerOptions)), new ()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(5)
            }); 
        }
    }
}