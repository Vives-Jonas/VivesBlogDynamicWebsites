namespace VivesBlog.Sdk.Settings
{
    public static class ApiSettings
    {
        public const string HttpClientName = "VivesBlogApi";
        public const string BlogBase = "/api/blog";
        public static string BlogById(int id) => $"{BlogBase}/{id}";
        public static string BlogByAuthorId(int? authorId) => authorId.HasValue ? 
                $"{BlogBase}?authorId={authorId.Value}"
                : BlogBase;
        public static string BlogRandom(int count) => $"{BlogBase}/random?count={count}";

        public const string PeopleBase = "/api/people";
        public static string PersonById(int id) => $"{PeopleBase}/{id}";
    }
}
