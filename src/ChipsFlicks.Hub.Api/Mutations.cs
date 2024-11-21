using HotChocolate.Subscriptions;

namespace ChipsFlicks.Hub.Api;

public class Mutations
{
   public async Task<Review> AddReview([Service] IReviewsApi reviews, Review review) => 
      await reviews.Add(review); 
   
   public async Task<string> AddBooking(Booking booking, [Service] IBookingApi bookings, ITopicEventSender sender)
   {
       var bookingResult = await bookings.Add(booking);
       await sender.SendAsync(nameof(Subscriptions.BookingAdded), bookingResult);
       return "Booking submitted";
   }
}