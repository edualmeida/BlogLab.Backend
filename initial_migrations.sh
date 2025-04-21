#!/bin/bash

# Install EF tool
#dotnet tool update --global dotnet-ef

#Navigate to solution root to apply the commands, example: F:\Projects\BlogLab.Backend

# InitialMigrations

dotnet ef migrations add InitialMigration --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup

dotnet ef migrations add InitialMigration --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup

# Update database

dotnet ef database update --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup
dotnet ef database update --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup
dotnet ef database update --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup


# MISC

#dotnet ef migrations add ChangeColumnName --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef migrations add AddUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef migrations add RemoveUsersTable --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

# Remove last migrations:

#dotnet ef migrations remove --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef migrations list --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup

#dotnet ef database update 20241223210540_LastChange --context "ArticleCatalogDbContext" --project ArticleCatalog/ArticleCatalog.Infrastructure --startup-project ProjectStartup


#dotnet ef migrations add NewUserNameColumns --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup


dotnet ef migrations add ChangeUserIdType --context "IdentityDbContext" --project Identity/Identity.Infrastructure --startup-project ProjectStartup

dotnet ef database update --context "BookmarksDbContext" --project Bookmarks/Bookmarks.Infrastructure --startup-project ProjectStartup