using System.Net.Http.Json;
using Vives.Services.Model;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Responses;
using VivesBlog.Sdk.Settings;

namespace VivesBlog.Sdk
{
    public class BlogSdkService(IHttpClientFactory httpClientFactory)
    {
        //Find
        public async Task<IList<ArticleResponse>> Find(int? authorId = null)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var response = await httpClient.GetAsync(ApiSettings.BlogByAuthorId(authorId));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResponse>>();

            return result ?? new List<ArticleResponse>();

        }

        //Get
        public async Task<ArticleResponse?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            

            var response = await httpClient.GetAsync(ApiSettings.BlogById(id));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ArticleResponse>();

            return result;

        }


        //Create
        public async Task<ServiceResult<ArticleResponse>> Create(ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           

            var response = await httpClient.PostAsJsonAsync(ApiSettings.BlogBase, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResponse>>();

            return result ?? new ServiceResult<ArticleResponse>();

        }

        //Update
        public async Task<ServiceResult<ArticleResponse>> Update(int id, ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            

            var response = await httpClient.PutAsJsonAsync(ApiSettings.BlogById(id), request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResponse>>();

            return result ?? new ServiceResult<ArticleResponse>();

        }


        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.DeleteAsync(ApiSettings.BlogById(id));

            response.EnsureSuccessStatusCode();

            return new ServiceResult();
        }
    }
}
