using Vives.Security;

namespace VivesBlog.Blazor.Stores
{
    public class TokenStore : ITokenStore
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public string GetToken()
        {
            return string.Empty;
        }

        public void SetToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
