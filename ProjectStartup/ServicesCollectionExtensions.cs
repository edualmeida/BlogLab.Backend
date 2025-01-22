using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddSwagger(
        this IServiceCollection services)
    {
        services
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Blog API", Version = "v1" });
                options.AddSecurityDefinition(
                    name: JwtBearerDefaults.AuthenticationScheme, 
                    securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                options.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "API Key",
                    Name = "X-Api-Key",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "APIKey"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        return services;
    }
}