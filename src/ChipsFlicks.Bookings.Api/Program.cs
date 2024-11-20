using System.Text.Json;
using ChipsFlicks.Bookings.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddRedisDistributedCache("cache");
builder.Services.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});
builder.Services.AddScoped<BookingRepository>();

var app = builder.Build();
app.UseSwaggerWithUi();
app.MapPost("/", async (BookingRepository bookings, [FromBody]Booking booking) =>
{
    var bookingResult = await bookings.Add(booking);
    await Task.Delay(TimeSpan.FromMilliseconds(30));
    return bookingResult;
}).WithName("Add bookings");

app.MapDelete("/{title}", async (BookingRepository bookings, string title) =>
{
    await bookings.Remove(title);
    return Results.Ok();
}).WithName("Remove bookings");
app.Run();