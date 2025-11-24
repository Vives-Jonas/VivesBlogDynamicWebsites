namespace Vives.Security
{
    public interface IUserContext<T> where T: struct
    {
        public T? UserId { get; }
    }

    public interface IUserContext : IUserContext<int>
    {
    }
}
