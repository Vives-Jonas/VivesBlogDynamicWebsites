using VivesBlog.Sdk.Extensions;
using Vives.Security;

namespace VivesBlog.Sdk.DelegatingHandlers
{
    public class AuthorizationHandler(ITokenStore tokenStore): DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Before request
            var token = tokenStore.GetToken();
            request.Headers.AddAuthorization(token);

            var responseMessage = await base.SendAsync(request, cancellationToken);

            //After request


            return responseMessage;
        }
    }
}
