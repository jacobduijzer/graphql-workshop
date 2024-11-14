namespace ChipsFlicks.Snacks.Api;

public record Recommendation(string Type, string Genre, string Snack);

public record SnackRecommendationRequest(string Type, string Genre);