using HotChocolate.Subscriptions;

namespace ChipsFlicks.Hub.Api;

public class Mutations
{
   public async Task<Review> AddReview([Service] IReviewsApi reviews, Review review) =>
      await reviews.Add(review);
   
   public async Task<Reservation> AddReservation(ITopicEventSender sender, Reservation reservation)
   {
      await sender.SendAsync("ReservationAdded", reservation);
      return reservation;
   }
}