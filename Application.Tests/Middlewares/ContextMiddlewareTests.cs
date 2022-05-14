using System.Security.Authentication;
using System.Threading.Tasks;
using Application.Middlewares;
using Application.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Middlewares;

public class ContextMiddlewareTests
{
    private ContextMiddleware _sut;
    private Mock<IContextService> _contextServiceMock;

    [SetUp]
    public void Setup()
    {
        _contextServiceMock = new Mock<IContextService>();
        
        _sut = new ContextMiddleware();
    }

    [Test]
    public async Task InvokeAsync_Called_GetCurrentUserCalled()
    {
        await _sut.InvokeAsync(_contextServiceMock.Object);

        _contextServiceMock.Verify(c => c.GetCurrentUserAsync(), Times.Once);
    }
    
    [Test]
    public async Task InvokeAsync_Throws_DoesNotCrash()
    {
        _contextServiceMock.Setup(m => m.GetCurrentUserAsync()).ThrowsAsync(new AuthenticationException());
        
        await _sut.InvokeAsync(_contextServiceMock.Object);
    }
}