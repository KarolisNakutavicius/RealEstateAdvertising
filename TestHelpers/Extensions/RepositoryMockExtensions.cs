using System.Linq.Expressions;
using Domain.Entities.Contracts;
using Domain.Services.Contracts;
using MockQueryable.Moq;
using Moq;

namespace TestHelpers.Extensions;

public static class RepositoryMockExtensions
{
    public static void OnGetAllReturnEntity<T>(this Mock<IRepository<T>> repoMock, T entity, bool noTracking = true)
        where T : class, IAggregateRoot
    {
        repoMock.SetupGetAllReturnEntities(new List<T> { entity }, noTracking);
    }

    public static void SetupGetAllReturnEntities<T>(this Mock<IRepository<T>> repoMock, IList<T> entities, bool noTracking = true)
        where T : class, IAggregateRoot
    {
        repoMock.Setup(m => m.GetAll(It.IsAny<Expression<Func<T, bool>>>(), noTracking))
            .Returns(
                (Expression<Func<T, bool>> p, bool _) =>
                    entities.AsQueryable().Where(p).BuildMock());

        repoMock.Setup(m => m.GetAll(noTracking))
            .Returns(entities.AsQueryable().BuildMock());
    }
}