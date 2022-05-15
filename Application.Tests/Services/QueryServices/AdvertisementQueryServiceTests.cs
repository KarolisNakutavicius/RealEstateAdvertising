using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Contracts;
using Application.Services.QueryServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Contracts;
using Domain.ValueObjects;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Services.QueryServices;

public class AdvertisementQueryServiceTests
{
    private Mock<IRepository<Advertisement>> _advertisementRepoMock;
    private Mock<IContextService> _contextServiceMock;
    private Mock<IFilterService> _filterServiceMock;
    private AdvertisementQueryService _sut;

    [SetUp]
    public void Setup()
    {
        _filterServiceMock = new Mock<IFilterService>();
        _contextServiceMock = new Mock<IContextService>();
        _advertisementRepoMock = new Mock<IRepository<Advertisement>>();

        _sut = new AdvertisementQueryService(_contextServiceMock.Object, _advertisementRepoMock.Object,
            _filterServiceMock.Object);
    }

    [Test]
    public async Task GetAllUsersAdvertisements_UserLoggedIn_AdsReturned()
    {
        var user = new User
        {
            UserName = "Studentas132",
            Id = "1"
        };
        var ads = new List<Advertisement>
        {
            new()
            {
                Id = 1,
                Owner = user,
                Title = "Very good ad",
                Price = 40000M,
                Description = "One of the best buildings",
                Building = new Building
                {
                    Id = 2,
                    Address = Address.CreateNew("Latvių g.", 5, new City { Id = 1, Name = "Vilnius" }, "08611"),
                    Category = BuildingType.Residential,
                    Size = 44
                }
            },
            new() { Id = 2, Owner = user },
            new() { Id = 3, Owner = new User { Id = "404" } }
        };
        _advertisementRepoMock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Advertisement, bool>>>(), true))
            .Returns((Expression<Func<Advertisement, bool>> p, bool _) =>
                ads.AsQueryable().Where(p).BuildMock());
        _contextServiceMock.Setup(m => m.GetCurrentUserAsync()).ReturnsAsync(user);

        var result = await _sut.GetAllUsersAdvertisements(CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().Contain(r => r.Id == 1);
        result.Should().Contain(r => r.Id == 2);
    }
}