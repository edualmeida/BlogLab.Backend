var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ArticleCatalog_Api>("articles");

builder.AddProject<Projects.Bookmarks_Api>("bookmarks-api");

builder.Build().Run();
