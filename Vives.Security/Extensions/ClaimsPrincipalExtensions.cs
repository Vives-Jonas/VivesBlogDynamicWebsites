namespace Vives.Security.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this System.Security.Claims.ClaimsPrincipal principal)
        {
            var userIdClaim = principal?.FindFirst("Id");

            if (userIdClaim is null)
            {
                return null;
            }

            return Guid.TryParse(userIdClaim.Value, out var userId) ? userId : null;
        }
    }
}
