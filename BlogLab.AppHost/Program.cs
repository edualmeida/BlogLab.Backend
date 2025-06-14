var builder = DistributedApplication.CreateBuilder(args);

var bookmarks = builder.AddProject<Projects.Bookmarks_Api>("bookmarks");
var identity = builder.AddProject<Projects.Identity_Api>("identity");

builder.AddProject<Projects.ArticleCatalog_Api>("articles")
    .WithExternalHttpEndpoints()
    .WithReference(bookmarks)
    .WithReference(identity)
    .WaitFor(bookmarks)
    .WaitFor(identity);

builder.AddProject<Projects.Comments_Api>("comments-api");

builder.Build().Run();
