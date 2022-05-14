using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Services;

public class ContextServiceTests
{
    private ContextService _sut;
    
    private Mock<HttpContext> _httpContextMock;
    private Mock<UserManager<User>> _userManagerMock;

    private Mock<ClaimsIdentity> _identity;
    private User _user;


    [SetUp]
    public void Setup()
    {
        _httpContextMock = new Mock<HttpContext>();

        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        
        var userId = "testUserId";
        _user = new User
        {
            Id = userId
        };
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, userId) };
        _identity = new Mock<ClaimsIdentity>( claims );
        _identity.Setup(i => i.IsAuthenticated).Returns(true);
        _userManagerMock.Setup(m => m.FindByIdAsync(It.Is<string>(c => c == userId))).ReturnsAsync(_user);
        _httpContextMock.Setup(m => m.User.Identity).Returns(_identity.Object);
        _httpContextMock.Setup(m => m.User.Claims).Returns(claims);
        
        _sut = new ContextService(_userManagerMock.Object, _httpContextMock.Object);
    }
    
    [Test]
    public async Task GetCurrentUserAsync_IdentitySetAndUserFound_UserSetToContext()
    {
        var result = await _sut.GetCurrentUserAsync();

        result.Should().NotBeNull();
        result.Should().Be(_user);
        _sut.IsAuthenticated().Should().BeTrue();
    }
    
    [Test]
    public async Task GetCurrentUserAsync_NotAuthenticated_UserContextNotCalled()
    {
        _identity.Setup(i => i.IsAuthenticated).Returns(false);

        var result = await _sut.GetCurrentUserAsync();
        
        result.Should().BeNull();
    }
    
    [Test]
    public async Task GetCurrentUserAsync_UserWithIdNotFound_UserContextNotCalled()
    {
        _userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        
        var result = await _sut.GetCurrentUserAsync();
        
        result.Should().BeNull();
    }

    [Test]
    public void IsUserAuthenticated_UserNotSet_ReturnFalse()
    {
        var result = _sut.IsAuthenticated();

        result.Should().BeFalse();
    }
    
    [Test]
    public async Task IsUserAuthenticated_UserSet_ReturnTrue()
    {
        await _sut.GetCurrentUserAsync();

        var result = _sut.IsAuthenticated();
        
        result.Should().BeTrue();
    }
    
    [Test]
    public async Task GetCurrentUserAsync_AlreadyRetrieved_DoesNotCallContextAgain()
    {
        await _sut.GetCurrentUserAsync();
        await _sut.GetCurrentUserAsync();

        _httpContextMock.Verify(c => c.User.Claims, Times.Once);
    }
}