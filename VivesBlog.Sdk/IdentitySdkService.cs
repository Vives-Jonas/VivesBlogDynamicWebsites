using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using System.Net.Http.Json;
using Vives.Services.Model.Extensions;

namespace VivesBlog.Sdk
{
    public class IdentitySdkService(IHttpClientFactory httpClientFactory)
    {
        public async Task<AuthenticationResult> SignIn(SignInRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/identity/sign-in";

            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

            return result ?? new AuthenticationResult().NoContent();
        }

        public async Task<AuthenticationResult> Register(RegisterRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/identity/register";

            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

            return result ?? new AuthenticationResult().NoContent();
        }
    }
}
