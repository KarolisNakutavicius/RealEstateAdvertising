using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs.InputModels;
using Application.Services;
using Application.Services.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using TestHelpers.Extensions;

namespace Application.Tests.Services;

public class AdvertisementServiceTests
{
    private Mock<IRepository<Advertisement>> _adRepoMock;
    private Mock<IRepository<City>> _cityRepoMock;
    private Mock<IContextService> _contextServiceMock;

    private CreateAdvertisementRequest _request;
    private AdvertisementService _sut;

    [SetUp]
    public void Setup()
    {
        _adRepoMock = new Mock<IRepository<Advertisement>>();
        _cityRepoMock = new Mock<IRepository<City>>();
        _contextServiceMock = new Mock<IContextService>();

        _contextServiceMock.Setup(c => c.GetCurrentUserAsync()).ReturnsAsync(new User { Id = "1" });

        _cityRepoMock.OnGetAllReturnEntity(new City { Id = 1, Name = "Vilnius" });
        
        _request = new CreateAdvertisementRequest
        {
            City = "Kaunas",
            Name = "Very good ad",
            Price = 40000M,
            Description = "One of the best buildings",
            Street = "Latvių g.",
            Type = BuildingType.Residential,
            Zip = "08611",
            Number = 5,
            BuildingSize = 44,
            PlotSize = 20,
        };

        _sut = new AdvertisementService(_contextServiceMock.Object,
            _adRepoMock.Object,
            _cityRepoMock.Object);
    }

    [Test]
    public async Task CreateNewAdvertisement_AllSet_SaveToRepoCalled()
    {
        _contextServiceMock.Setup(m => m.GetCurrentUserAsync()).ReturnsAsync((User)null);

        await _sut.CreateNewAdvertisement(_request, CancellationToken.None);

        _adRepoMock.Verify(a => a.Save(It.Is<Advertisement>(
            x => x.Description.Equals(_request.Description)
                 && x.Title.Equals(_request.Name)
                 && x.Price.Equals(_request.Price)
                 && x.Building.Address.City.Name == _request.City
                 && x.Building.Address.Street == _request.Street
                 && x.Building.Category == _request.Type
                 && x.Building.Address.Zip == _request.Zip
                 && x.Building.Address.Number == _request.Number
                 && x.Building.Size.BuildingSize == _request.BuildingSize
                 && x.Building.Size.PlotSize == _request.PlotSize
                 && x.Image == null
        ), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateNewAdvertisement_CityAlreadyExists_RelatesToExistingCity()
    {
        _contextServiceMock.Setup(m => m.GetCurrentUserAsync()).ReturnsAsync((User)null);
        _request.City = "Vilnius";

        await _sut.CreateNewAdvertisement(_request, CancellationToken.None);

        _adRepoMock.Verify(a => a.Save(It.Is<Advertisement>(
            x => x.Building.Address.City.Id == 1
                 && x.Building.Address.City.Name == _request.City
        ), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateNewAdvertisement_FileDoesNotExists_Continue()
    {        
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));
        _request.Files = new List<IFormFile>()
        {
            new FormFile(stream, 0, 1, "File", "FileName")
        };
        
        await _sut.CreateNewAdvertisement(_request, CancellationToken.None);

        _adRepoMock.Verify(a => a.Save(It.Is<Advertisement>(
            x => x.Image != null
        ), It.IsAny<CancellationToken>()), Times.Once);
    }
}