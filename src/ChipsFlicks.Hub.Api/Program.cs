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
builder.Services
    .AddGraphQLServer()
    .AddType<MovieType>()
    .AddQueryType<AllQueries>();

var app = builder.Build();
app.MapGraphQL();
app.Run();