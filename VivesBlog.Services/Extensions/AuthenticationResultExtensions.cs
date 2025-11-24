using Microsoft.AspNetCore.Identity;
using Vives.Services.Model;

namespace VivesBlog.Services.Extensions
{
    public static class AuthenticationResultExtensions
    {
        public static ServiceResult<IdentityUser> LoginFailed(this ServiceResult<IdentityUser> authenticationResult)
        {
            authenticationResult.Messages.Add(
                    new ServiceMessage()
                    {
                        Code = "LoginFailed",
                        Description = "Unable to login. Username/password incorrect",
                        Type = ServiceMessageType.Error
                    });

            return authenticationResult;
        }
    }
}
