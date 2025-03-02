#!/bin/bash

#dotnet tool update --global dotnet-ef

#Navigate to solution root, example: F:\Projects\BlogLab.Backend

dotnet ef migrations add InitialMigration --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup


dotnet ef database update --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup


#dotnet ef migrations add ChangeColumnName --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef migrations add AddUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef migrations add RemoveUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

# Remove last migrations:

#dotnet ef migrations remove --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef migrations list --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef database update 20241223210540_LastChange --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup


#dotnet ef migrations add NewUserNameColumns --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup