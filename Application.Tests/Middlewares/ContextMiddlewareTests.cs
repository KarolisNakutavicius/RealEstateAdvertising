using System.Threading.Tasks;
using Application.Middlewares;
using Application.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Middlewares;

public class ContextMiddlewareTests
{
    private ContextMiddleware _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new ContextMiddleware();
    }

    [Test]
    public async Task InvokeAsync_Called_GetCurrentUserCalled()
    {
        var context = new Mock<IContextService>();

        await _sut.InvokeAsync(context.Object);

        context.Verify(c => c.GetCurrentUserAsync(), Times.Once);
    }
}