using System.Net.Http.Json;
using Vives.Services.Model;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Responses;
using VivesBlog.Sdk.Settings;

namespace VivesBlog.Sdk
{
    public class PersonSdkService(IHttpClientFactory httpClientFactory)
    {
        //Find
        public async Task<IList<PersonResponse>> Find()
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            

            var response = await httpClient.GetAsync(ApiSettings.PeopleBase);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<PersonResponse>>();

            return result ?? new List<PersonResponse>();

        }

        //Get
        public async Task<PersonResponse?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);

            var response = await httpClient.GetAsync(ApiSettings.PersonById(id));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResponse>();

            return result;

        }


        //Create
        public async Task<ServiceResult<PersonResponse>> Create(PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            

            var response = await httpClient.PostAsJsonAsync(ApiSettings.PeopleBase, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResponse>>();

            return result ?? new ServiceResult<PersonResponse>();

        }

        //Update
        public async Task<ServiceResult<PersonResponse>> Update(int id, PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
            
            var response = await httpClient.PutAsJsonAsync(ApiSettings.PersonById(id), request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResponse>>();

            return result ?? new ServiceResult<PersonResponse>();

        }


        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient(ApiSettings.HttpClientName);
           
            var response = await httpClient.DeleteAsync(ApiSettings.PersonById(id));

            response.EnsureSuccessStatusCode();

            return new ServiceResult();
        }
    }
}
