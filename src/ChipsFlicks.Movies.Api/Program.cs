using ChipsFlicks.Movies.Api;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddScoped<MoviesRepository>();

var app = builder.Build();
app.UseSwaggerWithUi();
app.MapGet("/all", (MoviesRepository movies) => movies.All()).WithName("All movies");
app.MapGet("/genres", (MoviesRepository movies) => movies.Genres()).WithName("All genres");
app.MapGet("/genre/{genre}", (MoviesRepository movies, string genre) => movies.ByGenre(genre)).WithName("Movies by genre");
app.MapGet("/types", (MoviesRepository movies) => movies.Types()).WithName("All types");
app.MapGet("/type/{type}", (MoviesRepository movies, string type) => movies.ByType(type)).WithName("Movies by type");
app.Run();