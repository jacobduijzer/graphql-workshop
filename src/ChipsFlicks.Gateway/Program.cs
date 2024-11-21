using ChipsFlicks.Gateway;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHttpClient(WellKnownSchemaNames.Hub, c => 
    c.BaseAddress =new Uri("https://hub/graphql"));
builder.Services.AddHttpClient(WellKnownSchemaNames.Theaters, c => 
    c.BaseAddress =new Uri("https://theaters/graphql"));

builder.Services
    .AddGraphQLServer()
    .AddRemoteSchema(WellKnownSchemaNames.Hub)
    .AddRemoteSchema(WellKnownSchemaNames.Theaters);

var app = builder.Build();
app.MapGraphQL();
app.Run();