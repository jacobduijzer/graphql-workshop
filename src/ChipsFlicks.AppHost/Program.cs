var builder = DistributedApplication.CreateBuilder(args);
var movies = builder.AddProject<Projects.ChipsFlicks_Movies_Api>("movies");
var snacks = builder.AddProject<Projects.ChipsFlicks_Snacks_Api>("snacks");
builder.AddProject<Projects.ChipsFlicks_Hub_Api>("hub")
    .WithReference(movies)
    .WithReference(snacks);

builder.Build().Run();