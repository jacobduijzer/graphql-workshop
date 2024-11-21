using ChipsFlicks.Hub.Api;
using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddRabbitMQClient(connectionName: "messaging");

builder.Services
    .AddRefitClient<IMoviesApi>()
    .ConfigureHttpClient(config => config.BaseAddress = new Uri("https+http://movies"));
builder.Services
    .AddRefitClient<ISnacksApi>()
    .ConfigureHttpClient(config => config.BaseAddress = new Uri("https+http://snacks"));
builder.Services
    .AddRefitClient<IReviewsApi>()
    .ConfigureHttpClient(config => config.BaseAddress = new Uri("https+http://reviews"));
builder.Services
    .AddRefitClient<IBookingApi>()
    .ConfigureHttpClient(config => config.BaseAddress = new Uri("https+http://bookings"));

builder.Services
    .AddGraphQLServer()
    .AddDataLoader<SnackDataLoader>()
    .AddDataLoader<ReviewsDataLoader>()
    .AddType<MovieType>()
    .AddQueryType<AllQueries>()
    .AddMutationType<Mutations>()
    .AddSubscriptionType<Subscriptions>()
    .AddInMemorySubscriptions();

var app = builder.Build();
app.MapGraphQL();
app.UseWebSockets();
app.Run();