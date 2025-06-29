using Serilog;

namespace ProjectStartup;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder SetupWebApplication(
        this IApplicationBuilder app, IWebHostEnvironment env)
        => app
            .UseExceptionHandler()
            .UseSerilogRequestLogging()
            .SetupDevelopmentStage(env)
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints
                .MapControllers());

    private static IApplicationBuilder SetupDevelopmentStage(
        this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // app.UseDeveloperExceptionPage(); we are using custom exception handler with Services.AddExceptionHandler
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Article Shop v1");
            //    options.EnablePersistAuthorization();
            //});
        }

        return app;
    }

    public static IApplicationBuilder InitializeDatabase(
        this IApplicationBuilder app, 
        IConfiguration configuration)
    {
        //if (configuration.GetSection("InitializeDatabase").Value!.ToLowerInvariant() == "false")
        //    return app;

        //using var serviceScope = app.ApplicationServices.CreateScope();
        //var initializers = serviceScope.ServiceProvider.GetServices<IDbInitializer>();

        //foreach (var initializer in initializers)
        //{
        //    initializer.Initialize();
        //}

        return app;
    }
}