using System.Net.Http.Json;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Responses;

namespace VivesBlog.Sdk
{
    public class BlogSdkService(IHttpClientFactory httpClientFactory)
    {
        //Find
        public async Task<IList<ArticleResponse>> Find(int? authorId = null)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = "/api/blog";

            if (authorId.HasValue)
            {
                route += $"?authorId={authorId.Value}";
            }

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResponse>>();

            return result ?? new List<ArticleResponse>();

        }

        //Get
        public async Task<ArticleResponse?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/blog/{id}";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ArticleResponse>();

            return result;

        }


        //Create
        public async Task<ArticleResponse?> Create(ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = "/api/blog";

            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ArticleResponse>();

            return result;

        }

        //Update
        public async Task<ArticleResponse?> Update(int id, ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/blog/{id}";

            var response = await httpClient.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ArticleResponse>();

            return result;

        }


        //Delete
        public async Task Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/blog/{id}";

            var response = await httpClient.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
