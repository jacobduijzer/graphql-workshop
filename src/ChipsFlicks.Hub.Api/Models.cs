namespace ChipsFlicks.Hub.Api;

public record Movie(Guid Id, string Title, string Type, string Genre, string SnackRecommendation, IEnumerable<Review> Reviews);
public record SnackRecommendationRequest(string Type, string Genre);
public record SnackRecommendation(string Type, string Genre, string Snack);
public record Review(string Title, string Reviewer, string Content, int Rating);

