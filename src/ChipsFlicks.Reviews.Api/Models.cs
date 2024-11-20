namespace ChipsFlicks.Reviews.Api;

public record Review(string Title, string Reviewer, string Content, int Rating);

public record CachedReview(string Title, IEnumerable<Review> Reviews);