using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Reflection;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Filter;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using VivesBlog.Sdk.Settings;

namespace VivesBlog.Sdk
{
    public class BlogSdkService(IHttpClientFactory httpClientFactory)
    {
        //Find
        public async Task<FilteredPagedServiceResult<ArticleResult, ArticleFilter>> Find(Paging paging, string? sorting = null, ArticleFilter? filter = null)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var route = ApiSettings.BlogBase;

            route = QueryHelpers.AddQueryString(route, "offset", paging.Offset.ToString());
            route = QueryHelpers.AddQueryString(route, "limit", paging.Limit.ToString());

            if (!string.IsNullOrWhiteSpace(sorting))
            {
                route = QueryHelpers.AddQueryString(route, "sorting", sorting);
            }

            if (filter != null)
            {
                var queryParams = new Dictionary<string, string?>();

                if (!string.IsNullOrWhiteSpace(filter.Search))
                    queryParams["Search"] = filter.Search;

                if (!string.IsNullOrWhiteSpace(filter.AuthorName))
                    queryParams["AuthorName"] = filter.AuthorName;

                if (filter.AuthorId.HasValue)
                    queryParams["AuthorId"] = filter.AuthorId.ToString();

                queryParams["UseAuthorIdFilter"] = filter.UseAuthorIdFilter.ToString();

                // Add other filter properties as needed...

                if (queryParams.Any())
                {
                    route = QueryHelpers.AddQueryString(route, queryParams);
                }
            }

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<FilteredPagedServiceResult<ArticleResult, ArticleFilter>>();

            return result ?? new FilteredPagedServiceResult<ArticleResult, ArticleFilter>().NoContent();

        }

        //Get
        public async Task<ArticleResult?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.GetAsync(ApiSettings.BlogById(id));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<ArticleResult>();

            return result;

        }


        //Get Random
        public async Task<IList<ArticleResult>> GetRandom(int count = 5)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var response = await httpClient.GetAsync($"{ApiSettings.BlogRandom(count)}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<ArticleResult>();
            }

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResult>>();

            return result ?? new List<ArticleResult>();
        }


        //Create
        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.PostAsJsonAsync(ApiSettings.BlogBase, request);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult<ArticleResult>();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();

            return result ?? new ServiceResult<ArticleResult>();

        }

        //Update
        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.PutAsJsonAsync(ApiSettings.BlogById(id), request);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult<ArticleResult>();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();

            return result ?? new ServiceResult<ArticleResult>();

        }


        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.DeleteAsync(ApiSettings.BlogById(id));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult>();

            return result ?? new ServiceResult().NoContent();
        }
    }
}
