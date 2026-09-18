var builder = DistributedApplication.CreateBuilder(args);

var dbPassword = builder.AddParameter("db-password", "mysecretpassword");
var postgres = builder.AddPostgres("postgres", password: dbPassword)
                       .WithDataVolume()
                       .WithHostPort(1984)
                      .AddDatabase("catalog");

var redisCache = builder.AddRedis("redis-cache");

builder.AddProject<Projects.Catalog_API>("catalog-api")
       .WithReference(postgres)
       .WithReference(redisCache);

builder.Build().Run();
