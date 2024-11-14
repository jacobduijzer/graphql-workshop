namespace ChipsFlicks.Hub.Api;

public class SnackDataLoader(ISnacksApi snacks, IBatchScheduler batchScheduler, DataLoaderOptions options)
    : BatchDataLoader<(string Type, string Genre), string>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<(string Type, string Genre), string>> LoadBatchAsync(
        IReadOnlyList<(string Type, string Genre)> keys, CancellationToken cancellationToken)
    {
        var requests = keys
            .Select(k => new SnackRecommendationRequest(k.Type, k.Genre))
            .ToList();
        var snackRecommendations = await snacks.Batch(requests);
        return snackRecommendations.ToDictionary(s => (s.Type, s.Genre), s => s.Snack);
    }
}