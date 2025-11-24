using Vives.Security;
using Vives.Security.Extensions;

namespace VivesBlog.Api.Security
{
    public class HttpContextUserContext(IHttpContextAccessor httpContextAccessor): IUserContext<Guid>
    {
        public Guid? UserId => httpContextAccessor.HttpContext?.User.GetUserId();
    }
}
