using System.Linq.Expressions;
using Domain.Entities.Contracts;
using Domain.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly RealEstateAdvertisingDbContext _dbContext;

        public Repository(RealEstateAdvertisingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool noTracking = false)
        {
            return GetAll(noTracking).Where(predicate);
        }

        public IQueryable<T> GetAll(bool noTracking = false)
        {
            IQueryable<T> dbSet = _dbContext.Set<T>();

            if (noTracking)
            {
                dbSet = dbSet.AsNoTrackingWithIdentityResolution();
            }

            return dbSet;
        }

        public async Task<T> Save(T aggregate, CancellationToken cancellationToken)
        {
            var result = (await _dbContext.Set<T>().AddAsync(aggregate, cancellationToken)).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
