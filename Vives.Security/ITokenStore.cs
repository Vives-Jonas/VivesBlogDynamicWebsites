namespace Vives.Security
{
    public interface ITokenStore
    {
        public string GetToken();
        public void SetToken(string token);
        public void Clear();
    }
}
