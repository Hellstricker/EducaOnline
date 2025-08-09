using EducaOnline.Core.DomainObjects;

namespace EducaOnline.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
