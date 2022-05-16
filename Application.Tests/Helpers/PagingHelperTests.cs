using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs.InputModels;
using Application.Helpers;
using Domain.Entities;
using Domain.Entities.Contracts;
using FluentAssertions;
using MockQueryable.Moq;
using NUnit.Framework;

namespace Application.Tests.Helpers;

public class PagingHelperTests
{
    [TestCase(0, 0, 4, 0)]
    [TestCase(0, 1, 1, 4)]
    [TestCase(0, 2, 2, 2)]
    [TestCase(1, 2, 2, 2)]
    public async Task AddPagingAsync_AllSet_ExpectedPageDtoReturned(int pageIndex, int pageSize, int expectedCount,
        int totalPages)
    {
        var entityList = new List<IAggregateRoot>
        {
            new Advertisement(),
            new Advertisement(),
            new Advertisement(),
            new Advertisement()
        }.AsQueryable().BuildMock();
        var pageRequest = new PagingRequest()
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        

        var result = await PagingHelper.AddPagingAsync(entityList, pageRequest, CancellationToken.None);

        result.CurrentPage.Should().Be(pageIndex);
        result.PageSize.Should().Be(pageSize);
        result.TotalRecordsCount.Should().Be(entityList.Count());
        result.Items.Should().HaveCount(expectedCount);
        result.TotalPages.Should().Be(totalPages);
    }
}