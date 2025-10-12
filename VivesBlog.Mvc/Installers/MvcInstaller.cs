namespace VivesBlog.Mvc.Installers
{
    public static class MvcInstaller
    {
        public static WebApplicationBuilder InstallMvc(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            return builder;
        }
    }
}
