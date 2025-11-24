using Microsoft.OpenApi;

namespace VivesBlog.Api.Installers
{
    public static class SwaggerInstaller
    {
        public static WebApplicationBuilder InstallSwagger(this WebApplicationBuilder builder)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \n\nExample: Authorization: Bearer {token}",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("bearer", document)] = []
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Vives Blog API",
                    Version = "v1",
                    Description = "API for Vives Blog application",
                });
            });
            return builder;
        }
    }
}