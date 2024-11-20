using Refit;

namespace ChipsFlicks.Hub.Api;

public interface IBookingApi
{
    [Post("/")]
    Task<BookingResult> Add([Body]Booking booking);
}