using LibraryCult.Core.DomainObjects;

namespace LibraryCult.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
