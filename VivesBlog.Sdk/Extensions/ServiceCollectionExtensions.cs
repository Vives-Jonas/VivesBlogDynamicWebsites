using Microsoft.Extensions.DependencyInjection;
using VivesBlog.Sdk.DelegatingHandlers;

namespace VivesBlog.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Installs the VivesBlog API SDK services into the IServiceCollection with the specified base string Uri.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection InstallApi(this IServiceCollection services, string baseUrl)
        {
            return services.InstallApi(new Uri(baseUrl));
        }

        /// <summary>
        /// Installs the VivesBlog API SDK services into the IServiceCollection with the specified base Uri.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseUri"></param>
        /// <returns></returns>
        public static IServiceCollection InstallApi(this IServiceCollection services, Uri baseUri)
        {
            services.AddScoped<AuthorizationHandler>();

            services.AddHttpClient("VivesBlogApi", client =>
            {
                client.BaseAddress = baseUri;
            }).AddHttpMessageHandler<AuthorizationHandler>();

            services.AddScoped<BlogSdkService>();
            services.AddScoped<IdentitySdkService>();
            services.AddScoped<PersonSdkService>();

            return services;
        }
    }
}
