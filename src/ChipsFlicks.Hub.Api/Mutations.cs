namespace ChipsFlicks.Hub.Api;

public class Mutations
{
   public async Task<Review> AddReview([Service] IReviewsApi reviews, Review review) => 
      await reviews.Add(review); 
}