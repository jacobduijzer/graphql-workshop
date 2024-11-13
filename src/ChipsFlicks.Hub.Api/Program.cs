using ChipsFlicks.Hub.Api;
using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services
    .AddRefitClient<IMoviesApi>()
    .ConfigureHttpClient(config =>
        config.BaseAddress = new Uri("https+http://movies"));
builder.Services
    .AddRefitClient<ISnacksApi>()
    .ConfigureHttpClient(config =>
        config.BaseAddress = new Uri("https+http://snacks"));

var app = builder.Build();
app.UseSwaggerWithUi();

app.MapGet("/movies", async (IMoviesApi movies) =>
    await movies.All()).WithName("Get movies");

app.MapGet("/snack", async (ISnacksApi snacks) =>
    await snacks.Recommendation("movie", "Thriller")).WithName("Snack recommendation");

app.Run();