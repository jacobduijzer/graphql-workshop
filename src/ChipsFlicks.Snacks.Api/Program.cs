using ChipsFlicks.Snacks.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddScoped<SnacksRepository>();

var app = builder.Build();
app.UseSwaggerWithUi();
app.MapGet("/", (SnacksRepository snacks, string type, string genre) =>
    snacks.Recommendation(type, genre)).WithName("Snack recommendation").WithName("Snack recommendation");
app.MapPost("/batch", (SnacksRepository snacks, [FromBody]IEnumerable<SnackRecommendationRequest> requests) =>
{
    return requests
        .Select(r => new Recommendation(r.Type, r.Genre, snacks.Recommendation(r.Type, r.Genre)));
}).WithName("Batch snack recommendation");
app.Run();