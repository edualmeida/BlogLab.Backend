#!/bin/bash

#dotnet tool update --global dotnet-ef

dotnet ef migrations add InitialMigration --context "BlogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup

Update-Database -Context BlogDbContext

Update-Database -Context BlogDbContext