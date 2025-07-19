using Common.Infrastructure;
namespace BlogLab.AppHost.Extensions
{
    public static class PostgresHostingExtensions
    {
        public static (IResourceBuilder<PostgresServerResource> postgres, 
            IResourceBuilder<PostgresDatabaseResource> blogLabDb, 
            IResourceBuilder<ProjectResource> migrationService)
            AddPostgresWithMigration(
            this IDistributedApplicationBuilder builder, 
            int hostPort = 54285)
        {
            var username = builder.AddParameter("username", "postgres", secret: true);
            var password = builder.AddParameter("password", "1q2w3e4r", secret: true);

            var postgres = builder.AddPostgres("postgres", username, password)
                .WithHostPort(hostPort)
                .WithDataVolume(isReadOnly: false)
                //.WithPgWeb()
                .WithLifetime(ContainerLifetime.Persistent);

            var blogLabDb = postgres.AddDatabase(InfrastructureConstants.BlogLabDatabaseName);

            var migrationService = builder.AddProject<Projects.BlogLab_MigrationService>("migrationservice")
                .WithReference(blogLabDb)
                .WaitFor(blogLabDb);

            return (postgres, blogLabDb, migrationService);
        }
    }
}
