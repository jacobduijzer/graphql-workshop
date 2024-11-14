namespace ChipsFlicks.Reviews.Api;

public record Review(Guid MovieId, string Reviewer, string Content, int Rating);

public record CachedReview(Guid MovieId, IEnumerable<Review> Reviews);