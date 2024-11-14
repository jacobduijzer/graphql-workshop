using System.Text.Json;
using ChipsFlicks.Reviews.Api;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddRedisDistributedCache("cache");
builder.Services.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});
builder.Services.AddScoped<CacheFeeder>();
builder.Services.AddScoped<ReviewsRepository>();

var app = builder.Build();
app.UseSwaggerWithUi();
app
    .MapGet("/{title}", async (ReviewsRepository reviews, string title) 
        => await reviews.ByTitle(title))
    .WithName("Reviews by ISBN");

app
    .MapPost("/", async (ReviewsRepository reviews, Review review) =>
    {
        await reviews.AddReview(review);
        return Results.Created($"/{review.Title}", review);
    }).WithName("Add review");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var cacheFeeder = services.GetRequiredService<CacheFeeder>();
    await cacheFeeder.Feed();
}

app.Run();