using Application.DTOs;
using Application.DTOs.InputModels;
using Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers;

public static class PagingHelper
{
    public static async Task<PageDto<T>> AddPagingAsync<T>(
        IQueryable<T> entities,
        PagingRequest pagingRequest,
        CancellationToken cancellationToken) where T : IAggregateRoot
    {
        var pageSize = pagingRequest.PageSize;
        var pageIndex = pagingRequest.PageIndex;
        
        var totalRecords = await entities.CountAsync(cancellationToken);
        var totalPages = 0;
        
        if (pageSize > 0)
        {
            totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
            {
                totalPages++;
            }
            

            entities = entities.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        return new PageDto<T>
        {
            Items = await entities.ToListAsync(cancellationToken),
            CurrentPage = pageIndex,
            PageSize = pageSize,
            TotalRecordsCount = totalRecords,
            TotalPages = totalPages
        };
    }
    
    public static async Task<PageDto<TResponse>> AddPaging<T, TResponse>(
        IQueryable<T> entities,
        Func<T, TResponse> dataSelector,
        PagingRequest pagingRequest) where T : IAggregateRoot
    {
        var pageSize = pagingRequest.PageSize;
        var pageIndex = pagingRequest.PageIndex;
        
        var totalRecords = entities.Count();
        var totalPages = 0;

        if (pageSize == 0)
        {
            return new PageDto<TResponse>
            {
                Items = (await entities.ToListAsync()).Select(dataSelector),
                CurrentPage = pageIndex,
                PageSize = pageSize,
                TotalRecordsCount = totalRecords,
                TotalPages = totalPages
            };
        }

        totalPages = totalRecords / pageSize;

        if (totalRecords % pageSize > 0)
        {
            totalPages++;
        }

        entities = entities.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        
        return new PageDto<TResponse>
        {
            Items = (await entities.ToListAsync()).Select(dataSelector),
            CurrentPage = pageIndex,
            PageSize = pageSize,
            TotalRecordsCount = totalRecords,
            TotalPages = totalPages
        };
    }
}