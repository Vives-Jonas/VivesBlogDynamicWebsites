using VivesBlog.Services;

namespace VivesBlog.Api.Installers
{
    public static class ServicesInstaller
    {
        public static WebApplicationBuilder InstallServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<BlogService>();
            builder.Services.AddScoped<PersonService>();
            builder.Services.AddScoped<IdentityService>();

            return builder;
        }
    }
}
