using Domain.Entities.Contracts;

namespace Domain.Services.Contracts
{
    internal interface IRepository<T> where T : IAggregateRoot
    {
        // Query methods 
        IQueryable<T> GetAll(bool noTracking = false);

        // Command methods 
        void Save(T aggregate);

        // Transactional 
        void Commit();
    }
}
