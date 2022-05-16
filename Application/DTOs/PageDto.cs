namespace Application.DTOs;

public class PageDto<T>
{
    public IEnumerable<T> Items { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalRecordsCount { get; set; }

    public int TotalPages { get; set; }
}