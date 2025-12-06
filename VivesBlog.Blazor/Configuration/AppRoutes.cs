namespace VivesBlog.Blazor.Configuration
{
    public class AppRoutes
    {
        private const string CreateRoute = "create";
        private const string EditRoute = "edit";
        private const string DetailsRoute = "details";

        public static class Dashboard
        {
            public const string Index = "/";
            
        }

        public static class People
        {
            private const string Controller = "people";

            public const string Index = $"/{Controller}";
            public const string Create = $"/{Controller}/{CreateRoute}";
            public const string Edit = $"/{Controller}/{EditRoute}/{{id:int}}";
            public const string Details = $"/{Controller}/{DetailsRoute}/{{id:int}}";

            public static string GetEditUrl(int id)
            {
                return Edit.Replace("{id:int}", id.ToString());
            }


            public static string GetDetailsUrl(int id)
            {
                return Details.Replace("{id:int}", id.ToString());
            }
        }

        public static class Blog
        {
            private const string Controller = "blog";

            public const string Index = $"/{Controller}";
            public const string Create = $"/{Controller}/{CreateRoute}";
            public const string Edit = $"/{Controller}/{EditRoute}/{{id:int}}";
            public const string Details = $"/{Controller}/{DetailsRoute}/{{id:int}}";

            public static string GetEditUrl(int id)
            {
                return Edit.Replace("{id:int}", id.ToString());
            }

            public static string GetDetailsUrl(int id)
            {
                return Details.Replace("{id:int}", id.ToString());
            }
        }
    }
}
