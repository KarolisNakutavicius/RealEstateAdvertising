using Domain.Entities.Contracts;
using Domain.Services.Contracts;

namespace Infrastructure.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll(bool noTracking = false)
        {
            throw new NotImplementedException();
        }

        public void Save(T aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
