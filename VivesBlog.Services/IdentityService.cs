using Microsoft.AspNetCore.Identity;
using VivesBlog.Dto.Requests;
using VivesBlog.Services.Extensions;
using Vives.Services.Model;

namespace VivesBlog.Services
{
    public class IdentityService(UserManager<IdentityUser> userManager)
    {

        //SignIn
        public async Task<ServiceResult<IdentityUser>> SignIn(SignInRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ServiceResult<IdentityUser>().LoginFailed();
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return new ServiceResult<IdentityUser>().LoginFailed();
            }

            return new ServiceResult<IdentityUser>(user);
        }


        //Register
        public async Task<ServiceResult<IdentityUser>> Register(RegisterRequest request)
        {
            var user = new IdentityUser(request.Email);
            user.Email = request.Email;
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var serviceResult = new ServiceResult<IdentityUser>();
                foreach (var error in result.Errors)
                {
                    serviceResult.Messages.Add(new ServiceMessage
                    {
                        Code = error.Code,
                        Description = error.Description,
                        Type = ServiceMessageType.Error
                    });
                }

                return serviceResult;
            }

            return new ServiceResult<IdentityUser>(user);

        }
    }
}
