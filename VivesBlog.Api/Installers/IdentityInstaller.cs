using Microsoft.AspNetCore.Identity;
using VivesBlog.Api.Security;
using VivesBlog.Repository;
using Vives.Security;

namespace VivesBlog.Api.Installers
{
    public static class IdentityInstaller
    {
        public static WebApplicationBuilder InstallIdentity(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<VivesBlogDbContext>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserContext<Guid>, HttpContextUserContext>();

            return builder;
        }
    }
}
