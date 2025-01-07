#!/bin/bash

#dotnet tool update --global dotnet-ef

dotnet ef migrations add InitialMigration --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup

Update-Database -Context ArticleCatalogDbContext

Update-Database -Context BookmarksDbContext


dotnet ef migrations add LastChanges --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add AddUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup


Remove last migrations:

dotnet ef migrations remove --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup