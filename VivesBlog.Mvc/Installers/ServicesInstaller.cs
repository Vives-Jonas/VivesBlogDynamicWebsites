using VivesBlog.Sdk;

namespace VivesBlog.Mvc.Installers
{
    public static class ServicesInstaller
    {
        public static WebApplicationBuilder InstallServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<BlogSdkService>();
            builder.Services.AddScoped<PersonSdkService>();

            return builder;
        }
    }
}
