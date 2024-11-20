namespace ChipsFlicks.Bookings.Api;

public record Booking(string Title, DateOnly BookingDate, int NumberOfPeople);
public record BookingResult(Guid BookingNumber, Booking Booking);

public record CachedBookings(string Title, IEnumerable<BookingResult> Bookings);
