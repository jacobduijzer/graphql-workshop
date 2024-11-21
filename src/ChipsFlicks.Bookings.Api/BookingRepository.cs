using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ChipsFlicks.Bookings.Api;

public class BookingRepository(IDistributedCache cache, JsonSerializerOptions jsonSerializerOptions)
{
    public async Task<BookingResult> Add(Booking booking)
    {
        BookingResult bookingResult = new(Guid.NewGuid(), booking);
        CachedBookings updatedCachedBookings;

        var cachedBooking = await cache.GetAsync(bookingResult.Booking.Title);
        if (cachedBooking == null)
            updatedCachedBookings = new CachedBookings(bookingResult.Booking.Title, [bookingResult]);
        else
        {
            var cachedBookings = JsonSerializer.Deserialize<CachedBookings>(Encoding.UTF8.GetString(cachedBooking), jsonSerializerOptions);
            var bookingsList = cachedBookings?.Bookings?.ToList() ?? [];
            bookingsList.Add(bookingResult);
            updatedCachedBookings = new CachedBookings(bookingResult.Booking.Title, bookingsList);
        }

        await cache.SetAsync(bookingResult.Booking.Title,
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(updatedCachedBookings, jsonSerializerOptions)), new()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(5)
            });
        return bookingResult;
    }

    public async Task<BookingResult> Get(string title, Guid bookingNumber)
    {
        var cachedReview = await cache.GetAsync(title);
        if (cachedReview == null)
            return default;

        var cachedReviews =
            JsonSerializer.Deserialize<CachedBookings>(Encoding.UTF8.GetString(cachedReview), jsonSerializerOptions);
        return cachedReviews!.Bookings.FirstOrDefault(x => x.BookingNumber == bookingNumber)!;
    }

    public async Task Remove(string title)
    {
        await cache.RemoveAsync(title);
    }

    // public async Task Flush()
    // {
    //     await cache.RemoveAsync(string title)
    // }
}