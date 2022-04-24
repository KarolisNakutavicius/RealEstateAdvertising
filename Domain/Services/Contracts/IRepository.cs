using System.Linq.Expressions;
using Domain.Entities.Contracts;

namespace Domain.Services.Contracts
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        // Query methods 
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool noTracking = false);

        IQueryable<T> GetAll(bool noTracking = false);

        // Command methods 
        Task<T> Save(T aggregate, CancellationToken cancellationToken);

        // Transactional 
        Task Commit(CancellationToken cancellationToken);
    }
}
