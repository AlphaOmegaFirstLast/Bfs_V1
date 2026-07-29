var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Bfs_Identity_Web>("bfs-identity-web");

builder.AddProject<Projects.Bfs_Auth_Api>("bfs-auth-api");

builder.AddProject<Projects.Bfs_Master_Api>("bfs-master-api");

builder.AddProject<Projects.Bfs_Stores_Api>("bfs-stores-api");

builder.AddProject<Projects.Bfs_StockEx_Api>("bfs-stockex-api");

builder.Build().Run();
