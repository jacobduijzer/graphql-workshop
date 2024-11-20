namespace ChipsFlicks.Hub.Api;

public class ReviewsDataLoader(
    IReviewsApi reviews,
    IBatchScheduler scheduler,
    DataLoaderOptions? options = null) : GroupedDataLoader<string, Review>(scheduler, options)
{
    protected override async Task<ILookup<string, Review>> LoadGroupedBatchAsync(
        IReadOnlyList<string> titles,
        CancellationToken cancellationToken)
    {
        var batchedReviews = await reviews.Batch(titles.ToList());
        return batchedReviews.ToLookup(r => r.Title);
    }
}