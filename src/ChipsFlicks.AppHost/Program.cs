var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");

var username = builder.AddParameter("rabbit-user", secret: false);
var password = builder.AddParameter("rabbit-password", secret: false);
var rabbitmq = builder
    .AddRabbitMQ("messaging", username, password)
    .WithManagementPlugin();
var movies = builder.AddProject<Projects.ChipsFlicks_Movies_Api>("movies");
var snacks = builder.AddProject<Projects.ChipsFlicks_Snacks_Api>("snacks");
var reviews = builder.AddProject<Projects.ChipsFlicks_Reviews_Api>("reviews")
    .WithReference(redis)
    .WaitFor(redis);
builder.AddProject<Projects.ChipsFlicks_Hub_Api>("hub")
    .WithReference(movies)
    .WithReference(snacks)
    .WithReference(reviews)
    .WithReference(rabbitmq);

builder.Build().Run();