using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Application.Middlewares;
using Application.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Middlewares;

public class ContextMiddlewareTests
{
    private Mock<IContextService> _contextServiceMock;

    private Mock<HttpContext> _httpContextMock;
    private ContextMiddleware _sut;
    private Mock<UserManager<User>> _userManagerMock;

    private Mock<ClaimsIdentity> _identity;
    private User _user;

    [SetUp]
    public void Setup()
    {
        _httpContextMock = new Mock<HttpContext>();
        _contextServiceMock = new Mock<IContextService>();
        
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
        
        _sut = new ContextMiddleware(_userManagerMock.Object, _contextServiceMock.Object);
    }

    [Test]
    public async Task InvokeAsync_IdentitySetAndUserFound_UserSetToContext()
    {
        await _sut.InvokeAsync(_httpContextMock.Object);
        
        _contextServiceMock.Verify(c => c.SetUser(_user), Times.Once);
    }
    
    [Test]
    public async Task InvokeAsync_NotAuthenticated_UserContextNotCalled()
    {
        _identity.Setup(i => i.IsAuthenticated).Returns(false);

        await _sut.InvokeAsync(_httpContextMock.Object);
        
        _contextServiceMock.Verify(c => c.SetUser(_user), Times.Never);
    }
    
    [Test]
    public async Task InvokeAsync_UserWithIdNotFound_UserContextNotCalled()
    {
        _userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        
        await _sut.InvokeAsync(_httpContextMock.Object);
        
        _contextServiceMock.Verify(c => c.SetUser(_user), Times.Never);
    }
}