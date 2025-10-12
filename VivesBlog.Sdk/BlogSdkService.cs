using System.Net.Http.Json;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
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

            if (!response.IsSuccessStatusCode)
            {
                return new List<ArticleResponse>();
            }

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResponse>>();

            return result ?? new List<ArticleResponse>();

        }

        //Get
        public async Task<ArticleResponse?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.GetAsync(ApiSettings.BlogById(id));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<ArticleResponse>();

            return result;

        }


        //Get Random
        public async Task<IList<ArticleResponse>> GetRandom(int count = 5)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var response = await httpClient.GetAsync($"{ApiSettings.BlogRandom(count)}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<ArticleResponse>();
            }

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResponse>>();

            return result ?? new List<ArticleResponse>();
        }


        //Create
        public async Task<ServiceResult<ArticleResponse>> Create(ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.PostAsJsonAsync(ApiSettings.BlogBase, request);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult<ArticleResponse>().ApiError();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResponse>>();

            return result ?? new ServiceResult<ArticleResponse>();

        }

        //Update
        public async Task<ServiceResult<ArticleResponse>> Update(int id, ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.PutAsJsonAsync(ApiSettings.BlogById(id), request);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult<ArticleResponse>().ApiError();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResponse>>();

            return result ?? new ServiceResult<ArticleResponse>();

        }


        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.DeleteAsync(ApiSettings.BlogById(id));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult().ApiError();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult>();

            return result ?? new ServiceResult().NoContent();
        }
    }
}
