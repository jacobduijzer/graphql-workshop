using System.Text.Json;
using ChipsFlicks.Reviews.Api;
using Microsoft.AspNetCore.Mvc;

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
    .WithName("Reviews by movie title");

app
    .MapPost("/", async (ReviewsRepository reviews, Review review) =>
    {
        await reviews.AddReview(review);
        return Results.Created($"/{review.Title}", review);
    }).WithName("Add review");

app
    .MapPost("/batch", async (ReviewsRepository reviews, [FromBody]IEnumerable<string> titles) =>
    {
        List<Review> reviewsBatch = new();
        foreach(var title in titles)
        {
            var reviewsByTitle = await reviews.ByTitle(title);
            reviewsBatch.AddRange(reviewsByTitle);
        }
        return reviewsBatch;
    }).WithName("Get batched reviews");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var cacheFeeder = services.GetRequiredService<CacheFeeder>();
    await cacheFeeder.Feed();
}

app.Run();