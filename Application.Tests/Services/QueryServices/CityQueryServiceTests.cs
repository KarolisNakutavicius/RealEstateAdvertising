using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.QueryServices;
using Domain.Entities;
using Domain.Services.Contracts;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Services.QueryServices;

public class CityQueryServiceTests
{
    private Mock<IRepository<City>> _cityRepoMock;
    private CityQueryService _sut;

    [SetUp]
    public void Setup()
    {
        _cityRepoMock = new Mock<IRepository<City>>();

        _sut = new CityQueryService(_cityRepoMock.Object);
    }

    [Test]
    public async Task GetAll_AllSet_ReturnsAll()
    {
        var cities = new List<City>
        {
            new()
            {
                Id = 1,
                Name = "Vilnius"
            },
            new()
            {
                Id = 2,
                Name = "Kaunas"
            },
            new()
            {
                Id = 3,
                Name = "Klaipėda"
            }
        };
        _cityRepoMock.Setup(m => m.GetAll(true)).Returns(cities.AsQueryable().BuildMock());

        var result = await _sut.GetAll(CancellationToken.None);

        result.Should().HaveCount(3);
        result.All(r => cities.Any(c => c.Id == r.Id && c.Name == r.Name)).Should().BeTrue();
    }
}