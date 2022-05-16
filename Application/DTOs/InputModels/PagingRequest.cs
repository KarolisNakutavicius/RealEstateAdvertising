using Application.Enums;

namespace Application.DTOs.InputModels;

public class PagingRequest
{
    public int PageIndex { get; set; } = 0;

    public int PageSize { get; set; } = 10;

    public SortBy SortBy { get; set; } = SortBy.None;
}