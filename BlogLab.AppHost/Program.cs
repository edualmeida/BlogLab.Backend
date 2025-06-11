var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ArticleCatalog_Api>("articles");

builder.AddProject<Projects.Bookmarks_Api>("bookmarks-api");

builder.AddProject<Projects.Identity_Api>("identity-api");

builder.AddProject<Projects.Comments_Api>("comments-api");

builder.Build().Run();
