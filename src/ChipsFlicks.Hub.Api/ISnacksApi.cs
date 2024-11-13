using Refit;

namespace ChipsFlicks.Hub.Api;

public interface ISnacksApi
{
    [Get("/?type={type}&genre={genre}")]
    Task<string> Recommendation(string type, string genre);
}