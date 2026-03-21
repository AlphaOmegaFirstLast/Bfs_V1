var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Bfs_Identity_Web>("bfs-identity-web");

builder.AddProject<Projects.Bfs_Auth_Api>("bfs-auth-api");

builder.AddProject<Projects.Bfs_Infrastructure_Api>("bfs-infrastructure-api");

builder.AddProject<Projects.Bfs_Stores_Api>("bfs-stores-api");

builder.Build().Run();
