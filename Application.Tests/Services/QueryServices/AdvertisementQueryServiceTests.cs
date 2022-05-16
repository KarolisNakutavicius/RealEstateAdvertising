using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.InputModels;
using Application.Resources;
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

    private User _user;
    private List<Advertisement> _advertisements;

    [SetUp]
    public void Setup()
    {
        _filterServiceMock = new Mock<IFilterService>();
        _contextServiceMock = new Mock<IContextService>();
        _advertisementRepoMock = new Mock<IRepository<Advertisement>>();
        
        _user = new User
        {
            UserName = "Studentas132",
            Id = "1"
        };
        _advertisements = new List<Advertisement>
        {
            new()
            {
                Id = 1,
                Owner = _user,
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
            new() { Id = 2, Owner = _user },

        };
        _advertisementRepoMock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Advertisement, bool>>>(), true))
            .Returns((Expression<Func<Advertisement, bool>> p, bool _) =>
                _advertisements.AsQueryable().Where(p).BuildMock());
        _advertisementRepoMock.Setup(m => m.GetAll(true))
            .Returns(_advertisements.AsQueryable().BuildMock());
        _contextServiceMock.Setup(m => m.GetCurrentUserAsync()).ReturnsAsync(_user);
        

        _sut = new AdvertisementQueryService(_contextServiceMock.Object, _advertisementRepoMock.Object,
            _filterServiceMock.Object);
    }

    [Test]
    public async Task GetAllUsersAdvertisements_UserLoggedIn_OnlyUsersAdsReturned()
    {
        _advertisements.Add(new() { Id = 3, Owner = new User { Id = "404" } });
        var result = await _sut.GetAllUsersAdvertisements(CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().Contain(r => r.Id == 1);
        result.Should().Contain(r => r.Id == 2);
    }

    [Test]
    public async Task GetAll_NoFilters_ReturnsAllAds()
    {
        _filterServiceMock.Setup(m => m.FilterDown(It.IsAny<IQueryable<Advertisement>>(), It.IsAny<FilterRequest>()))
            .Returns(Result<IQueryable<Advertisement>>.Ok(_advertisements.AsQueryable().BuildMock()));
        
        var result = await _sut.GetAll(new FilterRequest(), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(2);
        result.Data.Should().Contain(r => r.Id == 1);
        result.Data.Should().Contain(r => r.Id == 2);
    }
    
    [Test]
    public async Task GetAll_NoFiltersAndUserLoggedIn_ReturnOnlyOtherUsersAds()
    {
        var otherAds = new List<Advertisement>
        {
            new Advertisement() { Id = 4, }, new Advertisement() { Id = 4, }, new Advertisement() { Id = 5, },
        };
        _advertisements.AddRange(otherAds);
        _filterServiceMock.Setup(m => m.FilterDown(It.IsAny<IQueryable<Advertisement>>(), It.IsAny<FilterRequest>()))
            .Returns(Result<IQueryable<Advertisement>>.Ok(_advertisements.AsQueryable().BuildMock()));
        _contextServiceMock.Setup(m => m.IsAuthenticated)
            .Returns(true);
        
        var result = await _sut.GetAll(new FilterRequest(), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(3);
        result.Data.Should().Contain(r => r.Id == 4);
        result.Data.Should().Contain(r => r.Id == 5);
        result.Data.Should().Contain(r => r.Id == 6);
    }

    [Test]
    public async Task GetAll_FiltersApplied_ReturnOnlyFiltered()
    {   var otherAds = new List<Advertisement>
        {
            new Advertisement() { Id = 8, }, new Advertisement() { Id = 9, }, new Advertisement() { Id = 10, },
        };
        _advertisements.AddRange(otherAds);
        _filterServiceMock.Setup(m => m.FilterDown(It.IsAny<IQueryable<Advertisement>>(), It.IsAny<FilterRequest>()))
            .Returns(Result<IQueryable<Advertisement>>.Ok(otherAds.AsQueryable().BuildMock()));
        
        var result = await _sut.GetAll(new FilterRequest(), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Data.Should().HaveCount(3);
        result.Data.Should().Contain(r => r.Id == 8);
        result.Data.Should().Contain(r => r.Id == 9);
        result.Data.Should().Contain(r => r.Id == 10);
    }
    
    [Test]
    public async Task GetAll_FiltersFailed_ErrorReturned()
    {
        var error = ErrorMessages.MinPriceHigherThanMax;
        _filterServiceMock.Setup(m => m.FilterDown(It.IsAny<IQueryable<Advertisement>>(), It.IsAny<FilterRequest>()))
            .Returns(Result<IQueryable<Advertisement>>.Fail(error));
        
        var result = await _sut.GetAll(new FilterRequest(), CancellationToken.None);

        result.Success.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Error == error);
    }
}