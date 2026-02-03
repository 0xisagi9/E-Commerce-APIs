using System.Linq.Expressions;

namespace E_Commerce_APIs.Shared.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
