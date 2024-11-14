var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var app = builder.Build();
app.UseSwaggerWithUi();
app.MapGet("/", () => "Hello World!");

app.Run();