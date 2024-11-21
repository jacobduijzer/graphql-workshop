var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");
var movies = builder.AddProject<Projects.ChipsFlicks_Movies_Api>("movies");
var snacks = builder.AddProject<Projects.ChipsFlicks_Snacks_Api>("snacks");
var reviews = builder.AddProject<Projects.ChipsFlicks_Reviews_Api>("reviews")
    .WithReference(redis)
    .WaitFor(redis);
var bookings = builder.AddProject<Projects.ChipsFlicks_Bookings_Api>("bookings")
    .WithReference(redis)
    .WaitFor(redis);
var theaters = builder.AddProject<Projects.ChipsFlicks_Theaters_Api>("theaters");
var hub = builder.AddProject<Projects.ChipsFlicks_Hub_Api>("hub")
    .WithReference(movies)
    .WithReference(snacks)
    .WithReference(reviews)
    .WithReference(bookings);

builder.AddProject<Projects.ChipsFlicks_Gateway>("gateway")
    .WithReference(hub)
    .WithReference(theaters);

builder.Build().Run();