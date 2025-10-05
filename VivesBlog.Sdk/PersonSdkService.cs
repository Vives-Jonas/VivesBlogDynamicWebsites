using System.Net.Http.Json;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Responses;

namespace VivesBlog.Sdk
{
    public class PersonSdkService(IHttpClientFactory httpClientFactory)
    {
        //Find
        public async Task<IList<PersonResponse>> Find()
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = "/api/people";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<PersonResponse>>();

            return result ?? new List<PersonResponse>();

        }

        //Get
        public async Task<PersonResponse?> Get(int id)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResponse>();

            return result;

        }


        //Create
        public async Task<PersonResponse?> Create(PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = "/api/people";

            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResponse>();

            return result;

        }

        //Update
        public async Task<PersonResponse?> Update(int id, PersonRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PersonResponse>();

            return result;

        }


        //Delete
        public async Task Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
