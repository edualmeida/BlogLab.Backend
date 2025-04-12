public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebService(
        this IApplicationBuilder app, IWebHostEnvironment env)
        => app
            .UseExceptionHandling(env)
            .UseValidationExceptionHandler()
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

    private static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Article Shop v1");
                options.EnablePersistAuthorization();
            });
            app.UseDeveloperExceptionPage();
        }

        return app;
    }

    public static IApplicationBuilder InitializeDatabase(
        this IApplicationBuilder app, 
        IConfiguration configuration)
    {
        if (configuration.GetSection("InitializeDatabase").Value!.ToLowerInvariant() == "false")
            return app;

        using var serviceScope = app.ApplicationServices.CreateScope();
        var initializers = serviceScope.ServiceProvider.GetServices<IDbInitializer>();

        foreach (var initializer in initializers)
        {
            initializer.Initialize();
        }

        return app;
    }
}