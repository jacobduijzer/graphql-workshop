using ChipsFlicks.Theaters.Api;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddScoped<TheatersRepository>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>();

var app = builder.Build();
app.MapGraphQL();
app.Run();