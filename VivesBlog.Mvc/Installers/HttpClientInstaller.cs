using VivesBlog.Mvc.Settings;

namespace VivesBlog.Mvc.Installers
{
    public static class HttpClientInstaller
    {
        public static WebApplicationBuilder InstallHttpClient(this WebApplicationBuilder builder)
        {
            var appSettings = new AppSettings();
            builder.Configuration.Bind(nameof(AppSettings), appSettings);
            builder.Services.AddSingleton(appSettings);

            builder.Services.AddHttpClient(appSettings.HttpClientName, (provider, client) =>
            {
                client.BaseAddress = new Uri(appSettings.ApiBaseUrl);
            });

            return builder;
        }
    }
}
