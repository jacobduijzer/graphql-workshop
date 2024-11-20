namespace ChipsFlicks.Hub.Api;

public class Subscriptions
{
    [Subscribe]
    public BookingResult BookingAdded([EventMessage] BookingResult bookingResult) => bookingResult;
}