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
    public class PersonSdkService(IHttpClientFactory httpClientFactory)
    {
        //Find
        public async Task<PagedServiceResult<PersonResult>> Find(Paging paging, string? sorting = null, PersonFilter? filter = null)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var route = ApiSettings.PeopleBase;

            route = QueryHelpers.AddQueryString(route, "offset", paging.Offset.ToString());
            route = QueryHelpers.AddQueryString(route, "limit", paging.Limit.ToString());


            if (!string.IsNullOrWhiteSpace(sorting))
            {
                route = QueryHelpers.AddQueryString(route, "sorting", sorting);
            }

            if (filter != null)
            {
                var queryParams = new Dictionary<string, string?>();

                if (!string.IsNullOrWhiteSpace(filter.FirstName))
                    queryParams["FirstName"] = filter.FirstName;

                if (!string.IsNullOrWhiteSpace(filter.LastName))
                    queryParams["LastName"] = filter.LastName;

                if (!string.IsNullOrWhiteSpace(filter.Search))
                    queryParams["SearchText"] = filter.Search;

                

                if (queryParams.Any())
                {
                    route = QueryHelpers.AddQueryString(route, queryParams);
                }
            }

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PagedServiceResult<PersonResult>>();

            return result ?? new PagedServiceResult<PersonResult>().NoContent();

        }

        //Get
        public async Task<PersonResult?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var response = await httpClient.GetAsync(ApiSettings.PersonById(id));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<PersonResult>();

            return result;

        }


        //Create
        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.PostAsJsonAsync(ApiSettings.PeopleBase, request);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult<PersonResult>();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>();

        }

        //Update
        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.PutAsJsonAsync(ApiSettings.PersonById(id), request);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult<PersonResult>();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            return result ?? new ServiceResult<PersonResult>();

        }


        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.DeleteAsync(ApiSettings.PersonById(id));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResult();
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResult>();

            return result ?? new ServiceResult().NoContent();
        }
    }
}
