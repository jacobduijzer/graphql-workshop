var builder = DistributedApplication.CreateBuilder(args);
var redis = builder.AddRedis("cache");
var movies = builder.AddProject<Projects.ChipsFlicks_Movies_Api>("movies");
var snacks = builder.AddProject<Projects.ChipsFlicks_Snacks_Api>("snacks");
var reviews = builder.AddProject<Projects.ChipsFlicks_Reviews_Api>("reviews")
    .WithReference(redis);
builder.AddProject<Projects.ChipsFlicks_Hub_Api>("hub")
    .WithReference(movies)
    .WithReference(snacks)
    .WithReference(reviews);

builder.Build().Run();