#!/bin/bash

#dotnet tool update --global dotnet-ef

dotnet ef migrations add InitialMigration --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup

Update-Database -Context ArticleCatalogDbContext

Update-Database -Context BookmarksDbContext

Update-Database -Context IdentityDbContext


dotnet ef migrations add LastChanges --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add AddUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add RemoveUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

Remove last migrations:

dotnet ef migrations remove --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations list --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef database update 20241223210540_LastChange --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup


dotnet ef migrations add NewUserNameColumns --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup