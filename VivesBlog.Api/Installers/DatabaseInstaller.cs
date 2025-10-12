using Microsoft.EntityFrameworkCore;
using VivesBlog.Repository;

namespace VivesBlog.Api.Installers
{
    public static class DatabaseInstaller
    {
        public static WebApplicationBuilder InstallDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<VivesBlogDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
            });
            return builder;
        }
    }
}
