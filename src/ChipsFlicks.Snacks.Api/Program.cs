using ChipsFlicks.Snacks.Api;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddScoped<SnacksRepository>();

var app = builder.Build();
app.UseSwaggerWithUi();
app.MapGet("/", (SnacksRepository snacks, string type, string genre) =>
    snacks.Recommendation(type, genre)).WithName("Snack recommendation").WithName("Snack recommendation");
app.Run();