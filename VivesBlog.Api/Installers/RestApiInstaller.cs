namespace VivesBlog.Api.Installers
{
    public static class RestApiInstaller
    {
        public static WebApplicationBuilder InstallRestApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            return builder;
        }
    }
}
